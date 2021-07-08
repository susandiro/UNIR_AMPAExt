using System;
using System.Collections.Generic;
using AMPA.Modelo;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Actividades
{
    public partial class GestionActividades : PageBase
    {
        #region propiedades privadas
        /// <summary>
        /// Mensaje de error generado
        /// </summary>
        private string _MensajeError = string.Empty;
        /// <summary>
        /// Página base en la que se encuentra la página actual
        /// </summary>
        private PageBase _Page { get { return Page as PageBase; } }

        /// <summary>
        /// Filtro guardado en sesión para conservar las búsquedas al volver de un detalle
        /// </summary>
        private Comun.FiltroActividad FiltroInicial
        {
            get { return (Session["FiltroActividad"] == null) ? null : (Comun.FiltroActividad)Session["FiltroActividad"]; }
            set { Session["FiltroActividad"] = value; }
        }

        #endregion

        #region Eventos
        /// <summary>
        /// Evento producido al cargar la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    CargarEmpresas();
                    CargarAMPA();
                    if (FiltroInicial != null)
                        CargarFiltro();
                    Session["IdEmpresa"] = null;
                    Session["IdActividad"] = null;
                    Session["listadoHorario"] = null;
                    Session["listadoDescuento"] = null;
                    CargarActivo();
                    CargarGrid();
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".Page_load()", ex);
                _Page.Error(_MensajeError);
            }
        }

        #endregion

        /// <summary>
        /// Evento producido al pulsar sobre el botón limpiar del filtro. Carga el grid como en el estado inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                cmbEmpresa.ClearSelection();
                cmbAMPA.ClearSelection();
                cmbActivo.ClearSelection();
                txtNombre.Text = string.Empty;
                Session.Remove("FiltroActividad");
                CargarGrid();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al limpiar los datos de filtro", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "GestorActi", "alert('Se ha producido un error al limpiar los datos del filtro');", true);
            }
        }
        /// <summary>
        /// Evento producido al pulsar sobre el botón filtrar del filtro. Carga el grid con los criterios indicados en el filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarGrid();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al filtrar", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "GestorAct", "alert('Se ha producido un error al buscar en el listado');", true);
            }
        }

        #region Eventos del grid
        /// <summary>
        /// Recarga el grid después de la paginación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridPaginacion_RecargarGrid(object sender, EventArgs e)
        {
            //FiltroInicial.Paginacion = gridPaginacion.Paginacion;
            //CargarGrid();
        }
        /// <summary>
        /// Procedimiento de la grid, al crear una fila
        /// </summary>
        protected void gvActividad_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.background='#FFF0A2'");
                e.Row.Attributes.Add("onmouseout", "this.style.background='#FFFFFF'");
            }
        }

        /// <summary>
        /// Procedimiento de la grid, al ejecutar un comando
        /// </summary>
        protected void gvActividad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (gvActividad.Rows.Count > 0)
            {
                //Se recupera el identificador de la actividad
                int idActividad = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Consulta":
                        Session["IdActividad"] = idActividad;
                        Response.Redirect(_Page.MasterBase.RelativeURL + "/Actividades/DetalleActividad.aspx?grid=S", false);
                        break;
                    case "modificar":
                        Session["IdActividad"] = idActividad;
                        Response.Redirect(_Page.MasterBase.RelativeURL + "/Actividades/ModificarActividad.aspx?grid=S", false);
                        break;
                    case "Baja":
                        try
                        {
                            List<ALUMNO_ACTIVIDAD> alumnos = NegActividad.GetAlumnnosByActividad(idActividad);
                            if (alumnos != null && alumnos.Count > 0)
                                ScriptManager.RegisterStartupScript(Page, GetType(), "gestor", "if(!confirm('Se han encontrado alumnos asociados a la actividad. En caso de continuar, los alumnos también se darán de baja en la actividad, ¿desea continuar?')){return false;} else {document.getElementById('" + BtnConfirmar.ClientID + "').click();}", true);
                            else //Se borra
                            {
                                ACTIVIDAD actividad = NegActividad.GetActividadById(idActividad);
                                actividad.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;
                                actividad.FECHA_MOD = DateTime.Now;
                                if (!NegActividad.BajaActividad(actividad))
                                {
                                    Comun.Log.TrazaLog.Error("No se ha podido dar de baja la actividad " + idActividad.ToString());
                                    Error("Se ha producido un error al intentar dar de baja la actividad");
                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".gvActividad_RowCommand().Baja(IdActividad: " + idActividad.ToString() + "). Descripcion: " + ex.ToString());
                            ScriptManager.RegisterStartupScript(Page, GetType(), "gestor", "alert('Se ha producido un error al dar de baja la actividad extraescolar');", true);
                            return;
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", "alert('La actividad ha sido dada de baja correctamente.');", true);
                        //Se carga el grid de nuevo para que desaparezca el usuario borrado
                        CargarGrid();
                        break;
                }
            }
        }

        #endregion

        /// <summary>
        /// Asigna los valores del filtro
        /// </summary>
        /// <returns></returns>
        private Comun.FiltroActividad SetFiltro()
        {
            Comun.FiltroActividad filtro = new Comun.FiltroActividad();
            try
            {
                filtro.Vacio = true;
                if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
                {
                    filtro.Vacio = false;
                    filtro.Nombre = txtNombre.Text.Trim();
                }

                if (!string.IsNullOrEmpty(cmbActivo.SelectedValue))
                {
                    filtro.Vacio = false;
                    filtro.Activo = cmbActivo.SelectedValue;
                }

                if (!string.IsNullOrEmpty(cmbEmpresa.SelectedValue))
                {
                    filtro.Vacio = false;
                    filtro.IdEmpresa = int.Parse(cmbEmpresa.SelectedValue);
                }

                if (!string.IsNullOrEmpty(cmbAMPA.SelectedValue))
                {
                    filtro.Vacio = false;
                    filtro.IdAMPA = int.Parse(cmbAMPA.SelectedValue);
                }
                FiltroInicial = filtro;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".SetFiltro(). Descripcion; ", ex);
                _Page.Error("Ha ocurrido un error al establecer el filtro de la página");
            }
            return filtro;
        }
        /// <summary>
        /// Asigna los valores del filtro en función de lo que venga de sesion
        /// </summary>
        private void CargarFiltro()
        {
            Comun.FiltroActividad filtro = FiltroInicial;
            try
            {
                if (!string.IsNullOrEmpty(filtro.Nombre))
                    txtNombre.Text = filtro.Nombre;

                if (!string.IsNullOrEmpty(filtro.Activo))
                    cmbEmpresa.SelectedValue = filtro.Activo;

                if (filtro.IdEmpresa > 0)
                    cmbEmpresa.SelectedValue = filtro.IdEmpresa.ToString();
                if (filtro.IdAMPA > 0)
                    cmbAMPA.SelectedValue = filtro.IdAMPA.ToString();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".CargarFiltro(). Descripcion; ", ex);
                _MensajeError = "Ha ocurrido un error al establecer el filtro de la página";
                throw;
            }
        }
        /// <summary>
        /// Procedimiento de la carga el listado
        /// </summary>
        private void CargarGrid()
        {
            try
            {
                List<ACTIVIDAD> listado = new List<ACTIVIDAD>();
                Comun.FiltroActividad filtro = new Comun.FiltroActividad();
                filtro = SetFiltro();
                listado = NegActividad.GetActividades(filtro);
                gvActividad.DataSource = listado;
                gvActividad.DataBind();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar el grid", ex);
                throw;
            }
        }

        /// <summary>
        /// Carga el combo de las empresas extraescolares. Si es un usuario de AMPA, carga las empresas extraescolares con la que trabaja. Si es un usuario
        /// de extraescolar, carga sólo su empresa.
        /// </summary>
        private void CargarEmpresas()
        {
            try
            {
                cmbEmpresa.Items.Clear();
                if (MasterBase.DatosSesionLogin.CodTipoUsuario != Comun.TipoDatos.TipoUsuario.AMPA)
                {
                    cmbEmpresa.Items.Add(new ListItem(MasterBase.DatosSesionLogin.Empresa, MasterBase.DatosSesionLogin.IdEmpresa.ToString()));
                    cmbEmpresa.Enabled = false;
                }
                else
                {
                    List<EMPRESA_AMPA> listado = new List<EMPRESA_AMPA>();
                    listado = NegUsuario.GetEmpresasByAMPA(MasterBase.DatosSesionLogin.IdEmpresa, true);
                    cmbEmpresa.Items.Add(new ListItem("-- Seleccione --", ""));
                    foreach (EMPRESA_AMPA empresa in listado)
                        cmbEmpresa.Items.Add(new ListItem(empresa.EMPRESA.NOMBRE, empresa.ID_EMPRESA.ToString()));
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de las empresas extraescolares.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de empresas extraescolares');", true);
            }
        }

        /// <summary>
        /// Carga el combo de las AMPA. Si es un usuario de AMPA, carga su AMPA. Si es un usuario de extraescolar, carga las AMPA con las que trabaja
        /// </summary>
        private void CargarAMPA()
        {
            try
            {
                cmbAMPA.Items.Clear();
                if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.AMPA)
                {
                    cmbAMPA.Items.Add(new ListItem(MasterBase.DatosSesionLogin.Empresa, MasterBase.DatosSesionLogin.IdEmpresa.ToString()));
                    cmbAMPA.Enabled = false;
                }
                else
                {
                    List<EMPRESA_AMPA> listado = new List<EMPRESA_AMPA>();
                    listado = NegUsuario.GetAMPASByEmpresa(MasterBase.DatosSesionLogin.IdEmpresa);
                    cmbAMPA.Items.Add(new ListItem("-- Seleccione --", ""));
                    foreach (EMPRESA_AMPA empresa in listado)
                        cmbAMPA.Items.Add(new ListItem(empresa.AMPA.NOMBRE, empresa.ID_AMPA.ToString()));
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de las AMPA.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de AMPA');", true);
            }
        }

        /// <summary>
        /// Carga el combo Activo del filtro
        /// </summary>
        private void CargarActivo()
        {
            cmbActivo.Items.Clear();
            cmbActivo.Items.Add(new ListItem("-- Seleccione --", ""));
            cmbActivo.Items.Add(new ListItem("S", "S"));
            cmbActivo.Items.Add(new ListItem("N", "N"));
        }

        protected void BtnConfirmar_Click(object sender, EventArgs e)
        {

        }
    }
}
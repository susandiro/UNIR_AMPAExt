using System;
using System.Collections.Generic;
using AMPA.Modelo;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Extraescolar
{
    public partial class MantenimientoExt : PageBase
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
        private Comun.FiltroUsuario FiltroInicial
        {
            get { return (Session["FiltroUsuario"] == null) ? null : (Comun.FiltroUsuario)Session["FiltroUsuario"]; }
            set { Session["FiltroUsuario"] = value; }
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
                    CargarActivo();
                    if (FiltroInicial != null)
                        CargarFiltro();
                    Session["IdEmpresa"] = null;
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
                txtNIF.Text = string.Empty;
                txtNombre.Text = string.Empty;
                Session.Remove("FiltroUsuario");
                CargarGrid();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al limpiar los datos de filtro", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "MantenimientoExt", "alert('Se ha producido un error al limpiar los datos del filtro');", true);
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
                ScriptManager.RegisterStartupScript(Page, GetType(), "GestorUsuario", "alert('Se ha producido un error al buscar en el listado');", true);
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
        protected void gvEmpresas_RowCreated(object sender, GridViewRowEventArgs e)
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
        protected void gvEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (gvEmpresas.Rows.Count > 0)
            {
                //Se recupera el identificador del usuario
                int idEmpresa = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Consulta":
                        Session["IdEmpresa"] = e.CommandArgument.ToString();
                        Response.Redirect(_Page.MasterBase.RelativeURL + "/Extraescolar/DetalleEmpresaExt.aspx?grid=S", false);
                        break;
                    case "Modificar":
                        Session["IdEmpresa"] = e.CommandArgument.ToString();
                        Response.Redirect(_Page.MasterBase.RelativeURL + "/Extraescolar/ModificarEmpresaExt.aspx?grid=S", false);
                        break;
                    case "Baja":
                        try
                        {
                            //TODO:FAlta esto
                                if (!NegUsuario.BajaUsuarioAMPA(idEmpresa, MasterBase.DatosSesionLogin.IdEmpresa))
                                {
                                    Comun.Log.TrazaLog.Error("No se ha podido dar de baja el usuario " + idEmpresa.ToString());
                                    Error("Se ha producido un error al intentar dar de baja al usuario para la AMPA");
                                    return;
                                }
                        }
                        catch (Exception ex)
                        {
                            Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".gvEmpresas_RowCommand().Baja(IdEmpresa: " + idEmpresa.ToString() + "). Descripcion: " + ex.ToString());
                            ScriptManager.RegisterStartupScript(Page, GetType(), "gestor", "alert('Se ha producido un error al dar de baja la empresa');", true);
                            return;
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", "alert('La empresa ha sido dada de baja correctamente.');", true);
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
        private Comun.FiltroUsuario SetFiltro()
        {
            Comun.FiltroUsuario filtro = new Comun.FiltroUsuario();
            try
            {
                filtro.Vacio = true;
                if (!string.IsNullOrEmpty(txtNIF.Text.Trim()))
                {
                    filtro.Vacio = false;
                    filtro.NumeroDocumento = txtNIF.Text.Trim();
                }

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
            Comun.FiltroUsuario filtro = FiltroInicial;
            try
            {
                if (!string.IsNullOrEmpty(filtro.NumeroDocumento))
                    txtNIF.Text = filtro.NumeroDocumento;

                if (!string.IsNullOrEmpty(filtro.Nombre))
                    txtNombre.Text = filtro.Nombre;

                if (!string.IsNullOrEmpty(filtro.Activo))
                    cmbActivo.SelectedValue = filtro.Activo;

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
                List<EMPRESA_AMPA> listado = new List<EMPRESA_AMPA>();
                Comun.FiltroUsuario filtro = new Comun.FiltroUsuario();
                filtro = SetFiltro();
                listado = NegExtraescolar.GetEmpresasbyIdAMPA(MasterBase.DatosSesionLogin.IdEmpresa, filtro);
                gvEmpresas.DataSource = listado;
                gvEmpresas.DataBind();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar el grid", ex);
                throw;
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
    }
}
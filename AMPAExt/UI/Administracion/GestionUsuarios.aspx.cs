using System;
using System.Collections.Generic;
using AMPA.Modelo;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Administracion
{
    public partial class GestionUsuarios : PageBase
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
                    CargarTipoDocumento();
                    if (FiltroInicial != null)
                        CargarFiltro();
                    Session["IdUsuario"] = null;
                    CargarGrid();
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".Page_load()", ex);
                _Page.ErrorGeneral(_MensajeError);
            }
        }

        #endregion

        #region Métodos privados
        /// <summary>
        /// Carga el combo de la interface con el listado de números de documento del sistema
        /// </summary>
        private void CargarTipoDocumento()
        {
            try
            {
                cmbTipoDocumento.DataSource = ((PageBase)Page).NegTablasMaestras.GetTiposDocumento();
                cmbTipoDocumento.DataBind();
                cmbTipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de tipos de documento de la tabla maestra.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de tipos de documento');", true);
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
                cmbTipoDocumento.ClearSelection();
                txtNumDoc.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtTelefono.Text = string.Empty;
                Session.Remove("FiltroUsuario");
                CargarGrid();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al limpiar los datos de filtro", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "GestorUsuario", "alert('Se ha producido un error al limpiar los datos del filtro');", true);
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
        /// Procedimiento de la grid, al crear una fila
        /// </summary>
        protected void gvUsuarios_RowCreated(object sender, GridViewRowEventArgs e)
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
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (gvUsuarios.Rows.Count > 0)
            {
                //Se recupera el identificador del usuario
                int idUsuario = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Consulta":
                        Session["IdUsuario"] = e.CommandArgument.ToString();
                        Response.Redirect(_Page.MasterBase.RelativeURL + "/Administracion/DetalleUsuario.aspx?grid=S", false);
                        break;
                    case "Modificar":
                        Session["IdUsuario"] = e.CommandArgument.ToString();
                        Response.Redirect(_Page.MasterBase.RelativeURL + "/Administracion/ModificarUsuario.aspx?grid=S", false);
                        break;
                    case "Baja":
                        try
                        {
                            if (idUsuario == MasterBase.DatosSesionLogin.IdUsuario)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", "alert('El usuario no puede darse de baja a sí mismo.');", true);
                                break;
                            }
                            else
                            {
                                //Se borra al usuario de la AMPA
                                if (!NegUsuario.BajaUsuarioAMPA(idUsuario, MasterBase.DatosSesionLogin.IdEmpresa))
                                {
                                    Comun.Log.TrazaLog.Error("No se ha podido dar de baja el usuario " + idUsuario.ToString());
                                    ErrorGeneral("Se ha producido un error al intentar dar de baja al usuario para la AMPA");
                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".gvUsuarios_RowCommand().Eliminar(IdUsuario: " + idUsuario.ToString() + "). Descripcion: " + ex.ToString());
                            ScriptManager.RegisterStartupScript(Page, GetType(), "gestor", "alert('Se ha producido un error al dar de baja al usuario');", true);
                            return;
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", "alert('El usuario ha sido dado de baja correctamente.');", true);
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
                if (!string.IsNullOrWhiteSpace(cmbTipoDocumento.SelectedValue))
                {
                    filtro.Vacio = false;
                    filtro.IdTipoDocumento = int.Parse(cmbTipoDocumento.SelectedValue);
                }

                if (!string.IsNullOrEmpty(txtNumDoc.Text.Trim()))
                {
                    filtro.Vacio = false;
                    filtro.NumeroDocumento = txtNumDoc.Text.Trim();
                }

                if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
                {
                    filtro.Vacio = false;
                    filtro.Nombre = txtNombre.Text.Trim();
                }

                if (!string.IsNullOrEmpty(txtTelefono.Text.Trim()))
                {
                    filtro.Vacio = false;
                    filtro.Telefono = txtTelefono.Text.Trim();
                }
                FiltroInicial = filtro;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".SetFiltro(). Descripcion; ", ex);
                _Page.ErrorGeneral("Ha ocurrido un error al establecer el filtro de la página");
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
                if (filtro.IdTipoDocumento > 0)
                    cmbTipoDocumento.SelectedValue = filtro.IdTipoDocumento.ToString();

                if (!string.IsNullOrEmpty(filtro.NumeroDocumento))
                    txtNumDoc.Text = filtro.NumeroDocumento;

                if (!string.IsNullOrEmpty(filtro.Nombre))
                    txtNombre.Text = filtro.Nombre;
                if (!string.IsNullOrEmpty(filtro.Telefono))
                    txtTelefono.Text = filtro.Telefono;
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
                List<USUARIO_AMPA> listado = new List<USUARIO_AMPA>();
                Comun.FiltroUsuario filtro = new Comun.FiltroUsuario();
                filtro = SetFiltro();
                listado = NegUsuario.GetUsuariosByAMPA(MasterBase.DatosSesionLogin.IdEmpresa, filtro);
                gvUsuarios.DataSource = listado;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar el grid", ex);
                throw;
            }
        }
    }
}
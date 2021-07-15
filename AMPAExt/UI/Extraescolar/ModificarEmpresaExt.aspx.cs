using System;
using System.Collections.Generic;
using AMPA.Modelo;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Extraescolar
{
    /// <summary>
    /// Funcionalidad para el mantenimiento de una empresa extraescolar
    /// </summary>
    public partial class ModificarEmpresaExt : PageBase
    {
        /// <summary>
        /// Propiedad con el mensaje de comunicación para el control
        /// </summary>
        private StringBuilder _mensaje;
        /// <summary>
        /// Página base en la que se encuentra la página actual
        /// </summary>
        private PageBase _Page { get { return Page as PageBase; } }
        /// <summary>
        /// Identificador de la empresa a modificar
        /// </summary>
        public int IdEmpresa { get; set; }
        /// <summary>
        /// Antes de cargar la página se define que es necesario estar logado
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            MasterBase.CheckSesion = true;
        }

        /// <summary>
        /// Evento de carga de página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.EXTR)
                    IdEmpresa = MasterBase.DatosSesionLogin.IdEmpresa;
                else
                    IdEmpresa = int.Parse(Session["IdEmpresa"].ToString());
                if (!Page.IsPostBack)
                {
                    CargarDatos();
                    CargarGrid();
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar modificación de empresa", ex);
                ErrorGeneral("Se ha producido un error al cargar los datos de la empresa");
            }
        }

        /// <summary>
        /// Obtiene los datos de la empresa introducidos. Se realiza una validación previa sobre el formulario y en caso de que sea correcto, se da de alta en base de datos
        /// </summary>
        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Limpiar, string.Empty);
                _mensaje = new StringBuilder();
                if (ValidarFormulario())
                {
                    EMPRESA empresa = new EMPRESA();
                    empresa.NIF = txtNumDocumento.Text.Trim().ToUpper();
                    empresa.NOMBRE = txtNombre.Text.Trim();
                    empresa.RAZON_SOCIAL = txtRazonSocial.Text.Trim();
                    empresa.TELEFONO = txtTelefono.Text.Trim();
                    empresa.PAGINA_WEB = txtWeb.Text.Trim();
                    empresa.ID_EMPRESA = IdEmpresa;
                    empresa.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;
                    empresa.FECHA_MOD = DateTime.Now;

                    if (NegExtraescolar.ModificarEmpresa(empresa))
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Success, "La empresa se ha modificado correctamente");
                    else
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al modificar la empresa");
                }
                else
                    PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, _mensaje.ToString());
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al modificar la empresa extraescolar.", ex);
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al modificar la empresa extraescolar");
            }
        }

        /// <summary>
        /// Evento generado al pulsar sobre el botón cancelar. Limpia las variable de sesión utilizadas y vuelve a la página de mantenimiento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session["IdEmpresa"] = null;
            if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.EXTR)
                Response.Redirect("../Extraescolar.aspx", false);
            else
                Response.Redirect("MantenimientoExt.aspx?grid=S", false);
        }

        /// <summary>
        /// Valida que los campos obligatorios hayan sido rellenados.
        /// </summary>
        /// <returns>True si los datos obligatorios han sido rellenos
        /// False en caso de que alguno no haya sido relleno</returns>
        private bool ValidarFormulario()
        {
            _mensaje.Clear();

            #region empresa

            lblNumDocumento.Visible = false;
            lblNombre.Visible = false;
            lblRazonSocial.Visible = false;
            lblTelefono.Visible = false;
            lblWeb.Visible = false;

            if (string.IsNullOrWhiteSpace(txtNumDocumento.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el NIF de la empresa extraescolar</li>");
                lblNumDocumento.Visible = true;
                txtNumDocumento.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el nombre de la empresa extraescolar</li>");
                lblNombre.Visible = true;
                if (_mensaje == null)
                    txtNombre.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir la razón social de la empresa extraescolar</li>");
                lblRazonSocial.Visible = true;
                if (_mensaje == null)
                    txtRazonSocial.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el teléfono de contacto de la empresa extraescolar</li>");
                lblTelefono.Visible = true;
                if (_mensaje == null)
                    txtTelefono.Focus();
            }
            #endregion
            return _mensaje.Length == 0;
        }

        /// <summary>
        /// Carga los datos de la empresa
        /// </summary>
        private void CargarDatos()
        {
            EMPRESA datosEmpresa = NegExtraescolar.GetEmpresabyId(IdEmpresa);
            if (datosEmpresa != null)
            {
                txtNumDocumento.Text = datosEmpresa.NIF;
                txtNombre.Text = datosEmpresa.NOMBRE;
                txtRazonSocial.Text = datosEmpresa.RAZON_SOCIAL;
                txtWeb.Text = datosEmpresa.PAGINA_WEB;
                txtTelefono.Text = datosEmpresa.TELEFONO;
            }
            else
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "ERROR: No se ha encontrado la empresa en el sistema");
            }
        }

        /// <summary>
        /// Procedimiento de la carga el listado
        /// </summary>
        private void CargarGrid()
        {
            try
            {
                List<MONITOR> listado = new List<MONITOR>();
                if (MasterBase.DatosSesionLogin.CodTipoUsuario != Comun.TipoDatos.TipoUsuario.AMPA)
                    IdEmpresa = MasterBase.DatosSesionLogin.IdEmpresa;
                listado = NegExtraescolar.GetMonitores(IdEmpresa);
                gvMonitores.DataSource = listado;
                gvMonitores.DataBind();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar el grid", ex);
                throw;
            }
        }

        /// <summary>
        /// Evento al pulsar sobre el botón de alta de monitor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAltaMonitor_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaMonitor.aspx?grid=S", false);
        }

        #region Eventos del grid

        /// <summary>
        /// Procedimiento de la grid, al crear una fila
        /// </summary>
        protected void gvMonitores_RowCreated(object sender, GridViewRowEventArgs e)
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
        protected void gvMonitores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (gvMonitores.Rows.Count > 0)
            {
                //Se recupera el identificador del usuario
                int idMonitor = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Consulta":
                        Session["IdMonitor"] = e.CommandArgument.ToString();
                        Response.Redirect(_Page.MasterBase.RelativeURL + "/Extraescolar/DetalleMonitor.aspx?grid=S", false);
                        break;
                    case "Modificar":
                        Session["IdMonitor"] = e.CommandArgument.ToString();
                        Response.Redirect(_Page.MasterBase.RelativeURL + "/Extraescolar/ModificarMonitor.aspx?grid=S", false);
                        break;
                    case "Baja":
                        try
                        {
                            //Se borra al monitor de la empresa
                            if (!NegExtraescolar.BajaMonitor(idMonitor, IdEmpresa))
                            {
                                Comun.Log.TrazaLog.Error("No se ha podido dar de baja el monitor " + idMonitor.ToString());
                                ErrorGeneral("Se ha producido un error al intentar dar de baja al monitor para la empresa");
                                return;
                            }

                        }
                        catch (Exception ex)
                        {
                            Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".gvMonitores_RowCommand().Eliminar(idMonitor: " + idMonitor.ToString() + "). Descripcion: " + ex.ToString());
                            ScriptManager.RegisterStartupScript(Page, GetType(), "gestor", "alert('Se ha producido un error al dar de baja al monitor');", true);
                            return;
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", "alert('El monitor ha sido dado de baja correctamente.');", true);
                        //Se carga el grid de nuevo para que desaparezca el usuario borrado
                        CargarGrid();
                        break;
                }
            }
        }

        #endregion
    }
}
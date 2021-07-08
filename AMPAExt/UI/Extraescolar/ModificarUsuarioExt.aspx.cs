using System;
using AMPA.Modelo;
using System.Text;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Extraescolar
{
    /// <summary>
    /// Funcionalidad para dar de alta a un usuario de una empresa extraescolar
    /// </summary>
    public partial class ModificarUsuarioExt : PageBase
    {
        /// <summary>
        /// Propiedad con el mensaje de comunicación para el control
        /// </summary>
        private StringBuilder _mensaje;

        /// <summary>
        /// Identificador del usuario que se va a modificar (privada)
        /// </summary>
        private int _idUsuario;

        /// <summary>
        /// Identificador del usuario que se va a modificar (pública)
        /// </summary>
        public int IdUsuario
        {
            get
            {
                return _idUsuario;
            }
            set
            {
                _idUsuario = value;
            }
        }
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
                IdUsuario = int.Parse(Session["IdUsuarioExt"].ToString());
                if (!Page.IsPostBack)
                {
                    CargarEmpresas();
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar modificación de usuario de extraescolar.", ex);
                Error("Se ha producido un error al cargar los datos del usuario");
            }
        }

        /// <summary>
        /// Se realiza una validación previa sobre el formulario y en caso de que sea correcto, se modifica el usuario en base de datos
        /// </summary>
        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Limpiar,string.Empty);
                _mensaje = new StringBuilder();
                USUARIO_EMPRESA nuevoUsuario = new USUARIO_EMPRESA();
                if (ValidarFormulario())
                {
                    nuevoUsuario.NOMBRE = txtNombre.Text.Trim();
                    nuevoUsuario.APELLIDO1 = txtApellido1.Text.Trim();
                    nuevoUsuario.APELLIDO2 = txtApellido2.Text.Trim();
                    nuevoUsuario.ID_TIPO_DOCUMENTO = int.Parse(cmbTipoDocumento.SelectedValue);
                    nuevoUsuario.NUMERO_DOCUMENTO = txtNumDocumento.Text.Trim().ToUpper();
                    nuevoUsuario.TELEFONO = txtTelefono.Text.Trim();
                    nuevoUsuario.EMAIL = txtemail.Text.Trim();
                    nuevoUsuario.ID_USUARIO_EMP = IdUsuario;
                    nuevoUsuario.ID_EMPRESA = int.Parse(cmbEmpresa.SelectedValue);
                    nuevoUsuario.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;
                    nuevoUsuario.FECHA_MOD = DateTime.Now;

                    if (NegExtraescolar.ModificarUsuarioExtraescolar(nuevoUsuario))
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Success, "El usuario se ha modificado correctamente");
                    else
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al modificar el usuario");
                }
                else
                    PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, _mensaje.ToString());
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al modificar el usuario.", ex);
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al modificar al usuario");
            }
        }

        /// <summary>
        /// Evento generado al pulsar sobre el botón cancelar. Limpia las variable de sesión utilizadas y vuelve a la página de gestión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session["IdUsuarioExt"] = null;
            Response.Redirect("GestionUsuariosExt.aspx?grid=S", false);
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
        /// Carga los datos del usuario
        /// </summary>
        private void CargarDatos()
        {
            USUARIO_EMPRESA datosUsuario = NegExtraescolar.GetUsuarioById(IdUsuario);
            if (datosUsuario != null)
            {
                txtNombre.Text = datosUsuario.NOMBRE;
                txtApellido1.Text = datosUsuario.APELLIDO1;
                txtApellido2.Text = datosUsuario.APELLIDO2;
                txtemail.Text = datosUsuario.EMAIL;
                txtNumDocumento.Text = datosUsuario.NUMERO_DOCUMENTO;
                txtTelefono.Text = datosUsuario.TELEFONO;
                cmbTipoDocumento.Items.Clear();
                cmbTipoDocumento.Items.Add(new ListItem(datosUsuario.TIPO_DOCUMENTO.NOMBRE, datosUsuario.ID_TIPO_DOCUMENTO.ToString()));
                cmbEmpresa.SelectedValue = datosUsuario.ID_EMPRESA.ToString();
            }
            else
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "ERROR: No se ha encontrado al usuario en el sistema");
            }
        }

        /// <summary>
        /// Valida que los campos obligatorios hayan sido rellenados.
        /// </summary>
        /// <returns>True si los datos obligatorios han sido rellenos
        /// False en caso de que alguno no haya sido relleno</returns>
        public bool ValidarFormulario()
        {
            lblNombre.Visible = false;
            lblApellido1.Visible = false;
            lblTipoDocumento.Visible = false;
            lblNumDocumento.Visible = false;
            lblEmail.Visible = false;
            _mensaje.Clear();
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el nombre del usuario</li>");
                lblNombre.Visible = true;
                txtNombre.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtApellido1.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el primer apellido del usuario</li>");
                lblApellido1.Visible = true;
                if (_mensaje == null)
                    txtApellido1.Focus();
            }
            if ((string.IsNullOrWhiteSpace(cmbTipoDocumento.SelectedValue)))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar el tipo de documento del usuario</li>");
                lblTipoDocumento.Visible = true;
                if (_mensaje == null)
                    txtNumDocumento.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtNumDocumento.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el número de documento del usuario</li>");
                lblNumDocumento.Visible = true;
                if (_mensaje == null)
                    txtNumDocumento.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtemail.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el correo electrónico del usuario</li>");
                lblEmail.Visible = true;
                if (_mensaje == null)
                    txtemail.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbEmpresa.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar la empresa a la que pertenece el usuario</li>");
                lblEmpresa.Visible = true;
                if (_mensaje == null)
                    cmbEmpresa.Focus();
            }
            return _mensaje.Length == 0;
        }
    }
}
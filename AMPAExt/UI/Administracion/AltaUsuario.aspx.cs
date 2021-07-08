using System;
using AMPA.Modelo;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Administracion
{
    /// <summary>
    /// Funcionalidad para dar de alta a un usuario de la AMPA
    /// </summary>
    public partial class AltaUsuario : PageBase
    {
        /// <summary>
        /// Propiedad con el mensaje de comunicación para el control
        /// </summary>
        private StringBuilder _mensaje;

        /// <summary>
        /// Antes de que se carge la página se define que se compruebe los si necesita estar logado
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

            if (!Page.IsPostBack)
            {
                CargarTipoDocumento();
            }
        }

        /// <summary>
        /// Obtiene los datos del usuario introducidos. Se realiza una validación previa sobre el formulario y en caso de que sea correcto, se da de alta en base de datos
        /// </summary>
        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Limpiar, string.Empty);
                _mensaje = new StringBuilder();
                USUARIO_AMPA nuevoUsuario = new USUARIO_AMPA();
                if (ValidarFormulario())
                {
                    nuevoUsuario.NOMBRE = txtNombre.Text.Trim();
                    nuevoUsuario.APELLIDO1 = txtApellido1.Text.Trim();
                    nuevoUsuario.APELLIDO2 = txtApellido2.Text.Trim();
                    nuevoUsuario.ID_TIPO_DOCUMENTO = int.Parse(cmbTipoDocumento.SelectedValue);
                    nuevoUsuario.NUMERO_DOCUMENTO = txtNumDocumento.Text.Trim().ToUpper();
                    nuevoUsuario.TELEFONO = txtTelefono.Text.Trim();
                    nuevoUsuario.EMAIL = txtemail.Text.Trim();
                    nuevoUsuario.ID_AMPA = MasterBase.DatosSesionLogin.IdEmpresa;
                    nuevoUsuario.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;
                    if (NegUsuario.AltaUsuarioAMPA(nuevoUsuario) > 0)
                    {
                        VaciarFormulario(this);
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Success, "El usuario ha sido dado de alta correctamente");
                    }
                    else
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al dar de alta al usuario");
                }
                else
                    PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, _mensaje.ToString());
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al insertar el nuevo usuario.", ex);
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al dar de alta al usuario");
            }
        }
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
        /// <summary>
        /// Valida que los campos obligatorios hayan sido rellenados.
        /// </summary>
        /// <returns>True si los datos obligatorios han sido rellenos
        /// False en caso de que alguno no haya sido relleno</returns>
        private bool ValidarFormulario()
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
            if (string.IsNullOrWhiteSpace(cmbTipoDocumento.SelectedValue))
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
            return _mensaje.Length == 0;
        }

        /// <summary>
        /// Recorre los controles y los inicializa
        /// </summary>
        /// <param name="control">Control a recorrer</param>
        private void VaciarFormulario(Control control)
        {
            if (control.GetType() == typeof(TextBox))
                ((TextBox)control).Text = string.Empty;
            else if (control.GetType() == typeof(DropDownList))
                ((DropDownList)control).ClearSelection();
            else if (control.GetType() == typeof(CheckBoxList))
                ((CheckBoxList)control).ClearSelection();
            else if (control.GetType() == typeof(System.Web.UI.HtmlControls.HtmlSelect))
                ((System.Web.UI.HtmlControls.HtmlSelect)control).SelectedIndex = 0;
            else if (control.GetType() == typeof(RadioButtonList))
                ((RadioButtonList)control).ClearSelection();
            foreach (Control controlHijo in control.Controls)
                VaciarFormulario(controlHijo);
        }
    }
}
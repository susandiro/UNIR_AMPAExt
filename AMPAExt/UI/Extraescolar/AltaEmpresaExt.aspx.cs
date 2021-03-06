using System;
using AMPA.Modelo;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Extraescolar
{
    /// <summary>
    /// Funcionalidad para dar de alta a una nueva empresa extraescolar
    /// </summary>
    public partial class AltaEmpresaExt : PageBase
    {
        /// <summary>
        /// Propiedad con el mensaje de comunicación para el control
        /// </summary>
        private StringBuilder _mensaje;

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
            if (!Page.IsPostBack)
                CargarTipoDocumento();
        }

        /// <summary>
        /// Obtiene los datos de la empresa introducidos. Se realiza una validación previa sobre el formulario y en caso de que sea correcto, se da de alta en base de datos
        /// </summary>
        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Limpiar, string.Empty);
                _mensaje = new StringBuilder();
              
                USUARIO_EMPRESA nuevoUsuario = new USUARIO_EMPRESA();
                if (ValidarFormulario())
                {
                    EMPRESA nuevaEmpresa = new EMPRESA();
                    nuevaEmpresa.NIF = txtNumDocumento.Text.Trim().ToUpper();
                    nuevaEmpresa.NOMBRE = txtNombre.Text.Trim();
                    nuevaEmpresa.RAZON_SOCIAL = txtRazonSocial.Text.Trim();
                    nuevaEmpresa.TELEFONO = txtTelefono.Text.Trim();
                    nuevaEmpresa.PAGINA_WEB = txtWeb.Text.Trim();
                    nuevoUsuario.EMPRESA = nuevaEmpresa;
                    
                    nuevoUsuario.NOMBRE = txtUNombre.Text.Trim();
                    nuevoUsuario.APELLIDO1 = txtUApellido1.Text.Trim();
                    nuevoUsuario.APELLIDO2 = txtUApellido2.Text.Trim();
                    nuevoUsuario.ID_TIPO_DOCUMENTO = int.Parse(cmbUTipoDocumento.SelectedValue);
                    nuevoUsuario.NUMERO_DOCUMENTO = txtUNumDocumento.Text.Trim().ToUpper();
                    nuevoUsuario.TELEFONO = txtUTelefono.Text.Trim();
                    nuevoUsuario.EMAIL = txtUemail.Text.Trim();
                    nuevoUsuario.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;

                    int resultado = NegExtraescolar.AltaEmpresaExtUsuario(nuevoUsuario, MasterBase.DatosSesionLogin.IdEmpresa, MasterBase.DatosSesionLogin.DatosUsuario);
                    if (resultado == -10)
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Warning, "La empresa que intenta dar de alta ya existe en el sistema. No se ha realizado la acción");
                    else if (resultado > 0)
                    {
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Success, "La empresa extraescolar ha sido dado de alta correctamente");
                        LimpiarFormulario();
                    }
                    else
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al dar de alta la empresa extraescolar");
                }
                else
                    PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, _mensaje.ToString());
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al insertar la nueva empresa extraescolar.", ex);
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al dar de alta la empresa extraescolar");
            }
        }
        /// <summary>
        /// Carga el combo de la interface con el listado de números de documento del sistema
        /// </summary>
        private void CargarTipoDocumento()
        {
            try
            {
                cmbUTipoDocumento.DataSource = ((PageBase)Page).NegTablasMaestras.GetTiposDocumento();
                cmbUTipoDocumento.DataBind();
                cmbUTipoDocumento.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de tipos de documento de la tabla maestra.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Empresa", "alert('Se ha producido un error al obtener el listado de tipos de documento');", true);
            }
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

            #region Usuario
            lblUNombre.Visible = false;
            lblUApellido1.Visible = false;
            lblUTipoDocumento.Visible = false;
            lblUNumDocumento.Visible = false;
            lblUemail.Visible = false;

            if (string.IsNullOrWhiteSpace(txtUNombre.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el nombre del usuario</li>");
                lblUNombre.Visible = true;
                if (_mensaje == null)
                    txtUNombre.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtUApellido1.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el primer apellido del usuario</li>");
                lblUApellido1.Visible = true;
                if (_mensaje == null)
                    txtUApellido1.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbUTipoDocumento.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar el tipo de documento del usuario</li>");
                lblUTipoDocumento.Visible = true;
                if (_mensaje == null)
                    txtUNumDocumento.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtUNumDocumento.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el número de documento del usuario</li>");
                lblUNumDocumento.Visible = true;
                if (_mensaje == null)
                    txtUNumDocumento.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtUemail.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el correo electrónico del usuario</li>");
                lblUemail.Visible = true;
                if (_mensaje == null)
                    txtUemail.Focus();
            }
            #endregion

            return _mensaje.Length == 0;
        }
        /// <summary>
        /// Inicializa el formulario
        /// </summary>
        private void LimpiarFormulario()
        {
            foreach(Control control in form1.Controls)
                VaciarFormulario(control);
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
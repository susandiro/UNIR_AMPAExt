using System;
using AMPA.Modelo;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Extraescolar
{
    /// <summary>
    /// Funcionalidad para modificar los datos de un monitor de una empresa extraescolar
    /// </summary>
    public partial class ModificarMonitor : PageBase
    {
        /// <summary>
        /// Propiedad con el mensaje de comunicación para el control
        /// </summary>
        private StringBuilder _mensaje;
        /// <summary>
        /// Identificador de la empresa
        /// </summary>
        public int IdEmpresa { get; set; }
        /// <summary>
        /// Identificador del monitor que se va a modificar (privada)
        /// </summary>
        private int _idMonitor;

        /// <summary>
        /// Identificador del monitor que se va a modificar (pública)
        /// </summary>
        public int IdMonitor
        {
            get
            {
                return _idMonitor;
            }
            set
            {
                _idMonitor = value;
            }
        }
        /// <summary>
        /// Antes de cargar la página se define que es necesario estar logado
        /// </summary>
        /// <param name="e"></param>
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
            if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.EXTR)
                IdEmpresa = MasterBase.DatosSesionLogin.IdEmpresa;
            else
                IdEmpresa = int.Parse(Session["IdEmpresa"].ToString());
            IdMonitor = int.Parse(Session["IdMonitor"].ToString());
            if (!Page.IsPostBack)
                CargarDatos();
        }

        /// <summary>
        /// Obtiene los datos del monitor introducidos. Se realiza una validación previa sobre el formulario y en caso de que sea correcto, se modifica en base de datos
        /// </summary>
        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Limpiar, string.Empty);
                _mensaje = new StringBuilder();
                MONITOR nuevoMonitor = new MONITOR();
                if (ValidarFormulario())
                {
                    nuevoMonitor.NOMBRE = txtNombre.Text.Trim();
                    nuevoMonitor.APELLIDO1 = txtApellido1.Text.Trim();
                    nuevoMonitor.APELLIDO2 = txtApellido2.Text.Trim();
                    nuevoMonitor.ID_TIPO_DOCUMENTO = int.Parse(cmbTipoDocumento.SelectedValue);
                    nuevoMonitor.NUMERO_DOCUMENTO = txtNumDocumento.Text.Trim().ToUpper();
                    nuevoMonitor.TELEFONO = txtTelefono.Text.Trim();
                    nuevoMonitor.EMAIL = txtemail.Text.Trim();
                    nuevoMonitor.ID_MONITOR = IdMonitor;
                    nuevoMonitor.ID_EMPRESA = IdEmpresa;
                    nuevoMonitor.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;
                    nuevoMonitor.FECHA_MOD = DateTime.Now;

                    if (NegExtraescolar.ModificarMonitor(nuevoMonitor))
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Success, "El monitor se ha modificado correctamente");
                    else
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al modificar el monitor");
                }
                else
                    PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, _mensaje.ToString());
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al modificar el monitor.", ex);
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al modificar el monitor");
            }
        }

        /// <summary>
        /// Carga los datos del monitor
        /// </summary>
        private void CargarDatos()
        {
            MONITOR datos = NegExtraescolar.GetMonitorById(IdMonitor,IdEmpresa);
            if (datos != null)
            {
                txtNombre.Text = datos.NOMBRE;
                txtApellido1.Text = datos.APELLIDO1;
                txtApellido2.Text = datos.APELLIDO2;
                txtemail.Text = datos.EMAIL;
                txtNumDocumento.Text = datos.NUMERO_DOCUMENTO;
                txtTelefono.Text = datos.TELEFONO;
                cmbTipoDocumento.Items.Clear();
                cmbTipoDocumento.Items.Add(new ListItem(datos.TIPO_DOCUMENTO.NOMBRE, datos.ID_TIPO_DOCUMENTO.ToString()));
            }
            else
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "ERROR: No se ha encontrado al monitor en el sistema");
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
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el nombre del monitor</li>");
                lblNombre.Visible = true;
                txtNombre.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtApellido1.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el primer apellido del monitor</li>");
                lblApellido1.Visible = true;
                if (_mensaje == null)
                    txtApellido1.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbTipoDocumento.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar el tipo de documento del monitor</li>");
                lblTipoDocumento.Visible = true;
                if (_mensaje == null)
                    txtNumDocumento.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtNumDocumento.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el número de documento del monitor</li>");
                lblNumDocumento.Visible = true;
                if (_mensaje == null)
                    txtNumDocumento.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtemail.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el correo electrónico del monitor</li>");
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

        /// <summary>
        /// Evento generado al pulsar sobre el botón cancelar. Limpia las variable de sesión utilizadas y vuelve a la página de mantenimiento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModificarEmpresaExt.aspx?grid=S", false);
        }
    }
}
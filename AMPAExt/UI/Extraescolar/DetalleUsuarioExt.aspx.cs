using System;
using AMPA.Modelo;
using System.Text;
using System.Web.UI;

namespace AMPAExt.UI.Extraescolar
{
    /// <summary>
    /// Funcionalidad para realizar la consulta de un usuario de empresa extraescolar
    /// </summary>
    public partial class DetalleUsuarioExt : PageBase
    {
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
            try
            {
                IdUsuario = int.Parse(Session["IdUsuarioExt"].ToString());
                if (!Page.IsPostBack)
                    CargarDatos();
            }
            catch(Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar el usuario.", ex);
                Error("Se ha producido un error al cargar los datos del usuario de la empresa extraescolar");
            }
        }

       /// <summary>
        /// Evento generado al pulsar sobre el botón cancelar. Limpia las variable de sesión utilizadas y vuelve a la página de gestión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Session["IdUsuarioExt"] = null;
            Response.Redirect("GestionUsuariosExt.aspx?grid=S", false);
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
                txtTipoDocumento.Text = datosUsuario.TIPO_DOCUMENTO.NOMBRE;
                txtEmpresa.Text = datosUsuario.EMPRESA.NOMBRE;
            }
            else
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "ERROR: No se ha encontrado al usuario en el sistema");
            }
        }
    }
}
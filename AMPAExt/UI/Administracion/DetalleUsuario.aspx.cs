using System;
using AMPA.Modelo;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Administracion
{
    /// <summary>
    /// Funcionalidad para realizar la consulta de un usuario de la AMPA
    /// </summary>
    public partial class DetalleUsuario : PageBase
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
                IdUsuario = int.Parse(Session["idUsuario"].ToString());
                if (!Page.IsPostBack)
                    CargarDatos();
            }
            catch(Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar el usuario de AMPA.", ex);
                ErrorGeneral("Se ha producido un error al cargar los datos del usuario de AMPA");
            }
        }

       /// <summary>
        /// Evento generado al pulsar sobre el botón cancelar. Limpia las variable de sesión utilizadas y vuelve a la página de gestión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Session["idUsuario"] = null;
            Response.Redirect("GestionUsuarios.aspx?grid=S", false);
        }

        /// <summary>
        /// Carga los datos del usuario
        /// </summary>
        private void CargarDatos()
        {
            USUARIO_AMPA datosUsuario = NegUsuario.GetUsuarioByIdAMPA(IdUsuario, MasterBase.DatosSesionLogin.IdEmpresa);
            if (datosUsuario != null)
            {
                txtNombre.Text = datosUsuario.NOMBRE;
                txtApellido1.Text = datosUsuario.APELLIDO1;
                txtApellido2.Text = datosUsuario.APELLIDO2;
                txtemail.Text = datosUsuario.EMAIL;
                txtNumDocumento.Text = datosUsuario.NUMERO_DOCUMENTO;
                txtTelefono.Text = datosUsuario.TELEFONO;
                txtTipoDocumento.Text = datosUsuario.TIPO_DOCUMENTO.NOMBRE;
            }
            else
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "ERROR: No se ha encontrado al usuario en el sistema");
            }
        }
    }
}
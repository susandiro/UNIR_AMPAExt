using System;
using AMPA.Modelo;
using System.Text;
using System.Web.UI;

namespace AMPAExt.UI.Extraescolar
{
    /// <summary>
    /// Funcionalidad para realizar la consulta de un monitor de empresa extraescolar
    /// </summary>
    public partial class DetalleMonitor : PageBase
    {
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
        /// Identificador de la empresa
        /// </summary>
        public int IdEmpresa { get; set; }

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
                if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.EXTR)
                    IdEmpresa = MasterBase.DatosSesionLogin.IdEmpresa;
                else
                    IdEmpresa = int.Parse(Session["IdEmpresa"].ToString());
                IdMonitor = int.Parse(Session["IdMonitor"].ToString());
                if (!Page.IsPostBack)
                    CargarDatos();
            }
            catch(Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar el monitor.", ex);
                ErrorGeneral("Se ha producido un error al cargar los datos del monitor de la empresa extraescolar");
            }
        }

       /// <summary>
        /// Evento generado al pulsar sobre el botón cancelar. Limpia las variable de sesión utilizadas y vuelve a la página de gestión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Session["IdMonitor"] = null;
            Response.Redirect("ModificarEmpresaExt.aspx?grid=S", false);
        }

        /// <summary>
        /// Carga los datos del monitor
        /// </summary>
        private void CargarDatos()
        {
            MONITOR datos = NegExtraescolar.GetMonitorById(IdMonitor, IdEmpresa);
            if (datos != null)
            {
                txtNombre.Text = datos.NOMBRE;
                txtApellido1.Text = datos.APELLIDO1;
                txtApellido2.Text = datos.APELLIDO2;
                txtemail.Text = datos.EMAIL;
                txtNumDocumento.Text = datos.NUMERO_DOCUMENTO;
                txtTelefono.Text = datos.TELEFONO;
                txtTipoDocumento.Text = datos.TIPO_DOCUMENTO.NOMBRE;
            }
            else
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "ERROR: No se ha encontrado al monitor en el sistema");
            }
        }
    }
}
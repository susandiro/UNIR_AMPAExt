using System;
using AMPA.Modelo;
using System.Web.UI;

namespace AMPAExt.UI.Extraescolar
{
    /// <summary>
    /// Funcionalidad para la consulta de una empresa extraescolar
    /// </summary>
    public partial class DetalleEmpresaExt : PageBase
    {
        /// <summary>
        /// Identificador de la empresa(privada)
        /// </summary>
        private int _idEmpresa;

        /// <summary>
        /// Identificador de la empresa(pública)
        /// </summary>
        public int IdEmpresa
        {
            get
            {
                return _idEmpresa;
            }
            set
            {
                _idEmpresa = value;
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
                IdEmpresa = int.Parse(Session["IdEmpresa"].ToString());
                if (!Page.IsPostBack)
                    CargarDatos();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar la empresa.", ex);
                Error("Se ha producido un error al cargar los datos de la empresa extraescolar");
            }
        }

        /// <summary>
        /// Evento generado al pulsar sobre el botón cancelar. Limpia las variable de sesión utilizadas y vuelve a la página de mantenimiento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Session["IdEmpresa"] = null;
            Response.Redirect("MantenimientoExt.aspx?grid=S", false);
        }

        /// <summary>
        /// Carga los datos del usuario
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
    }
}
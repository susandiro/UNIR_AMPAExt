using System;
using AMPA.Modelo;
using System.Web.UI;

namespace AMPAExt.UI.Actividades
{
    /// <summary>
    /// Funcionalidad para la consulta una actividad extraescolar
    /// </summary>
    public partial class DetalleActividad : PageBase
    {
        /// <summary>
        /// Identificador de la actividad a modificar
        /// </summary>
        public int IdActividad { get; set; }

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
                IdActividad = int.Parse(Session["IdActividad"].ToString());
                if (!Page.IsPostBack)
                {
                    if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.EXTR)
                    {
                        dvAMPA.Visible = true;
                        dvEmpresa.Visible = false;
                    }
                    else
                    {
                        dvAMPA.Visible = false;
                        dvEmpresa.Visible = true;
                    }
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar el detalle de la actividad", ex);
                ErrorGeneral("Se ha producido un error al cargar el detalle de la actividad");
            }
        }

        /// <summary>
        /// Carga los datos de la actividad
        /// </summary>
        private void CargarDatos()
        {
            ACTIVIDAD datos = NegActividad.GetActividadById(IdActividad);
            if (datos != null)
            {
                txtActNombre.Text = datos.NOMBRE;
                txtActDescripcion.Text = datos.DESCRIPCION;

                gvHorarios.DataSource = datos.ACTIVIDAD_HORARIO;
                gvHorarios.DataBind();

                gvDescuento.DataSource = datos.ACTIVIDAD_DESCUENTO;
                gvDescuento.DataBind();

                txtEmpresa.Text = datos.EMPRESA.NOMBRE;
                txtAMPA.Text = datos.AMPA.NOMBRE;
            }
            else
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "ERROR: No se ha encontrado la actividad en el sistema");
            }
        }

        /// <summary>
        /// Evento generado al pulsar sobre el botón volver. Limpia las variable de sesión utilizadas y vuelve a la página de gestión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Session["IdActividad"] = null;
            Response.Redirect("GestionActividades.aspx?grid=S", false);
        }
    }
}
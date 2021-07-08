using System;
using AMPA.Modelo;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Socio
{
    /// <summary>
    /// Funcionalidad para consultar un socio de AMPA
    /// </summary>
    public partial class DetalleSocio : PageBase
    {
        /// <summary>
        /// Identificador del socio que se va a modificar (privada)
        /// </summary>
        private int _idSocio;

        /// <summary>
        /// Identificador del socio que se va a modificar (pública)
        /// </summary>
        public int IdSocio
        {
            get
            {
                return _idSocio;
            }
            set
            {
                _idSocio = value;
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
                IdSocio = int.Parse(Session["IdSocio"].ToString());
                if (!Page.IsPostBack)
                    CargarDatos();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar el formulario", ex);
                Error("Se ha producido un error al cargar los datos del formulario");
            }
        }

        #region Eventos del grid

        /// <summary>
        /// Procedimiento de la grid, al crear una fila
        /// </summary>
        protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.background='#FFF0A2'");
                e.Row.Attributes.Add("onmouseout", "this.style.background='#FFFFFF'");
            }
        }

        #endregion

        /// <summary>
        /// Carga los datos del socio
        /// </summary>
        private void CargarDatos()
        {
            TUTOR datosTutor = NegSocio.GetSocioById(IdSocio, MasterBase.DatosSesionLogin.IdEmpresa);
            if (datosTutor != null)
            {
                lblNumSocio.Text = datosTutor.ID_TUTOR.ToString();
                if (datosTutor.INDIVIDUAL == "S")
                    rdIndividualSi.Checked=true;
                    
                //Tutor 1
                txtNombre1.Text = datosTutor.T1_NOMBRE;
                txtApellido1_1.Text = datosTutor.T1_APELLIDO1;
                txtApellido2_1.Text = datosTutor.T1_APELLIDO2;
                txtTipoDocumento1.Text= datosTutor.TIPO_DOCUMENTO.NOMBRE;
                txtNumDocumento1.Text = datosTutor.T1_NUMERO_DOCUMENTO;
                txtTelefono1.Text = datosTutor.T1_TELEFONO;
                txtemail1.Text = datosTutor.T1_EMAIL;

                //Tutor 2
                txtNombre2.Text = datosTutor.T2_NOMBRE;
                txtApellido1_2.Text = datosTutor.T2_APELLIDO1;
                txtApellido2_2.Text = datosTutor.T2_APELLIDO2;
                txtTipoDocumento2.Text = datosTutor.TIPO_DOCUMENTO1.NOMBRE;
                txtNumDocumento2.Text = datosTutor.T2_NUMERO_DOCUMENTO;
                txtTelefono2.Text = datosTutor.T2_TELEFONO;
                txtemail2.Text = datosTutor.T2_EMAIL;

                List<ALUMNO> alumnos = NegSocio.GetAlumnosBySocio(IdSocio);
                gvHijo.DataSource = alumnos;
                gvHijo.DataBind();
            }
            else
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "ERROR: No se ha encontrado al socio en el sistema");
            }
        }
        /// <summary>
        /// Evento generado al pulsar sobre el botón volver. Limpia las variable de sesión utilizadas y vuelve a la página de gestión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Session["IdSocio"] = null;
            Response.Redirect("GestionSocios.aspx?grid=S", false);
        }
    }
}
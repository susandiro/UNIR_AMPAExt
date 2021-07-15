using System;
using System.Collections.Generic;
using AMPA.Modelo;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Extraescolar
{
    public partial class AsignarActividad : PageBase
    {
        #region propiedades privadas
        /// <summary>
        /// Mensaje de error generado
        /// </summary>
        private string _MensajeError = string.Empty;
        /// <summary>
        /// Página base en la que se encuentra la página actual
        /// </summary>
        private PageBase _Page { get { return Page as PageBase; } }

        /// <summary>
        /// Filtro guardado en sesión para conservar las búsquedas al volver de un detalle
        /// </summary>
        private Comun.FiltroAlumno FiltroInicial
        {
            get { return (Session["FiltroActividad"] == null) ? null : (Comun.FiltroAlumno)Session["FiltroActividad"]; }
            set { Session["FiltroActividad"] = value; }
        }

        #endregion

        #region Eventos
        /// <summary>
        /// Evento producido al cargar la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    CargarCurso();
                    if (FiltroInicial != null)
                        CargarFiltro();
                    Session["IdAlumno"] = null;
                    CargarGrid();
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".Page_load()", ex);
                _Page.ErrorGeneral(_MensajeError);
            }
        }

        /// <summary>
        /// Al seleccionar un curso, se carga el listado de clases que tiene
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbCurso.SelectedValue))
            {
                CargarClase(int.Parse(cmbCurso.SelectedValue));
                cmbClase.Enabled = true;
            }
            else
            {
                cmbClase.Items.Clear();
                cmbClase.Enabled = false;
            }
        }

        /// <summary>
        /// Evento producido al pulsar sobre el botón limpiar del filtro. Carga el grid como en el estado inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                txtNumDoc.Text = string.Empty;
                txtNombre.Text = string.Empty;
                cmbCurso.ClearSelection();
                cmbClase.ClearSelection();
                cmbClase.Enabled = false;
                Session.Remove("FiltroActividad");
                CargarGrid();
              
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al limpiar los datos de filtro", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "GestorUsuario", "alert('Se ha producido un error al limpiar los datos del filtro');", true);
            }
        }

        /// <summary>
        /// Evento producido al pulsar sobre el botón filtrar del filtro. Carga el grid con los criterios indicados en el filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarGrid();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al filtrar", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "GestorUsuario", "alert('Se ha producido un error al buscar en el listado');", true);
            }
        }
        #endregion

        #region Eventos del grid

        /// <summary>
        /// Procedimiento de la grid, al crear una fila
        /// </summary>
        protected void gvAlumno_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.background='#FFF0A2'");
                e.Row.Attributes.Add("onmouseout", "this.style.background='#FFFFFF'");
            }
        }

        /// <summary>
        /// Procedimiento de la grid, al ejecutar un comando
        /// </summary>
        protected void gvAlumno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (gvAlumno.Rows.Count > 0)
            {
                //Se recupera el identificador del alumno
                int idUsuario = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Gestionar":
                        Session["IdAlumno"] = e.CommandArgument.ToString();
                        Response.Redirect(_Page.MasterBase.RelativeURL + "/Actividades/GestionarAsignacion.aspx?grid=S", false);
                        break;
                }
            }
        }

        #endregion

        #region Métodos privados
        /// <summary>
        /// Asigna los valores del filtro
        /// </summary>
        /// <returns></returns>
        private Comun.FiltroAlumno SetFiltro()
        {
            Comun.FiltroAlumno filtro = new Comun.FiltroAlumno();
            try
            {
                filtro.Vacio = true;
                if (!string.IsNullOrEmpty(txtNumDoc.Text.Trim()))
                {
                    filtro.Vacio = false;
                    filtro.NumDocumentoTutor = txtNumDoc.Text.Trim();
                }

                if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
                {
                    filtro.Vacio = false;
                    filtro.Nombre = txtNombre.Text.Trim();
                }
                if (!string.IsNullOrWhiteSpace(cmbCurso.SelectedValue))
                {
                    filtro.Vacio = false;
                    filtro.IdCurso = int.Parse(cmbCurso.SelectedValue);
                }
                if (!string.IsNullOrWhiteSpace(cmbClase.SelectedValue))
                {
                    filtro.Vacio = false;
                    filtro.IdClase = int.Parse(cmbClase.SelectedValue);
                }
                FiltroInicial = filtro;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".SetFiltro(). Descripcion; ", ex);
                _Page.ErrorGeneral("Ha ocurrido un error al establecer el filtro de la página");
            }
            return filtro;
        }
        /// <summary>
        /// Asigna los valores del filtro en función de lo que venga de sesion
        /// </summary>
        private void CargarFiltro()
        {
            Comun.FiltroAlumno filtro = FiltroInicial;
            try
            {
                if (filtro.IdCurso > 0)
                {
                    cmbCurso.SelectedValue = filtro.IdCurso.ToString();
                    cmbClase.Enabled = true;
                }

                if (filtro.IdClase > 0)
                    cmbClase.SelectedValue = filtro.IdClase.ToString();

                if (!string.IsNullOrEmpty(filtro.NumDocumentoTutor))
                    txtNumDoc.Text = filtro.NumDocumentoTutor;

                if (!string.IsNullOrEmpty(filtro.Nombre))
                    txtNombre.Text = filtro.Nombre;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".CargarFiltro(). Descripcion; ", ex);
                _MensajeError = "Ha ocurrido un error al establecer el filtro de la página";
                throw;
            }
        }

        /// <summary>
        /// Carga el combo de los cursos disponibles
        /// </summary>
        private void CargarCurso()
        {
            try
            {
                cmbCurso.DataSource = ((PageBase)Page).NegSocio.GetCursos();
                cmbCurso.DataBind();
                cmbCurso.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de cursos .", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de curso');", true);
            }
        }

        /// <summary>
        /// Carga el combo de las clases disponibles para un curso
        /// </summary>
        private void CargarClase(int idCurso)
        {
            try
            {
                cmbClase.Items.Clear();
                List<CURSO_CLASE> listado = NegSocio.GetClases(int.Parse(cmbCurso.SelectedValue), MasterBase.DatosSesionLogin.IdEmpresa);
                cmbClase.Items.Add(new ListItem("-- Seleccione --", ""));
                foreach (CURSO_CLASE objeto in listado)
                    cmbClase.Items.Add(new ListItem(objeto.CLASE.NOMBRE, objeto.ID_CURSO_CLASE.ToString()));
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de clases .", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de clases');", true);
            }
        }

        /// <summary>
        /// Procedimiento de la carga el listado
        /// </summary>
        private void CargarGrid()
        {
            try
            {
                List<ALUMNO> listado = new List<ALUMNO>();
                Comun.FiltroAlumno filtro = new Comun.FiltroAlumno();
                filtro = SetFiltro();
                if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.AMPA)
                    listado = NegSocio.GetAlumnosByAMPA(MasterBase.DatosSesionLogin.IdEmpresa, filtro);
                else
                    listado = NegSocio.GetAlumnosByEmpresa(MasterBase.DatosSesionLogin.IdEmpresa, filtro);

                gvAlumno.DataSource = listado;
                gvAlumno.DataBind();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar el grid", ex);
                throw;
            }
        }
        #endregion

    }
}
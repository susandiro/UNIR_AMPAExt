using System;
using AMPA.Modelo;
using System.Text;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Actividades
{
    /// <summary>
    /// Funcionalidad para gestionar las actividades extraescolares de un alumno
    /// </summary>
    public partial class GestionarAsignacion : PageBase
    {
        #region propiedades
        /// <summary>
        /// Propiedad con el mensaje de comunicación para el control
        /// </summary>
        private StringBuilder _mensaje;

        /// <summary>
        /// Identificador del alumno
        /// </summary>
        public int IdAlumno { get; set; }
        #endregion

        #region Eventos
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
            IdAlumno = int.Parse(Session["IdAlumno"].ToString());
            if (!Page.IsPostBack)
            {
                CargarAlumno();
                CargarEmpresa();
            }
        }

        /// <summary>
        /// Obtiene los datos del usuario introducidos. Se realiza una validación previa sobre el formulario y en caso de que sea correcto, se da de alta en base de datos
        /// </summary>
        protected void BtnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Limpiar, string.Empty);
                _mensaje = new StringBuilder();
                if (ValidarFormulario())
                {
                    ALUMNO_ACTIVIDAD resultado = new ALUMNO_ACTIVIDAD();
                    resultado.ID_ACT_HORARIO = int.Parse(cmbHorario.SelectedValue);
                    resultado.ID_ALUMNO = IdAlumno;
                    resultado.FECHA = DateTime.Now;
                    resultado.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;

                    if (NegActividad.AltaAlumnoHorario(resultado) > 0)
                    {
                        VaciarFormularioActividad();
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Success, "Se ha dado de alta al alumno en la actividad extraescolar");
                        CargarGrid();
                    }
                    else
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al dar de alta al alumno en la actividad extraescolar");
                }
                else
                    PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, _mensaje.ToString());
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error dar de alta en extraescolar.", ex);
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al dar de alta al alumno en la actividad extraescolar");
            }
        }

        /// <summary>
        /// Al seleccionar la empresa, se cargan las actividades que imparte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbEmpresa.SelectedValue))
            {
                CargarActividades(int.Parse(cmbEmpresa.SelectedValue));
                cmbActividad.Enabled = true;
            }
            else
            {
                cmbActividad.Items.Clear();
                cmbActividad.Enabled = false;
            }
        }

        /// <summary>
        /// Al seleccionar la actividad, se cargan los horarios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbActividad.SelectedValue))
            {
                CargarHorario(int.Parse(cmbActividad.SelectedValue));
                cmbHorario.Enabled = true;
            }
            else
            {
                cmbHorario.Items.Clear();
                cmbHorario.Enabled = false;
            }
        }
        #endregion

        #region Carga de datos

        /// <summary>
        /// Carga el combo de la interface con el listado de empresas para origen del sistema
        /// </summary>
        private void CargarEmpresa()
        {
            try
            {
                cmbEmpresa.Items.Clear();
                if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.AMPA)
                {
                    List<EMPRESA_AMPA> listado = new List<EMPRESA_AMPA>();
                    listado = NegUsuario.GetEmpresasByAMPA(MasterBase.DatosSesionLogin.IdEmpresa, true);
                    cmbEmpresa.Items.Add(new ListItem("-- Seleccione --", ""));
                    foreach (EMPRESA_AMPA empresa in listado)
                        cmbEmpresa.Items.Add(new ListItem(empresa.EMPRESA.NOMBRE, empresa.ID_EMPRESA.ToString()));
                }
                else
                {
                    cmbEmpresa.Items.Add(new ListItem(MasterBase.DatosSesionLogin.Empresa, MasterBase.DatosSesionLogin.IdEmpresa.ToString()));
                    cmbEmpresa.Enabled = false;
                    CargarActividades(MasterBase.DatosSesionLogin.IdEmpresa);
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado empresas", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de empresas');", true);
            }
        }

        /// <summary>
        /// Carga los datos del alumno
        /// </summary>
        private void CargarAlumno()
        {
            try
            {
                ALUMNO alumno = new ALUMNO();
                alumno = NegActividad.GetAlumnnoById(IdAlumno);
                if (alumno.TUTOR.INDIVIDUAL == "N")
                    dvIndividual.Visible = true;

                if (alumno == null|| alumno.CURSO_CLASE == null)
                    throw new Exception("No se ha encontrado al alumno");

                txtNombre.Text = alumno.NOMBRE + " " + alumno.APELLIDO1 + " " + alumno.APELLIDO2;
                txtFecha.Text = alumno.FECHA_NACIMIENTO.ToShortDateString();
                txtCurso.Text = alumno.CURSO_CLASE.CURSO.NOMBRE +" " + alumno.CURSO_CLASE.CLASE.NOMBRE;

                CargarGrid();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar los datos del alumno", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al cargar los datos del alumno');", true);
            }
        }

        /// <summary>
        /// Carga las actividades del alumno
        /// </summary>
        private void CargarGrid()
        {
            try
            {
                List<ALUMNO_ACTIVIDAD> resultado = NegActividad.GetActividadesByAlumnno(IdAlumno);
                gvActividades.DataSource = resultado;
                gvActividades.DataBind();
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar los datos del alumno", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al cargar las actividades del alumno');", true);
            }
        }

        /// <summary>
        /// Carga el combo de la interface con el listado actividades de la empresa
        /// </summary>
        /// <param name="IdEmpresa">Identificador de la empresa a la que pertenecen las actividades</param>
        private void CargarActividades(int IdEmpresa)
        {
            try
            {
                Comun.FiltroActividad filtro = new Comun.FiltroActividad();
                if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.AMPA)
                {
                    filtro.IdEmpresa = IdEmpresa;
                    filtro.IdAMPA = MasterBase.DatosSesionLogin.IdEmpresa;
                }
                else
                {
                    filtro.IdEmpresa = MasterBase.DatosSesionLogin.IdEmpresa;
                    ALUMNO datosAlumno= NegActividad.GetAMPAByAlumno(IdAlumno);
                    if (datosAlumno!=null && datosAlumno.TUTOR != null && datosAlumno.TUTOR.AMPA!=null)
                        filtro.IdAMPA = datosAlumno.TUTOR.AMPA.ID_AMPA;
                }
                filtro.Activo = "S";
                cmbActividad.DataSource = ((PageBase)Page).NegActividad.GetActividades(filtro);
                cmbActividad.DataBind();
                cmbActividad.Items.Insert(0, new ListItem("-- Seleccione --", ""));
                cmbActividad.Enabled = true;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de actividades.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "intercambio", "alert('Se ha producido un error al obtener el listado de actividades');", true);
            }
        }
             
        /// <summary>
        /// Carga el combo de la interface con el listado horarios de la actividad de la empresa
        /// </summary>
        /// <param name="IdActividad">Identificador de la actividad a la que pertenecen los horarios</param>
        private void CargarHorario(int IdActividad)
        {
            try
            {
                cmbHorario.Items.Clear();
                List<ACTIVIDAD_HORARIO> listado = new List<ACTIVIDAD_HORARIO>();
                listado = NegActividad.GetHorarioByActividad(IdActividad);
                cmbHorario.Items.Add(new ListItem("-- Seleccione --", ""));
                foreach (ACTIVIDAD_HORARIO obj in listado)
                    cmbHorario.Items.Add(new ListItem(obj.DIAS + ": (" + obj.HORA_INI + " - " + obj.HORA_FIN + ")", obj.ID_ACT_HORARIO.ToString()));
                cmbHorario.Enabled = true;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de horarios.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "intercambio", "alert('Se ha producido un error al obtener el listado de horarios');", true);
            }
        }

        #endregion

        #region Métodos privados
        /// <summary>
        /// Valida que los campos obligatorios hayan sido rellenados.
        /// </summary>
        /// <returns>True si los datos obligatorios han sido rellenos
        /// False en caso de que alguno no haya sido relleno</returns>
        private bool ValidarFormulario()
        {
            lblEmpresa.Visible = false;
            lblActividad.Visible = false;
            lblHorario.Visible = false;
            _mensaje.Clear();

            if (string.IsNullOrWhiteSpace(cmbEmpresa.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar la empresa de la actividad extraescolar</li>");
                lblEmpresa.Visible = true;
                if (_mensaje == null)
                    cmbEmpresa.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbActividad.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar la actividad extraescolar</li>");
                lblActividad.Visible = true;
                if (_mensaje == null)
                    cmbActividad.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbHorario.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar el horario de la actividad extraescolar</li>");
                lblHorario.Visible = true;
                if (_mensaje == null)
                    cmbHorario.Focus();
            }
            return _mensaje.Length == 0;
        }

        /// <summary>
        /// Inicializa los valores de la actividad
        /// </summary>
        private void VaciarFormularioActividad()
        {
            if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.AMPA)
            {
                cmbEmpresa.ClearSelection();
                cmbActividad.ClearSelection();
                cmbActividad.Enabled = false;
            }
            else
                cmbActividad.ClearSelection();
            cmbHorario.ClearSelection();
            cmbHorario.Enabled = false;
        }
        #endregion

        #region Eventos del grid

        /// <summary>
        /// Procedimiento de la grid, al crear una fila
        /// </summary>
        protected void gvActividades_RowCreated(object sender, GridViewRowEventArgs e)
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
        protected void gvActividades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (gvActividades.Rows.Count > 0)
            {
                //Se recupera el identificador del usuario
                int idActividad = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Baja":
                        try
                        {
                            if (!NegActividad.BajaAlumnoActividad(IdAlumno,idActividad))
                            {
                                Comun.Log.TrazaLog.Error("No se ha podido dar de baja al alumno en la extraescolar. IdAlumno " + IdAlumno.ToString() + ", IdActividadHorario: " + idActividad.ToString());
                                ErrorGeneral("Se ha producido un error al intentar dar de baja al alumno en la actividad extraescolar");
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".gvActividades_RowCommand().Baja(IdActividadHorario: " + idActividad.ToString() + "). Descripcion: " + ex.ToString());
                            ScriptManager.RegisterStartupScript(Page, GetType(), "gestor", "alert('Se ha producido un error al dar de baja al alumno en la extraescolar');", true);
                            return;
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", "alert('El alumno se ha dado de baja correctamente en la actividad.');", true);
                        //Se carga el grid de nuevo para que desaparezca la actividad borrada
                        CargarGrid();
                        break;
                }
            }
        }

        /// <summary>
        /// Evento generado al pulsar sobre el botón cancelar. Limpia las variable de sesión utilizadas y vuelve a la página de mantenimiento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session["IdAlumno"] = null;
            Response.Redirect("AsignarActividad.aspx?grid=S", false);
        }

        #endregion

    }
}
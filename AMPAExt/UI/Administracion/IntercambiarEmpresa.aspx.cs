using System;
using AMPA.Modelo;
using System.Text;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Administracion
{
    /// <summary>
    /// Funcionalidad para realizar el intercambio de actividades extraescolares
    /// </summary>
    public partial class IntercambiarEmpresa : PageBase
    {
        /// <summary>
        /// Propiedad con el mensaje de comunicación para el control
        /// </summary>
        private StringBuilder _mensaje;

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

            if (!Page.IsPostBack)
            {
                CargarEmpresaOrigen();
                CargarEmpresaDestino();
            }
        }

        /// <summary>
        /// Obtiene los datos del usuario introducidos. Se realiza una validación previa sobre el formulario y en caso de que sea correcto, se da de alta en base de datos
        /// </summary>
        protected void BtnIntercambiar_Click(object sender, EventArgs e)
        {
            try
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Limpiar, string.Empty);
                _mensaje = new StringBuilder();
                USUARIO_AMPA nuevoUsuario = new USUARIO_AMPA();
                if (ValidarFormulario())
                {
                    //Se guardan los alumnos que hay en origen
                    List<ALUMNO_ACTIVIDAD> alumnosOrigen = NegActividad.GetAlumnnosByActividad(int.Parse(cmbActividadOri.SelectedValue));

                    //Se insertan los alumnos de origen en actividad y hoario de destino
                    foreach (ALUMNO_ACTIVIDAD alumno in alumnosOrigen)
                        if (NegActividad.AltaAlumnoHorario(alumno)<1)
                            throw new Exception("Error al dar de alta al alumno");
                   //TODO:METERLO EN NEGOCIO EN TRANSACCION
                    //Se eliminan loas alumnos de la actividad  y hoario de origen
                    //Se eliminan los horarios de la actividad  y hoario de origen
                    //Se pone como activo N la actividad  y hoario origen

                    ACTIVIDAD origenActividad = NegActividad.GetActividadById(int.Parse(cmbActividadOri.SelectedValue));
                    ACTIVIDAD destinoActividad = NegActividad.GetActividadById(int.Parse(cmbActividadDes.SelectedValue));
                    origenActividad.ID_EMPRESA = destinoActividad.ID_EMPRESA;
                    origenActividad.NOMBRE = destinoActividad.NOMBRE;
                    origenActividad.DESCRIPCION = destinoActividad.DESCRIPCION;
                    origenActividad.FECHA_MOD = DateTime.Now;
                    origenActividad.OBSERVACIONES = destinoActividad.OBSERVACIONES;
                    origenActividad.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;

                    if (NegActividad.CambiarActividadEmpresa(origenActividad))
                    {
                        VaciarFormulario(this);
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Success, "La actividad se ha intercambiardo correctamente");
                    }
                    else
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al intercambiar la actividad");
                }
                else
                    PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, _mensaje.ToString());
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al intercambiar las actividades.", ex);
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al intercambiar las actividades");
            }
        }
        /// <summary>
        /// Carga el combo de la interface con el listado de empresas para origen del sistema
        /// </summary>
        private void CargarEmpresaOrigen()
        {
            try
            {
                cmbEmpresaOrigen.Items.Clear();
                List<EMPRESA_AMPA> listado = new List<EMPRESA_AMPA>();
                listado = NegUsuario.GetEmpresasByAMPA(MasterBase.DatosSesionLogin.IdEmpresa, true);
                cmbEmpresaOrigen.Items.Add(new ListItem("-- Seleccione --", ""));
                foreach (EMPRESA_AMPA empresa in listado)
                    cmbEmpresaOrigen.Items.Add(new ListItem(empresa.EMPRESA.NOMBRE, empresa.ID_EMPRESA.ToString()));
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado empresas para el origen", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de empresas para el origen');", true);
            }
        }

        /// <summary>
        /// Carga el combo de la interface con el listado de empresas para destino del sistema
        /// </summary>
        private void CargarEmpresaDestino()
        {
            try
            {
                cmbEmpresaDest.Items.Clear();
                List<EMPRESA_AMPA> listado = new List<EMPRESA_AMPA>();
                listado = NegUsuario.GetEmpresasByAMPA(MasterBase.DatosSesionLogin.IdEmpresa, true);
                cmbEmpresaDest.Items.Add(new ListItem("-- Seleccione --", ""));
                foreach (EMPRESA_AMPA empresa in listado)
                    cmbEmpresaDest.Items.Add(new ListItem(empresa.EMPRESA.NOMBRE, empresa.ID_EMPRESA.ToString()));
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de empresas para el destino", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de empresas para el destino');", true);
            }

        }

        /// <summary>
        /// Valida que los campos obligatorios hayan sido rellenados.
        /// </summary>
        /// <returns>True si los datos obligatorios han sido rellenos
        /// False en caso de que alguno no haya sido relleno</returns>
        private bool ValidarFormulario()
        {
            lblEmpresaOri.Visible = false;
            lblActividadOri.Visible = false;
            lblEmpresaDest.Visible = false;
            lblActividadDes.Visible = false;
            lblHorarioDes.Visible = false;
            lblHorarioOri.Visible = false;
            _mensaje.Clear();
            
            if (string.IsNullOrWhiteSpace(cmbEmpresaOrigen.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar la empresa de origen del intercambio</li>");
                lblEmpresaOri.Visible = true;
                if (_mensaje == null)
                    cmbEmpresaOrigen.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbActividadOri.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar la actividad extraescolar de origen</li>");
                lblActividadOri.Visible = true;
                if (_mensaje == null)
                    cmbActividadOri.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbHorarioOri.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar el horario de la actividad extraescolar de origen</li>");
                lblHorarioOri.Visible = true;
                if (_mensaje == null)
                    cmbHorarioOri.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbEmpresaDest.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar la empresa de destino del intercambio</li>");
                lblEmpresaDest.Visible = true;
                if (_mensaje == null)
                    cmbEmpresaDest.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbActividadDes.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar la actividad extraescolar de destino</li>");
                lblActividadDes.Visible = true;
                if (_mensaje == null)
                    cmbActividadDes.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbHorarioDes.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar el horario de la actividad extraescolar de destino</li>");
                lblHorarioDes.Visible = true;
                if (_mensaje == null)
                    cmbHorarioDes.Focus();
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
        /// Al seleccionar la empresa de origen, se cargan las actividades que imparte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbEmpresaOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbEmpresaOrigen.SelectedValue))
            {
                CargarActividadesOri(int.Parse(cmbEmpresaOrigen.SelectedValue));
                cmbActividadOri.Enabled = true;
            }
            else
            {
                cmbActividadOri.Items.Clear();
                cmbActividadOri.Enabled = false;
            }
        }

        /// <summary>
        /// Al seleccionar la empresa de destino, se cargan las actividades que imparte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbEmpresaDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbEmpresaDest.SelectedValue))
            {
                CargarActividadesDest(int.Parse(cmbEmpresaDest.SelectedValue));
                cmbActividadDes.Enabled = true;
            }
            else
            {
                cmbActividadDes.Items.Clear();
                cmbActividadDes.Enabled = false;
            }
        }

        /// <summary>
        /// Al seleccionar la actividad de destino, se cargan los horarios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbActividadDes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbActividadDes.SelectedValue))
            {
                CargarHorarioDes(int.Parse(cmbActividadDes.SelectedValue));
                cmbHorarioDes.Enabled = true;
            }
            else
            {
                cmbHorarioDes.Items.Clear();
                cmbHorarioDes.Enabled = false;
            }
        }

        /// <summary>
        /// Al seleccionar la actividad de destino, se cargan los horarios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbActividadOri_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbActividadOri.SelectedValue))
            {
                CargarHorarioOri(int.Parse(cmbActividadOri.SelectedValue));
                cmbHorarioOri.Enabled = true;
            }
            else
            {
                cmbHorarioOri.Items.Clear();
                cmbHorarioOri.Enabled = false;
            }
        }

        /// <summary>
        /// Carga el combo de la interface con el listado actividades de la empresa de origen
        /// </summary>
        /// <param name="IdEmpresa">Identificador de la empresa a la que pertenecen las actividades</param>
        private void CargarActividadesOri(int IdEmpresa)
        {
            try
            {
                Comun.FiltroActividad filtro = new Comun.FiltroActividad();
                filtro.IdEmpresa = IdEmpresa;
                filtro.IdAMPA = MasterBase.DatosSesionLogin.IdEmpresa;
                filtro.Activo = "S";
                cmbActividadOri.DataSource = ((PageBase)Page).NegActividad.GetActividades(filtro);
                cmbActividadOri.DataBind();
                cmbActividadOri.Items.Insert(0, new ListItem("-- Seleccione --", ""));
                cmbActividadOri.Enabled = true;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de actividades.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "intercambio", "alert('Se ha producido un error al obtener el listado de actividades de origen');", true);
            }
        }
        /// <summary>
        /// Carga el combo de la interface con el listado actividades de la empresa de destino
        /// </summary>
        /// <param name="IdEmpresa">Identificador de la empresa a la que pertenecen las actividades</param>
        private void CargarActividadesDest(int IdEmpresa)
        {
            try
            {
                Comun.FiltroActividad filtro = new Comun.FiltroActividad();
                filtro.IdEmpresa = IdEmpresa;
                filtro.IdAMPA = MasterBase.DatosSesionLogin.IdEmpresa;
                filtro.Activo = "S";
                cmbActividadDes.DataSource = ((PageBase)Page).NegActividad.GetActividades(filtro);
                cmbActividadDes.DataBind();
                cmbActividadDes.Items.Insert(0, new ListItem("-- Seleccione --", ""));
                cmbActividadDes.Enabled = true;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de actividades.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "intercambio", "alert('Se ha producido un error al obtener el listado de actividades de destino');", true);
            }
        }

        /// <summary>
        /// Carga el combo de la interface con el listado horarios de la actividad de la empresa de origen
        /// </summary>
        /// <param name="IdActividad">Identificador de la actividad a la que pertenecen los horarios</param>
        private void CargarHorarioOri(int IdActividad)
        {
            try
            {
                cmbHorarioOri.Items.Clear();
                List<ACTIVIDAD_HORARIO> listado = new List<ACTIVIDAD_HORARIO>();
                listado = NegActividad.GetHorarioByActividad(IdActividad);
                cmbHorarioOri.Items.Add(new ListItem("-- Seleccione --", ""));
                foreach (ACTIVIDAD_HORARIO obj in listado)
                    cmbHorarioOri.Items.Add(new ListItem(obj.DIAS + ": (" + obj.HORA_INI + " - " + obj.HORA_FIN + ")", obj.ID_ACT_HORARIO.ToString()));
                cmbHorarioOri.Enabled = true;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de hoarios.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "intercambio", "alert('Se ha producido un error al obtener el listado de horarios de origen');", true);
            }
        }
        /// <summary>
        /// Carga el combo de la interface con el listado horarios de la actividad de la empresa de destino
        /// </summary>
        /// <param name="IdActividad">Identificador de la actividad a la que pertenecen los horarios</param>
        private void CargarHorarioDes(int IdActividad)
        {
            try
            {
                cmbHorarioDes.Items.Clear();
                List<ACTIVIDAD_HORARIO> listado = new List<ACTIVIDAD_HORARIO>();
                listado = NegActividad.GetHorarioByActividad(IdActividad);
                cmbHorarioDes.Items.Add(new ListItem("-- Seleccione --", ""));
                foreach (ACTIVIDAD_HORARIO obj in listado)
                    cmbHorarioDes.Items.Add(new ListItem(obj.DIAS + ": (" + obj.HORA_INI + " - " + obj.HORA_FIN + ")", obj.ID_ACT_HORARIO.ToString()));
                cmbHorarioDes.Enabled = true;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de horarios.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "intercambio", "alert('Se ha producido un error al obtener el listado de horarios de destino');", true);
            }
        }

    }
}
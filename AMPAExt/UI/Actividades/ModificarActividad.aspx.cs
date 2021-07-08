using System;
using AMPA.Modelo;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Actividades
{
    /// <summary>
    /// Funcionalidad para modificar una actividad extraescolar
    /// </summary>
    public partial class ModificarActividad : PageBase
    {
        /// <summary>
        /// Propiedad con el mensaje de comunicación para el control
        /// </summary>
        private StringBuilder _mensaje;

        /// <summary>
        /// Listado de horarios de la actividad
        /// </summary>
        public List<ACTIVIDAD_HORARIO> Horario
        {
            get
            {
                if (Session["listadoHorario"] == null)
                    return null;
                else
                    return (List<ACTIVIDAD_HORARIO>)Session["listadoHorario"];
            }
        }
        /// <summary>
        /// Listado de descuentos de la actividad
        /// </summary>
        public List<ACTIVIDAD_DESCUENTO> Descuento
        {
            get
            {
                if (Session["listadoDescuento"] == null)
                    return null;
                else
                    return (List<ACTIVIDAD_DESCUENTO>)Session["listadoDescuento"];
            }
        }
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
                    Session["listadoHorario"] = null;
                    Session["listadoDescuento"] = null;
                    if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.EXTR)
                    {
                        dvAMPA.Visible = true;
                        dvEmpresa.Visible = false;
                        CargarMonitores(MasterBase.DatosSesionLogin.IdEmpresa);
                        CargarAMPA();
                    }
                    else
                    {
                        dvAMPA.Visible = false;
                        dvEmpresa.Visible = true;
                        CargarEmpresas();
                    }
                    CargarDescuentos();
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al cargar modificación de actividad", ex);
                Error("Se ha producido un error al cargar los datos de la actividad");
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

        /// <summary>
        /// Procedimiento de la grid, al ejecutar un comando
        /// </summary>
        protected void gvHorarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (gvHorarios.Rows.Count > 0)
                {
                    //Se recupera el identificador de la fila
                    int idHorario = int.Parse(e.CommandArgument.ToString());
                    switch (e.CommandName)
                    {
                        case "Baja":
                            if (Horario != null)
                                Horario.Remove(Horario.Find(c => c.ID_ACT_HORARIO == idHorario));
                            Session["listadoHorario"] = Horario;
                            gvHorarios.DataSource = Horario;
                            gvHorarios.DataBind();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".gvHorarios_RowCommand().Eliminar." + ex.ToString());
                ScriptManager.RegisterStartupScript(Page, GetType(), "gestor", "alert('Se ha producido un error al borrar el horario');", true);
                return;
            }
        }

        /// <summary>
        /// Procedimiento de la grid, al ejecutar un comando
        /// </summary>
        protected void gvDescuento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (gvDescuento.Rows.Count > 0)
                {
                    //Se recupera el identificador de la fila
                    int idDescuento = int.Parse(e.CommandArgument.ToString());
                    switch (e.CommandName)
                    {
                        case "Baja":
                            if (Descuento != null)
                                Descuento.Remove(Descuento.Find(c => c.ID_ACT_DESCUENTO== idDescuento));
                            Session["listadoDescuento"] = Descuento;
                            gvDescuento.DataSource = Descuento;
                            gvDescuento.DataBind();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".gvDescuento_RowCommand().Eliminar." + ex.ToString());
                ScriptManager.RegisterStartupScript(Page, GetType(), "gestor", "alert('Se ha producido un error al borrar el descuento');", true);
                return;
            }
        }
        #endregion

        /// <summary>
        /// Se realiza una validación previa sobre el formulario y en caso de que sea correcto, se da de alta en base de datos
        /// </summary>
        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Limpiar, string.Empty);
                _mensaje = new StringBuilder();
                if (ValidarFormulario())
                {
                    ACTIVIDAD actividad = NegActividad.GetActividadById(IdActividad);
                    actividad.NOMBRE = txtActNombre.Text;
                    actividad.DESCRIPCION = txtActDescripcion.Text;
                    actividad.FECHA_MOD = DateTime.Now;
                    actividad.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;
                    if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.AMPA)
                    {
                        actividad.ID_AMPA = MasterBase.DatosSesionLogin.IdEmpresa;
                        actividad.ID_EMPRESA = int.Parse(cmbEmpresa.SelectedValue);
                    }
                    else
                    {
                        actividad.ID_AMPA = int.Parse(cmbAMPA.SelectedValue);
                        actividad.ID_EMPRESA = MasterBase.DatosSesionLogin.IdEmpresa;
                    }
                    actividad.ACTIVO = "S";
                    #region horario
                    List<ACTIVIDAD_HORARIO> actHorario = new List<ACTIVIDAD_HORARIO>();
                    foreach (ACTIVIDAD_HORARIO horariosAct in Horario)
                    {
                        horariosAct.ID_ACT_HORARIO = 0;
                        horariosAct.FECHA_MOD = DateTime.Now;
                        horariosAct.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;
                        horariosAct.MONITOR = null;
                        actHorario.Add(horariosAct);
                    }
                    #endregion
                    #region Descuentos
                    List<ACTIVIDAD_DESCUENTO> actDescuento = new List<ACTIVIDAD_DESCUENTO>();
                    foreach (ACTIVIDAD_DESCUENTO descuentoAct in Descuento)
                    {
                        descuentoAct.ID_ACT_DESCUENTO = 0;
                        descuentoAct.DESCUENTO = null;
                        descuentoAct.FECHA_MOD = DateTime.Now;
                        descuentoAct.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;
                        actDescuento.Add(descuentoAct);
                    }
                    #endregion
                    if (NegActividad.ModificarActividad(actividad, actHorario, actDescuento))
                    {
                        Session["listadoHorario"] = null;
                        Session["listadoDescuento"] = null;
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Success, "La actividad extraescolar ha sido modificada correctamente");
                    }
                    else
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al modificar la actividad extraescolar");
                }
                else
                    PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, _mensaje.ToString());
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al modificar la actividad extraescolar.", ex);
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al modificar la actividad extraescolar");
            }
        }

        /// <summary>
        /// Evento generado al pulsar en añadir un nuevo horario para la actividad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnadirHora_Click(object sender, EventArgs e)
        {
            _mensaje = new StringBuilder();
            PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Limpiar, string.Empty);
            bool seleccionado = false;
            string dias = string.Empty;
            foreach (ListItem item in chkDias.Items)
                if (item.Selected == true)
                {
                    seleccionado = true;
                    //Acción al encontrar un check seleccionado
                    dias += item.Value.ToString() + ",";
                }

            if (!seleccionado)
                _mensaje.AppendLine("<li type='dic'>Es obligatorio marcar los días de la actividad</li>");
            if (string.IsNullOrEmpty(txtHoraIni.Value))
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar la hora de inicio de la actividad</li>");
            if (string.IsNullOrEmpty(txtHoraFin.Value))
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar la hora de finalización de la actividad</li>");
            if (string.IsNullOrEmpty(txtActCuota.Text))
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar una cuota para el horario</li>");
            if (_mensaje.Length != 0)
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, _mensaje.ToString());
            else
            {
                List<ACTIVIDAD_HORARIO> listado = new List<ACTIVIDAD_HORARIO>();
                if (Horario != null)
                    listado = Horario;
                ACTIVIDAD_HORARIO nuevo = new ACTIVIDAD_HORARIO();
                nuevo.ID_ACT_HORARIO = listado.Count;
                nuevo.DIAS = dias.Remove(dias.Length - 1);
                nuevo.HORA_INI = txtHoraIni.Value;
                nuevo.HORA_FIN = txtHoraFin.Value;
                nuevo.CUOTA = decimal.Parse(txtActCuota.Text);
                if (!string.IsNullOrEmpty(cmbMonitor.SelectedValue))
                {
                    nuevo.ID_MONITOR = int.Parse(cmbMonitor.SelectedValue);
                    nuevo.MONITOR = new MONITOR();
                    nuevo.MONITOR.NOMBRE = cmbMonitor.SelectedItem.Text;
                }
                listado.Add(nuevo);
                Session["listadoHorario"] = listado;
                gvHorarios.DataSource = listado;
                gvHorarios.DataBind();
            }
        }

        /// <summary>
        /// Valida que los campos obligatorios hayan sido rellenados.
        /// </summary>
        /// <returns>True si los datos obligatorios han sido rellenos
        /// False en caso de que alguno no haya sido relleno</returns>
        private bool ValidarFormulario()
        {
            _mensaje.Clear();

            lblActDescripcion.Visible = false;
            lblActHorario.Visible = false;
            lblActNombre.Visible = false;
            lblAMPA.Visible = false;
            lblEmpresa.Visible = false;
            if (MasterBase.DatosSesionLogin.CodTipoUsuario== Comun.TipoDatos.TipoUsuario.AMPA && string.IsNullOrWhiteSpace(cmbEmpresa.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio seleccionar la empresa a la que pertenece la actividad</li>");
                lblEmpresa.Visible = true;
                cmbEmpresa.Focus();
            }
            if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.EXTR && string.IsNullOrWhiteSpace(cmbAMPA.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio seleccionar la AMPA donde se impartirá la actividad</li>");
                lblAMPA.Visible = true;
                cmbAMPA.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtActNombre.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir un nombre para la actividad</li>");
                lblActNombre.Visible = true;
                txtActNombre.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtActDescripcion.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir una descripción para la actividad</li>");
                lblActDescripcion.Visible = true;
                if (_mensaje == null)
                    txtActDescripcion.Focus();
            }
            if (Horario == null || Horario.Count == 0)
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir al menos un horario para la actividad</li>");
                lblActHorario.Visible = true;
                if (_mensaje == null)
                    chkDias.Focus();
            }
            return _mensaje.Length == 0;
        }

        /// <summary>
        /// Carga el combo de la interface con el listado monitores de la empresa
        /// </summary>
        /// <param name="IdEmpresa">Identificador de la empresa a la que pertenecen los monitores</param>
        private void CargarMonitores(int IdEmpresa)
        {
            try
            {
                cmbMonitor.DataSource = ((PageBase)Page).NegExtraescolar.GetMonitores(IdEmpresa);
                cmbMonitor.DataBind();
                cmbMonitor.Items.Insert(0, new ListItem("-- Seleccione --", ""));
                cmbMonitor.Enabled = true;
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de monitores.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de monitores');", true);
            }
        }

        /// <summary>
        /// Carga el combo de la interface con el listado de descuentos
        /// </summary>
        private void CargarDescuentos()
        {
            try
            {
                cmbActDescuentos.DataSource = ((PageBase)Page).NegTablasMaestras.GetDescuentos();
                cmbActDescuentos.DataBind();
                cmbActDescuentos.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de descuentos de la tabla maestra.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al cargar el listado de descuentos');", true);
            }

        }

        /// <summary>
        /// Carga el combo de las empresas extraescolares. Si es un usuario de AMPA, carga las empresas extraescolares con la que trabaja. Si es un usuario
        /// de extraescolar, carga sólo su empresa.
        /// </summary>
        private void CargarEmpresas()
        {
            try
            {
                cmbEmpresa.Items.Clear();
                if (MasterBase.DatosSesionLogin.CodTipoUsuario != Comun.TipoDatos.TipoUsuario.AMPA)
                {
                    cmbEmpresa.Items.Add(new ListItem(MasterBase.DatosSesionLogin.Empresa, MasterBase.DatosSesionLogin.IdEmpresa.ToString()));
                    cmbEmpresa.Enabled = false;
                }
                else
                {
                    List<EMPRESA_AMPA> listado = new List<EMPRESA_AMPA>();
                    listado = NegUsuario.GetEmpresasByAMPA(MasterBase.DatosSesionLogin.IdEmpresa, true);
                    cmbEmpresa.Items.Add(new ListItem("-- Seleccione --", ""));
                    foreach (EMPRESA_AMPA empresa in listado)
                        cmbEmpresa.Items.Add(new ListItem(empresa.EMPRESA.NOMBRE, empresa.ID_EMPRESA.ToString()));
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de las empresas extraescolares.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de empresas extraescolares');", true);
            }
        }

        /// <summary>
        /// Carga el combo de las AMPA. Si es un usuario de AMPA, carga su AMPA. Si es un usuario de extraescolar, carga las AMPA con las que trabaja
        /// </summary>
        private void CargarAMPA()
        {
            try
            {
                cmbAMPA.Items.Clear();
                if (MasterBase.DatosSesionLogin.CodTipoUsuario == Comun.TipoDatos.TipoUsuario.AMPA)
                {
                    cmbAMPA.Items.Add(new ListItem(MasterBase.DatosSesionLogin.Empresa, MasterBase.DatosSesionLogin.IdEmpresa.ToString()));
                    cmbAMPA.Enabled = false;
                }
                else
                {
                    List<EMPRESA_AMPA> listado = new List<EMPRESA_AMPA>();
                    listado = NegUsuario.GetAMPASByEmpresa(MasterBase.DatosSesionLogin.IdEmpresa);
                    cmbAMPA.Items.Add(new ListItem("-- Seleccione --", ""));
                    foreach (EMPRESA_AMPA empresa in listado)
                        cmbAMPA.Items.Add(new ListItem(empresa.AMPA.NOMBRE, empresa.ID_AMPA.ToString()));
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de las AMPA.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de AMPA');", true);
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
                Session["listadoHorario"] = datos.ACTIVIDAD_HORARIO;
                gvHorarios.DataSource = datos.ACTIVIDAD_HORARIO;
                gvHorarios.DataBind();

                Session["listadoDescuento"] = datos.ACTIVIDAD_DESCUENTO;
                gvDescuento.DataSource = datos.ACTIVIDAD_DESCUENTO;
                gvDescuento.DataBind();

                cmbEmpresa.SelectedValue = datos.ID_EMPRESA.ToString();
                cmbAMPA.SelectedValue = datos.ID_AMPA.ToString();
                CargarMonitores(datos.ID_EMPRESA);
            }
            else
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "ERROR: No se ha encontrado al usuario en el sistema");
            }
        }

        /// <summary>
        /// Al seleccionar una empresa, se carga el listado de monitores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbEmpresa.SelectedValue))
                CargarMonitores(int.Parse(cmbEmpresa.SelectedValue));
            else
            {
                cmbMonitor.Items.Clear();
                cmbMonitor.Enabled = false;
            }

        }

        /// <summary>
        /// Evento generado al pulsar en añadir un nuevo descuento para la actividad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnDescuento_Click(object sender, EventArgs e)
        {
            _mensaje = new StringBuilder();
            PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Limpiar, string.Empty);
            if (string.IsNullOrEmpty(cmbActDescuentos.SelectedValue))
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar un descuento para la actividad</li>");
            if (string.IsNullOrEmpty(txtValor.Text))
                _mensaje.AppendLine("<li type='dic'>Es obligatorio indicar el valor del descuento</li>");
            if (_mensaje.Length != 0)
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, _mensaje.ToString());
            else
            {
                List<ACTIVIDAD_DESCUENTO> listado = new List<ACTIVIDAD_DESCUENTO>();
                if (Descuento != null)
                    listado = Descuento;
                ACTIVIDAD_DESCUENTO nuevo = new ACTIVIDAD_DESCUENTO();
                nuevo.ID_ACT_DESCUENTO = listado.Count;
                nuevo.ID_DESCUENTO = int.Parse(cmbActDescuentos.SelectedValue);
                nuevo.DESCUENTO = new DESCUENTO();
                nuevo.DESCUENTO.NOMBRE = cmbActDescuentos.SelectedItem.Text;
                nuevo.VALOR = int.Parse(txtValor.Text);
                listado.Add(nuevo);
                Session["listadoDescuento"] = listado;
                gvDescuento.DataSource = listado;
                gvDescuento.DataBind();
            }
        }

        /// <summary>
        /// Evento generado al pulsar sobre el botón cancelar. Limpia las variable de sesión utilizadas y vuelve a la página de gestión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session["IdActividad"] = null;
            Session["listadoHorario"] = null;
            Session["listadoDescuento"] = null;
            Response.Redirect("GestionActividades.aspx?grid=S", false);
        }
    }
}
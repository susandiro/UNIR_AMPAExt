using System;
using AMPA.Modelo;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMPAExt.UI.Socio
{
    /// <summary>
    /// Funcionalidad para un nuevo socio de AMPA
    /// </summary>
    public partial class AltaSocio : PageBase
    {
        /// <summary>
        /// Propiedad con el mensaje de comunicación para el control
        /// </summary>
        private StringBuilder _mensaje;

        /// <summary>
        /// Listado de alumnos del socio
        /// </summary>
        public List<ALUMNO> Alumnos
        {
            get
            {
                if (Session["listadoAlumnos"] == null)
                    return null;
                else
                    return (List<ALUMNO>)Session["listadoAlumnos"];
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
                if (!Page.IsPostBack)
                {
                    Session["listadoAlumnos"] = null;
                    CargarTipoDocumentoT1();
                    CargarTipoDocumentoT2();
                    CargarCurso();
                }
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

        /// <summary>
        /// Procedimiento de la grid, al ejecutar un comando
        /// </summary>
        protected void gvHijo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (gvHijo.Rows.Count > 0)
                {
                    //Se recupera el identificador de la fila
                    int idAlumno = int.Parse(e.CommandArgument.ToString());
                    switch (e.CommandName)
                    {
                        case "Baja":
                            if (Alumnos != null)
                                Alumnos.Remove(Alumnos.Find(c => c.ID_ALUMNO == idAlumno));
                            Session["listadoAlumnos"] = Alumnos;
                            gvHijo.DataSource = Alumnos;
                            gvHijo.DataBind();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".gvHijo_RowCommand().Eliminar." + ex.ToString());
                ScriptManager.RegisterStartupScript(Page, GetType(), "gestor", "alert('Se ha producido un error al borrar el alumno');", true);
                return;
            }
        }


        #endregion

        /// <summary>
        /// Se realiza una validación previa sobre el formulario y en caso de que sea correcto, se da de alta en base de datos
        /// </summary>
        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Limpiar, string.Empty);
                _mensaje = new StringBuilder();
                if (ValidarFormulario())
                {
                    TUTOR tutor = new TUTOR();
                    tutor.T1_NOMBRE = txtNombre1.Text;
                    tutor.T1_APELLIDO1 = txtApellido1_1.Text;
                    tutor.T1_APELLIDO2 = txtApellido2_1.Text;
                    tutor.T1_EMAIL = txtemail1.Text;
                    tutor.T1_ID_TIPO_DOCUMENTO = int.Parse(cmbTipoDocumento1.SelectedValue);
                    tutor.T1_NUMERO_DOCUMENTO = txtNumDocumento1.Text;
                    tutor.T1_TELEFONO = txtTelefono1.Text;
                    tutor.T2_NOMBRE = txtNombre2.Text;
                    tutor.T2_APELLIDO1 = txtApellido1_2.Text;
                    tutor.T2_APELLIDO2 = txtApellido2_2.Text;
                    tutor.T2_EMAIL = txtemail2.Text;
                    if (!string.IsNullOrEmpty(cmbTipoDocumento2.SelectedValue))
                        tutor.T2_ID_TIPO_DOCUMENTO = int.Parse(cmbTipoDocumento2.SelectedValue);
                    tutor.T2_NUMERO_DOCUMENTO = txtNumDocumento2.Text;
                    tutor.T2_TELEFONO = txtTelefono2.Text;
                    tutor.FECHA = DateTime.Now;
                    tutor.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;
                    tutor.INDIVIDUAL = (rdIndividualNo.Checked) ? "N" : "S";
                    tutor.ID_AMPA = MasterBase.DatosSesionLogin.IdEmpresa;
                                        
                    #region 
                    List<ALUMNO> actAlumnos = new List<ALUMNO>();
                    foreach (ALUMNO hijo in Alumnos)
                    {
                        hijo.ID_ALUMNO = 0;
                        hijo.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;
                        hijo.FECHA = DateTime.Now;
                        actAlumnos.Add(hijo);
                    }
                    #endregion

                    int numSocio = NegSocio.AltaSocio(tutor, actAlumnos);
                    if (numSocio > 0)
                    {
                        VaciarFormulario(this);
                        Session["listadoAlumnos"] = null;
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Success, "El socio se ha sido dado de alta correctamente. Su número como socio en la AMPA es el: " + numSocio.ToString());
                    }
                    else
                        PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al dar de alta al socio en la AMPA");
                }
                else
                    PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, _mensaje.ToString());
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al insertar el socio.", ex);
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, "Se ha producido un error al dar de alta el socio en la AMPA");
            }
        }

        /// <summary>
        /// Evento generado al pulsar en añadir un alumno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnadirHijo_Click(object sender, EventArgs e)
        {
            _mensaje = new StringBuilder();
            PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Limpiar, string.Empty);
            if (!ValidarFormularioHijo())
                PanelInfo.MostrarMensaje(Comun.TipoDatos.TipoError.Error, _mensaje.ToString());
            else
            {
                List<ALUMNO> listado = new List<ALUMNO>();
                if (Alumnos != null)
                    listado = Alumnos;
                ALUMNO nuevo = new ALUMNO();
                nuevo.ID_CURSO_CLASE = int.Parse(cmbClase.SelectedValue);
                nuevo.CURSO_CLASE = new CURSO_CLASE();

                CURSO curso = new CURSO();
                curso.NOMBRE = cmbCurso.SelectedItem.Text;
                CLASE clase = new CLASE();
                clase.NOMBRE = cmbClase.SelectedItem.Text;

                CURSO_CLASE cursoClase = new CURSO_CLASE();
                cursoClase.CURSO = new CURSO();
                cursoClase.CURSO = curso;
                cursoClase.CLASE = new CLASE();
                cursoClase.CLASE = clase;

                nuevo.CURSO_CLASE = cursoClase;

                nuevo.NOMBRE = txtNombre.Text.Trim();
                nuevo.APELLIDO1 = txtApellido1.Text.Trim();
                nuevo.APELLIDO2 = txtApellido2.Text.Trim();
                nuevo.FECHA_NACIMIENTO = DateTime.Parse(txtFecha.Text);
                nuevo.USUARIO = MasterBase.DatosSesionLogin.DatosUsuario;
                nuevo.FECHA = DateTime.Now;
               
                listado.Add(nuevo);
                Session["listadoAlumnos"] = listado;
                gvHijo.DataSource = listado;
                gvHijo.DataBind();
                VaciarformularioHijo();
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
            lblApellido1_1.Visible = false;
            lblEmail1.Visible = false;
            lblNombre1.Visible = false;
            lblNumDocumento1.Visible = false;
            lblTelefono1.Visible = false;
            lblTipoDocumento1.Visible = false;
            lblHijos.Visible = false;
            if (string.IsNullOrWhiteSpace(txtNombre1.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el nombre del padre/tutor 1</li>");
                lblNombre1.Visible = true;
                txtNombre1.Focus();
            }

            if (string.IsNullOrWhiteSpace(txtApellido1_1.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el primer apellido del padre/tutor 1</li>");
                lblApellido1_1.Visible = true;
                txtApellido1_1.Focus();
            }

            if (string.IsNullOrWhiteSpace(cmbTipoDocumento1.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio seleccionar el tipo de documento del padre/tutor 1</li>");
                lblTipoDocumento1.Visible = true;
                cmbTipoDocumento1.Focus();
            }

            if (string.IsNullOrWhiteSpace(txtNumDocumento1.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el número de documento del padre/tutor 1</li>");
                lblNumDocumento1.Visible = true;
                txtNumDocumento1.Focus();
            }
           
            if (string.IsNullOrWhiteSpace(txtTelefono1.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el teléfono de contacto del padre/tutor 1</li>");
                lblTelefono1.Visible = true;
                txtTelefono1.Focus();
            }

            if (string.IsNullOrWhiteSpace(txtemail1.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el coreo electrónico de contacto del padre/tutor 1</li>");
                lblEmail1.Visible = true;
                if (_mensaje == null)
                    txtemail1.Focus();
            }
            if (Alumnos == null || Alumnos.Count == 0)
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir al menos un hijo/alumno para poder ser socio</li>");
                lblHijos.Visible = true;
                if (_mensaje == null)
                    txtNombre.Focus();
            }
            return _mensaje.Length == 0;
        }

        /// <summary>
        /// Valida que los campos obligatorios hayan sido rellenados.
        /// </summary>
        /// <returns>True si los datos obligatorios han sido rellenos
        /// False en caso de que alguno no haya sido relleno</returns>
        private bool ValidarFormularioHijo()
        {
            _mensaje.Clear();
            lblNombre.Visible = false;
            lblApellido1.Visible = false;
            lblApellido2.Visible = false;
            lblFecha.Visible = false;
            lblCurso.Visible = false;
            lblClase.Visible = false;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el nombre del alumno</li>");
                lblNombre.Visible = true;
                txtNombre.Focus();
            }
            if (string.IsNullOrWhiteSpace(txtApellido1.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir el primer apellido del alumno</li>");
                lblApellido1.Visible = true;
                if (_mensaje == null)
                    txtApellido1.Focus();
            }

            if (string.IsNullOrWhiteSpace(txtFecha.Text))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio introducir la fecha de nacimiento del alumno</li>");
                lblFecha.Visible = true;
                if (_mensaje == null)
                    txtFecha.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbCurso.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio seleccionar el curso del alumno</li>");
                lblCurso.Visible = true;
                cmbCurso.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbClase.SelectedValue))
            {
                _mensaje.AppendLine("<li type='dic'>Es obligatorio seleccionar la clase del alumno</li>");
                lblClase.Visible = true;
                cmbClase.Focus();
            }
            return _mensaje.Length == 0;
        }

        /// <summary>
        /// Carga el combo de la interface con el listado de tipos de documento
        /// </summary>
        private void CargarTipoDocumentoT1()
        {
            try
            {
                cmbTipoDocumento1.DataSource = ((PageBase)Page).NegTablasMaestras.GetTiposDocumento();
                cmbTipoDocumento1.DataBind();
                cmbTipoDocumento1.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de tipos de documento de la tabla maestra.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de tipos de documento');", true);
            }
        }

        /// <summary>
        /// Carga el combo de la interface con el listado de tipos de documento
        /// </summary>
        private void CargarTipoDocumentoT2()
        {
            try
            {
                cmbTipoDocumento2.DataSource = ((PageBase)Page).NegTablasMaestras.GetTiposDocumento();
                cmbTipoDocumento2.DataBind();
                cmbTipoDocumento2.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }
            catch (Exception ex)
            {
                Comun.Log.TrazaLog.Error("Error al obtener el listado de tipos de documento de la tabla maestra.", ex);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('Se ha producido un error al obtener el listado de tipos de documento');", true);
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
        /// Recorre los controles y los inicializa
        /// </summary>
        /// <param name="control">Control a recorrer</param>
        private void VaciarFormulario(Control control)
        {
            gvHijo.DataSource = null;
            gvHijo.DataBind();

            txtFecha.Text = string.Empty;
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
        /// Vacia el formulario de hijo
        /// </summary>
        private void VaciarformularioHijo()
        {
            _mensaje.Clear();
            txtNombre.Text = string.Empty;
            txtApellido1.Text = string.Empty;
            txtApellido2.Text = string.Empty;
            txtFecha.Text = string.Empty;
            cmbCurso.ClearSelection();
            cmbClase.ClearSelection();
            cmbClase.Enabled = false;
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
    }
}
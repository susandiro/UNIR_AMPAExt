using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using GUAI.Ent.Clases;
using DividendoDigital.Modelo;

public partial class Conexion_Usuario : PageBase
{
    #region propiedades

    /// <summary>
    /// Identificador de la persona de la que se desea modificar los datos y/o perfil
    /// </summary>
    public long IdPersona
    {
        get
        {
            long error;
            if (Request["idPer"] != null && long.TryParse(Request["idPer"], out error))
                return long.Parse(Request["idPer"].ToString());
            return -1;
        }
    }

    /// <summary>
    /// Identificador de la persona de la que se desea modificar los datos y/o perfil
    /// </summary>
    public long IdGrupoPerfil
    {
        get
        {
            if (Session["idGrupoPerfil"] != null)
                return long.Parse(Session["idGrupoPerfil"].ToString());
            else
                return -1;
        }
        set
        {
            Session["idGrupoPerfil"] = value;
        }
    }

    #endregion

    #region Eventos

    /// <summary>
    /// Evento que se lanza antes de que se inicie la página. Se le indica que no muestre la cabecera y que se
    /// trata de una ventana secundaria
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreInit(EventArgs e)
    {
        MasterBase.MostrarCabecera = false;
        MasterBase.EsPopup = true;
    }

    /// <summary>
    /// Evento lanzado cuando se carga la página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (MasterBase.TpUsuario != Constantes.TipoUsuario.ADM || MasterBase.PerfilUsuario != Constantes.RevPerfil.Nivel3)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('No tiene permiso para acceder a esta aplicación');", true);
            Session.Clear();
            Response.Redirect("~/login.aspx", false);
            return;
        }
        System.Web.UI.HtmlControls.HtmlGenericControl script = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
        script.Attributes.Add("type", "text/javascript");
        script.Attributes.Add("src", MasterBase.RelativeURL + "Scripts/jquery-2.1.1.min.js");
        Header.Controls.Add(script);

        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("Pragma", "no-cache");
        Response.AddHeader("Expires", "0");

        if (!IsPostBack)
        {
            IdGrupoPerfil = -1;
            CargarComboGrupoUsuario();

            CargarDatosPersona();
            txtNombre.Focus();
        }
    }

    /// <summary>
    /// Evento producido al pulsar sobre el botón Guardar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAlta_Click(object sender, EventArgs e)
    {
        try
        {
            GUAI.Neg.NegPersona guaiNegPersona = new GUAI.Neg.NegPersona();
            Persona buscarPersona = guaiNegPersona.ExistePersona("OTROS", "DV_" + txtLogin.Text.ToUpper());
            if (buscarPersona == null || buscarPersona.IdPersona == IdPersona)
            {
                long resultado;
                GUAI.Neg.NegPersona guaiNeg = new GUAI.Neg.NegPersona();
                Persona usuario = new Persona();
                usuario.Apellido1 = txtAp1.Text;
                usuario.Apellido2 = txtAp2.Text;
                usuario.Email = txtEmail.Text;
                usuario.Nombre = txtNombre.Text;
                usuario.NumeroDocumento = "DV_" + txtLogin.Text.ToUpper();
                usuario.TipoDocumento = "OTROS";
                usuario.Observaciones = txtObservaciones.Value;
                if (IdPersona < 1 || !string.IsNullOrEmpty(txtPass.Value))
                    usuario.Password = txtPass.Value;

                usuario.Usuario = txtLogin.Text.ToUpper();

                string codigo = cmbGrupoUsuario.SelectedValue.Split('|')[0];
                string idGrupoUsuario = cmbGrupoUsuario.SelectedValue.Split('|')[1];

                List<PersonaOrganizacion> lstpermisos = new List<PersonaOrganizacion>();
                PersonaOrganizacion permisos = new PersonaOrganizacion();
                permisos.Perfil = new Perfil();
                if (codigo == Constantes.TipoUsuario.CAU.ToString())
                    permisos.Perfil.IdPerfil = Constantes.TipoPerfil.Basico.GetHashCode();
                else
                    permisos.Perfil.IdPerfil = long.Parse(cmbNivel.SelectedValue);

                permisos.Perfil.CertificadoObligatorio = GUAI.Recursos.Tipos.EnumSiNo.NO;
                permisos.Organizacion = new Organizacion();
                permisos.Organizacion.IdOrganizacion = GUAI.Recursos.Tipos.Constantes.ID_MINISTERIO;
                permisos.GrupoUsuario = new GrupoUsuario();
                permisos.GrupoUsuario.IdGrupoUsuario = long.Parse(idGrupoUsuario);
                permisos.GrupoUsuario.Codigo = codigo;
                lstpermisos.Add(permisos);

                if (IdPersona > 0) //Es una modificación
                {
                    usuario.IdPersona = IdPersona;
                   
                    GUAI.Neg.NegPersona guaiPersNeg = new GUAI.Neg.NegPersona();
                    GUAI.Neg.NegGestionPermisos guaiPermNeg = new GUAI.Neg.NegGestionPermisos();

                    List<long> lstAplicaciones = new List<long>();
                    lstAplicaciones.Add(Constantes.IdAplicacion);

                    List<PersonaOrganizacion> lstEstadoActual = new List<PersonaOrganizacion>();
                    lstEstadoActual = guaiPermNeg.GetPersonaOrganizaciones(usuario.IdPersona, GUAI.Recursos.Tipos.Constantes.ID_MINISTERIO, lstAplicaciones);
                    if (lstEstadoActual != null || lstEstadoActual.Count > 0)
                    {
                        if (lstEstadoActual[0].Perfil.CertificadoObligatorio == GUAI.Recursos.Tipos.EnumSiNo.NO) //Nos aseguramos de que el usuario se pueda modificar y no esté dado de baja
                        {
                            bool correo = (string.IsNullOrEmpty(txtPass.Value)) ? false : true;

                            GrupoUsuario guNuevo = new GrupoUsuario();
                            guNuevo.IdGrupoUsuario = long.Parse(idGrupoUsuario);
                            guNuevo.Codigo = codigo;

                            guaiPersNeg.DVModificarPersona(usuario, guNuevo, permisos.Perfil.IdPerfil, IdGrupoPerfil, correo);
                            if (!string.IsNullOrEmpty(guaiPersNeg.ErrorMsg))
                                ScriptManager.RegisterStartupScript(Page, GetType(), "usuario", "alert('" + guaiPersNeg.ErrorMsg + "');", true);
                            resultado = 1;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "usuario", "alert('Acción no permitida.'); window.parent.closePopUp();window.parent.refrescar();", true);
                            return;
                        }
                    }
                    else
                        throw new Exception("No se ha podido modificar el usuario " + usuario.IdPersona.ToString() + ". No se ha encontrado en el sistema");
                }
                else
                    resultado = guaiNeg.AltaPersona(usuario, lstpermisos);

                if (resultado > 0)
                {
                    if (IdPersona > 0)
                        ScriptManager.RegisterStartupScript(Page, GetType(), "usuario", "alert('El usuario se ha modificado correctamente.');window.parent.closePopUp();window.parent.refrescar();", true);
                    else
                        ScriptManager.RegisterStartupScript(Page, GetType(), "usuario", "alert('El usuario ha sido dado de alta correctamente en el sistema.');window.parent.closePopUp();window.parent.refrescar();", true);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, GetType(), "usuario", "alert('Se ha producido un error al dar de alta al usuario, por favor inténtelo más tarde.');window.parent.closePopUp();window.parent.refrescar();", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, GetType(), "usuario", "alert('El login introducido ya está siendo utilizado en el sistema. Por favor, introduzca un login distinto.');", true);
        }
        catch (Exception ex)
        {
            Log.WriteLog(Log.TipoLog.LogError, Constantes.NombreAplicacion, "Error en " + this.GetType().FullName + ".btnAlta_Click(). Excepcion: " + ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "usuario", "alert('" + Constantes.ErrorGenerico + "'); window.parent.closePopUp();window.parent.refrescar();", true);
        }
    }

    /// <summary>
    /// Evento producido al seleccionar el tipo de usuario. Si el tipo seleccionado es un revisor, se carga el nivel de 
    /// usuario para que se indique si es de nivel1, 2 o 3
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmbGrupoUsuario_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            string codigo = cmbGrupoUsuario.SelectedValue.Split('|')[0];
            if (codigo == Constantes.TipoUsuario.RVS.ToString())
            {
                //Carga los niveles disponibles para el revisor
                CargarNivelRevisores();

                cmbGrupoUsuario.Focus();
                dvNivel.Style.Remove("display");
                RFV_Nivel.Enabled = true;
            }
            else if (codigo == Constantes.TipoUsuario.ADM.ToString())
            {
                //Carga los niveles disponibles para el administrador
                CargarNivelAdministradores();

                cmbGrupoUsuario.Focus();
                dvNivel.Style.Remove("display");
                RFV_Nivel.Enabled = true;
            }
            else
            {
                cmbGrupoUsuario.Focus();
                RFV_Nivel.Enabled = false;
                dvNivel.Style.Add("display", "none");
                cmbNivel.ClearSelection();
            }
        }
        catch (Exception ex)
        {
            Log.WriteLog(Log.TipoLog.LogError, Constantes.NombreAplicacion, "Error en " + this.GetType().FullName + ".cmbGrupoUsuario_SelectedIndexChanged(). Excepcion: " + ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "usuario", "alert('" + Constantes.ErrorGenerico + "'); window.parent.closePopUp();window.parent.refrescar();", true);
        }
    }

    #endregion

    #region Métodos privados

    /// <summary>
    /// Método para cargar los datos de la persona seleccionada
    /// </summary>
    private void CargarDatosPersona()
    {
        if (IdPersona > 0)
        {
            try
            {
                lblTitulo.Text = "DETALLE DEL USUARIO";
                GUAI.Neg.NegPersona guaiNeg = new GUAI.Neg.NegPersona();
                PersonaOrganizacion datosPersona = new PersonaOrganizacion();
                datosPersona = guaiNeg.GetPersonaDividendoDigital(Constantes.IdAplicacion, IdPersona);
                if (datosPersona != null)
                {
                    IdGrupoPerfil = datosPersona.Perfil.IdGrupoPerfil;
                    txtNombre.Text = datosPersona.Persona.Nombre;
                    txtAp1.Text = datosPersona.Persona.Apellido1;
                    txtAp2.Text = datosPersona.Persona.Apellido2;
                    txtEmail.Text = datosPersona.Persona.Email;
                    txtLogin.Text = datosPersona.Persona.Usuario;
                    txtLogin.ReadOnly = true;
                    RFV_Pass.Enabled = false;
                    txtPass.Value = string.Empty;
                    txtObservaciones.Value = datosPersona.Persona.Observaciones;
                    cmbGrupoUsuario.SelectedValue = datosPersona.GrupoUsuario.Codigo + "|" + datosPersona.GrupoUsuario.IdGrupoUsuario.ToString();
                    if (datosPersona.GrupoUsuario.Codigo == Constantes.TipoUsuario.RVS.ToString())
                    {
                        CargarNivelRevisores();
                        RFV_Nivel.Enabled = true;
                        dvNivel.Style.Remove("display");
                        cmbNivel.SelectedValue = datosPersona.Perfil.IdPerfil.ToString(); ;
                    }
                    else if (datosPersona.GrupoUsuario.Codigo == Constantes.TipoUsuario.ADM.ToString())
                    {
                        CargarNivelAdministradores();
                        RFV_Nivel.Enabled = true;
                        dvNivel.Style.Remove("display");
                        cmbNivel.SelectedValue = datosPersona.Perfil.IdPerfil.ToString(); ;
                    }
                    else
                    {
                        RFV_Nivel.Enabled = false;
                        dvNivel.Style.Add("display", "none");
                        cmbNivel.ClearSelection();
                    }
                    if (datosPersona.Perfil.CertificadoObligatorio == GUAI.Recursos.Tipos.EnumSiNo.SI)
                        DesactivarCampos();
                }
                else
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('No se ha encontrado el usuario en el sistema.');window.parent.hideLoading(); window.parent.closePopUp();window.parent.refrescar();", true);
            }
            catch (Exception ex)
            {
                Log.WriteLog(Log.TipoLog.LogError, Constantes.NombreAplicacion, "Error en " + this.GetType().FullName + ".cargarDatosPersona(IdPersona=" + IdPersona.ToString() + "). Excepcion: " + ex.ToString());
                ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('" + Constantes.ErrorGenerico + "');window.parent.hideLoading(); window.parent.closePopUp();window.parent.refrescar();", true);
            }
        }
        else
        {
            lblTitulo.Text = "ALTA DE USUARIO";
            RFV_Pass.Enabled = true;
        }
    }

    /// <summary>
    /// Desactiva todos los campos para que sean de sólo lectura
    /// </summary>
    private void DesactivarCampos()
    {
        txtNombre.ReadOnly = true;
        txtAp1.ReadOnly = true;
        txtAp2.ReadOnly = true;
        txtEmail.ReadOnly = true;
        txtLogin.ReadOnly = true;
        txtPass.Disabled = true;
        cmbGrupoUsuario.Enabled = false;
        cmbNivel.Enabled = false;
        txtObservaciones.Disabled = true;
        btnAlta.Enabled = false;
        spObligatorios.Visible = false;
    }

    /// <summary>
    ///procedimiento que carga el combo de los grupos de usuario
    /// </summary>  
    private void CargarComboGrupoUsuario()
    {
        try
        {
            GUAI.Neg.NegGestionPermisos NegGrupoUsuario = new GUAI.Neg.NegGestionPermisos();
            System.Data.DataTable dtGruposUsuario = NegGrupoUsuario.GetGruposUsuarioDatos(Constantes.IdAplicacion, GUAI.Recursos.Tipos.Constantes.ID_MINISTERIO);
            foreach (System.Data.DataRow row in dtGruposUsuario.Rows)
                row["CODIGO"] = string.Format("{0}|{1}", row["CODIGO"], row["ID_GRUPO_USUARIOS"]);

            if (dtGruposUsuario != null)
            {
                System.Data.DataView dv = dtGruposUsuario.DefaultView;
                dv.Sort = "DESCRIPCION ASC";
                cmbGrupoUsuario.DataSource = dv;
            }
            cmbGrupoUsuario.DataBind();
            cmbGrupoUsuario.Items.Insert(0, new ListItem("--Seleccione el tipo de usuario--", ""));
        }
        catch(Exception ex)
        {
            Log.WriteLog(Log.TipoLog.LogError, Constantes.NombreAplicacion, "Error en " + this.GetType().FullName + ".CargarComboGrupoUsuario(). Excepcion: " + ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('" + Constantes.ErrorGenerico + "');window.parent.hideLoading(); window.parent.closePopUp();window.parent.refrescar();", true);
        }
    }

    /// <summary>
    /// Carga los valores del combo de perfiles correspondiente a la selección del grupo usuario administrador
    /// </summary>
    private void CargarNivelAdministradores()
    {
        cmbNivel.Items.Clear();
        //Carga los niveles disponibles para el administrador
        cmbNivel.Items.Add(new ListItem("--Seleccione el nivel del administrador--", ""));
        cmbNivel.Items.Add(new ListItem("Administrador", "1"));
        cmbNivel.Items.Add(new ListItem("Generador de lotes", "2"));
        cmbNivel.ClearSelection();
    }

    /// <summary>
    /// Carga los valores del combo de perfiles correspondiente a la selección del grupo usuario administrador
    /// </summary>
    private void CargarNivelRevisores()
    {
        cmbNivel.Items.Clear();
        //Carga los niveles disponibles para el revisor
        cmbNivel.Items.Add(new ListItem("--Seleccione el nivel del revisor--", ""));
        cmbNivel.Items.Add(new ListItem("Validador nivel 1", "3"));
        cmbNivel.Items.Add(new ListItem("Validador nivel 2", "2"));
        cmbNivel.Items.Add(new ListItem("Auditor", "1"));
        cmbNivel.ClearSelection();
    }
    #endregion
}
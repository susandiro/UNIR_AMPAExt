//using System;
//using System.Collections.Generic;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using GUAI.Neg;
//using GUAI.Ent.Clases;
//using System.Data;
//using DividendoDigital.Modelo;

public partial class Conexion_GestionUsuarios : PageBase
{
    //#region propiedades

    ///// <summary>
    ///// Propiedad para indicar el filtro utilizado por id del usuario
    ///// </summary>
    //public long? FIdUsuario
    //{
    //    get
    //    {
    //        if (ViewState["IdUsuario"] != null)
    //            return long.Parse(ViewState["IdUsuario"].ToString());
    //        return null;
    //    }
    //    set
    //    {
    //        ViewState["IdUsuario"] = value;
    //    }
    //}

    ///// <summary>
    ///// Propiedad para indicar el filtro utilizado por login
    ///// </summary>
    //public string FLogin
    //{
    //    get
    //    {
    //        if (ViewState["Login"] != null)
    //            return ViewState["Login"].ToString();
    //        return null;
    //    }
    //    set
    //    {
    //        ViewState["Login"] = value;
    //    }
    //}

    ///// <summary>
    ///// Propiedad para indicar el filtro utilizado por tipo de usuario
    ///// </summary>
    //public string FTipoUsuario
    //{
    //    get
    //    {
    //        if (ViewState["TpUsuario"] != null)
    //            return ViewState["TpUsuario"].ToString();
    //        return null;
    //    }
    //    set
    //    {
    //        ViewState["TpUsuario"] = value;
    //    }
    //}

    //#endregion

    //#region Eventos

    ///// <summary>
    ///// Evento generado al cargar la página
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void Page_Load(object sender, EventArgs e)
    //{


    //    if (MasterBase.TpUsuario != Constantes.TipoUsuario.ADM || MasterBase.PerfilUsuario != Constantes.RevPerfil.Nivel3)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, GetType(), "listados", "alert('No tiene permiso para acceder a esta aplicación');", true);
    //        Session.Clear();
    //        Response.Redirect("~/login.aspx", false);
    //        return;
    //    }

    //    System.Web.UI.HtmlControls.HtmlGenericControl script = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
    //    script.Attributes.Add("type", "text/javascript");
    //    script.Attributes.Add("src", MasterBase.RelativeURL + "Scripts/jquery-2.1.1.min.js");
    //    Header.Controls.Add(script);

    //    Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
    //    Response.AddHeader("Pragma", "no-cache");
    //    Response.AddHeader("Expires", "0");
    //    try
    //    {
    //        if (!Page.IsPostBack)
    //        {
    //            this.Form.DefaultButton = this.btnBuscar.UniqueID;
    //            this.Title = "Gestión de usuarios";//Título general
    //            pnlFiltro.DefaultButton = "btnBuscar";//Botón del filtro por defecto

    //            cmbVer.Items.Add(new ListItem("Activos", "0"));
    //            cmbVer.Items.Add(new ListItem("Eliminados", "1"));
    //            cmbVer.Items.Add(new ListItem("Todos", "2"));
    //            cmbVer.SelectedValue = "0";

    //            //Grid cargado por defecto
    //            CargarTipoUsuario();
    //            CargarGrid();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.WriteLog(Log.TipoLog.LogError, Constantes.NombreAplicacion, "Error en " + this.GetType().FullName + ".Page_Load(). Excepcion: " + ex.ToString());
    //        ScriptManager.RegisterStartupScript(Page, GetType(), "gestion", "alert('" + Constantes.ErrorGenerico + "');refrescar();", true);
    //    }
    //}

    ///// <summary>
    ///// Evento lanzado cuando se pulsa el botón filtrar
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnBuscar_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (!string.IsNullOrEmpty(txtIdUsuario.Text))
    //            FIdUsuario = long.Parse(txtIdUsuario.Text);
    //        else
    //            FIdUsuario = null;
    //        FLogin = txtLogin.Text;
    //        FTipoUsuario = cmbGrupoUsuario.SelectedValue;
    //        CargarGrid();
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.WriteLog(Log.TipoLog.LogError, Constantes.NombreAplicacion, "Error en " + this.GetType().FullName + ".btnBuscar_Click(). Excepcion: " + ex.ToString());
    //        ScriptManager.RegisterStartupScript(Page, GetType(), "gestion", "alert('" + Constantes.ErrorGenerico + "');refrescar();", true);
    //        ViewState.Clear();
    //    }
    //}

    ///// <summary>
    ///// Limpia el filtro dejándolo con los valores iniciales
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void limpiar_Click(object sender, EventArgs e)
    //{
    //    txtIdUsuario.Text = string.Empty;
    //    cmbGrupoUsuario.ClearSelection();
    //    txtLogin.Text = string.Empty;
    //    cmbVer.SelectedValue = "0";
    //    ViewState.Clear();
    //    CargarGrid();
    //}

    ///// <summary>
    ///// Refresca la pantalla
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void refrescar(object sender, EventArgs e)
    //{
    //    CargarGrid();
    //}

    ///// <summary>
    ///// Evento lanzado cuando se pulsa en el botón de nuevo usuario
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnNuevo_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string titulo = "Alta de usuario";
    //        string urlVerExpediente = AbsolutePath("Conexion/Modales/Usuario.aspx");
    //        MostrarPopup(urlVerExpediente, "680", "900", titulo);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.WriteLog(Log.TipoLog.LogError, Constantes.NombreAplicacion, "Error en " + this.GetType().FullName + ".btnNuevo_Click(). Error: " + ex.ToString());
    //        ScriptManager.RegisterStartupScript(Page, GetType(), "solicitudes", "alert('" + Constantes.ErrorGenerico + "');refrescar();", true);
    //    }
    //}

    //#endregion

    //#region Eventos del grid

    ///// <summary>
    ///// Evento que se produce al vincular los datos con el grid
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void gvResultados_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        string certificado = gvResultados.DataKeys[e.Row.RowIndex].Values[1].ToString();
    //        if (certificado == GUAI.Recursos.Tipos.EnumSiNo.SI.ToString())
    //        {
    //            e.Row.BackColor = System.Drawing.Color.FromName("#FAC6B8");
    //            e.Row.Attributes.Add("onmouseover", "this.style.background='#FEA59C'");
    //            e.Row.Attributes.Add("onmouseout", "this.style.background='#FAC6B8'");
    //            ImageButton Borrar = (ImageButton)e.Row.Cells[2].FindControl("imgBorrar");
    //            Borrar.Visible = false;
    //        }
    //        else
    //        {
    //            e.Row.Attributes.Add("onmouseover", "this.style.background='#FFF0A2'");
    //            e.Row.Attributes.Add("onmouseout", "this.style.background='#FFFFFF'");
    //        }
    //    }
    //}

    ///// <summary>
    ///// Evento producido al realizar alguna acción sobre un registro del grid. Se podrá ver/modificar al usuario o eliminarlo
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void gvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (gvResultados.Rows.Count > 0)
    //    {
    //        try
    //        {
    //            //Recuperamos el identificador de la persona
    //            long idPersona = long.Parse(e.CommandArgument.ToString());
    //            switch (e.CommandName)
    //            {
    //                case "Detalle":
    //                    //Recuperamos el identificador de la persona
    //                    string titulo = "Detalle del usuario " + idPersona.ToString();
    //                    string urlVerExpediente = AbsolutePath("Conexion/Modales/Usuario.aspx?idPer=" + idPersona.ToString());
    //                    MostrarPopup(urlVerExpediente, "680", "900", titulo);
    //                    break;

    //                case "Eliminar":
    //                    try
    //                    {
    //                        if (idPersona == MasterBase.IdUsuario)
    //                        {
    //                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", "alert('El usuario no puede borrarse a sí mismo.');", true);
    //                            break;
    //                        }
    //                        else
    //                        {
    //                            //Borramos de forma lógica al usuario: se le pone que necesita certificado obligatorio y su contraseña a vacía
    //                            List<long> lstAplicaciones = new List<long>();
    //                            lstAplicaciones.Add(Constantes.IdAplicacion);

    //                            List<PersonaOrganizacion> lstResultado = new List<PersonaOrganizacion>();

    //                            NegGestionPermisos guaiNeg = new NegGestionPermisos();
    //                            lstResultado = guaiNeg.GetPersonaOrganizaciones(idPersona, GUAI.Recursos.Tipos.Constantes.ID_MINISTERIO, lstAplicaciones);
    //                            if (lstResultado != null || lstResultado.Count > 0)
    //                            {
    //                                guaiNeg.BorrarGrupoUsuarioPersona(idPersona, GUAI.Recursos.Tipos.Constantes.ID_MINISTERIO, lstResultado[0].GrupoUsuario.IdGrupoUsuario);
    //                                lstResultado[0].Perfil.CertificadoObligatorio = GUAI.Recursos.Tipos.EnumSiNo.SI;
    //                                guaiNeg.AltaPersonaOrganizacion(idPersona, lstResultado);
    //                            }
    //                            else
    //                                throw new Exception("No se ha podido eliminar el usuario " + idPersona.ToString() + ". No se ha encontrado en el sistema");
    //                        }
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        Log.WriteLog(Log.TipoLog.LogError, Constantes.NombreAplicacion, "Error en " + this.GetType().FullName + ".gvResultados_RowCommand().Eliminar(IdPersona: " + idPersona.ToString() + "). Descripción: " + ex.ToString());
    //                        ScriptManager.RegisterStartupScript(Page, GetType(), "solicitudes", "alert('" + Constantes.ErrorGenerico + "');refrescar();", true);
    //                        return;
    //                    }
    //                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", "alert('El usuario se ha eliminado correctamente.');", true);
    //                    //Cargamos la grid de nuevo para que desaparezca el usuario borrado
    //                    CargarGrid();
    //                    break;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Log.WriteLog(Log.TipoLog.LogError, Constantes.NombreAplicacion, "Error en " + this.GetType().FullName + ".gvResultados_RowCommand(). Error: " + ex.ToString());
    //            ScriptManager.RegisterStartupScript(Page, GetType(), "solicitudes", "alert('" + Constantes.ErrorGenerico + "');refrescar();", true);
    //        }
    //    }
    //}

    //#endregion

    //#region métodos privados

    ///// <summary>
    ///// Carga de valores de los usuarios/personas
    ///// </summary>
    ///// <param name="filtro">Clase que obtiene los parámetros de filtrado</param>
    //private void CargarGrid()
    //{
    //    NegPersona PersonaNeg = new NegPersona();
    //    DataTable dtPersonas = new DataTable();
    //    try
    //    {
    //        dtPersonas = PersonaNeg.GetPersonasDividendoDigital(Constantes.IdAplicacion, FIdUsuario, FLogin, FTipoUsuario);
    //        if (dtPersonas != null && dtPersonas.Rows.Count > 0)
    //        {
    //            DataTable dtResultado = dtPersonas.Clone();
    //            DataRow[] resultado = dtPersonas.Select();
    //            switch (cmbVer.SelectedValue)
    //            {
    //                case "0":
    //                    resultado = dtPersonas.Select("Certificado = 'NO'");
    //                    break;
    //                case "1":
    //                    resultado = dtPersonas.Select("Certificado = 'SI'");
    //                    break;
    //                default:
    //                    break;
    //            }
    //            foreach (DataRow dr in resultado)
    //                dtResultado.ImportRow(dr);
    //            gvResultados.DataSource = dtResultado;
    //        }
    //        else
    //            gvResultados.DataSource = null;
    //        gvResultados.DataBind();

    //    }
    //    catch (Exception ex)
    //    {
    //        Log.WriteLog(Log.TipoLog.LogError, Constantes.NombreAplicacion, "Error en " + this.GetType().FullName + ".CargarGrid(). Excepcion: " + ex.ToString());
    //        throw;
    //    }
    //}

    ///// <summary>
    ///// Muestra la página como modal.
    ///// </summary>
    ///// <param name="urlpagina">Url de la página a mostrar.</param>
    ///// <param name="alto">Alto de la página.</param>
    ///// <param name="ancho">Ancho de la página.</param>
    ///// <param name="titulo">Titulo de la página.</param>
    //private void MostrarPopup(string urlpagina, string alto, string ancho, string titulo)
    //{
    //    System.Web.UI.HtmlControls.HtmlGenericControl fPopUp = this.framePopUp as System.Web.UI.HtmlControls.HtmlGenericControl;
    //    fPopUp.Attributes.Add("src", urlpagina);
    //    fPopUp.Attributes.Add("height", alto);
    //    fPopUp.Attributes.Add("width", ancho);
    //    txtTituloPopUp.Text = titulo;
    //    mpGestionar.PopupDragHandleControlID = "pnlDrag";
    //    mpGestionar.Show();
    //}

    ///// <summary>
    ///// Recupera la ruta absoluta
    ///// </summary>
    ///// <param name="file"></param>
    ///// <returns></returns>
    //private string AbsolutePath(String file)
    //{
    //    String end = (Request.ApplicationPath.EndsWith("/")) ? "" : "/";
    //    String path = Request.ApplicationPath + end;
    //    return String.Format("https://{0}{1}{2}", Request.Url.Authority, path, file);
    //}

    ///// <summary>
    ///// Carga el combo del tipo de usuario del panel de filtros
    ///// </summary>
    //private void CargarTipoUsuario()
    //{
    //    try
    //    {
    //        GUAI.Neg.NegGestionPermisos NegGrupoUsuario = new GUAI.Neg.NegGestionPermisos();
    //        DataTable dtGruposUsuario = NegGrupoUsuario.GetGruposUsuarioDatos(Constantes.IdAplicacion, GUAI.Recursos.Tipos.Constantes.ID_MINISTERIO);

    //        if (dtGruposUsuario != null)
    //        {
    //            System.Data.DataView dv = dtGruposUsuario.DefaultView;
    //            dv.Sort = "DESCRIPCION ASC";
    //            cmbGrupoUsuario.DataSource = dv;
    //        }
    //        cmbGrupoUsuario.DataBind();
    //        cmbGrupoUsuario.Items.Insert(0, new ListItem("-- Todos --", ""));
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.WriteLog(Log.TipoLog.LogError, Constantes.NombreAplicacion, "Error en " + this.GetType().FullName + ".CargarTipoUsuario(). Excepcion: " + ex.ToString());
    //        ScriptManager.RegisterStartupScript(Page, GetType(), "Usuario", "alert('" + Constantes.ErrorGenerico + "');refrescar();", true);
    //    }
    //}

    //#endregion
}
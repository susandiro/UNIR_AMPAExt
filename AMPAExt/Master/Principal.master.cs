using System;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

public partial class Masters_principal : MasterPageBase
{
    //#region Propiedades

    ///// <summary>
    ///// Propiedad para conocer la opción de menú en la que nos encontramos. La opción la deja desplegada y colapsa el resto. En la página de index se pone esta variable de sesión a vacía
    ///// </summary>
    //public string OpcionMenu
    //{
    //    get
    //    {
    //        if (Session["Menu"] != null && !string.IsNullOrEmpty(Session["Menu"].ToString()))
    //            return Session["Menu"].ToString();
    //        return string.Empty;
    //    }
    //    set
    //    {
    //        Session["Menu"] = value;
    //    }
    //}

    //  #endregion

    //#region Eventos

    ///// <summary>
    ///// Evento generado antes de cargar la página
    ///// </summary>
    ///// <param name="e"></param>
    //protected override void OnInit(EventArgs e)
    //{
    //    if (!Page.IsPostBack)
    //    {
    //        bool borrarFiltroGestores = string.IsNullOrEmpty(Request["grid"]) || Request["grid"] == "N"; //Si no tenemos el queryString o su valor es N, borramos el filtro de sesión
    //        if (borrarFiltroGestores)
    //        {
    //            Session.Remove(TipoDatos.FiltroGestorTareasProductores);
    //            Session.Remove(TipoDatos.FiltroGestorTareasSCRAP);
    //            Session.Remove(TipoDatos.FiltroGestorTareasGestion);
    //            Session.Remove(TipoDatos.FiltroGestorTareasComunicaciones);
    //            Session.Remove(TipoDatos.FiltroAplicacion);
    //            Session.Remove("pestanaActivaGestor");
    //            Session.Remove("IdCategoria");
    //            Session.Remove("IdSubcategoria");
    //        }
            
    //    }
    //    base.OnInit(e);
    //}

    ///// <summary>
    ///// Evento generado cuando se carga la página maestra. Se define si se va a ver los distintos elementos en la página     
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected new void Page_Load(object sender, EventArgs e)
    //{
    //    this.Page.Title = this.Page.Title.Replace("RII_AEE", TipoDatos.NombreAplicacion);
    //    masterTitle.Text = RAEE.Comun.TipoDatos.NombreAplicacion; 
    //    if (!Page.IsPostBack)
    //    {
    //        aOficinaVirtual.HRef = "mailto:" + ConfigurationManager.AppSettings["EmailIncidencias"];
    //        aOficinaVirtual.InnerText = ConfigurationManager.AppSettings["EmailIncidencias"];
    //        base.Page_Load(sender, e);
    //        header.Visible = VerCabecera;
    //        dvBotonera.Visible = VerBotonera;
    //        dvBotoneraVacia.Visible = !VerBotonera;            
    //        dvMenu.Visible = VerMenu;
    //        dvRastroMigas.Visible = VerMigas;

    //        //creaLink(rutaEstilos + "/Style Library/SedeElectronica.css");
    //        //creaLink(rutaEstilos + "/Style Library/SedeElectronica_print.css", true);

    //        if (DatosSesionLogin != null && DatosSesionLogin.Perfil != null)
    //        {
    //            usuarioLogado.Text = DatosSesionLogin.Perfil.Persona.NombreCompleto.ToUpper() + " (" + DatosSesionLogin.Perfil.Organizacion.Nombre.ToUpper() + ")";
    //            divPlaceHolder.Attributes["class"] = "center_left anchoCompleto";
    //        }
    //        else
    //            divPlaceHolder.Attributes["class"] = "center_completo anchoCompleto";

    //        CargarUrlManuales();
    //        Documentacion.Visible = VerAyuda;

    //        if (!VerMenu)
    //        {
    //            columnaMenu.Visible = false;
    //            divPlaceHolder.Attributes["class"] = "center_completo anchoCompleto";
    //            divPlaceHolder.Attributes.Add("style", "background-image:none");
    //        }
    //        GetVersion();
    //    }
    //    if (TipoDatos.EntornoOrigen == TipoDatos.Entorno.Intranet)
    //    {
    //        divAccesibilidad.Visible = false;
    //    }
    //}

    ///// <summary>
    ///// Evento generado cuando el usuario pulsa sobre el botón de salir. Se limpia la sesión y se vuelve a la página de login
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnLogout_Click(object sender, EventArgs e)
    //{
    //    Session.Clear();
    //    base.DatosSesionLogin = null;
    //    SesionConexion = null;
    //}

    ///// <summary>
    ///// Fija la opción de menú que se haya desplegado
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnCargarOpcion_Click(object sender, EventArgs e)
    //{
    //    OpcionMenu = menuActivo.Value;
    //}

    ///// <summary>
    ///// Evento que se desencadena al pulsar desconectar
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void Desconectar(object sender, EventArgs e)
    //{
    //    if (DatosSesionLogin.AccesoClave)
    //    {
    //        System.Collections.Specialized.NameValueCollection valores = new System.Collections.Specialized.NameValueCollection();
    //        valores.Add("PSC", ConfigurationManager.AppSettings["aplicacion"]);
    //        valores.Add("SIA", (long.Parse(ConfigurationManager.AppSettings["SIA"]) > 0) ? ConfigurationManager.AppSettings["SIA"] : string.Empty);
    //        valores.Add("MOD", (long.Parse(ConfigurationManager.AppSettings["MOD"]) > 0) ? ConfigurationManager.AppSettings["MOD"] : string.Empty);

    //        //Prepare the Posting form
    //        string strForm = PrepararFormPost(ConfigurationManager.AppSettings["ProxyClaveUrlLogout"], valores);

    //        //Add a literal control the specified page holding 
    //        //the Post Form, this is to submit the Posting form with the request.
    //        Page.Controls.Add(new LiteralControl(strForm));
    //    }
    //    else
    //        Response.Redirect("~/Login.aspx", false);
    //}

    //#endregion

    //#region Métodos

    ///// <summary>
    ///// Cambia el pdf al que apunta el icono de documentación dependiendo del entorno y perfil en el que te encuentres
    ///// </summary>
    //private void CargarUrlManuales()
    //{
    //    //SI ESTAMOS LOGADOS
    //    if (DatosSesionLogin != null && DatosSesionLogin.Perfil != null)
    //    {
    //        //INTRANET
    //        if (TipoDatos.EntornoOrigen == TipoDatos.Entorno.Intranet)
    //        {
    //            manualDocumentacion.NavigateUrl = "~/Content/Manuales/Manual Usuario " + RAEE.Comun.TipoDatos.NombreAplicacion + ". Gestión Intranet.pdf";
    //        }
    //        //EXTRANET
    //        else
    //        {
    //            //SCRAP
    //            if (DatosSesionLogin.Perfil.GrupoUsuario.Codigo == TipoDatos.GrupoUsuario.SCR.ToString())
    //            {
    //                manualDocumentacion.NavigateUrl = "~/Content/Manuales/Manual Usuario " + RAEE.Comun.TipoDatos.NombreAplicacion + ". Gestión SCRAP.pdf";
    //            }
    //            //PRODUCTOR
    //            else if (DatosSesionLogin.Perfil.GrupoUsuario.Codigo == TipoDatos.GrupoUsuario.PRD.ToString())
    //            {
    //                manualDocumentacion.NavigateUrl = "~/Content/Manuales/Manual Usuario " + RAEE.Comun.TipoDatos.NombreAplicacion + ". Gestión Productor.pdf";
    //            }
    //            //CC.AA.
    //            else
    //            {
    //                manualDocumentacion.NavigateUrl = "~/Content/Manuales/Manual Usuario " + RAEE.Comun.TipoDatos.NombreAplicacion + ". Comunidades Autónomas.pdf";
    //            }
    //        }
    //    }
    //    //ALTA PRODUCTOR SIN HABER HECHO LOGIN
    //    else if (HttpContext.Current.Request.Url.AbsoluteUri.ToUpper().Contains("/UI/PRODUCTORES/ALTAPRODUCTOR.ASPX"))
    //    {
    //        manualDocumentacionSinLogin.NavigateUrl = "~/Content/Manuales/Manual Usuario " + RAEE.Comun.TipoDatos.NombreAplicacion + ". Solicitud inscripción productor.pdf";
    //    }
    //    //ALTA SCRAP SIN HABER HECHO LOGIN
    //    else if (HttpContext.Current.Request.Url.AbsoluteUri.ToUpper().Contains("/UI/SCRAP/ALTASCRAP.ASPX"))
    //    {
    //        manualDocumentacionSinLogin.NavigateUrl = "~/Content/Manuales/Manual Usuario " + RAEE.Comun.TipoDatos.NombreAplicacion + ". Solicitud inscripción SCRAP.pdf";
    //    }
    //    if (string.IsNullOrEmpty(manualDocumentacionSinLogin.NavigateUrl))
    //        VerAyuda = false;

    //}

    ///// <summary>
    ///// Evento generado cuando el usuario pulsa sobre el botón de salir. Se limpia la sesión y se vuelve a la página de login
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void GetVersion()
    //{
    //    //Añadimos el número de versión
    //    XmlDocument xDoc = new XmlDocument();
    //    string end = (Request.PhysicalApplicationPath.EndsWith("/")) ? "" : "/";
    //    string path = Request.PhysicalApplicationPath + end;
    //    xDoc.Load(path + "version.xml");
    //    XmlNodeList numeroVersion = xDoc.GetElementsByTagName("numeroVersion");
    //    XmlNodeList fechaUltimaModificacion = xDoc.GetElementsByTagName("fechaUltimaModificacion");
    //    lbCodeVersion.Text = numeroVersion[0].InnerText;
    //    lbFechaAct.Text = fechaUltimaModificacion[0].InnerText;
    //}

    //protected void creaLink(string sRuta, bool bPrint = false)
    //{
    //    try
    //    {
    //        HtmlLink link = new HtmlLink();
    //        link.Attributes.Add("href", Page.ResolveClientUrl(sRuta));
    //        link.Attributes.Add("Type", "text/css");
    //        link.Attributes.Add("rel", "stylesheet");
    //        if (bPrint)
    //            link.Attributes.Add("media", "print");
    //        Page.Header.Controls.Add(link);
    //    }
    //    catch (Exception ex)
    //    {
    //        MasterPageBase.TrazaLog.Error("Ha ocurrido un error al cargar de los datos de la consulta", ex);
    //    }

    //}

    ///// <summary>
    ///// Prepara el formulario para el post de cl@ve
    ///// </summary>
    ///// <param name="url"></param>
    ///// <param name="data"></param>
    ///// <returns></returns>
    //private string PrepararFormPost(string url, System.Collections.Specialized.NameValueCollection data)
    //{
    //    //Nombre del formulario
    //    string formID = "PostForm";
    //    //Build the form using the specified data to be posted.
    //    StringBuilder strForm = new StringBuilder();
    //    strForm.Append("<form id=\"" + formID + "\" name=\"" +
    //                   formID + "\" action=\"" + url +
    //                   "\" method=\"POST\">");

    //    foreach (string key in data)
    //        strForm.Append("<input type=\"hidden\" name=\"" + key +
    //           "\" value=\"" + data[key] + "\">");

    //    strForm.Append("</form>");
    //    //Build the JavaScript which will do the Posting operation.
    //    StringBuilder strScript = new StringBuilder();
    //    strScript.Append("<script language='javascript'>");
    //    strScript.Append("var v" + formID + " = document." +
    //                     formID + ";");
    //    strScript.Append("v" + formID + ".submit();");
    //    strScript.Append("</script>");
    //    //Return the form and the script concatenated.
    //    //(The order is important, Form then JavaScript)
    //    return strForm.ToString() + strScript.ToString();
    //}

    //#endregion

    //#region Gestores

    ///// <summary>
    ///// Devuelve el número total de solicitudes para el gestor de tareas
    ///// </summary>
    ///// <param name="node"></param>
    ///// <param name="administracion"></param>
    ///// <returns></returns>
    //public string GetNumPendientes(bool gestorTareas)
    //{
    //    string pendientes = string.Empty;

    //    if (gestorTareas)
    //    {
    //        GestorTareasCls contadorSolicitudes = new GestorTareasCls();

    //        if (Session["contadores"] == null)
    //        {
    //            RAEE.Neg.SolicitudNeg negocio = new RAEE.Neg.SolicitudNeg();
    //            contadorSolicitudes = new GestorTareasCls();
    //            contadorSolicitudes = negocio.ContadorSolicitudes(RAEE.Comun.TipoDatos.Aplicacion);
    //            Session["contadores"] = contadorSolicitudes;
    //        }
    //        else
    //        {
    //            contadorSolicitudes = (GestorTareasCls)Session["contadores"];
    //        }

    //        if (contadorSolicitudes.Total > 0)
    //        {
    //            int numPendientes = contadorSolicitudes.Total;
    //            pendientes = " <span class='gestorTareas'>(" + numPendientes + ")</span>";
    //        }
    //    }

    //    return pendientes;
    //}

    //#endregion

}



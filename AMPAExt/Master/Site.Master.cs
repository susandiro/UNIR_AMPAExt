using System;
using System.Web.UI;

public partial class Masters_Site : MasterPageBase
{
    #region Propiedades

    /// <summary>
    /// Propiedad para conocer la opción de menú en la que nos encontramos. La opción la deja desplegada y colapsa el resto. En la página de index se pone esta variable de sesión a vacía
    /// </summary>
    public string OpcionMenu
    {
        get
        {
            if (Session["Menu"] != null && !string.IsNullOrEmpty(Session["Menu"].ToString()))
                return Session["Menu"].ToString();
            return string.Empty;
        }
        set
        {
            Session["Menu"] = value;
        }
    }

    #endregion

    #region Eventos

    /// <summary>
    /// Evento generado antes de cargar la página
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    /// <summary>
    /// Evento generado cuando se carga la página maestra. Se define si se va a ver los distintos elementos en la página     
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected new void Page_Load(object sender, EventArgs e) 
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            menuCabecera.Visible = VerCabecera;
            menuHorizontal.Visible = VerBotonera;
            //Poner el nombre del usuario en la cabacera
            if (DatosSesionLogin != null && !string.IsNullOrEmpty(DatosSesionLogin.Empresa))
            {
                datosUsuario.Visible = true;
                datosUsuario.InnerText = DatosSesionLogin.NombreCompleto.ToUpper() + " (" + DatosSesionLogin.Empresa.ToUpper() + ")";
                lmenuAdministracion.Visible = (DatosSesionLogin.CodTipoUsuario == AMPAExt.Comun.TipoDatos.TipoUsuario.AMPA);
            }
            else
                datosUsuario.Visible = false;
        }
    }

    /// <summary>
    /// Evento generado cuando el usuario pulsa sobre el botón de salir. Se limpia la sesión y se vuelve a la página de login
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        base.DatosSesionLogin = null;
        SesionConexion = null;
    }

    /// <summary>
    /// Fija la opción de menú que se haya desplegado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCargarOpcion_Click(object sender, EventArgs e)
    {
     //   OpcionMenu = menuActivo.Value;
    }

    /// <summary>
    /// Evento que se desencadena al pulsar desconectar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Desconectar(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/Login.aspx", false);
    }

    #endregion

    #region Métodos

    /// <summary>
    /// Cambia el pdf al que apunta el icono de documentación dependiendo del entorno y perfil en el que te encuentres
    /// </summary>
    private void CargarUrlManuales()
    {
        ////SI ESTAMOS LOGADOS
        //if (DatosSesionLogin != null && DatosSesionLogin.Perfil != null)
        //{
        //    //INTRANET
        //    if (TipoDatos.EntornoOrigen == TipoDatos.Entorno.Intranet)
        //    {
        //        manualDocumentacion.NavigateUrl = "~/Content/Manuales/Manual Usuario " + RAEE.Comun.TipoDatos.NombreAplicacion + ". Gestión Intranet.pdf";
        //    }
        //    //EXTRANET
        //    else
        //    {
        //        //SCRAP
        //        if (DatosSesionLogin.Perfil.GrupoUsuario.Codigo == TipoDatos.GrupoUsuario.SCR.ToString())
        //        {
        //            manualDocumentacion.NavigateUrl = "~/Content/Manuales/Manual Usuario " + RAEE.Comun.TipoDatos.NombreAplicacion + ". Gestión SCRAP.pdf";
        //        }
        //        //PRODUCTOR
        //        else if (DatosSesionLogin.Perfil.GrupoUsuario.Codigo == TipoDatos.GrupoUsuario.PRD.ToString())
        //        {
        //            manualDocumentacion.NavigateUrl = "~/Content/Manuales/Manual Usuario " + RAEE.Comun.TipoDatos.NombreAplicacion + ". Gestión Productor.pdf";
        //        }
        //        //CC.AA.
        //        else
        //        {
        //            manualDocumentacion.NavigateUrl = "~/Content/Manuales/Manual Usuario " + RAEE.Comun.TipoDatos.NombreAplicacion + ". Comunidades Autónomas.pdf";
        //        }
        //    }
        //}
        ////ALTA PRODUCTOR SIN HABER HECHO LOGIN
        //else if (HttpContext.Current.Request.Url.AbsoluteUri.ToUpper().Contains("/UI/PRODUCTORES/ALTAPRODUCTOR.ASPX"))
        //{
        //    manualDocumentacionSinLogin.NavigateUrl = "~/Content/Manuales/Manual Usuario " + RAEE.Comun.TipoDatos.NombreAplicacion + ". Solicitud inscripción productor.pdf";
        //}
        ////ALTA SCRAP SIN HABER HECHO LOGIN
        //else if (HttpContext.Current.Request.Url.AbsoluteUri.ToUpper().Contains("/UI/SCRAP/ALTASCRAP.ASPX"))
        //{
        //    manualDocumentacionSinLogin.NavigateUrl = "~/Content/Manuales/Manual Usuario " + RAEE.Comun.TipoDatos.NombreAplicacion + ". Solicitud inscripción SCRAP.pdf";
        //}
        //if (string.IsNullOrEmpty(manualDocumentacionSinLogin.NavigateUrl))
        //    VerAyuda = false;

    }
        
    #endregion

    #region Gestores

    /// <summary>
    /// Devuelve el número total de solicitudes para el gestor de tareas
    /// </summary>
    /// <param name="node"></param>
    /// <param name="administracion"></param>
    /// <returns></returns>
    public string GetNumPendientes(bool gestorTareas)
    {
        string pendientes = string.Empty;

        if (gestorTareas)
        {
            //GestorTareasCls contadorSolicitudes = new GestorTareasCls();

            //if (Session["contadores"] == null)
            //{
            //    RAEE.Neg.SolicitudNeg negocio = new RAEE.Neg.SolicitudNeg();
            //    contadorSolicitudes = new GestorTareasCls();
            //    contadorSolicitudes = negocio.ContadorSolicitudes(RAEE.Comun.TipoDatos.Aplicacion);
            //    Session["contadores"] = contadorSolicitudes;
            //}
            //else
            //{
            //    contadorSolicitudes = (GestorTareasCls)Session["contadores"];
            //}

            //if (contadorSolicitudes.Total > 0)
            //{
            //    int numPendientes = contadorSolicitudes.Total;
            //    pendientes = " <span class='gestorTareas'>(" + numPendientes + ")</span>";
            //}
        }

        return pendientes;
    }

    #endregion
}
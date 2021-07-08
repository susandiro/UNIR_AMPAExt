using System;
using AMPAExt.Comun;

/// <summary>
/// MasterPage del resto de páginas
/// </summary>
public class MasterPageBase : System.Web.UI.MasterPage
{
    #region atributos
    /// <summary>
    /// Utilidad para generar trazas de log en la aplicación
    /// </summary>
   // public static readonly ILog TrazaLog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region propiedades privadas
    /// <summary>
    /// true si se quiere ver el rastro de migas; false si se quiere ocultar
    /// </summary>
    private bool _verMigas = true;
    /// <summary>
    /// true si se quiere ver la botonera de la parte inferior de la cabecera; false si se quiere ocultar
    /// </summary>
    private bool _verBotonera = true;
    /// <summary>
    /// true si se quiere ver el menú de opciones; false si se quiere ocultar
    /// </summary>
    private bool _verMenu = true;
    /// <summary>
    /// true si se quiere ver la cabecera; false si se quiere ocultar
    /// </summary>
    private bool _verCabecera = true;
    /// <summary>
    /// true si se quiere chequear que el usuario está logado
    /// </summary>
    private bool _checkSesion = true;
    /// <summary>
    /// Ruta relativa de la aplicación
    /// </summary>
    private string _relativeURL;
    /// Ver botón ayuda
    /// </summary>    
    private bool _verAyuda = true;

    /// <summary>
    /// Indica si la aplicación está en intranet o extranet
    /// </summary>
    private string _entornoAcceso = string.Empty;
    #endregion

    #region propiedades públicas

    /// <summary>
    /// Indica si debemos comprobar la existencia de una sesión
    /// </summary>
    public bool CheckSesion
    {
        get { return _checkSesion; }
        set { _checkSesion = value; }
    }
   
    /// <summary>
    /// true si se quiere ver la cabecera; false si se quiere ocultar. En el caso de ocultarla, también se ocultará la botonera
    /// </summary>
    public bool VerCabecera
    {
        get { return _verCabecera; }
        set
        {
            _verCabecera = value;
            if (!value)
                _verBotonera = false;
        }
    }
    /// <summary>
    /// true si se quiere ver el rastro de migas; false si se quiere ocultar
    /// </summary>
    public bool VerMigas
    {
        get { return _verMigas; }
        set { _verMigas = value; }
    }
    /// <summary>
    /// true si se quiere ver la botonera de la parte inferior de la cabecera; false si se quiere ocultar
    /// </summary>
    public bool VerBotonera
    {
        get { return _verBotonera; }
        set { _verBotonera = value; }
    }
    /// <summary>
    /// true si se quiere ver el menú de opciones de la izquierda; false si se quiere ocultar
    /// </summary>
    public bool VerMenu
    {
        get { return _verMenu; }
        set { _verMenu = value; }
    }
    /// <summary>
    /// true si se quiere ver el boton de ayuda
    /// </summary>
    public bool VerAyuda
    {
        get { return _verAyuda; }
        set { _verAyuda = value; }
    }
  
    /// <summary>
    /// Obtiene la ruta relativa del aspx para su uso en URLs
    /// </summary>
    public string RelativeURL
    {
        get { return _relativeURL; }
        set { _relativeURL = value; }
    }
  
    public UsuarioConexion DatosSesionLogin
    {
        get
        {
            if (Session["datosSesionLogin"] != null)
                return (UsuarioConexion)Session["datosSesionLogin"];
            return null;
        }
        set { Session["datosSesionLogin"] = value; }
    }


    #endregion

    #region eventos
    /// <summary>
    /// Sobreescribimos este método para tener siempre disponible la ruta relativa de la página
    /// </summary>
    /// <param name="e">Argumentos del evento</param>
    protected override void OnInit(EventArgs e)
    {
        // No queremos cachear las páginas en el cliente
        Response.Cache.SetNoStore();

        // Obtenemos la ruta relativa
        _relativeURL = RelativePath();
        if (DatosSesionLogin == null || string.IsNullOrEmpty(DatosSesionLogin.Empresa))
        {
            if (_checkSesion)
                Response.Redirect(_relativeURL + "~/ui/SesionCaducada.aspx");
            else
                DatosSesionLogin = new UsuarioConexion();
        }
        base.OnInit(e);
    }

    /// <summary>
    /// Metodo que se ejecuta al cargar la página
    /// </summary>
    /// <param name="sender">Quien envía</param>
    /// <param name="e">Los argumentos</param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion

    #region métodos

    /// <summary>
    /// Objeto de sesión donde vamos a guardar todos los datos relevantes de la misma
    /// </summary>
    public UsuarioConexion SesionConexion
    {
        get { return Session["SesionConexion"] as UsuarioConexion; }
        set { Session["SesionConexion"] = value; }
    }

    /// <summary>
    /// Obtiene la ruta relativa de la aplicación
    /// </summary>
    /// <returns>Cadena con la ruta relativa</returns>
    protected string RelativePath()
    {
        string iProf = string.Empty;
        int iFSlash = Request.ServerVariables["SCRIPT_NAME"].IndexOf('/');

        for (int iCS = 0; iCS < Request.ServerVariables["SCRIPT_NAME"].Length; iCS++)
            if (Request.ServerVariables["SCRIPT_NAME"][iCS].Equals('/') && iFSlash != iCS)
                iProf += "../";

        return iProf.Substring(0, (iProf.Length >= 3) ? iProf.Length - 3 : iProf.Length);
    }

    #endregion
}
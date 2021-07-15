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
        log4net.Config.XmlConfigurator.Configure();
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
            menuUsuario.Visible = (DatosSesionLogin.CodTipoUsuario == AMPAExt.Comun.TipoDatos.TipoUsuario.AMPA);
            menuSocios.Visible = (DatosSesionLogin.CodTipoUsuario == AMPAExt.Comun.TipoDatos.TipoUsuario.AMPA);
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

    #endregion
}
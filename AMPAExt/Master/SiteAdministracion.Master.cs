using System;
using System.Web.UI;

public partial class Masters_SiteAdministracion : MasterPageBase
{
     #region Eventos

    /// <summary>
    /// Evento generado antes de cargar la página
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        log4net.Config.XmlConfigurator.Configure();
        if (!Page.IsPostBack)
        {
            bool borrarFiltro = string.IsNullOrEmpty(Request["grid"]) || Request["grid"] == "N"; //Si no tenemos el queryString o su valor es N, borramos el filtro de sesión
            if (borrarFiltro)
            {
                Session.Remove("FiltroUsuario");
                Session.Remove("IdUsuario");
            }

        }
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
            menuUsuario.Visible = (DatosSesionLogin.CodTipoUsuario == AMPAExt.Comun.TipoDatos.TipoUsuario.AMPA);
            menuSocios.Visible = (DatosSesionLogin.CodTipoUsuario == AMPAExt.Comun.TipoDatos.TipoUsuario.AMPA);
            //Se establecen los menús a los que se tiene acceso. si es de empresa, no puede entrar
            if (DatosSesionLogin.CodTipoUsuario != AMPAExt.Comun.TipoDatos.TipoUsuario.AMPA)
            {
                Session.Clear();
                Response.Redirect(RelativeURL + "Login.aspx", false);
            }

            //Poner el nombre del usuario en la cabacera
            if (DatosSesionLogin != null)
            {
                datosUsuario.Visible = true;
                datosUsuario.InnerText = DatosSesionLogin.NombreCompleto.ToUpper() + " (" + DatosSesionLogin.Empresa.ToUpper() + ")";
            }
            else
                datosUsuario.Visible = false;
        }

    }
    #endregion
}
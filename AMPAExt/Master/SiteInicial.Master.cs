using System;
using System.Web.UI;
using AMPAExt.Comun;

public partial class Masters_SiteInicial : MasterPageBase
{
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
        }

    }

    /// <summary>
    /// Muestra en pantalla el tipo de mensaje que se desee dar al usuario
    /// </summary>
    /// <param name="tipo">Tipo de mensaje a mostrar</param>
    /// <param name="mensaje">Mensaje que se le va a dar</param>
    public void MostrarMensaje(TipoDatos.TipoError tipo, string mensaje)
    {
        switch (tipo)
        {
            case TipoDatos.TipoError.Error:
                alertError.Visible = true;
                lbError.Text = mensaje;
                break;
            case TipoDatos.TipoError.Warning:
                alertWarning.Visible = true;
                lbWarning.Text = mensaje;
                break;
            case TipoDatos.TipoError.Success:
                alertSuccess.Visible = true;
                lbSuccess.Text = mensaje;
                break;
            case TipoDatos.TipoError.Limpiar:
            default:
                alertSuccess.Visible = false;
                alertWarning.Visible = false;
                alertError.Visible = false;
                break;
        }
    }

    /// <summary>
    /// Evento que se desencadena al pulsar desconectar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Desconectar(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/UI/Login.aspx", false);
    }

    #endregion
}
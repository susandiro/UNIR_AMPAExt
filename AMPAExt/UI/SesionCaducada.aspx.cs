using System;

public partial class SesionCaducada : PageBase
{
    /// <summary>
    /// Antes de que se carge la página se define que se limpie la sesión y que no queriere sesión para ver la página
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreInit(EventArgs e)
    {
        Session.Clear();
        MasterBase.CheckSesion = false;
    }

    /// <summary>
    /// Evento cuando se carga la página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MasterBase.VerBotonera = false;
            MasterBase.VerCabecera = false;
            MasterBase.VerMenu = false;
        }
    }
}
using System;
using System.Collections.Generic;
using AMPA.Modelo;
using AMPAExt.Comun;
using System.Data;
public partial class Login : PageBase
{
    /// <summary>
    /// Antes de que se carge la página se define que no se compruebe los si necesita estar logado
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreInit(EventArgs e)
    {
        MasterBase.CheckSesion = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        MasterBase.VerBotonera = false;
        MasterBase.VerCabecera = false;
    }

    /// <summary>
    /// Realiza el login de la aplicación al usuario
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        PanelInfo.MostrarMensaje(TipoDatos.TipoError.Limpiar, string.Empty);
        try
        {
            UsuarioConexion resultado = new UsuarioConexion();
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
                PanelInfo.MostrarMensaje(TipoDatos.TipoError.Error, "Es necesario introducir un usuario y contraseña para el acceso a la aplicación");
            else
            {
                TipoDatos.TipoUsuario tipo = (rdAccesoAMPA.Checked) ? TipoDatos.TipoUsuario.AMPA : TipoDatos.TipoUsuario.EXTR;
                resultado = NegUsuario.Login(txtUsuario.Text.Trim().ToUpper(), txtContrasena.Text, tipo);
                if (resultado != null)
                {
                    //Se guardan los datos en sesión
                    MasterBase.DatosSesionLogin = resultado;
                    Response.Redirect("~/UI/Index.aspx", false);
                }
                else
                    PanelInfo.MostrarMensaje(TipoDatos.TipoError.Error, "El usuario no tiene permiso para acceder a la aplicación");
            }
        }
        catch(Exception ex)
        {
            Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".btnLogin_Click().", ex);
            ErrorGeneral("Se ha producido un error al realizar el acceso a la aplicación");
        }
    }
}

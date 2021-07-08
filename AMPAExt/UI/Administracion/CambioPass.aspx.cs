//using System;
//using GUAI.Neg;

///// <summary>
///// Representa el cambio de contraseña de un usuario en el sistema
///// </summary>
//public partial class UI_Administracion_CambioPass : PageBase
//{
//    /// <summary>
//    /// Evento producido al pulsar sobre el botón de cambiar contraseña. Comprueba que el campo nueva contraseña y repetir contraseña coincidan y que la contraseña tenga un mínimo de 6 caracteres
//    /// </summary>
//    /// <param name="sender"></param>
//    /// <param name="e"></param>
//    protected void btnCambiar_Click(object sender, EventArgs e)
//    {
//        try
//        {
//            MostrarMensaje(RAEE.Comun.Log.TipoError.Limpiar, string.Empty);

//            //Si no coincide la nueva con repetir
//            if (tbNueva.Text != tbRepetir.Text)
//            {
//                MostrarMensaje(RAEE.Comun.Log.TipoError.Error, "La nueva contraseña introducida no coincide");
//                tbAntigua.Focus();
//                return;
//            }
//            //Si tiene menos de 6 caracteres
//            if (tbNueva.Text.Length < 6)
//            {
//                MostrarMensaje(RAEE.Comun.Log.TipoError.Error, "La nueva contraseña debe tener al menos 6 caracteres");
//                tbAntigua.Focus();
//                return;
//            }
//            //Si ha ido todo bien se guarda la nueva contraseña

//            NegPersona negocio = new NegPersona();

//            if (negocio.ModificarContrasenia(MasterBase.DatosSesionLogin.Perfil.Persona.IdPersona, tbAntigua.Text, tbNueva.Text) == 0)
//                MostrarMensaje(RAEE.Comun.Log.TipoError.Success, "El cambio de contraseña se ha realizado correctamente");
//            else
//                MostrarMensaje(RAEE.Comun.Log.TipoError.Error, negocio.ErrorMsg);
//        }
//        catch (Exception ex)
//        {
//            string idPersonaIGUAI = "Sin datos de sesión";
//            if (MasterBase.DatosSesionLogin != null && MasterBase.DatosSesionLogin.Perfil!=null && MasterBase.DatosSesionLogin.Perfil.Persona!=null)
//                idPersonaIGUAI= MasterBase.DatosSesionLogin.Perfil.Persona.IdPersona.ToString();
//            MasterPageBase.TrazaLog.Error("Error en " + this.GetType().FullName + ".btnCambiar_Click(idUsuarioGUAI: " + idPersonaIGUAI + ")", ex);
//            Error("La contraseña no ha podido ser modificada");
//        }
//    }

//    /// <summary>
//    /// Muestra en pantalla el tipo de mensaje que se desee dar al usuario
//    /// </summary>
//    /// <param name="tipo">Tipo de mensaje a mostrar</param>
//    /// <param name="mensaje">Mensaje que se le va a dar</param>
//    private void MostrarMensaje(RAEE.Comun.Log.TipoError tipo, string mensaje)
//    {
//        switch (tipo)
//        {
//            case RAEE.Comun.Log.TipoError.Error:
//                alertError.Visible = true;
//                lbError.Text = mensaje;
//                break;
//            case RAEE.Comun.Log.TipoError.Warning:
//                alertWarning.Visible = true;
//                lbWarning.Text = mensaje;
//                break;
//            case RAEE.Comun.Log.TipoError.Success:
//                alertSuccess.Visible = true;
//                lbSuccess.Text = mensaje;
//                break;
//            case RAEE.Comun.Log.TipoError.Limpiar:
//            default:
//                alertSuccess.Visible = false;
//                alertWarning.Visible = false;
//                alertError.Visible = false;
//                break;
//        }
//    }
//}
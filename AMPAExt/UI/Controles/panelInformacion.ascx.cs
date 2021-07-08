using System;
using AMPAExt.Comun;

namespace AMPAExt.UI.Controles
{
    /// <summary>
    /// Panel utilizado para mostrar mensajes de error en las interfaces de la aplicación
    /// </summary>
    public partial class panelInformacion : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
    }
}
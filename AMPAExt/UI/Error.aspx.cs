using System;

namespace AMPAExt.UI
{
    /// <summary>
    /// Clase controladora de errores controlados
    /// </summary>
    public partial class Error : PageBase
    {
        /// <summary>
        /// Antes de que se carge la página se define que no se compruebe los permisos del usuario
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            MasterBase.CheckSesion = false;
        }

        /// <summary>
        /// Se ocultan las sessiones no necesarias y en el caso de que se quiere visualizar el detalle del error, se mostrará mediante la propiedad MensajeError de la PageBase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
          //  MasterBase.VerBotonera = false;
         //   MasterBase.VerCabecera = false;

            if (!string.IsNullOrEmpty(MensajeError))
            {
                dvError.Visible = true;
                mensaje.Text = MensajeError;
            }
            else
                dvError.Visible = false;
        }

        /// <summary>
        /// Libera la sesión y redirige a la página de login
        /// </summary>
        /// <param name="sender">Control que remite la acción</param>
        /// <param name="e">Argumento que ejecuta el evento</param>
        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(MasterBase.RelativeURL + "Login.aspx", false);
        }

        /// <summary>
        /// Vuelve a la página inicial de la aplicación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btnvolver_Click(object sender, EventArgs e)
        {
            Response.Redirect(MasterBase.RelativeURL + "index.aspx", false);
        }
    }
}
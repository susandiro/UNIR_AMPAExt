using System;
using System.Web.UI;

namespace AMPAExt.UI.Controles
{
    public partial class UCPanelConfirmacion : System.Web.UI.UserControl
    {
        #region Propiedades
        private PageBase _Page { get { return Page as PageBase; } }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "scriptsPanel" + ClientID, "CerrarMensajeConfirmacion()", true);
        }

        /// <summary>
        /// Evento generado al pulsar el botón aceptar crear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            AceptarCrear?.Invoke(sender, e);
        }

        /// <summary>
        /// Evento que se lanza cuando se hace click en el botón aceptar crear
        /// </summary>
        public event EventHandler AceptarCrear;

        #endregion

        #region Métodos

        /// <summary>
        /// Método que abre el mensaje de confirmación
        /// </summary>
        /// <param name="textoConfirmacion"></param>
        public void AbrirMensajeConfirmacion(string textoConfirmacion)
        {
            lblMensajeConfirmacion.Text = textoConfirmacion;
            ScriptManager.RegisterStartupScript(Page, GetType(), "confirm", "AbrirMensajeConfirmacion();", true);
        }
        #endregion
    }
}
using System;

namespace AMPAExt.UI
{
    /// <summary>
    /// Clase inicial para el menú de extraecolar
    /// </summary>
    public partial class UI_Extraescolar : PageBase
    {
        /// <summary>
        /// Antes de que se carge la página se define que no se compruebe los si necesita estar logado
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            MasterBase.CheckSesion = true;
        }
    }
}
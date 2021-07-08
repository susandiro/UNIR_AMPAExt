using System;

namespace AMPAExt.UI
{
    public partial class Index : PageBase
    {
        /// <summary>
        /// Antes de que se carge la página se define que no se compruebe los si necesita estar logado
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            MasterBase.CheckSesion = true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMPA.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class USUARIO_EMPRESA
    {
        public int ID_USUARIO_EMP { get; set; }
        public int ID_EMPRESA { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO1 { get; set; }
        public string APELLIDO2 { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONO { get; set; }
        public string LOGIN { get; set; }
        public string PASSWORD { get; set; }
        public string OBSERVACIONES { get; set; }
        public System.DateTime FECHA { get; set; }
        public Nullable<System.DateTime> FECHA_MOD { get; set; }
        public string USUARIO { get; set; }
    
        public virtual EMPRESA EMPRESA { get; set; }
        public virtual TIPO_DOCUMENTO TIPO_DOCUMENTO { get; set; }
    }
}

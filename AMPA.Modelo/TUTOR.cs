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
    
    public partial class TUTOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TUTOR()
        {
            this.ALUMNO = new HashSet<ALUMNO>();
        }
    
        public int ID_TUTOR { get; set; }
        public int ID_AMPA { get; set; }
        public int T1_ID_TIPO_DOCUMENTO { get; set; }
        public string T1_NUMERO_DOCUMENTO { get; set; }
        public string T1_NOMBRE { get; set; }
        public string T1_APELLIDO1 { get; set; }
        public string T1_APELLIDO2 { get; set; }
        public string T1_EMAIL { get; set; }
        public string T1_TELEFONO { get; set; }
        public Nullable<int> T2_ID_TIPO_DOCUMENTO { get; set; }
        public string T2_NUMERO_DOCUMENTO { get; set; }
        public string T2_NOMBRE { get; set; }
        public string T2_APELLIDO1 { get; set; }
        public string T2_APELLIDO2 { get; set; }
        public string T2_EMAIL { get; set; }
        public string T2_TELEFONO { get; set; }
        public string INDIVIDUAL { get; set; }
        public System.DateTime FECHA { get; set; }
        public Nullable<System.DateTime> FECHA_MOD { get; set; }
        public string USUARIO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ALUMNO> ALUMNO { get; set; }
        public virtual AMPA AMPA { get; set; }
        public virtual TIPO_DOCUMENTO TIPO_DOCUMENTO { get; set; }
        public virtual TIPO_DOCUMENTO TIPO_DOCUMENTO1 { get; set; }
    }
}

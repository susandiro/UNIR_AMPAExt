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
    
    public partial class CURSO_CLASE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CURSO_CLASE()
        {
            this.ALUMNO = new HashSet<ALUMNO>();
        }
    
        public int ID_CURSO_CLASE { get; set; }
        public int ID_CURSO { get; set; }
        public int ID_CLASE { get; set; }
        public int ID_AMPA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ALUMNO> ALUMNO { get; set; }
        public virtual AMPA AMPA { get; set; }
        public virtual CLASE CLASE { get; set; }
        public virtual CURSO CURSO { get; set; }
    }
}

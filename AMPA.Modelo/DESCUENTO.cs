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
    
    public partial class DESCUENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DESCUENTO()
        {
            this.ACTIVIDAD_DESCUENTO = new HashSet<ACTIVIDAD_DESCUENTO>();
        }
    
        public int ID_DESCUENTO { get; set; }
        public string NOMBRE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACTIVIDAD_DESCUENTO> ACTIVIDAD_DESCUENTO { get; set; }
    }
}

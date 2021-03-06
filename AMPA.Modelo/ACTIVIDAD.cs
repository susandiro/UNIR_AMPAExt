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
    
    public partial class ACTIVIDAD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACTIVIDAD()
        {
            this.ACTIVIDAD_HORARIO = new HashSet<ACTIVIDAD_HORARIO>();
            this.ACTIVIDAD_DESCUENTO = new HashSet<ACTIVIDAD_DESCUENTO>();
        }
    
        public int ID_ACTIVIDAD { get; set; }
        public int ID_EMPRESA { get; set; }
        public int ID_AMPA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string ACTIVO { get; set; }
        public string OBSERVACIONES { get; set; }
        public System.DateTime FECHA { get; set; }
        public Nullable<System.DateTime> FECHA_MOD { get; set; }
        public string USUARIO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACTIVIDAD_HORARIO> ACTIVIDAD_HORARIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACTIVIDAD_DESCUENTO> ACTIVIDAD_DESCUENTO { get; set; }
        public virtual AMPA AMPA { get; set; }
        public virtual EMPRESA EMPRESA { get; set; }
    }
}

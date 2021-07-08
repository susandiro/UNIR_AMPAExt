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
    
    public partial class ALUMNO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ALUMNO()
        {
            this.ALUMNO_ACTIVIDAD = new HashSet<ALUMNO_ACTIVIDAD>();
        }
    
        public int ID_ALUMNO { get; set; }
        public int ID_TUTOR { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO1 { get; set; }
        public string APELLIDO2 { get; set; }
        public System.DateTime FECHA_NACIMIENTO { get; set; }
        public int ID_CURSO_CLASE { get; set; }
        public System.DateTime FECHA { get; set; }
        public Nullable<System.DateTime> FECHA_MOD { get; set; }
        public string USUARIO { get; set; }
    
        public virtual CURSO_CLASE CURSO_CLASE { get; set; }
        public virtual TUTOR TUTOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ALUMNO_ACTIVIDAD> ALUMNO_ACTIVIDAD { get; set; }
    }
}

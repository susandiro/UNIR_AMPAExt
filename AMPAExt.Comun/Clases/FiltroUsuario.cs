using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMPAExt.Comun
{
    public class FiltroUsuario
    {
        public bool Vacio { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int IdEmpresa { get; set; }
        public string CampoOrdenacion { get; set; }
        public bool OdenacionAscendente { get; set; }
        public string Activo { get; set; }
    }

    public class FiltroActividad
    {
        public bool Vacio { get; set; }
        public int IdActividad { get; set; }
        public string Nombre { get; set; }
        public string Nombre_Monitor { get; set; }
        public int Id_Monitor { get; set; }
        public int Id_Descuento { get; set; }
        public string Telefono { get; set; }
        public int IdEmpresa { get; set; }
        public int IdAMPA { get; set; }
        public string CampoOrdenacion { get; set; }
        public bool OdenacionAscendente { get; set; }
        public string Activo { get; set; }
    }

    public class FiltroAlumno
    {
        public bool Vacio { get; set; }
        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string NumDocumentoTutor { get; set; }
        public int Id_ActividadHorario { get; set; }
        public int IdCurso { get; set; }
        public int IdClase { get; set; }
        public int IdEmpresa { get; set; }
        public int IdAMPA { get; set; }
        public string CampoOrdenacion { get; set; }
        public bool OdenacionAscendente { get; set; }
        public string Activo { get; set; }
    }
}
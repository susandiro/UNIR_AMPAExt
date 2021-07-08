using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMPAExt.Comun
{
    /// <summary>
    /// Se definen los tipos de datos comunes de la aplicación
    /// </summary>
    public class TipoDatos
    {
        /// <summary>
        /// Tipo de documentos
        /// </summary>
        public enum TiposMIME
        {
            Text=0,
            XML=1,
            Excel =2,
            XSD=3,
            PDF= 4
        }

        /// <summary>
        /// Listado con el tipo de errores que se pueden producir en la aplicación
        /// </summary>
        public enum TipoError
        {
            Error = 0,
            Warning = 1,
            Success = 2,
            Limpiar = 3
        }

       /// <summary>
        /// Listado con el tipo de usuario que puede tener la aplicación
        /// </summary>
        public enum TipoUsuario
        {
            AMPA = 1,
            EXTR = 2
        }

        /// <summary>
        /// Listado del tipo de ordenación
        /// </summary>
        public enum Ordenacion
        {
            ASC,
            DESC
        }
    }
}

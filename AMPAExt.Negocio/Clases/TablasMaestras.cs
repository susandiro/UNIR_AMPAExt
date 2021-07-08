using System.Collections.Generic;
using AMPA.Modelo;
using AMPAExt.Comun;

namespace AMPAExt.Negocio
{
    /// <summary>
    /// Tratamiento de la información de todas las tablas maestras del sistema
    /// </summary>
    public class TablasMaestras : Comun
    {
        /// <summary>
        /// Recupera todos los tipos de documento existentes en el sistema
        /// </summary>
        /// <returns></returns>
        public List<TIPO_DOCUMENTO> GetTiposDocumento()
        {
            return TablasMaestrasDat.GetTiposDocumento();
        }

        /// <summary>
        /// Obtiene el listado de descuentos posibles
        /// </summary>
        /// <returns>Listado de <see cref="DESCUENTO"/> con los descuentos existentes</returns>
        public List<DESCUENTO> GetDescuentos()
        {
            return TablasMaestrasDat.GetDescuentos();
        }
    }
}
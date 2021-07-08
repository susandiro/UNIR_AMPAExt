using System;
using System.Collections.Generic;
using System.Linq;
using AMPA.Modelo;
using AMPAExt.Comun;
namespace AMPAExt.Modelo
{
    /// <summary>
    /// Tratamiento de la información de todas las tablas maestras del sistema
    /// </summary>
    public class clsTablasMaestras
    {
        /// <summary>
        /// Recupera todos los tipos de documento existentes en el sistema
        /// </summary>
        /// <returns></returns>
        public List<TIPO_DOCUMENTO> GetTiposDocumento()
        {
            using (AMPAEXTBD conn = new AMPAEXTBD())
            {
                return conn.TIPO_DOCUMENTO.OrderBy(d => d.DESCRIPCION).ToList();
            }
        }

        /// <summary>
        /// Obtiene el listado de descuentos posibles
        /// </summary>
        /// <returns>Listado de <see cref="DESCUENTO"/> con los descuentos existentes</returns>
        public List<DESCUENTO> GetDescuentos()
        {
            List<DESCUENTO> resultado = new List<DESCUENTO>();
            try
            {
                using (AMPAEXTBD db = new AMPAEXTBD())
                {
                    resultado = db.DESCUENTO
                       .ToList();
                }
            }
            catch (Exception ex)
            {
                Log.TrazaLog.Error("Error en " + this.GetType().FullName + ".GetDescuentos()", ex);
            }
            return resultado;
        }
    }
}

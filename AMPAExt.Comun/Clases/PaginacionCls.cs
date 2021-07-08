using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMPAExt.Comun.Clases
{
    [Serializable]
    public class PaginacionCls
    {
        private int pagActual;
        /// <summary>
        /// Página actual que estamos mostrando
        /// </summary>
        public int PaginaActual
        {
            get { return pagActual; }
            set { pagActual = value; }
        }

        private int regIni;
        /// <summary>
        /// Registro inicial que queremos mostrar.
        /// </summary>
        public int RegistroInicial
        {
            get
            {
                if (regIni <= 0)
                    return 0;
                else
                    return regIni;
            }
            set { regIni = value; }
        }

        private int numRegsPP;
        /// <summary>
        /// Indica el número de registros por página que van a mostrarse
        /// </summary>
        public int RegistrosPorPagina
        {
            get
            {
                if (numRegsPP <= 0)
                    return 15;
                else
                    return numRegsPP;
            }
            set { numRegsPP = value; }
        }

        private int numRegsTotales;
        /// <summary>
        /// Indica el número de registros totales de la consulta.
        /// </summary>
        public int NumeroRegistrosTotales
        {
            get { return numRegsTotales; }
            set { numRegsTotales = value; }
        }

        private string campoOrdenacion;
        /// <summary>
        /// Campo por el que se ordena
        /// </summary>
        public string CampoOrdenacion
        {
            get
            {
                if (string.IsNullOrEmpty(campoOrdenacion))
                    return string.Empty;
                else
                    return campoOrdenacion;
            }
            set { campoOrdenacion = value; }
        }

        private string orden;
        /// <summary>
        /// Si es ASCENDENTE o DESCENDENTE
        /// </summary>
        public string Orden
        {
            get
            {
                if (string.IsNullOrEmpty(CampoOrdenacion))
                    return string.Empty;
                else
                    if (string.IsNullOrEmpty(orden))
                        return Asc;
                    else return orden;
            }
            set { orden = value; }
        }

        public const string Asc = "Ascending";
        public const string Desc = "Descending";

    }
}

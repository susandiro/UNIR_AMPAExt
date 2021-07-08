using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAEE.Comun.Clases
{
    [Serializable]
    public class ProductorCls
    {
        public class listProductores
        { 
            public int ID_PRODUCTOR {get;set;}
            public int NUMERO_EMPRESA { get; set; }
            public int? ID_ORGANIZACION { get; set; }
            public string NOMBRE { get; set; }
            public int ID_HIS_PRODUCTOR { get; set; }
            public string TIPO_DOCUMENTO { get; set; }
            public string NUMERO_DOCUMENTO { get; set; }
            public string COD_CCAA { get; set; }
            public string ACTIVO { get; set; }  

        }
    }
}

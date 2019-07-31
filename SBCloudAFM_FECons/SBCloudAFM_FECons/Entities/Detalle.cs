using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBCloudAFM_FECons.Entities
{
    public class Detalle
    {
        public int CodigoD { get; set; }
        public int CantidadD { get; set; }
        public string ProductoD { get; set; }
        public float PrecioD { get; set; }
        public float IvaD { get; set; }
        public float ImporteD { get; set; }
    }
}

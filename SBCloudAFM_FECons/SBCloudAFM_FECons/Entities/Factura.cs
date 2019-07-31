using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBCloudAFM_FECons.Entities
{
    public class Factura
    {
        public string CodigoFact { get; set; }
        public int RucCI { get; set; }
        public string Cliente { get; set; }
        public DateTime FechaFac { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public float SubTotal { get; set; }
        public float IvaTotal { get; set; }
        public float Total { get; set; }
        public string Observacion { get; set; }
        public List<Detalle> ListaDet { get; set; }
    }
}

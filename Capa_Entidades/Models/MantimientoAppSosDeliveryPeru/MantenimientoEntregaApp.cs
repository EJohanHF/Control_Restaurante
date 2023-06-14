using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.MantimientoAppSosDeliveryPeru
{
    public class MantenimientoEntregaApp
    {
        public string id { get; set; }
        public string id_business { get; set; }
        public string valor { get; set; }
        public string costo { get; set; }
        public byte estado { get; set; }
        public string image { get; set; }
    }
}

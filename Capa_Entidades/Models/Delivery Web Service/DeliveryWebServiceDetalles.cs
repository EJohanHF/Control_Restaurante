using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Delivery_Web_Service
{
    public class DeliveryWebServiceDetalles
    {
        public string id { get; set; }
        public string id_carta { get; set; }
        public string id_order { get; set; }
        public string itemname { get; set; }
        public string itemquantity { get; set; }
        public string attribute { get; set; }
        public string itemprice { get; set; }
        public string itemtotal { get; set; }
        public string sosfood { get; set; }
        public string descripcion { get; set; }
    }
}

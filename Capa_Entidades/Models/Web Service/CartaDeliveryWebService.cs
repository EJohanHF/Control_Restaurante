using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Web_Service
{
    public class CartaDeliveryWebService
    {
        public string Name { get; set; }
        public string id_carta { get; set; }
        public decimal price { get; set; }
        public decimal discount { get; set; }
        public bool estado_del { get; set; }
        public byte[] imagen { get; set; }
        public decimal price_salon { get; set; }
        public bool estado_sal { get; set; }
        public decimal discount_salon { get; set; }
    }
}

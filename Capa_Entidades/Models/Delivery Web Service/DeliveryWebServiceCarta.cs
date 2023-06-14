using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Delivery_Web_Service
{
    public class DeliveryWebServiceCarta
    {
        public string name { get; set; }
                        public string id_business { get; set; }
        //values["token"] = "app963";
        public string discount { get; set; }
        public string price { get; set; }
        public object imagen { get; set; }
        public string id_carta { get; set; }
        public string estado { get; set; }
    }
}

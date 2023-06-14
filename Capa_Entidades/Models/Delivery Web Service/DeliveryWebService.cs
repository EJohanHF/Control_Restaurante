using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Delivery_Web_Service
{
    public class DeliveryWebService
    {
        public int cod_orden { get; set; }
        public int id_business { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string mobile { get; set; }
        public string area { get; set; }
        public string address { get; set; }

        public string status { get; set; }
        public DateTime date { get; set; }
        public string user_id { get; set; }
        public decimal total { get; set; }
        public int idpedido { get; set; }
        public string Habilitado { get; set; }

        public string latitud { get; set; }
        public string longitud { get; set; }
        public string tipo_doc_electronico { get; set; }
        public string tipo_doc_identidad { get; set; }
        public string nro_identidad_cliente { get; set; }

        public string deno_cliente { get; set; }

        public string NomTipDoc { get; set; }
        public string NomTipIde { get; set; }
        public string deno_entrega { get; set; }

        public string img_entrega { get; set; }
        public string nombre_mesa { get; set; }

        public string token { get; set; }

        public string tipo_sosfood { get; set; }
        public string metodo_pago { get; set; }
        public string NomMesa { get; set; }
        public string paga_con { get; set; }
        public string vuelto { get; set; }
    }
}

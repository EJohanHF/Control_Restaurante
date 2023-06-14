using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Web_Service
{
    public class EmpresaDelivery
    {
        public string id { get; set; }
        public string id_rubro { get; set; }
        public string ruc { get; set; }
        public string nombre_razon { get; set; }
        public string direccion { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string imagen { get; set; }
        public string llave_publica { get; set; }
        public string llave_secreta { get; set; }
        public string tiempo_espera { get; set; }

    }
}

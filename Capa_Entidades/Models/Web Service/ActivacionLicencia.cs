using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Web_Service
{
    public class ActivacionLicencia
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public DateTime fecha_vencimiento { get; set; }
        public int cambio { get; set; }
        public DateTime fecha_actualizacion { get; set; }
    }
}

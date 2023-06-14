using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Cuentas
{
    public class Cuentas
    {
        public string ID { get; set; }
        public string ID_PED { get; set; }
        public string NOM_CUENTA { get; set; }
        public string SUBTOTAL_CUENTA { get; set; }
        public string EST_CUENTA { get; set; }
        public string ESTADO_CUENTA { get; set; }
        public DateTime FECHA_CUENTA { get; set; }
    }
}

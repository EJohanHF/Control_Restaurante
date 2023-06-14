using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.Pedido
{
    public class DpAdelantoxEmpleado
    {
        public string id { get; set; }
        public string EMPL_NOM { get; set; }
        public string CC_MONTO { get; set; }
        public string CC_DESCR { get; set; }
        public string CC_F_CREATE { get; set; }
        public DateTime desde { get; set; } = DateTime.Now;
        public DateTime hasta { get; set; } = DateTime.Now;
        public DateTime mes { get; set; } = DateTime.Now;
        public DateTime dia { get; set; } = DateTime.Now;
    }
}

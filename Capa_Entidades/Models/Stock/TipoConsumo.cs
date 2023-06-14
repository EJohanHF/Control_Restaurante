using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Stock
{
    public class TipoConsumo
    {
        public int ID { get; set; }
        public string TC_DESCR { get; set; }
        public DateTime TC_F_CREATE { get; set; }
        public DateTime TC_F_MODIFICACION { get; set; }
    }
}

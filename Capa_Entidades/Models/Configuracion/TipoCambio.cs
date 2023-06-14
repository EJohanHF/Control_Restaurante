using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Configuracion
{
    public class TipoCambio
    {
        public int ID { get; set; }
        public string TM_DESCR { get; set; }
        public string VALOR { get; set; }
        public decimal TC_CAMBIO { get; set; } = 0;
    }
}

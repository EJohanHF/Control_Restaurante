using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Configuracion
{
    public class TipoCaja
    {
        public TipoCaja()
        {

        }
        public int id { get; set; }
        public string MC_DESCR { get; set; }
        public byte MC_ACT { get; set; }
        public int MC_ID_TIPO { get; set; }
        public string TM_DESCR { get; set; }

    }
}

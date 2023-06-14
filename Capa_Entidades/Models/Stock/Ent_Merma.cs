using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Stock
{
    public class Ent_Merma
    {
        public int ID { get; set; }
        public string MI_ID_INS { get; set; }
        public decimal MI_CANT { get; set; }
        public string INS_NOM { get; set; }
        public int MI_ID_USU { get; set; }
        public string USU_NOM { get; set; }
        public string MI_DESCR { get; set; }
        public DateTime MI_F_CREATE { get; set; }
        public int MI_ID_ALM { get; set; }
        public string ALM_NOM { get; set; }

    }
}

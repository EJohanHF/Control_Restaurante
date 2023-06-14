using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Carta
{
    public class DetDescripciones
    {
        public DetDescripciones()
        {

        }
        public int ID { get; set; }
        public int DP_ID_DESC { get; set; }
        public string DET_DESCRIPTION { get; set; }
        public string TITLE_DESCRIPTION { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Receta
{
    public class Recetas
    {
        public Recetas()
        {
        }
        public int ID { get; set; }
        public int RE_ID_CARTA { get; set; }

        public string INS_NOM { get; set; }
        public int RE_ID_INS { get; set; }
        public bool RE_ACT { get; set; }

        public DateTime RE_F_CREATE { get; set; }
        public decimal RE_CANT_INS { get; set; }
        public string TM_DESC { get; set; }
        public string RE_CANT_MED_INS { get; set; }
        public decimal RE_COSTO_RECETA { get; set; }
        public bool RE_SUB_RECETA { get; set; }
        public string RE_DESCR { get; set; }
        public bool RE_INS_ACT { get; set; }

        //SUB RECETA
        public int SR_ID { get; set; }
        public String SR_DESCR { get; set; }
        public bool SR_ACT { get; set; }
        public decimal SR_COSTO { get; set; }
        public DateTime SR_F_CREATE { get; set; }
        public DateTime SR_F_MODIFICACION { get; set; }

        //objects
        public string VisibilityEdit { get; set; }
        public string iconoActDes { get; set; }
        public string TextoActDes { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes
{
    public class DpAnulados
    {
        public int IDPED { get; set; }
        public string PED_NUM_DIARIO { get; set; }
        public string M_NOM { get; set; }
        public string PED_TOTAL { get; set; }
        public string USU_NOM { get; set; }
        public object EMPL_NOM { get; set; }
        public string NOM_CARGO { get; set; }
        public string PED_FECH_PED { get; set; }
        public object nomllevar { get; set; }

        public DateTime desde { get; set; } = DateTime.Now;
        public DateTime hasta { get; set; } = DateTime.Now;
        public DateTime mes { get; set; } = DateTime.Now;
        public DateTime dia { get; set; } = DateTime.Now;


        /*dp.ID,ISNULL(P.PED_NUM_DIARIO,'')PED_NUM_DIARIO ,M.M_NOM,dp.DP_CANT,dp.DP_DESCRIP,dp.DP_IMPORT,dp.DP_COMENTARIO,e.EMPL_NOM,ISNULL(c.NOM_CARGO,'')NOM_CARGO*/
    }
}

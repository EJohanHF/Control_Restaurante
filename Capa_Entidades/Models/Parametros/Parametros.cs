using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Parametros
{
    public class Parametros
    {
        public string IDTIPO { get; set; }
        public int ID_PAR { get; set; }
        public string NOM_PAR { get; set; }
        public string VALOR_PAR { get; set; }
        public string EST_PAR { get; set; }
        public bool ACT_PAR { get; set; }
        public DateTime FEC_CREAC { get; set; }
        public string activo { get; set; }
        public string est_activo { get; set; }
        public string activo_string { get; set; }
        public int tipoParametro { get; set; }
    }
}

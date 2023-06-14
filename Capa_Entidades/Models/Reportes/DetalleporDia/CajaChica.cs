using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.DetalleporDia
{
    public  class CajaChica
    {
        #region CAJA CHICA
        public object IDTMCAJA { get; set; }
        public string TM_DESCR { get; set; }
        public object IDMOVCAJA { get; set; }
        public string MC_DESCR { get; set; }
        public object IDCAJACHICA { get; set; }
        public object CC_MONTO { get; set; }
        public string CC_DESCR { get; set; }
        public string EMPL_NOM { get; set; }
        public string CC_F_CREATE { get; set; }
        public DateTime desde { get; set; } = DateTime.Now;
        public DateTime hasta { get; set; } = DateTime.Now;
        public DateTime dia { get; set; } = DateTime.Now;
        #endregion
    }
}

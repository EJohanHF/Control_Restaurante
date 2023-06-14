using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.DetalleporDia
{
    public class VentaPlatosxImpresora
    {
        #region VENTAS POR IMPRESORA
        public int IDIMP { get; set; }
        public string NOMEQUIPOIMP { get; set; }
        public object IDDETPED { get; set; }
        public object DP_CANT { get; set; }
        public object IDCAJACHICA { get; set; }
        public string DP_DESCRIP { get; set; }
        public object DP_IMPORT { get; set; }
        public string EMPL_NOM { get; set; }
        public object DP_FEC_REG { get; set; }
        public int IDIMPRESORA { get; set; }
        public DateTime desde { get; set; } = DateTime.Now;
        public DateTime hasta { get; set; } = DateTime.Now;
        public DateTime dia { get; set; } = DateTime.Now;
        #endregion
    }
}

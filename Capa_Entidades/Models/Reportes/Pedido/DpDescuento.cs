using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.Pedido
{
    public class DpDescuento
    {

        public int ID { get; set; }
        public string PED_NUM_DIARIO { get; set; }
        public string M_NOM { get; set; }
        public object DP_DESCU { get; set; }
        public object DP_IMPORT { get; set; }
        public string DENO_PAGO { get; set; }
        public string DESC_EST { get; set; }
        //public DateTime DP_FEC_REG { get; set; }
        public string DP_FEC_REG { get; set; }
        public string DP_FEC_REG2 { get; set; }
        public object efectivo { get; set; }
        public object descuento { get; set; }
        public string EMPL_NOM { get; set; }
        public string NOM_CARGO { get; set; }
        //public DateTime DC_F_CREATE { get; set; }
        public string DC_F_CREATE { get; set; }
        public string IDDC { get; set; }
        public string TD_DESCR { get; set; }
        public int TD_ID { get; set; }

        public int idDescuento { get; set; }
        public string Descripcion { get; set; }
        public string Cantidad { get; set; }
        public string Monto { get; set; }

        public DateTime desde { get; set; } = DateTime.Now;
        public DateTime hasta { get; set; } = DateTime.Now;
        public DateTime mes { get; set; } = DateTime.Now;
        public DateTime dia { get; set; } = DateTime.Now;
      
    }
}

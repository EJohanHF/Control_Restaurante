using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.Pedido
{
    public class DpVentaMozo
    {

        public object ID { get; set; }
        public string PED_NUM_DIARIO { get; set; }
        public string M_NOM { get; set; }
        public object PED_TOTAL { get; set; }
        public string DENO_PAGO { get; set; }
        public string DESC_EST { get; set; }
        public string PED_FECH_PED { get; set; }
        public string EMPL_NOM { get; set; }

        #region tipo pago
        public object tp_m_efectivo { get; set; }
        public object tp_m_tarjeta { get; set; }
        public object tp_m_total { get; set; }
        #endregion
        #region ranking mozo
        public string RM_id_emp { get; set; }
        public string RM_nom_emp { get; set; }
        public object RM_subtotal { get; set; }
        public object RM_descuento { get; set; }
        public object RM_totalped{ get; set; }
        #endregion

        #region Detalle Pedido
        public object DP_Cant { get; set; }
        public string DP_Detalle { get; set; }
        public object DP_importe { get; set; }
        public string DP_fech_ped { get; set; }
        #endregion

        #region Detalle Producto
        public string DProd_nom_emp { get; set; }
        public object DProd_cant { get; set; }
        public string DProd_nom_carta { get; set; }
        public object DProd_monto { get; set; }
        #endregion
        /*SELECT e.EMPL_NOM,SUM(p.PED_SUBTOTAL),SUM(p.PED_DESCUENTO),SUM(p.PED_TOTAL)    */
        public DateTime desde { get; set; } = DateTime.Now;
        public DateTime hasta { get; set; } = DateTime.Now;
        public DateTime mes { get; set; } = DateTime.Now;
        public DateTime dia { get; set; } = DateTime.Now;
    }
}

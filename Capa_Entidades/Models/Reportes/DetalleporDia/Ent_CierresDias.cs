using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.DetalleporDia
{
    public class Ent_CierresDias
    {
        public int ID { get; set; }
        public decimal DC_MONTO_EFECTIVO { get; set; }
        public decimal DC_MONTO_TARJETA { get; set; }
        public decimal DC_MONTO_TOTAL { get; set; }
        public decimal DC_ID_TIP_MONEDA { get; set; }
        public int DC_ID_TIPO_CAMBIO { get; set; }
        public int DC_ID_USU { get; set; }
        public string USU_NOM { get; set; }
        public string DC_F_CREATE { get; set; }
    }
}

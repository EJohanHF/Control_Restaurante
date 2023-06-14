using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.DetalleporDia
{
    public class Pagar
    {
        public int id { get; set; }
        public int idpago { get; set; } = 0;

        public decimal totals { get; set; }
        public decimal amortizars { get; set; }
        public decimal monto_propina { get; set; }
        public decimal saldos { get; set; }

        public string idpedido { get; set; }
        public string idusuario { get; set; }

        public decimal amortizar { get; set; }
        public decimal pagarcon { get; set; } = 0;
        public decimal vuelto { get; set; }
        public byte logo { get; set; }

        public int idtpago { get; set; }
        public string nomtpago { get; set; }

        public int idtmoneda { get; set; }
        public int ?idtpagoPropina { get; set; }
        public string nomtmoneda { get; set; }

        public string idtcambio { get; set; }
        public string nomtcambio { get; set; }

        public decimal monto { get; set; }

        public string comentario { get; set; }

        public string nrotarjeta { get; set; }

        public decimal P_SALDO { get; set; }
        public decimal P_MONTO { get; set; }
        public decimal P_ABONADO { get; set; }
    }
}

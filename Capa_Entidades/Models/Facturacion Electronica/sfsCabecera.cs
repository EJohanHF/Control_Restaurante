using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Facturacion_Electronica
{
    public class sfsCabecera
    {
        public int idCabecera { get; set; }
        public string tipOperacion { get; set; }
        public string fecEmision { get; set; }
        public string horEmision { get; set; }
        public string fecVencimiento { get; set; }
        public string codLocalEmisor { get; set; }
        public string tipDocUsuario { get; set; }
        public string numDocUsuario { get; set; }
        public string rznSocialUsuario { get; set; }
        public string tipMoneda { get; set; }
        public decimal sumTotTributos { get; set; }
        public decimal sumTotValVenta { get; set; }
        public decimal sumPrecioVenta { get; set; }
        public decimal sumDescTotal { get; set; }
        public decimal sumOtrosCargos { get; set; }
        public decimal sumTotalAnticipos { get; set; }
        public decimal sumImpVenta { get; set; }
        public string ublVersionId { get; set; }
        public string customizationId { get; set; }
    }
}

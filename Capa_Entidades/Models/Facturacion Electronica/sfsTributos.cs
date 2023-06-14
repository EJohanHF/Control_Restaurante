using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Facturacion_Electronica
{
    public class sfsTributos
    {
        public int idTributo { get; set; }
        public int idCabecera { get; set; }
        public string ideTributo { get; set; }
        public string nomTributo { get; set; }
        public string codTipTributo { get; set; }
        public decimal mtoBaseImponible { get; set; }
        public decimal mtoTributo { get; set; }
    }
}

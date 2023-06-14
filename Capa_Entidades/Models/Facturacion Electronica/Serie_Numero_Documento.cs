using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Facturacion_Electronica
{
    public class Serie_Numero_Documento
    {
        public int ID { get; set; }
        public string SERIE { get; set; }
        public string VALOR { get; set; }
        public string EST { get; set; }
        public DateTime FEC_CREAC { get; set; } = DateTime.Now;
        public DateTime FEC_UPDA { get; set; } = DateTime.Now;
        public Serie_Numero_Documento()
        {

        }
    }
}

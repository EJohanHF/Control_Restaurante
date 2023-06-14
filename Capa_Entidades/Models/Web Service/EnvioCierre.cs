using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Web_Service
{
    public class EnvioCierre
    {
        public string VBRUTA { get; set; }
        public string DESCU { get; set; }
        public string TOTALVENTA { get; set; }
        public string INGRESO { get; set; }
        public string EGRESO { get; set; }
        public string TOTALCAJACHICA { get; set; }
        public string TOTALCAJA { get; set; }
        public string BOLECANT { get; set; }
        public string FACTCANT { get; set; }
        public string TOTALBOLE { get; set; }
        public string TOTALFACT { get; set; }
        public string TOTALDOC { get; set; }
        public string TOTALSB { get; set; }
        public string BOLECANTANU { get; set; }
        public string FACTCANTANU { get; set; }
        public string TOTALBOLEANU { get; set; }
        public string TOTALFACTANU { get; set; }
        public string TOTALDOCANU { get; set; }
        public DetallePago DetallePago { get; set; }
        public DateTime FECHA { get; set; }
    }
    public class DetallePago
    {
        public string Descripcion { get; set; }
        public string Monto { get; set; }
    }
}

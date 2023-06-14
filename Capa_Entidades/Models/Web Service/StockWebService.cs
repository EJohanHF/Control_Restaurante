using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Web_Service
{
    public class StockWebService
    {
        public string id_business { get; set; }
        public string fecha { get; set; }
        public string INS_NOM { get; set; }
        public string ALM_NOM { get; set; }
        public string CANT { get; set; }
        public string id_cierre { get; set; }
    }
}

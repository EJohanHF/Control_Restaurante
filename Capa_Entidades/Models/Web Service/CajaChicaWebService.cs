using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Web_Service
{
    public class CajaChicaWebService
    {
        public string id_business { get; set; }
        public string ID { get; set; }
        public string TM_DESCR { get; set; }
        public string CC_ID_MOV { get; set; }
        public string CC_ID_TIPO { get; set; }
        public string MC_DESCR { get; set; }
        public string CC_DESCR { get; set; }
        public string CC_MONTO { get; set; }
        public string nombre_empleado { get; set; }
        public string CC_ID_EMPL { get; set; }
        public string CC_F_CREATE { get; set; }
        public string CC_CIERRE_DIA { get; set; }
    }
}

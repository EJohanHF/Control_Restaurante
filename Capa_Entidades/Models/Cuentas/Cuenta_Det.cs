using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Cuentas
{
    public class Cuenta_Det
    {
        public string ORDEN { get; set; }
        public string ID { get; set; }
        public string ID_CUENTA { get; set; }
        public string ID_PED { get; set; }
        public string ID_DET_PED { get; set; }
        public string CANT_DET_CUENTA { get; set; }
        public string DESCRIP_DET_CUENTA { get; set; }
        public string PRE_UNI_DET_CUENTA { get; set; }
        public decimal IMP_DET_CUENTA { get; set; }
        public string EST_DET_CUENTA { get; set; }
    }
}

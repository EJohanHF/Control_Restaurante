using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Configuracion
{
    public class Caja
    {
        public Caja()
        {

        }
        public int id { get; set; }
        public string TM_DESCR { get; set; }
        public int CC_ID_MOV { get; set; }  
        public int CC_ID_EMPL { get; set; }
        public string MC_DESCR { get; set; }
        public string CC_DESCR { get; set; }
        public string NOMBRE_EMPLEADO { get; set; }
        public int CC_ID_TIPO { get; set; }
        public object CC_MONTO { get; set; }
        public DateTime CC_F_CREATE { get; set; }
        public Decimal INGRESOS { get; set; }
        public Decimal EGRESOS { get; set; }
        public int MC_ID_TIPO { get; set; }
 

 


    }
}

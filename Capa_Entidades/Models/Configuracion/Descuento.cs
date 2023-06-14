using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Configuracion
{
    public class Descuento
    {
        public Descuento()
        {

        }
        public int id { get; set; }
        public string TD_DESCR { get; set; }
        public byte TD_ACT { get; set; }
        public int TD_ID_GRUPO { get; set; }
        public object TD_PORCENTAJE { get; set; }

    }
}

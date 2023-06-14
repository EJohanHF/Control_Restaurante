using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Stock
{
    public class TipoMovimientoInsumo
    {
        public TipoMovimientoInsumo()
        {
        }
        public int ID { get; set; }
        public string MOV_DESCR { get; set; }
        public bool MOV_ACT { get; set; }
        public DateTime MOV_F_CREATE { get; set; }
        public DateTime MOV_F_MODIFICACION { get; set; }


        public bool ischeck { get; set; } = true;
    }
}

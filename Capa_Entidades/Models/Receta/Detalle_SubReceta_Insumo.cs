using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Receta
{
    public class Detalle_SubReceta_Insumo
    {

        public int ID { get; set; }
        public string DSI_DESC { get; set; }
        public int DSI_ID_INS { get; set; }
        public string INS_NOM { get; set; }
        public int DSI_ID_SUB_RECETA { get; set; }
        public bool DSI_ACT { get; set; }
        public decimal DSI_COSTO_INS { get; set; }
        public DateTime DSI_F_CREATE { get; set; }
        public DateTime DSI_F_MODIFICACION { get; set; }
        public decimal DSI_CANT_INS { get; set; }
        public string SR_DESCR { get; set; }

        public Detalle_SubReceta_Insumo() { }
    }
}

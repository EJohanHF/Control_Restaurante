using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Factura_Compra
{
    public class TipoDoc
    {
        public TipoDoc()
        {
        }
        public int ID { get; set; }
        public string DC_DESCR { get; set; }
        public bool DC_ACT { get; set; }
        public int DC_ID_SERIE { get; set; }
        public DateTime DC_F_CREATE { get; set; }
        public DateTime DC_F_MODIFICACION { get; set; }
        public string SERIE { get; set; }
    }
}

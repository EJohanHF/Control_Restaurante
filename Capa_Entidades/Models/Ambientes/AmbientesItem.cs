using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Ambientes
{
    public class AmbientesItem
    {
        public AmbientesItem()
        {

        }
        public int ID { get; set; }
        public string A_NOM { get; set; }
        public int A_X { get; set; }
        public int A_Y { get; set; }
        public int A_WIDTH { get; set; }
        public int A_HEIGHT { get; set; }
        public string A_TEXTO { get; set; }
        public Boolean A_ACT { get; set; }
        public DateTime A_F_CREATE { get; set; } = DateTime.Now;
        public int nrocolumnas { get; set; }
        public int A_TOP { get; set; }
        public int A_BOTTOM { get; set; }
      
    }
}

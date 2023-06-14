using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Carta
{
  public  class Grupo
    {
        public Grupo()
        {

        }
        public int idgrup { get; set; }

        public string idscat { get; set; }
        public string nomscat { get; set; }

        public string idcat { get; set; }
        public string nomcat { get; set; }

        public string nomgrup{ get; set; }
        public byte[] imagengrup { get; set; }
        public object descgrup { get; set; }
        public bool ischeck { get; set; }
    }
}

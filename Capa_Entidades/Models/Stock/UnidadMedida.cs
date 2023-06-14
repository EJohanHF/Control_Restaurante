using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Stock
{
  public  class UnidadMedida
    {
        public int idum { get; set; }
        public string descum { get; set; }
        public string denom { get; set; }
        public byte estadoum { get; set; } = 1;
    }
}

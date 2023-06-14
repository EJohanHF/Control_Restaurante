using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Ambientes
{
    public class Ambiente
    {
        public Ambiente()
        {
        }
        public int id { get; set; }
        public String nombre { get; set; }
        public String estado { get; set; }
        public DateTime fecRegistro { get; set; }
    }
}

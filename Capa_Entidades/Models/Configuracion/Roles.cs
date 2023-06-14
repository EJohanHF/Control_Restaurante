using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Configuracion
{
    public class Roles
    {
        public Roles()
        {

        }
        public int idrol { get; set; }
        public string nomrol { get; set; }
        public byte estadorol { get; set; } = 1;
        public string menu { get; set; }
        public string color { get; set; }
        public byte estadopriv { get; set; } = 1;
    }
}

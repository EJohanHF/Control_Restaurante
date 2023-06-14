using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Principal
{
    public class Ent_MenuItem_Principal
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string color { get; set; }
        public string icon { get; set; }
        public List<Ent_MenuItem_Secundario> items { get; set; }
    }
    public class Ent_MenuItem_Secundario
    {
        public int idPrincipal { get; set; }
        public int id { get; set; }
        public string nombre { get; set; }
        public string color { get; set; }
        public string icon { get; set; }
        public string userControl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Acceso
{
    public class Ent_Usuario
    {
        //Cambio
        public string usu_nom { get; set; }
        public string usu_pass { get; set; } = "";
        public string usu_ape { get; set; }
        public int id { get; set; }
        public int id_empl { get; set; }
        public int idrol { get; set; }
        public byte estadopriv { get; set; }
    }
}

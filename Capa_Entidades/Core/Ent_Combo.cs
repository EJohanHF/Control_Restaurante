using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Ent_Combo
    {
        public Ent_Combo()
        {

        }
        public string id { get; set; }
        public string nombre { get; set; }
        public object valor1 { get; set; }
        public object valor2 { get; set; }
        public decimal valor3 { get; set; }
        
        public int idchek { get; set; }
        public string nomchek { get; set; }
        public bool ischeck { get; set; } = true;



        //l
        public string idimp { get; set; }
        public string impresora { get; set; }
        public string carta { get; set; }
        public string cantidad { get; set; }
        
        
    }
}

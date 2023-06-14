using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Carta
{
   public class Categoria
    {
        public Categoria()
        {

        }
        public int idcat { get; set; }

        public int idscat { get; set; }
        public string nomscat { get; set; }

        public string  nomcat { get; set; }
        public object desccat { get; set; }
        public byte[] imagencat { get; set; }
        public int columna { get; set; }
        public string impresoras { get; set; }
        public byte cloud { get; set; } = 1;
    }
}

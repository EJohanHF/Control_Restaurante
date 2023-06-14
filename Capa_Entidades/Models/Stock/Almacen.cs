using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Stock
{
   public class Almacen
    {
        public int idalm { get; set; }
        public string nomalm { get; set; }
        public byte estadoalm { get; set; }
        public object iddefault { get; set; }

        public int idCantidadRegistros { get; set; }
        public string cantidadRegistros { get; set; }


        public bool ischeck { get; set; } = true;
    }
}

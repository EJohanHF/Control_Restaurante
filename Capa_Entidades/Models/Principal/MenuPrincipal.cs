using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models
{
    public class MenuItem
    {
        public int id { get; set; }
        public int orden { get; set; }
        public string nombre { get; set; }
        public string icono { get; set; }
        public string vista { get; set; }
        public int idPadre { get; set; }
        public bool value { get; set; }
        public string colorIcon { get; set; }

        public List<MenuItem> items { get; set; }
        public MenuItem()
        {

        }
        public MenuItem(int id, string nombre, string icono, string vista, List<MenuItem> items)
        {
            this.id = id;
            this.nombre = nombre;
            this.icono = icono;
            this.vista = vista;
            this.items = items;
        }
    }

}

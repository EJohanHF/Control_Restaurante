using System;
using System.Collections.ObjectModel;

namespace Capa_Entidades.Models
{
    public class Persona
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public ObservableCollection<Persona> items { get; set; }
        
        public DateTime  fecNAc { get; set; }
        public Persona()
        {

        }
        public Persona(int id, string nombre , ObservableCollection<Persona> items)
        {
            this.id = id;
            this.nombre = nombre;
            this.items = items;
        }
        public Persona(int id, string nombre, DateTime fecNAc)
        {
            this.id = id;
            this.nombre = nombre;
            this.fecNAc = fecNAc;
        }
        public Persona(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }
        public override string ToString()
        {
            return nombre;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF
{
    public class RadioClass
    {
        public RadioClass(string nombre, bool value)
        {
            Nombre = nombre;
            Value = value;
        }

        public string Nombre { get; set; }
        public bool Value { get; set; }

    }
}

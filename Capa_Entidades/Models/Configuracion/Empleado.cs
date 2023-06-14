using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models
{
    public class Empleado
    {
        public Empleado()
        {

        }

        public int id { get; set; }
        public string nroDocumento { get; set; }

        public string idTipoDI { get; set; }
        public string tipoDocumento { get; set; }

        public string idcargo { get; set; }
        public string cargo { get; set; }

        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string clave { get; set; }
        public string genero { get; set; }
        public byte estado { get; set; } = 1;
        public DateTime fecNacimiento { get; set; } = DateTime.Now;
        public DateTime fecRegistro { get; set; }
        public DateTime fecActualizacion { get; set; }

      
    }
}

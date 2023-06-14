using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Usuario
{
    public class Usuario
    {
        public Usuario()
        {

        }

        public int idusu { get; set; }
        public string nomusu { get; set; }
        public string apeusu { get; set; }
        public byte estadousu { get; set; } = 1;
        public string claveusu { get; set; }
        public string claveusu_cambio { get; set; }
        public string idemple { get; set; }
        public string nomemple { get; set; }
        public string idrol { get; set; }
    }
}

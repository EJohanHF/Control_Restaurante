using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Configuracion
{
  public  class Proveedor
    {

        public int idp { get; set; }

        public string iddoc { get; set; }
        public string nomdoc { get; set; }

        public string prov_nrdoc { get; set; }
        public string prov_nom { get; set; }
        public string prov_direc { get; set; }
        public string prov_dist { get; set; }
        public string prov_telfijo { get; set; }
        public string prov_telmovil { get; set; }
        public string prov_correo { get; set; }
        public byte prov_activo { get; set; }
        public string prov_rubro { get; set; }
        public bool ischeck { get; set; } = true;
    }
}

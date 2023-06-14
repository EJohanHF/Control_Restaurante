using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Capa_Entidades.Models.Configuracion
{
  public  class Empresa
    {
       
        
        public Empresa()
        {


        }
        
        public int idEmpr { get; set; }
        public string empr_ruc { get; set; }
        public string empr_nom { get; set; }
        public string empr_nom_com  { get; set; }
        public string empr_tel { get; set; }
        public string empr_cor { get; set; }

        public string idcorp { get; set; }
        public string empr_corp { get; set; }

        public string idpais { get; set; }
        public string empr_pais { get; set; }

        public string iddepa { get; set; }
        public string empr_depa { get; set; }

        public string idprov { get; set; }
        public string empr_prov { get; set; }

        public string iddist { get; set; }
        public string empr_dist { get; set; }

        public string empr_ubig { get; set; }
        public string empr_urb { get; set; }
        public byte[] empr_logo { get; set; } //no sme permite convertir en image
        public string empr_firma { get; set; }
        public string empr_firma_alias { get; set; }
        public string empr_firma_clave_alias { get; set; }
        public string empr_firma_usr_sol { get; set; }
        public string empr_firma_clave_sol { get; set; }
        public string empr_direc { get; set; }
        public string empr_ruta_facelect { get; set; }
        public string EMPR_DEFAULT { get; set; }
        public string codigo { get; set; }
        public string token { get; set; }
        public int idcorreo { get; set; }
        public string correo { get; set; }
        public string EMPR_FV_LINCENCIA { get; set; }
    }
}

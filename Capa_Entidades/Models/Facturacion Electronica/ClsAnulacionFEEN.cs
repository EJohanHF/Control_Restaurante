using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Facturacion_Electronica
{
    public class ClsAnulacionFEEN
    {
        public string TipoDocumentoEmisor { get; set; }
        public string NroDoc{ get; set; }
        public string FechaEmision{ get; set; }
        public string Tipodocumento{ get; set; }
        public string FechaGeneracion { get; set; }
        public DataTable items { get; set; }
    }
}

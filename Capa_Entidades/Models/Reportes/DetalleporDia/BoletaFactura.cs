using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.DetalleporDia
{
  public class BoletaFactura
    {
        public int id_bf{ get; set; }
        
        public int cant { get; set; }
        public string descripcion { get; set; }
        public decimal preciouni { get; set; }
        public decimal descuento { get; set; }
        public decimal importe { get; set; }

        //public DateTime f_ped { get; set; }
        //public string p { get; set; }
        ////public decimal impor_ped { get; set; }
        //public string desc_ped { get; set; }
        ////public decimal desc_ped { get; set; }
        //public string num_dia_ped { get; set; }
        //public string subtotal_ped { get; set; }

        //public string id_tip_desc { get; set; }
        //public string nom_tip_desc { get; set; }
    }
}

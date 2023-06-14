using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.Pedido
{
    public class DpVentas
    {

        public int id_ped { get; set; }

        public string id_mesa { get; set; }
        public string nom_mesa { get; set; }

        public string id_emple { get; set; }
        public string nom_emple { get; set; }

        public string f_ped { get; set; }
        public string impor_ped { get; set; }
        //public decimal impor_ped { get; set; }
        public object desc_ped { get; set; }
        //public decimal desc_ped { get; set; }
        public string num_dia_ped { get; set; }
        public string subtotal_ped { get; set; }

        public string id_tip_desc { get; set; }
        public string nom_tip_desc { get; set; }

        public object total_ped { get; set; }
        //public decimal total_ped { get; set; }

        public string id_fpago { get; set; }
        public string nom_fpago { get; set; }

        public string id_estado_f { get; set; }
        public string nom_estado_f { get; set; }

        public string id_usu { get; set; }
        public string nom_usu { get; set; }

        public string id_turn { get; set; }
        public string nom_turn { get; set; }

        public string id_tipo_cambio { get; set; }
        public string nom_tipo_cambio { get; set; }
        public string id_dia_cierre { get; set; }
        public string fech_dia_cerre { get; set; }
        public string tipoDocumento { get; set; }

        #region detped
        public object cant_ped { get; set; }

        public string DEt_ped { get; set; }
        public object Import_ped { get; set; }
        #endregion

        #region cajachicafech
        public object cc_ingre { get; set; }

        public object cc_egreso { get; set; }
        public object cc_saldo { get; set; }
        #endregion


        #region tipo pago 
        public object tp_Montoefectivo { get; set; }
        public object tp_Montotarjeta{ get; set; }
        public object tp_Montototal { get; set; }
        #endregion
        public DateTime? desde { get; set; } = DateTime.Now;
        public DateTime? hasta { get; set; } = DateTime.Now;
        public DateTime? mes { get; set; } = DateTime.Now;
        public DateTime? dia { get; set; } = DateTime.Now;



    }
}

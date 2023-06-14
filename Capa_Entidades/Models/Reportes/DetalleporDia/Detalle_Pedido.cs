using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Capa_Entidades.Models.Reportes.DetalleporDia
{
    public class Detalle_Pedido
    {
        public int id_ped { get; set; }

        public string id_mesa { get; set; }
        public string nom_mesa { get; set; }

        public string id_emple { get; set; }
        public string nom_emple { get; set; }
        public decimal P_PROPINA { get; set; }

        public DateTime f_ped { get; set; }
        public string impor_ped { get; set; }
        //public decimal impor_ped { get; set; }
        public string desc_ped { get; set; }
        //public decimal desc_ped { get; set; }
        public string num_dia_ped { get; set; }
        public string subtotal_ped { get; set; }

        public string id_tip_desc { get; set; }
        public string nom_tip_desc { get; set; }

        public string total_ped { get; set; }
        //public decimal total_ped { get; set; }

        public string id_fpago { get; set; }
        public string nom_fpago { get; set; }

        public object id_estado_f { get; set; }
        public string nom_estado_f { get; set; }

        public string id_usu { get; set; }
        public string nom_usu { get; set; }

        public string id_turn { get; set; }
        public string nom_turn { get; set; }

        public string id_tipo_cambio { get; set; }
        public string nom_tipo_cambio { get; set; }

        public object nro_personas { get; set; }
        public string nro_tarjeta { get; set; }
        public string color_chips { get; set; }
        public object id_tipo_Doc { get; set; }
        public string nom_tipo_Doc { get; set; }
        public object importe_Total_Doc_Elec { get; set; }
        public string id_cliente { get; set; }
        public string propina { get; set; }
        public string TPpropina { get; set; }
        public string nomllevar { get; set; }
        public string telefcli { get; set; }
        public string PED_NOM_EQUIPO { get; set; }
        public List<cantidad> cant { get; set; }
        public decimal igv { get; set; }
        public decimal icbper { get; set; }
        public decimal totalOPGravadas { get; set; }
        public decimal rc { get; set; }

        #region Detalle Producto
        public int DProd_id_ped { get; set; }
        public object DProd_id_Det_ped { get; set; }
        public object DProd_pre_uni { get; set; }
        public int DProd_cant { get; set; }
        public string DProd_nom_carta { get; set; }
        public object DProd_monto { get; set; }
        public object DProd_fecha { get; set; }
        public object DProd_descuento { get; set; }
        public object DProd_cant_Eliminar { get; set; }

        public int id_carta { get; set; }
        public decimal CANTIDAD { get; set; }
        public string PLATO { get; set; }
        public string GRUPO { get; set; }
        public decimal DELIVERY { get; set; }
        public decimal LLEVAR { get; set; }
        public decimal SALON { get; set; }
        public decimal IMPORTE { get; set; }
        public decimal DESCUENTO { get; set; }
        public decimal TOTALINCDESCUENTO { get; set; }

        #endregion
        public class cantidad
        {
            public int ids { get; set; } = 0;
            public int id { get; set; }
            public int value { get; set; }
        }
    }
}

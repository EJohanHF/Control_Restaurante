using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Pedido
{
    public class Pedidos
    {
        public Pedidos()
        {

        }
        public int ID{ get; set; }
        public int DP_ID_PED { get; set; }
        public decimal DP_CANT { get; set; }
        public string DP_DESCRIP { get; set; }
        public string DP_IMPORT { get; set; }
        public int DP_ID_CARTA { get; set; }
        public string DP_EST { get; set; }
        public DateTime DP_FEC_REG { get; set; }
        public decimal PED_SUBTOTAL { get; set; }
        public decimal PED_DESCUENTO { get; set; }
        public decimal PED_TOTAL { get; set; }

        //pedido
        public int PED_ID_MESA { get; set; }
        public string M_NOM { get; set; }
        public string M_TEXTO { get; set; }
        public int PED_ID_EMPL { get; set; }
        public string EMPL_NOM { get; set; }
        public DateTime PED_FECH_PED { get; set; }
        public int PED_ID_ESTADO { get; set; }
        public decimal PED_IMPORTE { get; set; }
        //public decimal PED_DESCUENTO { get; set; }
        public int PED_ID_TIP_DESC { get; set; }
        //public decimal PED_TOTAL { get; set; }
        public int PED_ID_CLIENTE { get; set; }
        public string C_NOM { get; set; }
        public int PED_ID_CIERRE { get; set; }
        public int PED_ID_USU { get; set; }
        public string USU_NOM { get; set; }
        public int PED_ID_TURNO { get; set; }
        public DateTime PED_FECH_MODIFI { get; set; }
        public int PED_ID_CAMBIO_MONE { get; set; }
        public decimal TC_CAMBIO { get; set; }
        public int PED_NUM_DIARIO { get; set; }
        public int PED_NRO_PERSONAS { get; set; }
        public string MOTORIZADO { get; set; }
        //public decimal PED_SUBTOTAL { get; set; }

        //DETALLE PEDIDO
        public int DP_ID { get; set; }
        public string CAR_NOM { get; set; }
        public decimal DP_DESCU { get; set; }
        public int DP_ID_EMPL { get; set; }
        public DateTime DP_FEC_MODIFI { get; set; }
        public int DP_USU_MODIFI { get; set; }
        public int DP_IMP { get; set; }
        public string DP_COMENTARIO { get; set; }
        public int IDCAT { get; set; }
        public int IDGRUP { get; set; }
        public int IDSCAT { get; set; }
        public decimal DP_PRE_UNI { get; set; }
        public string GR_NOM { get; set; }
        public string CAT_NOM { get; set; }
        public string DETEMPL { get; set; }
        public int idPadreDetalle { get; set; }
        public string ID_CARTA_SN { get; set; }
        public bool EST_ENTREGADO { get; set; }
        //RECURRENCIA

        public DateTime PRIMER_CONSUMO { get; set; }
        public DateTime ULTIMO_CONSUMO { get; set; }
        public decimal IMPORTE_CONSUMO { get; set; }
        public int CANTIDAD_CONSUMO { get; set; }
        public int ID_CLIENTE{ get; set; }
        public string NOMBRE_CLIENTE{ get; set; }
        public string nomllevar { get; set; }
        public string telllevar { get; set; }
        public string DP_ID_CARTA_SN { get; set; }
        public string ColorPedido { get; set; }

        public decimal cantidad_carta_subnivel { get; set; }
        public string id_carta_subnivel { get; set; }
        public string isCuenta { get; set; }
    }
}

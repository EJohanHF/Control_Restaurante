using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Stock
{
    public class MovimientoInsumoAlmacen
    {
        public MovimientoInsumoAlmacen()
        {
        }
        public int ID { get; set; }
        public int MI_ID_INS { get; set; }
        public string INS_NOM { get; set; }
        public string TM_DESC { get; set; }
        public decimal MI_CANT { get; set; }
        public decimal INS_PRECIO { get; set; }
        public int MI_ID_USU { get; set; }
        public string USU_NOM { get; set; }
        public string MI_DESCR { get; set; }
        public DateTime MI_F_CREATE { get; set; }
        public int MI_ID_ALM { get; set; }
        public string ALM_NOM { get; set; }
        public int MI_TIP_MOV { get; set; }
        public string MOV_DESCR { get; set; }
        public decimal ENTRADA { get; set; }
        public string EntradaForeColor { get; set; }
        public decimal SALIDA { get; set; }
        public string Salida_texto { get; set; }
        public decimal DEVOLUCION { get; set; }
        public decimal Devolucion_texto { get; set; }
        public string SalidaForeColor { get; set; }
        public decimal STOCK { get; set; }
        public string StockForeColor { get; set; }



        //CIERRE INSUMO
        public int CI_ID { get; set; }
        public int CI_ID_INS { get; set; }
        public decimal CI_CANT { get; set; }
        public int CI_ID_USU { get; set; }
        public string CI_USU_NOM { get; set; }
        public DateTime  CI_F_CREATE { get; set; }
        public int CI_ID_CIERRE { get; set; }
        public int CI_ID_ALM { get; set; }
        public string CI_ALM_NOM { get; set; }
    }
}

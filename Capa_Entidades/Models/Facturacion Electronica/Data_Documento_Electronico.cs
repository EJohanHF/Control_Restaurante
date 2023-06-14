using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Facturacion_Electronica
{
    public class Data_Documento_Electronico
    {
        public int orden { get; set; }
        public int ID { get; set; }
        public int ID_PEDIDO { get; set; }
        public decimal CANTIDAD { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal PRECIO_UNI { get; set; }
        public decimal DESCUENTO { get; set; }
        public decimal IMPORTE { get; set; }
        public decimal SUB_TOTAL { get; set; }
        public decimal DESCUENTO_TOTAL { get; set; }
        public decimal TOTAL { get; set; }
        public string CLASIFICACION_PRODUCTO_ITEM { get; set; }
        public string DP_ID_CARTA { get; set; }
        public string TM_DESC { get; set; }
        public decimal CANT_DOC { get; set; }
        public byte RC { get; set; } = 0;
        public byte EXONERADA { get; set; } = 0;
        public string numeroDocIdentidadEmisor { get; set; }
        public string tipoDocumento { get; set; }
        public string serieNumero { get; set; }
    }
}

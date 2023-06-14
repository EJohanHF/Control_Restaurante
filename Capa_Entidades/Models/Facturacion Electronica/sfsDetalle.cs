using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Facturacion_Electronica
{
    public class sfsDetalle
    {
        public int idDetalle { get; set; }
        public int idCabecera { get; set; }
        public string codUnidadMedida { get; set; }
        public decimal ctdUnidadItem { get; set; }
        public string codProducto { get; set; }
        public string codProductoSUNAT { get; set; }
        public string desItem { get; set; }
        public decimal mtoValorUnitario { get; set; }
        public decimal sumTotTributosItem { get; set; }
        public string codTriIGV { get; set; }
        public decimal mtoIgvItem { get; set; }
        public decimal mtoBaseIgvItem { get; set; }
        public string nomTributoIgvItem { get; set; }
        public string codTipTributoIgvItem { get; set; }
        public string tipAfeIGV { get; set; }
        public decimal porIgvItem { get; set; }
        public string codTriISC { get; set; }
        public decimal mtoIscItem { get; set; }
        public string mtoBaseIscItem { get; set; }
        public string nomTributoIscItem { get; set; }
        public string codTipTributoIscItem { get; set; }
        public string tipSisISC { get; set; }
        public decimal porIscItem { get; set; }
        public string codTriOtroItem { get; set; }
        public decimal mtoTriOtroItem { get; set; }
        public decimal mtoBaseTriOtroItem { get; set; }
        public string nomTributoIOtroItem { get; set; }
        public string codTipTributoIOtroItem { get; set; }
        public string porTriOtroItem { get; set; }
        public string codTriIcbper { get; set; }
        public decimal mtoTriIcbperItem { get; set; }
        public decimal ctdBolsasTriIcbperItem { get; set; }
        public string nomTributoIcbperItem { get; set; }
        public string codTipTributoIcbperItem { get; set; }
        public decimal mtoTriIcbperUnidad { get; set; }
        public decimal mtoPrecioVentaUnitario { get; set; }
        public decimal mtoValorVentaItem { get; set; }
        public decimal mtoValorReferencialUnitario { get; set; }
    }
}

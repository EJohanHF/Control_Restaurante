using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.DetalleporDia
{
public class CierreParcial
    {
        #region PLATOS ANULADOS DIA
        public string descipcion { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public string comentario { get; set; }
        #endregion


        #region REPORTE VENTAS DIA
        public decimal _saldo_caja { get; set; }
        public decimal _ingreso_caja { get; set; }
        public decimal _egreso_caja { get; set; }
        public decimal _visa { get; set; }
        public decimal _efectivo { get; set; }
        public decimal _mastercard { get; set; }
        public decimal _descuento { get; set; }
        public decimal _subtotal { get; set; }
        public decimal _total { get; set; }
        #endregion

        #region REPORTE DOCUMENTO ELECTRONICO
        public decimal DE_nbol { get; set; }
        public decimal DE_nfac { get; set; }
        public decimal DE_montobol { get; set; }
        public decimal DE_montofact { get; set; }
        public decimal DE_totalbolfact { get; set; }
        public decimal DE_totalsinboleta { get; set; }
        public decimal DE_nbol_anulado { get; set; }
        public decimal DE_nfac_anulado { get; set; }
        public decimal DE_montobol_anulado { get; set; }
        public decimal DE_montofact_anulado { get; set; }
        public decimal DE_totalbolfact_anulado { get; set; }
        //public string nom_emple { get; set; }
        #endregion
    }
}

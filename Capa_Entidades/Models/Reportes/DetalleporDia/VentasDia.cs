using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reportes.DetalleporDia
{
  public class VentasDia
    {
        #region Ventas card
        public decimal monto { get; set; }
        public object desc { get; set; }
        public string dias { get; set; }

        #endregion
        #region Clientes card
        public string empresa { get; set; }
        public string persona { get; set; }
        public string total_cli { get; set; }
        public object mont_emp { get; set; }
        public object mont_pers { get; set; }
        #endregion
        #region Promedio por mesa
        public string PM_promedio { get; set; }
        public object PM_nventas { get; set; }
        #endregion
        #region Tipo Pago
        public object TP_id { get; set; }
        public object TP_monto { get; set; }
        public string TP_deno { get; set; }
        #endregion
        #region MOZO Record
        public object tpedido { get; set; }
        public string mozo { get; set; }
        public object porcentaje { get; set; }
        #endregion
        #region Mesas Atendidas
        public object MA_nmesas { get; set; }
        public object MA_promedio { get; set; }
        public object MA_totalmesas { get; set; }
        #endregion
        #region CajaChica
        public object Cc_ingreso { get; set; }
        public object Cc_egreso { get; set; }
        public object Cc_total { get; set; }
        #endregion
        #region VentasAnulados
        public string VA_anulados { get; set; }
        public string VA_porcentaje { get; set; }
        public string VA_pedidos { get; set; }
        #endregion
        #region DocElectronicos
        public int DC_tipo { get; set; }
        public object DC_docelect { get; set; }
        public object DC_Monto { get; set; }


        public int id_doc_electronico { get; set; }
        public string serieNumero { get; set; }
        public string tipoDocumento { get; set; }
        public DateTime fechaEmision { get; set; }
        public string numeroDocIdentidadEmisor { get; set; }
        public string tipoDocIdentidadEmisor { get; set; }
        public string numeroDocIdentidadReceptor { get; set; }
        public string razonSocialReceptor { get; set; }
        public string direccionReceptor { get; set; }
        public string correoReceptor { get; set; }
        public string tipoDocIdentidadReceptor { get; set; }
        public string telefono { get; set; }
        public int idCliente { get; set; }
        public decimal totalOPGravadas { get; set; }
        public decimal totalOPExoneradas { get; set; }
        public decimal totalOPNoGravadas { get; set; }
        public decimal totalOPGratuitas { get; set; }
        public decimal sumatoriaIGV { get; set; }
        public decimal sumatoriaISC { get; set; }
        public decimal importeTotal { get; set; }
        public decimal importeTotalVenta { get; set; }
        public decimal totalDescuentos { get; set; }
        public decimal descuentosGlobales { get; set; }
        public decimal sumatoriaOtrosCargos { get; set; }
        public decimal porcentajeOtrosCargos { get; set; }
        public decimal sumatoriaImpuestoBolsas { get; set; }
        public string tipoMoneda { get; set; }
        public string codigoPaisReceptor { get; set; }
        public string codigoTipoOperacion { get; set; }
        public string montoEnLetras { get; set; }
        public int idPedido { get; set; }
        public bool Doc_Estado { get; set; }
        public int Doc_id_cierre { get; set; }
        #endregion

        #region cierrecaja
        public string CJ_id_usu { get; set; }
        #endregion

        #region Frecuenci de atencion
        public object FA_min { get; set; }
        public object FA_max { get; set; }
        #endregion

        #region VentasDiavsVSAnterior
        public object VDVA_monto_SA { get; set; }
        public object VDVA_monto_AC { get; set; }
        #endregion
        #region Ventas por HOra
        public object VPH_Hora { get; set; }
        public object VPH_Monto { get; set; }
        #endregion
        #region det  Ventas Anulados
        public object VA_id { get; set; }
        public object VA_desc { get; set; }
        public object VA_monto { get; set; }
        #endregion
        #region det prod anulados
        public object PA_id { get; set; }
        public object PA_Desc { get; set; }
        public object PA_Monto { get; set; }
        #endregion

        #region CanalVentaConsolidado
        public int cantidad_venta { get; set; }
        public decimal importe_venta { get; set; }
        public string canal_venta { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos.Data.Reportes;
using Capa_Entidades.Models.Reportes;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio.Funciones_Globales;

namespace Capa_Negocio.Reportes.VentasporDia
{
  public class Neg_VentasDia
    {
        Dat_VentasDia datventasDia = new Dat_VentasDia();
        Funcion_Global globales = new Funcion_Global();
        #region GetVentasDia
        public List<VentasDia> GetVentasDia_Venta(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_Venta(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            monto = Convert.ToDecimal(dr["Monto"]),
                            desc = dr["Descuento"].ToString(),
                            dias = dr["Dias"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetVentasDia_cliente
        public List<VentasDia> GetVentasDia_cliente(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_cliente(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            total_cli = dr["IDCLI"].ToString(),
                            empresa = dr["EMPRESAS"].ToString(),
                            persona = dr["PERSONAS"].ToString(),
                            mont_emp = dr["totalEmp"].ToString(),
                            mont_pers = dr["totalPer"].ToString(),

                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetVentasDia_promedio por mesa
        public List<VentasDia> GetVentasDia_promedio_mesa(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_promedio(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            PM_promedio = dr["PROMEDIO"].ToString(),
                            PM_nventas = dr["NVENTAS"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetVentasDia_promedio_mesa2(int idCierre)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_promedio2(idCierre);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            PM_promedio = dr["PROMEDIO"].ToString(),
                            PM_nventas = dr["NVENTAS"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region record mozo
        public List<VentasDia> GetVentasDia_Recordmozo(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_Recordmozo(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            tpedido = dr["TOTAL_PEDIDO"].ToString(),
                            mozo = dr["MOZO"].ToString(),
                            porcentaje = Math.Round(Convert.ToDecimal(dr["PORCENTAJE"]),2)
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region tipo  pago
        public List<VentasDia> GetVentasDia_TipoPago(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_TipoPago(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            TP_id = dr["IDPAGO"].ToString(),
                            TP_monto = dr["MONTO"].ToString(),
                            TP_deno = dr["DENOPAGO"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region ventas dia mesas
        public List<VentasDia> GetMesasAtendidas(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetMesasAtendidas(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            MA_nmesas = dr["NMESAS"].ToString(),
                            MA_promedio = dr["PROMEDIO"].ToString(),
                            MA_totalmesas = dr["TOTALMESAS"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }

        #endregion
        #region Caja chica
        public List<VentasDia> GetCajaChica(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetCajaChica(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            Cc_ingreso = Math.Round(Convert.ToDecimal(dr["INGRESO"]),2),
                            Cc_egreso = Math.Round(Convert.ToDecimal(dr["EGRESO"]),2),
                            Cc_total = Math.Round(Convert.ToDecimal(dr["TOTAL"]),2),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region Ventas Anulados
        public List<VentasDia> GetVentasAnulados(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentasAnulados(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            VA_anulados= dr["anulados"].ToString(),
                            VA_porcentaje = dr["porcentaje"].ToString(),
                            //VA_pedidos = dr["totalped"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion 
        #region Documento electronico
        public List<VentasDia> GetDocElectronico(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetDocElectronico(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            DC_tipo = Convert.ToInt32(dr["tipoDocumento"]),
                            DC_Monto = dr["importeTotalVenta"].ToString(),
                            DC_docelect = dr["id_doc_electronico"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetDocElectronicoAnu(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetDocElectronicoAnu(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            DC_tipo = Convert.ToInt32(dr["tipoDocumento"]),
                            DC_Monto = dr["importeTotalVenta"].ToString(),
                            DC_docelect = dr["id_doc_electronico"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetDocElectronicoTODOS(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetDocElectronicoTODOS(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            id_doc_electronico = Convert.ToInt32(dr["id_doc_electronico"]),
                            serieNumero = dr["serieNumero"].ToString(),
                            tipoDocumento = dr["tipoDocumento"].ToString(),
                            fechaEmision = Convert.ToDateTime(dr["fechaEmision"]),
                            numeroDocIdentidadEmisor = dr["numeroDocIdentidadEmisor"].ToString(),
                            tipoDocIdentidadEmisor = dr["tipoDocIdentidadEmisor"].ToString(),
                            numeroDocIdentidadReceptor = dr["numeroDocIdentidadReceptor"].ToString(),
                            razonSocialReceptor = dr["razonSocialReceptor"].ToString(),
                            direccionReceptor = dr["direccionReceptor"].ToString(),
                            correoReceptor = dr["correoReceptor"].ToString(),
                            tipoDocIdentidadReceptor = dr["tipoDocIdentidadReceptor"].ToString(),
                            telefono = dr["telefono"].ToString(),
                            idCliente = Convert.ToInt32(dr["idCliente"]),
                            totalOPGravadas = Convert.ToInt32(dr["totalOPGravadas"]),
                            totalOPExoneradas = Convert.ToInt32(dr["totalOPExoneradas"]),
                            totalOPNoGravadas = Convert.ToInt32(dr["totalOPNoGravadas"]),
                            totalOPGratuitas = Convert.ToInt32(dr["totalOPGratuitas"]),
                            sumatoriaIGV = Convert.ToInt32(dr["sumatoriaIGV"]),
                            sumatoriaISC = Convert.ToInt32(dr["sumatoriaISC"]),
                            importeTotal = Convert.ToInt32(dr["importeTotal"]),
                            importeTotalVenta = Convert.ToInt32(dr["importeTotalVenta"]),
                            totalDescuentos = Convert.ToInt32(dr["totalDescuentos"]),
                            descuentosGlobales = Convert.ToInt32(dr["descuentosGlobales"]),
                            sumatoriaOtrosCargos = Convert.ToInt32(dr["sumatoriaOtrosCargos"]),
                            porcentajeOtrosCargos = Convert.ToInt32(dr["porcentajeOtrosCargos"]),
                            sumatoriaImpuestoBolsas = Convert.ToInt32(dr["sumatoriaImpuestoBolsas"]),
                            tipoMoneda = dr["tipoMoneda"].ToString(),
                            codigoPaisReceptor = dr["codigoPaisReceptor"].ToString(),
                            codigoTipoOperacion = dr["codigoTipoOperacion"].ToString(),
                            montoEnLetras = dr["montoEnLetras"].ToString(),
                            idPedido = Convert.ToInt32(dr["idPedido"]),
                            Doc_Estado = Convert.ToBoolean(dr["Doc_Estado"]),
                            Doc_id_cierre = Convert.ToInt32(dr["Doc_id_cierre"]),

                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region Fecuencia de atencion
        public List<VentasDia> GetFrecAtencion(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetFrecAtencion(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            FA_min = dr["MINIMO"].ToString(),
                            FA_max = dr["MAXIMO"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region Dashboar VentasDaisvsVEntasAnteriordemana
        public List<VentasDia> GetVDiavsVSanterior(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVDiavsVSanterior(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            VDVA_monto_SA = Math.Round(Convert.ToDecimal(dr["MONTOPASADO"]),2),
                            VDVA_monto_AC = Math.Round(Convert.ToDecimal(dr["MONTOACTUAL"]), 2)
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region Dashboar VentasPor Hora
        public List<VentasDia> GetVentasPorHora(string NombreEquipo)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentasPorHora(NombreEquipo);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            VPH_Hora = dr["HORA"].ToString(),
                            VPH_Monto = Math.Round(Convert.ToDecimal(dr["MONTO"]), 2)
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region tipo  pago 2
        public List<VentasDia> GetVentasDia_TipoPago2()
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_TipoPago2();
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            TP_id = dr["IDPAGO"].ToString(),
                            TP_monto = dr["MONTO"].ToString(),
                            TP_deno = dr["DENOPAGO"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetVentasDia_TipoPago3(DateTime desde, DateTime hasta)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_TipoPago3(desde,hasta);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            TP_id = dr["IDPAGO"].ToString(),
                            TP_monto = dr["MONTO"].ToString(),
                            TP_deno = dr["DENOPAGO"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region DEtalle productos Anulados
        public List<VentasDia> GetDetalleProdAnulados()
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_Det_Prod_Anulado();
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            
                            PA_id = dr["ID"].ToString(),
                            PA_Desc = dr["DP_DESCRIP"].ToString(),
                            PA_Monto = dr["DP_IMPORT"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region detalle Ventas anulados
        public List<VentasDia> GetDetalleVentasAnulados()
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_Det_Venta_Anulado();
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            VA_id = dr["ID"].ToString(),
                            VA_desc = dr["M_NOM"].ToString(),
                            VA_monto = dr["PED_TOTAL"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region Historial
        public List<VentasDia> GetVentasDia_Venta_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_Venta_HISTORIAL(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            monto = Convert.ToDecimal(dr["Monto"]),
                            desc = dr["Descuento"].ToString(),
                            dias = dr["Dias"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetVentasDia_cliente_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_cliente_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            total_cli = dr["IDCLI"].ToString(),
                            empresa = dr["EMPRESAS"].ToString(),
                            persona = dr["PERSONAS"].ToString(),
                            mont_emp = dr["totalEmp"].ToString(),
                            mont_pers = dr["totalPer"].ToString(),

                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetVentasDia_promedio_mesa_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_promedio_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            PM_promedio = dr["PROMEDIO"].ToString(),
                            PM_nventas = dr["NVENTAS"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetVentasDia_TipoPago_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_TipoPago_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            TP_id = dr["IDPAGO"].ToString(),
                            TP_monto = dr["MONTO"].ToString(),
                            TP_deno = dr["DENOPAGO"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetVentasDia_Recordmozo_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_Recordmozo_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            tpedido = dr["TOTAL_PEDIDO"].ToString(),
                            mozo = dr["MOZO"].ToString(),
                            porcentaje = Math.Round(Convert.ToDecimal(dr["PORCENTAJE"]), 2)
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetMesasAtendidas_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetMesasAtendidas_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            MA_nmesas = dr["NMESAS"].ToString(),
                            MA_promedio = dr["PROMEDIO"].ToString(),
                            MA_totalmesas = dr["TOTALMESAS"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetCajaChica_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetCajaChica_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            Cc_ingreso = Math.Round(Convert.ToDecimal(dr["INGRESO"]), 2),
                            Cc_egreso = Math.Round(Convert.ToDecimal(dr["EGRESO"]), 2),
                            Cc_total = Math.Round(Convert.ToDecimal(dr["TOTAL"]), 2),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetVentasAnulados_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentasAnulados_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            VA_anulados = dr["anulados"].ToString(),
                            VA_porcentaje = dr["porcentaje"].ToString(),
                            //VA_pedidos = dr["totalped"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetDocElectronico_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetDocElectronico_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            DC_tipo = Convert.ToInt32(dr["tipoDocumento"]),
                            DC_Monto = dr["importeTotalVenta"].ToString(),
                            DC_docelect = dr["id_doc_electronico"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetDocElectronicoAnu_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetDocElectronicoAnu_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            DC_tipo = Convert.ToInt32(dr["tipoDocumento"]),
                            DC_Monto = dr["importeTotalVenta"].ToString(),
                            DC_docelect = dr["id_doc_electronico"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetDocElectronicoTODOS_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetDocElectronicoTODOS_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            id_doc_electronico = Convert.ToInt32(dr["id_doc_electronico"]),
                            serieNumero = dr["serieNumero"].ToString(),
                            tipoDocumento = dr["tipoDocumento"].ToString(),
                            fechaEmision = Convert.ToDateTime(dr["fechaEmision"]),
                            numeroDocIdentidadEmisor = dr["numeroDocIdentidadEmisor"].ToString(),
                            tipoDocIdentidadEmisor = dr["tipoDocIdentidadEmisor"].ToString(),
                            numeroDocIdentidadReceptor = dr["numeroDocIdentidadReceptor"].ToString(),
                            razonSocialReceptor = dr["razonSocialReceptor"].ToString(),
                            direccionReceptor = dr["direccionReceptor"].ToString(),
                            correoReceptor = dr["correoReceptor"].ToString(),
                            tipoDocIdentidadReceptor = dr["tipoDocIdentidadReceptor"].ToString(),
                            telefono = dr["telefono"].ToString(),
                            idCliente = Convert.ToInt32(dr["idCliente"]),
                            totalOPGravadas = Convert.ToInt32(dr["totalOPGravadas"]),
                            totalOPExoneradas = Convert.ToInt32(dr["totalOPExoneradas"]),
                            totalOPNoGravadas = Convert.ToInt32(dr["totalOPNoGravadas"]),
                            totalOPGratuitas = Convert.ToInt32(dr["totalOPGratuitas"]),
                            sumatoriaIGV = Convert.ToInt32(dr["sumatoriaIGV"]),
                            sumatoriaISC = Convert.ToInt32(dr["sumatoriaISC"]),
                            importeTotal = Convert.ToInt32(dr["importeTotal"]),
                            importeTotalVenta = Convert.ToInt32(dr["importeTotalVenta"]),
                            totalDescuentos = Convert.ToInt32(dr["totalDescuentos"]),
                            descuentosGlobales = Convert.ToInt32(dr["descuentosGlobales"]),
                            sumatoriaOtrosCargos = Convert.ToInt32(dr["sumatoriaOtrosCargos"]),
                            porcentajeOtrosCargos = Convert.ToInt32(dr["porcentajeOtrosCargos"]),
                            sumatoriaImpuestoBolsas = Convert.ToInt32(dr["sumatoriaImpuestoBolsas"]),
                            tipoMoneda = dr["tipoMoneda"].ToString(),
                            codigoPaisReceptor = dr["codigoPaisReceptor"].ToString(),
                            codigoTipoOperacion = dr["codigoTipoOperacion"].ToString(),
                            montoEnLetras = dr["montoEnLetras"].ToString(),
                            idPedido = Convert.ToInt32(dr["idPedido"]),
                            Doc_Estado = Convert.ToBoolean(dr["Doc_Estado"]),
                            Doc_id_cierre = Convert.ToInt32(dr["Doc_id_cierre"]),

                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetFrecAtencion_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetFrecAtencion_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            FA_min = dr["MINIMO"].ToString(),
                            FA_max = dr["MAXIMO"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetVDiavsVSanterior_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVDiavsVSanterior_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            VDVA_monto_SA = Math.Round(Convert.ToDecimal(dr["MONTOPASADO"]), 2),
                            VDVA_monto_AC = Math.Round(Convert.ToDecimal(dr["MONTOACTUAL"]), 2)
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetVentasPorHora_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentasPorHora_Historial(desde, hasta, dia);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            VPH_Hora = dr["HORA"].ToString(),
                            VPH_Monto = Math.Round(Convert.ToDecimal(dr["MONTO"]), 2)
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetDetalleProdAnulados_Historial(string desde, string hasta)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_Det_Prod_Anulado_Historial(desde, hasta);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {

                            PA_id = dr["ID"].ToString(),
                            PA_Desc = dr["DP_DESCRIP"].ToString(),
                            PA_Monto = dr["DP_IMPORT"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public List<VentasDia> GetDetalleVentasAnulados_Historial(string desde, string hasta)
        {
            List<VentasDia> menu = new List<VentasDia>();
            try
            {
                DataTable dt = new DataTable();
                dt = datventasDia.GetVentaDia_Det_Venta_Anulado_Historial(desde, hasta);
                menu = (from DataRow dr in dt.Rows
                        select new VentasDia()
                        {
                            VA_id = dr["ID"].ToString(),
                            VA_desc = dr["M_NOM"].ToString(),
                            VA_monto = dr["PED_TOTAL"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        public DataTable GetPropinas(string nomequipo)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = datventasDia.GetPropinas(nomequipo);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetPropinasHistorial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = datventasDia.GetPropinasHistorial(desde, hasta, dia);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetPropinasPago(string nomequipo)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = datventasDia.GetPropinasPago(nomequipo);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        #endregion

    }
}

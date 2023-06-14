using Capa_Datos.Data.Reportes.VentasPorDia;
using Capa_Entidades.Models.Reportes;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Entidades.Models.Reportes.Pedido;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Reportes.VentasporDia
{
    public class Neg_Detalle_pedido
    {
        Dat_Detalle_Pedido datDetPed = new Dat_Detalle_Pedido();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Detalle_Pedido> GetDetallepedido(string NombreEquipo)
        {
            ObservableCollection<Detalle_Pedido> detallepedido = new ObservableCollection<Detalle_Pedido>();
            try
            {
                DataTable dt = datDetPed.GetDetallePedido(NombreEquipo);
                foreach (DataRow row in dt.Rows)
                {
                    Detalle_Pedido _detPed = new Detalle_Pedido();
                    _detPed.id_ped = Convert.ToInt32(row["ID"]);
                    _detPed.num_dia_ped = row["PED_NUM_DIARIO"].ToString();

                    _detPed.id_mesa = row["IDMESA"].ToString();
                    _detPed.nom_mesa = row["M_NOM"].ToString();
                    _detPed.id_cliente = row["PED_ID_CLIENTE"].ToString();
                    _detPed.id_emple = row["IDEMP"].ToString();
                    _detPed.nom_emple = row["EMPL_NOM"].ToString();
                    _detPed.P_PROPINA = Convert.ToDecimal(row["P_PROPINA"]);

                    _detPed.subtotal_ped = row["PED_SUBTOTAL"].ToString();
                    _detPed.f_ped = Convert.ToDateTime(row["PED_FECH_PED"].ToString());
                    _detPed.impor_ped = row["PED_IMPORTE"].ToString();
                    //_detPed.impor_ped = Convert.ToDecimal(row["PED_IMPORTE"]);
                    _detPed.desc_ped = row["PED_DESCUENTO"].ToString();
                    //_detPed.desc_ped = Convert.ToDecimal(row["PED_DESCUENTO"]);

                    _detPed.id_tip_desc = row["IDTDESC"].ToString();
                    _detPed.nom_tip_desc = row["TD_DESCR"].ToString();

                    _detPed.id_estado_f = row["PED_ID_ESTADO"].ToString();
                    _detPed.nom_estado_f = row["DESC_EST"].ToString();

                    _detPed.total_ped = row["PED_TOTAL"].ToString();
                    //_detPed.total_ped = Convert.ToDecimal(row["PED_TOTAL"]);

                    _detPed.id_fpago = row["IDFPAGO"].ToString();
                    _detPed.nom_fpago = row["DENO_PAGO"].ToString();
                    _detPed.TPpropina = row["TPPROPINA"].ToString();

                    _detPed.id_usu = row["IDUSU"].ToString();
                    _detPed.nom_usu = row["USU_NOM"].ToString();
                    _detPed.nro_personas = row["PED_NRO_PERSONAS"].ToString();
                    _detPed.nro_tarjeta = row["DT_NRO_TARJETA"].ToString();
                    _detPed.nom_tipo_Doc = row["tipoDocumento"].ToString();
                    _detPed.importe_Total_Doc_Elec = row["importeTotal"].ToString();
                    _detPed.nomllevar = row["nomllevar"].ToString();
                    _detPed.telefcli = row["telefcli"].ToString();
                    _detPed.PED_NOM_EQUIPO = row["PED_NOM_EQUIPO"].ToString();
                    /*cabiar color del estado en la grilla según estado*/
                    if (Convert.ToInt32(_detPed.id_estado_f) == 1)
                    {
                        _detPed.color_chips = "#26A001";
                    }
                    else if (Convert.ToInt32(_detPed.id_estado_f) == 2)
                    {
                        _detPed.color_chips = "#0371C2";
                    }
                    else if (Convert.ToInt32(_detPed.id_estado_f) == 3)
                    {
                        _detPed.color_chips = "#D73506";
                    }
                    
                    /* tipo de documento */
                    //if (Convert.ToInt32(_detPed.id_tipo_Doc) == 01)
                    //{
                    //    _detPed.nom_tipo_Doc = "FACTURA ELECTRONICA";
                    //}
                    //else if (Convert.ToInt32(_detPed.id_tipo_Doc) == 03)
                    //{
                    //    _detPed.nom_tipo_Doc = "BOLETA DE VENTA ELECTRONICA";
                    //}
                    detallepedido.Add(_detPed);
                }
            }
            catch (Exception ex)
            {
                detallepedido = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return detallepedido;
        }
        public DataTable GetCuentaPedido(int  id_pedido)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetCuentaxPedido(id_pedido);
            }
            catch (Exception ex)
            {
                dt = null;
                ////globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetCuentaxPedidototal(int id_pedido)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetCuentaxPedidototal(id_pedido);
            }
            catch (Exception ex)
            {
                dt = null;
                ////globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetCargarPlatosSelect()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetCargarPLatosSelect();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetComandaAnuladaPedido(int id_pedido)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetComandaAnuladaxPedido(id_pedido);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetComandaPlatoAnuladaPedido(int id_det_pedido)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetComandaPlatoAnuladaxPedido(id_det_pedido);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public int SetCierreCaja(int operacion, VentasDia ventadia, ref string _mensaje, string NombreEquipo)
        {
            /*aqui se hacen las validaciones de los campos antes de enviarlos al metodo */
            int result = 0;
            result = datDetPed.SetCierreCaja(operacion, ventadia, ref _mensaje, NombreEquipo);
            
            return result;
        }

        #region Dashboard torta
        public DataTable GetPlatoxArea(string NombreEquipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetPlatoxArea(NombreEquipo);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        public DataTable GetPlatoxArea_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetPlatoxArea_Historial(desde, hasta, dia);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        
        public DataTable GetVentavsCosto(string NombreEquipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetVentavsCosto(NombreEquipo);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable GetCanaldeVenta(string NombreEquipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetCanaldeVenta(NombreEquipo);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable GetCanaldeVenta_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetCanaldeVenta_Historial(desde, hasta, dia);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        #endregion

        #region Detalle Producto
        public class cantidad
        {
            public int id { get; set; }
            public int value { get; set; }
        }
        public ObservableCollection<Detalle_Pedido> GetDetProducto( string idcli)
        {
            ObservableCollection<Detalle_Pedido> detProd = new ObservableCollection<Detalle_Pedido>();
            try
            {
                DataTable dt = datDetPed.GetDetProducto(idcli);
                foreach (DataRow row in dt.Rows)
                {
                    Detalle_Pedido _detProd = new Detalle_Pedido();
                    _detProd.DProd_id_ped = Convert.ToInt32(row["IDPED"]);
                    _detProd.DProd_id_Det_ped = row["iddetped"].ToString();
                    _detProd.DProd_cant = Convert.ToInt32(row["DP_CANT"]);
                    _detProd.DProd_nom_carta = row["DP_DESCRIP"].ToString();
                    _detProd.DProd_pre_uni = row["DP_PRE_UNI"].ToString();
                    _detProd.DProd_monto = row["DP_IMPORT"].ToString();
                    _detProd.DProd_fecha = row["DP_FEC_REG"].ToString();
                    _detProd.DProd_descuento = Convert.ToDecimal(row["DP_DESCU"]);
                    _detProd.cant = new List<Detalle_Pedido.cantidad>();
                    for (int j = 1; j <= _detProd.DProd_cant; j++)
                    {
                        Detalle_Pedido.cantidad combo = new Detalle_Pedido.cantidad();
                        combo.id = j;
                        combo.value = j;
                        _detProd.cant.Add(combo);
                    }

                    detProd.Add(_detProd);
                }
            }
            catch (Exception)
            {
                detProd = null;
            }
            return detProd;
        }

        #endregion
        public DataTable GetPlatosVendidosxImpresora(string desde, string hasta, string dia, int id_impre)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetPlatosVendidosxImpresora(desde,hasta,dia,id_impre);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetCanaldeVentaConsolidado(string NombreEquipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetCanaldeVentaConsolidado(NombreEquipo);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetCanaldeVentaConsolidado_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDetPed.GetCanaldeVentaConsolidado_Historial(desde, hasta, dia);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public ObservableCollection<Detalle_Pedido> GetDetallepedido_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            ObservableCollection<Detalle_Pedido> detallepedido = new ObservableCollection<Detalle_Pedido>();
            try
            {
                DataTable dt = datDetPed.GetDetallePedido_Historial(desde, hasta, dia);
                foreach (DataRow row in dt.Rows)
                {
                    Detalle_Pedido _detPed = new Detalle_Pedido();
                    _detPed.id_ped = Convert.ToInt32(row["ID"]);
                    _detPed.num_dia_ped = row["PED_NUM_DIARIO"].ToString();

                    _detPed.id_mesa = row["IDMESA"].ToString();
                    _detPed.nom_mesa = row["M_NOM"].ToString();
                    _detPed.id_cliente = row["PED_ID_CLIENTE"].ToString();
                    _detPed.id_emple = row["IDEMP"].ToString();
                    _detPed.nom_emple = row["EMPL_NOM"].ToString();

                    _detPed.subtotal_ped = row["PED_SUBTOTAL"].ToString();
                    _detPed.f_ped = Convert.ToDateTime(row["PED_FECH_PED"].ToString());
                    _detPed.impor_ped = row["PED_IMPORTE"].ToString();
                    //_detPed.impor_ped = Convert.ToDecimal(row["PED_IMPORTE"]);
                    _detPed.desc_ped = row["PED_DESCUENTO"].ToString();
                    //_detPed.desc_ped = Convert.ToDecimal(row["PED_DESCUENTO"]);

                    _detPed.id_tip_desc = row["IDTDESC"].ToString();
                    _detPed.nom_tip_desc = row["TD_DESCR"].ToString();

                    _detPed.id_estado_f = row["PED_ID_ESTADO"].ToString();
                    _detPed.nom_estado_f = row["DESC_EST"].ToString();

                    _detPed.total_ped = row["PED_TOTAL"].ToString();
                    //_detPed.total_ped = Convert.ToDecimal(row["PED_TOTAL"]);

                    _detPed.id_fpago = row["IDFPAGO"].ToString();
                    _detPed.nom_fpago = row["DENO_PAGO"].ToString();
                    _detPed.propina = row["P_PROPINA"].ToString();
                    _detPed.TPpropina = row["TPPROPINA"].ToString();

                    _detPed.id_usu = row["IDUSU"].ToString();
                    _detPed.nom_usu = row["USU_NOM"].ToString();
                    _detPed.nro_personas = row["PED_NRO_PERSONAS"].ToString();
                    _detPed.nro_tarjeta = row["DT_NRO_TARJETA"].ToString();
                    _detPed.nom_tipo_Doc = row["tipoDocumento"].ToString();
                    _detPed.importe_Total_Doc_Elec = row["importeTotal"].ToString();
                    _detPed.nomllevar = row["nomllevar"].ToString();
                    _detPed.telefcli = row["telefcli"].ToString();
                    /*cabiar color del estado en la grilla según estado*/
                    if (Convert.ToInt32(_detPed.id_estado_f) == 1)
                    {
                        _detPed.color_chips = "#26A001";
                    }
                    else if (Convert.ToInt32(_detPed.id_estado_f) == 2)
                    {
                        _detPed.color_chips = "#0371C2";
                    }
                    else if (Convert.ToInt32(_detPed.id_estado_f) == 3)
                    {
                        _detPed.color_chips = "#D73506";
                    }

                    /* tipo de documento */
                    //if (Convert.ToInt32(_detPed.id_tipo_Doc) == 01)
                    //{
                    //    _detPed.nom_tipo_Doc = "FACTURA ELECTRONICA";
                    //}
                    //else if (Convert.ToInt32(_detPed.id_tipo_Doc) == 03)
                    //{
                    //    _detPed.nom_tipo_Doc = "BOLETA DE VENTA ELECTRONICA";
                    //}
                    detallepedido.Add(_detPed);
                }
            }
            catch (Exception ex)
            {
                detallepedido = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return detallepedido;
        }
    }
}
   
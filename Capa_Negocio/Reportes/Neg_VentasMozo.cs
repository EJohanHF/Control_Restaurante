using Capa_Datos.Data.Reportes.Pedidos;
using Capa_Entidades.Models.Reportes.Pedido;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Reportes
{
    public class Neg_VentasMozo
    {
        Dat_DpVentaMozo datDpVent = new Dat_DpVentaMozo();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<DpVentaMozo> GetHVentasMozo(string desde, string hasta, string dia,string idemp /*, string mes, string dia*/)
        {
            ObservableCollection<DpVentaMozo> VentMozo = new ObservableCollection<DpVentaMozo>();
            try
            {
                DataTable dt = datDpVent.GetDpVentasMozo(desde, hasta, dia, idemp );
                foreach (DataRow row in dt.Rows)
                {
                    DpVentaMozo _vmozo = new DpVentaMozo();
                    _vmozo.ID = Convert.ToInt32(row["ID"]);
                    _vmozo.PED_NUM_DIARIO = row["PED_NUM_DIARIO"].ToString();
                    _vmozo.M_NOM = row["M_NOM"].ToString();
                    _vmozo.PED_TOTAL = row["PED_TOTAL"].ToString();
                    _vmozo.DENO_PAGO = row["DENO_PAGO"].ToString();
                    _vmozo.DESC_EST = row["DESC_EST"].ToString();
                    _vmozo.PED_FECH_PED = Convert.ToDateTime(row["PED_FECH_PED"]).ToString("dd/MM/yyyy HH:mm:ss");
                    _vmozo.EMPL_NOM = row["EMPL_NOM"].ToString();
                    VentMozo.Add(_vmozo);
                }
            }
            catch (Exception ex)
            {
                VentMozo = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return VentMozo;
        }

        public ObservableCollection<DpVentaMozo> GetRankingMozo(string desde, string hasta, string dia/*, string mes, string dia*/)
        {
            ObservableCollection<DpVentaMozo> RankingMozo = new ObservableCollection<DpVentaMozo>();
            try
            {
                DataTable dt = datDpVent.GetRankingMozo(desde, hasta, dia);
                foreach (DataRow row in dt.Rows)
                {
                    DpVentaMozo _Rmozo = new DpVentaMozo();
                    _Rmozo.RM_id_emp = row["PED_ID_EMPL"].ToString();
                    _Rmozo.RM_nom_emp = row["EMPL_NOM"].ToString();
                    _Rmozo.RM_subtotal = row["PED_SUBTOTAL"].ToString();
                    _Rmozo.RM_descuento = row["PED_DESCUENTO"].ToString();
                    _Rmozo.RM_totalped = row["PED_TOTAL"].ToString();
                    RankingMozo.Add(_Rmozo);
                }
            }
            catch (Exception ex)
            {
                RankingMozo = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return RankingMozo;
        }
        #region Tipo Pago
        //public ObservableCollection<DpVentaMozo> GetTipoPago(string desde, string hasta)
        //{
        //    ObservableCollection<DpVentaMozo> detPed = new ObservableCollection<DpVentaMozo>();
        //    try
        //    {
        //        DataTable dt = datDpVent.GetTipoPago(desde, hasta);
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            DpVentaMozo _detPed = new DpVentaMozo();
        //            _detPed.tp_formapago = row["DENO_PAGO"].ToString();
        //            _detPed.tp_importe = Math.Round(Convert.ToDecimal(row["PED_IMPORTE"]), 2);
        //            detPed.Add(_detPed);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        detPed = null;
        //    }
        //    return detPed;
        //}

        #endregion

        #region Detalle pedido
        public ObservableCollection<DpVentaMozo> GetDepPedido(string desde, string hasta, string dia, object idped)
        {
            ObservableCollection<DpVentaMozo> detPed = new ObservableCollection<DpVentaMozo>();
            try
            {
                DataTable dt = datDpVent.GetDetPedido(desde, hasta, dia, idped);
                foreach (DataRow row in dt.Rows)
                {
                    DpVentaMozo _detPed = new DpVentaMozo();
                    _detPed.DP_Cant = Convert.ToInt32(row["DP_CANT"]);
                    _detPed.DP_Detalle = row["DP_DESCRIP"].ToString();
                    _detPed.DP_importe = Math.Round(Convert.ToDecimal(row["DP_IMPORT"]), 2);
                    _detPed.DP_fech_ped = Convert.ToDateTime(row["DP_FEC_REG"]).ToString("dd/MM/yyyy HH:mm:ss");
                    detPed.Add(_detPed);
                }
            }
            catch (Exception ex)
            {
                detPed = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return detPed;
        }

        #endregion

        #region Detalle Producto
        public ObservableCollection<DpVentaMozo> GetDetProducto(string desde, string hasta, string dia, string idcli)
        {
            ObservableCollection<DpVentaMozo> detProd = new ObservableCollection<DpVentaMozo>();
            try
            {
                DataTable dt = datDpVent.GetDetProducto(desde, hasta, dia, idcli);
                foreach (DataRow row in dt.Rows)
                {
                    DpVentaMozo _detProd = new DpVentaMozo();
                    _detProd.DProd_nom_emp = row["EMPL_NOM"].ToString();
                    _detProd.DProd_cant = Convert.ToInt32(row["nro_platos"]);
                    _detProd.DProd_monto = Math.Round(Convert.ToDecimal(row["total_dinero_platos"]), 2);
                    _detProd.DProd_nom_carta = row["CAR_NOM"].ToString();
                    detProd.Add(_detProd);
                    /*em.EMPL_NOM, count(dp.DP_ID_CARTA) as nro_platos, ISNULL(SUM(dp.DP_IMPORT),0) as total_dinero_platos, cart.IDCARTA, cart.CAR_NOM */
                }
            }
            catch (Exception ex)
            {
                detProd = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return detProd;
        }

        #endregion
        #region TIPOPAGO
        public List<DpVentaMozo> GetTipoPago(string desde, string hasta, string dia,string idemp)
        {
            List<DpVentaMozo> menu = new List<DpVentaMozo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datDpVent.GetTipoPago(desde, hasta, dia, idemp);
                menu = (from DataRow dr in dt.Rows
                        select new DpVentaMozo()
                        {
                            tp_m_efectivo = Math.Round(Convert.ToDecimal(dr["MontoEfectivo"]), 2),
                            tp_m_tarjeta = Math.Round(Convert.ToDecimal(dr["MontoTarjeta"]), 2),
                            tp_m_total = Math.Round(Convert.ToDecimal(dr["TotalMonto"]), 2)
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
    }
}

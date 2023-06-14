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
    public class Neg_DpVentas
    {
        Dat_DpVentas datDpVent = new Dat_DpVentas();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<DpVentas> GetHVentas(string desde, string hasta, string mes, string dia)
        {
            ObservableCollection<DpVentas> HistVeentas = new ObservableCollection<DpVentas>();
            try
            {
                DataTable dt = datDpVent.GetHistVentas(desde, hasta, mes, dia);
                foreach (DataRow row in dt.Rows)
                {
                    DpVentas _detPed = new DpVentas();
                    _detPed.id_ped = Convert.ToInt32(row["ID"]);
                    _detPed.num_dia_ped = row["PED_NUM_DIARIO"].ToString();

                    _detPed.id_mesa = row["IDMESA"].ToString();
                    _detPed.nom_mesa = row["M_NOM"].ToString();

                    _detPed.id_emple = row["IDEMP"].ToString();
                    _detPed.nom_emple = row["EMPL_NOM"].ToString();

                    _detPed.subtotal_ped = row["PED_SUBTOTAL"].ToString();
                    _detPed.f_ped =  Convert.ToDateTime(row["PED_FECH_PED"]).ToString("dd/MM/yyyy HH:mm:ss");
                    _detPed.impor_ped = row["PED_IMPORTE"].ToString();
                    _detPed.desc_ped = row["PED_DESCUENTO"].ToString();

                    _detPed.id_tip_desc = row["IDTDESC"].ToString();
                    _detPed.nom_tip_desc = row["TD_DESCR"].ToString();

                    _detPed.id_estado_f = row["PED_ID_ESTADO"].ToString();
                    _detPed.nom_estado_f = row["DESC_EST"].ToString();

                    _detPed.total_ped = row["PED_TOTAL"].ToString();

                    _detPed.id_fpago = row["IDFPAGO"].ToString();
                    _detPed.nom_fpago = row["DENO_PAGO"].ToString();

                    _detPed.id_usu = row["IDUSU"].ToString();
                    _detPed.nom_usu = row["USU_NOM"].ToString();

                    _detPed.id_dia_cierre = row["IDDC"].ToString();
                    _detPed.fech_dia_cerre = Convert.ToDateTime(row["DC_F_CREATE"]).ToString("dd/MM/yyyy HH:mm:ss");
                    _detPed.tipoDocumento = row["tipoDocumento"].ToString();
                    HistVeentas.Add(_detPed);
                }
            }
            catch (Exception ex)
            {
                HistVeentas = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return HistVeentas;
        }
        public ObservableCollection<DpVentas> GetDetPedido(string idped)
        {
            ObservableCollection<DpVentas> detPed = new ObservableCollection<DpVentas>();
            try
            {
                DataTable dt = datDpVent.GetDetPedido(idped);
                foreach (DataRow row in dt.Rows)
                {
                    DpVentas _detPed = new DpVentas();
                    _detPed.cant_ped = Convert.ToInt32(row["DP_CANT"]);
                    _detPed.DEt_ped = row["DP_DESCRIP"].ToString();
                    _detPed.Import_ped = Convert.ToDecimal(row["DP_IMPORT"]);
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

        #region Caja chica por fech
        public List<DpVentas> GetCajaChica(string desde, string hasta, string mes, string dia)
        {
            List<DpVentas> menu = new List<DpVentas>();
            try
            {
                DataTable dt = new DataTable();
                dt = datDpVent.GetCajaChicaxFech(desde,hasta,mes,dia);
                menu = (from DataRow dr in dt.Rows
                        select new DpVentas()
                        {
                            cc_ingre = Math.Round(Convert.ToDecimal(dr["INGRESO"]),2),
                            cc_egreso = Math.Round(Convert.ToDecimal(dr["EGRESO"]),2),
                            cc_saldo = Math.Round(Convert.ToDecimal(dr["TOTAL"]),2)
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
        #region Tipo Pago
        //public ObservableCollection<DpVentas> GetTipoPago(string desde, string hasta, string mes, string dia)
        //{
        //    ObservableCollection<DpVentas> detPed = new ObservableCollection<DpVentas>();
        //    try
        //    {
        //        DataTable dt = datDpVent.GetTipoPago(desde, hasta, mes, dia);
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            DpVentas _detPed = new DpVentas();
        //            _detPed.tp_formapago = row["DENO_PAGO"].ToString();
        //            _detPed.tp_importe = Math.Round(Convert.ToDecimal(row["PED_IMPORTE"]), 2);
        //            detPed.Add(_detPed);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        detPed = null;
        //    }
        //    return detPed;
        //}

        #endregion
        #region TIPOPAGO
        public List<DpVentas> GetTipoPago(string desde, string hasta, string mes, string dia)
        {
            List<DpVentas> menu = new List<DpVentas>();
            try
            {
                DataTable dt = new DataTable();
                dt = datDpVent.GetTipoPago(desde, hasta, mes, dia);
                menu = (from DataRow dr in dt.Rows
                        select new DpVentas()
                        {
                            tp_Montoefectivo = Math.Round(Convert.ToDecimal(dr["MontoEfectivo"]), 2),
                            tp_Montotarjeta = Math.Round(Convert.ToDecimal(dr["MontoTarjeta"]), 2),
                            tp_Montototal = Math.Round(Convert.ToDecimal(dr["TotalMonto"]), 2)
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

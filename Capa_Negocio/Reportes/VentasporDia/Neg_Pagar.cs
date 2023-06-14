using Capa_Datos.Data.Reportes.VentasPorDia;
using Capa_Entidades.Models.Reportes.DetalleporDia;
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
    public class Neg_Pagar
    {
        Dat_Pagar datPagar = new Dat_Pagar();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Pagar> GetDetallePago(int id_ped)
        {
            ObservableCollection<Pagar> pagar = new ObservableCollection<Pagar>();
            try
            {

                DataTable dt = datPagar.GetPagoDet(id_ped);
                foreach (DataRow row in dt.Rows)
                {
                    Pagar _pag = new Pagar();
                    _pag.id = Convert.ToInt32(row["ID"]);

                    _pag.idpago = Convert.ToInt32(row["IDPAGO"]);

                    _pag.idtpago = Convert.ToInt32(row["IDFPAGO"]);
                    _pag.nomtpago = row["DENO_PAGO"].ToString();

                    _pag.idtmoneda = Convert.ToInt32(row["IDTM"]);
                    _pag.nomtmoneda = row["TM_DESCR"].ToString();

                    _pag.idtcambio = row["IDTCAMBIO"].ToString();
                    _pag.nomtcambio = row["TC_CAMBIO"].ToString();

                    _pag.monto = Convert.ToDecimal(row["DT_TOTAL"]);

                    _pag.saldos = Convert.ToDecimal(row["SALDO"]);
                    _pag.amortizars = Convert.ToDecimal(row["AMORTIZARS"]);
                    _pag.P_SALDO = Convert.ToDecimal(row["P_SALDO"]);

                    _pag.P_MONTO = Convert.ToDecimal(row["P_MONTO"]);
                    _pag.P_ABONADO = Convert.ToDecimal(row["P_ABONADO"]);
                    pagar.Add(_pag);
                }
            }
            catch (Exception ex)
            {
                pagar = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return pagar;
        }
        //public ObservableCollection<Pagar> GetDetallePagoCuentas(int id_ped)
        //{
        //    ObservableCollection<Pagar> pagar = new ObservableCollection<Pagar>();
        //    try
        //    {

        //        DataTable dt = dataCuentas.GetPagoDet(id_ped, id_cuent);
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            Pagar _pag = new Pagar();
        //            _pag.id = Convert.ToInt32(row["ID"]);

        //            _pag.idpago = Convert.ToInt32(row["IDPAGO"]);

        //            _pag.idtpago = Convert.ToInt32(row["IDFPAGO"]);
        //            _pag.nomtpago = row["DENO_PAGO"].ToString();

        //            _pag.idtmoneda = Convert.ToInt32(row["IDTM"]);
        //            _pag.nomtmoneda = row["TM_DESCR"].ToString();

        //            _pag.idtcambio = row["IDTCAMBIO"].ToString();
        //            _pag.nomtcambio = row["TC_CAMBIO"].ToString();

        //            _pag.monto = Convert.ToDecimal(row["DT_TOTAL"]);

        //            _pag.saldos = Convert.ToDecimal(row["SALDO"]);
        //            _pag.amortizars = Convert.ToDecimal(row["AMORTIZARS"]);
        //            pagar.Add(_pag);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        pagar = null;
        //        //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
        //    }
        //    return pagar;
        //}

        public bool SetPagar(int operacion, Pagar pagar, ref string _mensaje, string NombreEquipo, DataTable dt_pagos)
        {
            bool result = false;
            result = datPagar.SetPagoDet(operacion, pagar, ref _mensaje, NombreEquipo,dt_pagos);
            if (operacion == 1)
            {
                if (result)
                {
                    _mensaje = "Operacion realizada con éxito !";
                }
            }

            return result;
        }
        public bool SetPagarOnline(int operacion, Pagar pagar, ref string _mensaje, DataTable dt_pagos)
        {
            bool result = false;
            result = datPagar.SetPagoDetOnline(operacion, pagar, ref _mensaje, dt_pagos);
            if (operacion == 1)
            {
                if (result)
                {
                    _mensaje = "Operacion realizada con éxito !";
                }
            }

            return result;
        }
        public bool SetAnularPedido(int operacion, Pagar pagar, ref string _mensaje,int idusuarioanulacion)
        {
            bool result = false;
            result = datPagar.SetAnularPedido(operacion, pagar, ref _mensaje, idusuarioanulacion);
            if (operacion == 1)
            {
                if (result)
                {
                    _mensaje = "Operacion realizada con éxito !";
                }
            }

            return result;
        }
        public bool QuitarDescuento(int id_pedido)
        {
            bool result = false;
            result = datPagar.QuitarDesc(id_pedido);
            if (result)
            {
               
            }
            return result;
        }
    }
}

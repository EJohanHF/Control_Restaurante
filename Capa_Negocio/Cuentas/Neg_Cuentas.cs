using Capa_Datos.Cuentas;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Cuentas
{
    public class Neg_Cuentas
    {
        Dat_Cuentas dataCuentas = new Dat_Cuentas();
        public ObservableCollection<Capa_Entidades.Models.Cuentas.Cuentas> GetPedidoxId(int id_pedido)
        {
            ObservableCollection<Capa_Entidades.Models.Cuentas.Cuentas> cuentas = new ObservableCollection<Capa_Entidades.Models.Cuentas.Cuentas>();
            try
            {
                DataTable dt = dataCuentas.sp_get_cuentasx_id_ped(id_pedido);
                foreach (DataRow row in dt.Rows)
                {
                    Capa_Entidades.Models.Cuentas.Cuentas _cuentas = new Capa_Entidades.Models.Cuentas.Cuentas();
                    _cuentas.ID = row["ID"].ToString();
                    _cuentas.ID_PED = row["ID_PED"].ToString();
                    _cuentas.NOM_CUENTA = row["NOM_CUENTA"].ToString();
                    _cuentas.SUBTOTAL_CUENTA =row["SUBTOTAL_CUENTA"].ToString();
                    _cuentas.EST_CUENTA = row["EST_CUENTA"].ToString();
                    _cuentas.ESTADO_CUENTA = row["ESTADO_CUENTA"].ToString();
                    _cuentas.FECHA_CUENTA = Convert.ToDateTime(row["FECHA_CUENTA"]);
                    cuentas.Add(_cuentas);
                }
            }
            catch (Exception ex)
            {
                cuentas = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return cuentas;
        }
        public int SetCuenta(DataTable dt, string id_ped, string nom_cuenta, ref string _mensaje)
        {
            int result = 0;
            result = dataCuentas.SetCuenta(dt, id_ped, nom_cuenta,ref _mensaje);
            if (result != 0)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public bool SetPagar(int operacion, Pagar pagar, ref string _mensaje, string NombreEquipo, DataTable dt_pagos, string id_cuenta)
        {
            bool result = false;
            result = dataCuentas.SetPagoDet(operacion, pagar, ref _mensaje, NombreEquipo, dt_pagos, id_cuenta);
            if (operacion == 1)
            {
                if (result)
                {
                    _mensaje = "Operacion realizada con éxito !";
                }
            }

            return result;
        }
        public DataTable GetCuentaxIdCuenta(int id_cuenta)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataCuentas.GetCuentaxIdCuenta(id_cuenta);
            }
            catch (Exception ex)
            {
                dt = null;
                ////globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable sp_verifica_pago(int id_ped)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dataCuentas.sp_verifica_pago(id_ped);
            }
            catch (Exception ex)
            {
                dt = null;
                ////globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public bool sp_anular_cuenta(string IdCuenta, ref string _mensaje)
        {
            bool result = false;
            result = dataCuentas.sp_anular_cuenta(IdCuenta, ref _mensaje);
            if (result != false)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public ObservableCollection<Pagar> GetDetallePago(int id_ped,int id_cuent)
        {
            ObservableCollection<Pagar> pagar = new ObservableCollection<Pagar>();
            try
            {

                DataTable dt = dataCuentas.GetPagoDet(id_ped, id_cuent);
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
    }
}

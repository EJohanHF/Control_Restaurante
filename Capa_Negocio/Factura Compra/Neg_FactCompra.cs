using Capa_Datos.Factura_Compra;
using Capa_Entidades.Models.Factura_Compra;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    

namespace Capa_Negocio.Factura_Compra
{
    public class Neg_FactCompra
    {
        DatFactCompra datFactCompra = new DatFactCompra();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public ObservableCollection<FactCompra> GetFacturaCompra()
        {
            ObservableCollection<FactCompra> facCompra = new ObservableCollection<FactCompra>();
            try
            {
                DataTable dt = datFactCompra.GetFacCompra();
                foreach (DataRow row in dt.Rows)
                {
                    FactCompra _td = new FactCompra();
                    _td.ID = Convert.ToInt32(row["ID"]);
                    _td.FC_NRO_DOC_EMISOR = row["FC_NRO_DOC_EMISOR"].ToString();
                    _td.FC_TIP_DOC_EMISOR = Convert.ToInt32(row["FC_TIP_DOC_EMISOR"]);
                    _td.FC_TIP_DOC = Convert.ToInt32(row["FC_TIP_DOC"]);
                    _td.FC_SER_NRO = row["FC_SER_NRO"].ToString();
                    _td.FC_F_EMISION = Convert.ToDateTime(row["FC_F_EMISION"]);
                    _td.FC_TIP_MONEDA = row["FC_TIP_MONEDA"].ToString();
                    _td.FC_TOTAL_OP_GRABADAS = Convert.ToDecimal(row["FC_TOTAL_OP_GRABADAS"]);
                    _td.FC_SUMA_IGV = Convert.ToDecimal(row["FC_SUMA_IGV"]);
                    _td.FC_IMPORTE_TOTAL_COMPRA = Convert.ToDecimal(row["FC_IMPORTE_TOTAL_COMPRA"]);
                    _td.FC_TOTAL_DESC = Convert.ToDecimal(row["FC_TOTAL_DESC"]);
                    _td.FC_DESCUENTOS_GLOBALES = Convert.ToDecimal(row["FC_DESCUENTOS_GLOBALES"]);
                    _td.FC_MONTO_LETRA = row["FC_MONTO_LETRA"].ToString();
                    _td.FC_CORREO_EMISOR = row["FC_CORREO_EMISOR"].ToString();
                    _td.FC_COD_PAIS_EMISOR = row["FC_COD_PAIS_EMISOR"].ToString();
                    _td.FC_ID_PROVEEDOR = Convert.ToInt32(row["FC_ID_PROVEEDOR"]);
                    _td.P_NOM = row["P_NOM"].ToString();
                    _td.FC_ESTADO_DOC = Convert.ToInt32(row["FC_ESTADO_DOC"]);
                    _td.FC_VENCIMIENTO = Convert.ToDateTime(row["FC_VENCIMIENTO"]);
                    _td.FC_TOTAL_PAGADO = Convert.ToDecimal(row["FC_TOTAL_PAGADO"]);
                    _td.FC_SALDO_X_PAGAR = Convert.ToDecimal(row["FC_SALDO_X_PAGAR"]);
                    _td.FC_ESTADO_CARGA = Convert.ToBoolean(row["FC_ESTADO_CARGA"]);
                    if (_td.FC_ESTADO_DOC == 1)
                    {
                        _td.FC_COLOR_ESTADO = "LightGreen";
                        _td.FC_NOMBRE_ESTADO = "Emitida";
                        _td.EnabledCarga = true;
                    }
                    else if (_td.FC_ESTADO_DOC == 0)
                    {
                        _td.FC_COLOR_ESTADO = "OrangeRed";
                        _td.FC_NOMBRE_ESTADO = "Anulada";
                        _td.EnabledCarga = false;
                    }
                    else if (_td.FC_ESTADO_DOC == 2)
                    {
                        _td.FC_COLOR_ESTADO = "SkyBlue";
                        _td.FC_NOMBRE_ESTADO = "Abastecida";
                        _td.EnabledCarga = false;
                    }
                    facCompra.Add(_td);
                }
            }
            catch (Exception ex)
            {
                facCompra = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return facCompra;
        }

        public ObservableCollection<FactCompra> GetFacturaCompraTipDoc()
        {
            ObservableCollection<FactCompra> facCompra = new ObservableCollection<FactCompra>();
            try
            {
                DataTable dt = datFactCompra.GetFacCompraTipoDoc();
                foreach (DataRow row in dt.Rows)
                {
                    FactCompra _td = new FactCompra();
                    _td.FC_TIP_DOC = Convert.ToInt32(row["ID"]);
                    _td.FC_NOMBRE_TIP_DOC = row["DC_DESCR"].ToString();
                    facCompra.Add(_td);
                }
            }
            catch (Exception ex)
            {
                facCompra = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return facCompra;
        }
        public ObservableCollection<FactCompraDet> GetFacturaCompraDet()
        {
            ObservableCollection<FactCompraDet> facCompraDet = new ObservableCollection<FactCompraDet>();
            try
            {
                DataTable dt = datFactCompra.GetFacCompraDet();
                foreach (DataRow row in dt.Rows)
                {
                    FactCompraDet _td = new FactCompraDet();
                    _td.ID = Convert.ToInt32(row["ID"]);
                    _td.FCD_ID_FACT_COMPRA = Convert.ToInt32(row["FCD_ID_FACT_COMPRA"]);
                    _td.FCD_ORDEN_ITEM = Convert.ToInt32(row["FCD_ORDEN_ITEM"]);
                    _td.FCD_ID_INS = Convert.ToInt32(row["FCD_ID_INS"]);
                    _td.INS_NOM = row["INS_NOM"].ToString();
                    _td.FCD_DESCR_INS = row["FCD_DESCR_INS"].ToString();
                    _td.FCD_UN_MED_INS = row["FCD_UN_MED_INS"].ToString();
                    _td.FCD_CANT_ITEM = Convert.ToInt32(row["FCD_CANT_ITEM"]);
                    _td.FCD_VALOR_UNITARIO_SIN_IGV = Convert.ToDecimal(row["FCD_VALOR_UNITARIO_SIN_IGV"]);
                    _td.FCD_VALOR_UNITARIO_CON_IGV = Convert.ToDecimal(row["FCD_VALOR_UNITARIO_CON_IGV"]);
                    _td.FCD_IMPORTE_IGV_INS = Convert.ToDecimal(row["FCD_IMPORTE_IGV_INS"]);
                    _td.FCD_COD_AFECTACION_IGV_ITEM = Convert.ToDecimal(row["FCD_COD_AFECTACION_IGV_ITEM"]);
                    _td.FCD_DESC_INS = Convert.ToDecimal(row["FCD_DESC_INS"]);
                    _td.FCD_VALOR_COMPRA_ITEM = Convert.ToDecimal(row["FCD_VALOR_COMPRA_ITEM"]);
                    _td.FCD_PRE_UNI = Convert.ToDecimal(row["FCD_PRE_UNI"]);
                    _td.FCD_IMPORTE_ITEM = Convert.ToDecimal(row["FCD_IMPORTE_ITEM"]);
                    _td.FCD_VALOR_COMPRA_SIN_IGV = Convert.ToDecimal(row["FCD_VALOR_COMPRA_SIN_IGV"]);
                    facCompraDet.Add(_td);
                }
            }
            catch (Exception ex)
            {
                facCompraDet = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return facCompraDet;
        }
        public ObservableCollection<FactCompraDet> GetFacturaCompraDetTotal(int tipo, DateTime desde, DateTime hasta)
        {
            ObservableCollection<FactCompraDet> facCompraDet = new ObservableCollection<FactCompraDet>();
            try
            {
                DataTable dt = datFactCompra.GetFacCompraDetTotal(tipo, desde, hasta);
                foreach (DataRow row in dt.Rows)
                {
                    FactCompraDet _td = new FactCompraDet();
                    _td.ID = Convert.ToInt32(row["ID"]);
                    _td.INS_NOM = row["INS_NOM"].ToString();
                    _td.FCD_CANT_ITEM = Convert.ToDecimal(row["FCD_CANT_ITEM"]);
                    _td.FCD_IMPORTE_ITEM = Convert.ToDecimal(row["FCD_IMPORTE_ITEM"]);
                    _td.FC_ID_PROVEEDOR = Convert.ToInt32(row["FC_ID_PROVEEDOR"]);
                    _td.FC_ESTADO_DOC = Convert.ToInt32(row["FC_ESTADO_DOC"]);
                    _td.FC_TIP_DOC = Convert.ToInt32(row["FC_TIP_DOC"]);
                    _td.P_NOM = row["P_NOM"].ToString();
                    facCompraDet.Add(_td);
                }
            }
            catch (Exception ex)
            {
                facCompraDet = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return facCompraDet;
        }
        public ObservableCollection<FactCompra> GetCondicionPago()
        {
            ObservableCollection<FactCompra> facCompraDet = new ObservableCollection<FactCompra>();
            try
            {
                DataTable dt = datFactCompra.GetCondicionPago();
                foreach (DataRow row in dt.Rows)
                {
                    FactCompra _td = new FactCompra();
                    _td.CONDICION_ID = Convert.ToInt32(row["ID"]);
                    _td.CONDICION_DESCR = row["CON_DESCR"].ToString();
                    _td.CONDICION_FCREATE = Convert.ToDateTime(row["CON_F_CREATE"]);
                    _td.CONDICION_FMODIF = Convert.ToDateTime(row["CON_F_MODIFICACION"]);
                    facCompraDet.Add(_td);
                }
            }
            catch (Exception ex)
            {
                facCompraDet = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return facCompraDet;
        }

        public bool SetFacturaCompra(int operacion, FactCompra factcom, DataTable dt, ref string _mensaje)
        {
            bool result = false;
            result = datFactCompra.SetFactCompra(operacion, factcom, dt, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public bool SetProductos(int operacion, int id,int id_usuario, ref string _mensaje)
        {
            bool result = false;
            result = datFactCompra.SetSetProductos(operacion, id, id_usuario,ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public ObservableCollection<FactCompra> GetFacturaCompraEstado()
        {
            ObservableCollection<FactCompra> facCompra = new ObservableCollection<FactCompra>();
            try
            {
                DataTable dt = datFactCompra.GetFacCompraEstado();
                foreach (DataRow row in dt.Rows)
                {
                    FactCompra _td = new FactCompra();
                    _td.FC_ESTADO_DOC = Convert.ToInt32(row["FC_ESTADO_DOC"]);
                    if (_td.FC_ESTADO_DOC == 1)
                    {
                        _td.FC_COLOR_ESTADO = "LightGreen";
                        _td.FC_NOMBRE_ESTADO = "Emitida";
                        _td.EnabledCarga = true;
                    }
                    else if (_td.FC_ESTADO_DOC == 0)
                    {
                        _td.FC_COLOR_ESTADO = "OrangeRed";
                        _td.FC_NOMBRE_ESTADO = "Anulada";
                        _td.EnabledCarga = false;
                    }
                    else if (_td.FC_ESTADO_DOC == 2)
                    {
                        _td.FC_COLOR_ESTADO = "SkyBlue";
                        _td.FC_NOMBRE_ESTADO = "Abastecida";
                        _td.EnabledCarga = false;
                    }
                    facCompra.Add(_td);
                }
            }
            catch (Exception ex)
            {
                facCompra = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return facCompra;
        }


    }
}

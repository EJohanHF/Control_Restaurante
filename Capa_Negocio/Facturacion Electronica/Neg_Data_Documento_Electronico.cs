using Capa_Datos.Facturacion_Electronica;
using Capa_Entidades.Models.Facturacion_Electronica;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Facturacion_Electronica
{
    public class Neg_Data_Documento_Electronico
    {
        Dat_Data_Documento_Electronico datDataDocumentoElectronico = new Dat_Data_Documento_Electronico();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Data_Documento_Electronico> GetDataDocumentoElectronico(int id_pedido)
        {
            ObservableCollection<Data_Documento_Electronico> DataDocumentoElectronico = new ObservableCollection<Data_Documento_Electronico>();
            try
            {

                DataTable dt = datDataDocumentoElectronico.GetDataDocumentoElectronico(id_pedido);
                int orden = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Data_Documento_Electronico _DataDocumentoElectronico = new Data_Documento_Electronico();
                    orden += 1;
                    _DataDocumentoElectronico.orden = orden;
                    _DataDocumentoElectronico.ID = Convert.ToInt32(row["ID"]);
                    _DataDocumentoElectronico.ID_PEDIDO = Convert.ToInt32(row["DP_ID_PED"]);
                    _DataDocumentoElectronico.CANTIDAD = (decimal)row["DP_CANT"];
                    _DataDocumentoElectronico.DESCRIPCION = row["DP_DESCRIP"].ToString();
                    _DataDocumentoElectronico.PRECIO_UNI = (decimal)row["DP_PRE_UNI"]; //xd
                    _DataDocumentoElectronico.DESCUENTO = (decimal)row["DP_DESCU"];
                    _DataDocumentoElectronico.IMPORTE = (decimal)row["DP_IMPORT"];

                    _DataDocumentoElectronico.SUB_TOTAL = (decimal)row["PED_SUBTOTAL"];
                    _DataDocumentoElectronico.DESCUENTO_TOTAL = (decimal)row["PED_DESCUENTO"];

                    _DataDocumentoElectronico.TOTAL = (decimal)row["PED_TOTAL"];
                    _DataDocumentoElectronico.CLASIFICACION_PRODUCTO_ITEM = row["codigo"].ToString();
                    _DataDocumentoElectronico.DP_ID_CARTA = row["DP_ID_CARTA"].ToString();
                    _DataDocumentoElectronico.EXONERADA = Convert.ToByte(row["EXONERADA"]);
                    _DataDocumentoElectronico.TM_DESC = row["TM_DESC"].ToString();
                    _DataDocumentoElectronico.CANT_DOC = (decimal)row["CANTIDADDOC"];
                    _DataDocumentoElectronico.RC = Convert.ToByte(row["RC"]);
                    DataDocumentoElectronico.Add(_DataDocumentoElectronico);
                }
            }
            catch (Exception ex)
            {
                DataDocumentoElectronico = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return DataDocumentoElectronico;
        }
        public ObservableCollection<Data_Documento_Electronico> GetDataDocumentoElectronicoTotal(int id_pedido)
        {
            ObservableCollection<Data_Documento_Electronico> DataDocumentoElectronico = new ObservableCollection<Data_Documento_Electronico>();
            try
            {

                DataTable dt = datDataDocumentoElectronico.GetDataDocumentoElectronicoTotal(id_pedido);
                int orden = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Data_Documento_Electronico _DataDocumentoElectronico = new Data_Documento_Electronico();
                    orden += 1;
                    _DataDocumentoElectronico.orden = orden;
                    _DataDocumentoElectronico.ID = Convert.ToInt32(row["ID"]);
                    _DataDocumentoElectronico.ID_PEDIDO = Convert.ToInt32(row["DP_ID_PED"]);
                    _DataDocumentoElectronico.CANTIDAD = (decimal)row["DP_CANT"];
                    _DataDocumentoElectronico.DESCRIPCION = row["DP_DESCRIP"].ToString();
                    _DataDocumentoElectronico.PRECIO_UNI = (decimal)row["DP_PRE_UNI"]; //xd
                    _DataDocumentoElectronico.DESCUENTO = (decimal)row["DP_DESCU"];
                    _DataDocumentoElectronico.IMPORTE = (decimal)row["DP_IMPORT"];

                    _DataDocumentoElectronico.SUB_TOTAL = (decimal)row["PED_SUBTOTAL"];
                    _DataDocumentoElectronico.DESCUENTO_TOTAL = (decimal)row["PED_DESCUENTO"];

                    _DataDocumentoElectronico.TOTAL = (decimal)row["PED_TOTAL"];
                    _DataDocumentoElectronico.CLASIFICACION_PRODUCTO_ITEM = row["codigo"].ToString();
                    _DataDocumentoElectronico.DP_ID_CARTA = row["DP_ID_CARTA"].ToString();
                    _DataDocumentoElectronico.EXONERADA = Convert.ToByte(row["EXONERADA"]);
                    _DataDocumentoElectronico.TM_DESC = row["TM_DESC"].ToString();
                    _DataDocumentoElectronico.RC = Convert.ToByte(row["RC"]);
                    DataDocumentoElectronico.Add(_DataDocumentoElectronico);
                }
            }
            catch (Exception ex)
            {
                DataDocumentoElectronico = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return DataDocumentoElectronico;
        }
        public ObservableCollection<Data_Documento_Electronico> GetDataDocumentoElectronicoxId(int id_pedido)
        {
            ObservableCollection<Data_Documento_Electronico> DataDocumentoElectronico = new ObservableCollection<Data_Documento_Electronico>();
            try
            {

                DataTable dt = datDataDocumentoElectronico.GetDataDocumentoElectronicoxID(id_pedido);
                int orden = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Data_Documento_Electronico _DataDocumentoElectronico = new Data_Documento_Electronico();
                    orden += 1;
                    _DataDocumentoElectronico.orden = orden;
                    _DataDocumentoElectronico.ID = Convert.ToInt32(row["ID"]);
                    _DataDocumentoElectronico.ID_PEDIDO = Convert.ToInt32(row["DP_ID_PED"]);
                    _DataDocumentoElectronico.CANTIDAD = (decimal)row["DP_CANT"];
                    _DataDocumentoElectronico.DESCRIPCION = row["DP_DESCRIP"].ToString();
                    _DataDocumentoElectronico.PRECIO_UNI = (decimal)row["DP_PRE_UNI"]; //xd
                    _DataDocumentoElectronico.DESCUENTO = (decimal)row["DP_DESCU"];
                    _DataDocumentoElectronico.IMPORTE = (decimal)row["DP_IMPORT"];

                    _DataDocumentoElectronico.SUB_TOTAL = (decimal)row["PED_SUBTOTAL"];
                    _DataDocumentoElectronico.DESCUENTO_TOTAL = (decimal)row["PED_DESCUENTO"];

                    _DataDocumentoElectronico.TOTAL = (decimal)row["PED_TOTAL"];
                    _DataDocumentoElectronico.CLASIFICACION_PRODUCTO_ITEM = row["codigo"].ToString();
                    _DataDocumentoElectronico.DP_ID_CARTA = row["DP_ID_CARTA"].ToString();
                    _DataDocumentoElectronico.TM_DESC = row["TM_DESC"].ToString();
                    _DataDocumentoElectronico.CANT_DOC = (decimal)row["CANTIDADDOC"];
                    _DataDocumentoElectronico.RC = Convert.ToByte(row["RC"]);
                    _DataDocumentoElectronico.numeroDocIdentidadEmisor = row["numeroDocIdentidadEmisor"].ToString();
                    _DataDocumentoElectronico.tipoDocumento = row["tipoDocumento"].ToString();
                    _DataDocumentoElectronico.serieNumero = row["serieNumero"].ToString();
                    DataDocumentoElectronico.Add(_DataDocumentoElectronico);
                }
            }
            catch (Exception ex)
            {
                DataDocumentoElectronico = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return DataDocumentoElectronico;
        }
        public ObservableCollection<Data_Documento_Electronico> GetDataDocumentoElectronicoCuenta(int id_pedido,int id_cuenta)
        {
            ObservableCollection<Data_Documento_Electronico> DataDocumentoElectronico = new ObservableCollection<Data_Documento_Electronico>();
            try
            {

                DataTable dt = datDataDocumentoElectronico.GetDataDocumentoElectronicoCuenta(id_pedido, id_cuenta);
                int orden = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Data_Documento_Electronico _DataDocumentoElectronico = new Data_Documento_Electronico();
                    orden += 1;
                    _DataDocumentoElectronico.orden = orden;
                    _DataDocumentoElectronico.ID = Convert.ToInt32(row["ID"]);
                    _DataDocumentoElectronico.ID_PEDIDO = Convert.ToInt32(row["DP_ID_PED"]);
                    _DataDocumentoElectronico.CANTIDAD = (decimal)row["DP_CANT"];
                    _DataDocumentoElectronico.DESCRIPCION = row["DP_DESCRIP"].ToString();
                    _DataDocumentoElectronico.PRECIO_UNI = (decimal)row["DP_PRE_UNI"]; //xd
                    _DataDocumentoElectronico.DESCUENTO = (decimal)row["DP_DESCU"];
                    _DataDocumentoElectronico.IMPORTE = (decimal)row["DP_IMPORT"];

                    _DataDocumentoElectronico.SUB_TOTAL = (decimal)row["PED_SUBTOTAL"];
                    _DataDocumentoElectronico.DESCUENTO_TOTAL = (decimal)row["PED_DESCUENTO"];

                    _DataDocumentoElectronico.TOTAL = (decimal)row["PED_TOTAL"];
                    _DataDocumentoElectronico.CLASIFICACION_PRODUCTO_ITEM = row["codigo"].ToString();
                    _DataDocumentoElectronico.DP_ID_CARTA = row["DP_ID_CARTA"].ToString();
                    _DataDocumentoElectronico.EXONERADA = Convert.ToByte(row["EXONERADA"]);
                    _DataDocumentoElectronico.TM_DESC = row["TM_DESC"].ToString();
                    _DataDocumentoElectronico.CANT_DOC = (decimal)row["CANTIDADDOC"];
                    _DataDocumentoElectronico.RC = Convert.ToByte(row["RC"]);
                    DataDocumentoElectronico.Add(_DataDocumentoElectronico);
                }
            }
            catch (Exception ex)
            {
                DataDocumentoElectronico = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return DataDocumentoElectronico;
        }
    }
}

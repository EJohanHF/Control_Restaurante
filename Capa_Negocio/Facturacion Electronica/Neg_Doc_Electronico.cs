using Capa_Datos.Facturacion_Electronica;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Facturacion_Electronica
{
    public class Neg_Doc_Electronico
    {
        Dat_Doc_Electronico datDocElect = new Dat_Doc_Electronico();
        Funcion_Global globales = new Funcion_Global();
        public DataTable GetDocElectronico(int idDocElectr)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datDocElect.GetDocElectronico(idDocElectr);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return dt;
        }
        public int SetDocElectronico(decimal sumatoriaImpuestoBolsas,int idCliente,string direccionReceptor,decimal sumatoriaOtrosCargos,decimal porcentajeOtrosCargos,string telefono, string codigoTipoOperacion, string idpedido, string codigoPaisReceptor,
            decimal importeTotal,string correoReceptor,string numeroDocIdentidadEmisor, string tipoDocIdentidadEmisor,string tipoDocumento,string serieNumero,DateTime fechaEmision,string tipoMoneda,string numeroDocIdentidadReceptor,
            string razonSocialReceptor,string tipoDocIdentidadReceptor,decimal totalOPGravadas,decimal totalOPExoneradas,decimal totalOPNoGravadas,decimal totalOPGratuitas,decimal sumatoriaIGV,decimal sumatoriaISC,decimal importeTotalVenta,
            decimal totalDescuentos,decimal descuentosGlobales,string montoEnLetras,string nomequipo, DataTable dt)
        {
            int result = 0;
            result = datDocElect.SetDocElectronico(sumatoriaImpuestoBolsas,idCliente,direccionReceptor, sumatoriaOtrosCargos, porcentajeOtrosCargos,telefono, codigoTipoOperacion, idpedido, codigoPaisReceptor,importeTotal,correoReceptor,numeroDocIdentidadEmisor,tipoDocIdentidadEmisor,
                tipoDocumento,serieNumero,fechaEmision,tipoMoneda,numeroDocIdentidadReceptor,razonSocialReceptor,tipoDocIdentidadReceptor,totalOPGravadas,totalOPExoneradas,totalOPNoGravadas,totalOPGratuitas,sumatoriaIGV,sumatoriaISC,importeTotalVenta,
            totalDescuentos,descuentosGlobales,montoEnLetras, nomequipo, dt);
            if (result != 0)
            {
                
            }
            return result;
        }

        public string MONTO_LETRAS(decimal numero)
        {
            string monto_letras = "";
            DataTable dt = new DataTable();
            try
            {
                dt = datDocElect.GetMontoLetras(numero);
                monto_letras = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return monto_letras;
        }
        public string JsonDocElectronico(int idDocElec)
        {
            string monto_letras = "";
            DataTable dt = new DataTable();
            try
            {
                dt = datDocElect.sp_generar_json_fe(idDocElec);
                monto_letras = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return monto_letras;
        }
        public bool EliminarDocElectronico(int idDocElec)
        {
            bool result = false;
            try
            {
                result = datDocElect.EliminarDocElectronico(idDocElec);
            }
            catch (Exception ex)
            {
                result = false;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return result;
        }
        public bool ActualizarCorrelaticoDocElectronico(string serie)
        {
            bool result = false;
            try
            {
                result = datDocElect.ActualizarCorrelaticoDocElectronico(serie);
            }
            catch (Exception ex)
            {
                result = false;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return result;
        }
        public bool VerificaCliente (string nrodoc,string nombre,int tipo_doc,string direccion,string telefono,string correo)
        {
            bool result = false;
            try
            {
                result = datDocElect.VerificaCliente(nrodoc,nombre,tipo_doc,direccion,telefono,correo);
            }
            catch (Exception ex)
            {
                result = false;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return result;
        }
        public string getCabecera(int idDocElec)
        {
            string result = "";
            DataTable dt = new DataTable();
            try
            {
                dt = datDocElect.getCabecera(idDocElec);
                result = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return result;
        }
        public string getDetalle(int idDocElec)
        {
            string result = "";
            DataTable dt = new DataTable();
            try
            {
                dt = datDocElect.getDetalle(idDocElec);
                result = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return result;
        }
        public string getTributo(int idDocElec)
        {
            string result = "";
            DataTable dt = new DataTable();
            try
            {
                dt = datDocElect.getTributo(idDocElec);
                result = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return result;
        }
        public string getResumen(string fecha)
        {
            string result = "";
            DataTable dt = new DataTable();
            try
            {
                dt = datDocElect.getResumen(fecha);
                result = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return result;
        }
        public string getResumenTrd(string fecha)
        {
            string result = "";
            DataTable dt = new DataTable();
            try
            {
                dt = datDocElect.getResumenTrd(fecha);
                result = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return result;
        }
    }
    
}

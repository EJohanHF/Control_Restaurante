using Capa_Datos;
using Capa_Datos.Data.Reportes.VentasPorDia;
using Capa_Entidades.Models.Web_Service;
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
 public class Neg_CierreParcial
    {
        Dat_CierreParcial datCierre = new Dat_CierreParcial();
        Funcion_Global globales = new Funcion_Global();
        Dat_Util datCombo = new Dat_Util();
        public DataTable GetCabeceraBoleta()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetCabeceraBoleta();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public string GetDetalleJson(string iddoc)
        {
            string json = "";
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetDetalleJson(iddoc);
                json = dt.Rows[0][0].ToString();

            }
            catch (Exception ex)
            {
                dt = null;
                json = "";
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return json;
        }
        public bool sp_actualizar_envio(string id)
        {
            bool result = false;
            result = datCierre.sp_actualizar_envio(id);
            return result;
        }
        public bool sp_actualizar_faltante(string serieNumero)
        {
            bool result = false;
            result = datCierre.sp_actualizar_faltante(serieNumero);
            return result;
        }
        public DataTable GetAplatosXComentario(string NombreEquipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetAplatosXComentario(NombreEquipo);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetAplatosXComentario2(int idCierre)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetAplatosXComentario2(idCierre);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public string GetCierreJson()
        {
            string json = "";
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetCierreJson();
                json = dt.Rows[0][0].ToString();

            }
            catch (Exception ex)
            {
                dt = null;
                json = "";
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return json;
        }
        public DataTable GetreporteVetasDia(string NombreEquipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetreporteVentasDia(NombreEquipo);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetreporteVetasDia2(int idCierre)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetreporteVentasDia2(idCierre);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        #region doc electronico
        public DataTable GetreporteDocumentoElect(string NombreEquipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetreporteDocumentosElect(NombreEquipo);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetreporteDocumentoElect2(int idCierre)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetreporteDocumentosElect2(idCierre);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetTipoPago(string NombreEquipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetTipoPago(NombreEquipo);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetTipoPago2(int idCierre)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetTipoPago2(idCierre);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public ObservableCollection<EnvioCierre> GetVentaDia()
        {
            ObservableCollection<EnvioCierre> ventadia = new ObservableCollection<EnvioCierre>();
            try
            {

                DataTable dt = datCierre.GetVentaDia();
                foreach (DataRow row in dt.Rows)
                {
                    EnvioCierre _ventadia = new EnvioCierre();
                    _ventadia.VBRUTA = row["VBRUTA"].ToString();
                    _ventadia.DESCU = row["DESCU"].ToString();
                    _ventadia.TOTALVENTA = row["TOTALVENTA"].ToString();
                    _ventadia.INGRESO = row["INGRESO"].ToString(); //xd
                    _ventadia.EGRESO = row["EGRESO"].ToString();
                    _ventadia.TOTALCAJACHICA = row["TOTALCAJACHICA"].ToString();

                    _ventadia.TOTALCAJA = row["TOTALCAJA"].ToString();
                    _ventadia.BOLECANT = row["BOLECANT"].ToString();

                    _ventadia.FACTCANT = row["FACTCANT"].ToString();
                    _ventadia.TOTALBOLE = row["TOTALBOLE"].ToString();

                    _ventadia.TOTALFACT = row["TOTALFACT"].ToString();
                    _ventadia.TOTALDOC = row["TOTALDOC"].ToString();

                    _ventadia.TOTALSB = row["TOTALSB"].ToString();
                    _ventadia.BOLECANTANU = row["BOLECANTANU"].ToString();

                    _ventadia.FACTCANTANU = row["FACTCANTANU"].ToString();
                    _ventadia.TOTALBOLEANU = row["TOTALBOLEANU"].ToString();

                    _ventadia.TOTALFACTANU = row["TOTALFACTANU"].ToString(); //xd
                    _ventadia.TOTALDOCANU = row["TOTALDOCANU"].ToString();
                    // aqui se convierte en ToByte;
                    
                    ventadia.Add(_ventadia);
                }
            }
            catch (Exception ex)
            {
                ventadia = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return ventadia;
        }
        public ObservableCollection<EnvioCierre> GetVentaDiaCierre(string id)
        {
            ObservableCollection<EnvioCierre> ventadia = new ObservableCollection<EnvioCierre>();
            try
            {

                DataTable dt = datCierre.GetVentaDiaCierre(id);
                foreach (DataRow row in dt.Rows)
                {
                    EnvioCierre _ventadia = new EnvioCierre();
                    _ventadia.VBRUTA = row["VBRUTA"].ToString();
                    _ventadia.DESCU = row["DESCU"].ToString();
                    _ventadia.TOTALVENTA = row["TOTALVENTA"].ToString();
                    _ventadia.INGRESO = row["INGRESO"].ToString(); //xd
                    _ventadia.EGRESO = row["EGRESO"].ToString();
                    _ventadia.TOTALCAJACHICA = row["TOTALCAJACHICA"].ToString();

                    _ventadia.TOTALCAJA = row["TOTALCAJA"].ToString();
                    _ventadia.BOLECANT = row["BOLECANT"].ToString();

                    _ventadia.FACTCANT = row["FACTCANT"].ToString();
                    _ventadia.TOTALBOLE = row["TOTALBOLE"].ToString();

                    _ventadia.TOTALFACT = row["TOTALFACT"].ToString();
                    _ventadia.TOTALDOC = row["TOTALDOC"].ToString();

                    _ventadia.TOTALSB = row["TOTALSB"].ToString();
                    _ventadia.BOLECANTANU = row["BOLECANTANU"].ToString();

                    _ventadia.FACTCANTANU = row["FACTCANTANU"].ToString();
                    _ventadia.TOTALBOLEANU = row["TOTALBOLEANU"].ToString();

                    _ventadia.TOTALFACTANU = row["TOTALFACTANU"].ToString(); //xd
                    _ventadia.TOTALDOCANU = row["TOTALDOCANU"].ToString();
                    _ventadia.FECHA = Convert.ToDateTime(row["FECHA"]);
                    // aqui se convierte en ToByte;

                    ventadia.Add(_ventadia);
                }
            }
            catch (Exception ex)
            {
                ventadia = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return ventadia;
        }
        public DataTable GetDetallePagos()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetDetallePagos();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetDetallePagosCierre(string idcierre)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetDetallePagosCierre(idcierre);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        #endregion
        public DataTable GetreportetotalPedidosDia( string idscat, string NombreEquipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GettotalPedidosDia(idscat, NombreEquipo);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetreportetotalPedidosDia2(string idscat,int idCierre)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GettotalPedidosDia2(idscat,idCierre);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable Getsupercat()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCombo.GetSuperCategoria();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable Getsupercat2(int idCierre)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCombo.GetSuperCategoria2(idCierre);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetReporteCajaDia()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetReporteCajaDia();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetReporteCajaDia2(int idCierre)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetReporteCajaDia2(idCierre);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetReporteFecha(string desde,string hasta)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetReporteFecha(desde, hasta);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetTopPlatosCierre(string idcierre)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetTopPlatosCierre(idcierre);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetTopPlatos()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetTopPlatos();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetMesa()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetMesa();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetStock()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetStock();
            }
            catch (Exception ex)
            {
                dt = null;
                 //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetCajaChicaCierre(string idcierre,string NomEquipo)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetCajaChicaCierre(idcierre, NomEquipo);
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetPlatosWebService()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datCierre.GetPlatosWebService();
            }
            catch (Exception ex)
            {
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return dt;
        }
        public bool sp_actualizar_plato_web_service(string id_carta,string nom_plato,string precio_plato)
        {
            bool result = false;
            result = datCierre.sp_actualizar_plato_web_service(id_carta, nom_plato, precio_plato);
            return result;
        }
    }
}

using Capa_Datos.Parametros;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Parametros
{
    public class Neg_Parametros
    {
        Dat_Parametros datParametros = new Dat_Parametros();
        Funcion_Global globales = new Funcion_Global();
        public string RutaFe()
        {
            string rutafe = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rutafe = dt.Rows[0][2].ToString();
            return rutafe;
        }
        public string RutaFePrueba()
        {
            string rutafeprueba = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rutafeprueba = dt.Rows[1][2].ToString();
            return rutafeprueba;
        }
        public string RutaFeFirmado()
        {
            string rutafefirmado = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rutafefirmado = dt.Rows[2][2].ToString();
            return rutafefirmado;
        }
        public string RutaFeError()
        {
            string rutafe_error = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rutafe_error = dt.Rows[3][2].ToString();
            return rutafe_error;
        }
        public string NroColumnasTicket()
        {
            string NroColumnasTicket = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            NroColumnasTicket = dt.Rows[4][2].ToString();
            return NroColumnasTicket;
        }
        public string MaxCharDescriptionTicket()
        {
            string MaxCharDescriptionTicket = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            MaxCharDescriptionTicket = dt.Rows[5][2].ToString();
            return MaxCharDescriptionTicket;
        }
        public int margenLogoTicket()
        {
            int margenLogoTicket = 0;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            margenLogoTicket = Convert.ToInt32(dt.Rows[6][2]);
            return margenLogoTicket;
        }
        public int margenMaxDescrip()
        {
            int margenLogoTicket = 0;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            margenLogoTicket = Convert.ToInt32(dt.Rows[17][2]);
            return margenLogoTicket;
        }
        public string margenQRTicket()
        {
            string margenQRTicket = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            margenQRTicket = dt.Rows[7][2].ToString();
            return margenQRTicket;
        }
        public string URLEasyfact()
        {
            string URLEasyfact = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            URLEasyfact = dt.Rows[8][2].ToString();
            return URLEasyfact;
        }
        public decimal IGV()
        {
            decimal igv = 0;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            igv = Convert.ToDecimal(dt.Rows[9][2]);
            return igv;
        }
        public decimal RC()
        {
            decimal rc = 0;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rc = Convert.ToDecimal(dt.Rows[10][2]);
            return rc;
        }
        public decimal BP()
        {
            decimal bp = 0;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            bp = Convert.ToDecimal(dt.Rows[11][2]);
            return bp;
        }
        public string NroPersonas()
        {
            string NroPerson = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            NroPerson = dt.Rows[12][2].ToString();
            return NroPerson;
        }
        public string RutaServicioWebSostic()
        {
            string WebServiceSostic = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            WebServiceSostic = dt.Rows[13][2].ToString();
            return WebServiceSostic;
        }
        public string RutaServicioWebMensaje()
        {
            string WebServiceMensaje = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            WebServiceMensaje = dt.Rows[14][2].ToString();
            return WebServiceMensaje;
        }
        public string ClaveMozoMoverPlato()
        {
            string ClaveMozoMoverPlato = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            ClaveMozoMoverPlato = dt.Rows[17][2].ToString();
            return ClaveMozoMoverPlato;
        }
        public string ClavePedir()
        {
            string ClavePedir = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            ClavePedir = dt.Rows[18][2].ToString();
            return ClavePedir;
        }
        public bool ImprimirSubNiveles()
        {
            bool ImprimirSubNiveles = false;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            ImprimirSubNiveles = Convert.ToBoolean(dt.Rows[19][2]);
            return ImprimirSubNiveles;
        }
        public bool FacturacionElectronica()
        {
            bool FacturacionElectronica = false;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            FacturacionElectronica = Convert.ToBoolean(dt.Rows[20][2]);
            return FacturacionElectronica;
        }
        public double TiempoEnvioDocElectronico()
        {
            double TiempoEnvioDocElectronico = 0;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            TiempoEnvioDocElectronico = Convert.ToDouble(dt.Rows[21][2]);
            return TiempoEnvioDocElectronico;
        }
        public bool ImprimirCajaChica()
        {
            bool ImprimirCajaChica = false;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            ImprimirCajaChica = Convert.ToBoolean(dt.Rows[22][2]);
            return ImprimirCajaChica;
        }
        public bool ImpComAnuladaCaja()
        {
            bool ImprimirCajaChica = false;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            ImprimirCajaChica = Convert.ToBoolean(dt.Rows[23][2]);
            return ImprimirCajaChica;
        }
        public bool ImpComAnuladaPlato()
        {
            bool ImprimirCajaChica = false;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            ImprimirCajaChica = Convert.ToBoolean(dt.Rows[24][2]);
            return ImprimirCajaChica;
        }
        public decimal MaxTamanoImgKb()
        {
            decimal MaxTamanoImgKb = 0;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            MaxTamanoImgKb = Convert.ToDecimal(dt.Rows[29][2]);
            return MaxTamanoImgKb;
        }
        public string getNombreSistema()
        {
            string sistema = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            sistema = dt.Rows[30][2].ToString();
            return sistema;
        }
        public string getIconoSistema()
        {
            string sistema = "";
            try
            {
                
                DataTable dt = new DataTable();
                dt = datParametros.SelectParametros();
                sistema = dt.Rows[33][2].ToString();
                return sistema;
            }
            catch (Exception ex)
            {
                return sistema;
            }
        }
        public string logoPreCuenta()
        {
            string logoprecuenta = "";
            try
            {
                DataTable dt = new DataTable();
                dt = datParametros.SelectParametros();
                logoprecuenta = dt.Rows[34][2].ToString();
                return logoprecuenta;
            }
            catch (Exception ex)
            {
                return logoprecuenta;
            }
        }
        public string logoFacturacion()
        {
            string logofact = "";
            try
            {
                DataTable dt = new DataTable();
                dt = datParametros.SelectParametros();
                logofact = dt.Rows[35][2].ToString();
                return logofact;
            }
            catch (Exception ex)
            {
                return logofact;
            }
        }
        public string tipOperacion()
        {
            string tipOperacion = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosSUNAT();
            tipOperacion = dt.Rows[0][2].ToString();
            return tipOperacion;
        }
        public string codLocalEmisor()
        {
            string codLocalEmisor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosSUNAT();
            codLocalEmisor = dt.Rows[1][2].ToString();
            return codLocalEmisor;
        }
        public string ublVersionId()
        {
            string ublVersionId = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosSUNAT();
            ublVersionId = dt.Rows[2][2].ToString();
            return ublVersionId;
        }
        public string customizationId()
        {
            string customizationId = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosSUNAT();
            customizationId = dt.Rows[3][2].ToString();
            return customizationId;
        }
        public string rutaFacturacion()
        {
            string rutaFacturacion = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosSUNAT();
            rutaFacturacion = dt.Rows[4][2].ToString();
            return rutaFacturacion;
        }
        public ObservableCollection<Capa_Entidades.Models.Parametros.Parametros> GetParametros()
        {
            ObservableCollection<Capa_Entidades.Models.Parametros.Parametros> empresa = new ObservableCollection<Capa_Entidades.Models.Parametros.Parametros>();
            try
            {

                DataTable dt = datParametros.SelectParametros();
                foreach (DataRow row in dt.Rows)
                {
                    Capa_Entidades.Models.Parametros.Parametros _p = new Capa_Entidades.Models.Parametros.Parametros();
                    _p.ID_PAR = Convert.ToInt32(row["ID_PAR"]);
                    _p.NOM_PAR = row["NOM_PAR"].ToString();
                    _p.VALOR_PAR = row["VALOR_PAR"].ToString();
                    _p.EST_PAR = row["EST_PAR"].ToString();
                    _p.FEC_CREAC = Convert.ToDateTime(row["FEC_CREAC"]);
                    empresa.Add(_p);
                }
            }
            catch (Exception ex)
            {
                empresa = null;
                // globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return empresa;
        }

        public string EnvioDoc()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[0][2].ToString();
            return valor;
        }
        public string SolicitarSosManager()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[1][2].ToString();
            return valor;
        }
        public string EnvioDataSosManager()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[2][2].ToString();
            return valor;
        }
        public string DeliveryApp()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[3][2].ToString();
            return valor;
        }
        public string RecojoApp()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[4][2].ToString();
            return valor;
        }
        public string SelectBusiness()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[5][2].ToString();
            return valor;
        }
        public string UpdateBusiness()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[6][2].ToString();
            return valor;
        }
        public string GuardarPlato()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[7][2].ToString();
            return valor;
        }
        public string UpdateState()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[8][2].ToString();
            return valor;
        }
        public string DetPedidos()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[9][2].ToString();
            return valor;
        }
        public string EnvioCierre()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[10][2].ToString();
            return valor;
        }
        public string MesaApp()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[11][2].ToString();
            return valor;
        }
        public string DetPedidosSosFood()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[12][2].ToString();
            return valor;
        }
        public string UpdateStateSosFood()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[13][2].ToString();
            return valor;
        }
        public string GetMetPago()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[14][2].ToString();
            return valor;
        }
        public string GetUpdatePago()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[15][2].ToString();
            return valor;
        }
        public string GetEntPago()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[16][2].ToString();
            return valor;
        }
        public string GetUpdateEntrega()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[17][2].ToString();
            return valor;
        }
        public string GetToken()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[18][2].ToString();
            return valor;
        }
        public string SetToken()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[19][2].ToString();
            return valor;
        }

        public string SetPlatosVendidosHistorial()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[20][2].ToString();
            return valor;
        }
        public string SetPlatosVendidos()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[21][2].ToString();
            return valor;
        }
        public string CambioMesaWebService()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[22][2].ToString();
            return valor;
        }
        public string EstadoPedidoWebService()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[23][2].ToString();
            return valor;
        }
        public string GuardarCat()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[24][2].ToString();
            return valor;

        }
        public string GuardarMesas()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[25][2].ToString();
            return valor;

        }
        public string GuardarStock()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[26][2].ToString();
            return valor;

        }


        public string UploadAnulacion()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[30][2].ToString();
            return valor;
        }
        public bool EnvioUploadAnulacion()
        {
            bool EnvioUploadAnulacion = false;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            EnvioUploadAnulacion = Convert.ToBoolean(dt.Rows[25][2]);
            return EnvioUploadAnulacion;
        }

        public int CountPrintLlevar()
        {

            int CountPrintLlevar = 0;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            CountPrintLlevar = Convert.ToInt32(dt.Rows[27][2]);
            return CountPrintLlevar;


        }


        public int CountPrintDelivery()
        {
            int CountPrintDelivery = 0;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            CountPrintDelivery = Convert.ToInt32(dt.Rows[28][2]);
            return CountPrintDelivery;
        }
        public string RappiAbrirTienda()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[31][2].ToString();
            return valor;

        }
        public string CajaChicaCloud()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[32][2].ToString();
            return valor;

        }

        public ObservableCollection<Capa_Entidades.Models.Parametros.Parametros> GetParametros_2()
        {
            ObservableCollection<Capa_Entidades.Models.Parametros.Parametros> empresa = new ObservableCollection<Capa_Entidades.Models.Parametros.Parametros>();
            try
            {

                DataTable dt = datParametros.SelectParametros2();
                foreach (DataRow row in dt.Rows)
                {
                    Capa_Entidades.Models.Parametros.Parametros _p = new Capa_Entidades.Models.Parametros.Parametros();
                    _p.IDTIPO = row["IDTIPO"].ToString();
                    _p.ID_PAR = Convert.ToInt32(row["ID_PAR"]);
                    _p.NOM_PAR = row["NOM_PAR"].ToString();
                    _p.VALOR_PAR = row["VALOR_PAR"].ToString();
                    _p.activo = row["activo"].ToString();
                    _p.est_activo = row["est_activo"].ToString();
                    //if (_p.activo == "1")
                    //{
                    //    _p.activo_string = "Activo";
                    //}
                    //else if (_p.activo == "0")
                    //{
                    //    _p.activo_string = "Inactivo";
                    //}
                    //else {
                    //    _p.activo_string = _p.activo;
                    //}
                    _p.tipoParametro = Convert.ToInt32(row["TipoParametro"]);
                    empresa.Add(_p);
                }
            }
            catch (Exception ex)
            {
                empresa = null;
                // globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return empresa;
        }
        public ObservableCollection<Capa_Entidades.Models.Parametros.Parametros> GetParametrosImpuestos()
        {
            ObservableCollection<Capa_Entidades.Models.Parametros.Parametros> empresa = new ObservableCollection<Capa_Entidades.Models.Parametros.Parametros>();
            try
            {

                DataTable dt = datParametros.GetParametrosImpuestos();
                foreach (DataRow row in dt.Rows)
                {
                    Capa_Entidades.Models.Parametros.Parametros _p = new Capa_Entidades.Models.Parametros.Parametros();
                    _p.IDTIPO = row["IDTIPO"].ToString();
                    _p.ID_PAR = Convert.ToInt32(row["ID_PAR"]);
                    _p.NOM_PAR = row["NOM_PAR"].ToString();
                    _p.VALOR_PAR = row["VALOR_PAR"].ToString();
                    _p.tipoParametro = Convert.ToInt32(row["TIPOPAR"]);
                    _p.est_activo = row["est_activo"].ToString();
                    empresa.Add(_p);
                }
            }
            catch (Exception ex)
            {
                empresa = null;
                // globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return empresa;
        }

        public bool SetParametros(Capa_Entidades.Models.Parametros.Parametros parametros)
        {
            bool result = false;
            result = datParametros.SetParametros(parametros);
            return result;
        }
        public bool Set_Parametros_Impuestos(Capa_Entidades.Models.Parametros.Parametros parametros)
        {
            bool result = false;
            result = datParametros.Set_Parametros_Impuestos(parametros);
            return result;
        }
        public string DocElectronicosFaltantes()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[27][2].ToString();
            return valor;

        }
        public string PlatosWebService()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[28][2].ToString();
            return valor;

        }
        public string PlatosWebServiceUpdate()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametrosWebService();
            valor = dt.Rows[29][2].ToString();
            return valor;

        }
        public string GetTipoCambio()
        {
            string valor = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectTipoMoneda();
            valor = dt.Rows[0][1].ToString();
            return valor;
        }
        public bool TipoDocPagar()
        {
            bool TipoDocPagar = false;
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            TipoDocPagar = Convert.ToBoolean(dt.Rows[26][2]);
            return TipoDocPagar;
        }
    }
}

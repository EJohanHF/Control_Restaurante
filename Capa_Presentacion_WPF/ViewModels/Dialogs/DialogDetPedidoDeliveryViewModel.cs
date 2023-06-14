using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogDetPedidoDeliveryViewModel
    {
        public List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebServiceDetalles> dataDeliveryWebServiceDet { get; set; }
        public string cod_orden { get; set; }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Parametros negParametros = new Neg_Parametros();
        public DialogDetPedidoDeliveryViewModel()
        {
            try {
                bool internet = negFuncionGlobal.ValidarInternet();
                if (internet == false) {
                    string Estado = "Estimado Cliente:\n" +
                    "actualmente usted no cuenta con conexión a internet por favor registre manualmente los datos de su cliente.";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", Estado, 2);
                }
                cod_orden = Application.Current.Properties["cod_orden"].ToString();
                if(cod_orden != null || cod_orden != "")
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        //values["token"] = "app963";
                        values["orderid"] = cod_orden;

                        var response = client.UploadValues(negParametros.DetPedidos(), values);
                        var responseString = Encoding.Default.GetString(response);
                        var WebService = new List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebServiceDetalles>();
                        //responseString = responseString.Replace("}", "},");
                        WebService = JsonConvert.DeserializeObject<List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebServiceDetalles>>(responseString);

                        //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                        this.dataDeliveryWebServiceDet = WebService;

                    }
                }   
            }
            catch (Exception ex)
            {
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
        }
    }
}

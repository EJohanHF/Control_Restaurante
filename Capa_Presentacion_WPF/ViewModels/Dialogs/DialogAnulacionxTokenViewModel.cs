using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogAnulacionxTokenViewModel
    {
        public ICommand CerrarDialog { get; set; }
        public ICommand Guardar { get; set; }
        public string token { get; set; }
        public DialogAnulacionxTokenViewModel()
        {
            this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
            this.Guardar = new ParamCommand(new Action<object>(GuardarToken));
        }
        private void CloseDialog()
        {
            Application.Current.Properties["ValidacionToken"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
            token = "";
        }
        public ObservableCollection<Empresa> empresa { get; set; }
        Neg_Empresa negEmpresa = new Neg_Empresa();
        Neg_Parametros negParametros = new Neg_Parametros();
        public List<Capa_Entidades.Models.Web_Service.SosFoodToken> dataDeliveryWebServiceSosFood { get; set; }
        private void GuardarToken(object parameter)
        {
            try
            {
                var textBox = parameter as TextBox;
                var texto = textBox.Text;
                token = texto;
                if (token.ToString().Trim() == "")
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese token", 2);
                    return;
                }
                empresa = negEmpresa.GetEmpresa();
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    //values["token"] = "app963";
                    values["id"] = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().codigo;

                    var response = client.UploadValues(negParametros.GetToken(), values);
                    var responseString = Encoding.Default.GetString(response);
                    var WebService = new List<Capa_Entidades.Models.Web_Service.SosFoodToken>();
                    //responseString = responseString.Replace("}", "},");
                    WebService = JsonConvert.DeserializeObject<List<Capa_Entidades.Models.Web_Service.SosFoodToken>>(responseString);

                    //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                    this.dataDeliveryWebServiceSosFood = WebService;

                }
                if (dataDeliveryWebServiceSosFood == null)
                {
                    Application.Current.Properties["ValidacionToken"] = null;
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "El token no existe.", 2);
                    return;
                }
                if (dataDeliveryWebServiceSosFood.Count == 0)
                {
                    Application.Current.Properties["ValidacionToken"] = null;
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "El token no existe.", 2);
                    return;
                }
                if (dataDeliveryWebServiceSosFood.Count > 0)
                {
                    if (token == dataDeliveryWebServiceSosFood.FirstOrDefault().token)
                    {
                        Application.Current.Properties["ValidacionToken"] = "SI";
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                    else
                    {
                        Application.Current.Properties["ValidacionToken"] = null;
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Token Invalido", 2);
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                Application.Current.Properties["ValidacionToken"] = null;
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Error al validar el token.", 2);
                return;

            }
            
        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
    }
}

using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogPagoDeliveryViewModel : IGeneric
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Parametros negParametros = new Neg_Parametros();
        public string NomPago { get; set; }
        public bool EstPago { get; set; }
        public string IdPago { get; set; }
        public string IdBusinessPago { get; set; }
        public string id_emp { get; set; }
        public ICommand CerrarDialog { get; set; }
        public ICommand Guardar { get; set; }
        public DialogPagoDeliveryViewModel()
        {
            this.NomPago = Application.Current.Properties["NomPago"].ToString();
            this.EstPago = Convert.ToBoolean(Application.Current.Properties["EstPago"]);
            this.IdPago = Application.Current.Properties["IdPago"].ToString();
            this.IdBusinessPago = Application.Current.Properties["IdBusinessPago"].ToString();
            this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
            this.Guardar = new RelayCommand(new Action(GuardarEmpresaDelivery));
        }
        private void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public async void GuardarEmpresaDelivery()
        {
            
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["id"] = IdPago;
                    values["id_business"] = IdBusinessPago;
                    values["nom_metodo"] = NomPago;
                    if (EstPago)
                    {
                        values["estado"] = "1";

                    }
                    else
                    {
                        values["estado"] = "0";
                    }
                    //values["price"] = this.cartadelivery.price.ToString();
                    //values["imagen"] = this.cartadelivery.imagen.ToString();
                    //values["id_carta"] = this.IdCarta.ToString();
                    //values["estado"] = "1";


                    var response = client.UploadValues(negParametros.GetUpdatePago(), values);
                    var responseString = Encoding.Default.GetString(response);
                    //var WebService = new RespuestaWebService();

                    //WebService = JsonConvert.DeserializeObject<RespuestaWebService>(responseString);





                }
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {

                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }

        }
    }
}

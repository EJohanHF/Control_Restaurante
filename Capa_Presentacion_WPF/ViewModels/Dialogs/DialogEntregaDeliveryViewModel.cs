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
    public class DialogEntregaDeliveryViewModel
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Parametros negParametros = new Neg_Parametros();
        public string NomEntrega { get; set; }
        public bool EstEntrega { get; set; }
        public string IdEntrega { get; set; }
        public string IdEntregaPago { get; set; }
        public string id_emp { get; set; }
        public ICommand CerrarDialog { get; set; }
        public ICommand Guardar { get; set; }
        public DialogEntregaDeliveryViewModel()
        {
            this.NomEntrega = Application.Current.Properties["NomEntrega"].ToString();
            this.EstEntrega = Convert.ToBoolean(Application.Current.Properties["EstEntrega"]);
            this.IdEntrega = Application.Current.Properties["IdEntrega"].ToString();
            this.IdEntregaPago = Application.Current.Properties["IdBusinessEntrega"].ToString();
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
                    values["id"] = IdEntrega;
                    values["id_business"] = IdEntregaPago;
                    values["valor"] = NomEntrega;
                    if (EstEntrega)
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


                    var response = client.UploadValues(negParametros.GetUpdateEntrega(), values);
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

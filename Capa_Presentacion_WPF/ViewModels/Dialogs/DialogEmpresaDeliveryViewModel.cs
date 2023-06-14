using Capa_Entidades.Models.Web_Service;
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
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogEmpresaDeliveryViewModel : IGeneric
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Parametros negParametros = new Neg_Parametros();
        public ICommand CerrarDialog { get; set; }
        public ICommand Guardar { get; set; }
        public string idempresa { get; set; }
        public ObservableCollection<EmpresaDelivery> DataEmpresaDelivery { get; set; }
        private List<EmpresaDelivery> empresadelivery;
        private EmpresaDelivery empresadelivery2;
        public List<EmpresaDelivery> EmpresaDelivery
        {
            get => empresadelivery;
            set
            {
                empresadelivery = value;
            }
        }
        public EmpresaDelivery EmpresaDelivery2
        {
            get => empresadelivery2;
            set
            {
                empresadelivery2 = value;
            }
        }
        public DialogEmpresaDeliveryViewModel()
        {
            try
            {
                idempresa = Application.Current.Properties["Idempresa"].ToString();
                this.DataEmpresaDelivery = new ObservableCollection<EmpresaDelivery>();
                this.EmpresaDelivery2 = new EmpresaDelivery();
                this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
                this.Guardar = new RelayCommand(new Action(GuardarEmpresaDelivery));
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    //values["token"] = "app963";
                    values["id"] = idempresa;

                    var response = client.UploadValues(negParametros.SelectBusiness(), values);
                    var responseString = Encoding.Default.GetString(response);
                    //responseString = responseString.Replace("}", "},");
                    EmpresaDelivery = JsonConvert.DeserializeObject<List<EmpresaDelivery>>(responseString);
                    var empresa = new EmpresaDelivery();
                    empresa.id = EmpresaDelivery.FirstOrDefault().id;
                    empresa.ruc = EmpresaDelivery.FirstOrDefault().ruc;
                    empresa.nombre_razon = EmpresaDelivery.FirstOrDefault().nombre_razon;
                    empresa.direccion = EmpresaDelivery.FirstOrDefault().direccion;
                    empresa.latitud = EmpresaDelivery.FirstOrDefault().latitud;
                    empresa.longitud = EmpresaDelivery.FirstOrDefault().longitud;
                    empresa.tiempo_espera = EmpresaDelivery.FirstOrDefault().tiempo_espera;

                    this.DataEmpresaDelivery.Add(empresa);
                    this.EmpresaDelivery2 = this.DataEmpresaDelivery.FirstOrDefault();
                }

               
            }
            catch (Exception ex)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion","No se encontro datos", 2);
            }
            
        }
        private void CloseDialog()
        {
            Application.Current.Properties["Idempresa"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public async void GuardarEmpresaDelivery()
        {
            EmpresaDelivery Empresa = this.EmpresaDelivery2;
                try
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                    values["id"] = Empresa.id;
                    values["tiempo_espera"] = Empresa.tiempo_espera;
                    values["ruc"] = Empresa.ruc;
                    values["nombre_razon"] = Empresa.nombre_razon;
                    values["direccion"] = Empresa.direccion;
                    //values["price"] = this.cartadelivery.price.ToString();
                    //values["imagen"] = this.cartadelivery.imagen.ToString();
                    //values["id_carta"] = this.IdCarta.ToString();
                    //values["estado"] = "1";


                    var response = client.UploadValues(negParametros.UpdateBusiness(), values);
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

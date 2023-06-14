﻿using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Presentacion_WPF.Views.Dialogs;
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

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.MantenimientoApp
{
    public class MantenimientoAppSosDeliveryEntregaViewModel
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Parametros negParametros = new Neg_Parametros();
        static ObservableCollection<Empresa> empresa = new ObservableCollection<Empresa>();
        Neg_Empresa negEmpr = new Neg_Empresa();
        public string cod_orden { get; set; }
        public ICommand EditarCommandListo { get; set; }
        public List<Capa_Entidades.Models.MantimientoAppSosDeliveryPeru.MantenimientoEntregaApp> dataDeliveryPago { get; set; }
        public MantenimientoAppSosDeliveryEntregaViewModel()
        {
            try
            {
                this.EditarCommandListo = new ParamCommand(new Action<object>(Editar));
                CargarData();


            }
            catch (Exception ex)
            {

                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
        }
        public void CargarData()
        {
            empresa = negEmpr.GetEmpresa();
            cod_orden = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
            if (cod_orden != null || cod_orden != "")
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    //values["token"] = "app963";
                    values["id"] = cod_orden;
                    var response = client.UploadValues(negParametros.GetEntPago(), values);
                    var responseString = Encoding.Default.GetString(response);

                    var WebService2 = new List<Capa_Entidades.Models.MantimientoAppSosDeliveryPeru.MantenimientoEntregaApp>();
                    //responseString = responseString.Replace("}", "},");
                    WebService2 = JsonConvert.DeserializeObject<List<Capa_Entidades.Models.MantimientoAppSosDeliveryPeru.MantenimientoEntregaApp>>(responseString);

                    //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                    this.dataDeliveryPago = WebService2;

                }
            }
        }
        private async void Editar(object id)
        {
            empresa = negEmpr.GetEmpresa();
            Application.Current.Properties["NomEntrega"] = this.dataDeliveryPago.Where(p => p.id == id).FirstOrDefault().valor;
            Application.Current.Properties["EstEntrega"] = this.dataDeliveryPago.Where(p => p.id == id).FirstOrDefault().estado;
            Application.Current.Properties["IdEntrega"] = id;
            Application.Current.Properties["IdBusinessEntrega"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString(); ;
            var SiNo = new DialogEditarEntrega
            {

            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            CargarData();
        }
    }
}

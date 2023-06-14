using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Web_Service;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.WebService;
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

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Mensaje
{
    public class MensajeViewModel
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string Fecha_Mensaje { get; set; }
        public string Tipo_Mensaje { get; set; }
        public Byte[] Imagen { get; set; }
        public ObservableCollection<ConsultaMensaje> DataMensaje { get; set; }
        public Neg_ConsultaMensaje mensaje = new Neg_ConsultaMensaje();
        public ICommand Aceptar { get; set; }
        Neg_Empresa negEmpr = new Neg_Empresa();
        static ObservableCollection<Empresa> empresa = new ObservableCollection<Empresa>();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public MensajeViewModel()
        {
            this.Aceptar = new RelayCommand(new Action(Leido));
            empresa = negEmpr.GetEmpresa();
            if (Application.Current.Properties["IdMensaje"] != null)
            {
                DataMensaje = mensaje.GeMensaje();
                Titulo = DataMensaje.Where(m => m.id_unique_messages == Application.Current.Properties["IdMensaje"].ToString()).FirstOrDefault().title_messages.ToUpper();
                Contenido = DataMensaje.Where(m => m.id_unique_messages == Application.Current.Properties["IdMensaje"].ToString()).FirstOrDefault().text_messages;
                Fecha_Mensaje = DataMensaje.Where(m => m.id_unique_messages == Application.Current.Properties["IdMensaje"].ToString()).FirstOrDefault().date_messages;
                Tipo_Mensaje = DataMensaje.Where(m => m.id_unique_messages == Application.Current.Properties["IdMensaje"].ToString()).FirstOrDefault().type_messages;
                Imagen = DataMensaje.Where(m => m.id_unique_messages == Application.Current.Properties["IdMensaje"].ToString()).FirstOrDefault().image_business;
                if (Tipo_Mensaje == "1")
                {
                    Tipo_Mensaje = " MENSAJE PERSONAL";
                }
                else
                {
                    Tipo_Mensaje = " MENSAJE GLOBAL";
                }
            }
        }
        private void Leido()
        {
            var codigo = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo;
            var token = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().token;
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["cod_empresa"] = codigo;
                    values["token"] = token;
                    values["id_unique_messages"] = Application.Current.Properties["IdMensaje"].ToString();
                    var response = client.UploadValues("http://cierres.sos-food.com/confirmation/", values);
                    var responseString = Encoding.Default.GetString(response);
                    var WebService = new ConsultaMensaje();
                    WebService = JsonConvert.DeserializeObject<ConsultaMensaje>(responseString);
                    if (WebService == null)
                    {

                    }
                    else
                    {
                        if (responseString != "" && WebService.messages_response == "OK")
                        {
                                bool result = mensaje.SetMensajeLeido(Convert.ToInt32(Application.Current.Properties["IdMensaje"]));
                        }
                    }
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                
            }
            catch (Exception ex)
            {
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
                 DialogHost.CloseDialogCommand.Execute(null, null);
            }
           
        }
    }
}

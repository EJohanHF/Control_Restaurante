using Capa_Datos.WebService;
using Capa_Entidades.Models.Web_Service;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.WebService
{
    public class Neg_ConsultaMensaje
    {
        Dat_ConsultaMensaje datMensaje = new Dat_ConsultaMensaje();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<ConsultaMensaje> GeMensaje()
        {
            ObservableCollection<ConsultaMensaje> mensaje = new ObservableCollection<ConsultaMensaje>();
            try
            {
                DataTable dt = datMensaje.GetMensaje();
                foreach (DataRow row in dt.Rows)
                {
                    ConsultaMensaje _mensaje = new ConsultaMensaje();
                    _mensaje.id_unique_messages = row["id_unique_messages"].ToString();
                    _mensaje.title_messages = row["title_messages"].ToString();
                    _mensaje.text_messages = row["text_messages"].ToString();
                    _mensaje.date_messages = row["date_messages"].ToString();
                    _mensaje.cod_business = row["cod_business"].ToString();
                    _mensaje.executed = row["executed"].ToString();
                    _mensaje.received = row["received"].ToString();
                    _mensaje.type_messages = row["type_messages"].ToString();
                    _mensaje.image_business = (Byte[])row["image_business"];
                    _mensaje.leido = row["leido"].ToString();
                    _mensaje.text_messages_abrev = row["text_messages_abrev"].ToString();
                    mensaje.Add(_mensaje);
                }
            }
            catch (Exception ex)
            {
                mensaje = null;
               // globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return mensaje;
        }
        public bool SetMensaje(ConsultaMensaje WebService)
        {
            bool result = false;
            result = datMensaje.sp_set_mensaje(WebService.id_unique_messages, WebService.title_messages, WebService.text_messages, WebService.date_messages, WebService.cod_business, WebService.executed,
                                WebService.received, WebService.type_messages, WebService.image_business);
            return result;
        }
        public bool SetMensajeLeido(int id_mensaje)
        {
            bool result = false;
            result = datMensaje.sp_set_mensaje_leido(id_mensaje);
            return result;
        }
    }
}  
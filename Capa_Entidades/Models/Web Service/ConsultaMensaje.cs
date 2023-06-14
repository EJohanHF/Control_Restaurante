using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Web_Service
{
    public class ConsultaMensaje
    {
        public ConsultaMensaje()
        {

        }
        public string id_unique_messages { get; set; }
        public string title_messages { get; set; }
        public string text_messages { get; set; }
        public string date_messages { get; set; }
        public string cod_business { get; set; }
        public string executed { get; set; }
        public string received { get; set; }
        public string type_messages { get; set; }
        public string leido { get; set; } 
        public Byte[] image_business { get; set; }
        public string text_messages_abrev { get; set; }
        public string messages_response { get; set; }
    }
}
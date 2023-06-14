using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models
{
    public class ConsultaWebService
    {
        public string movil { get; set; }
        public string fijo { get; set; }
        public string correo { get; set; }
        public Byte[] imagen_empresa { get; set; }
        public Byte[] imagen_producto { get; set; }
        public int cambio_info { get; set; }
    }
    public class DataReq
    {
        public string cod_empresa { get; set; }
        public string token { get; set; }
    }
}

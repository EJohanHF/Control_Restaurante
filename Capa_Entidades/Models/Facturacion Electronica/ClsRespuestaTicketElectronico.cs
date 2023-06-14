using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Facturacion_Electronica
{
    public class ClsRespuestaTicketElectronico
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string numeroDocumentoEmisor { get; set; }
        public string tipoDocumento { get; set; }
        public string serieNumero { get; set; }
        public string serie { get; set; }
        public string numero { get; set; }
        public string hash { get; set; }
        public string signature { get; set; }
        public string fechaHoraRegistro { get; set; }
        public ClsRespuestaTicketElectronico()
        {

        }
    }
}

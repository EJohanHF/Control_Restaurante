using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Facturacion_Electronica
{
    public class Tipo_Doc_Electronico
    {
        public int ID { get; set; }
        public string NOM_TIPO_DOC { get; set; }
        public string VALOR_TIPO_DOC { get; set; }
        public string EST_TIP_DOC { get; set; }
        public Tipo_Doc_Electronico()
        {

        }
    }
}

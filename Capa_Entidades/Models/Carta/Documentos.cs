using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Carta
{
    public class Documentos
    {
        public Documentos()
        {

        }

        public int ID { get; set; }
        public int DI_ID_DOCUMENTO { get; set; }
        public string DOC_NOM { get; set; }
        public int DI_ID_IMP { get; set; }
        public string NOMIMP { get; set; }
        public bool DI_ACT { get; set; }
        public bool DOC_ACT { get; set; }

        public string impresoras { get; set; }
    }
}

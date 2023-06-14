using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Stock
{
    public class ConsumoInterno
    {
        public int ID { get; set; }
        public int CI_ID_EMPLEADO { get; set; }
        public string EMPL_NOM { get; set; }
        public int CI_ID_TIPO_CONSUMO { get; set; }
        public string TC_DESCR { get; set; }
        public decimal CI_CANT { get; set; }
        public int CI_ID_CARTA { get; set; }
        public string CAR_NOM { get; set; }
        public int CI_ID_INSUMO { get; set; }
        public string INS_NOM { get; set; }
        public DateTime CI_F_CREATE { get; set; }
        public DateTime CI_F_MODIFICACION { get; set; }
        public string CI_OBS { get; set; }

    }
}

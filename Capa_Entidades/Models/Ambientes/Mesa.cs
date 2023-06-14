using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Ambientes
{
    public class Mesa
    {
        public int ID { get; set; }
        public string M_NOM { get; set; }
        public int M_EST { get; set; }
        public int M_ID_AMB { get; set; }
        public string M_X { get; set; }
        public int M_ATENDIDA { get; set; }
        public int M_WIDTH { get; set; }
        public int M_HEIGHT { get; set; }
        public string M_TEXTO { get; set; }
        public int M_TIPO { get; set; }
        public Boolean M_ACT { get; set; }
        public DateTime M_F_CREATE { get; set; }
        public DateTime M_F_MODIFICACION { get; set; }
        public int M_ID_PADRE { get; set; }
        public string color { get; set; }
        public string NOMBRE_PADRE { get; set; }
    }
}

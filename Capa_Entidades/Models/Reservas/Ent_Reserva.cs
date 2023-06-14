using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Reservas
{
    public class Ent_Reserva
    {
        //SF_RESERVA
        public int ID { get; set; }
        public int R_ID_CLIENTE { get; set; }
        public string C_NOMINA { get; set; }
        public string C_NRO_DOC { get; set; }
        public string C_TEL { get; set; }
        public string C_COR { get; set; }
        public int R_ID_USUARIO { get; set; }
        public string USU_NOM { get; set; }
        public int R_ID_MESA { get; set; }
        public int R_ID_AMBIENTE { get; set; }
        public string M_NOM { get; set; }
        public DateTime R_F_RESERVA_DESDE { get; set; }
        public DateTime R_F_RESERVA_HASTA { get; set; }
        public int R_ID_ESTADO { get; set; }
        public string ER_DESCR { get; set; }
        public string R_OBS { get; set; } = "";
        public int R_ID_TIPO_RESERVA { get; set; }
        public string R_DESCR { get; set; }
        public DateTime R_F_CREATE { get; set; }
        public decimal R_AMORTIZADO { get; set; } = 0;


        //SF_TIPO_RESERVA
        public int TR_ID { get; set; }
        public string TR_DESCR { get; set; }
        public bool TR_ACT { get; set; }
        public DateTime TR_F_CREATE { get; set; }
        public DateTime TR_F_MODIFICACION { get; set; }

        //ESTADO DE RESERVA
        public int ER_ID { get; set; }
        public string ER_DESCRIPCION { get; set; }
        public bool ER_ACT { get; set; }
        public DateTime ER_F_CREATE { get; set; }
        public DateTime ER_F_MODIFICACION { get; set; }
        public bool check { get; set; }
    }
}

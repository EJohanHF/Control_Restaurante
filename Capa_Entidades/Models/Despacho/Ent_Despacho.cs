using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Despacho
{
    public class Ent_Despacho
    {
        public int ID { get; set; }
        public int PED_ID_MESA { get; set; }
        public string M_NOM { get; set; }
        public int PED_ID_EMPL { get; set; }
        public DateTime PED_FECH_PED { get; set; }
        public int PED_ID_ESTADO { get; set; }
        public decimal PED_IMPORTE { get; set; }
        public decimal PED_DESCUENTO { get; set; }
        public int PED_ID_TIP_DESC { get; set; }
        public decimal PED_TOTAL { get; set; }
        public int PED_ID_CLIENTE { get; set; }
        public int PED_ID_CIERRE { get; set; }
        public int PED_ID_USU { get; set; }
        public int PED_ID_TURNO { get; set; }
        public DateTime PED_FECH_MODIFI { get; set; }
        public int PED_ID_CAMBIO_MONE { get; set; }
        public int PED_NUM_DIARIO { get; set; }
        public decimal PED_SUBTOTAL { get; set; }
        public int PED_NRO_PERSONAS { get; set; }
        public string nomllevar { get; set; }
        public string telefcli { get; set; }
        public string C_NOMINA { get; set; }
        public string C_TEL { get; set; }
        public string C_DIREC { get; set; }
        public string ColorSeleccionado { get; set; }

    }
}

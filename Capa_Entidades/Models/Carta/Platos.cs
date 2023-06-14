using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Carta
{
    public class Platos
    {
        public Platos()
        {

        }
        public int idplato { get; set; }
        public string idcat { get; set; }
        public string nomcat { get; set; }
        public string idgrup { get; set; }
        public string nomgrup { get; set; }
        public string idscat { get; set; }
        public string nomscat { get; set; }
        public byte idimpre { get; set; }
        public string nombimpre { get; set; }
        public string nomplato { get; set; }
        public object precplato { get; set; }
        public string id_niveles { get; set; }
        public string nivelplato { get; set; }
        public byte estadoplato { get; set; } = 1;
        public string porcionplato { get; set; }
        public string idproditem { get; set; }
        public string nomproditem { get; set; }
        public string impresoras { get; set; }
        public string cantidad { get; set; }
        public string importe { get; set; }
        public string comentario { get; set; }
        public string idCartaSubNivel { get; set; } = "";
        public int orden { get; set; }
        public string idUnidadMedida { get; set; }
        public bool ischeck { get; set; }
        public int cant { get; set; } // lo uso para los niveles
        public bool estadoD { get; set; }
        public byte RC { get; set; } = 0;
        public byte exonerada { get; set; } = 0;
        public object precplato_delivery { get; set; }
        public byte estadoplato_delivery { get; set; } = 1;
        public byte[] imgplato { get; set; }
        public byte cloud { get; set; } = 1;
        public string descrip_plato { get; set; }
        public string cbarplato { get; set; }


    }
}
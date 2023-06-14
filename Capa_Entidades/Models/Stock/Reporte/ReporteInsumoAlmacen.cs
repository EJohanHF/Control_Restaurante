using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades.Models.Stock.Reporte
{
    public class ReporteInsumoAlmacen
    {
        //ia.ID, a.ALM_NOM, i.INS_NOM, ia.CANT
        public int id{ get; set; }
        public int id_insum { get; set; } 
        public int id_alm { get; set; } 
        public string nomal { get; set; }
        public string nomins { get; set; } 
        public string umed { get; set; } 
        public decimal cant { get; set; } 

        public DateTime desde { get; set; } = DateTime.Now;

        public DateTime hasta { get; set; } = DateTime.Now;

        #region AlamcenCant
        public string AC_NomAlm { get; set; }
        public object AC_Cant_Prod { get; set; }
        #endregion
        #region almacen det insummo
        public string AI_NomInsumo { get; set; }
        public object AI_Cant_Prod { get; set; }
        public string AI_NomAlmacen { get; set; }
        public string AI_UM_Deno { get; set; }
        #endregion
        #region AlmacenSelected
        //public int id { get; set; }
        //public int id_insum { get; set; }
        //public int id_alm { get; set; }
        //public string nomal { get; set; }
        //public string nomins { get; set; }
        //public decimal cant { get; set; }
        #endregion
    }
}

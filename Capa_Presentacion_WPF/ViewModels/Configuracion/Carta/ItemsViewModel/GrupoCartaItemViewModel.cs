using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Carta.ItemsViewModel
{
    public class GrupoCartaItemViewModel : IGeneric
    {
        public int ID { get; set; }
        public string IDSCAT { get; set; }
        public string NOMSCAT { get; set; }
        public string IDCAT { get; set; }
        public string NOMCAT { get; set; }
        public string NOMGRUP { get; set; }
        public byte[] IMAGENGRUP { get; set; }
        public decimal DESCGRUP { get; set; } = 0;
        public GrupoCartaItemViewModel(int _ID, string _IDSCAT, string _NOMSCAT, string _IDCAT, string _NOMCAT,string _NOMGRUP, byte[] _IMAGENGRUP, decimal _DESCGRUP)
        {
            this.ID = _ID;
            this.IDSCAT = _IDSCAT;
            this.NOMSCAT = _NOMSCAT;
            this.IDCAT = _IDCAT;
            this.NOMCAT = _NOMCAT;
            this.NOMGRUP = _NOMGRUP;
            this.IMAGENGRUP = _IMAGENGRUP;
            this.DESCGRUP = _DESCGRUP;
        }
    }
}

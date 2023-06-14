using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Carta.ItemsViewModel
{
    public class CategoriaCartaItemViewModel : IGeneric
    {
        public int ID { get; set; }
        public string IDSCAT { get; set; }
        public string NOMSCAT { get; set; }
        public string NOMCAT { get; set; }
        public decimal DESCCAT { get; set; } = 0;
        public byte[] IMAGENCAT { get; set; }
        public int COLUMNA { get; set; }
        public CategoriaCartaItemViewModel(int _ID, string _IDSCAT, string _NOMSCAT, string _NOMCAT, decimal _DESCCAT, byte[] _IMAGENCAT,int _columna)
        {
            this.ID = _ID;
            this.IDSCAT = _IDSCAT;
            this.NOMSCAT = _NOMSCAT;
            this.NOMCAT = _NOMCAT;
            this.DESCCAT = _DESCCAT;
            this.IMAGENCAT = _IMAGENCAT;
            this.COLUMNA = _columna;
        }
    }
    
}

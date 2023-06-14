using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Carta.ItemsViewModel
{
   public class PlatosItemViewModel :IGeneric
    {
        public int idchek { get; set; } 
        public string nomchek { get; set; }
        public bool value { get; set; }
        #region Public Commands
        public ICommand ExpandCommand { get; set; }
        #endregion
        public PlatosItemViewModel(int idchek,string nomchek ,bool value)
        {
            this.idchek = idchek;
            this.nomchek = nomchek;
            this.value = value;
        }
        public PlatosItemViewModel(string nomchek)
        {
            this.nomchek = nomchek;
        }
        
    }
}

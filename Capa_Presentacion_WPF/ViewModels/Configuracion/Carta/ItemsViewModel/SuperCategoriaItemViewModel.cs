using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Carta.ItemsViewModel
{
    public class SuperCategoriaItemViewModel : IGeneric
    {
        #region Public Properties 
        public int ID { get; set; }
        public string NOM { get; set; }
        #endregion
        public SuperCategoriaItemViewModel(int ID, string S_NOM)
        {
            this.ID = ID;
            this.NOM = S_NOM;
        }
    }
}

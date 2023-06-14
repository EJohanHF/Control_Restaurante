using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Ambientes
{
    public class AmbientesItemViewModel : IGeneric
    {
        #region Public Properties 
        public int ID { get; set; }
        public string A_NOM { get; set; }
        public int A_X { get; set; }
        public int A_Y { get; set; }
        public int A_WIDTH { get; set; }
        public int A_HEIGHT { get; set; }
        public string A_TEXTO { get; set; }
        public Boolean A_ACT { get; set; }
        public DateTime A_F_CREATE { get; set; }
    
        public ObservableCollection<AmbientesItemViewModel> Children { get; set; }
        #endregion
        #region Public Commands
        public ICommand ExpandCommand { get; set; }
        #endregion
        #region Constructor
        public AmbientesItemViewModel(int ID, string A_NOM, int A_X, int A_Y, int A_WIDTH, int A_HEIGHT, string A_TEXTO, Boolean A_ACT, DateTime A_F_CREATE)
        {
            //this.ExpandCommand = new RelayCommand(Expand);
            this.ID = ID;
            this.A_NOM = A_NOM;
            this.A_X = A_X;
            this.A_Y = A_Y;
            this.A_WIDTH = A_WIDTH;
            this.A_HEIGHT = A_HEIGHT;
            this.A_TEXTO = A_TEXTO;
            this.A_ACT = A_ACT;
            this.A_F_CREATE = A_F_CREATE;
           
            //this.ClearChildren();
        }

        public AmbientesItemViewModel(string nombre)
        {
            this.A_NOM = nombre;
        }
        #endregion
    }
    public class nose : IGeneric
    {
        String texto { get; set; }
        public nose(String texto)
        {
            this.texto = texto;
        }
    }
}

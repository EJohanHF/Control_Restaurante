using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Ambientes
{
    public class MesasItemViewModel : IGeneric
    {
        #region Public Properties 
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
        public int NroColumnas { get; set; }
        public int isreservada { get; set; } = 0;
        public int PED_ID_CLIENTE { get; set; } = 0;
        public string C_NOMINA { get; set; }
        public string nomllevar  { get; set; }
        public string EMPL_NOM { get; set; }
        public int mesa { get; set; }
        public ObservableCollection<MesasItemViewModel> Children { get; set; }
        #endregion
        #region Constructor
        public MesasItemViewModel(int ID, String M_NOM, int M_EST, int M_ID_AMB, string M_X, int M_ATENDIDA, int M_WIDTH, 
            int M_HEIGHT, string M_TEXTO, int M_TIPO, Boolean M_ACT, DateTime M_F_CREATE, string color, int M_ID_PADRE, 
            DateTime M_F_MODIFICACION, string NOMBRE_PADRE,int PED_ID_CLIENTE,string C_NOMINA, string nomllevar,string EMPL_NOM,int mesa)
        {
            this.ID = ID;
            this.M_NOM = M_NOM;
            this.M_EST = M_EST;
            this.M_ID_AMB = M_ID_AMB;
            this.M_X = M_X;
            this.M_ATENDIDA = M_ATENDIDA;
            this.M_WIDTH = M_WIDTH;
            this.M_HEIGHT = M_HEIGHT;
            this.M_TEXTO = M_TEXTO;
            this.M_TIPO = M_TIPO;
            this.M_ACT = M_ACT;
            this.M_F_CREATE = M_F_CREATE;
            if (M_EST == 1)
            {
                this.color = "red";
            }
            else if (M_EST == 2)
            {
                this.color = "skyblue";
            }
            else if (M_EST == 0)
            {
                this.color = "green";
            }
            this.M_ID_PADRE = M_ID_PADRE;
            this.M_F_MODIFICACION = M_F_MODIFICACION;
            this.NOMBRE_PADRE = NOMBRE_PADRE;
            this.PED_ID_CLIENTE = PED_ID_CLIENTE;
            this.C_NOMINA = C_NOMINA;
            this.nomllevar = nomllevar;
            this.EMPL_NOM = EMPL_NOM;
            this.mesa = mesa;
            this.ClearChildren();
        }
        public MesasItemViewModel(string nombre)
        {
            this.M_NOM = M_NOM;
        }
        #endregion
        #region Helper Methods
        private void ClearChildren()
        {
            this.Children = new ObservableCollection<MesasItemViewModel>();
            this.Children.Add(null);
        }
        #endregion
        private void Expand()
        {
            var children = MesasStructure.GetDirectoryContents(this.ID);
            this.Children = new ObservableCollection<MesasItemViewModel>(
                children.Select(c => new MesasItemViewModel(c.ID, c.M_NOM, c.M_EST, c.M_ID_AMB, c.M_X, c.M_ATENDIDA, c.M_WIDTH, c.M_HEIGHT,
                c.M_TEXTO, c.M_TIPO, c.M_ACT, c.M_F_CREATE, c.color, c.M_ID_PADRE, c.M_F_MODIFICACION, c.NOMBRE_PADRE, c.PED_ID_CLIENTE,c.C_NOMINA,c.nomllevar,c.EMPL_NOM,c.mesa)));
        }
    }
}

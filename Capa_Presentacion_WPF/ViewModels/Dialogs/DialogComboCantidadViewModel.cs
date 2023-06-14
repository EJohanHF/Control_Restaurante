using Capa_Negocio.Funciones_Globales;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogComboCantidadViewModel
    {
        public List<ComboCantidad> CombCantidad { get; set; }
        public ICommand CerrarDialog { get; set; }
        public ICommand Guardar { get; set; }
        private ComboCantidad cbocantidad;
        public ComboCantidad CboCantidad
        {
            get => cbocantidad;
            set
            {
                cbocantidad = value;
            }
        }
        public DialogComboCantidadViewModel()
        {
            this.CombCantidad = new List<ComboCantidad>();
            
            this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
            this.Guardar = new RelayCommand(new Action(GuardarCantidad));
            for (int j =1;j <= Convert.ToInt32(Application.Current.Properties["CantidadCombo"]); j++)
            {
                ComboCantidad combo = new ComboCantidad();
                combo.id = j;
                combo.value = j;
                this.CombCantidad.Add(combo);
            }
            this.CboCantidad = new ComboCantidad();
        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        private void CloseDialog()
        {
            Application.Current.Properties["CantidadComboAnulado"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void GuardarCantidad()
        {
            if (CboCantidad.id != 0)
            {
                if (Application.Current.Properties["Solo"].ToString() == "SI")
                {
                    if(CboCantidad.id == Convert.ToInt32(Application.Current.Properties["CantidadCombo"]))
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Solo queda un plato, no puede anular todos el plato", 2);
                    }
                    else
                    {
                        Application.Current.Properties["CantidadComboAnulado"] = CboCantidad.id;
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                }
                else
                {
                    Application.Current.Properties["CantidadComboAnulado"] = CboCantidad.id;
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese cantidad valida", 2);
            }

        }
        public class ComboCantidad
        {
            public int id { get; set; }
            public int value { get; set; }
        }
    }
}

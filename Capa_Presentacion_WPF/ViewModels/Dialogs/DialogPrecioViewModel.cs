using Capa_Negocio.Funciones_Globales;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogPrecioViewModel
    {
        public string NuevoPrecio { get; set; }
        public ICommand CerrarDialog { get; set; }
        public ICommand Guardar { get; set; }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public DialogPrecioViewModel()
        {
            this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
            this.Guardar = new ParamCommand(new Action<object>(GuardarNroPersonas));
        }
        private void CloseDialog()
        {
            Application.Current.Properties["NuevoPrecio0"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void GuardarNroPersonas(object parameter)
        {
            var textBox = parameter as TextBox;
            var texto = textBox.Text;
            NuevoPrecio = texto;
            if (NuevoPrecio.ToString().Trim() == "")
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese cantidad Valida", 2);
                return;
            }
            if (NuevoPrecio != null && Convert.ToDecimal(NuevoPrecio) >= 0 && NuevoPrecio.ToString().Trim() != "")
            {
                Application.Current.Properties["NuevoPrecio0"] = NuevoPrecio;
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese cantidad Valida", 2);
            }

        }
    }
}

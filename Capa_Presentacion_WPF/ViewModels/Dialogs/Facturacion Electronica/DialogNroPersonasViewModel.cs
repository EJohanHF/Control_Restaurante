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

namespace Capa_Presentacion_WPF.ViewModels.Dialogs.Facturacion_Electronica
{
    public class DialogNroPersonasViewModel
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public int NroPersonas { get; set; }
        public ICommand CerrarDialog { get; set; }
        public ICommand Guardar { get; set; }
        public string TipoNroPersonas { get; set; }
        public DialogNroPersonasViewModel()
        {
            this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
            this.Guardar = new ParamCommand(new Action<object>(GuardarNroPersonas));
            if (Application.Current.Properties["TipoNroPersonas"].ToString() == "1")
            {
                this.TipoNroPersonas = "Visible";
            }
            else
            {
                this.TipoNroPersonas = "Collapsed";
            }
            if(Application.Current.Properties["NroPersonasActual"] != null)
            {
                this.NroPersonas = Convert.ToInt32(Application.Current.Properties["NroPersonasActual"]);
            }
            else
            {
                this.NroPersonas = 0;
            }
            
        }
        private void CloseDialog()
        {
            Application.Current.Properties["NroPersonasNuevo"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void GuardarNroPersonas(object parameter)
        {
            try
            {
                var textBox = parameter as TextBox;
                var texto = textBox.Text;
                NroPersonas = Convert.ToInt32(texto);
                if (NroPersonas.ToString().Trim() == "")
                {
                    this.NroPersonas = 0;
                }
                if (NroPersonas != null && Convert.ToDecimal(NroPersonas) != 0 && NroPersonas.ToString().Trim() != "")
                {
                    Application.Current.Properties["NroPersonasNuevo"] = NroPersonas;
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese cantidad Valida", 2);
                }
            }
            catch (Exception ex)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese cantidad Valida", 2);
            }
        }
    }
}

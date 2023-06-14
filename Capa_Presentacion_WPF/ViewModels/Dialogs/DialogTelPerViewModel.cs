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
    public class DialogTelPerViewModel : IGeneric
    {
        public ICommand CerrarDialog { get; set; }
        public ICommand GuardarNomPer { get; set; }
        public string TelPersona { get; set; }
        public DialogTelPerViewModel()
        {
            this.CerrarDialog = new RelayCommand(new Action(CerrarDialogo));
            //this.CambMesa = new RelayCommand(new Action(CambiarMesa));
            this.GuardarNomPer = new ParamCommand(new Action<object>(GuardarNom));
            if (Application.Current.Properties["TelLlevarCliente"] == null)
            {
                this.TelPersona = "";
            }
            else
            {
                this.TelPersona = Application.Current.Properties["TelLlevarCliente"].ToString();
            }
        }
        public void CerrarDialogo()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public void GuardarNom(object parameter)
        {
            try
            {
                var textbox = parameter as TextBox;
                var texto = textbox.Text;
                this.TelPersona = texto;
                Application.Current.Properties["TelLlevarCliente"] = this.TelPersona;
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

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
    public class DialogNomPerViewModel : IGeneric
    {
        public ICommand CerrarDialog { get; set; }
        public ICommand GuardarNomPer { get; set; }

        public string NomPersona { get; set; }
        public DialogNomPerViewModel()
        {
            this.CerrarDialog = new RelayCommand(new Action(CerrarDialogo));
            //this.CambMesa = new RelayCommand(new Action(CambiarMesa));
            this.GuardarNomPer = new ParamCommand(new Action<object>(GuardarNom));
            if (Application.Current.Properties["NomLlevarCliente"] == null)
            {
                this.NomPersona = "";
            }
            else
            {
                this.NomPersona = Application.Current.Properties["NomLlevarCliente"].ToString();
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
                this.NomPersona = texto;
                Application.Current.Properties["NomLlevarCliente"] = this.NomPersona;
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

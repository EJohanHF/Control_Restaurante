using Capa_Entidades;
using Capa_Negocio;
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
    public class DialogMotorizadoViewModel : IGeneric
    {
        public ICommand CerrarDialog { get; set; }
        public ICommand GuardarNomPer { get; set; }

        public string Motorizado { get; set; }
        public List<Ent_Combo> ComboMotorizado { get; set; }
        Neg_Combo negCombo = new Neg_Combo();
        private Ent_Combo cboMotorizado;
        public Ent_Combo cbMotorizado
        {
            get => cboMotorizado;
            set
            {
                cboMotorizado = value;
            }
        }
        public DialogMotorizadoViewModel()
        {
            this.CerrarDialog = new RelayCommand(new Action(CerrarDialogo));
            this.ComboMotorizado = negCombo.GetComboMotorizado();
            cbMotorizado = new Ent_Combo();
            //this.CambMesa = new RelayCommand(new Action(CambiarMesa));
            this.GuardarNomPer = new RelayCommand(new Action(GuardarNom));
        }
        public void CerrarDialogo()
        {
            Application.Current.Properties["GuardaMotorizado"] = false;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public void GuardarNom()
        {
            try
            {
                if (cbMotorizado.id != "0" && cbMotorizado.id != null)
                {
                    Application.Current.Properties["NomMotorizado"] = cbMotorizado.id;
                    Application.Current.Properties["GuardaMotorizado"] = true;
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Seleccione un motorizado", 2);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

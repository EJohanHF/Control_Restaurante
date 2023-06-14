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
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogMesaDeliveryViewModel
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public List<Ent_Combo> ComboMesasDelivery { get; set; }
        Neg_Combo negCombo = new Neg_Combo();
        private Ent_Combo cboMesaSelec;
        public ICommand CerrarDialog { get; set; }
        public ICommand GuardarMesasDelivery { get; set; }
        public Ent_Combo CboMesaSelec
        {
            get => cboMesaSelec;
            set
            {
                cboMesaSelec = value;
            }
        }
        public DialogMesaDeliveryViewModel()
        {
            this.ComboMesasDelivery = negCombo.GetComboMesaDelivery();
            this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
            this.GuardarMesasDelivery = new RelayCommand(new Action(GuardarMesaDelivery));
            this.CboMesaSelec = new Ent_Combo();
        }
        private void GuardarMesaDelivery()
        {
            if (CboMesaSelec.id != "0" && CboMesaSelec.id != null)
            {
                Application.Current.Properties["IdMesaDelivery"] = CboMesaSelec.id;
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Seleccione una mesa", 2);
            }
        }
        private void CloseDialog()
        {
            Application.Current.Properties["IdMesaDelivery"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}

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
    public class DialogRecojoDeliveryViewModel
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
        public DialogRecojoDeliveryViewModel()
        {
            this.ComboMesasDelivery = negCombo.GetComboRecojoDelivery();
            this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
            this.GuardarMesasDelivery = new RelayCommand(new Action(GuardarRecojoDelivery));
            this.CboMesaSelec = new Ent_Combo();
        }
        private void GuardarRecojoDelivery()
        {
            if (CboMesaSelec.id != "0" && CboMesaSelec.id != null)
            {
                Application.Current.Properties["IdRecojoDelivery"] = CboMesaSelec.id;
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Seleccione una mesa", 2);
            }
        }
        private void CloseDialog()
        {
            Application.Current.Properties["IdRecojoDelivery"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}

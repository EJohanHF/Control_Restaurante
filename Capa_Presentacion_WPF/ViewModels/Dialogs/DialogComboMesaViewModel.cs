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
    
    public class DialogComboMesaViewModel
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public List<Ent_Combo> ComboMesasOcupadas { get; set; }
        public List<Ent_Combo> ComboMesasDisponibles { get; set; }
        Neg_Combo negCombo = new Neg_Combo();
        public ICommand CerrarDialog { get; set; }
        public ICommand GuardarMesasOcupadas { get; set; }
        public ICommand GuardarMesasDisponibles { get; set; }
        private Ent_Combo cboMesaSelecO;
        public Ent_Combo CboMesaSelecO
        {
            get => cboMesaSelecO;
            set
            {
                cboMesaSelecO = value;
            }
        }
        private Ent_Combo cboMesaSelecD;
        public Ent_Combo CboMesaSelecD
        {
            get => cboMesaSelecD;
            set
            {
                cboMesaSelecD = value;
            }
        }
        public DialogComboMesaViewModel()
        {
            this.ComboMesasOcupadas = negCombo.GetComboMesaOcupadas(Convert.ToInt32(Application.Current.Properties["IdMesaBase"]));
            this.ComboMesasDisponibles = negCombo.GetComboMesaDisponibles(Convert.ToInt32(Application.Current.Properties["IdMesaBase"]));
            this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
            this.GuardarMesasOcupadas = new RelayCommand(new Action(GuardarMesaOcupada));
            this.GuardarMesasDisponibles = new RelayCommand(new Action(GuardarMesaDisponible));
            this.CboMesaSelecO = new Ent_Combo();
            this.CboMesaSelecD = new Ent_Combo();
        }
        private void GuardarMesaOcupada()
        {
            if (CboMesaSelecO.id != "0" && CboMesaSelecO.id != null)
            {
                Application.Current.Properties["IdMesaSeleccionada"] = CboMesaSelecO.id;
                Application.Current.Properties["TipoMesa"] = "OCUPADO";
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Seleccione una mesa ocupada", 2);
            }
        }
        private void GuardarMesaDisponible()
        {
            if (CboMesaSelecD.id != "0" && CboMesaSelecD.id != null)
            {
                Application.Current.Properties["IdMesaSeleccionada"] = CboMesaSelecD.id;
                Application.Current.Properties["TipoMesa"] = "DISPONIBLE";
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Seleccione una mesa disponible", 2);
            }
        }
        private void CloseDialog()
        {
            Application.Current.Properties["IdMesaSeleccionada"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}

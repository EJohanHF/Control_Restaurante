using Capa_Entidades;
using Capa_Entidades.Models.Pedido;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio;
using Capa_Negocio.Pedido;
using Capa_Negocio.Reportes.VentasporDia;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogPagoClienteViewModel : INotifyPropertyChanged
    {
        public ICommand CerrarDialog { get; set; }
        public ICommand GuardarCommand { get; set; }

        #region Negocio
        Neg_Pedido negPedido = new Neg_Pedido();
        Neg_Combo negCombo = new Neg_Combo();

        #endregion
        #region Entidad
        private Pedidos pedidos;
        public Pedidos Pedidos {
            get => pedidos;
            set {
                pedidos = value;
            }
        }

        private Ent_Combo _TipoPagoSelected;
        public Ent_Combo TipoPagoSelected {
            get => _TipoPagoSelected;
            set {
                if (value != null) {
                    Application.Current.Properties["TipoPagoDialogPagoCliente"] = ((Ent_Combo)value).id;
                    Application.Current.Properties["TipoPagoNombreDialogPagoCliente"] = ((Ent_Combo)value).nombre;
                    if (Convert.ToInt32(((Ent_Combo)value).id) != 4)
                    {
                        readonly_text_monto = true;
                        Monto = montoTotalPedido.ToString();
                        Vuelto = 0;
                    }
                    else {
                        readonly_text_monto = false;
                    }
                }
                _TipoPagoSelected = value;
            }
        }

        #endregion
        #region Objetos
        public decimal montoTotalPedido { get; set; }
        private string _monto;
        public string Monto
        {
            get => _monto;
            set
            {
                if (value.ToString().Trim() != ""){
                Vuelto = Convert.ToDecimal(value) - montoTotalPedido;
                Vuelto = Math.Round(Vuelto, 2);
                Application.Current.Properties["MontoDialogPagoCliente"] = Math.Round(Convert.ToDecimal(value), 2);
                Application.Current.Properties["VueltoDialogPagoCliente"] = Math.Round(Vuelto, 2);
                }
                else
                {
                    Vuelto = 0;
                    Application.Current.Properties["MontoDialogPagoCliente"] = 0.00;
                    Application.Current.Properties["VueltoDialogPagoCliente"] = 0.00;
                }
                _monto = value;
            }
        }
        private decimal _vuelto;
        public decimal Vuelto
        {
            get => _vuelto;
            set
            {
                _vuelto = value;
            }
        }
        public bool readonly_text_monto { get; set; } = false;
        #endregion
        #region INotify
        //Para el INotifyPropertyChanged sea Inicializado.
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        //
        #endregion
        public List<Ent_Combo> ComboFormaPago { get; set; }
        public DialogPagoClienteViewModel() {
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.CerrarDialog = new RelayCommand(new Action(CancelarDialog));
            this.ComboFormaPago = negCombo.GetComboFormaPago(1);
            montoTotalPedido = Convert.ToDecimal(Application.Current.Properties["MontoTotalPedido"]);
        }
        private async void Guardar()
        {
            if(TipoPagoSelected != null)
            {
                Application.Current.Properties["MontoDialogPagoCliente"] = Math.Round(Convert.ToDecimal(Monto), 2);
                Application.Current.Properties["VueltoDialogPagoCliente"] = Math.Round(Vuelto, 2);
                Application.Current.Properties["DeliveryMetPago"] = true;
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }
        private void CancelarDialog()
        {
            Application.Current.Properties["DeliveryMetPago"] = false;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

    }
}

using Capa_Entidades;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Delivery_Web_Service;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Pedido;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogPagarViewModel : IGeneric
    {
        [DllImport("wininet.dll")]

        private extern static bool InternetGetConnectedState(out int Descripcion, int Reserved);
        public bool enabled { get; set; }
        private Byte[] logoEmpresa;
        public Byte[] LogoEmpresa { get => logoEmpresa; set { logoEmpresa = value; } }
        Neg_Empresa negEmpr = new Neg_Empresa();
        #region BotonPagar
        Neg_Pagar negPagar = new Neg_Pagar();
        private Pagar pagar;
        public decimal totals { get; set; }
        public decimal amortizars { get; set; }
        public decimal saldos { get; set; }
        public decimal amortizar { get; set; }
        public string pedido { get; set; }
        public string usua { get; set; }
        public string _EstadoPago { get; set; }
        public decimal pagarcon { get; set; }
        public decimal vuelto { get; set; }
        public bool _isEnabled1 { get; set; }
        public bool _IsEnabledbtnguardar { get; set; }
        public bool _IsEnabledbtnpagar { get; set; }
        private string operacion_detPago;
        public decimal TCambio { get; set; }
        public ObservableCollection<Pagar> DataDetallePagar { get; set; }
        public List<Ent_Combo> ComboFormaPago { get; set; }
        public List<Ent_Combo> ComboFormaPagoPropina { get; set; }
        private Ent_Combo _STipoMoneda;
        public List<Ent_Combo> ComboTipoMoneda { get; set; }
        public Pagar SelectedDetPedDataFile { get; set; }
        List<Pagar> list = new List<Pagar>();
        public ObservableCollection<Empresa> empresa { get; set; }
        Neg_Empresa negEmpresa = new Neg_Empresa();
        Neg_Parametros negParametros = new Neg_Parametros();
        Neg_DeliveryWebService negDeliveryWebService = new Neg_DeliveryWebService();
        private ICommand DetPed_RowClickCommand;
        #endregion

        public string NombreEquipo { get; set; }
        public ICommand RowClickDPCommand
        {
            get
            {
                return DetPed_RowClickCommand;
            }
        }
        public Ent_Combo STipoMoneda
        {
            get => _STipoMoneda;
            set
            {
                if (value != null)
                {
                    this.TCambio = Convert.ToDecimal(((Ent_Combo)value).valor2);
                    
                    if (value.id != "1")
                    {
                        IsEnabledbtnguardar = false;
                        IsEnabledbtnpagar = true;
                        this.pagar = new Pagar();
                        this.Pagar = new Pagar();
                        //DataDetallePagar = new ObservableCollection<Pagar>();
                        GetListaDetPago();
                        if (this.Operacion_detPago == "CAMBIAR PAGO" || Application.Current.Properties["PedidoCuenta"].ToString() == "SI")
                        {

                        }
                        else
                        {
                            amortizars = 0;
                        }
                        this.Totals = Convert.ToDecimal((Convert.ToDecimal(Application.Current.Properties["Totals"]) / this.TCambio).ToString("N2"));
                        this.ComboFormaPago = negCombo.GetComboFormaPago(int.Parse(((Ent_Combo)value).id));
                        this.TipoPagoSelected = ComboFormaPago.FirstOrDefault();
                        //if (this.Operacion_detPago == "CAMBIAR PAGO")
                        //{
                        //    if (DataDetallePagar != null)
                        //    {
                        //        if (DataDetallePagar.Count == 0)
                        //        {

                        //        }
                        //        else
                        //        {
                        //            GetListaDetPago();
                        //        }
                        //    }
                        //}    
                    }
                    else
                    {
                        IsEnabledbtnguardar = false;
                        IsEnabledbtnpagar = true;
                        this.pagar = new Pagar();
                        this.Pagar = new Pagar();
                        //DataDetallePagar = new ObservableCollection<Pagar>();
                        GetListaDetPago();
                        if (this.Operacion_detPago == "CAMBIAR PAGO" || Application.Current.Properties["PedidoCuenta"].ToString() == "SI")
                        {

                        }
                        else
                        {
                            amortizars = 0;
                        } 
                        this.Totals = Convert.ToDecimal(Application.Current.Properties["Totals"]);
                        this.ComboFormaPago = negCombo.GetComboFormaPago(int.Parse(((Ent_Combo)value).id));
                        this.TipoPagoSelected = ComboFormaPago.FirstOrDefault();
                        //if (this.Operacion_detPago == "CAMBIAR PAGO")
                        //{
                        //    if (DataDetallePagar != null)
                        //    {
                        //        if (DataDetallePagar.Count == 0)
                        //        {
                                    
                        //        }
                        //        else
                        //        {
                                    
                        //        }
                        //    }
                        //}
                    }

                }
                _STipoMoneda = value;


            }
        }
        public class DelegateDetPedCommand : ICommand
        {
            private Action m_Action;
            public DelegateDetPedCommand(Action action)
            {
                this.m_Action = action;
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                m_Action();
            }
        }
        public string Operacion_detPago
        {
            get => operacion_detPago;
            set
            {
                if (value == "PAGAR")
                    GetListaDetPago();
                this.enabled = false;
                operacion_detPago = value;
            }
        }
        public Pagar Pagar
        {
            get => pagar;
            set
            {
                pagar = value;
            }
        }
        #region variable Pagar
        public decimal Totals
        {
            get => totals;
            set
            {

                totals = value;
                saldos = totals - amortizars;
                amortizar = saldos;
            }
        }
        public decimal Amortizars
        {
            get => amortizars;
            set
            {
                amortizars = value;

            }
        }
        public decimal Amortizar
        {
            get => amortizar;
            set
            {
                amortizar = value;
                pagarcon = 0;
                vuelto = 0;
            }
        }
        public decimal Saldos
        {
            get => saldos;
            set
            {
                saldos = value;

                if (value > 0)
                {
                    IsEnabled1 = true;
                    IsEnabledbtnpagar = true;
                }
                else
                {
                    IsEnabledbtnpagar = false;
                }
                amortizar = saldos;
            }
        }
        private string _tips { get; set; }
        public string Tips
        { get { return _tips; } set { _tips = value; OnPropertyChanged_Tips("Tips"); } }
        public event PropertyChangedEventHandler PropertyChanged_Tips;
        public void OnPropertyChanged_Tips(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_Tips;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
            try
            {
            }
            catch (Exception ex)
            {
                vuelto = 0;
            }
        }
        public bool IsEnabled1
        {
            get => _isEnabled1;

            set
            {
                _isEnabled1 = value;

            }
        }
        public bool IsEnabledbtnguardar
        {
            get => _IsEnabledbtnguardar;

            set
            {
                _IsEnabledbtnguardar = value;
            }
        }
        public bool IsEnabledbtnpagar
        {
            get => _IsEnabledbtnpagar;

            set
            {
                _IsEnabledbtnpagar = value;
            }
        }
        public decimal Pagarcon
        {
            get { return pagarcon; }
            set
            {
                pagarcon = value;
                vuelto = pagarcon - Saldos;
            }
        }
        public decimal Vuelto
        {
            get { return vuelto; }
            set
            {
                vuelto = value;
            }
        }
        public string Pedido
        {
            get => pedido;
            set
            {
                //this.pedido = Pagar.idpedido;
                pedido = value;
            }
        }
        public string EstadoPago
        {
            get => _EstadoPago;
            set
            {
                _EstadoPago = value;
            }
        }
        public string Usuario
        {
            get => usua;
            set
            {
                //this.usua = Pagar.idusuario;
                usua = value;
            }
        }
        Neg_Combo negCombo = new Neg_Combo();
        public ICommand PagarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public DialogPagarViewModel()
        {

            this.NombreEquipo = ConfigurationManager.AppSettings["NombreEquipo"].ToString();
            this.Operacion_detPago = "PAGAR";
            if (Application.Current.Properties["OperPagoCel"] != null)
            { this.Operacion_detPago = Application.Current.Properties["OperPagoCel"].ToString(); }
            this.PagarCommand = new RelayCommand(new Action(PagarPed));
            this.GuardarCommand = new RelayCommand(new Action(GuardarPed));
            this.EliminarCommand = new ParamCommand(new Action<object>(EliminarDetPagar));
            IsEnabledbtnguardar = false;
            IsEnabledbtnpagar = true;
            this.pagar = new Pagar();
            this.Pagar = new Pagar();
            this.ComboFormaPagoPropina = negCombo.GetComboFormaPago2();
            this.ComboTipoMoneda = negCombo.GetComboTipoMoneda();
            this.TipoPagoSelectedPropina = ComboFormaPagoPropina.FirstOrDefault();
            GetListaDetPago();
            
            if (Application.Current.Properties["id_pedido"] != null)
            {
                this.Pedido = Application.Current.Properties["id_pedido"].ToString();
            }
            if (Application.Current.Properties["Totals"] != null)
            {
                this.Totals = Convert.ToDecimal(Application.Current.Properties["Totals"]);
            }
            if (Application.Current.Properties["Usuario"] != null)
            {
                this.Usuario = Application.Current.Properties["Usuario"].ToString();
            }
            if (Application.Current.Properties["Pedido"] != null)
            {
                this.Pedido = Application.Current.Properties["Pedido"].ToString();
            }
            if (Application.Current.Properties["EstadoF"] != null)
            {
                this.EstadoPago = Application.Current.Properties["EstadoF"].ToString();
            }
            ObservableCollection<Empresa> empresa = new ObservableCollection<Empresa>();
            empresa = negEmpr.GetEmpresa();
            LogoEmpresa = empresa.Where(w => w.EMPR_DEFAULT == "1").First().empr_logo;
        }
        public string Nrotarjeta { get; set; }
        #endregion

        Funcion_Global global = new Funcion_Global(); 

        private void PagarPed()
        {
            if (this.Totals == this.Saldos)
            {
                Pagarpedidos();
            }
            else
            {
                if (this.saldos > 0)
                {
                    Pagarpedidos();
                }
            }


        }
        private void Pagarpedidos()
        {
            if (pagar.idtmoneda != 0 && pagar.idtpago != 0)
            {

                Pagar pagar2 = new Pagar();
                pagar2.id = pagar.id + 1;
                pagar2.idtmoneda = pagar.idtmoneda;
                pagar2.idtpago = pagar.idtpago;
                pagar2.nomtmoneda = ComboTipoMoneda.Where(w => Convert.ToInt32(w.id) == pagar.idtmoneda).ToList().FirstOrDefault().nombre;
                pagar2.nomtpago = ComboFormaPago.Where(w => Convert.ToInt32(w.id) == pagar.idtpago).ToList().FirstOrDefault().nombre;
                pagar2.totals = pagar.totals;
                pagar2.monto = Amortizar;
                pagar2.nrotarjeta = Nrotarjeta;
                if (DataDetallePagar.Count > 0)
                {
                    if (pagar2.monto == totals)
                    {
                        return;
                    }
                    else
                    {
                        if (pagar2.monto <= saldos)
                        {
                            DataDetallePagar.Add(pagar2);
                            amortizars += amortizar;
                            saldos -= amortizar;
                            amortizar = saldos;
                            IsEnabledbtnguardar = true;
                            if (saldos == 0)
                            {
                                IsEnabled1 = false;
                            }
                            else
                            {
                                IsEnabled1 = true;
                            }
                        }
                        else global.isactive = true;
                    }
                }
                else if (pagar2.monto <= saldos)
                {
                    DataDetallePagar.Add(pagar2);
                    amortizars += amortizar;
                    saldos -= amortizar;
                    amortizar = saldos;
                    IsEnabledbtnguardar = true;
                    if (saldos == 0)
                    {
                        IsEnabled1 = false;
                    }
                    else
                    {
                        IsEnabled1 = true;
                    }
                }
                else global.isactive = true;
            }
        }
        #region pagar nro tarjeta
        private Ent_Combo _tipopagoSelected;
        public Ent_Combo TipoPagoSelected
        {
            get => _tipopagoSelected;
            set
            {
                if (value != null)
                {
                    object _ubi = ((Ent_Combo)value).id;
                    if (Convert.ToInt32(_ubi) != 4)
                    {
                        this.enabled = true;
                    }
                    else
                    {
                        this.enabled = false;
                    }
                }
                _tipopagoSelected = value;
            }
        }
        private Ent_Combo _tipopagoSelectedPropina;
        public Ent_Combo TipoPagoSelectedPropina
        {
            get => _tipopagoSelectedPropina;
            set
            {
                if (value != null)
                {
                    object _ubi = ((Ent_Combo)value).id;
                }
                _tipopagoSelectedPropina = value;
            }
        }
        #endregion
        Neg_Pedido negPedido = new Neg_Pedido();
        #region guardar Pedido
        private async void GuardarPed()
        {
            Application.Current.Properties["EstPago"] = null;
            if (this.Saldos != 0)
            {
                global.Mensaje("Informacion: ", "Tiene un saldo por cancelar", 2);
                return;
            }
            if (this.Operacion_detPago == "CAMBIAR PAGO")
            {
                if (this.Saldos == 0)
                {
                    GuardarCambioPago();
                }
                else {
                    global.Mensaje("Informacion: ", "Tiene un saldo por cancelar", 2);
                }
            }
            else
            {
                Pagar pagar = this.Pagar;
                var _id = pagar.idpago;
                string _mensaje = "";
                pagar.idpedido = this.Pedido;
                pagar.idusuario = this.Usuario;
                pagar.totals = this.Totals;
                pagar.amortizars = this.Amortizars;
                pagar.amortizar = this.Amortizar;
                pagar.saldos = this.Saldos;
                pagar.pagarcon = this.Pagarcon;
                pagar.vuelto = this.Vuelto;
                pagar.nrotarjeta = this.Nrotarjeta;
                pagar.monto_propina = Convert.ToDecimal(this.Tips);
                if (this.Tips == "" || this.Tips == "0") {
                    pagar.idtpagoPropina = null;
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("DT_ID_TIP_MONEDA");
                dt.Columns.Add("DT_ID_FORM_PAGO");
                dt.Columns.Add("DT_AMORT");
                dt.Columns.Add("DT_TOTAL");
                dt.Columns.Add("DT_NRO_TARJETA");
                decimal suma = 0;
                int contador_efectivo = 0;
                foreach (Pagar item in this.DataDetallePagar)
                {
                    DataRow row = dt.NewRow();
                    row["DT_ID_TIP_MONEDA"] = item.idtmoneda;
                    row["DT_ID_FORM_PAGO"] = item.idtpago;
                    row["DT_AMORT"] = item.monto;
                    row["DT_TOTAL"] = pagar.totals;
                    row["DT_NRO_TARJETA"] = pagar.nrotarjeta;
                    dt.Rows.Add(row);
                    suma += Convert.ToDecimal(row["DT_AMORT"]);
                    if (item.nomtpago.ToUpper().Contains("EFECTIVO"))
                    {
                        contador_efectivo = contador_efectivo + 1;
                    }
                }
                if (this.Totals == suma)
                {
                    string _mensaje1 = "";

                    bool result = negPagar.SetPagar((_id == 0 ? 1 : 2), pagar, ref _mensaje1, NombreEquipo, dt);
                    if (result)
                    {
                        if (contador_efectivo > 0)
                        {
                            AbrirGaveta();
                        }
                        Application.Current.Properties["EstPago"] = "PAGADO";
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                }
                else
                {
                    global.Mensaje("Informacion: ", "Tiene un saldo por cancelar", 2);
                }
            }
        }

        #endregion
        private async void GuardarCambioPago()
        {
            Pagar pagar = this.Pagar;
            var _id = pagar.idpago;
            string _mensaje = "";
            pagar.idpedido = this.Pedido;
            pagar.idusuario = this.Usuario;
            pagar.totals = this.Totals;
            pagar.amortizars = this.Amortizars;
            pagar.amortizar = this.Amortizar;
            pagar.saldos = this.Saldos;
            pagar.pagarcon = this.Pagarcon;
            pagar.vuelto = this.Vuelto;
            pagar.nrotarjeta = this.Nrotarjeta;
            DataTable dt = new DataTable();
            dt.Columns.Add("DT_ID_TIP_MONEDA");
            dt.Columns.Add("DT_ID_FORM_PAGO");
            dt.Columns.Add("DT_AMORT");
            dt.Columns.Add("DT_TOTAL");
            dt.Columns.Add("DT_NRO_TARJETA");
            foreach (Pagar item in this.DataDetallePagar)
            {
                DataRow row = dt.NewRow();
                row["DT_ID_TIP_MONEDA"] = item.idtmoneda;
                row["DT_ID_FORM_PAGO"] = item.idtpago;
                row["DT_AMORT"] = item.monto;
                row["DT_TOTAL"] = pagar.totals;
                row["DT_NRO_TARJETA"] = pagar.nrotarjeta;
                dt.Rows.Add(row);
            }
            bool result = negPagar.SetPagar((_id == 0 ? 1 : 2), pagar, ref _mensaje, NombreEquipo, dt);
            var view = new MessageDialog
            {
                Mensaje = { Text = _mensaje }
            };
            DialogHost.CloseDialogCommand.Execute(null, null);
            await DialogHost.Show(view, "RootDialog");
            if (result)
            {

            }
            return;
            //}
            //else
            //{
            //    IsActive = true;
            //}

        }
        private void EliminarDetPagar(object idpag)
        {
            if (DataDetallePagar.Count > 0)
            {
                //saldos += DataDetallePagar.Where(w => Convert.ToInt32(w.id) == (int)idpag).ToList().FirstOrDefault().monto;
                //amortizars -= DataDetallePagar.Where(w => Convert.ToInt32(w.id) == (int)idpag).ToList().FirstOrDefault().monto;

                RemoveVlans();
                if (operacion_detPago == "CAMBIAR PAGO")
                {
                    IsEnabledbtnpagar = true;
                    amortizar = saldos;
                }
                else
                {
                    if (saldos > 0)
                    {

                        IsEnabledbtnpagar = true;
                        IsEnabledbtnguardar = true;
                        amortizar = saldos;
                    }
                    else
                    {
                        IsEnabledbtnpagar = false;
                    }
                }

            }
        }
        void RemoveVlans()
        {
            if (DataDetallePagar != null)
            {
                //saldos += DataDetallePagar.Where(w => Convert.ToInt32(w.id) == (int)idpag).ToList().FirstOrDefault().monto;
                //amortizars -= DataDetallePagar.Where(w => Convert.ToInt32(w.id) == (int)idpag).ToList().FirstOrDefault().monto;
                //saldos += DataDetallePagar.Select(s=> s.monto).Where(w=> )(SelectedDetPedDataFile).
                saldos += SelectedDetPedDataFile.monto;
                amortizars -= SelectedDetPedDataFile.monto;
                DataDetallePagar.Remove(SelectedDetPedDataFile);
                DataDetallePagar.Select(s => s.id).ToList().FirstOrDefault();
            }
        }
        private void GetListaDetPago()
        {
            if (Convert.ToString(Application.Current.Properties["EstadoPago"]) == "PAGADO")
            {
                int idpedido = 0;
                idpedido = Convert.ToInt32(Application.Current.Properties["id_pedido"]);
                DataDetallePagar = negPagar.GetDetallePago(idpedido);
                if (DataDetallePagar.Count > 0)
                {
                    IsEnabledbtnguardar = true;
                    var s = DataDetallePagar.FirstOrDefault().P_SALDO;
                    var a = DataDetallePagar.FirstOrDefault().P_ABONADO;
                    var k = DataDetallePagar.FirstOrDefault().P_SALDO;
                    this.Saldos = s;
                    this.Amortizars = a;
                    this.Amortizar = k;
                }
            }
            else
            {
                //if(Application.Current.Properties["PedidoCuenta"] == "SI")
                //{
                //    DataDetallePagar = negPagar.GetDetallePago(Convert.ToInt32(Application.Current.Properties["id_pedido"]));
                //    if (DataDetallePagar.Count > 0)
                //    {
                //        IsEnabledbtnguardar = true;
                //        var s = DataDetallePagar.FirstOrDefault().saldos;
                //        var a = DataDetallePagar.FirstOrDefault().amortizars;
                //        var k = DataDetallePagar.FirstOrDefault().saldos;
                //        this.Saldos = s;
                //        this.Amortizars = a;
                //        this.Amortizar = k;
                //    }
                //}
                //else
                //{
                    int idpedido2 = -1;
                    DataDetallePagar = negPagar.GetDetallePago(idpedido2);
                //}          
            }
        }
        private void AbrirGaveta()
        {
            string s;
            System.Windows.Forms.PrintDialog pd = new System.Windows.Forms.PrintDialog();
            //s = Strings.Chr(27) + Strings.Chr(112) + Strings.Chr(0) + Strings.Chr(50) + Strings.Chr(250);
            s = "" + (char)27 + (char)112 + (char)0 + (char)50 + (char)250;
            pd.PrinterSettings = new PrinterSettings();
            string impresora = ConfigurationManager.AppSettings["ImpCaja"].ToString();  // nombre de la impresora 
            RawPrinterHelper.SendStringToPrinter(impresora, s);
        }

    }
}
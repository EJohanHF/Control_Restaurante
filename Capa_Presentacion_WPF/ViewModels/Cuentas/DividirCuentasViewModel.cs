using Capa_Entidades;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Pedido;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Cuentas;
using Capa_Negocio.Delivery_Web_Service;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Pedido;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Presentacion_WPF.Util;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.Cuentas
{
    public class DividirCuentasViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));

        }
        #region PagoViewModel
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Descripcion, int Reserved);
        public bool enabled { get; set; }
        private Byte[] logoEmpresa;
        public Byte[] LogoEmpresa { get => logoEmpresa; set { logoEmpresa = value; } }
        Neg_Empresa negEmpr = new Neg_Empresa();
        #region BotonPagar
        public bool ischeckBoleta { get; set; }
        public bool ischeckFactura { get; set; }
        public bool ischeckTicket { get; set; }
        public string NroDocCliente { get; set; }
        public string visibleBoleta { get; set; } = "Visible";
        public decimal tamañoBoleta { get; set; } = 0;
        Neg_Pagar negPagar = new Neg_Pagar();
        private Pagar pagar;
        Neg_Cliente negCli = new Neg_Cliente();
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
        public ICommand BuscarCliente { get; set; }
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
                        amortizars = 0;
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
                        amortizars = 0;
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
                if (pagarcon != null)
                {
                    if (Amortizar != null)
                    {
                        if (Amortizar != 0)
                        {
                            vuelto = pagarcon - Amortizar;
                        }
                        else
                        {
                            vuelto = 0;
                        }
                    }
                    else
                    {
                        vuelto = 0;
                    }
                }
                else
                {
                    vuelto = 0;
                }
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
         public ObservableCollection<Cliente> DataCliente { get; set; }
        #endregion
        #endregion
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
        public ObservableCollection<Pedidos> Pedido2 { get; set; }
        public ObservableCollection<Capa_Entidades.Models.Cuentas.Cuentas> Cuenta { get; set; }
        public ObservableCollection<Capa_Entidades.Models.Cuentas.Cuenta_Det> CuentaDet { get; set; }
        Neg_Pedido negPedido = new Neg_Pedido();
        Neg_Cuentas negCuentas = new Neg_Cuentas();
        public string VisibleCuentas { get; set; }
        public string VisibleNuevaCuenta { get; set; }
        public string VisiblePagarCuenta { get; set; }
        public string VerLista { get; set; }
        public string CantidadElegir { get; set; }
        public string VisibleAccion { get; set; }
        public string NomCuenta { get; set; }
        public string TotalCuenta { get; set; }
        public string PagoCuenta { get; set; }
        public string Nrotarjeta { get; set; }
        public string Titulo { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand2 { get; set; }
        public ICommand PasarPlatosCommand { get; set; }
        public ICommand CerrarCommand { get; set; }
        public ICommand EliminaPlatoCommand { get; set; }
        public ICommand imprimirCuenta { get; set; }
        public ICommand AnularCuentaCommand { get; set; }
        public ICommand PagarCommand2 { get; set; }
        public string VisibleTicket { get; set; }
        public string VisibleBoleta { get; set; }
        public string VisibleFactura { get; set; }
        Funcion_Global globales = new Funcion_Global();
        Funcion_Global global = new Funcion_Global();
        public ObservableCollection<ComboCantidad> CombCantidad { get; set; }
        public ICommand CerrarDialog { get; set; }
        public ICommand Guardar { get; set; }
        private ComboCantidad cbocantidad;
        public ComboCantidad CboCantidad
        {
            get => cbocantidad;
            set
            {
                cbocantidad = value;
            }
        }
        public class ComboCantidad
        {
            public int id { get; set; }
            public int value { get; set; }
        }
        public DividirCuentasViewModel()
        {
            bool FacturacionEnable = Convert.ToBoolean(negParametros.FacturacionElectronica());
            if(FacturacionEnable == false)
            {
                ischeckTicket = true;
                VisibleTicket = "Visible";
                VisibleBoleta = "Collapsed";
                VisibleFactura = "Collapsed";
            }
            else
            {
                VisibleTicket = "Visible";
                VisibleBoleta = "Visible";
                VisibleFactura = "Visible";
                if (negParametros.TipoDocPagar() == true)
                {
                    ischeckTicket = true;
                }
                else
                {
                    ischeckBoleta = true;
                }
            }
            this.RazonCliente = "";
            //if (Application.Current.Properties["NomCliente"] != null)
            //{
            //    this.RazonCliente = Application.Current.Properties["NomCliente"].ToString();
            //}
            DataTable dt_info_delivery = new DataTable();
            if (Application.Current.Properties["dt_info_delivery"] != null)
            {
                dt_info_delivery = (DataTable)Application.Current.Properties["dt_info_delivery"];
                if (dt_info_delivery.Rows.Count > 0)
                {
                    this.RucCliente = dt_info_delivery.Rows[0]["C_NRO_DOC"].ToString();
                    this.RazonCliente = dt_info_delivery.Rows[0]["C_NOMINA"].ToString();
                    this.DireccionCliente = dt_info_delivery.Rows[0]["C_DIREC"].ToString();
                    this.TelefonoCliente = dt_info_delivery.Rows[0]["C_TEL"].ToString();
                    if (dt_info_delivery.Rows[0]["id_tipo_doc"].ToString() == "2") { ischeckFactura = true; ischeckBoleta = false; } else { ischeckBoleta = true; ischeckFactura = false; }
                    if(dt_info_delivery.Rows[0]["id_tipo_doc"].ToString() == "2")
                    {
                        TipoDoc = "1";
                    }
                    else
                    {
                        TipoDoc = "3";
                    }
                }
            }
            this.Titulo = "Cuenta Principal";
            this.Pedido2 = new ObservableCollection<Pedidos>();
            this.Cuenta = new ObservableCollection<Capa_Entidades.Models.Cuentas.Cuentas>();
            this.CuentaDet = new ObservableCollection<Capa_Entidades.Models.Cuentas.Cuenta_Det>();
            this.Pedido2 = negPedido.SP_SELECT_PEDIDO_X_ID_CUENTA(Convert.ToInt32(Application.Current.Properties["id_cuen_pedido"]));
            this.Cuenta = negCuentas.GetPedidoxId(Convert.ToInt32(Application.Current.Properties["id_cuen_pedido"]));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand2 = new RelayCommand(new Action(GuardarCuenta));
            this.CerrarCommand = new RelayCommand(new Action(Cerrar));
            this.PasarPlatosCommand = new ParamCommand(new Action<object>(PasarPlatos));
            this.EliminaPlatoCommand = new ParamCommand(new Action<object>(ElminarPlatos));
            this.imprimirCuenta = new ParamCommand(new Action<object>(ImprimirCuenta));
            this.AnularCuentaCommand = new ParamCommand(new Action<object>(AnularCuenta));
            this.PagarCommand2 = new ParamCommand(new Action<object>(PagarCuenta));
            this.BuscarCliente = new RelayCommand(new Action(BuscaCliente));
            this.VisibleCuentas = "Visible";
            this.VisibleNuevaCuenta = "Collapsed";
            this.VisiblePagarCuenta = "Collapsed";
            this.VisibleAccion = "Visible";
            this.CantidadElegir = "Collapsed";
            this.VerLista = "Visible";

            #region PagoViewModel
            DataCliente = negCli.GetCliente2();
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
            Application.Current.Properties["EstPagoCuenta"] = null;
            this.ComboTipoMoneda = negCombo.GetComboTipoMoneda();
            this.TipoPagoSelectedPropina = ComboFormaPagoPropina.FirstOrDefault();

            if (Application.Current.Properties["id_cuen_pedido"] != null)
            {
                this.Pedido = Application.Current.Properties["id_cuen_pedido"].ToString();
            }
            
            ObservableCollection<Empresa> empresa = new ObservableCollection<Empresa>();
            empresa = negEmpr.GetEmpresa();
            LogoEmpresa = empresa.Where(w => w.EMPR_DEFAULT == "1").First().empr_logo;
            #endregion

            this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
            this.Guardar = new RelayCommand(new Action(GuardarCantidad));
            this.CombCantidad = new ObservableCollection<ComboCantidad>();
            
        }
        private void CloseDialog()
        {
            this.CantidadElegir = "Collapsed";
            this.VerLista = "Visible";
            Application.Current.Properties["id_det_pedido"] = null;
        }
        private void GuardarCantidad()
        {
            try
            {
                if (CboCantidad.id != 0)
                {
                    if (Application.Current.Properties["id_det_pedido"] != null)
                    {
                        var asd1 = this.CuentaDet.Where(p => p.ID == Application.Current.Properties["id_det_pedido"].ToString()).FirstOrDefault();
                        if (asd1 != null)
                        {
                            this.CuentaDet.Remove(asd1);
                            var asd2 = this.Pedido2.Where(p => p.ID == (int)Application.Current.Properties["id_det_pedido"]).FirstOrDefault();
                            Capa_Entidades.Models.Cuentas.Cuenta_Det det_cuenta = new Capa_Entidades.Models.Cuentas.Cuenta_Det();
                            det_cuenta.ORDEN = (CuentaDet.Count() + 1).ToString();
                            det_cuenta.ID = asd2.ID.ToString();
                            det_cuenta.ID_DET_PED = asd2.ID.ToString();
                            det_cuenta.ID_PED = asd2.DP_ID_PED.ToString();
                            det_cuenta.CANT_DET_CUENTA = (Convert.ToDecimal(CboCantidad.id) + Convert.ToDecimal(asd1.CANT_DET_CUENTA)).ToString();
                            det_cuenta.DESCRIP_DET_CUENTA = asd2.DP_DESCRIP;
                            det_cuenta.PRE_UNI_DET_CUENTA = asd2.DP_PRE_UNI.ToString();
                            det_cuenta.IMP_DET_CUENTA = Convert.ToDecimal(Convert.ToDecimal(CboCantidad.id + Convert.ToDecimal(asd1.CANT_DET_CUENTA)) * Convert.ToDecimal(asd1.PRE_UNI_DET_CUENTA));
                            CuentaDet.Add(det_cuenta);

                            Capa_Entidades.Models.Cuentas.Cuenta_Det det_cuenta_resta = new Capa_Entidades.Models.Cuentas.Cuenta_Det();
                            this.TotalCuenta = CuentaDet.Sum(c => c.IMP_DET_CUENTA).ToString();
                            this.Pedido2.Remove(this.Pedido2.Where(p => p.ID == (int)Application.Current.Properties["id_det_pedido"]).FirstOrDefault());

                            Pedidos pedido = new Pedidos();
                            pedido.ID = Convert.ToInt32(asd2.ID);
                            pedido.DP_ID_PED = Convert.ToInt32(asd2.DP_ID_PED);
                            pedido.DP_CANT = Convert.ToDecimal(asd2.DP_CANT - CboCantidad.id);
                            pedido.DP_DESCRIP = asd2.DP_DESCRIP;
                            pedido.DP_PRE_UNI = Convert.ToDecimal(asd2.DP_PRE_UNI);
                            pedido.DP_IMPORT = (Convert.ToDecimal(asd2.DP_CANT - CboCantidad.id) * Convert.ToDecimal(asd2.DP_PRE_UNI)).ToString();
                            Pedido2.Add(pedido);

                            CloseDialog();
                        }
                        else
                        {
                            var asd2 = this.Pedido2.Where(p => p.ID == (int)Application.Current.Properties["id_det_pedido"]).FirstOrDefault();
                            Capa_Entidades.Models.Cuentas.Cuenta_Det det_cuenta = new Capa_Entidades.Models.Cuentas.Cuenta_Det();
                            det_cuenta.ORDEN = (CuentaDet.Count() + 1).ToString();
                            det_cuenta.ID = asd2.ID.ToString();
                            det_cuenta.ID_DET_PED = asd2.ID.ToString();
                            det_cuenta.ID_PED = asd2.DP_ID_PED.ToString();
                            det_cuenta.CANT_DET_CUENTA = CboCantidad.id.ToString();
                            det_cuenta.DESCRIP_DET_CUENTA = asd2.DP_DESCRIP;
                            det_cuenta.PRE_UNI_DET_CUENTA = asd2.DP_PRE_UNI.ToString();
                            det_cuenta.IMP_DET_CUENTA = Convert.ToDecimal(CboCantidad.id * asd2.DP_PRE_UNI);
                            CuentaDet.Add(det_cuenta);

                            Capa_Entidades.Models.Cuentas.Cuenta_Det det_cuenta_resta = new Capa_Entidades.Models.Cuentas.Cuenta_Det();
                            this.TotalCuenta = CuentaDet.Sum(c => c.IMP_DET_CUENTA).ToString();
                            this.Pedido2.Remove(this.Pedido2.Where(p => p.ID == (int)Application.Current.Properties["id_det_pedido"]).FirstOrDefault());

                            Pedidos pedido = new Pedidos();
                            pedido.ID = Convert.ToInt32(asd2.ID);
                            pedido.DP_ID_PED = Convert.ToInt32(asd2.DP_ID_PED);
                            pedido.DP_CANT = Convert.ToDecimal(asd2.DP_CANT - CboCantidad.id);
                            pedido.DP_DESCRIP = asd2.DP_DESCRIP;
                            pedido.DP_PRE_UNI = Convert.ToDecimal(asd2.DP_PRE_UNI);
                            pedido.DP_IMPORT = (Convert.ToDecimal(asd2.DP_CANT - CboCantidad.id) * Convert.ToDecimal(asd2.DP_PRE_UNI)).ToString();
                            Pedido2.Add(pedido);

                            CloseDialog();
                        }
                        
                        //global.Mensaje("SOS-FOOD - Informacion", "Cantidad" + CboCantidad.id, 2);
                    }
                    else
                    {
                        global.Mensaje("SOS-FOOD - Informacion", "Ingrese cantidad valida", 2);
                    }
                }
                else
                {
                    global.Mensaje("SOS-FOOD - Informacion", "Ingrese cantidad valida", 2);
                }
            }
            catch (Exception ex)
            {
                global.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
        }
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
        #region PagaPedido
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
        #endregion
        #region EliminarPagoDet
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
        #endregion
        #region guardar Pedido
        private async void GuardarPed()
        {
            Application.Current.Properties["EstPago"] = null;
            if (this.Operacion_detPago == "CAMBIAR PAGO")
            {
                if (this.Saldos == this.totals || this.Saldos == 0)
                {
                    GuardarCambioPago();

                }
                else
                    global.MensajeSnack("Tiene un saldo por cancelar");
            }
            else
            {
                bool FacturacionEnable = Convert.ToBoolean(negParametros.FacturacionElectronica());
                if (FacturacionEnable == false)
                {
                    ischeckTicket = true;
                }
                else
                {
                    if (ischeckTicket == false)
                    {
                        if (ischeckBoleta == true)
                        {
                            if (RucCliente.Trim().Length > 0 && RucCliente.Trim().Length != 8 && RucCliente.Trim() != "0")
                            {
                                global.Mensaje("SOS-FOOD - Informacion", "El DNI debe tener 11 digitos", 2);
                                return;
                            }
                            Application.Current.Properties["TipDocElectronico"] = 1;
                            Application.Current.Properties["TipoComprobanteElectronicoDelivery"] = "BOLETA";

                        }
                        else if (ischeckFactura == true)
                        {
                            if (RucCliente.Trim().Length != 11)
                            {
                                global.Mensaje("SOS-FOOD - Informacion", "El RUC debe tener 11 digitos", 2);
                                return;
                            }

                            Application.Current.Properties["TipDocElectronico"] = 2;
                            Application.Current.Properties["TipoComprobanteElectronicoDelivery"] = "FACTURA";
                        }
                        Application.Current.Properties["RucCliente"] = RucCliente;
                        Application.Current.Properties["RazonCliente"] = RazonCliente;
                        Application.Current.Properties["CorreoCliente"] = CorreoCliente;
                        Application.Current.Properties["DireccionCliente"] = DireccionCliente;
                        Application.Current.Properties["IdPedidoDoc"] = Convert.ToInt32(this.Pedido);
                        Application.Current.Properties["IdCuentaDoc"] = Convert.ToInt32(Application.Current.Properties["id_cuenta"].ToString());
                        Application.Current.Properties["ClienteLlevar"] = null;
                        Application.Current.Properties["PagarconCliente"] = Pagarcon;
                        Application.Current.Properties["VueltoCliente"] = Vuelto;

                        if (ischeckBoleta == true)
                        {
                            TipoDoc = "1";
                        }
                        else
                        {
                            TipoDoc = "6";
                        }
                        Application.Current.Properties["TipoDoc"] = TipoDoc;
                    }
                }
                
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
                if (this.Tips == "" || this.Tips == "0")
                {
                    pagar.idtpagoPropina = null;
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("DT_ID_TIP_MONEDA");
                dt.Columns.Add("DT_ID_FORM_PAGO");
                dt.Columns.Add("DT_AMORT");
                dt.Columns.Add("DT_TOTAL");
                dt.Columns.Add("DT_NRO_TARJETA");
                decimal suma = 0;
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
                }
                if (this.Totals == suma)
                {
                    string _mensaje1 = "";

                    bool result = negCuentas.SetPagar((_id == 0 ? 1 : 2), pagar, ref _mensaje1, NombreEquipo, dt, Application.Current.Properties["id_cuenta"].ToString());
                    if (result)
                    {
                        //bool internet = global.ValidarInternet();
                        //if (internet == true)
                        //{
                        //    string id_pedido = this.Pedido;
                        //    DataTable dt2 = negPedido.GetDeliveryxPedidoApp(Convert.ToInt32(id_pedido));
                        //    if (dt2 != null)
                        //    {
                        //        if (dt2.Rows.Count > 0)
                        //        {
                        //            empresa = negEmpresa.GetEmpresa();
                        //            for (int j = 0; j < dt2.Rows.Count; j++)
                        //            {
                        //                using (var client = new WebClient())
                        //                {
                        //                    var values = new NameValueCollection();
                        //                    //values["token"] = "app963";
                        //                    values["orderid"] = dt2.Rows[j]["id"].ToString();
                        //                    values["id_business"] = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().codigo;
                        //                    values["status"] = "PEDIDO CONCLUIDO";
                        //                    var response = client.UploadValues(negParametros.UpdateState(), values);
                        //                    var responseString = Encoding.Default.GetString(response);
                        //                    negDeliveryWebService.sp_cambiar_estado_pedido_delivery(Convert.ToInt32(dt2.Rows[0]["id"]), "PEDIDO CONCLUIDO");
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        if (FacturacionEnable == false)
                        {
                            ischeckTicket = true;
                        }
                        else
                        {
                            if (ischeckTicket == false)
                            {
                                Application.Current.Properties["RucCliente"] = RucCliente;
                                Procesos_FE pro = new Procesos_FE();
                                pro.GeneraFE();
                            }
                        }
                            
                            

                        DataTable dt2 = new DataTable();
                        dt2 = negCuentas.sp_verifica_pago(Convert.ToInt32(Application.Current.Properties["id_cuen_pedido"]));

                        if (dt2.Rows[0]["PED_ID_ESTADO"].ToString().Trim() == "1")
                        {
                            Application.Current.Properties["EstPagoCuenta"] = "PAGADO"; 
                            Cancelar();
                        }
                        else
                        {
                            Application.Current.Properties["EstPagoCuenta"] = "PENDIENTE";
                            globales.Mensaje("SOS-FOOD - Exito", "Cuenta Pagada Correctamente", 1);
                            this.VisibleCuentas = "Visible";
                            this.VisibleNuevaCuenta = "Collapsed";
                            this.VisiblePagarCuenta = "Collapsed";
                            this.VisibleAccion = "Visible";
                            this.CantidadElegir = "Collapsed";
                            this.VerLista = "Visible";
                            this.Titulo = "Cuenta Principal";
                            NomCuenta = "";
                            this.CuentaDet = new ObservableCollection<Capa_Entidades.Models.Cuentas.Cuenta_Det>();
                            this.TotalCuenta = "0.00";
                            this.Pedido2 = negPedido.SP_SELECT_PEDIDO_X_ID_CUENTA(Convert.ToInt32(Application.Current.Properties["id_cuen_pedido"]));
                            this.Cuenta = negCuentas.GetPedidoxId(Convert.ToInt32(Application.Current.Properties["id_cuen_pedido"]));
                        }
                        
                    }
                }
                else
                {
                    global.MensajeSnack("Tiene un saldo por cancelar");
                }

            }
        }
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
            bool result = negCuentas.SetPagar((_id == 0 ? 1 : 2), pagar, ref _mensaje, NombreEquipo, dt, Application.Current.Properties["id_cuenta"].ToString());
            globales.Mensaje("SOS-FOOD Mensaje:", _mensaje, 2);
            return;
            //}
            //else
            //{
            //    IsActive = true;
            //}

        }
        #endregion
        public string IdCLiente { get; set; }
        public string RucCliente { get; set; } = "";
        public string RazonCliente { get; set; } = "";
        public string CorreoCliente { get; set; } = "";
        public string TelefonoCliente { get; set; } = "";
        public string DireccionCliente { get; set; } = "";
        public string TipoDoc { get; set; } = "";
        public async void BuscaCliente()
        {
            string nombrecompleto;
            ServiceReference1.sosfoodSoapClient vf = new ServiceReference1.sosfoodSoapClient();
            var cadena = RucCliente;
            if (cadena.Trim() != "")
            {
                if (DataCliente != null)
                {
                    if (DataCliente.Where(c => c.ndoc == cadena).Count() != 0)
                    {
                        this.IdCLiente = DataCliente.Where(c => c.ndoc == cadena).FirstOrDefault().idcli.ToString();
                        this.RucCliente = DataCliente.Where(c => c.ndoc == cadena).FirstOrDefault().ndoc;
                        this.RazonCliente = DataCliente.Where(c => c.ndoc == cadena).FirstOrDefault().denominacion;
                        this.CorreoCliente = DataCliente.Where(c => c.ndoc == cadena).FirstOrDefault().corcli;
                        this.TelefonoCliente = DataCliente.Where(c => c.ndoc == cadena).FirstOrDefault().telcli;
                        this.DireccionCliente = DataCliente.Where(c => c.ndoc == cadena).FirstOrDefault().dircli;
                        return;
                    }
                }
            }
            else
            {
                return;
            }
            int Desc;
            if (ischeckBoleta == true)
            {
                if (cadena != "" && cadena.Length == 8)
                {
                    try
                    {
                        nombrecompleto = vf.ConsultaDNI(this.RucCliente);
                        string[] partes = nombrecompleto.Split(' ');
                        string result = partes[0] + ' ' + partes[1];
                        string result1 = partes[2] + ' ' + partes[3];
                        this.RazonCliente = result1 + " " + result;
                        this.CorreoCliente = "";
                        this.TelefonoCliente = "";
                        this.DireccionCliente = "";
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    return;
                }
            }
            else if (ischeckFactura == true)
            {
                if (cadena != "" && cadena.Length == 11)
                {
                    try
                    {
                        string valor = "";
                        int _existe = 0;


                        ServiceReference1.ClsClienteConsultaEN cn;
                        cn = vf.ConsultaRuc("0a682fbe-009d-4758-aad1-2ff1092ab7c2-838d6cd5-620a-4add-9632-a3b37c5ae216", this.RucCliente);
                        this.RazonCliente = cn.nombre_o_razon_social;
                        this.DireccionCliente = cn.direccion_completa;
                        this.CorreoCliente = "";
                        this.TelefonoCliente = "";

                        valor = cn.estado_del_contribuyente;

                    }
                    catch (Exception ex)
                    {
                        return;
                    }

                }
                else
                {
                    return;
                }


            }
        }
        public void GuardarCuenta()
        {
            try
            {
                if (VisibleNuevaCuenta == "Visible")
                {
                    if (this.NomCuenta.Trim() == "")
                    {
                        globales.Mensaje("SOS-FOOD Mensaje:", "Debe ingresar un nombre a la cuenta", 2);
                        return;
                    }
                    if (this.CuentaDet.Count == 0)
                    {
                        globales.Mensaje("SOS-FOOD Mensaje:", "Debe agregar items a la cuenta", 2);
                        return;
                    }
                    if (this.CuentaDet.Count != 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("ID_DET_PED");
                        dt.Columns.Add("DC_CANT");
                        dt.Columns.Add("DC_DESCRIP");
                        dt.Columns.Add("DC_PRE_UNI");
                        dt.Columns.Add("DC_IMPORT");
                        foreach (var item in this.CuentaDet)
                        {
                            dt.Rows.Add(item.ID_DET_PED, item.CANT_DET_CUENTA, item.DESCRIP_DET_CUENTA, item.PRE_UNI_DET_CUENTA, item.IMP_DET_CUENTA);
                        }
                        string _mensaje = "";
                        int result = negCuentas.SetCuenta(dt, Convert.ToInt32(Application.Current.Properties["id_cuen_pedido"]).ToString(), NomCuenta, ref _mensaje);
                        if (result != 0)
                        {
                            globales.Mensaje("SOS-FOOD - Exito", "Cuenta Generada Correctamente", 1);
                            this.VisibleCuentas = "Visible";
                            this.VisibleNuevaCuenta = "Collapsed";
                            this.VisiblePagarCuenta = "Collapsed";
                            this.VisibleAccion = "Visible";
                            this.Titulo = "Cuenta Principal";
                            this.CantidadElegir = "Collapsed";
                            this.VerLista = "Visible";
                            NomCuenta = "";
                            this.CuentaDet = new ObservableCollection<Capa_Entidades.Models.Cuentas.Cuenta_Det>();
                            this.TotalCuenta = "0.00";
                            this.Pedido2 = negPedido.SP_SELECT_PEDIDO_X_ID_CUENTA(Convert.ToInt32(Application.Current.Properties["id_cuen_pedido"]));
                            this.Cuenta = negCuentas.GetPedidoxId(Convert.ToInt32(Application.Current.Properties["id_cuen_pedido"]));
                            Application.Current.Properties["EstPagoCuenta"] = "PENDIENTE";
                        }
                    }
                }
                else
                {
                    globales.Mensaje("SOS-FOOD Mensaje:", "Debe generar una nueva cuenta", 2);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void Cancelar()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void Cerrar()
        {
            this.VisibleCuentas = "Visible";
            this.VisibleNuevaCuenta = "Collapsed";
            this.VisiblePagarCuenta = "Collapsed";
            this.VisibleAccion = "Visible";
            this.CantidadElegir = "Collapsed";
            this.VerLista = "Visible";
            this.Titulo = "Cuenta Principal";
            NomCuenta = "";
            this.CuentaDet = new ObservableCollection<Capa_Entidades.Models.Cuentas.Cuenta_Det>();
            this.TotalCuenta = "0.00";
            this.Pedido2 = negPedido.SP_SELECT_PEDIDO_X_ID_CUENTA(Convert.ToInt32(Application.Current.Properties["id_cuen_pedido"]));
        }
        private void Nuevo()
        {
            this.VisibleCuentas = "Collapsed";
            this.VisibleNuevaCuenta = "Visible";
            this.VisiblePagarCuenta = "Collapsed";
            this.VisibleAccion = "Visible";
            this.Titulo = "Cuenta Principal";
            this.CantidadElegir = "Collapsed";
            this.VerLista = "Visible";
            NomCuenta = "Cuenta N°" + (this.Cuenta.Count + 1).ToString();
            this.CuentaDet = new ObservableCollection<Capa_Entidades.Models.Cuentas.Cuenta_Det>();
            this.TotalCuenta = "0.00";
        }
        private void PasarPlatos(object id)
        {
            try
            {
                if (VisibleNuevaCuenta == "Visible")
                {
                    var asd2 = this.Pedido2.Where(p => p.ID == (int)id).FirstOrDefault();
                    if (asd2.DP_CANT > 1)
                    {
                        this.CantidadElegir = "Visible";
                        this.VerLista = "Collapsed";
                        this.CboCantidad = new ComboCantidad();
                        this.CombCantidad = new ObservableCollection<ComboCantidad>();
                        for (int j = 1; j <= Convert.ToInt32(asd2.DP_CANT); j++)
                        {
                            ComboCantidad combo = new ComboCantidad();
                            combo.id = j;
                            combo.value = j;
                            this.CombCantidad.Add(combo);
                        }
                        Application.Current.Properties["id_det_pedido"] = id;
                    }
                    else
                    {
                        var asd1 = this.CuentaDet.Where(p => p.ID == id.ToString()).FirstOrDefault();
                        if (asd1 != null)
                        {
                            this.CuentaDet.Remove(asd1);
                            Capa_Entidades.Models.Cuentas.Cuenta_Det det_cuenta = new Capa_Entidades.Models.Cuentas.Cuenta_Det();
                            det_cuenta.ORDEN = (CuentaDet.Count() + 1).ToString();
                            det_cuenta.ID = asd2.ID.ToString();
                            det_cuenta.ID_DET_PED = asd2.ID.ToString();
                            det_cuenta.ID_PED = asd2.DP_ID_PED.ToString();
                            det_cuenta.CANT_DET_CUENTA = (asd2.DP_CANT + Convert.ToDecimal(asd1.CANT_DET_CUENTA)).ToString();
                            det_cuenta.DESCRIP_DET_CUENTA = asd2.DP_DESCRIP;
                            det_cuenta.PRE_UNI_DET_CUENTA = asd2.DP_PRE_UNI.ToString();
                            det_cuenta.IMP_DET_CUENTA = Convert.ToDecimal((asd2.DP_CANT + Convert.ToDecimal(asd1.CANT_DET_CUENTA)) * asd2.DP_PRE_UNI);
                            CuentaDet.Add(det_cuenta);
                            this.TotalCuenta = CuentaDet.Sum(c => c.IMP_DET_CUENTA).ToString();
                            this.Pedido2.Remove(this.Pedido2.Where(p => p.ID == (int)id).FirstOrDefault());
                        }
                        else
                        {

                            Capa_Entidades.Models.Cuentas.Cuenta_Det det_cuenta = new Capa_Entidades.Models.Cuentas.Cuenta_Det();
                            det_cuenta.ORDEN = (CuentaDet.Count() + 1).ToString();
                            det_cuenta.ID = asd2.ID.ToString();
                            det_cuenta.ID_DET_PED = asd2.ID.ToString();
                            det_cuenta.ID_PED = asd2.DP_ID_PED.ToString();
                            det_cuenta.CANT_DET_CUENTA = asd2.DP_CANT.ToString();
                            det_cuenta.DESCRIP_DET_CUENTA = asd2.DP_DESCRIP;
                            det_cuenta.PRE_UNI_DET_CUENTA = asd2.DP_PRE_UNI.ToString();
                            det_cuenta.IMP_DET_CUENTA = Convert.ToDecimal(asd2.DP_IMPORT);
                            CuentaDet.Add(det_cuenta);
                            this.TotalCuenta = CuentaDet.Sum(c => c.IMP_DET_CUENTA).ToString();
                            this.Pedido2.Remove(this.Pedido2.Where(p => p.ID == (int)id).FirstOrDefault());
                        }
                    }
                }
                else
                {
                    globales.Mensaje("SOS-FOOD Mensaje:", "Debe generar una nueva cuenta", 2);
                }
            }
            catch (Exception ex)
            {
                globales.Mensaje("SOS-FOOD Error:", ex.Message, 3);
            }   
        }
        private void ElminarPlatos(object id)
        {
            try
            {
                if (VisibleNuevaCuenta == "Visible")
                {
                    var asd2 = this.CuentaDet.Where(p => p.ORDEN == id.ToString()).FirstOrDefault();
                    string id_det = asd2.ID_DET_PED;
                    var asd1 = this.Pedido2.Where(p => p.ID == Convert.ToInt32(id_det)).FirstOrDefault();
                    if (asd1 != null)
                    {
                        this.Pedido2.Remove(asd1);

                        Pedidos pedido = new Pedidos();
                        pedido.ID = Convert.ToInt32(asd2.ID_DET_PED);
                        pedido.DP_ID_PED = Convert.ToInt32(asd2.ID_PED);
                        pedido.DP_CANT = Convert.ToDecimal(asd2.CANT_DET_CUENTA) + asd1.DP_CANT;
                        pedido.DP_DESCRIP = asd2.DESCRIP_DET_CUENTA;
                        pedido.DP_PRE_UNI = Convert.ToDecimal(asd2.PRE_UNI_DET_CUENTA);
                        pedido.DP_IMPORT = ((Convert.ToDecimal(asd2.CANT_DET_CUENTA) + asd1.DP_CANT) * Convert.ToDecimal(asd2.PRE_UNI_DET_CUENTA)).ToString();
                        Pedido2.Add(pedido);

                        this.CuentaDet.Remove(this.CuentaDet.Where(p => p.ORDEN == id.ToString()).FirstOrDefault());
                        this.TotalCuenta = CuentaDet.Sum(c => c.IMP_DET_CUENTA).ToString();
                    }
                    else
                    {
                        Pedidos pedido = new Pedidos();
                        pedido.ID = Convert.ToInt32(asd2.ID_DET_PED);
                        pedido.DP_ID_PED = Convert.ToInt32(asd2.ID_PED);
                        pedido.DP_CANT = Convert.ToDecimal(asd2.CANT_DET_CUENTA);
                        pedido.DP_DESCRIP = asd2.DESCRIP_DET_CUENTA;
                        pedido.DP_PRE_UNI = Convert.ToDecimal(asd2.PRE_UNI_DET_CUENTA);
                        pedido.DP_IMPORT = asd2.IMP_DET_CUENTA.ToString();
                        Pedido2.Add(pedido);

                        this.CuentaDet.Remove(this.CuentaDet.Where(p => p.ORDEN == id.ToString()).FirstOrDefault());
                        this.TotalCuenta = CuentaDet.Sum(c => c.IMP_DET_CUENTA).ToString();
                    }
                }
                else
                {
                    globales.Mensaje("SOS-FOOD Mensaje:", "Debe generar una nueva cuenta", 2);
                }
            }
            catch (Exception ex)
            {
                globales.Mensaje("SOS-FOOD Error:", ex.Message, 3);
            }
        }
        private void ImprimirCuenta(object id)
        {
            try
            {
                DataTable impresora = new DataTable();
                string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
                DataTable dt = new DataTable();
                dt = negCuentas.GetCuentaxIdCuenta(Convert.ToInt32(id));
                Ticket ticket = new Ticket();
                ticket.MaxCharDescrip = negParametros.margenMaxDescrip();
                if (Convert.ToBoolean(negParametros.logoPreCuenta()) == true)
                {
                    System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["EMPR_LOGO"]));
                    ticket.MargenLogo = negParametros.margenLogoTicket();
                    ticket.HeaderImage = x;
                }
                
                ticket.AddSubHeaderLine("Cuenta: " + dt.Rows[0]["NOM_CUENTA"].ToString());
                ticket.AddSubHeaderLine("NRO. Orden: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                ticket.AddSubHeaderLine("Atendido Por: " + dt.Rows[0]["EMPL_NOM"].ToString());
                ticket.AddSubHeaderLine("Mesa: " + dt.Rows[0]["M_NOM"].ToString());
                ticket.AddSubHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    //ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                    if (dt.Rows[j]["DP_COMENTARIO"].ToString().Trim() == "")
                    {
                        ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DESCRIP_DET_CUENTA"].ToString() + dt.Rows[j]["DP_COMENTARIO"].ToString(), Math.Round(Convert.ToDecimal(dt.Rows[j]["PRE_UNI_DET_CUENTA"]),2).ToString(), Math.Round(Convert.ToDecimal(dt.Rows[j]["IMPORTE_DETALLEPEDIDO"]), 2).ToString());
                    }
                    else
                    {
                        //ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + "c/ " + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                        //preZumo
                        ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DESCRIP_DET_CUENTA"].ToString() + " " + dt.Rows[j]["DP_COMENTARIO"].ToString(), Math.Round(Convert.ToDecimal(dt.Rows[j]["PRE_UNI_DET_CUENTA"]), 2).ToString(), Math.Round(Convert.ToDecimal(dt.Rows[j]["IMPORTE_DETALLEPEDIDO"]), 2).ToString());
                    }

                    if (j == 35 || j == 70 || j == 105 || j == 140 || j == 175 || j == 205)
                    {
                        if (ticket.PrinterExists(ImpCaja))
                        {
                            ticket.PrintTicket(ImpCaja);
                            ticket = new Ticket();
                        }
                        else
                        {
                            globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                        }
                    }
                }
                //ticket.AddTotal("SUB TOTAL", dt.Rows[0]["PED_SUBTOTAL"].ToString());
                //ticket.AddTotal("DESC.", dt.Rows[0]["PED_DESCUENTO"].ToString());
                //ticket.AddTotal("", "---------");
                ticket.AddFooterLine("");
                ticket.AddTotal("TOTAL", Math.Round(Convert.ToDecimal(dt.Rows[0]["SUBTOTAL_CUENTA"]),2).ToString());
                    ticket.AddFooterLine("");
                    ticket.AddTotal("", "---------");

                    ticket.AddFooterLine("");
                    ticket.AddFooterLine("Este documento no es comprobante de pago.");
                    ticket.AddFooterLine("");
                    ticket.AddFooterLine("   Sistema para restaurantes SOS-FOOD");
                    ticket.AddFooterLine("        Desarrollado por SOS-TIC");
                    ticket.AddFooterLine("   www.sos-food.com   www.sos-tic.com");
                    if (ticket.PrinterExists(ImpCaja))
                    {
                        ticket.PrintTicket(ImpCaja);
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                    }
            }
            catch (Exception ex)
            {
                globales.Mensaje("SOS-FOOD Error:", ex.Message, 3);
            }
        }
        private void AnularCuenta(object id)
        {
            try
            {
                string _mensaje = "";
                bool result = negCuentas.sp_anular_cuenta(id.ToString(),ref _mensaje);
                if (result == true)
                {
                    globales.Mensaje("SOS-FOOD - Anulacion", "Cuenta Anulada Correctamente", 1);
                    this.Pedido2 = negPedido.SP_SELECT_PEDIDO_X_ID_CUENTA(Convert.ToInt32(Application.Current.Properties["id_cuen_pedido"]));
                    this.Cuenta = negCuentas.GetPedidoxId(Convert.ToInt32(Application.Current.Properties["id_cuen_pedido"]));
                }
            }
            catch (Exception ex)
            {
                globales.Mensaje("SOS-FOOD Error:", ex.Message, 3);
            }
        }
        private void PagarCuenta(object id)
        {
            try
            {
                this.VisibleCuentas = "Collapsed";
                this.VisibleNuevaCuenta = "Collapsed";
                this.VisiblePagarCuenta = "Visible";
                this.VisibleAccion = "Collapsed";
                this.CantidadElegir = "Collapsed";
                this.VerLista = "Visible";
                this.Pedido2 = negPedido.SP_SELECT_DET_X_ID_CUENTA2(Convert.ToInt32(Application.Current.Properties["id_cuen_pedido"]), Convert.ToInt32(id));
                this.Titulo = "Cuenta : " + this.Cuenta.Where(c => c.ID == id).FirstOrDefault().NOM_CUENTA;
                GetListaDetPago();
                amortizars = 0;
                saldos = Convert.ToDecimal(this.Cuenta.Where(c => c.ID == id).FirstOrDefault().SUBTOTAL_CUENTA);
                this.Totals = Convert.ToDecimal(this.Cuenta.Where(c => c.ID == id).FirstOrDefault().SUBTOTAL_CUENTA);
                this.Amortizar = Convert.ToDecimal(this.Cuenta.Where(c => c.ID == id).FirstOrDefault().SUBTOTAL_CUENTA);
                this.Usuario = Application.Current.Properties["IdUsuario"].ToString();
                this.EstadoPago = "PENDIENTE";
                this.PagoCuenta = "Cuenta : " + this.Cuenta.Where(c => c.ID == id).FirstOrDefault().NOM_CUENTA;
                Application.Current.Properties["id_cuenta"] = id;

                this.RucCliente = "";
                this.RazonCliente = "";
                this.DireccionCliente = "";
                this.TelefonoCliente = "";
                if (negParametros.TipoDocPagar() == true)
                {
                    ischeckTicket = true;
                }
                else
                {
                    ischeckBoleta = true;
                }
            }
            catch (Exception ex)
            {
                globales.Mensaje("SOS-FOOD Error:", ex.Message, 3);
            }
        }
        private void GetListaDetPago()
        {
            try
            {
                int idpedido = -1;
                DataDetallePagar = negCuentas.GetDetallePago(idpedido,Convert.ToInt32(Application.Current.Properties["id_cuenta"]));
                if (DataDetallePagar.Count > 0)
                {
                    IsEnabledbtnguardar = true;
                    var s = DataDetallePagar.FirstOrDefault().saldos;
                    var a = DataDetallePagar.FirstOrDefault().amortizars;
                    var k = DataDetallePagar.FirstOrDefault().saldos;
                    this.Saldos = s;
                    this.Amortizars = a;
                    this.Amortizar = k;
                }
            }
            catch (Exception ex)
            {
                globales.Mensaje("SOS-FOOD Error:", ex.Message, 3);
            }
        }
    }
}

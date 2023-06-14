using Capa_Datos.Data.Reportes.VentasPorDia;
using Capa_Entidades;
using Capa_Entidades.Models;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Entidades.Models.Reportes.Pedido;
using Capa_Entidades.Models.Usuario;
using Capa_Entidades.Models.Web_Service;
using Capa_Negocio;
using Capa_Negocio.Carta;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Delivery_Web_Service;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Pedido;
using Capa_Negocio.Reportes;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Negocio.User;
using Capa_Presentacion_WPF.Views.Dialogs;
using Capa_Presentacion_WPF.Views.Dialogs.DialogVentaporDia;
using Capa_Presentacion_WPF.Views.Dialogs.Facturacion_Electronica;
using ExportToExcel;
using LiveCharts;
using LiveCharts.Wpf;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Newtonsoft.Json;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.ReporteporDias
{
    public class VentasDiaViewModel : IGeneric
    {
        #region Negocio
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Descripcion, int Reserved);
        Neg_VentasDia negVentaD = new Neg_VentasDia();
        Neg_VentasMozo negvMozo = new Neg_VentasMozo();
        Neg_BoletaFactura negBolFac = new Neg_BoletaFactura();
        Neg_Detalle_pedido negDetVent = new Neg_Detalle_pedido();
        Funcion_Global globales = new Funcion_Global();
        Neg_Parametros negParametros = new Neg_Parametros();
        Neg_Usuario negUser = new Neg_Usuario();
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Empresa negEmpresa = new Neg_Empresa();
        Neg_DeliveryWebService negDeliveryWebService = new Neg_DeliveryWebService();
        #endregion
        #region Objetos
        Funcion_Global global { get; set; }
        private VentasDia ventasdia;
        private Detalle_Pedido detalle_pedido;
        private decimal _monto;
        private decimal _montodolar;
        private string _persona;
        private string _empresa;
        private string _totalcli;
        private string _promedio;
        private string _promediodolar;
        private BoletaFactura boletafactura;
        private string operacion;
        public int id_pedido { get; set; }
        public decimal totalcaja { get; set; }
        public string Nanulados { get; set; }
        public string Panulados { get; set; }
        public string Porcentaje { get; set; }
        public string VAMonto { get; set; }
        public string NPedidos { get; set; }
        public object FA_minimo { get; set; }
        public object FA_maximo { get; set; }
        public object VDVP_monto_P { get; set; }
        public object VDVP_monto_A { get; set; }
        public Decimal DC_MontoBol { get; set; }
        public Decimal DC_MontoFac { get; set; }
        public Decimal DC_Total { get; set; }
        public Decimal DC_Sum_Total { get; set; }
        public int DC_CantDocBol { get; set; }
        public int DC_CantDocFAc { get; set; }
        public decimal DC_MontoBolAnuladas { get; set; }
        public int DC_CantBolAnuladas { get; set; }
        public decimal DC_MontoFactAnuladas { get; set; }
        public int DC_CantFactAnuladas { get; set; }
        public decimal TotalxCobrar { get; set; }
        public bool FacturacionEnable { get; set; } = true;
        public decimal TotalEfectivo { get; set; } = 0;
        public string NombreEquipo { get; set; }
        #endregion
        #region Lista
        public ObservableCollection<Pagar> DataPedido { get; set; }
        public List<Pagar> Pedido2 { get; set; }
        public ObservableCollection<Empresa> DataEmpresa { get; set; }

        public ObservableCollection<Usuario> DataUsuario { get; set; }
        public ObservableCollection<Detalle_Pedido> DataDetprod { get; set; }
        public ObservableCollection<Detalle_Pedido> DataDetallePedido { get; set; }
        //ObservableCollection<BoletaFactura> DataBolFac { get; set; }
        public List<VentasDia> datatotal { get; set; }
        public List<VentasDia> datacliente { get; set; }
        public List<VentasDia> dataPromedioMesas { get; set; }
        public List<VentasDia> dataTipopago { get; set; }
        public List<VentasDia> dataRecordMozo { get; set; }
        public List<VentasDia> dataMesasAtendidas { get; set; }
        public List<VentasDia> dataCajachica { get; set; }
        public List<VentasDia> dataVentasAnulados { get; set; }
        public List<Ent_Combo> ComboTipoDoc { get; set; }
        public List<VentasDia> dataDocElect { get; set; }
        public List<VentasDia> dataDocElectAnu { get; set; }
        public List<VentasDia> dataDocElectTODOS { get; set; }
        public List<VentasDia> dataFrecAtencion { get; set; }
        public List<VentasDia> dataVentasDiavsVentasPasadas { get; set; }
        public List<VentasDia> dataVentasPorHora { get; set; }
        public ObservableCollection<BoletaFactura> DataBolFac { get; set; }
        public ObservableCollection<VentasDia> DataVentasxCanal { get; set; }
        #endregion
        #region Entidad
        public BoletaFactura BoletaFactura
        {
            get => boletafactura;
            set
            {
                boletafactura = value;
            }
        }

        public VentasDia VentasDia
        {
            get => ventasdia;
            set
            {
                ventasdia = value;
            }
        }
        public Detalle_Pedido Detalle_Pedido
        {
            get => detalle_pedido;
            set
            {
                detalle_pedido = value;
            }
        }
        public decimal TotalVenta { get; set; }

        public decimal Monto
        {
            get => _monto;
            set
            {

                _monto = value;
            }
        }
        public decimal MontoDolar
        {
            get => _montodolar;
            set
            {

                _montodolar = value;
            }
        }
        public string VisibleMontoDolar { get; set; }
        public decimal Descuento { get; set; }
        public string Dias { get; set; }
        public string Empresa
        {
            get => _empresa;
            set
            {
                _empresa = value;
            }
        }
        public string Persona
        {
            get => _persona;
            set
            {
                _persona = value;
            }
        }
        #endregion
        #region tiket promedio
        public int TP_Nro_personas { get; set; }
        public decimal TP_totalpromedio { get; set; }
        #endregion
        #region values Card Consolidado
        public string TotalCli
        {
            get => _totalcli;
            set
            {
                _totalcli = value;
            }
        }
        public Decimal totalEmpre { get; set; }
        public Decimal totalPer { get; set; }
        public decimal PM_Nventas { get; set; }
        public decimal PM_NventasDolar { get; set; }
        public string PM_Promedio
        {
            get => _promedio;
            set
            {
                _promedio = value;
            }
        }
        public string PM_PromedioDolr
        {
            get => _promediodolar;
            set
            {
                _promediodolar = value;
            }
        }
        public string VisiblePromedioDolr { get; set; }
        #region entidad tipo pago
        public string TP_denoTarj { get; set; }
        public string TP_denoEfec { get; set; }
        public object TP_id { get; set; }
        public decimal TP_MontoEfec { get; set; }
        public decimal TP_MontoTarj { get; set; }
        public decimal TP_MontoPropina { get; set; }
        public decimal TP_MontoPropinaEfectivo { get; set; }

        public decimal TP_PorcenTarj { get; set; }
        public decimal TP_PorcenEfec { get; set; }
        #endregion
        #region èntidad ultima hora
        public double UH_hora { get; set; }
        #endregion
        public string RMMozo { get; set; }
        public decimal RMProcentaje { get; set; }
        public int RMTotalPedido { get; set; }
        public int MA_nmesas { get; set; }
        public decimal MA_promedio { get; set; }
        public decimal MA_totalmesas { get; set; }

        #region card_cajachica
        public decimal CC_ingreso { get; set; }
        public decimal CC_egreso { get; set; }
        public decimal CC_total { get; set; }
        #endregion
        #endregion
        public Detalle_Pedido SelectedDataFile { get; set; }
        Neg_Platos negPlatos = new Neg_Platos();
        #region Icommands
        public ICommand ConsolidadoCommand { get; set; }
        public ICommand DetalladoCommand { get; set; }
        //public ICommand botonBolFacCommand { get; set; }
        public ICommand ImprimirCuentaCommand { get; set; }
        public ICommand BoletaFacturaCommand { get; set; }
        public ICommand FacturaCommand { get; set; }
        public ICommand CargarSunatCommand { get; set; }
        public ICommand PagarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand OpenDialogPagarCommand { get; set; }
        public ICommand OpenDialogAnularPedidoCommand { get; set; }
        public ICommand OpenDialogCambiarPagoCommand { get; set; }
        public ICommand OpenDialogReporteFechaCommand { get; set; }
        public ICommand OpenDialogBolFactCommand { get; set; }
        public ICommand CerrarCajaCommand { get; set; }
        public ICommand DrillDownCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand AbrirDialogDetPropinas { get; set; }
        public ICommand AbrirDialogDetPedido { get; set; }
        public ICommand AbrirDialogDetPagos { get; set; }
        public ICommand AbrirDialogDetVentasAnualdos { get; set; }
        public ICommand AbrirDialogDetProdAnualdos{ get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand CerrarDialog { get; set; }
        public ICommand ExportarExcelConsolidadoCommand { get; set; }
        private ICommand m_RowClickCommand;
        public ICommand RowClickCommand
        {
            get
            {
                return m_RowClickCommand;
            }
        }
        #endregion
        public string Operacion
        {
            get => operacion;
            set
            {
                if (value == "Detallado")
                {
                    GetLista();
                    this.id_pedido = 0;
                }
                operacion = value;
            }
        }
        private void DataList()
        {
            this.datatotal = negVentaD.GetVentasDia_Venta(NombreEquipo);
            this.datacliente = negVentaD.GetVentasDia_cliente(NombreEquipo);
            this.dataPromedioMesas = negVentaD.GetVentasDia_promedio_mesa(NombreEquipo);
            this.dataTipopago = negVentaD.GetVentasDia_TipoPago(NombreEquipo);
            this.dataRecordMozo = negVentaD.GetVentasDia_Recordmozo(NombreEquipo);
            this.dataMesasAtendidas = negVentaD.GetMesasAtendidas(NombreEquipo);
            this.dataCajachica = negVentaD.GetCajaChica(NombreEquipo);
            this.dataVentasAnulados = negVentaD.GetVentasAnulados(NombreEquipo);
            this.dataDocElect = negVentaD.GetDocElectronico(NombreEquipo);
            this.dataDocElectAnu = negVentaD.GetDocElectronicoAnu(NombreEquipo);
            this.dataDocElectTODOS = negVentaD.GetDocElectronicoTODOS(NombreEquipo);
            this.dataFrecAtencion = negVentaD.GetFrecAtencion(NombreEquipo);
            this.dataVentasDiavsVentasPasadas = negVentaD.GetVDiavsVSanterior(NombreEquipo);
            this.dataVentasPorHora = negVentaD.GetVentasPorHora(NombreEquipo);
            this.DataUsuario = negUser.GetUsuario();
        }
        public VentasDiaViewModel()
        {
            try
            {
                this.NombreEquipo = ConfigurationManager.AppSettings["NombreEquipo"].ToString();
                this.Operacion = "Consolidado";
                this.Operacion_detPago = "PAGAR";
                if (Application.Current.Properties["OperPagoCel"] != null)
                { this.Operacion_detPago = Application.Current.Properties["OperPagoCel"].ToString(); }
                this.DataDetprod = new ObservableCollection<Detalle_Pedido>();
                this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
                this.DonaPlatxArea();
                this.DonaVentavsCosto();
                this.DonaCanaldeVenta();
                this.global = new Funcion_Global();
                DataList();
                this.pagar = new Pagar();
                this.Pagar = new Pagar();
                this.DataDetallePedido = new ObservableCollection<Detalle_Pedido>();
                Data();
                this.datosbacis();
                this.DatosBarra();
                this.ConsolidadoCommand = new RelayCommand(new Action(Consolidado));
                this.DetalladoCommand = new RelayCommand(new Action(Detallado));
                //this.botonBolFacCommand = new RelayCommand(new Action(botBolFac));
                this.ImprimirCuentaCommand = new RelayCommand(new Action(ImpCuenta));
                this.BoletaFacturaCommand = new RelayCommand(new Action(BolFac));
                this.FacturaCommand = new RelayCommand(new Action(Fac));
                this.PagarCommand = new RelayCommand(new Action(PagarPed));
                this.GuardarCommand = new RelayCommand(new Action(GuardarPed));
                this.CargarSunatCommand = new ParamCommand(new Action<object>(ServicioSunat));
                this.EliminarCommand = new ParamCommand(new Action<object>(EliminarDetPagar));
                this.OpenDialogPagarCommand = new RelayCommand(new Action(AbrirDailogPagar));
                this.OpenDialogAnularPedidoCommand = new RelayCommand(new Action(AbrirDailogAnularPedido));
                this.OpenDialogCambiarPagoCommand = new RelayCommand(new Action(AbrirDailogCambiarPago));
                this.OpenDialogReporteFechaCommand = new RelayCommand(new Action(AbrirDailogReporteFecha));
                this.CierreParcialCommand = new RelayCommand(new Action(CierreParcial));
                this.CerrarCajaCommand = new RelayCommand(new Action(Cierrecaja));
                this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
                this.ExportarExcelConsolidadoCommand = new RelayCommand(new Action(ExportarExcelConsolidado));
                this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
                this.AbrirDialogDetPropinas = new RelayCommand(new Action(DialogoPropinas));
                this.OpenDialogBolFactCommand = new RelayCommand(new Action(AbrirDialogBolFac));
                this.AbrirDialogDetPedido = new ParamCommand(new Action<object>(DialogDetPed));
                this.AbrirDialogDetPagos = new RelayCommand(new Action(DialogoPagar));
                this.AbrirDialogDetProdAnualdos = new RelayCommand(new Action(DialogoDetProdAnualdos));
                this.AbrirDialogDetVentasAnualdos = new RelayCommand(new Action(DialogoVentasAnualdos));
                this.ComboTipoDoc = negCombo.GetComboTipoDI();
                this.ComboFormaPago = negCombo.GetComboFormaPago(1);
                this.ComboTipoMoneda = negCombo.GetComboTipoMoneda();
                IsEnabledbtnguardar = false;
                IsEnabledbtnpagar = true;
                GetListaDetPago();
                FacturacionEnable = Convert.ToBoolean(negParametros.FacturacionElectronica());
                this.TipoPagoSelected = ComboFormaPago.FirstOrDefault();
                m_RowClickCommand = new DelegateCommand(() =>
                {
                    var set = this.SelectedDataFile;
                    if (set != null)
                    {
                        if (Application.Current.Properties["id_pedido"] == null)
                        {
                            Application.Current.Properties["id_pedido"] = set.id_ped;
                        }
                        this.id_pedido = set.id_ped;
                        this.Totals = Convert.ToDecimal(set.total_ped);
                        this.Usuario = set.id_usu;
                        this.Pedido = Convert.ToInt32(set.id_ped).ToString();
                        this.EstadoPago = set.nom_estado_f.ToString();
                        Application.Current.Properties["IdClienteDel"] = set.id_cliente;
                        Application.Current.Properties["ClienteLlevar"] = set.nomllevar;
                        Application.Current.Properties["id_pedido"] = set.id_ped;
                        Application.Current.Properties["Totals"] = set.total_ped;
                        Application.Current.Properties["Usuario"] = set.id_usu;
                        Application.Current.Properties["Pedido"] = set.id_ped;
                        Application.Current.Properties["EstadoPago"] = set.nom_estado_f;
                        //this.DataBolFac = negBolFac.GetBoletaFactura(this.id_pedido);
                        //ObservableCollection<BoletaFactura> DataBolFactura = new ObservableCollection<BoletaFactura>(DataBolFac);
                    }
                });

                DetPed_RowClickCommand = new DelegateDetPedCommand(() =>
                {
                    var set = this.SelectedDetPedDataFile;
                    if (set != null)
                    {
                        this.id_pedido = set.id;
                        //this.DataBolFac = negBolFac.GetBoletaFactura(this.id_pedido);

                        //ObservableCollection<BoletaFactura> DataBolFactura = new ObservableCollection<BoletaFactura>(DataBolFac);
                    }
                });

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
            }
            catch (Exception ex)
            {

            }
        }
        private void CloseDialog()
        {
            Application.Current.Properties["AnularPedido"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public class DelegateCommand : ICommand
        {
            private Action m_Action;
            public DelegateCommand(Action action)
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
        public decimal VBruta { get; set; }
        public decimal VDia { get; set; }
        private void Data()
        {
            try
            {
                #region Ventas
                this.Monto = this.datatotal.Select(w => w.monto).ToList().FirstOrDefault();
                this.MontoDolar = this.datatotal.Select(w => w.monto).ToList().LastOrDefault();
                if (this.MontoDolar > 0)
                {
                    VisibleMontoDolar = "Visible";
                }
                else
                {
                    VisibleMontoDolar = "Hidden";
                }
                this.Descuento = this.datatotal.Select(w => Convert.ToDecimal(w.desc)).ToList().FirstOrDefault();
                ObservableCollection<Detalle_Pedido> data = negDetVent.GetDetallepedido(NombreEquipo);
                this.TotalVenta = data.Where(w => Convert.ToInt32(w.id_estado_f) != 3 && Convert.ToInt32(w.id_estado_f) != 4).Sum(s => Convert.ToDecimal(s.total_ped));
                this.TotalxCobrar = data.Where(w => Convert.ToInt32(w.id_estado_f) == 2).Sum(s => Convert.ToDecimal(s.total_ped)); ;
                this.Dias = this.datatotal.Select(w => w.dias).ToList().FirstOrDefault();
                this.VDia = this.TotalVenta;
                this.VBruta = this.VDia + this.Descuento;
                #endregion
                this.Empresa = this.datacliente.Select(w => w.empresa).ToList().FirstOrDefault();
                this.Persona = this.datacliente.Select(w => w.persona).ToList().FirstOrDefault();
                this.TotalCli = this.datacliente.Select(w => w.total_cli).ToList().FirstOrDefault();
                this.totalEmpre = this.datacliente.Select(w => Convert.ToDecimal(w.mont_emp)).ToList().FirstOrDefault();
                this.totalPer = this.datacliente.Select(w => Convert.ToDecimal(w.mont_pers)).ToList().FirstOrDefault();
                #region Promedio cosumo por mesas 
                this.PM_Promedio = this.dataPromedioMesas.Select(w => w.PM_promedio).ToList().FirstOrDefault();
                this.PM_PromedioDolr = this.dataPromedioMesas.Select(w => w.PM_promedio).ToList().LastOrDefault();
                if (this.PM_PromedioDolr != "0")
                {
                    VisiblePromedioDolr = "Visible";
                }
                else
                {
                    VisiblePromedioDolr = "Hidden";
                }
                this.PM_Nventas = this.dataPromedioMesas.Select(w => Convert.ToDecimal(w.PM_nventas)).ToList().FirstOrDefault();
                this.PM_NventasDolar = this.dataPromedioMesas.Select(w => Convert.ToDecimal(w.PM_nventas)).ToList().LastOrDefault();
                this.PM_Nventas = PM_Nventas + this.PM_NventasDolar;
                #endregion
                #region Tipo de pago
                this.TP_MontoEfec = this.dataTipopago.Where(w => Convert.ToInt32(w.TP_id) == 04).Select(w => Convert.ToDecimal(w.TP_monto)).ToList().FirstOrDefault();
                this.TP_MontoTarj = this.dataTipopago.Where(w => Convert.ToInt32(w.TP_id) != 04).Sum(s => Convert.ToDecimal(s.TP_monto));
                this.TP_denoEfec = this.dataTipopago.Where(w => Convert.ToInt32(w.TP_id) == 04).Select(w => w.TP_deno).ToList().FirstOrDefault();
                this.TP_denoTarj = this.dataTipopago.Where(w => Convert.ToInt32(w.TP_id) != 17).Select(w => w.TP_deno).ToList().FirstOrDefault();
                decimal TPtotal = TP_MontoTarj + TP_MontoEfec;
                this.TP_PorcenTarj = (TP_MontoTarj > 0) ? Math.Round((TP_MontoTarj * 100 / TPtotal), 2) : 0;
                this.TP_PorcenEfec = (TP_MontoEfec > 0) ? Math.Round((TP_MontoEfec * 100 / TPtotal), 2) : 0;

                //Propina
                DataTable dtPropina = new DataTable();
                dtPropina = negVentaD.GetPropinas(NombreEquipo);
                if (dtPropina != null & dtPropina.Rows.Count > 0)
                {
                    this.TP_MontoPropina = Convert.ToDecimal(dtPropina.Rows[0]["TOTAL_PROPINA"]);
                    foreach (DataRow drP in dtPropina.Rows)
                    {
                        if (drP["DENO_PAGO"].ToString().ToUpper() == "EFECTIVO")
                        {
                            TP_MontoPropinaEfectivo = TP_MontoPropinaEfectivo + Convert.ToDecimal(drP["P_PROPINA"]);
                        }
                    }
                }
                #endregion
                #region Record de Mozo
                this.RMTotalPedido = this.dataRecordMozo.Select(w => Convert.ToInt32(w.tpedido)).ToList().FirstOrDefault();
                this.RMMozo = this.dataRecordMozo.Select(w => w.mozo).ToList().FirstOrDefault();
                this.RMProcentaje = this.dataRecordMozo.Select(w => Convert.ToDecimal(w.porcentaje)).ToList().FirstOrDefault();
                #endregion
                #region Mesas Atendidas
                this.MA_nmesas = this.dataMesasAtendidas.Select(w => Convert.ToInt32(w.MA_nmesas)).ToList().FirstOrDefault();
                this.MA_promedio = this.dataMesasAtendidas.Select(w => Convert.ToDecimal(w.MA_promedio)).ToList().FirstOrDefault();
                this.MA_totalmesas = this.dataMesasAtendidas.Select(w => Convert.ToInt32(w.MA_totalmesas)).ToList().FirstOrDefault();
                #endregion
                #region Caja Chica
                this.CC_ingreso = this.dataCajachica.Select(s => Convert.ToDecimal(s.Cc_ingreso)).ToList().FirstOrDefault();
                this.CC_egreso = this.dataCajachica.Select(s => Convert.ToDecimal(s.Cc_egreso)).ToList().FirstOrDefault();
                this.CC_total = this.dataCajachica.Select(s => Convert.ToDecimal(s.Cc_total)).ToList().FirstOrDefault();
                this.totalcaja = this.Monto + this.CC_total + TP_MontoPropina;
                this.TotalEfectivo = this.TP_MontoEfec + this.CC_total;
                #endregion
                #region Ventas Anulados
                this.Nanulados = this.dataVentasAnulados.Select(s => s.VA_anulados).ToList().FirstOrDefault();
                this.VAMonto = this.dataVentasAnulados.Select(s => s.VA_porcentaje).ToList().LastOrDefault();
                this.Panulados = this.dataVentasAnulados.Select(s => s.VA_anulados).ToList().LastOrDefault();
                this.NPedidos = this.dataVentasAnulados.Select(s => s.VA_pedidos).ToList().FirstOrDefault();
                #endregion
                #region Frecuencia de atencion
                //this.FA_minimo = (dataFrecAtencion.Count > 0) ? this.dataFrecAtencion.Select(s => s.FA_min).ToList().FirstOrDefault() : 0;
                //this.FA_maximo = (dataFrecAtencion.Count > 0) ? this.dataFrecAtencion.Select(s => s.FA_max).ToList().FirstOrDefault() : 0;

                DataVentasxCanal = new ObservableCollection<VentasDia>();
                DataTable dtVentas = new DataTable();
                dtVentas = negDetVent.GetCanaldeVentaConsolidado(NombreEquipo);
                foreach (DataRow dr in dtVentas.Rows) {
                    VentasDia v = new VentasDia();
                    v.cantidad_venta = Convert.ToInt32(dr["CANTIDAD"]);
                    v.importe_venta = Convert.ToInt32(dr["TOTAL"]);
                    v.canal_venta = dr["NOMBRE"].ToString();
                    DataVentasxCanal.Add(v);
                }


                #endregion
                #region Ventas Dia vs Ventas PAsada mismo dia
                this.VDVP_monto_P = (dataVentasDiavsVentasPasadas.Count > 0) ? this.dataVentasDiavsVentasPasadas.Select(s => s.VDVA_monto_SA).ToList().FirstOrDefault() : 0;
                this.VDVP_monto_A = (dataVentasDiavsVentasPasadas.Count > 0) ? this.dataVentasDiavsVentasPasadas.Select(s => s.VDVA_monto_AC).ToList().FirstOrDefault() : 0;
                #endregion
                #region Documento Electronico
                this.DC_MontoBol = this.dataDocElect.Where(w => w.DC_tipo == 03).Select(s => Convert.ToDecimal(s.DC_Monto)).ToList().FirstOrDefault();
                this.DC_CantDocBol = this.dataDocElect.Where(w => w.DC_tipo == 03).Select(s => Convert.ToInt32(s.DC_docelect)).ToList().FirstOrDefault();
                this.DC_MontoFac = this.dataDocElect.Where(w => w.DC_tipo == 01).Select(s => Convert.ToDecimal(s.DC_Monto)).ToList().FirstOrDefault();
                this.DC_CantDocFAc = this.dataDocElect.Where(w => w.DC_tipo == 01).Select(s => Convert.ToInt32(s.DC_docelect)).ToList().FirstOrDefault();
                this.DC_MontoBolAnuladas = this.dataDocElectAnu.Where(w => w.DC_tipo == 03).Select(s => Convert.ToDecimal(s.DC_Monto)).ToList().FirstOrDefault();
                this.DC_MontoFactAnuladas = this.dataDocElectAnu.Where(w => w.DC_tipo == 01).Select(s => Convert.ToDecimal(s.DC_Monto)).ToList().FirstOrDefault();
                this.DC_CantBolAnuladas = this.dataDocElectAnu.Where(w => w.DC_tipo == 03).Select(s => Convert.ToInt32(s.DC_docelect)).ToList().FirstOrDefault();
                this.DC_CantFactAnuladas = this.dataDocElectAnu.Where(w => w.DC_tipo == 01).Select(s => Convert.ToInt32(s.DC_docelect)).ToList().FirstOrDefault();
                this.DC_Total = DC_MontoBol + DC_MontoFac;
                this.DC_Sum_Total = Monto - DC_Total;
                #endregion
                GetLista();
                #region Ticket promedio
                this.TP_Nro_personas = (DataDetallePedido.Count > 0) ? DataDetallePedido.Sum(s => Convert.ToInt32(s.nro_personas)) : 0;
                this.TP_totalpromedio = (TP_Nro_personas > 0) ? Math.Round(Convert.ToInt32(TotalVenta) / Convert.ToDecimal(TP_Nro_personas), 2) : 0;
                #endregion
                #region Hora primer registro hasta cerrar caja
                DateTime fecha = (DataDetallePedido.Count > 0) ? this.DataDetallePedido.FirstOrDefault().f_ped : DateTime.Now;
                TimeSpan result = DateTime.Now.Subtract(fecha);
                double horas = result.TotalHours;
                this.UH_hora = Math.Round(Convert.ToDouble(horas));
                #endregion
            }catch(Exception ex)
            {
                //globales.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
        }
        private void Consolidado()
        {
            this.Operacion = "Detallado";
        }
        private void Detallado()
        {
            this.Operacion = "Consolidado";
        }
        private void GetLista()
        {
            DataDetallePedido = negDetVent.GetDetallepedido(NombreEquipo);
            //DataBolFac2 = negBolFac.GetBoletaFactura();
        }
        private void ImpCuenta()
        {
            if (this.id_pedido == 0)
            {
                globales.Mensaje("SOS-FOOD Información", "Seleccione una Fila para Imprimir Cuenta", 2);
            }
            else
            {
                DataTable dt = new DataTable();
                dt = negDetVent.GetCuentaxPedidototal(this.id_pedido);
                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
                if (dt.Rows.Count > 0)
                {
                    DataTable impresora = new DataTable();
                    string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
                    impresora = negPlatos.GetImpresoraxDocumento(1); //Precuenta
                    for (int i = 0; i < impresora.Rows.Count; i++)
                    {
                        Ticket ticket = new Ticket();
                        ticket.MaxCharDescrip = negParametros.margenMaxDescrip();
                        if (Convert.ToBoolean(negParametros.logoPreCuenta()) == true) { 
                            System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["EMPR_LOGO"]));
                            ticket.MargenLogo = negParametros.margenLogoTicket();
                            ticket.HeaderImage = x;
                        }
                        
                        ticket.AddTitleLine("");
                        //ticket.AddSubHeaderLine("NRO. Orden: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                        ticket.AddSubHeaderLineEnorme("         " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                        ticket.AddSubHeaderLine("");
                        ticket.AddSubHeaderLine("Atendido Por: " + dt.Rows[0]["EMPL_NOM"].ToString());
                        ticket.AddSubHeaderLine("Ambiente: " + dt.Rows[0]["M_NOM"].ToString());
                        ticket.AddSubHeaderLine("");
                        if (dt.Rows[0]["M_NOM"].ToString().Contains("LLEVAR") || dt.Rows[0]["M_NOM"].ToString().Contains("RECOJO"))
                        {
                            ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["nomllevar"].ToString());
                            ticket.AddSubHeaderLine("");
                            ticket.AddSubHeaderLine("Telefono : " + dt.Rows[0]["telefcli"].ToString());
                        }
                        else if (dt.Rows[0]["M_NOM"].ToString().Contains("DELIVERY"))
                        {
                            ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["C_NOM"].ToString());
                            ticket.AddSubHeaderLine("");
                            ticket.AddSubHeaderLine("Nro Documento: " + dt.Rows[0]["C_NRO_DOC"].ToString());
                        }
                        else
                        {
                            ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["nomllevar"].ToString());
                            ticket.AddSubHeaderLine("");
                            ticket.AddSubHeaderLine("Telefono : " + dt.Rows[0]["telefcli"].ToString());
                        }

                        ticket.AddSubHeaderLine("");
                        if (dt.Rows[0]["M_NOM"].ToString().Contains("DELIVERY"))
                        {
                            ticket.AddSubHeaderLine("Direccion: " + dt.Rows[0]["C_DIREC"].ToString());
                            ticket.AddSubHeaderLineTelefono("");
                            ticket.AddSubHeaderLineTelefono("Telefono: " + dt.Rows[0]["C_TEL"].ToString());
                        }
                        ticket.AddSubHeaderLine("");
                        ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
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
                        ticket.AddTotal("SUB TOTAL", dt.Rows[0]["PED_SUBTOTAL"].ToString());
                        ticket.AddTotal("DESC.", dt.Rows[0]["PED_DESCUENTO"].ToString());
                        ticket.AddTotal("", "---------");
                        ticket.AddTotal("TOTAL", dt.Rows[0]["PED_TOTAL"].ToString());
                        ticket.AddFooterLine("");
                        ticket.AddTotal("", "---------");

                        ticket.AddFooterLinePub("");
                        ticket.AddFooterLinePub("  GRACIAS POR SU VISITA");
                        ticket.AddFooterLinePub("");
                        ticket.AddFooterLinePub("       Y SU PROPINA");
                        ticket.AddFooterLine("Este documento no es comprobante de pago.");
                        ticket.AddFooterLine("");
                        ticket.AddFooterLine(globales.PublicidadPreCuenta());

                        if (ticket.PrinterExists(ImpCaja))
                        {
                            ticket.PrintTicket(ImpCaja);
                        }
                        else
                        {
                            globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                        }
                    }
                }
                else
                {
                    if (true)
                    {

                    }
                    globales.Mensaje("SOS-FOOD Información", "No hay nada que imprimir", 3);
                }
            }
        }
        private async void DialogDetPed(object id)
        {

            Application.Current.Properties["N"] = id;
            this.DataDetprod = negDetVent.GetDetProducto(Application.Current.Properties["N"].ToString());
            Application.Current.Properties["detPEdido"] = DataDetprod;
            var SiNo = new DialogDetPedido
            {

            };
            var x = await DialogHost.Show(SiNo, "RootDialog");


        }
        //public ScriptRepositoryViewModel(IUnityContainer container,
        //IScriptService scriptService, IEventAggregator eventAggregator)
        //{
        //    _container = container;
        //    _scriptService = scriptService;
        //    _eventAggregator = eventAggregator;
        //}

        //public ICollectionView Scripts
        //{
        //    get
        //    {
        //        if (_view == null)
        //        {
        //            _view = CollectionViewSource.GetDefaultView(
        //                _scriptService.Scripts);
        //            _view.Filter = Filter;
        //        }

        //        return _view;
        //    }
        //}

        #region Method Dialogs 
        //private async void botBolFac()
        //{


        //}

        private async void BolFac()
        {

            DialogHost.CloseDialogCommand.Execute(null, null);
            if (Application.Current.Properties["id_pedido"] == null)
            {

                var SiNo = new MessageDialog
                {
                    Mensaje = { Text = "Debe seleccionar un pedido" }
                };
                var x6 = await DialogHost.Show(SiNo, "RootDialog");
                return;

            }
            else
            {
                int id = Convert.ToInt32(Application.Current.Properties["id_pedido"]);
                Application.Current.Properties["IdPedidoDoc"] = id;
                Application.Current.Properties["TipDocElectronico"] = 1;
                var BoletaElectronica = new DialogFacturacionElectronica
                {

                };
                var x3 = await DialogHost.Show(BoletaElectronica, "RootDialog");

            }


        }
        private async void Fac()
        {

            DialogHost.CloseDialogCommand.Execute(null, null);
            if (Application.Current.Properties["id_pedido"] == null)
            {
                var x2 = new MessageDialog
                {
                    Mensaje = { Text = "Debe seleccionar un pedido" }
                };
                var x5 = await DialogHost.Show(x2, "RootDialog");
                return;
            }
            else
            {
                int id = Convert.ToInt32(Application.Current.Properties["id_pedido"]);
                Application.Current.Properties["IdPedidoDoc"] = id;
                Application.Current.Properties["TipDocElectronico"] = 2;
                var BoletaElectronica2 = new DialogFacturacionElectronica
                {

                };
                var x4 = await DialogHost.Show(BoletaElectronica2, "RootDialog");
            }


        }
        private async void AbrirDailogPagar()
        {
            if (this.DataDetallePedido.Count != 0)
            {
                if (this.id_pedido > 0)
                {

                    //DataTable doc_x_pedido = negPedido.getBoletaxPedido(id_pedido);
                    //if (doc_x_pedido.Rows.Count == 0)
                    //{

                    //    globales.Mensaje("SOS-FOOD Mensaje:", "Este pedido no tiene boleta o factura generado", 2);
                    //    return;
                    //}



                    this.ValorData = 0;
                    this.ValorData = this.DataDetallePedido.Where(w => Convert.ToInt32(w.id_ped) == id_pedido).Select(s => s.id_estado_f).FirstOrDefault();
                    if (Convert.ToInt32(ValorData) == 2)
                    {
                        DataTable dt = new DataTable();
                        dt = negPedido.sp_verificar_pedido_cuenta(this.id_pedido);

                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                Application.Current.Properties["PedidoCuenta"] = "SI";
                            }
                            else
                            {
                                Application.Current.Properties["PedidoCuenta"] = "NO";
                            }
                        }
                        else
                        {
                            Application.Current.Properties["PedidoCuenta"] = "NO";
                        }
                        ObservableCollection<Capa_Entidades.Models.Pedido.Pedidos> Pedido2 = new ObservableCollection<Capa_Entidades.Models.Pedido.Pedidos>();
                        Pedido2 = negPedido.GetPedidoxIdTotal(Convert.ToInt32(this.id_pedido));
                        string SubTotalPedido = "TOTAL : S/. " + Pedido2.ToList().FirstOrDefault().PED_TOTAL.ToString();
                        Application.Current.Properties["EstadoPago"] = "PAGADO";
                        Application.Current.Properties["OperPagoCel"] = "PAGAR";
                        Application.Current.Properties["id_pedido"] = Pedido2.ToList().FirstOrDefault().DP_ID_PED;
                        Application.Current.Properties["Totals"] = Pedido2.ToList().FirstOrDefault().PED_TOTAL;
                        Application.Current.Properties["Usuario"] = Application.Current.Properties["IdUsuario"];
                        Application.Current.Properties["Pedido"] = Pedido2.ToList().FirstOrDefault().DP_ID_PED;
                        Application.Current.Properties["EstadoF"] = "PENDIENTE";
                        Application.Current.Properties["NomCliente"] = Pedido2.ToList().FirstOrDefault().NOMBRE_CLIENTE;
                        //Application.Current.Properties["OperPagoCel"] = "PAGAR";
                        var openpago = new DialogPago
                        {

                        };
                        var x = await DialogHost.Show(openpago, "RootDialog");


                        this.GetLista();
                        this.Data();
                    }
                    else
                    {
                        var bolfac_emitido = this.DataDetallePedido.Where(c => Convert.ToInt32(c.id_ped) == id_pedido).Select(s => s.nom_estado_f).FirstOrDefault();
                        if (bolfac_emitido == "PAGADO")
                        {
                            globales.Mensaje("SOS-FOOD Realizado", "Pedido " + bolfac_emitido + "", 1);
                        }
                        else if (bolfac_emitido == "ANULADO")
                        {
                            globales.Mensaje("SOS-FOOD Información", "Pedido " + bolfac_emitido + "", 3);
                        }
                    }
                }
                else
                {
                    globales.Mensaje("SOS-FOOD Información", "Seleccione una fila para realizar pago", 2);
                }
                this.id_pedido = 0;
            }
        }
        private async void AbrirDailogAnularPedido()
        {
            //if (this.DataDetallePedido.Count != 0)
            //{
            //    Application.Current.Properties["OperPagoCel"] = "ANULAR PEDIDO";
            //    this.estadoCierre = this.DataDetallePedido.Count(c => Convert.ToInt32(c.id_estado_f) != 3);
            //    if (Convert.ToInt32(estadoCierre) != 0)
            //    {
            //        //Application.Current.Properties["OperPagoCel"] = "ANULAR PEDIDO";
            //        //Application.Current.Properties["RegistrarPedido"] = null;
            //        //if (content != "ANULADO" || content == "PAGADO" || content == "PENDIENTE")
            //        //{
            //        var opencampago = new DialogAnularPedido
            //        {

            //        };
            //        var x = await DialogHost.Show(opencampago, "RootDialog");

            //        this.Operacion = "Detallado";
            //        this.Data();

            //    }
            //}
            //else
            //{
            //    MessageBox.Show("no hay datos");
            //}
            if (this.DataDetallePedido.Count != 0)
            {
                if (this.id_pedido > 0)
                {
                    this.ValorData = 0;
                    this.ValorData = this.DataDetallePedido.Where(w => Convert.ToInt32(w.id_ped) == id_pedido && Convert.ToInt32(w.id_estado_f) != 3).Count();
                    if (Convert.ToInt32(ValorData) > 0)
                    {
                        Application.Current.Properties["OperPagoCel"] = "ANULAR PEDIDO";
                        var ESTADO = this.DataDetallePedido.Where(c => Convert.ToInt32(c.id_ped) == id_pedido).Select(s => s.nom_estado_f).FirstOrDefault();
                        Application.Current.Properties["EstPedidoAnulacion"] = ESTADO.ToUpper().ToString();
                        /*llhssseb*/
                        Application.Current.Properties["RegistrarPedido"] = null;
                        /*llhssseb*/
                        var openpago = new DialogAnularPedido{};
                        var x = await DialogHost.Show(openpago, "RootDialog");
                        this.GetLista();
                        this.Data();
                        Properties.Settings.Default.Reload();
                    }
                    else
                    {
                        var bolfac_emitido = this.DataDetallePedido.Where(c => Convert.ToInt32(c.id_ped) == id_pedido).Select(s => s.nom_estado_f).FirstOrDefault();
                        if (bolfac_emitido == "PAGADO")
                        {
                            globales.Mensaje("SOS-FOOD Realizado", "Pedido : " + bolfac_emitido + "", 1);
                        }
                        else if (bolfac_emitido == "ANULADO")
                        {
                            globales.Mensaje("SOS-FOOD Información", "Pedido : " + bolfac_emitido + "", 3);
                        }
                        else if (bolfac_emitido == "CAMBIO")
                        {
                            globales.Mensaje("SOS-FOOD Información", "Pedido : " + bolfac_emitido + "", 3);
                        }
                    }
                }
                else
                {
                    globales.Mensaje("SOS-FOOD Información", "Seleccione una fila para anular pedido", 2);
                }
                this.id_pedido = 0;
            }
        }
        private async void AbrirDailogCambiarPago()
        {
            Application.Current.Properties["OperPagoCel"] = "CAMBIAR PAGO";
            if (EstadoPago == "PAGADO")
            {
                var opencampago = new DialogPago {};
                var x = await DialogHost.Show(opencampago, "RootDialog");
                this.Operacion = "Detallado";
                this.Data();
            }
        }
        #endregion


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
        public ObservableCollection<Pagar> DataDetallePagar { get; set; }
        public List<Ent_Combo> ComboFormaPago { get; set; }
        public List<Ent_Combo> ComboTipoMoneda { get; set; }
        public Pagar SelectedDetPedDataFile { get; set; }
        List<Pagar> list = new List<Pagar>();
        private ICommand DetPed_RowClickCommand;
        #endregion
        public ICommand RowClickDPCommand
        {
            get
            {
                return DetPed_RowClickCommand;
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
        public string Nrotarjeta { get; set; }
        #endregion
        #region DashboardChard
        public SeriesCollection CustomerSalesData { get; set; }
        private SeriesCollection dataplatosxarea = new SeriesCollection();
        private SeriesCollection datacentavscosto = new SeriesCollection();
        private SeriesCollection datacanaldeventa = new SeriesCollection();
        public SeriesCollection dataPlatosxArea
        {
            get
            {
                return dataplatosxarea;
            }
            set
            {
                dataplatosxarea = value;
                //RaisePropertyChanged("MyPie");
            }
        }
        public SeriesCollection dataVentavsCosto
        {
            get
            {
                return datacentavscosto;
            }
            set
            {
                datacentavscosto = value;
                //RaisePropertyChanged("MyPie");
            }
        }
        public SeriesCollection dataCanaldeVenta
        {
            get
            {
                return datacanaldeventa;
            }
            set
            {
                datacanaldeventa = value;
                //RaisePropertyChanged("MyPie");
            }
        }
        public Func<ChartPoint, string> PointLabel { get; set; }
        public string[] Labels { get; set; }
        public string[] Labels1 { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public Func<double, string> Formatter { get; set; }
        public SeriesCollection dataVentasxHora { get; set; }
        public SeriesCollection dataVActualvsVPasado { get; set; }
        public void DonaPlatxArea()
        {
            DataTable dt = new DataTable();
            dt = negDetVent.GetPlatoxArea(NombreEquipo);
            foreach (DataRow dr in dt.Rows)
            {
                Func<ChartPoint, string> labelPoint = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                PieSeries ps = new PieSeries
                {
                    Title = dr["NOMIMP"].ToString(),
                    Values = new ChartValues<double> {
                        double.Parse(dr["PED_TOTAL"].ToString()) },
                    DataLabels = true,
                    ToolTip = true,
                    FontSize = 10,
                    Foreground = System.Windows.Media.Brushes.Black,
                    LabelPoint = labelPoint,
                };
                dataplatosxarea.Add(ps);
            }
        }
        public void DonaVentavsCosto()
        {
            DataTable dt = new DataTable();
            dt = negDetVent.GetVentavsCosto(NombreEquipo);
            foreach (DataRow dr in dt.Rows)
            {
                Func<ChartPoint, string> labelPoint = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                PieSeries ps = new PieSeries
                {
                    Title = dr["Descripcion"].ToString(),
                    Values = new ChartValues<double> {
                        double.Parse(dr["Valor"].ToString())},
                    DataLabels = true,
                    ToolTip = true,
                    FontSize = 10,
                    Foreground = System.Windows.Media.Brushes.Black,
                    LabelPoint = labelPoint

                };
                datacentavscosto.Add(ps);
            }
        }
        public void DonaCanaldeVenta()
        {
            DataTable dt = new DataTable();
            dt = negDetVent.GetCanaldeVenta(NombreEquipo);
            foreach (DataRow dr in dt.Rows)
            {
                Func<ChartPoint, string> labelPoint = chartPoint =>
                string.Format("{0:n0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                PieSeries ps = new PieSeries
                {
                    Title = dr["AMBIENTE"].ToString(),
                    Values = new ChartValues<double> {
                        double.Parse(dr["MONTO"].ToString()) },
                    DataLabels = true,
                    ToolTip = true,
                    FontSize = 10,
                    Foreground = System.Windows.Media.Brushes.Black,
                    LabelPoint = labelPoint
                };
                datacanaldeventa.Add(ps);
            }
        }

        public void datosbacis()
        {
            string[] horas = dataVentasPorHora.Select(s => s.VPH_Hora.ToString()).ToArray();
            decimal[] data = dataVentasPorHora.Select(s => Convert.ToDecimal(s.VPH_Monto)).ToArray();
            dataVentasxHora = new SeriesCollection
            {new LineSeries { Values = new ChartValues<decimal>(data),Title="Ventas" }};
            Labels1 = horas;
            Formatter = value => value.ToString("C");
        }
        public void DatosBarra()
        {
            dataVActualvsVPasado = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Venta Pasada",
                    Values = new ChartValues<double> {Convert.ToDouble(VDVP_monto_P), Convert.ToDouble(VDVP_monto_A) }
                },
                
                //new ColumnSeries
                //{
                //    Title = "Venta Actual",
                //    Values = new ChartValues<double> {Convert.ToDouble(VDVP_monto_A) }
                //}
            };
            dataVActualvsVPasado = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Ventas",
                    Values = new ChartValues<double> { Convert.ToDouble(VDVP_monto_P), Convert.ToDouble(VDVP_monto_A)},
                    
                },

            };

            Labels = new[] { "DIA SEMANA PASADA", "HOY" };
            Formatter = value => value.ToString("C");

        }
        #endregion



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
        public bool enabled { get; set; }
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
                  
                    bool result = negPagar.SetPagar((_id == 0 ? 1 : 2), pagar, ref _mensaje1,NombreEquipo, dt);
                    if (result)
                    {
                        string id_pedido = this.Pedido;
                        DataTable dt2 = negPedido.GetDeliveryxPedidoApp(Convert.ToInt32(id_pedido));
                        if (dt2 != null)
                        {
                            if (dt2.Rows.Count > 0)
                            {
                                empresa = negEmpresa.GetEmpresa();
                                int Desc;
                                string verificar = InternetGetConnectedState(out Desc, 0).ToString();
                                if ((Convert.ToBoolean(verificar) == true))
                                {
                                    using (var client = new WebClient())
                                    {
                                        var values = new NameValueCollection();
                                        //values["token"] = "app963";
                                        values["orderid"] = dt2.Rows[0]["id"].ToString();
                                        values["id_business"] = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().codigo;
                                        values["status"] = "PEDIDO CONCLUIDO";
                                        var response = client.UploadValues(negParametros.UpdateState(), values);
                                        var responseString = Encoding.Default.GetString(response);
                                        negDeliveryWebService.sp_cambiar_estado_pedido_delivery(Convert.ToInt32(dt2.Rows[0]["id"]), "PEDIDO CONCLUIDO");
                                    }
                                }
                                  
                                
                            }
                        }
                        
                        
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        Application.Current.Properties["EstPago"] = "PAGADO";

                    }
                }
                else
                {
                    global.MensajeSnack("Tiene un saldo por cancelar");
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
            bool result = negPagar.SetPagar((_id == 0 ? 1 : 2), pagar, ref _mensaje, NombreEquipo,dt);
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
                    var s = DataDetallePagar.FirstOrDefault().saldos;
                    var a = DataDetallePagar.FirstOrDefault().amortizars;
                    var k = DataDetallePagar.FirstOrDefault().saldos;
                    this.Saldos = s;
                    this.Amortizars = a;
                    this.Amortizar = k;
                }
            }
            else
            {
                int idpedido2 = -1;
                DataDetallePagar = negPagar.GetDetallePago(idpedido2);
            }
        }
        #region BotonConsultaSunat
        private async void ServicioSunat(object numdoc)
        {
            //string nombrecompleto;
            ServiceReference1.sosfoodSoapClient vf = new ServiceReference1.sosfoodSoapClient();

            try
            {
                string nombrecompleto;
                if (!string.IsNullOrWhiteSpace(numdoc.ToString()))
                {
                    //if (cliente.idtd == "1")
                    //{
                    //    nombrecompleto = vf.ConsultaDNI(numdoc.ToString());

                    //    string[] partes = nombrecompleto.Split(' ');
                    //    string result = partes[0] + ' ' + partes[1];
                    //    string result1 = partes[2] + ' ' + partes[3];
                    //    this.Apellido = result;
                    //    this.Nombre = result1;
                    //}
                    //else if (cliente.idtd == "2")
                    //{
                    //    ServiceReference1.ClsClienteConsultaEN cn;
                    //    cn = vf.ConsultaRuc("0a682fbe-009d-4758-aad1-2ff1092ab7c2-838d6cd5-620a-4add-9632-a3b37c5ae216", numdoc.ToString());
                    //    this.Nombre = cn.nombre_o_razon_social;
                    //    this.Direccion = cn.direccion_completa;
                    //}
                }
                else return;
            }
            catch (Exception ex)
            {
                var SiNo = new SiNoMessageDialog
                {
                    Mensaje = { Text = "Problemas al cargar: " + ex.Message.ToString() + "" }
                };
                var x = await DialogHost.Show(SiNo, "RootDialog");
                if (!(bool)x)
                    return;
            }
        }
        #endregion
        #region CierreParcial
        Neg_CierreParcial negCierre = new Neg_CierreParcial();

        public ICommand CierreParcialCommand { get; set; }

        private async void CierreParcial()
        {
            if (DataDetallePedido != null)
            {
                if (this.DataDetallePedido.Count > 0)
                {
                    //ImprimirTicketsCierreParcial();//tarwi jm
                    ImprimirTickets();

                }
                else
                {
                    var SiNo = new SiNoMessageDialog
                    {
                        Mensaje = { Text = "No hay nada datos" }
                    };
                    var x = await DialogHost.Show(SiNo, "RootDialog");
                    if (!(bool)x)
                        return;
                }

            }
        }
        public ObservableCollection<Empresa> empresa { get; set; }
        public ObservableCollection<Empresa> correo { get; set; }
        public ObservableCollection<EnvioCierre> VentaDia { get; set; }
        private void ImprimirTickets()
        {
            string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
            #region REPORTE PLATOS X COMENTARIO Y ANULADOS
            /*****************************************************REPORTE PLATOS X COMENTARIO Y ANULADOS***************************************/
            DataTable dt = new DataTable();
            dt = negCierre.GetAplatosXComentario(NombreEquipo);
            if (dt.Rows.Count > 0)
            {
                string comentario_raw = "";
                string[] comentario;
                char[] spearator = { '-' };
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        comentario_raw += dt.Rows[i]["COMENTARIO"].ToString();
                    }
                    else
                    {
                        comentario_raw += "-" + dt.Rows[i]["COMENTARIO"].ToString();
                    }

                }
                comentario = comentario_raw.Split(spearator).ToArray();
                comentario = comentario.Distinct().ToArray();
                for (int p = 0; p < comentario.Distinct().Count(); p++)
                {
                    Ticket ticket = new Ticket();
                    ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    ticket.AddHeaderLine_2("PRODUCTOS ANULADOS");
                    ticket.AddHeaderLine("");

                    ticket.AddSubHeaderLine(comentario[p].ToString());
                    ticket.AddSubHeaderLine("");
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["COMENTARIO"].ToString() == comentario[p].ToString())
                        {
                            string cantidad = (dt.Rows.Count > 0) ? dt.Rows[j]["CANTIDAD"].ToString() : "0.00";
                            string descripcion = (dt.Rows.Count > 0) ? dt.Rows[j]["DESCRIPCION"].ToString() : "0.00";
                            ticket.AddItemSinRecorte("[" + cantidad + "]" + descripcion, "", (dt.Rows.Count > 0) ? dt.Rows[j]["IMPORTE"].ToString() : "0.00");

                        }
                    }
                    ticket.AddFooterLine("");

                    if (ticket.PrinterExists(ImpCaja))
                    {
                        ticket.PrintTicket(ImpCaja);
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                    }
                }

            }
            /*****************************************************FIN **********************************************************/
            #endregion
            #region REPORTE VENTA POR DIAS
            /******************REPORTE VENTA POR DIAS **************/
            DataTable dttipag = new DataTable();
            dttipag = negCierre.GetTipoPago(NombreEquipo);
            string denotg_row = "";
            string[] denotg;
            char[] spearatordeno = { '-' };
            for (int i = 0; i < dttipag.Rows.Count; i++)
            {
                if (i == 0)
                {
                    denotg_row += dttipag.Rows[i]["DENO_PAGO"].ToString();
                }
                else
                {
                    denotg_row += "-" + dttipag.Rows[i]["DENO_PAGO"].ToString();
                }
            }
            denotg = denotg_row.Split(spearatordeno).ToArray();
            denotg = denotg.Distinct().ToArray();

            DataTable dt2 = new DataTable();
            dt2 = negCierre.GetreporteVetasDia(NombreEquipo);
            Ticket ticket2 = new Ticket();

            ticket2.AddHeaderLine_2("REPORTE DE VENTA DEL DIA");
            ticket2.AddSubHeaderLine("");
            ticket2.AddSubHeaderLine("FECHA INICIO:" + dt2.Rows[0]["FECHAINICIO"].ToString());
            ticket2.AddSubHeaderLine("FECHA FIN:" + dt2.Rows[0]["FECHAFIN"].ToString());
            //ticket2.AddSubHeaderLine("FECHA FIN:" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            ticket2.AddSubHeaderLine("");


            int nroPersonas = (dt2.Rows.Count > 0) ? Convert.ToInt32(dt2.Rows[0]["TOTAL_PERSONAS"]) : 0;
            ticket2.AddReporteItem("Mesas Atendidas", "ESTADISTICAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_MESAS"].ToString() : "0");
            ticket2.AddReporteItem("Personas", "ESTADISTICAS", nroPersonas.ToString());

            ticket2.AddReporteItem("Promedio Mesas Soles", "ESTADISTICAS", "S/ " + PM_Promedio.ToString());
            if (PM_PromedioDolr != "")
            {
                if (Convert.ToDecimal(PM_PromedioDolr) != 0)
                {
                    ticket2.AddReporteItem("Promedio Mesas Dolares", "ESTADISTICAS", "$ " + PM_PromedioDolr.ToString());
                }
            }
            ticket2.AddReporteItem("Promedio N° Personas", "ESTADISTICAS", (PM_Nventas == 0 || nroPersonas == 0) ? "0" : Math.Round((PM_Nventas / nroPersonas), 2).ToString("N2"));
            ticket2.AddReporteItem("", "ESTADISTICAS", "");
            ticket2.AddReporteItem("Monto Delivery Soles", "ESTADISTICAS", "S/ " + ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_DELIVERY_IMPORTE"].ToString() : "S/ 0.00").ToString().ToString());
            ticket2.AddReporteItem("Monto Recojo Soles", "ESTADISTICAS", "S/ " + ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_RECOJO_IMPORTE"].ToString() : "S/ 0.00").ToString().ToString());
            if(dt2.Rows.Count > 0){
                if (dt2.Rows[0]["TOTAL_DELIVERY_IMPORTE_DOLAR"].ToString() != "")
                {
                    if (Convert.ToDecimal(dt2.Rows[0]["TOTAL_DELIVERY_IMPORTE_DOLAR"].ToString()) != 0)
                    {
                        ticket2.AddReporteItem("Monto Delivery Dolares", "ESTADISTICAS", "$ " + ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_DELIVERY_IMPORTE_DOLAR"].ToString() : "$ 0.00").ToString().ToString());
                    }
                }
                if (dt2.Rows[0]["TOTAL_RECOJO_IMPORTE_DOLAR"].ToString() != "")
                {
                    if (Convert.ToDecimal(dt2.Rows[0]["TOTAL_RECOJO_IMPORTE_DOLAR"].ToString()) != 0)
                    {
                        ticket2.AddReporteItem("Monto Recojo Dolares", "ESTADISTICAS", "$ " + ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_RECOJO_IMPORTE_DOLAR"].ToString() : "$ 0.00").ToString().ToString());
                    }
                }
            }
            ticket2.AddReporteItem("Cantidad de Delivery", "ESTADISTICAS", ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_DELIVERY"].ToString() : "0").ToString());
            ticket2.AddReporteItem("Cantidad de Recojo", "ESTADISTICAS", ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_RECOJO"].ToString() : "0").ToString());

            Decimal saldo = Convert.ToDecimal((dt2.Rows.Count > 0) ? dt2.Rows[0]["SALDO_CAJA"].ToString() : "S/ 0.00");

            ticket2.AddReporteItemventas("CAJA CHICA", "", "");
            ticket2.AddReporteItemventas("Ingresos", "CAJA CHICA", (dt2.Rows.Count > 0) ? "S/ " + dt2.Rows[0]["INGRESO_CAJA"].ToString() : "S/ 0.00");
            ticket2.AddReporteItemventas("Egresos", "CAJA CHICA", (dt2.Rows.Count > 0) ? "S/ " + dt2.Rows[0]["EGRESO_CAJA"].ToString() : "S/ 0.00");
            ticket2.AddReporteItemventas("Saldo", "CAJA CHICA", (dt2.Rows.Count > 0) ? "S/ " + dt2.Rows[0]["SALDO_CAJA"].ToString() : "S/ 0.00");

            //ticket2.AddReporteItemventas("TOTAL CAJA : ", "CAJA CHICA", (dt2.Rows.Count > 0) ? dt2.Rows[0]["CAJA"].ToString() : "0.00");

            ticket2.AddReporteItemventas("----------------------------------", "", "");

            ticket2.AddReporteItemventas("COBRADO SOLES", "VENTAS", (dt2.Rows.Count > 0) ? "S/ " + dt2.Rows[0]["TOTAL"].ToString() : "S/ 0.00");
            if (dt2.Rows.Count > 0)
            {
                if (dt2.Rows[0]["TOTAL_DOLAR"].ToString() != "")
                {
                    if (Convert.ToDecimal(dt2.Rows[0]["TOTAL_DOLAR"].ToString()) != 0)
                    {
                        string tcambio = negParametros.GetTipoCambio();
                        decimal cambio = Convert.ToDecimal(tcambio);
                        ticket2.AddReporteItemventas("COBRADO DOLARES", "VENTAS", (dt2.Rows.Count > 0) ? "$ " + dt2.Rows[0]["TOTAL_DOLAR"].ToString() : "$ 0.00");
                        ticket2.AddReporteItemventas(" --> T.C. = " + tcambio.ToString(), "VENTAS", (dt2.Rows.Count > 0) ? "S/ " + Math.Round((Convert.ToDecimal(dt2.Rows[0]["TOTAL_DOLAR"]) * cambio), 1).ToString("N2") : "S/ 0.00");
                    }
                }
            }
            ticket2.AddReporteItemventas("X COBRAR", "VENTAS", (dt2.Rows.Count > 0) ? "S/ " + dt2.Rows[0]["POR_PAGAR"].ToString() : "S/ 0.00");
            double totalventas = 0;
            totalventas = Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL"] : 0.00) + Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["POR_PAGAR"] : 0.00) + Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_DOLAR_IMP"] : 0.00);
            ticket2.AddReporteItemventas("TOTAL", "VENTAS", "S/ " + totalventas.ToString("N2"));
            ticket2.AddReporteItemventas("", "VENTAS", "");

            ticket2.AddReporteItemventas("Vendido", "VENTAS", (dt2.Rows.Count > 0) ? "S/ " + dt2.Rows[0]["SUBTOTAL"].ToString() : "S/ 0.00");
            ticket2.AddReporteItemventas("Descuento", "VENTAS", (dt2.Rows.Count > 0) ? "S/ " + dt2.Rows[0]["DESCUENTO"].ToString() : "S/ 0.00");
            double total = 0;
            total = Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["SUBTOTAL"] : 0.00) - Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["DESCUENTO"] : 0.00);
            ticket2.AddReporteItemventas("Importe Total de la Venta ", "VENTAS", "S/ " + total.ToString("N2"));

            decimal montoefe = 0;
            decimal montodol = 0;
            bool pago = false;
            for (int p1 = 0; p1 < denotg.Distinct().Count(); p1++)
            {
                if (denotg[p1].ToString().Trim() != "")
                {
                    if (dttipag.Rows[p1]["DENO_PAGO"].ToString() == "EFECTIVO")
                    {
                        montoefe = Convert.ToDecimal((dttipag.Rows.Count > 0) ? dttipag.Rows[p1]["PED_TOTAL"].ToString() : "0.00");
                    }
                    if (dttipag.Rows[p1]["DENO_PAGO"].ToString() == "DOLARES")
                    {
                        montodol = Convert.ToDecimal((dttipag.Rows.Count > 0) ? dttipag.Rows[p1]["PED_TOTAL"].ToString() : "0.00");
                    }
                    if (dttipag.Rows[p1]["DENO_PAGO"].ToString() != "DOLARES")
                    {
                        string nomcat = denotg[p1].ToString();
                        ticket2.AddDocElectronicoAnuladosTotal(dttipag.Rows[p1]["DENO_PAGO"].ToString(), "TIPO PAGO", (dttipag.Rows.Count > 0) ? dttipag.Rows[p1]["PED_TOTAL"].ToString() : "0.00");
                        pago = true;
                    }       
                }
            }

            if (pago == true)
            {
                ticket2.AddFooterLine("");

                //ticket2.AddDocElectronicoAnuladosTotal("INGRESO CAJA CHICA", "", (dt2.Rows.Count > 0) ? dt2.Rows[0]["INGRESO_CAJA"].ToString() : "0.00");
                //Double total,totalventa,totcaja;
                //totcaja = Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["INGRESO_CAJA"].ToString() : "0.00");
                //total = Convert.ToDouble(dttipag.Rows[0]["MONTOTOTAL"].ToString());
                //total = totcaja + total;
                ticket2.AddDocElectronicoAnuladosTotal("TOTAL:", "", "S/ " + dttipag.Rows[0]["MONTOTOTAL"].ToString());
            }
            Decimal totalcaja = 0;
            totalcaja = montoefe + saldo;
            //string tcambio = negParametros.GetTipoCambio();
            //decimal cambio = Convert.ToDecimal(tcambio);
            //montodol = Convert.ToDecimal(montodol * cambio);
            //ticket2.AddReporteItemventas("----------------------------------", "", "");
            ticket2.AddDocElectronicoAnuladosTotal("", "", "");
            ticket2.AddDocElectronicoAnuladosTotal("TOTAL DOLAR : ", "CAJA","$ " + montodol.ToString("N2"));
            ticket2.AddDocElectronicoAnuladosTotal("EFECTIVO : ", "CAJA", "S/ " + montoefe.ToString());
            ticket2.AddDocElectronicoAnuladosTotal("CAJA CHICA: ", "CAJA", "S/ " + saldo.ToString());
            ticket2.AddDocElectronicoAnuladosTotal("TOTAL : ", "CAJA", "S/ " + totalcaja.ToString());
            ticket2.AddFooterLine("");


            //Propina
            DataTable dttipagp = new DataTable();
            dttipagp = negVentaD.GetPropinasPago(NombreEquipo);
            if (dttipagp.Rows.Count > 0) {
                ticket2.AddDocElectronicoAnuladosTotal("", "", "");
                ticket2.AddDocElectronicoAnuladosTotal("PROPINAS", "", "");
                ticket2.AddDocElectronicoAnuladosTotal("---------", "", "");
                string denotg_rowp = "";
                string[] denotgp;
                char[] spearatordenop = { '-' };
                for (int i = 0; i < dttipagp.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        denotg_rowp += dttipagp.Rows[i]["DENO_PAGO"].ToString();
                    }
                    else
                    {
                        denotg_rowp += "-" + dttipagp.Rows[i]["DENO_PAGO"].ToString();
                    }
                }
                denotgp = denotg_rowp.Split(spearatordenop).ToArray();
                denotgp = denotgp.Distinct().ToArray();
                decimal montoefepropina = 0;
                decimal montodolpropina = 0;
                bool pagop = false;
                for (int p1 = 0; p1 < denotgp.Distinct().Count(); p1++)
                {
                    if (denotgp[p1].ToString().Trim() != "")
                    {
                        if (dttipagp.Rows[p1]["DENO_PAGO"].ToString() == "EFECTIVO")
                        {
                            montoefepropina = Convert.ToDecimal((dttipagp.Rows.Count > 0) ? dttipagp.Rows[p1]["PED_TOTAL"].ToString() : "0.00");
                        }
                        if (dttipagp.Rows[p1]["DENO_PAGO"].ToString() == "DOLARES")
                        {
                            montodolpropina = Convert.ToDecimal((dttipagp.Rows.Count > 0) ? dttipagp.Rows[p1]["PED_TOTAL"].ToString() : "0.00");
                        }
                        string nomcat = denotgp[p1].ToString();
                        ticket2.AddDocElectronicoAnuladosTotal(dttipagp.Rows[p1]["DENO_PAGO"].ToString(), "TIPO PAGO", (dttipagp.Rows.Count > 0) ? dttipagp.Rows[p1]["PED_TOTAL"].ToString() : "0.00");
                        pagop = true;
                    }
                }
                if (pagop == true)
                {
                    ticket2.AddFooterLine("");
                    ticket2.AddDocElectronicoAnuladosTotal("TOTAL:", "", "S/ " + dttipagp.Rows[0]["MONTOTOTAL"].ToString());
                }
            }
            

            if (ticket2.PrinterExists(ImpCaja))
            {
                ticket2.PrintTicket(ImpCaja);
            }
            else
            {
                globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
            }
            /******************FIN *********************/
            #endregion
            #region DOCUMENTO ELECTRONICO
            /*****************************************************DOCUMENTO ELECTRONICO***************************************/


            DataTable dt3 = new DataTable();
            dt3 = negCierre.GetreporteDocumentoElect(NombreEquipo);
            Ticket ticket3 = new Ticket();
            ticket3.AddTitleLine("REPORTE DE DOCUMENTOS");
            ticket3.AddTitleLine("ELECTRONICO");
            ticket3.AddSubHeaderLine("");

            ticket3.AddDocElectronico((dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALBOLETA"].ToString() : "0", "Boletas", (dt3.Rows.Count > 0) ? dt3.Rows[0]["MONTOBOL"].ToString() : "0.00");
            ticket3.AddDocElectronico((dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALFACTURA"].ToString() : "0", "Facturas", (dt3.Rows.Count > 0) ? dt3.Rows[0]["MONTOFACT"].ToString() : "0.00");
            ticket3.AddDocElectronicoTotal("TOTAL B/F", "", (dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALBF"].ToString() : "0.00");
            ticket3.AddDocElectronicoTotal("TOTAL S/B", "", (dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALSB"].ToString() : "0.00");

            ticket3.AddTitleLine2("REPORTE DE DOCUMENTOS");
            ticket3.AddTitleLine2("ELECTRONICOS ANULADOS");

            ticket3.AddDocElectronicoAnulados((dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALBOLANULADO"].ToString() : "0", "Boletas", (dt3.Rows.Count > 0) ? dt3.Rows[0]["MONTOBOLANULADO"].ToString() : "0.00");
            ticket3.AddDocElectronicoAnulados((dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALFACANULADO"].ToString() : "0", "Facturas", (dt3.Rows.Count > 0) ? dt3.Rows[0]["MONTOFACANULADO"].ToString() : "0.00");
            ticket3.AddDocElectronicoAnuladosTotal("TOTAL B/F", "", (dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALMONTOANULADO"].ToString() : "0.00");
            ticket3.AddFooterLine("");
            if (ticket3.PrinterExists(ImpCaja))
            {
                ticket3.PrintTicket(ImpCaja);
            }
            else
            {
                globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
            }

            /*****************************************************FIN **********************************************************/
            #endregion
            #region PLATOS VENDIDOS POR DIA
            /***************************************PLATOS VENDIDOS POR DIA**********************************************************/

            DataTable dtscat = new DataTable();
            dtscat = negCierre.Getsupercat();
            string nomscat_row = "";
            string[] nomscat;
            char[] spearatorScat = { '-' };
            for (int i = 0; i < dtscat.Rows.Count; i++)
            {
                if (i == 0)
                {
                    nomscat_row += dtscat.Rows[i]["SCAT_NOM"].ToString();
                }
                else
                {
                    nomscat_row += "-" + dtscat.Rows[i]["SCAT_NOM"].ToString();
                }
            }
            nomscat = nomscat_row.Split(spearatorScat).ToArray();
            nomscat = nomscat.Distinct().ToArray();
            for (int p1 = 0; p1 < nomscat.Distinct().Count(); p1++)
            {
                int pagina = 2;
                string nomcat = nomscat[p1].ToString();
                DataTable dtVdia = new DataTable();
                dtVdia = negCierre.GetreportetotalPedidosDia(nomscat[p1].ToString(), NombreEquipo);
                int c = Convert.ToInt32(Math.Round(Convert.ToDouble(dtVdia.Rows.Count / 35),0)) + 1;
                if (dtVdia.Rows.Count > 0)
                {
                    Ticket ticketPlatos = new Ticket();
                    ticketPlatos.AddSubHeaderLine("Fecha/Hora: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    ticketPlatos.AddHeaderLine_2("PLATOS VENDIDOS DEL DIA");
                    ticketPlatos.AddSubHeaderLine("");
                    ticketPlatos.AddSubHeaderLine(nomscat[p1].ToString() + "                   " + "1/"+ c);
                    ticketPlatos.AddSubHeaderLine("");
                    for (int j = 0; j < dtVdia.Rows.Count; j++)
                    {
                        if (dtVdia.Rows[j]["SUPERCAT"].ToString() == nomscat[p1].ToString())
                        {
                            string cantidad = (dtVdia.Rows.Count > 0) ? dtVdia.Rows[j]["CANTIDAD"].ToString() : "0.00";
                            string descripcion = (dtVdia.Rows.Count > 0) ? dtVdia.Rows[j]["DESCRIPCION"].ToString() : "0.00";
                            ticketPlatos.AddItemSinRecorte(cantidad + " " + descripcion, "", (dtVdia.Rows.Count > 0) ? dtVdia.Rows[j]["MONTO"].ToString() : "0.00");
                        }
                        if (j == 35 || j == 70 || j == 105 || j == 140 || j == 175 || j == 205)
                        {
                            if (ticketPlatos.PrinterExists(ImpCaja))
                            {
                                ticketPlatos.PrintTicket(ImpCaja);
                                ticketPlatos = new Ticket();
                                ticketPlatos.AddSubHeaderLine("Fecha/Hora: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                                ticketPlatos.AddSubHeaderLine("");
                                ticketPlatos.AddSubHeaderLine(nomscat[p1].ToString() + "                   " + pagina+"/" + c);
                                pagina = pagina + 1;

                            }
                            else
                            {
                                globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                                ticketPlatos = new Ticket();
                                
                            }
                        }
                    }
                    ticketPlatos.AddDocElectronicoAnuladosTotal("TOTAL", "", (dtVdia.Rows.Count > 0) ? dtVdia.Rows[0]["TOTAL"].ToString() : "0.00");
                    ticketPlatos.AddFooterLine("");
                    if (ticketPlatos.PrinterExists(ImpCaja))
                    {
                        ticketPlatos.PrintTicket(ImpCaja);
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                    }
                }

            }

            /*****************************************************FIN **********************************************************/
            #endregion
            #region REPORTE CAJA
            /**********************************************************REPORTE CAJA****************************************/
            DataTable caja = new DataTable();
            caja = negCierre.GetReporteCajaDia();
            if (caja.Rows.Count > 0)
            {
                string tipo_raw = "";
                string tipoMOVIMIENTO = "";
                string[] tipo;
                string[] tipo2;
                char[] separador = { '-' };
                char[] separador2 = { '-' };
                for (int i = 0; i < caja.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        tipo_raw += caja.Rows[i]["CC_ID_TIPO"].ToString();
                    }
                    else
                    {
                        tipo_raw += "-" + caja.Rows[i]["CC_ID_TIPO"].ToString();
                    }
                }
                tipo = tipo_raw.Split(separador).ToArray();
                tipo = tipo.Distinct().ToArray();


                for (int i = 0; i < caja.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        tipoMOVIMIENTO += caja.Rows[i]["TIPO"].ToString() + caja.Rows[i]["CC_ID_TIPO"].ToString();
                    }
                    else
                    {
                        tipoMOVIMIENTO += "-" + caja.Rows[i]["TIPO"].ToString() + caja.Rows[i]["CC_ID_TIPO"].ToString();
                    }
                }
                tipo2 = tipoMOVIMIENTO.Split(separador2).ToArray();
                tipo2 = tipo2.Distinct().ToArray();

                Ticket ticketcaja = new Ticket();

                ticketcaja.AddHeaderLine_2("REPORTE CAJA");
                ticketcaja.AddHeaderLine("");
                ticketcaja.AddSubHeaderLine("         " + DateTime.Now.ToShortDateString());
                ticketcaja.AddSubHeaderLine("");
                for (int p = 0; p < tipo.Distinct().Count(); p++)
                {

                    ticketcaja.AddSubHeaderLine((tipo[p].ToString() == "1") ? "INGRESO : " : "EGRESO :");
                    ticketcaja.AddSubHeaderLine("");
                    for (int k = 0; k < tipo2.Distinct().Count(); k++)
                    {
                        if (tipo2[k].ToString().Substring(tipo2[k].Length - 1, 1) == tipo[p].ToString())
                        {
                            ticketcaja.AddSubHeaderLine(tipo2[k].ToString().Substring(0, tipo2[k].Length - 1));
                            string dotted = "";
                            for (int x = 0; x < Convert.ToInt32(negParametros.MaxCharDescriptionTicket()); x++)
                            {
                                dotted += "-";
                            }
                            ticketcaja.AddSubHeaderLine(dotted);
                        }

                        //ticketcaja.AddSubHeaderLine("");
                        for (int j = 0; j < caja.Rows.Count; j++)
                        {
                            if (caja.Rows[j]["TIPO"].ToString() == tipo2[k].ToString().Substring(0, tipo2[k].Length - 1) && caja.Rows[j]["CC_ID_TIPO"].ToString() == (tipo[p].ToString()))
                            {
                                //ticketcaja.AddReporteItemventas(caja.Rows[j]["DESCRIPCION"].ToString(), "", (caja.Rows.Count > 0) ? caja.Rows[j]["MONTO"].ToString() : "0.00");
                                ticketcaja.AddSubHeaderLine(caja.Rows[j]["DESCRIPCION"].ToString() + " : " + Convert.ToString((caja.Rows.Count > 0) ? caja.Rows[j]["MONTO"].ToString() : "0.00"));
                            }
                        }
                        ticketcaja.AddSubHeaderLine("");
                    }
                    ticketcaja.AddSubHeaderLine("");
                }
                //ticketcaja.AddDocElectronicoAnuladosTotal("TOTAL MONTO", "", (caja.Rows.Count > 0) ? "S/" + caja.Rows[0]["MONTO"].ToString() : "0.00");
                ticketcaja.AddDocElectronicoAnuladosTotal("INGRESO CAJA", "", (caja.Rows.Count > 0) ? "S/" + caja.Rows[0]["INGRESO_CAJA"].ToString() : "0.00");
                ticketcaja.AddDocElectronicoAnuladosTotal("EGRESO CAJA", "", (caja.Rows.Count > 0) ? "S/" + caja.Rows[0]["EGRESO_CAJA"].ToString() : "0.00");
                ticketcaja.AddDocElectronicoAnuladosTotal("TOTAL SALDO", "", (caja.Rows.Count > 0) ? "S/" + caja.Rows[0]["SALDO_CAJA"].ToString() : "0.00");
                ticketcaja.AddFooterLine("");

                if (ticketcaja.PrinterExists(ImpCaja))
                {
                    ticketcaja.PrintTicket(ImpCaja);
                }
                else
                {
                    globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                }
            }
            /*****************************************************FIN **********************************************************/
            #endregion
            //}
        }
        private void ImprimirTicketsCierreParcial()
        {
            #region REPORTE VENTA POR DIAS
            /******************REPORTE VENTA POR DIAS **************/
            DataTable dttipag = new DataTable();
            dttipag = negCierre.GetTipoPago(NombreEquipo);
            string denotg_row = "";
            string[] denotg;
            char[] spearatordeno = { '-' };
            for (int i = 0; i < dttipag.Rows.Count; i++)
            {
                if (i == 0)
                {
                    denotg_row += dttipag.Rows[i]["DENO_PAGO"].ToString();
                }
                else
                {
                    denotg_row += "-" + dttipag.Rows[i]["DENO_PAGO"].ToString();
                }
            }
            denotg = denotg_row.Split(spearatordeno).ToArray();
            denotg = denotg.Distinct().ToArray();

            DataTable dt2 = new DataTable();
            dt2 = negCierre.GetreporteVetasDia(NombreEquipo);
            Ticket ticket2 = new Ticket();

            ticket2.AddHeaderLine_2("REPORTE DE VENTA DEL DIA");
            ticket2.AddSubHeaderLine("");
            ticket2.AddSubHeaderLine("FECHA INICIO:" + dt2.Rows[0]["FECHAINICIO"].ToString());
            ticket2.AddSubHeaderLine("FECHA FIN:" + dt2.Rows[0]["FECHAFIN"].ToString());
            ticket2.AddSubHeaderLine("");


            int nroPersonas = (dt2.Rows.Count > 0) ? Convert.ToInt32(dt2.Rows[0]["TOTAL_PERSONAS"]) : 0;
            //ticket2.AddReporteItem("Total Mesas", "ESTADISTICAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_MESAS"].ToString() : "0.00");
            //ticket2.AddReporteItem("Total Personas", "ESTADISTICAS", nroPersonas.ToString());

            //ticket2.AddReporteItem("Promedio Mesa", "ESTADISTICAS", "S/. " + PM_Promedio.ToString());
            //ticket2.AddReporteItem("Promedio Personas", "ESTADISTICAS", (PM_Nventas == 0 || nroPersonas == 0) ? "0" : Math.Round((PM_Nventas / nroPersonas), 2).ToString());
            //ticket2.AddReporteItem("", "ESTADISTICAS", "");
            //ticket2.AddReporteItem("Total Delivery", "ESTADISTICAS", "S/. " + ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_DELIVERY_IMPORTE"].ToString() : "0.00").ToString().ToString());
            //ticket2.AddReporteItem("Total Recojo", "ESTADISTICAS", "S/. " + ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_RECOJO_IMPORTE"].ToString() : "0.00").ToString().ToString());
            //ticket2.AddReporteItem("Cantidad de Delivery", "ESTADISTICAS", ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_DELIVERY"].ToString() : "0").ToString());
            //ticket2.AddReporteItem("Cantidad de Recojo", "ESTADISTICAS", ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_RECOJO"].ToString() : "0").ToString());

            //Double saldo = Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["SALDO_CAJA"].ToString() : "0.00");

            //ticket2.AddReporteItemventas("CAJA CHICA", "", "");
            //ticket2.AddReporteItemventas("Total Ingresos", "CAJA CHICA", (dt2.Rows.Count > 0) ? dt2.Rows[0]["INGRESO_CAJA"].ToString() : "0.00");
            //ticket2.AddReporteItemventas("Total Egresos", "CAJA CHICA", (dt2.Rows.Count > 0) ? dt2.Rows[0]["EGRESO_CAJA"].ToString() : "0.00");
            //ticket2.AddReporteItemventas("Saldo", "CAJA CHICA", (dt2.Rows.Count > 0) ? dt2.Rows[0]["SALDO_CAJA"].ToString() : "0.00");

            //ticket2.AddReporteItemventas("TOTAL CAJA : ", "CAJA CHICA", (dt2.Rows.Count > 0) ? dt2.Rows[0]["CAJA"].ToString() : "0.00");

            ticket2.AddReporteItemventas("----------------------------------", "", "");

            ticket2.AddReporteItemventas("COBRADO", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL"].ToString() : "0.00");
            ticket2.AddReporteItemventas("X COBRAR", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["POR_PAGAR"].ToString() : "0.00");
            object totalventas = 0;
            totalventas = Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL"] : 0.00) + Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["POR_PAGAR"] : 0.00);
            ticket2.AddReporteItemventas("TOTAL", "VENTAS", totalventas.ToString());
            ticket2.AddReporteItemventas("", "VENTAS", "");

            ticket2.AddReporteItemventas("Total Vendido", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["SUBTOTAL"].ToString() : "0.00");
            ticket2.AddReporteItemventas("Total Descuento", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["DESCUENTO"].ToString() : "0.00");
            ticket2.AddReporteItemventas("Importe Total de la Venta ", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL"].ToString() : "0.00");

            double montoefe = 0.00;

            bool pago = false;
            for (int p1 = 0; p1 < denotg.Distinct().Count(); p1++)
            {
                if (denotg[p1].ToString().Trim() != "")
                {
                    if (dttipag.Rows[p1]["DENO_PAGO"].ToString() == "EFECTIVO")
                    {
                        montoefe = Convert.ToDouble((dttipag.Rows.Count > 0) ? dttipag.Rows[p1]["PED_TOTAL"].ToString() : "0.00");
                    }
                    string nomcat = denotg[p1].ToString();
                    ticket2.AddDocElectronicoAnuladosTotal(dttipag.Rows[p1]["DENO_PAGO"].ToString(), "TIPO PAGO", (dttipag.Rows.Count > 0) ? dttipag.Rows[p1]["PED_TOTAL"].ToString() : "0.00");
                    pago = true;
                }
            }
            //if (pago == true)
            //{
            //    ticket2.AddFooterLine("");

            //    //ticket2.AddDocElectronicoAnuladosTotal("INGRESO CAJA CHICA", "", (dt2.Rows.Count > 0) ? dt2.Rows[0]["INGRESO_CAJA"].ToString() : "0.00");
            //    //Double total,totalventa,totcaja;
            //    //totcaja = Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["INGRESO_CAJA"].ToString() : "0.00");
            //    //total = Convert.ToDouble(dttipag.Rows[0]["MONTOTOTAL"].ToString());
            //    //total = totcaja + total;
            //    ticket2.AddDocElectronicoAnuladosTotal("TOTAL:", "", dttipag.Rows[0]["MONTOTOTAL"].ToString());
            //}
            //Double totalcaja = 0.00;
            //totalcaja = montoefe + saldo;
            ////ticket2.AddReporteItemventas("----------------------------------", "", "");
            //ticket2.AddDocElectronicoAnuladosTotal("", "", "");
            //ticket2.AddDocElectronicoAnuladosTotal("TOTAL EFECTIVO : ", "CAJA", montoefe.ToString());
            //ticket2.AddDocElectronicoAnuladosTotal("TOTAL CAJA CHICA: ", "CAJA", saldo.ToString());
            //ticket2.AddDocElectronicoAnuladosTotal("TOTAL CAJA : ", "CAJA", totalcaja.ToString());
            //ticket2.AddFooterLine("");
            if (ticket2.PrinterExists(globales.ImpCaja()) != true)
            {
                MessageBox.Show("Impresora: " + globales.ImpCaja() + " no está disponible.");
            }
            else
            {
                ticket2.PrintTicket(globales.ImpCaja());
            }
            /******************FIN *********************/
            #endregion
        }
        #endregion
        #region abrir dialog


        public object ValorData { get; set; }
        private void AbrirDailogReporteFecha()
        {
            var reporteF = new DialogReporteFecha
            {

            };
            DialogHost.Show(reporteF, "RootDialog");
        }
        #endregion
        #region  Cierre de caja

        public object estadoCierre { get; set; }
        private async void Cierrecaja()
        {

            if (this.DataDetallePedido.Count != 0)
            {
                this.estadoCierre = this.DataDetallePedido.Where(s => s.id_estado_f.ToString() == "2").Count();

                if (Convert.ToInt32(estadoCierre) == 0)
                {
                    var SiNo = new SiNoMessageDialog
                    {
                        Mensaje = { Text = "Está seguro de cerrar caja?"}
                    };
                    var x = await DialogHost.Show(SiNo, "RootDialog");
                    if (!(bool)x)
                        return;
                    ImprimirTickets();
                    DataTable DetallePagos = new DataTable();
                    DetallePagos = negCierre.GetDetallePagos();
                    VentaDia = negCierre.GetVentaDia();
                    empresa = new ObservableCollection<Empresa>();
                    VentasDia ventadia = new VentasDia();
                    var user = this.DataUsuario.Where(w => w.estadousu == 1).FirstOrDefault().idusu;
                    ventadia.CJ_id_usu = user.ToString();
                    string _mensaje = "";
                    int result = negDetVent.SetCierreCaja((1), ventadia, ref _mensaje, NombreEquipo);
                    int Desc;
                    string verificar = InternetGetConnectedState(out Desc, 0).ToString();
                    if ((Convert.ToBoolean(verificar) == true))
                    {
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        // Envio cierre de caja
                        string json = "";
                        empresa = negEmpresa.GetEmpresa();
                        json = negCierre.GetCierreJson();
                        var jsonDatareq = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(VentaDia, Formatting.Indented));
                        //foreach (var e in correo)
                        //{
                        try
                        {
                            NombreEquipo = ConfigurationManager.AppSettings["NombreEquipo"].ToString();
                            using (var client = new WebClient())
                            {
                                var values = new NameValueCollection();
                                values["id_empresa"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                                values["fecha"] = DateTime.Now.ToString("yyyy/MM/dd");
                                values["monto_total"] = VentaDia.First().TOTALVENTA;
                                values["monto_descuento"] = (Convert.ToDecimal(VentaDia.First().VBRUTA) - Convert.ToDecimal(VentaDia.First().TOTALVENTA)).ToString();
                                values["total_ingresos_caja"] = VentaDia.First().INGRESO;
                                values["total_egresos_caja"] = VentaDia.First().EGRESO;
                                values["total_efectivo"] = DetallePagos.Rows[0]["EFECTIVO"].ToString();
                                values["total_visa"] = DetallePagos.Rows[0]["VISA"].ToString();
                                values["total_mastercard"] = DetallePagos.Rows[0]["MASTERCARD"].ToString();
                                values["total_boletas"] = VentaDia.First().TOTALBOLE;
                                values["cantidad_boletas"] = VentaDia.First().BOLECANT;
                                values["total_facturas"] = VentaDia.First().TOTALFACT;
                                values["cantidad_facturas"] = VentaDia.First().FACTCANT;
                                values["id_cierre"] = result.ToString();
                                values["nombre_empresa"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().empr_nom.ToString();
                                values["correo"] = json;
                                values["nombre_caja"] = NombreEquipo;
                                var response = client.UploadValues(negParametros.EnvioCierre(), values);
                                var responseString = Encoding.Default.GetString(response);
                            }
                            using (var client = new WebClient())
                            {
                                DataTable dtTopPlatos = new DataTable();
                                dtTopPlatos = negCierre.GetTopPlatosCierre(result.ToString());
                                if (dtTopPlatos.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dtTopPlatos.Rows.Count; j++)
                                    {
                                        var values = new NameValueCollection();
                                        values["id_business"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                                        values["fecha"] = DateTime.Now.ToString("yyyy/MM/dd");
                                        values["id_carta"] = dtTopPlatos.Rows[j]["DP_ID_CARTA"].ToString();
                                        values["nom_carta"] = dtTopPlatos.Rows[j]["DP_DESCRIP"].ToString();
                                        values["cant"] = dtTopPlatos.Rows[j]["DP_CANT"].ToString();
                                        values["importe"] = dtTopPlatos.Rows[j]["DP_IMPORT"].ToString();
                                        values["id_cierre"] = result.ToString();
                                        var response = client.UploadValues(negParametros.SetPlatosVendidosHistorial(), values);
                                        var responseString = Encoding.Default.GetString(response);
                                    }
                                }
                            }
                            using (var client = new WebClient())
                            {
                                DataTable dtTopPlatos = new DataTable();
                                dtTopPlatos = negCierre.GetStock();
                                if (dtTopPlatos.Rows.Count > 0)
                                {
                                    ObservableCollection<StockWebService> items_web = new ObservableCollection<StockWebService>();
                                    for (int j = 0; j < dtTopPlatos.Rows.Count; j++)
                                    {
                                        StockWebService new_item = new StockWebService();
                                        new_item.id_business = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                                        new_item.fecha = DateTime.Now.ToString("yyyy/MM/dd");
                                        new_item.INS_NOM = dtTopPlatos.Rows[j]["INS_NOM"].ToString();
                                        new_item.ALM_NOM = dtTopPlatos.Rows[j]["ALM_NOM"].ToString();
                                        new_item.CANT = dtTopPlatos.Rows[j]["CANT"].ToString();
                                        new_item.id_cierre = result.ToString();
                                        items_web.Add(new_item);
                                    }
                                    string json_items = JsonConvert.SerializeObject(items_web);
                                    var values = new NameValueCollection();
                                    values["items"] = json_items;
                                    var response = client.UploadValues(negParametros.GuardarStock(), values);
                                    var responseString = Encoding.Default.GetString(response);
                                }
                            }
                            using (var client = new WebClient())
                            {
                                DataTable dtCajaChica = new DataTable();
                                dtCajaChica = negCierre.GetCajaChicaCierre(result.ToString(),NombreEquipo);
                                if (dtCajaChica.Rows.Count > 0)
                                {
                                    ObservableCollection<CajaChicaWebService> items_web = new ObservableCollection<CajaChicaWebService>();
                                    for (int j = 0; j < dtCajaChica.Rows.Count; j++)
                                    {
                                        CajaChicaWebService new_item = new CajaChicaWebService();
                                        new_item.id_business = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                                        new_item.ID = dtCajaChica.Rows[j]["ID"].ToString();
                                        new_item.TM_DESCR = dtCajaChica.Rows[j]["TM_DESCR"].ToString();
                                        new_item.CC_ID_MOV = dtCajaChica.Rows[j]["CC_ID_MOV"].ToString();
                                        new_item.CC_ID_TIPO = dtCajaChica.Rows[j]["CC_ID_TIPO"].ToString();
                                        new_item.MC_DESCR = dtCajaChica.Rows[j]["MC_DESCR"].ToString();
                                        new_item.CC_DESCR = dtCajaChica.Rows[j]["CC_DESCR"].ToString();
                                        new_item.CC_MONTO = dtCajaChica.Rows[j]["CC_MONTO"].ToString();
                                        new_item.nombre_empleado = dtCajaChica.Rows[j]["nombre_empleado"].ToString();
                                        new_item.CC_ID_EMPL = dtCajaChica.Rows[j]["CC_ID_EMPL"].ToString();
                                        new_item.CC_F_CREATE = Convert.ToDateTime(dtCajaChica.Rows[j]["CC_F_CREATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                                        new_item.CC_CIERRE_DIA = dtCajaChica.Rows[j]["CC_CIERRE_DIA"].ToString();
                                        items_web.Add(new_item);
                                    }
                                    string json_items = JsonConvert.SerializeObject(items_web);
                                    var values = new NameValueCollection();
                                    values["items"] = json_items;
                                    var response = client.UploadValues(negParametros.CajaChicaCloud(), values);
                                    var responseString = Encoding.Default.GetString(response);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //globales.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
                        }
                    }
                    if (result != 0)
                    {
                        this.Operacion = "Consolidado";
                        this.GetLista();
                        this.DataList();
                        this.Data();
                        this.datosbacis();
                        this.DatosBarra();
                    }
                }
                else
                {
                    int validarCierre = this.DataDetallePedido.Where(w => Convert.ToInt32(w.id_estado_f) == 2).Count();
                    if (validarCierre > 0)
                    {
                        globales.Mensaje("SOS-FOOD Información", "Tienes " + validarCierre + " pedidos por pagar", 3);
                    }
                }
            }
        }

        #endregion
        #region Exportar 
        
        private void ExportarPDF()
        {
            string fecha = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            saveFileDialog1.FileName = "Reporte_Det_Pedido_" + fecha;

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("N° DIARIO");
            dt.Columns.Add("MESA");
            dt.Columns.Add("SUB T.");
            dt.Columns.Add("DESC");
            dt.Columns.Add("TOTAL");
            dt.Columns.Add("EMPLEADO");
            dt.Columns.Add("F. PEDIDO");
            dt.Columns.Add("DESC. DESCUENTO");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("F. PAGO");
            dt.Columns.Add("PROPINA");
            dt.Columns.Add("F. PROPINA");
            dt.Columns.Add("USUARIO");
            dt.Columns.Add("PERS");
            dt.Columns.Add("N° T.");
            foreach (var p in DataDetallePedido)
            {
                dt.Rows.Add(new object[] { p.id_ped, p.num_dia_ped, p.nom_mesa, p.subtotal_ped, p.desc_ped, p.total_ped, p.nom_emple, p.f_ped, p.nom_tip_desc, p.nom_estado_f, p.nom_fpago, p.P_PROPINA, p.TPpropina, p.nom_usu, p.nro_personas, p.nro_tarjeta });
            }
            if (dt.Rows.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == true)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        string direccion = saveFileDialog1.InitialDirectory;
                        string nombrearchivo = saveFileDialog1.FileName;
                        // Code to write the stream goes here.

                        string ubicacion = saveFileDialog1.FileName;
                        myStream.Close();
                        globales.ExportDataTableToPdf(dt, ubicacion, "Historial Ventas del Día");
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("no hay registros");
            }
        }
        private void ExportarExcelConsolidado()
        {
            try
            {
                #region Ubicacion del Archivo Excel
                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.Title = "Exportar Archivo a Excel";
                saveFileDialog1.FileName = "Reporte Ventas Diarias_" + DateTime.Now.ToString("ddMMyyyy HHmmss");
                string ubicacion = "";
                if (saveFileDialog1.ShowDialog() == true)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        string direccion = saveFileDialog1.InitialDirectory;
                        string nombrearchivo = saveFileDialog1.FileName;
                        ubicacion = saveFileDialog1.FileName;
                        myStream.Close();
                    }
                }
                #endregion

                SLDocument sl = new SLDocument();
                sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Reporte");
                sl.SetCellValue("A1", "VENTAS DIARIAS");
                #region Estilos
                //Cabecera
                SLStyle estiloCabecera = sl.CreateStyle();
                estiloCabecera.Font.FontName = "Arial";
                estiloCabecera.Font.FontSize = 13;
                estiloCabecera.Font.Bold = true;
                estiloCabecera.Font.FontColor = System.Drawing.Color.OrangeRed;

                //Titulos
                SLStyle estiloT = sl.CreateStyle();
                estiloT.Font.FontName = "Arial";
                estiloT.Font.FontSize = 11;
                estiloT.Font.Bold = true;

                //Colores
                SLStyle ecventas = sl.CreateStyle(); ecventas.Font.FontColor = System.Drawing.Color.FromArgb(0, 150, 62);
                SLStyle eccajachica = sl.CreateStyle(); eccajachica.Font.FontColor = System.Drawing.Color.FromArgb(0, 166, 182);
                SLStyle ecpagos = sl.CreateStyle(); ecpagos.Font.FontColor = System.Drawing.Color.FromArgb(109, 1, 232);
                SLStyle ectotalcaja = sl.CreateStyle(); ectotalcaja.Font.FontColor = System.Drawing.Color.FromArgb(2, 153, 93);
                SLStyle ecboletadyfacturas = sl.CreateStyle(); ecboletadyfacturas.Font.FontColor = System.Drawing.Color.FromArgb(9, 101, 235);
                SLStyle ecindicadores = sl.CreateStyle(); ecindicadores.Font.FontColor = System.Drawing.Color.FromArgb(0, 130, 151);

                //Montos
                SLStyle estiloM = sl.CreateStyle();
                estiloM.Font.FontName = "Arial";
                estiloM.Font.FontSize = 10;
                estiloM.Font.Bold = true;
                #endregion

                sl.SetCellStyle("A1", estiloCabecera);
                sl.MergeWorksheetCells("A1", "B1");

                #region Ventas
                sl.SetCellValue("A3", "VENTAS");
                sl.MergeWorksheetCells("A3", "B3");
                sl.SetCellStyle("A3", estiloT);
                sl.SetCellStyle("A3", ecventas);
                sl.SetCellStyle("A4", "A6", estiloM);

                sl.SetCellValue("A4", "Total Cobrado:");
                sl.SetCellValue("B4", Monto);

                sl.SetCellValue("A5", "X Cobrar:");
                sl.SetCellValue("B5", TotalxCobrar);

                sl.SetCellValue("A6", "Total:");
                sl.SetCellValue("B6", VDia);
                #endregion
                #region Caja Chica
                sl.SetCellValue("D3", "Caja Chica");
                sl.MergeWorksheetCells("D3", "E3");
                sl.SetCellStyle("D3", estiloT);
                sl.SetCellStyle("D3", eccajachica);
                sl.SetCellStyle("D4", "D6", estiloM);

                sl.SetCellValue("D4", "Ingresos: ");
                sl.SetCellValue("E4", CC_ingreso);

                sl.SetCellValue("D5", "Egresos: ");
                sl.SetCellValue("E5", CC_egreso);

                sl.SetCellValue("D6", "Total: ");
                sl.SetCellValue("E6", CC_total);
                #endregion
                #region Pagos
                sl.SetCellValue("G3", "Pagos");
                sl.MergeWorksheetCells("G3", "H3");
                sl.SetCellStyle("G3", estiloT);
                sl.SetCellStyle("G3", ecpagos);

                int row_pagos = 4;
                //this.dataTipopago = negVentaD.GetVentasDia_TipoPago2();
                foreach (var h in dataTipopago)
                {
                    sl.SetCellValue("G" + row_pagos.ToString(), h.TP_deno);
                    sl.SetCellStyle("G" + row_pagos.ToString(), estiloM);
                    sl.SetCellValue("H" + row_pagos.ToString(), Convert.ToDecimal(h.TP_monto));
                    row_pagos += 1;
                }
                #endregion
                #region Total Caja
                sl.SetCellValue("J3", "Total Caja");
                sl.MergeWorksheetCells("J3", "K3");
                sl.SetCellStyle("J3", estiloT);
                sl.SetCellStyle("J3", ectotalcaja);
                sl.SetCellStyle("J4", "J8", estiloM);

                sl.SetCellValue("J4", "Venta: ");
                sl.SetCellValue("K4", Monto);

                sl.SetCellValue("J5", "Total Caja Chica: ");
                sl.SetCellValue("K5", CC_total);

                sl.SetCellValue("J6", "Dolar: ");
                sl.SetCellValue("K6", MontoDolar);

                sl.SetCellValue("J7", "Caja Efectivo: ");
                sl.SetCellValue("K7", TotalEfectivo);

                sl.SetCellValue("J8", "Total: ");
                sl.SetCellValue("K8", totalcaja);
                #endregion
                #region Boleta y Factura
                //Emitidos
                int row_bf = row_pagos + 2;
                sl.SetCellStyle("A" + row_bf, "A" + (row_bf + 4), estiloM);
                sl.SetCellStyle("B" + (row_bf + 1), "C" + (row_bf + 1), estiloM);

                sl.SetCellValue("A" + row_bf, "Boletas y Facturas");
                sl.MergeWorksheetCells("A" + row_bf, "C" + row_bf);
                sl.SetCellStyle("A" + row_bf, estiloT);
                sl.SetCellStyle("A" + row_bf, ecboletadyfacturas);

                sl.SetCellValue("A" + (row_bf + 1), "");
                sl.SetCellValue("B" + (row_bf + 1), "Importe");
                sl.SetCellValue("C" + (row_bf + 1), "Cantidad");

                sl.SetCellValue("A" + (row_bf + 2), "Boletas: ");
                sl.SetCellValue("B" + (row_bf + 2), DC_MontoBol);
                sl.SetCellValue("C" + (row_bf + 2), DC_CantDocBol);

                sl.SetCellValue("A" + (row_bf + 3), "Facturas: ");
                sl.SetCellValue("B" + (row_bf + 3), DC_MontoFac);
                sl.SetCellValue("C" + (row_bf + 3), DC_CantDocFAc);

                sl.SetCellValue("A" + (row_bf + 4), "Total: ");
                sl.SetCellValue("B" + (row_bf + 4), DC_Total);


                // Anulaciones
                sl.SetCellStyle("E" + (row_bf + 2), "E" + (row_bf + 4), estiloM);
                sl.SetCellStyle("F" + (row_bf + 1), "G" + (row_bf + 1), estiloM);

                sl.SetCellValue("E" + row_bf, "Boletas y Facturas Anuladas");
                sl.MergeWorksheetCells("E" + row_bf, "G" + row_bf);
                sl.SetCellStyle("E" + row_bf, estiloT);
                sl.SetCellStyle("E" + row_bf, ecboletadyfacturas);

                sl.SetCellValue("E" + (row_bf + 1), "");
                sl.SetCellValue("F" + (row_bf + 1), "Importe");
                sl.SetCellValue("G" + (row_bf + 1), "Cantidad");

                sl.SetCellValue("E" + (row_bf + 2), "Boletas: ");
                sl.SetCellValue("F" + (row_bf + 2), DC_MontoBolAnuladas);
                sl.SetCellValue("G" + (row_bf + 2), DC_CantBolAnuladas);

                sl.SetCellValue("E" + (row_bf + 3), "Facturas: ");
                sl.SetCellValue("F" + (row_bf + 3), DC_MontoFactAnuladas);
                sl.SetCellValue("G" + (row_bf + 3), DC_CantFactAnuladas);

                sl.SetCellValue("E" + (row_bf + 4), "S/B: ");
                sl.SetCellValue("F" + (row_bf + 4), DC_Sum_Total);

                #endregion
                #region Indicadores
                row_bf = row_bf + 4;
                int row = row_bf + 2;
                sl.SetCellStyle("A" + row, "A" + (row + 4), estiloM);

                sl.SetCellValue("A" + row, "Indicadores");
                sl.SetCellStyle("A" + row, estiloT);
                sl.SetCellStyle("A" + row, ecindicadores);

                sl.SetCellValue("A" + (row + 1), "Mesas Atendidas");
                sl.SetCellValue("B" + (row + 1), MA_nmesas);

                sl.SetCellValue("A" + (row + 2), "Promedio Consumo por Mesa");
                sl.SetCellValue("B" + (row + 2), TP_totalpromedio);

                sl.SetCellValue("A" + (row + 3), "Mozo del Dia");
                sl.SetCellValue("B" + (row + 3), RMMozo);

                sl.SetCellValue("A" + (row + 4), "Nro Personas Atendidas");
                sl.SetCellValue("B" + (row + 4), TP_Nro_personas);
                #endregion
                sl.AutoFitColumn("A", "K");
                sl.SaveAs(ubicacion);
            }
            catch (Exception ex)
            {
            }
        }
        private void ExportarExcel()
        {
            string fecha = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            saveFileDialog1.FileName = "Reporte_Det_Pedido_" + fecha;

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("N° DIARIO");
            dt.Columns.Add("MESA");
            dt.Columns.Add("SUB T.");
            dt.Columns.Add("DESC");
            dt.Columns.Add("TOTAL");
            dt.Columns.Add("EMPLEADO");
            dt.Columns.Add("F. PEDIDO");
            dt.Columns.Add("DESC. DESCUENTO");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("F. PAGO");
            dt.Columns.Add("PROPINA");
            dt.Columns.Add("F. PROPINA");
            dt.Columns.Add("USUARIO");
            dt.Columns.Add("PERS");
            dt.Columns.Add("N° T.");
            foreach (var p in DataDetallePedido)
            {
                dt.Rows.Add(new object[] { p.id_ped, p.num_dia_ped, p.nom_mesa, p.subtotal_ped, p.desc_ped, p.total_ped, p.nom_emple, p.f_ped, p.nom_tip_desc, p.nom_estado_f, p.nom_fpago,p.P_PROPINA,p.TPpropina ,p.nom_usu, p.nro_personas, p.nro_tarjeta });
            }
            if (dt.Rows.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == true)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        string direccion = saveFileDialog1.InitialDirectory;
                        string nombrearchivo = saveFileDialog1.FileName;
                        // Code to write the stream goes here.

                        string ubicacion = saveFileDialog1.FileName;
                        myStream.Close();
                        CreateExcelFile.CreateExcelDocument(dt, ubicacion);
                        //DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("no hay registros");
            }
        }
        #endregion
        #region DEtalle dialogs
        private async void DialogoPagar()
        {
            Application.Current.Properties["DesdeHistorialVentas"] = null;
            Application.Current.Properties["DetallePropinas"] = null;
            var SiNo = new DialogDetPagos
            {

            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
        }
        private async void DialogoDetProdAnualdos()
        {
            System.Windows.Application.Current.Properties["HastaHistorialVentas"] = null;
            var SiNo = new DialogDetProdAnulados
            {

            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
        }
        private async void DialogoVentasAnualdos()
        {
            Application.Current.Properties["DesdeHistorialVentas"] = null;
            var SiNo = new DialogDetVentasAnulados
            {

            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
        }
        #endregion
        public async void AbrirDialogBolFac()
        {
            if (this.DataDetallePedido.Count != 0)
            {
                if (this.id_pedido > 0)
                {
                    ValidarTieneDoc(id_pedido);
                    this.ValorData = this.DataDetallePedido.Where(w => Convert.ToInt32(w.id_ped) == id_pedido && Convert.ToInt32(w.id_tipo_Doc) == 0 && Convert.ToInt32(w.id_estado_f) != 3).Count();
                    if (Convert.ToInt32(ValorData) > 0)
                    {
                        var BolFac = new DialogBoleta_Factura {};
                        var x = await DialogHost.Show(BolFac, "RootDialog");
                    }
                    else
                    {
                        int validaranulado = this.DataDetallePedido.Where(w => Convert.ToInt32(w.id_ped) == id_pedido && Convert.ToInt32(w.id_estado_f) == 3).Count();
                        if (validaranulado > 0)
                        {
                            globales.Mensaje("SOS-FOOD Información", "El pedido fué Anulado", 3);
                        }
                        else
                        {
                            var bolfac_emitido = this.DataDetallePedido.Where(c => Convert.ToInt32(c.id_ped) == id_pedido).Select(s => s.nom_tipo_Doc).FirstOrDefault();
                            globales.Mensaje("SOS-FOOD Información", "Ya tienes una " + bolfac_emitido + " emitida", 2);
                        }
                    }
                    this.ValorData = 0;
                }
                else
                {
                    globales.Mensaje("SOS-FOOD Información", "Seleccione una fila para emitir documento", 2);
                }
                this.id_pedido = 0;
            }
        }
        private async void DialogoPropinas()
        {
            Application.Current.Properties["DesdeHistorialVentas"] = null;
            Application.Current.Properties["DetallePropinas"] = "SI";
            var SiNo = new DialogDetPagos
            {

            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
        }
        Neg_Pedido negPedido = new Neg_Pedido();
        private void ValidarTieneDoc(int id_pedido)
        {
            DataTable doc_x_pedido = negPedido.getBoletaxPedido(id_pedido);
            if (doc_x_pedido.Rows.Count > 0)
            {
                String boletas = "";
                for (int i = 0; i < doc_x_pedido.Rows.Count; i++)
                {
                    if (boletas == "")
                    {
                        boletas = doc_x_pedido.Rows[i]["serieNumero"].ToString();
                    }
                    else
                    {
                        boletas = boletas + ", " + doc_x_pedido.Rows[i]["serieNumero"].ToString();
                    }
                }
                globales.Mensaje("SOS-FOOD Mensaje:", "Este pedido tiene documento(s) emitido(s): \n " + boletas, 2);
            }
        }
        private async void EliminarPlatoPedido(object id){}
    }
}
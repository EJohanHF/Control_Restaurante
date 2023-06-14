using Capa_Entidades;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Entidades.Models.Reportes.Pedido;
using Capa_Entidades.Models.Usuario;
using Capa_Negocio.Carta;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Pedido;
using Capa_Negocio.Reportes;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Negocio.User;
using Capa_Presentacion_WPF.Views.Dialogs.DialogVentaporDia;
using ExportToExcel;
using LiveCharts;
using LiveCharts.Wpf;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.Pedidos
{
    public class HistorialdeVentasViewModel : IGeneric
    {
        #region Negocio
        Neg_DpVentas negHistVentas = new Neg_DpVentas();
        Neg_Detalle_pedido negDetVent = new Neg_Detalle_pedido();
        Neg_Pedido negPedido = new Neg_Pedido();
        Neg_Platos negPlatos = new Neg_Platos();
        Neg_Parametros negParametros = new Neg_Parametros();
        #endregion
        #region Objetos
        private string operacion;
        private DateTime desde;
        private DateTime hasta;
        private DateTime fechadia;
        private string nombredia;
        private string _promediodolar;
        public string PM_PromedioDolr
        {
            get => _promediodolar;
            set
            {
                _promediodolar = value;
            }
        }
        public DateTime FechDia { get; set; } = DateTime.Now;
        public DateTime FechDesde { get; set; } = DateTime.Now;
        public DateTime FechHasta { get; set; } = DateTime.Now;
        private DpVentas dpventas;
        public ObservableCollection<DpVentas> DataHistVentas { get; set; }
        public ObservableCollection<DpVentas> DataDetPedido { get; set; }
        public ObservableCollection<Detalle_Pedido> DataDetprod { get; set; }
        public List<DpVentas> DataTipoPago { get; set; }
        //public List<DpVentas> dataCajachica { get; set; }
        public object IdPed { get; set; }
        
        public DpVentas DpVentas
        {
            get => dpventas;
            set
            {
                dpventas = value;
            }
        }
        public string Operacion
        {
            get => operacion;
            set
            {
                if (value == "Lista")
                {
                    GetLista();
                }

                operacion = value;

            }
        }
        public DateTime NombreDia
        {
            get
            {
                return DpVentas == null ? DateTime.Now : (DateTime)DpVentas.dia;

            }
            set
            {
                if (value != null)
                {
                    DpVentas.dia = value;
                }
                //DateTime fecha = Convert.ToDateTime(this.DpDescuento.dia;);
                //SelectFormatday = fecha.ToString("dddd");
                fechadia = value;
            }
        }
        public decimal totals { get; set; }
        public decimal amortizars { get; set; }
        public decimal saldos { get; set; }
        public decimal amortizar { get; set; }
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
        public int id_pedido { get; set; }
        public string usua { get; set; }
        public string Usuario
        {
            get => usua;
            set
            {
                //this.usua = Pagar.idusuario;
                usua = value;
            }
        }
        public string pedido { get; set; }
        public string Pedido
        {
            get => pedido;
            set
            {
                //this.pedido = Pagar.idpedido;
                pedido = value;
            }
        }
        public string _EstadoPago { get; set; }
        public string EstadoPago
        {
            get => _EstadoPago;
            set
            {
                _EstadoPago = value;
            }
        }
        #endregion
        #region Commands
        public ICommand ObtenerrutaCommand { get; set; }
        public ICommand BuscarporfechasCommand { get; set; }
        public ICommand AbrirDialogDetPagos { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand RbtTagCommand { get; set; }
        public ICommand ConsolidadoCommand { get; set; }
        public ICommand DetalladoCommand { get; set; }
        public ICommand AbrirDialogDetPedido { get; set; }
        public ICommand AbrirDialogDetVentasAnualdos { get; set; }
        public ICommand AbrirDialogDetProdAnualdos { get; set; }
        public ICommand OpenDialogBolFactCommand { get; set; }
        public ICommand ImprimirCuentaCommand { get; set; }
        #endregion
        #region ClicSelectDatagrid
        private ICommand m_RowClickCommand;
        private ICommand m_ClickDetalleCommand;
        public ICommand AbrirDialogDetPropinas { get; set; }
        public decimal TP_MontoPropina { get; set; }
        public decimal TP_MontoPropinaEfectivo { get; set; }
        public DpVentas SelectedDataFile { get; set; }
        public Detalle_Pedido SelectedDetalle { get; set; }
        public ICommand RowClickCommand
        {
            get
            {
                return m_RowClickCommand;
            }
        }
        public ICommand ClickDetalleCommand
        {
            get
            {
                return m_ClickDetalleCommand;
            }
        }
        public class DelegateCommand : ICommand
        {
            private Action m_Action;
            public DelegateCommand(Action action)
            {
                m_Action = action;
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


        #endregion
        #region radiobuton
        public string rbbtdia { get; set; }
        public string rbbtmes { get; set; }
        public string rbbtdesdehasta { get; set; }
        public string Idradiobuton { get; set; }




        void valorbt()
        {
            rbbtdia = "1";
            rbbtmes = "2";
            rbbtdesdehasta = "3";
        }
        #endregion
        private SeriesCollection dataplatosxarea = new SeriesCollection();
        private SeriesCollection datacanaldeventa = new SeriesCollection();
        public SeriesCollection dataVentasxHora { get; set; }
        public SeriesCollection dataPlatosxArea
        {
            get
            {
                return dataplatosxarea;
            }
            set
            {
                dataplatosxarea = value;
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
            }
        }
        public string[] Labels1 { get; set; }
        public Func<double, string> Formatter { get; set; }
        public void datosbacis()
        {
            string[] horas = dataVentasPorHora.Select(s => s.VPH_Hora.ToString()).ToArray();
            decimal[] data = dataVentasPorHora.Select(s => Convert.ToDecimal(s.VPH_Monto)).ToArray();
            dataVentasxHora = new SeriesCollection
            {new LineSeries { Values = new ChartValues<decimal>(data),Title="Ventas" }};
            Labels1 = horas;
            Formatter = value => value.ToString("C");
        }
        public void DonaPlatxArea()
        {
            try
            {
                string desdet = "";
                string hastat = "";
                string dia = "";

                if (Idradiobuton == "1")
                {
                    desdet = null;
                    hastat = null;
                    DpVentas.desde = null;
                    DpVentas.hasta = null;
                    DpVentas.dia = FechDia;
                }
                else if (Idradiobuton == "3")
                {
                    DpVentas.desde = FechDesde;
                    DpVentas.hasta = FechHasta;
                    dia = null;
                    DpVentas.dia = null;
                }
                if (dataplatosxarea.Count > 0)
                {
                    dataplatosxarea.Clear();
                }
                DataTable dt = new DataTable();
                dt = negDetVent.GetPlatoxArea_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
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
            catch (Exception ex)
            {

            
            }
        }
        public void DonaCanaldeVenta()
        {
            string desdet = "";
            string hastat = "";
            string dia = "";

            if (Idradiobuton == "1")
            {
                desdet = null;
                hastat = null;
                DpVentas.desde = null;
                DpVentas.hasta = null;
                DpVentas.dia = FechDia;
            }
            else if (Idradiobuton == "3")
            {
                DpVentas.desde = FechDesde;
                DpVentas.hasta = FechHasta;
                dia = null;
                DpVentas.dia = null;
            }
            if (datacanaldeventa.Count > 0)
            {
                datacanaldeventa.Clear();
            }
            DataTable dt = new DataTable();
            dt = negDetVent.GetCanaldeVenta_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
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
        public HistorialdeVentasViewModel()
        {
            VisibleMontoDolar = "Hidden";
            Operacion = "Consolidado";
            valorbt();
            this.DataDetallePedido = new ObservableCollection<Detalle_Pedido>();
            DpVentas = new DpVentas();
            DpVentas.dia = DateTime.Now;
            DpVentas.desde = DateTime.Now;
            DpVentas.hasta = DateTime.Now;
            BuscarporfechasCommand = new RelayCommand(new Action(buscarporfechas));
            ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.AbrirDialogDetPagos = new RelayCommand(new Action(DialogoPagar));
            RbtTagCommand = new ParamCommand(new Action<object>(IdRadiobuton));
            this.ConsolidadoCommand = new RelayCommand(new Action(Consolidado)); 
            this.DetalladoCommand = new RelayCommand(new Action(Detallado));
            this.AbrirDialogDetPedido = new ParamCommand(new Action<object>(DialogDetPed));
            this.AbrirDialogDetProdAnualdos = new RelayCommand(new Action(DialogoDetProdAnualdos));
            this.AbrirDialogDetVentasAnualdos = new RelayCommand(new Action(DialogoVentasAnualdos));
            this.OpenDialogBolFactCommand = new RelayCommand(new Action(AbrirDialogBolFac));
            this.ImprimirCuentaCommand = new RelayCommand(new Action(ImpCuenta));
            this.AbrirDialogDetPropinas = new RelayCommand(new Action(DialogoPropinas));
            Idradiobuton = "1";
            DataList();
            this.DonaPlatxArea();
            this.DonaCanaldeVenta();
            this.datosbacis();
            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = SelectedDataFile;
                if (set != null)
                {
                    IdPed = set.id_ped;
                    DataDetPedido = negHistVentas.GetDetPedido(Convert.ToString(IdPed));
                }
            });
            m_ClickDetalleCommand = new DelegateCommand(() =>
            {
                var set = SelectedDetalle;
                if (set != null)
                {
                    if (System.Windows.Application.Current.Properties["id_pedido"] == null)
                    {
                        System.Windows.Application.Current.Properties["id_pedido"] = set.id_ped;
                    }
                    this.id_pedido = set.id_ped;
                    this.Totals = Convert.ToDecimal(set.total_ped);
                    this.Usuario = set.id_usu;
                    this.Pedido = Convert.ToInt32(set.id_ped).ToString();
                    this.EstadoPago = set.nom_estado_f.ToString();
                    System.Windows.Application.Current.Properties["IdClienteDel"] = set.id_cliente;
                    System.Windows.Application.Current.Properties["ClienteLlevar"] = set.nomllevar;
                    System.Windows.Application.Current.Properties["id_pedido"] = set.id_ped;
                    System.Windows.Application.Current.Properties["Totals"] = set.total_ped;
                    System.Windows.Application.Current.Properties["Usuario"] = set.id_usu;
                    System.Windows.Application.Current.Properties["Pedido"] = set.id_ped;
                    System.Windows.Application.Current.Properties["EstadoPago"] = set.nom_estado_f;
                }
            });
            System.Windows.Application.Current.Properties["DesdeHistorialVentas"] = DateTime.Now.ToString("dd/MM/yyyy");
            System.Windows.Application.Current.Properties["HastaHistorialVentas"] = DateTime.Now.ToString("dd/MM/yyyy");
            DpVentas.dia = DateTime.Now;
            DpVentas.desde = DateTime.Now;
            DpVentas.hasta = DateTime.Now;
        }
        private async void DialogoPropinas()
        {
            System.Windows.Application.Current.Properties["DetallePropinas"] = "SI";
            if (Idradiobuton == "1")
            {
                System.Windows.Application.Current.Properties["DesdeHistorialVentas"] = Convert.ToDateTime(DpVentas.dia).ToString("yyyy-MM-dd");
                System.Windows.Application.Current.Properties["HastaHistorialVentas"] = Convert.ToDateTime(DpVentas.dia).ToString("yyyy-MM-dd");
            }
            else if (Idradiobuton == "3")
            {
                System.Windows.Application.Current.Properties["DesdeHistorialVentas"] = Convert.ToDateTime(DpVentas.desde).ToString("yyyy-MM-dd");
                System.Windows.Application.Current.Properties["HastaHistorialVentas"] = Convert.ToDateTime(DpVentas.hasta).ToString("yyyy-MM-dd");
            }
            var SiNo = new DialogDetPagos
            {

            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
        }
        private async void ImpCuenta()
        {
            if (this.id_pedido == 0)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD Información", "Seleccione una Fila para Imprimir Cuenta", 2);
            }
            else
            {
                DataTable dt = new DataTable();
                dt = negDetVent.GetCuentaPedido(this.id_pedido);
                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
                if (dt.Rows.Count > 0)
                {
                    DataTable impresora = new DataTable();
                    string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
                    impresora = negPlatos.GetImpresoraxDocumento(1); //Precuenta
                    for (int i = 0; i < impresora.Rows.Count; i++)
                    {
                        Ticket ticket = new Ticket();

                        System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["EMPR_LOGO"]));

                        ticket.MargenLogo = negParametros.margenLogoTicket();
                        ticket.MaxCharDescrip = negParametros.margenMaxDescrip();
                        ticket.HeaderImage = x;
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
                        else
                        {
                            ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["C_NOM"].ToString());
                            ticket.AddSubHeaderLine("");
                            ticket.AddSubHeaderLine("Nro Documento: " + dt.Rows[0]["C_NRO_DOC"].ToString());
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
                                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                                }
                            }
                        }
                        ticket.AddTotal("SUB TOTAL", dt.Rows[0]["PED_SUBTOTAL"].ToString());
                        ticket.AddTotal("DESC.", dt.Rows[0]["PED_DESCUENTO"].ToString());
                        ticket.AddTotal("", "---------");
                        ticket.AddTotal("TOTAL", dt.Rows[0]["PED_TOTAL"].ToString());
                        ticket.AddFooterLine("");
                        ticket.AddTotal("", "---------");

                        ticket.AddFooterLine("");
                        ticket.AddFooterLine("Este documento no es comprobante de pago.");

                        if (ticket.PrinterExists(ImpCaja))
                        {
                            ticket.PrintTicket(ImpCaja);
                        }
                        else
                        {
                            negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                        }
                    }
                }
                else
                {
                    if (true)
                    {

                    }
                    negFuncionGlobal.Mensaje("SOS-FOOD Información", "No hay nada que imprimir", 3);
                }
            }
        }
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
                negFuncionGlobal.Mensaje("SOS-FOOD Mensaje:", "Este pedido tiene documento(s) emitido(s): \n " + boletas, 2);
            }
        }
        public object ValorData { get; set; }
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
                        var BolFac = new DialogBoleta_Factura
                        {

                        };
                        var x = await DialogHost.Show(BolFac, "RootDialog");
                    }
                    else
                    {
                        int validaranulado = this.DataDetallePedido.Where(w => Convert.ToInt32(w.id_ped) == id_pedido && Convert.ToInt32(w.id_estado_f) == 3).Count();
                        if (validaranulado > 0)
                        {
                            negFuncionGlobal.Mensaje("SOS-FOOD Información", "El pedido fué Anulado", 3);
                        }
                        else
                        {
                            var bolfac_emitido = this.DataDetallePedido.Where(c => Convert.ToInt32(c.id_ped) == id_pedido).Select(s => s.nom_tipo_Doc).FirstOrDefault();
                            negFuncionGlobal.Mensaje("SOS-FOOD Información", "Ya tienes una " + bolfac_emitido + " emitida", 2);
                        }

                    }
                    this.ValorData = 0;

                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD Información", "Seleccione una fila para emitir documento", 2);
                }
                this.id_pedido = 0;
            }
        }
        private async void DialogoDetProdAnualdos()
        {
            if (Idradiobuton == "1")
            {
                System.Windows.Application.Current.Properties["DesdeHistorialVentas"] = Convert.ToDateTime(DpVentas.dia).ToString("yyyy-MM-dd");
                System.Windows.Application.Current.Properties["HastaHistorialVentas"] = Convert.ToDateTime(DpVentas.dia).ToString("yyyy-MM-dd");
            }
            else if (Idradiobuton == "3")
            {
                System.Windows.Application.Current.Properties["DesdeHistorialVentas"] = Convert.ToDateTime(DpVentas.desde).ToString("yyyy-MM-dd");
                System.Windows.Application.Current.Properties["HastaHistorialVentas"] = Convert.ToDateTime(DpVentas.hasta).ToString("yyyy-MM-dd");
            }
            var SiNo = new DialogDetProdAnulados {};
            var x = await DialogHost.Show(SiNo, "RootDialog");
        }
        private async void DialogoVentasAnualdos()
        {
            if (Idradiobuton == "1")
            {
                System.Windows.Application.Current.Properties["DesdeHistorialVentas"] = Convert.ToDateTime(DpVentas.dia).ToString("yyyy-MM-dd");
                System.Windows.Application.Current.Properties["HastaHistorialVentas"] = Convert.ToDateTime(DpVentas.dia).ToString("yyyy-MM-dd");
            }
            else if (Idradiobuton == "3")
            {
                System.Windows.Application.Current.Properties["DesdeHistorialVentas"] = Convert.ToDateTime(DpVentas.desde).ToString("yyyy-MM-dd");
                System.Windows.Application.Current.Properties["HastaHistorialVentas"] = Convert.ToDateTime(DpVentas.hasta).ToString("yyyy-MM-dd");
            }
            var SiNo = new DialogDetVentasAnulados{};
            var x = await DialogHost.Show(SiNo, "RootDialog");
        }
        private async void DialogDetPed(object id)
        {

            System.Windows.Application.Current.Properties["N"] = id;
            this.DataDetprod = negDetVent.GetDetProducto(System.Windows.Application.Current.Properties["N"].ToString());
            System.Windows.Application.Current.Properties["detPEdido"] = DataDetprod;
            var SiNo = new DialogDetPedido
            {

            };
            var x = await DialogHost.Show(SiNo, "RootDialog");


        }
        #region dashboard
        public decimal TotalCaja { get; set; }
        public decimal Totalmonto { get; set; }
        public decimal Bruto { get; set; }
        //public decimal Descuento { get; set; }
        public decimal Cc_ingreso { get; set; }
        public decimal Cc_egreso { get; set; }
        public decimal Cc_importe { get; set; }
        public decimal TP_efectivo { get; set; }
        public decimal TP_tarjeta { get; set; }
        public decimal TP_total { get; set; }

        #endregion
        private void GetLista()
        {
            //string desdet = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //string hastat = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //string mes = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //string dia = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //DataHistVentas = negHistVentas.GetHVentas(null, null, null, null);
            //if (DataHistVentas.Count>0)
            //{
            //    Bruto = DataHistVentas.ToList().Sum(s => Convert.ToDecimal(s.total_ped));
            //    Descuento = DataHistVentas.ToList().Sum(s => Convert.ToDecimal(s.desc_ped));
            //    Totalmonto = Bruto - Descuento;
            //}

            //DataTipoPago = negHistVentas.GetTipoPago(null, null, null, null);
            //dataCajachica = negHistVentas.GetCajaChica(null, null, null, null);

            //Data();

            //DataBolFac2 = negBolFac.GetBoletaFactura();
        }
        private void Consolidado()
        {
            this.Operacion = "Detallado";
            
        }
        private void Detallado()
        {
            this.Operacion = "Consolidado";
        }
        private void DialogoPagar()
        {
            System.Windows.Application.Current.Properties["DetallePropinas"] = null;
            if (Idradiobuton == "1")
            {
                System.Windows.Application.Current.Properties["DesdeHistorialVentas"] = Convert.ToDateTime(DpVentas.dia).ToString("yyyy-MM-dd");
                System.Windows.Application.Current.Properties["HastaHistorialVentas"] = Convert.ToDateTime(DpVentas.dia).ToString("yyyy-MM-dd");
            }
            else if (Idradiobuton == "3")
            {
                System.Windows.Application.Current.Properties["DesdeHistorialVentas"] = Convert.ToDateTime(DpVentas.desde).ToString("yyyy-MM-dd");
                System.Windows.Application.Current.Properties["HastaHistorialVentas"] = Convert.ToDateTime(DpVentas.hasta).ToString("yyyy-MM-dd");
            }

            var SiNo = new DialogDetPagos { };
            DialogHost.Show(SiNo, "RootDialog");

            System.Windows.Application.Current.Properties["DesdeHistorialVentas"] = null;
            System.Windows.Application.Current.Properties["HastaHistorialVentas"] = null;
        }
        private void IdRadiobuton(object id)
        {
            Idradiobuton = id.ToString();
        }
        private void buscarporfechas()
        {
            
            string desdet = Convert.ToDateTime(DpVentas.desde).ToString("dd/MM/yyyy");
            string hastat = Convert.ToDateTime(DpVentas.hasta).ToString("dd/MM/yyyy");
            string mes = Convert.ToDateTime(DpVentas.mes).ToString("dd/MM/yyyy");
            string dia = Convert.ToDateTime(DpVentas.dia).ToString("dd/MM/yyyy");

            if (this.Idradiobuton == "1")
            {
                System.Windows.Application.Current.Properties["DesdeHistorialVentas"] = dia;
                System.Windows.Application.Current.Properties["HastaHistorialVentas"] = dia;
            }
            else if (this.Idradiobuton == "3") {
                System.Windows.Application.Current.Properties["DesdeHistorialVentas"] = desdet;
                System.Windows.Application.Current.Properties["HastaHistorialVentas"] = hastat;
            }
            DataList();
            this.DonaPlatxArea();
            this.DonaCanaldeVenta();
            this.datosbacis();
        }
        private void ExportarPDF()
        {
            string fecha1 = Convert.ToDateTime(this.DpVentas.dia).ToString("yyyy-MM-dd");
            string fecha2 = Convert.ToDateTime(this.DpVentas.mes).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.DpVentas.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.DpVentas.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            if (this.Idradiobuton == "1")
            {
                saveFileDialog1.FileName = "Historial_de_Venta_" + fecha1;
            }
            else if (this.Idradiobuton == "2")
            {
                saveFileDialog1.FileName = "Historial_de_Venta_" + fecha2;
            }
            else
            {
                saveFileDialog1.FileName = "Historial_de_Venta_" + fecha3 + "_" + fecha4;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NUMERO PEDIDO");
            dt.Columns.Add("MESA");
            dt.Columns.Add("DESCUENTO");
            dt.Columns.Add("SUB TOTAL");
            dt.Columns.Add("PRECIO TOTAL");
            dt.Columns.Add("FECHA DEL PEDIDO");
            dt.Columns.Add("ESTADO FINANCIERO");
            dt.Columns.Add("NRO PERSONAS");
            dt.Columns.Add("CLIENTE");
            dt.Columns.Add("TELEFONO");
            dt.Columns.Add("TIPO DESCUENTO");
            dt.Columns.Add("FORMA PAGO");
            dt.Columns.Add("DATOS TARJETA");
            dt.Columns.Add("MOZO");
            dt.Columns.Add("PROPINA");
            dt.Columns.Add("FORMA PAGO PROPINA");
            dt.Columns.Add("USUARIO");
            dt.Columns.Add("SERIE DE DOCUMENTO");
            dt.Columns.Add("IMPORTE");
            foreach (var p in DataDetallePedido)
            {
                dt.Rows.Add(new object[] { p.id_ped, p.num_dia_ped, p.nom_mesa, p.desc_ped, p.subtotal_ped, p.total_ped,
                    p.f_ped, p.nom_estado_f, p.nro_personas, p.nomllevar, p.telefcli, p.nom_tip_desc, p.nom_fpago, p.nro_tarjeta,
                    p.nom_emple, p.propina, p.TPpropina, p.nom_usu, p.nom_tipo_Doc, p.importe_Total_Doc_Elec });
            }
            if (dt.Rows.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        string direccion = saveFileDialog1.InitialDirectory;
                        string nombrearchivo = saveFileDialog1.FileName;

                        string ubicacion = saveFileDialog1.FileName;
                        myStream.Close();
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "Historial de Ventas");
                    }
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }
        private void ExportarExcel()
        {
            string fecha1 = Convert.ToDateTime(this.DpVentas.dia).ToString("yyyy-MM-dd");
            string fecha2 = Convert.ToDateTime(this.DpVentas.mes).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.DpVentas.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.DpVentas.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            if (this.Idradiobuton == "1")
            {
                saveFileDialog1.FileName = "Historial_de_Venta_" + fecha1;
            }
            else if (this.Idradiobuton == "2")
            {
                saveFileDialog1.FileName = "Historial_de_Venta_" + fecha2;
            }
            else
            {
                saveFileDialog1.FileName = "Historial_de_Venta_" + fecha3 + "_" + fecha4;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NUMERO PEDIDO");
            dt.Columns.Add("MESA");
            dt.Columns.Add("DESCUENTO");
            dt.Columns.Add("SUB TOTAL");
            dt.Columns.Add("PRECIO TOTAL");
            dt.Columns.Add("FECHA DEL PEDIDO");
            dt.Columns.Add("ESTADO FINANCIERO");
            dt.Columns.Add("NRO PERSONAS");
            dt.Columns.Add("CLIENTE");
            dt.Columns.Add("TELEFONO");
            dt.Columns.Add("TIPO DESCUENTO");
            dt.Columns.Add("FORMA PAGO");
            dt.Columns.Add("DATOS TARJETA");
            dt.Columns.Add("MOZO");
            dt.Columns.Add("PROPINA");
            dt.Columns.Add("FORMA PAGO PROPINA");
            dt.Columns.Add("USUARIO");
            dt.Columns.Add("SERIE DE DOCUMENTO");
            dt.Columns.Add("IMPORTE");
            foreach (var p in DataDetallePedido)
            {
                dt.Rows.Add(new object[] { p.id_ped, p.num_dia_ped, p.nom_mesa, p.desc_ped, p.subtotal_ped, p.total_ped,
                    p.f_ped, p.nom_estado_f, p.nro_personas, p.nomllevar, p.telefcli, p.nom_tip_desc, p.nom_fpago, p.nro_tarjeta,
                    p.nom_emple, p.propina, p.TPpropina ,p.nom_usu, p.nom_tipo_Doc, p.importe_Total_Doc_Elec });
            }
            if (dt.Rows.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
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
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        //private void Data()
        //{
        //    /*public decimal Cc_ingreso { get; set; }
        //public decimal Cc_egreso { get; set; }
        //public decimal Cc_importe { get; set; }*/
        //    Cc_ingreso = dataCajachica.Select(w => Convert.ToDecimal(w.cc_ingre)).ToList().FirstOrDefault();
        //    Cc_egreso = dataCajachica.Select(w => Convert.ToDecimal(w.cc_egreso)).ToList().FirstOrDefault();
        //    Cc_importe = dataCajachica.Select(w => Convert.ToDecimal(w.cc_saldo)).ToList().FirstOrDefault();
        //    TotalCaja = Totalmonto - Cc_egreso;
        //    TP_efectivo = DataTipoPago.Select(w => Convert.ToDecimal(w.tp_Montoefectivo)).ToList().FirstOrDefault();
        //    TP_tarjeta = DataTipoPago.Select(w => Convert.ToDecimal(w.tp_Montotarjeta)).ToList().FirstOrDefault();
        //    TP_total = DataTipoPago.Select(w => Convert.ToDecimal(w.tp_Montototal)).ToList().FirstOrDefault();
        //}
        #region VentasDelDiaConsolidado
        public ObservableCollection<Detalle_Pedido> DataDetallePedido { get; set; }
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
        public ObservableCollection<Usuario> DataUsuario { get; set; }
        public ObservableCollection<Detalle_Pedido> Data { get; set; }
        public decimal Monto { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal TotalxCobrar { get; set; }
        public decimal Descuento { get; set; }
        public decimal VBruta { get; set; }
        public decimal VDia { get; set; }
        public string Empresa { get; set; }
        public string Persona { get; set; }
        public string Dias { get; set; }
        private string _totalcli;
        private string _promedio;
        Neg_VentasDia negVentaD = new Neg_VentasDia();
        Neg_Usuario negUser = new Neg_Usuario();
        #endregion
       
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
        public string PM_Promedio
        {
            get => _promedio;
            set
            {
                _promedio = value;
            }
        }
        #region entidad tipo pago
        public string TP_denoTarj { get; set; }
        public string TP_denoEfec { get; set; }
        public object TP_id { get; set; }
        public decimal TP_MontoEfec { get; set; }
        public decimal TP_MontoTarj { get; set; }
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
        public decimal totalcaja { get; set; }
        public decimal TotalEfectivo { get; set; } = 0;
        public string Nanulados { get; set; }
        public string Panulados { get; set; }
        public string VAMonto { get; set; }
        public string NPedidos { get; set; }
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

        #endregion

        #region tiket promedio
        public int TP_Nro_personas { get; set; }
        public decimal TP_totalpromedio { get; set; }
        #endregion
        private void DataList()
        {
            try
            {
                string desdet = "";
                string hastat = "";
                string dia = "";

                if (Idradiobuton == "1")
                {
                    desdet = null;
                    hastat = null;
                    DpVentas.desde = null;
                    DpVentas.hasta = null;
                    DpVentas.dia = FechDia;
                }
                else if (Idradiobuton == "3")
                {
                    //desdet = Convert.ToDateTime(DpVentas.desde).ToString("dd/MM/yyyy");
                    //hastat = Convert.ToDateTime(DpVentas.hasta).ToString("dd/MM/yyyy");
                    DpVentas.desde = FechDesde;
                    DpVentas.hasta = FechHasta;
                    dia = null;
                    DpVentas.dia = null;
                }

                this.datatotal = negVentaD.GetVentasDia_Venta_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.datacliente = negVentaD.GetVentasDia_cliente_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.dataPromedioMesas = negVentaD.GetVentasDia_promedio_mesa_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.dataTipopago = negVentaD.GetVentasDia_TipoPago_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.dataRecordMozo = negVentaD.GetVentasDia_Recordmozo_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.dataMesasAtendidas = negVentaD.GetMesasAtendidas_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.dataCajachica = negVentaD.GetCajaChica_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.dataVentasAnulados = negVentaD.GetVentasAnulados_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.dataDocElect = negVentaD.GetDocElectronico_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.dataDocElectAnu = negVentaD.GetDocElectronicoAnu_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.dataDocElectTODOS = negVentaD.GetDocElectronicoTODOS_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.dataFrecAtencion = negVentaD.GetFrecAtencion_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.dataVentasDiavsVentasPasadas = negVentaD.GetVDiavsVSanterior_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.dataVentasPorHora = negVentaD.GetVentasPorHora_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                //this.Data = negDetVent.GetDetallepedido_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                this.DataUsuario = negUser.GetUsuario();
                DataVentasxCanal = new ObservableCollection<VentasDia>();
                DataTable dtVentas = new DataTable();
                dtVentas = negDetVent.GetCanaldeVentaConsolidado_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
                foreach (DataRow dr in dtVentas.Rows)
                {
                    VentasDia v = new VentasDia();
                    v.cantidad_venta = Convert.ToInt32(dr["CANTIDAD"]);
                    v.importe_venta = Convert.ToInt32(dr["TOTAL"]);
                    v.canal_venta = dr["NOMBRE"].ToString();
                    DataVentasxCanal.Add(v);
                }

                DataDetallePedido = negDetVent.GetDetallepedido_Historial(DpVentas.desde, DpVentas.hasta, DpVentas.dia); // corregido

                obtenerValoresGraficos();
            }
            catch (Exception ex)
            {
            }
        }
        private decimal _montodolar;
        public decimal MontoDolar
        {
            get => _montodolar;
            set
            {

                _montodolar = value;
            }
        }
        public string VisiblePromedioDolr { get; set; }
        public string VisibleMontoDolar { get; set; }
        private void obtenerValoresGraficos()
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
                //ObservableCollection<Detalle_Pedido> data = negDetVent.GetDetallepedido_Historial();
                this.TotalVenta = DataDetallePedido.Where(w => Convert.ToInt32(w.id_estado_f) != 3 && Convert.ToInt32(w.id_estado_f) != 4).Sum(s => Convert.ToDecimal(s.total_ped));
                this.TotalxCobrar = TotalVenta - Monto;
                this.Dias = this.datatotal.Select(w => w.dias).ToList().FirstOrDefault();
                this.VDia = this.Monto + this.TotalxCobrar;
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
                dtPropina = negVentaD.GetPropinasHistorial(DpVentas.desde, DpVentas.hasta, DpVentas.dia);
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
            }
            catch (Exception ex)
            {
                //globales.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
        }
    }
}

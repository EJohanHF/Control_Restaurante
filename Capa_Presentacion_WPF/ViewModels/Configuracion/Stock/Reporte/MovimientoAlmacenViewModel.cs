using Capa_Entidades;
using Capa_Entidades.Models.Stock;
using Capa_Entidades.Models.Stock.Reporte;
using Capa_Negocio;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Stock;
using Capa_Negocio.Stock.Reporte;
using Capa_Presentacion_WPF.Views.Dialogs;
using ExportToExcel;
using LiveCharts;
using LiveCharts.Wpf;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Stock.Reporte
{
    public class MovimientoAlmacenViewModel : IGeneric
    {
        #region Negocio
        Neg_ReporteInsumoAlmacen negReportInsAlm = new Neg_ReporteInsumoAlmacen();
        Neg_InsumoAlmacen negInsumoAl = new Neg_InsumoAlmacen();
        Neg_Combo negCombo = new Neg_Combo();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        #endregion
        #region Entidad
        private Ent_Combo _almacenSelected;
        private Ent_Combo _insumoSelected;
        private Ent_Combo _almacenReportSelected;
        private Ent_Combo _almacenentradaSelected;
        private Ent_Combo _Ent_Combo;
        private InsumoAlmacen insumoalmacen;
        private ReporteInsumoAlmacen reporteinsumoalmacen;
        public InsumoAlmacen InsumoAlmacen
        {
            get => insumoalmacen;
            set
            {
                insumoalmacen = value;
            }
        }
        public Ent_Combo Ent_Combo
        {
            get => _Ent_Combo;
            set
            {
                _Ent_Combo = value;
            }
        }
        public ReporteInsumoAlmacen ReporteInsumoAlmacen
        {
            get => reporteinsumoalmacen;
            set
            {
                reporteinsumoalmacen = value;
            }
        }
        #endregion
        #region Seleccion
        public ReporteInsumoAlmacen SelectedDataFile { get; set; }
        public Ent_Combo AlmacenSelected
        {
            get => _almacenSelected;
            set
            {
                //this.ComboInsumo = new List<Ent_Combo>();
                //this.ComboalmacenSalida = new List<Ent_Combo>();
                this.Cantidad = 0;
                if (value != null)
                {

                    idalm = ((Ent_Combo)value).id;
                    this.ComboInsumo = negCombo.GetComboAlmacenInsumo(value.id.ToString());
                    this.ComboInsumoData = negCombo.GetComboAlmacenInsumo(value.id.ToString());
                    //if (ComboInsumo.Count == 0)
                    //{
                    //    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no tienes Insumos", 2);
                    //    this.ComboInsumo = new List<Ent_Combo>();
                    //    this.ComboAlmacen = new List<Ent_Combo>();
                    //    this.Cantidad = 0;
                    //}
                    //else
                    //{
                    //    //this.ComboAlmacen = negCombo.GetCombo_Almacen();
                    //}


                }
                _almacenSelected = value;
            }
        }
        public Ent_Combo InsumoSelected
        {
            get => _insumoSelected;
            set
            {
                this.ComboalmacenSalida = new List<Ent_Combo>();
                if (value != null)
                {
                    idIns = ((Ent_Combo)value).id;
                    //this.Cantidad = this.DataInsumoAlmacen.Where(w => w.id_insum == Convert.ToInt32(idIns)).FirstOrDefault().cant;
                    this.Insumo = negCombo.GetInsumoxId(idIns);
                    this.Cantidad = this.Insumo.FirstOrDefault().valor1;



                    if (Operacion_Reporte == "Movimiento almacen")
                    {
                        this.ComboalmacenSalida = negCombo.GetComboAlmacenSalida(idalm, value.id.ToString());
                        if (ComboalmacenSalida.Count == 0)
                        {
                            negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no tiene Almacén de salida", 2);

                            //this.ComboAlmacen = new List<Ent_Combo>();
                            //this.Cantidad = 0;
                        }
                        else
                        {
                            Activarboton = true;
                        }
                    }
                }
                _insumoSelected = value;
            }
        }
        public Ent_Combo AlmacenReportSelected
        {
            get => _almacenReportSelected;
            set
            {
                if (value != null)
                {
                    idalm = ((Ent_Combo)value).id;
                    //this.DataReportInsAlm = negReportInsAlm.GetInsumoAlmacen(idalm);
                }
                _almacenReportSelected = value;
            }
        }
        public Ent_Combo AlmacenEntradaSelected
        {
            get => _almacenentradaSelected;
            set
            {
                if (value != null)
                {
                    idAlmEntra = ((Ent_Combo)value).id;
                    //this.DataReportInsAlm = negReportInsAlm.GetInsumoAlmacen(idalm);
                }
                _almacenentradaSelected = value;
            }
        }
        #endregion
        #region Objetos
        private object _cant;
        public string idIns { get; set; }
        public string idAlmEntra { get; set; }
        private string _TextoInsumoBuscador;
        public string TextoInsumoBuscador {
            get => _TextoInsumoBuscador;
            set {
                if (value != null)
                {
                    ComboInsumo = ComboInsumoData.Where(w => w.nombre.ToLower().Contains(value.ToString().ToLower())).ToList();
                }
                else {
                    ComboInsumo = ComboInsumoData;
                }
                _TextoInsumoBuscador = value;
            }
        }
        public string idalm { get; set; }
        public object cant1 { get; set; }

        private string operacion;
        public string _operacion_reporte;
        public bool Activarboton { get; set; }
        public object idInsu { get; set; }
        public object Cantidad
        {
            get => _cant;
            set
            {
                _cant = value;
            }
        }
        public object Cant { get; set; }
        public string Operacion
        {
            get => operacion;
            set
            {
                if (value == "Almacen Insumo")
                    GetLista();
                operacion = value;

            }
        }
        public string Operacion_Reporte
        {
            get => _operacion_reporte;
            set
            {
                _operacion_reporte = value;

            }
        }
        public string TextoBuscar { get; set; }
        #endregion
        #region Commands
        public ICommand FiltrarBusCommand { get; set; }
        public ICommand BuscarCommand2 { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand BtnMovAlmInsCommand { get; set; }
        public ICommand BtnLlenarAlmInsCommand { get; set; }
        public ICommand MovInsumoAlamcenCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand ExportarPDFCommand { get; set; }
        public ICommand ImprimirCommand { get; set; }
        private ICommand m_RowClickCommand;
        public ICommand RowClickCommand
        {
            get
            {
                return m_RowClickCommand;
            }
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
        #endregion
        #region Listas
        public ObservableCollection<ReporteInsumoAlmacen> DataReportInsAlm { get; set; }
        public ObservableCollection<ReporteInsumoAlmacen> DataInsumoAlmacen { get; set; }
        public List<Ent_Combo> ComboAlmacen { get; set; }
        public List<Ent_Combo> ComboInsumo { get; set; }
        public List<Ent_Combo> ComboInsumoData { get; set; }
        public List<Ent_Combo> Insumo { get; set; }
        public List<Ent_Combo> ComboalmacenSalida { get; set; }
        public List<ReporteInsumoAlmacen> dataAlmacenCant { get; set; }
        public List<ReporteInsumoAlmacen> dataAlmacenDetCant { get; set; }
        #endregion
        public MovimientoAlmacenViewModel()
        {
            this.InsumoAlmacen = new InsumoAlmacen();
            
            this.DataInsumoAlmacen = negReportInsAlm.GetListaInsumoAlmacen();
            this.ReporteInsumoAlmacen = new ReporteInsumoAlmacen();
           
            this.Operacion_Reporte = "Llenar almacen";
            this.Operacion = "Almacen Insumo";
            this.ComboAlmacen = negCombo.GetCombo_Almacen();
            this.dataAlmacenCant = negReportInsAlm.GetAlmacenCant();

            datosAlmacenCant();
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.MovInsumoAlamcenCommand = new RelayCommand(new Action(MovimientoAlmInsum));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ImprimirCommand = new RelayCommand(new Action(Imprimir));
            this.FiltrarBusCommand = new RelayCommand(new Action(FiltrarReporte));
            this.BuscarCommand2 = new RelayCommand(new Action(Buscar2));
            this.BtnMovAlmInsCommand = new RelayCommand(new Action(BtnMovAlmIns));
            this.BtnLlenarAlmInsCommand = new RelayCommand(new Action(BtnLlenarAlmIns));
            this.ComboInsumo = new List<Ent_Combo>();
            this.Insumo = new List<Ent_Combo>();
            this.ComboalmacenSalida = new List<Ent_Combo>();
            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = this.SelectedDataFile;
                if (set != null)
                {
                    this.idInsu = set.id_insum;

                    this.dataAlmacenDetCant = negReportInsAlm.GetAlmacenDetInusmo(idInsu.ToString());
                    datosAlmacenDetCant();
                }

            });
        }
        private void Imprimir() {
            try
            {
                if (DataReportInsAlm.Count == 0) {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "No hay datos", 1);
                    return;
                }
                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();

                DataTable dt_stock = new DataTable();
                dt_stock.Columns.Add("Id");
                dt_stock.Columns.Add("Producto");
                dt_stock.Columns.Add("Stock");
                dt_stock.Columns.Add("U. Medida");

                foreach (var i in DataReportInsAlm)
                {
                    DataRow dr = dt_stock.NewRow();
                    dr["Id"] = i.id;
                    dr["Producto"] = i.nomins;
                    dr["Stock"] = i.cant;
                    dr["U. Medida"] = i.umed;
                    dt_stock.Rows.Add(dr);
                }
                int c = Convert.ToInt32(Math.Round(Convert.ToDouble(dt_stock.Rows.Count / 35), 0)) + 1;

                Ticket ticket = new Ticket();
                ticket.AddHeaderLine_2("REPORTES STOCK");
                ticket.AddHeaderLine("                             " + "1/" + c);
                ticket.AddHeaderLine_2("----------------------------");
                ticket.AddSubHeaderLine("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy"));
                ticket.AddHeaderLine_2("----------------------------");

                int contador = 0;
                int pagina = 1;

                foreach (DataRow dr_platos in dt_stock.Rows)
                {
                    ticket.AddItemSinRecorte3(dr_platos["Producto"].ToString(), "", dr_platos["Stock"].ToString());
                    if (contador == 35 || contador == 70 || contador == 105 || contador == 140 || contador == 175 || contador == 205)
                    {
                        if (ticket.PrinterExists(ImpCaja))
                        {
                            pagina = pagina + 1;
                            ticket.PrintTicket(ImpCaja);
                            ticket = new Ticket();
                            ticket.AddHeaderLine("                             " + pagina + "/" + c);
                            ticket.AddSubHeaderLine("");
                            ticket.AddHeaderLine_2("----------------------------");
                            ticket.AddSubHeaderLine("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy"));
                            ticket.AddHeaderLine_2("----------------------------");
                        }
                    }
                    contador = contador + 1;
                }
                if (ticket.PrinterExists(ImpCaja))
                {
                    ticket.PrintTicket(ImpCaja);
                    ticket = new Ticket();
                }
            }
            catch (Exception ex)
            {
            }
        }
        private async void Guardar()
        {
            try
            {
                if (Convert.ToDecimal(InsumoAlmacen.cantidad) == 0)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese la cantidad correcta", 2);
                    return;
                }
                if (InsumoAlmacen.id_almacen != null && InsumoAlmacen.id_insumo != null && InsumoAlmacen.cantidad != null)
                {
                    InsumoAlmacen inal = this.InsumoAlmacen;
                    string _mensaje = "";
                    bool result = negInsumoAl.SetInsumoAlmacen((1), inal, ref _mensaje);
                    if (result)
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", _mensaje, 1);
                        //InsumoAlmacen.cantidad = 0;
                        //this.ComboInsumo = negCombo.GetComboAlmacenInsumo(idalm.ToString());
                        //this.Cantidad = 0;
                        //object ccc = InsumoAlmacen.cantidad;
                        //decimal nueva_cantidad = Convert.ToDecimal(this.Cantidad) + Convert.ToDecimal(inal.cantidad);
                        //ComboInsumo.Where(w => w.id == inal.id_insumo).FirstOrDefault().cantidad = nueva_cantidad.ToString();
                        //this.Cantidad = nueva_cantidad;
                        this.Insumo = negCombo.GetInsumoxId(inal.id_insumo.ToString());
                        this.Cantidad = this.Insumo.FirstOrDefault().valor1;
                        InsumoAlmacen.cantidad = "0";
                    }
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "seleccione o agregue una cantidad", 2);
                }
            }
            catch (Exception ex)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "seleccione o agregue una cantidad", 2);
            }
            
        }
        private void FiltrarReporte()
        {
            if (idalm == null)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe seleccionar un almacen.", 2);
                return;
            }
            this.DataReportInsAlm = negReportInsAlm.GetInsumoAlmacen(idalm);
        }
        private void Buscar2()
        {
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                if (idalm == null)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe seleccionar un almacen.", 2);
                    return;
                }
                this.DataReportInsAlm = negReportInsAlm.GetInsumoAlmacen(idalm);
            }
            else
            {
                if (idalm == null)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe seleccionar un almacen.", 2);
                    return;
                }
                this.DataReportInsAlm = negReportInsAlm.GetInsumoAlmacenxNombre(idalm, TextoBuscar);
            }
        }
        private async void MovimientoAlmInsum()
        {
            try
            {
                if (Convert.ToDecimal(Cantidad) == 0)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "No hay insumos para mover", 2);
                    return;
                }
                if (Convert.ToDecimal(InsumoAlmacen.cantidad) == 0)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingresar cantidad correcta de insumos a mover", 2);
                    return;
                }
                if (InsumoAlmacen.id_almacensal == null) {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Seleccione el almacen de entrada por favor", 2);
                    return;
                }
                if (Convert.ToDecimal(Cantidad) >= Convert.ToDecimal(InsumoAlmacen.cantidad))
                {
                    InsumoAlmacen inal = this.InsumoAlmacen;
                    string _mensaje = "";
                    bool result = negInsumoAl.SetMovimientoInsumoAlmacen((1), inal, ref _mensaje);
                    if (result)
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Registro ingresado correctamente", 1);
                        this.InsumoAlmacen = new InsumoAlmacen();
                        this.ComboalmacenSalida = new List<Ent_Combo>();
                        this.ComboInsumo = new List<Ent_Combo>();
                        this.Cantidad = 0;
                        this.ComboAlmacen = new List<Ent_Combo>();
                        this.ComboAlmacen = negCombo.GetCombo_Almacen();
                        Activarboton = false;
                    }
                    else
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "No se pudo ingresar el registro", 1);
                    }
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "tiene que ser menor o igual al a cantidad actual", 2);
                }
                this.cant1 = new object();
            }
            catch (Exception ex)
            {
            }
        }
        private void ExportarExcel()
        {
            string fecha= Convert.ToDateTime(this.ReporteInsumoAlmacen.desde).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            saveFileDialog1.FileName = "ReporteAlmacen_" + fecha ;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("ALM_NOM");
            dt.Columns.Add("INS_NOM");
            dt.Columns.Add("CANT");
            dt.Columns.Add("TM_DESC");

            //cargar con datos de observablecollection
            foreach (var p in DataReportInsAlm)
            {
                dt.Rows.Add(new object[] { p.id, p.nomal, p.nomins, p.cant,p.umed });
            }
            //DataTable dt = new DataTable();
            //dt = this.DataReportInsAlm();
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
        private void GetLista()
        {
            if (idalm != null)
            {
                DataReportInsAlm = negReportInsAlm.GetInsumoAlmacen(idalm);
                
            }
        }
        private void BtnMovAlmIns()
        {
            this.Operacion_Reporte = "Llenar almacen";
            this.InsumoAlmacen = new InsumoAlmacen();
            //this.Ent_Combo = new Ent_Combo();
            this.ComboalmacenSalida = new List<Ent_Combo>();
            this.ComboInsumo = new List<Ent_Combo>();
            this.Cantidad = 0;
            this.ComboAlmacen = new List<Ent_Combo>();
            this.ComboAlmacen = negCombo.GetCombo_Almacen();
            //System.Windows.Application.Current.Properties["EstMov"] = "Movimiento almacén";
        }
        private void BtnLlenarAlmIns()
        {
            this.Operacion_Reporte = "Movimiento almacen";
            this.InsumoAlmacen = new InsumoAlmacen();
            this.ComboalmacenSalida = new List<Ent_Combo>();
            this.ComboInsumo = new List<Ent_Combo>();
            this.Cantidad = 0;
            this.ComboAlmacen = new List<Ent_Combo>();
            this.ComboAlmacen = negCombo.GetCombo_Almacen();
            Activarboton = false;
        }
        #region Dashboard
        public void datosAlmacenCant()
        {
            string[] Almacen = dataAlmacenCant.Select(s => s.AC_NomAlm.ToString()).ToArray();
            int[] CantProd = dataAlmacenCant.Select(s => Convert.ToInt32(s.AC_Cant_Prod)).ToArray();
            dataAlmacenxCantidad = new SeriesCollection
            {new LineSeries { Values = new ChartValues<Int32>(CantProd),Title="Insumos" }};
            Labels = Almacen;
            Formatter = value => value +"";
        }

        public void datosAlmacenDetCant()
        {
            string[] NomAlmacen = dataAlmacenDetCant.Select(s => s.AI_NomAlmacen.ToString()).ToArray();
            string Insumo = dataAlmacenDetCant.Select(s => s.AI_NomInsumo.ToString()).FirstOrDefault();
            decimal[] Cant = dataAlmacenDetCant.Select(s => Convert.ToDecimal(s.AI_Cant_Prod)).ToArray();
            string UM = dataAlmacenDetCant.Select(s => s.AI_UM_Deno.ToString()).FirstOrDefault();
            dataAlmacenDetxCantidad = new SeriesCollection
            //{new LineSeries { Values = new ChartValues<Int32>(Cant),Title=Insumo.ToString() }};
            {new ColumnSeries { Values = new ChartValues<Decimal>(Cant),Title=Insumo ,}};
            Labels1 = NomAlmacen;
            Formatter1 = value => value + UM;
        }
        public string[] Labels { get; set; }
        public string[] Labels1 { get; set; }
        public Func<double, string> Formatter { get; set; }
        public Func<double, string> Formatter1 { get; set; }
        public SeriesCollection dataAlmacenxCantidad { get; set; }
        public SeriesCollection dataAlmacenDetxCantidad { get; set; }
        #endregion
    }
}

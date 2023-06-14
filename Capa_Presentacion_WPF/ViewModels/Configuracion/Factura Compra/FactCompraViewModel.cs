using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Factura_Compra;
using Capa_Entidades.Models.Stock;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Factura_Compra;
using Capa_Negocio.Stock;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using System.Windows;
using System.IO;
using Syncfusion.Pdf.Lists;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Interactive;
using System.Windows.Data;

using System.Text.RegularExpressions;
using System.Windows.Forms;
using Capa_Negocio.Funciones_Globales;
using ExportToExcel;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Factura_Compra
{
    public class FactCompraViewModel : INotifyPropertyChanged
    {

        FactCompraStructure fcstructure = new FactCompraStructure();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        static ObservableCollection<FactCompra> fCompra = new ObservableCollection<FactCompra>();
        #region Negocio
        Neg_Proveedor negProv = new Neg_Proveedor();
        Neg_Insumos negInsumo = new Neg_Insumos();
        Neg_FactCompra negFact = new Neg_FactCompra();
        Neg_Almacen negAlmacen = new Neg_Almacen();
        #endregion
        #region Entidad
        static ObservableCollection<Proveedor> prov = new ObservableCollection<Proveedor>();
        static List<Insumos> ins = new List<Insumos>();
        public FactCompra FactCompra
        {
            get => _factCompra;
            set
            {
                _factCompra = value;
            }
        }
        #endregion
        #region Objetos
        private FactCompraDet _FactCompraDet;
        private object m_dialogObject;
        private bool _openMantProveedores;
        private string operacion;
        private TipoDoc _TipoDocSelected;
        private FactCompra _CondicionPagoSelected;
        private Proveedor _ProveedorSelected;
        private Almacen _AlmacenSelected;
        private Almacen _Almacen;
        private string _serie;
        private string _DocProveedor;
        private decimal _Importe;
        private FactCompra _factCompra;
        private bool _CanUserAddRowsDataGrid;
        private int _ProductoId;
        private decimal _ImporteProducto;
        private decimal _OpGrabadas;
        private string _Total_Pagado;
        private decimal _Igv;
        private string _VisibilityCancelar;
        private string _VisibilityNuevo;
        private string _TextoPrincipal;
        private string _EstadoDoc;
        private int _CantAnulado;
        private int _CantEmitido;
        private int _CantAbastecido;
        private bool _IsEnabledComboAlmacen;
        private bool _IsCheckedAlmacen;
        public string TextoProveedoresSeleccionados { get; set; }
        public string TextoEstadosSeleccionados { get; set; }
        public string TextoTipoDocSeleccionados { get; set; }
        public int[] idProveedores = null;
        public int[] idEstadoDocs = null;
        public int[] idTipoDocs = null;
        public string ForegroundComboProveedor { get; set; } = "Black";
        public string ForegroundComboTipoDoc { get; set; } = "Black";
        public decimal CONSOLIDADO_TOTAL { get; set; } = 0;
        public int tipo { get; set; } = 1;
        public decimal CONSOLIDADO_TOTAL_ANULADO { get; set; } = 0;

        private Insumos _InsumoSeleccionado;
        //
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged_date("StartDate"); }
        }
        public void OnPropertyChanged_date(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_date;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
        public DateTime FinishDate
        {
            get { return _FinishDate; }
            set { _FinishDate = value; OnPropertyChanged_finishdate("FinishDate"); }
        }
        public void OnPropertyChanged_finishdate(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_finishdate;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
        public string numeroSerie
        {
            get { return _numeroSerie; }
            set
            {
                _numeroSerie = value;

                OnPropertyChanged_numeroSerie("numeroSerie");
            }
        }

        public void OnPropertyChanged_numeroSerie(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_numeroSerie;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
        public string DescTotal
        { get { return _descTotal; } set { _descTotal = value; OnPropertyChanged_DescTotal("DescTotal"); } }

        public void OnPropertyChanged_DescTotal(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_DescTotal;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
            var i = this.DataProductosCompra.Select(x => x.FCD_IMPORTE_ITEM).Sum();
            if (this.DescTotal == null || this.DescTotal == "") 
            {
                this.DescTotal = "0";
            }
            decimal Importe = i - Convert.ToDecimal(this.DescTotal); //importe sin igv
            var h = (decimal)0.18 * Importe;
            decimal _igv = h;
            this.Igv = _igv; //igv
            this.OpGrabadas = Importe - this.Igv; //op grabadas
            this.ImporteProducto = OpGrabadas + Igv; // importe total
            if (this.FactCompra.FC_CONDICION_PAGO == 1)
            {
                this.Total_Pagado = this.ImporteProducto.ToString();
            }
        }

        public string InsumoSelected
        { get { return _insumoselected; } set { _descTotal = value; OnPropertyChanged_InsumoSelected("InsumoSelected"); } }

        public void OnPropertyChanged_InsumoSelected(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_insumoselected;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
            this.FactCompraDet.FCD_UN_MED_INS = fcstructure.GetUniMedxIns(17);
        }
        #endregion
        #region GetSetObjetos
        public bool openMantProveedores
        {
            get => _openMantProveedores;
            set
            {
                _openMantProveedores = value;
            }
        }
        public string Operacion
        {
            get => operacion;
            set
            {
                if (value == "Lista")
                    GetLista();
                operacion = value;
            }
        }

        public object DialogObject
        {
            get { return m_dialogObject; }
            set
            {
                if (m_dialogObject == value) return;
                m_dialogObject = value;
            }
        }
        public string Serie
        {
            get => _serie;
            set
            {
                _serie = value;
                if (!IsAlphaNumeric(_serie))
                {
                    string stringAux = Regex.Replace(_serie, @"\W", "");
                    this._serie = stringAux;
                }
            }
        }
        public string DocProveedor
        {
            get => _DocProveedor;
            set
            {
                _DocProveedor = value;
            }
        }
        public decimal Importe
        {
            get => _Importe;
            set
            {
                _Importe = value;
            }
        }
        public bool CanUserAddRowsDataGrid
        {
            get => _CanUserAddRowsDataGrid;
            set
            {
                _CanUserAddRowsDataGrid = value;
            }
        }
        public int ProductoId
        {
            get => _ProductoId;
            set
            {
                _ProductoId = value;
            }
        }
        public decimal ImporteProducto
        {
            get => _ImporteProducto;
            set { _ImporteProducto = value; }
        }
        public decimal OpGrabadas
        {
            get => _OpGrabadas;
            set { _OpGrabadas = value; }
        }
        public string Total_Pagado
        {
            get => _Total_Pagado;
            set { _Total_Pagado = value; }
        }

        public decimal Igv
        {
            get => _Igv;
            set { _Igv = value; }
        }
        public string VisibilityCancelar
        {
            get => _VisibilityCancelar;
            set { _VisibilityCancelar = value; }
        }
        public string VisibilityNuevo
        {
            get => _VisibilityNuevo;
            set { _VisibilityNuevo = value; }
        }
        public string TextoPrincipal
        {
            get => _TextoPrincipal;
            set { _TextoPrincipal = value; }
        }
        public string EstadoDoc
        {
            get => _EstadoDoc;
            set { _EstadoDoc = value; }
        }

        public int CantEmitido
        {
            get => _CantEmitido;
            set { _CantEmitido = value; }
        }
        public int CantAbastecido
        {
            get => _CantAbastecido;
            set { _CantAbastecido = value; }
        }
        public int CantAnulado
        {
            get => _CantAnulado;
            set { _CantAnulado = value; }
        }

        public bool IsEnabledComboAlmacen
        {
            get => _IsEnabledComboAlmacen;
            set 
            {
                _IsEnabledComboAlmacen = value;
            }
        }
        public bool IsCheckedAlmacen
        {
            get => _IsCheckedAlmacen;
            set 
            {
                _IsCheckedAlmacen = value;
            }
        }


        #endregion
        #region SeleccionObjetos
        public TipoDoc TipoDocSelected
        {
            get => _TipoDocSelected;
            set
            {
                if (value != null)
                {
                    this.Serie = fcstructure.GetSerie(((TipoDoc)value).ID);
                    this.FactCompra.FC_TIP_DOC = ((TipoDoc)value).ID;
                }
                _TipoDocSelected = value;
            }
        }
        public FactCompra CondicionPagoSelected
        {
            get => _CondicionPagoSelected;
            set
            {
                if (value != null)
                {
                    this.FactCompra.FC_CONDICION_PAGO = ((FactCompra)value).CONDICION_ID;
                    if (((FactCompra)value).CONDICION_ID == 2)
                    {
                        this.Total_Pagado = ImporteProducto.ToString();
                    }
                }
                _CondicionPagoSelected = value;
            }
        }

        public Proveedor ProveedorSelected
        {
            get => _ProveedorSelected;
            set
            {
                if (value != null)
                {
                    DocProveedor = fcstructure.GetDocProveedor(((Proveedor)value).idp);
                    int i = Convert.ToInt32(((Proveedor)value).idp);
                    this.FactCompra.FC_ID_PROVEEDOR = i;
                }
                _ProveedorSelected = value;
            }
        }
        public Almacen AlmacenSelected
        {
            get => _AlmacenSelected;
            set
            {
                if (value != null)
                {
                    List<Insumos> _data = new List<Insumos>();
                    _data = fcstructure.GetInsumos_x_Almacen(((Almacen)value).idalm);
                    ObservableCollection<Insumos> _dataInsumos = new ObservableCollection<Insumos>(_data);
                    DataInsumos = _dataInsumos;
                    FactCompraDet.FCD_ID_ALM = ((Almacen)value).idalm;
                    IsEnabledComboAlmacen = false;
                    IsCheckedAlmacen = true;
                }
                _AlmacenSelected = value;
            }
        }
        //public Insumos InsumoSeleccionado
        //{
            
        //    get { return _InsumoSeleccionado; }
        //    set
        //    {
        //        _InsumoSeleccionado = value;

        //        FactCompraDet.FCD_UN_MED_INS = fcstructure.GetUniMedxIns(((Insumos)value).idins);
        //        OnPropertyChanged("InsumoSeleccionado");
        //    }
        //}

        public FactCompraDet FactCompraDet
        {
            get => _FactCompraDet;
            set
            {
                _FactCompraDet = value;
            }

        }
        public Almacen Almacen
        {
            get => _Almacen;
            set
            {
                _Almacen = value;
            }

        }

        #endregion
        #region Listas
        public List<TipoDoc> DataComboTipoDocumento { get; set; }
        public List<Proveedor> DataProveedores { get; set; }
        public List<FactCompra> DataCondicionPago { get; set; }
        public List<FactCompra> DataConsolidadoFactCompras { get; set; }

        public ObservableCollection<Almacen> DataAlmacen { get; set; }
        public List<FactCompra> DataFactCompra { get; set; }
        public List<FactCompra> DataFactCompraEstado { get; set; }
        public List<FactCompra> DataEstadoDocumento { get; set; }
        public List<FactCompra> DataFactCompraTipoDoc { get; set; }
        public List<FactCompra> DataTipoDocDocumento { get; set; }

        //public List<Insumos> DataInsumos { get; set; }
        private ObservableCollection<FactCompraDet> dataProductosCompra;
        private ObservableCollection<Insumos> _DataInsumos;

        private ObservableCollection<FactCompra> _ProductosCompra;

        #endregion
        #region Commands
        public ICommand CargarMantProveedoresCommand { get; set; }
        public ICommand CerrarDialogCommand { get; set; }
        public ICommand CargarInsumosCommand { get; set; }
        public ICommand AgregarFilaDataGridCommand { get; set; }
        public ICommand GuardarDocCompraCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand GenerarPdfCommand { get; set; }
        public ICommand verDetalle { get; set; }
        public ICommand verDetalleGlobal { get; set; }
        public ICommand AnularFactCompraCommand { get; set; }
        public ICommand CargarCompraCommand { get; set; }
        public ICommand ComboAlmacenCheckedCommand { get; set; }
        public ICommand ProveedorSelectedCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand EstadoDocSelectedCommand { get; set; }
        public ICommand TipoDocSelectedCommand { get; set; }
        public ICommand BuscarCommand { get; set; }


        #endregion
        #region INotifyPropertyChanged Impl
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));

        }

        private DateTime _startDate = DateTime.Now;
        public event PropertyChangedEventHandler PropertyChanged_date;
        private DateTime _FinishDate = DateTime.Now;
        public event PropertyChangedEventHandler PropertyChanged_finishdate;
        private string _numeroSerie;
        public event PropertyChangedEventHandler PropertyChanged_numeroSerie;
        private string _descTotal;
        public event PropertyChangedEventHandler PropertyChanged_DescTotal;
        private string _insumoselected;
        public event PropertyChangedEventHandler PropertyChanged_insumoselected;
        public ObservableCollection<FactCompra> ProductosCompra
        {
            get { return _ProductosCompra; }
            private set
            {
                if (value == _ProductosCompra)
                    return;
                _ProductosCompra = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FactCompraDet> DataProductosCompra
        {
            get { return dataProductosCompra; }
            private set
            {
                if (value == dataProductosCompra)
                    return;
                dataProductosCompra = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Insumos> DataInsumos
        {
            get { return _DataInsumos; }
            private set
            {
                if (value == _DataInsumos)
                    return;
                _DataInsumos = value;
                OnPropertyChanged();
            }
        }


        private void dataProductosCompra_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (INotifyPropertyChanged item in e.NewItems.OfType<INotifyPropertyChanged>())
                {
                    item.PropertyChanged += person_PropertyChanged;
                }
            }
            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (INotifyPropertyChanged item in e.OldItems.OfType<INotifyPropertyChanged>())
                {
                    item.PropertyChanged -= person_PropertyChanged;
                }
            }
        }
        private void person_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var row = sender as FactCompraDet;
            row.FCD_IMPORTE_ITEM = row.FCD_CANT_ITEM * row.FCD_PRE_UNI - row.FCD_DESC_INS;
            var c = (decimal)0.18 * row.FCD_IMPORTE_ITEM;
            row.FCD_IMPORTE_IGV_INS = c;
            var i = this.DataProductosCompra.Select(x => x.FCD_IMPORTE_ITEM).Sum();

            decimal Importe = i - Convert.ToDecimal(this.DescTotal); //importe sin igv
            var h = (decimal)0.18 * Importe;
            decimal _igv = h;
            this.Igv = _igv; //igv
            this.OpGrabadas = Importe - this.Igv; //op grabadas
            this.ImporteProducto = OpGrabadas + Igv; // importe total
            if (this.FactCompra.FC_CONDICION_PAGO == 2)
            {
                this.Total_Pagado = this.ImporteProducto.ToString();
            }
            row.FCD_UN_MED_INS = fcstructure.GetUniMedxIns(row.FCD_ID_INS);

            if (DataProductosCompra.Count() == 0) {
                this.Igv = 0;
                this.OpGrabadas = 0;
                this.ImporteProducto = 0;
                this.Total_Pagado = "0";
            }

            SaveData(row);
        }
        private void SaveData(FactCompraDet row) { }
        #endregion
        #region
        private string _CantidadItem;
        public event PropertyChangedEventHandler PropertyChanged_CantidadItem;
        public string CantidadItem
        {
            get { return _CantidadItem; }
            set
            {
                int temp = 0;
                if (value.ToString() == "")
                { _CantidadItem = "0"; }
                else { if (int.TryParse(value.ToString(), out temp)) { _CantidadItem = value; } }

                OnPropertyChanged_PorcentajeUtilidad("CantidadItem");
            }
        }

        public void OnPropertyChanged_PorcentajeUtilidad(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_CantidadItem;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
            

        }
        #endregion
        
        public FactCompraViewModel()
        {
            this.CargarMantProveedoresCommand = new RelayCommand(new Action(CargarMantProveedores));
            this.CerrarDialogCommand = new RelayCommand(new Action(CerrarDialog));
            this.CargarInsumosCommand = new RelayCommand(new Action(CargarInsumos));
            this.AgregarFilaDataGridCommand = new RelayCommand(new Action(AgregarFilaDataGrid));
            this.GuardarDocCompraCommand = new RelayCommand(new Action(GuardarDocCompra));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.GenerarPdfCommand = new ParamCommand(new Action<object>(GenerarPdf));
            this.verDetalle = new ParamCommand(new Action<object>(DialogDetFact));
            this.verDetalleGlobal = new RelayCommand(new Action(DialogDetFactTotal));
            this.AnularFactCompraCommand = new ParamCommand(new Action<object>(AnularFactCompra));
            this.CargarCompraCommand = new ParamCommand(new Action<object>(CargarCompra));
            this.ComboAlmacenCheckedCommand = new RelayCommand(new Action(ComboAlmacenChecked));
            this.ProveedorSelectedCommand = new ParamCommand(new Action<object>(AlmacenProveedorC));
            this.EstadoDocSelectedCommand = new ParamCommand(new Action<object>(EstadoDocSelectedC));
            this.TipoDocSelectedCommand = new ParamCommand(new Action<object>(TipoDocSelectedC));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));

            this.FactCompra = new FactCompra();
            this.FactCompraDet = new FactCompraDet();
            this.Almacen = new Almacen();

            //Llenar al principio una fila el datagrid para que el usuario pueda empezar a llenar datos
            dataProductosCompra = new ObservableCollection<FactCompraDet>();
            dataProductosCompra.CollectionChanged += dataProductosCompra_CollectionChanged;
            dataProductosCompra.Add(new FactCompraDet() { ID = 0, FCD_CANT_ITEM = 0, FCD_DESCR_INS = "", FCD_UN_MED_INS = "", FCD_PRE_UNI = 0, FCD_IMPORTE_ITEM = 0 });
            this.Operacion = "Lista";
        }
        private void ExportarPDF()
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            saveFileDialog1.FileName = "Factura de Compra " + DateTime.Now.ToString("yyyyMMddHHmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("FECHA EMISION");
            dt.Columns.Add("FECHA VENCIMIENTO");
            dt.Columns.Add("SERIE");
            dt.Columns.Add("PROVEEDOR");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("TOTAL");
            dt.Columns.Add("SALDO POR PAGAR");

            foreach (var p in DataFactCompra)
            {
                dt.Rows.Add(new object[] { p.ID, p.FC_F_EMISION, p.FC_VENCIMIENTO, p.FC_SER_NRO, p.P_NOM, p.FC_NOMBRE_ESTADO, p.FC_IMPORTE_TOTAL_COMPRA, p.FC_SALDO_X_PAGAR });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "Factura de Compra");
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
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            saveFileDialog1.FileName = "Factura de Compra " + DateTime.Now.ToString("yyyyMMddHHmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("FECHA EMISION");
            dt.Columns.Add("FECHA VENCIMIENTO");
            dt.Columns.Add("SERIE");
            dt.Columns.Add("PROVEEDOR");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("TOTAL");
            dt.Columns.Add("SALDO POR PAGAR");

            foreach (var p in DataFactCompra)
            {
                dt.Rows.Add(new object[] { p.ID, p.FC_F_EMISION, p.FC_VENCIMIENTO, p.FC_SER_NRO, p.P_NOM, p.FC_NOMBRE_ESTADO, p.FC_IMPORTE_TOTAL_COMPRA, p.FC_SALDO_X_PAGAR });
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
        private async void DialogDetFact(object id)
        {
            List<FactCompraDet> _data = new List<FactCompraDet>();
            _data = fcstructure.GetFactCompraDetxFC(Convert.ToInt32(id));
            ObservableCollection<FactCompraDet> _dataProductosCompra = new ObservableCollection<FactCompraDet>(_data);
            dataProductosCompra = _dataProductosCompra;
            System.Windows.Application.Current.Properties["detFactCompra"] = dataProductosCompra;
            var SiNo = new DialogDetFactCompra
            {

            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
        }
        private async void DialogDetFactTotal()
        {
            List<FactCompraDet> _data = new List<FactCompraDet>();
            _data = fcstructure.GetFactCompraDetxFCTotal(2, StartDate.Date, FinishDate.Date);
            if (idProveedores == null || idEstadoDocs == null || idTipoDocs == null)
            {
                return;
            }
            ObservableCollection<FactCompraDet> _dataProductosCompra = new ObservableCollection<FactCompraDet>(_data.GroupBy(l => new
            {
                l.ID,
                l.INS_NOM,
                l.FC_ID_PROVEEDOR,
                l.FC_ESTADO_DOC,
                l.FC_TIP_DOC,
                l.P_NOM
            }).Select(cl => new Capa_Entidades.Models.Factura_Compra.FactCompraDet
            {
                ID = cl.First().ID,
                INS_NOM = cl.First().INS_NOM,
                FCD_CANT_ITEM = cl.Sum(c => c.FCD_CANT_ITEM),
                FCD_IMPORTE_ITEM = cl.Sum(c => c.FCD_IMPORTE_ITEM),
                FC_ID_PROVEEDOR = cl.First().FC_ID_PROVEEDOR,
                FC_ESTADO_DOC = cl.First().FC_ESTADO_DOC,
                FC_TIP_DOC = cl.First().FC_TIP_DOC,
                P_NOM = cl.First().P_NOM
            }).Where(w =>
                    idProveedores.Contains(w.FC_ID_PROVEEDOR) &&
                    idEstadoDocs.Contains(w.FC_ESTADO_DOC) &&
                    idTipoDocs.Contains(w.FC_TIP_DOC)).OrderBy(o => o.INS_NOM).ToList());

            dataProductosCompra = _dataProductosCompra;
            System.Windows.Application.Current.Properties["detFactCompra"] = dataProductosCompra;
            var SiNo = new DialogDetFactCompraTotal
            {

            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
        }
        private void Buscar()
        {
            if (idProveedores == null || idEstadoDocs == null)
            {
                return;
            }
            List<FactCompra> fc = fcstructure.GetFactCompra();
            this.DataFactCompra = fc
                .Where(w => 
                    w.FC_F_EMISION.Date >= StartDate.Date && 
                    w.FC_F_EMISION.Date <= FinishDate.Date && 
                    idProveedores.Contains(w.FC_ID_PROVEEDOR) && 
                    idEstadoDocs.Contains(w.FC_ESTADO_DOC) &&
                    idTipoDocs.Contains(w.FC_TIP_DOC))
                .ToList();
            tipo = 2;
            this.CONSOLIDADO_TOTAL = this.DataFactCompra.Where(w => w.FC_ESTADO_DOC != 0).Sum(w => w.FC_IMPORTE_TOTAL_COMPRA);
            this.CONSOLIDADO_TOTAL_ANULADO = this.DataFactCompra.Where(w => w.FC_ESTADO_DOC == 0).Sum(w => w.FC_IMPORTE_TOTAL_COMPRA);
        }
        public void AlmacenProveedorC(object id)
        {
            int _id = (int)id;
            bool ischeck = this.DataProveedores.Where(w => w.idp == _id).FirstOrDefault().ischeck;
            if (ischeck == false) { this.DataProveedores.Where(w => w.idp == _id).FirstOrDefault().ischeck = false; }
            else { this.DataProveedores.Where(w => w.idp == _id).FirstOrDefault().ischeck = true; }
            string[] texto = this.DataProveedores.Where(w => w.ischeck == true).Select(s => s.prov_nom).ToArray();
            ForegroundComboProveedor = "Black";
            TextoProveedoresSeleccionados = texto.Count().ToString();
            TextoProveedoresSeleccionados = TextoProveedoresSeleccionados + " Seleccionado(s)";
            idProveedores = DataProveedores.Where(w => w.ischeck == true).Select(s => s.idp).ToArray();
        }
        public void EstadoDocSelectedC(object id)
        {
            int _id = (int)id;
            bool ischeck = this.DataEstadoDocumento.Where(w => w.FC_ESTADO_DOC == _id).FirstOrDefault().ischeckEstadoDoc;
            if (ischeck == false) { this.DataEstadoDocumento.Where(w => w.FC_ESTADO_DOC == _id).FirstOrDefault().ischeckEstadoDoc = false; }
            else { this.DataEstadoDocumento.Where(w => w.FC_ESTADO_DOC == _id).FirstOrDefault().ischeckEstadoDoc = true; }
            string[] texto = this.DataEstadoDocumento.Where(w => w.ischeckEstadoDoc == true).Select(s => s.FC_NOMBRE_ESTADO).ToArray();
            ForegroundComboProveedor = "Black";
            TextoEstadosSeleccionados = texto.Count().ToString();
            TextoEstadosSeleccionados = TextoEstadosSeleccionados + " Seleccionado(s)";
            idEstadoDocs = DataEstadoDocumento.Where(w => w.ischeckEstadoDoc == true).Select(s => s.FC_ESTADO_DOC).ToArray();
        }
        public void TipoDocSelectedC(object id)
        {
            int _id = (int)id;
            bool ischeck = this.DataTipoDocDocumento.Where(w => w.FC_TIP_DOC == _id).FirstOrDefault().ischeckTipoDoc;
            if (ischeck == false) { this.DataTipoDocDocumento.Where(w => w.FC_TIP_DOC == _id).FirstOrDefault().ischeckTipoDoc = false; }
            else { this.DataTipoDocDocumento.Where(w => w.FC_TIP_DOC == _id).FirstOrDefault().ischeckTipoDoc = true; }
            string[] texto = this.DataTipoDocDocumento.Where(w => w.ischeckTipoDoc == true).Select(s => s.FC_NOMBRE_TIP_DOC).ToArray();
            ForegroundComboTipoDoc = "Black";
            TextoTipoDocSeleccionados = texto.Count().ToString();
            TextoTipoDocSeleccionados = TextoTipoDocSeleccionados + " Seleccionado(s)";
            idTipoDocs = DataTipoDocDocumento.Where(w => w.ischeckTipoDoc == true).Select(s => s.FC_TIP_DOC).ToArray();
        }
        #region voids
        public async void CargarMantProveedores()
        {
            this.openMantProveedores = true;
            System.Windows.Application.Current.Properties["OperacionxFactCompra"] = "Nuevo";
            DialogObject = new Views.Configuracion.Proveedor();
            this.DataProveedores = fcstructure.GetProveedor();
            System.Windows.Application.Current.Properties["OperacionxFactCompra"] = null;
        }
        public void CerrarDialog()
        {
            this.openMantProveedores = false;
            System.Windows.Application.Current.Properties["OperacionxFactCompra"] = null;
            this.DataProveedores = fcstructure.GetProveedor();
            System.Windows.Application.Current.Properties["OperacionxFactCompra"] = null;
        }
        public async void CargarInsumos()
        {
            this.openMantProveedores = true;
            System.Windows.Application.Current.Properties["OperacionxFactCompra"] = "Nuevo";
            DialogObject = new Views.Stock.Insumos();
            System.Windows.Application.Current.Properties["OperacionxFactCompra"] = null;
            List<Insumos> _data = new List<Insumos>();
            _data = fcstructure.GetInsumos_x_Almacen(FactCompraDet.FCD_ID_ALM);
            ObservableCollection<Insumos> _dataInsumos = new ObservableCollection<Insumos>(_data);
            DataInsumos = _dataInsumos;
        }
        public void AgregarFilaDataGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("CANTIDADPROD");
            dt.Columns.Add("NOMBREINSU");
            dt.Columns.Add("UNIMED");
            dt.Columns.Add("PRECIOUNI");
            dt.Columns.Add("DESC");
            dt.Columns.Add("IMPORTEPROD");

            var c = DataProductosCompra.Count();
            int id = c + 1;

            DataRow row = dt.NewRow();
            row["ID"] = id;
            row["CANTIDADPROD"] = 0;
            row["NOMBREINSU"] = "";
            row["UNIMED"] = "";
            row["PRECIOUNI"] = 0;
            row["DESC"] = 0;
            row["IMPORTEPROD"] = 0;
            dt.Rows.Add(row);

            ObservableCollection<FactCompraDet> fact = new ObservableCollection<FactCompraDet>();
            for (int i = 0; i < dt.Rows.Count; i++) //para que solo me traiga una fila vacia
            {
                FactCompraDet f = new FactCompraDet();
                f.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                f.FCD_CANT_ITEM = Convert.ToInt32(dt.Rows[i]["CANTIDADPROD"]);
                f.FCD_DESCR_INS = dt.Rows[i]["NOMBREINSU"].ToString();
                f.FCD_UN_MED_INS = dt.Rows[i]["UNIMED"].ToString();
                f.FCD_PRE_UNI = Convert.ToDecimal(dt.Rows[i]["PRECIOUNI"]);
                f.FCD_DESC_INS = Convert.ToDecimal(dt.Rows[i]["DESC"]);
                f.FCD_IMPORTE_ITEM = Convert.ToDecimal(dt.Rows[i]["IMPORTEPROD"]);
                fact.Add(f);
            }
            var h = fact.FirstOrDefault();
            DataProductosCompra.Add(h);
        }
        public void Nuevo()
        {
            this.Operacion = "Nuevo";
            this.VisibilityCancelar = "Visible";
            this.VisibilityNuevo = "Hidden";
            this.TextoPrincipal = "NUEVA COMPRA";
        }
        private void Cancelar()
        {
            this.Operacion = "Lista";
            this.VisibilityCancelar = "Hidden";
            this.VisibilityNuevo = "Visible";
            this.FactCompra = new FactCompra();
            this.FactCompraDet = new FactCompraDet();
        }
        public void llenardt(DataTable dt)
        {
            dt.Columns.Add("FCD_ORDEN_ITEM");
            dt.Columns.Add("FCD_ID_INS");
            dt.Columns.Add("FCD_ID_ALM");
            dt.Columns.Add("FCD_DESCR_INS");
            dt.Columns.Add("FCD_UN_MED_INS");
            dt.Columns.Add("FCD_CANT_ITEM");
            dt.Columns.Add("FCD_VALOR_UNITARIO_SIN_IGV");
            dt.Columns.Add("FCD_VALOR_UNITARIO_CON_IGV");
            dt.Columns.Add("FCD_IMPORTE_IGV_INS");
            dt.Columns.Add("FCD_COD_AFECTACION_IGV_ITEM");
            dt.Columns.Add("FCD_DESC_INS");
            dt.Columns.Add("FCD_VALOR_COMPRA_ITEM");
            dt.Columns.Add("FCD_PRE_UNI");
            dt.Columns.Add("FCD_IMPORTE_ITEM");
            dt.Columns.Add("FCD_VALOR_COMPRA_SIN_IGV");
            DataRow row = dt.NewRow();
            row["FCD_ORDEN_ITEM"] = 0;
            row["FCD_ID_INS"] = 0;
            row["FCD_ID_ALM"] = 0;
            row["FCD_DESCR_INS"] = "";
            row["FCD_UN_MED_INS"] = "";
            row["FCD_CANT_ITEM"] = 0;
            row["FCD_VALOR_UNITARIO_SIN_IGV"] = 0;
            row["FCD_VALOR_UNITARIO_CON_IGV"] = 0;
            row["FCD_IMPORTE_IGV_INS"] = 0;
            row["FCD_COD_AFECTACION_IGV_ITEM"] = 0;
            row["FCD_DESC_INS"] = 0;
            row["FCD_VALOR_COMPRA_ITEM"] = 0;
            row["FCD_PRE_UNI"] = 0;
            row["FCD_IMPORTE_ITEM"] = 0;
            row["FCD_VALOR_COMPRA_SIN_IGV"] = 0;
            dt.Rows.Add(row);
        }
        public void ComboAlmacenChecked()
        {
            if (IsCheckedAlmacen == false)
            {
                IsEnabledComboAlmacen = true;
                DataProductosCompra.Clear();
                this.DescTotal = "";
                this.OpGrabadas = 0;
                this.Igv = 0;
                this.ImporteProducto = 0;
                this.Total_Pagado = "";
            }
        }
        #endregion
        #region procesos
        public void GenerarPdf2(object id)
        {

            List<FactCompra> _dataFactCompra = new List<FactCompra>();
            _dataFactCompra = fcstructure.GetFactCompraxId(Convert.ToInt32(id));

            string serie = _dataFactCompra.FirstOrDefault().FC_SER_NRO;
            DateTime fec_emision = _dataFactCompra.FirstOrDefault().FC_F_EMISION;

            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            PdfFont fontTitulo = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            PdfFont fontParrafo = new PdfStandardFont(PdfFontFamily.Helvetica, 15);
            PdfFont fontTable = new PdfStandardFont(PdfFontFamily.Helvetica, 15);

            PdfTextElement textElement = new PdfTextElement("FACTURA DE COMPRA", fontTitulo, PdfBrushes.Black);
            PdfLayoutResult layoutResult = textElement.Draw(page, new RectangleF(0, 0, page.GetClientSize().Width, page.GetClientSize().Height));

            graphics.DrawString("Numero: " + serie, fontTitulo, PdfBrushes.Black, new PointF(0, 30));
            graphics.DrawString("Fecha de Emision: " + fec_emision, fontParrafo, PdfBrushes.Black, new PointF(0, 60));

            PdfLine line = new PdfLine(new PointF(0, 0), new PointF(page.GetClientSize().Width, 0)) { Pen = PdfPens.DarkGray };

            layoutResult = line.Draw(page, new PointF(0, layoutResult.Bounds.Bottom + 5));
            PdfGrid grid = new PdfGrid();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ORDEN ITEM");
            dataTable.Columns.Add("CANTIDAD");
            dataTable.Columns.Add("COD INS");
            dataTable.Columns.Add("INSUMO");
            dataTable.Columns.Add("UNI.MED");
            dataTable.Columns.Add("PRE.UNI");
            dataTable.Columns.Add("DESC");
            dataTable.Columns.Add("IMPORTE");

            List<FactCompraDet> _data = new List<FactCompraDet>();
            _data = fcstructure.GetFactCompraDetxFC(Convert.ToInt32(id));
            ObservableCollection<FactCompraDet> _dataProductosCompra = new ObservableCollection<FactCompraDet>(_data);
            dataProductosCompra = _dataProductosCompra;
            foreach (var p in dataProductosCompra)
            {
                dataTable.Rows.Add(new object[] { p.ID, p.FCD_CANT_ITEM, p.FCD_ID_INS, p.INS_NOM, p.FCD_UN_MED_INS, p.FCD_PRE_UNI, p.FCD_DESC_INS, p.FCD_IMPORTE_ITEM });
            }
            grid.DataSource = dataTable;

            grid.Style.CellPadding.All = 5;
            grid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable1LightAccent3);
            //Draw the table in page, below the line with a height gap of 20
            grid.Draw(page, new PointF(0, layoutResult.Bounds.Bottom + 50));
            document.Save("Output.pdf");
            document.Close(true);
            System.Diagnostics.Process.Start("Output.pdf");
        }
        private void GenerarPdf(object id)
        {
            List<FactCompra> _dataFactCompra = new List<FactCompra>();
            _dataFactCompra = fcstructure.GetFactCompraxId(Convert.ToInt32(id));

            string serie = _dataFactCompra.FirstOrDefault().FC_SER_NRO;
            DateTime fec_emision = _dataFactCompra.FirstOrDefault().FC_F_EMISION;


            List<FactCompraDet> _data = new List<FactCompraDet>();
            _data = fcstructure.GetFactCompraDetxFC(Convert.ToInt32(id));
            ObservableCollection<FactCompraDet> _dataProductosCompra = new ObservableCollection<FactCompraDet>(_data);
            dataProductosCompra = _dataProductosCompra;
            
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            saveFileDialog1.FileName = "Historial_de_Venta_" + DateTime.Now.ToString("ddMMyyyy HHmmss");
            
            DataTable dt = new DataTable();
            dt.Columns.Add("ORDEN ITEM");
            dt.Columns.Add("CANTIDAD");
            dt.Columns.Add("CODIGO INS");
            dt.Columns.Add("INSUMO");
            dt.Columns.Add("UNI. MED");
            dt.Columns.Add("PRECIO UNI.");
            dt.Columns.Add("DESCUENTO");
            dt.Columns.Add("IMPORTE");
            foreach (var p in dataProductosCompra)
            {
                dt.Rows.Add(new object[] { p.ID, p.FCD_CANT_ITEM, p.FCD_ID_INS, p.INS_NOM, p.FCD_UN_MED_INS, p.FCD_PRE_UNI, p.FCD_DESC_INS, p.FCD_IMPORTE_ITEM });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "DOCUMENTO DE COMPRA");
                    }
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }


        //private void ExportarExcel()
        //{
        //    Stream myStream;
        //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

        //    saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
        //    saveFileDialog1.FilterIndex = 2;
        //    saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

        //    saveFileDialog1.RestoreDirectory = true;
        //    saveFileDialog1.Title = "Exportar Archivo a Excel";
        //    if (this.Idradiobuton == "1")
        //    {
        //        saveFileDialog1.FileName = "Historial_de_Venta_" + fecha1;
        //    }
        //    else if (this.Idradiobuton == "2")
        //    {
        //        saveFileDialog1.FileName = "Historial_de_Venta_" + fecha2;
        //    }
        //    else
        //    {
        //        saveFileDialog1.FileName = "Historial_de_Venta_" + fecha3 + "_" + fecha4;
        //    }

        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("ID");
        //    dt.Columns.Add("N° DIARIO");
        //    dt.Columns.Add("MESA");
        //    dt.Columns.Add("SUB T.");
        //    dt.Columns.Add("DESC");
        //    dt.Columns.Add("TOTAL");
        //    dt.Columns.Add("T. PAGO");
        //    dt.Columns.Add("F. PEDIDO");
        //    dt.Columns.Add("EMPLEADO");
        //    dt.Columns.Add("USUARIO");
        //    dt.Columns.Add("F. CIERRE");
        //    dt.Columns.Add("ID CIERRE");
        //    foreach (var p in DataHistVentas)
        //    {
        //        dt.Rows.Add(new object[] { p.id_ped, p.num_dia_ped, p.nom_mesa, p.subtotal_ped, p.desc_ped, p.total_ped, p.nom_fpago, p.f_ped, p.nom_emple, p.nom_usu, p.fech_dia_cerre, p.id_dia_cierre });
        //    }
        //    if (dt.Rows.Count > 0)
        //    {
        //        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        //        {
        //            if ((myStream = saveFileDialog1.OpenFile()) != null)
        //            {
        //                string direccion = saveFileDialog1.InitialDirectory;
        //                string nombrearchivo = saveFileDialog1.FileName;
        //                // Code to write the stream goes here.

        //                string ubicacion = saveFileDialog1.FileName;
        //                myStream.Close();
        //                CreateExcelFile.CreateExcelDocument(dt, ubicacion);
        //                //DialogHost.CloseDialogCommand.Execute(null, null);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
        //    }
        //}
        public void GuardarDocCompra()
        {
            if (Serie == null || numeroSerie == null || FactCompra.FC_ID_PROVEEDOR <= 0 || DataProductosCompra.Count == 0 || FactCompra.FC_CONDICION_PAGO < 0)
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = "Completar todos los campos, porfavor" }
                };
                DialogHost.Show(view, "RootDialog");
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("FCD_ORDEN_ITEM");
            dt.Columns.Add("FCD_ID_INS");
            dt.Columns.Add("FCD_ID_ALM");
            dt.Columns.Add("FCD_DESCR_INS");
            dt.Columns.Add("FCD_UN_MED_INS");
            dt.Columns.Add("FCD_CANT_ITEM");
            dt.Columns.Add("FCD_VALOR_UNITARIO_SIN_IGV");
            dt.Columns.Add("FCD_VALOR_UNITARIO_CON_IGV");
            dt.Columns.Add("FCD_IMPORTE_IGV_INS");
            dt.Columns.Add("FCD_COD_AFECTACION_IGV_ITEM");
            dt.Columns.Add("FCD_DESC_INS");
            dt.Columns.Add("FCD_VALOR_COMPRA_ITEM");
            dt.Columns.Add("FCD_PRE_UNI");
            dt.Columns.Add("FCD_IMPORTE_ITEM");
            dt.Columns.Add("FCD_VALOR_COMPRA_SIN_IGV");

            var c = DataProductosCompra.Count();
            int id = c + 1;
            int ii = 1;
            foreach (var i in DataProductosCompra.OrderBy(o=> o.FCD_ID_INS))
            {
                DataRow row = dt.NewRow();
                row["FCD_ORDEN_ITEM"] = ii;
                row["FCD_ID_INS"] = i.FCD_ID_INS;
                row["FCD_ID_ALM"] = FactCompraDet.FCD_ID_ALM;
                row["FCD_DESCR_INS"] = i.FCD_DESCR_INS;
                row["FCD_UN_MED_INS"] = i.FCD_UN_MED_INS;
                row["FCD_CANT_ITEM"] = i.FCD_CANT_ITEM;
                row["FCD_VALOR_UNITARIO_SIN_IGV"] = i.FCD_VALOR_UNITARIO_SIN_IGV;
                row["FCD_VALOR_UNITARIO_CON_IGV"] = i.FCD_VALOR_UNITARIO_CON_IGV;
                row["FCD_IMPORTE_IGV_INS"] = i.FCD_IMPORTE_IGV_INS;
                row["FCD_COD_AFECTACION_IGV_ITEM"] = i.FCD_COD_AFECTACION_IGV_ITEM;
                row["FCD_DESC_INS"] = i.FCD_DESC_INS;
                row["FCD_VALOR_COMPRA_ITEM"] = i.FCD_VALOR_COMPRA_ITEM;
                row["FCD_PRE_UNI"] = i.FCD_PRE_UNI;
                row["FCD_IMPORTE_ITEM"] = i.FCD_IMPORTE_ITEM;
                row["FCD_VALOR_COMPRA_SIN_IGV"] = i.FCD_VALOR_COMPRA_SIN_IGV;
                dt.Rows.Add(row);
                ii = ii + 1;
            }
            int _id = 0;
            string _mensaje = " ";
            FactCompra.FC_F_EMISION = StartDate;
            FactCompra.FC_VENCIMIENTO = FinishDate;
            FactCompra.FC_SER_NRO = Serie + '-' + numeroSerie;
            FactCompra.FC_TOTAL_DESC = Convert.ToDecimal(this.DescTotal);
            FactCompra.FC_TOTAL_OP_GRABADAS = this.OpGrabadas;
            FactCompra.FC_SUMA_IGV = this.Igv;
            FactCompra.FC_IMPORTE_TOTAL_COMPRA = this.ImporteProducto;
            if (this.Total_Pagado == "" || this.Total_Pagado == null) 
            {
                this.Total_Pagado = "0";
            }
            FactCompra.FC_TOTAL_PAGADO = Convert.ToDecimal(this.Total_Pagado);
            

            bool res = negFact.SetFacturaCompra((_id == 0 ? 1 : 2), FactCompra, dt, ref _mensaje);
            if (res)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Registro insertado correctamente", 1);
                this.Operacion = "Lista";
                GetLista();
            }
        }
        public async void CargarCompra(object id)
        {
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "Esta seguro de ingresar los productos a su almacen?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            int id_FactCompra = Convert.ToInt32(id);
            string _mensaje = "";
            if (!(bool)x)
            {
                return;
            }
            else
            {
                int id_usuario = Convert.ToInt32(System.Windows.Application.Current.Properties["IdUsuario"]);
                bool res = negFact.SetProductos(1, id_FactCompra,id_usuario, ref _mensaje);
                if (res) { GetLista(); }
            }
        }

        public async void AnularFactCompra(object id)
        {

            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "Desea anular la factura de compra?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
            {
                return;
            }
            else
            {
                DataTable dt = new DataTable();
                llenardt(dt);
                string _mensaje = "";
                this.FactCompra.ID = Convert.ToInt32(id);
                bool res = negFact.SetFacturaCompra(3, FactCompra, dt, ref _mensaje);
                this.FactCompra = new FactCompra();
                this.FactCompraDet = new FactCompraDet();
                GetLista();
            }
        }
        #endregion

        public void GetLista()
        {
            this.TextoPrincipal = "GESTION DE COMPRAS";
            this.VisibilityCancelar = "Hidden";
            this.VisibilityNuevo = "Visible";
            this.IsEnabledComboAlmacen = true;
            this.Serie = "";
            this.numeroSerie = "";

            this.DataProveedores = fcstructure.GetProveedor();
            idProveedores = DataProveedores.Where(w => w.ischeck == true).Select(s => s.idp).ToArray();
            TextoProveedoresSeleccionados = idProveedores.Count() + " Seleccionado(s)";
            this.DataComboTipoDocumento = fcstructure.GetTipoDocumento();
            //this.DataFactCompra = fcstructure.GetFactCompra();
            this.DataFactCompraEstado = fcstructure.GetFactCompraEstado();
            this.DataEstadoDocumento = DataFactCompraEstado.Distinct().ToList();
            idEstadoDocs = DataEstadoDocumento.Where(w => w.ischeckEstadoDoc == true).Select(s => s.FC_ESTADO_DOC).ToArray();
            TextoEstadosSeleccionados = idEstadoDocs.Count() + " Seleccionado(s)";
            this.DataFactCompraTipoDoc = fcstructure.GetFactCompraTipoDoc();
            this.DataTipoDocDocumento = DataFactCompraTipoDoc.Distinct().ToList();
            idTipoDocs = DataTipoDocDocumento.Where(w => w.ischeckTipoDoc == true).Select(s => s.FC_TIP_DOC).ToArray();
            TextoTipoDocSeleccionados = idTipoDocs.Count() + " Seleccionado(s)";
            List<FactCompraDet> _prod = new List<FactCompraDet>();
            _prod = fcstructure.GetFactCompraDet();
            DataProductosCompra = new ObservableCollection<FactCompraDet>();
            ObservableCollection<FactCompraDet> _dprodcom = new ObservableCollection<FactCompraDet>(_prod);
            this.dataProductosCompra = _dprodcom;
            DataAlmacen = negAlmacen.GetAlmacen();
            tipo = 1;
            dataProductosCompra = new ObservableCollection<FactCompraDet>();
            dataProductosCompra.CollectionChanged += dataProductosCompra_CollectionChanged;
            this.DataCondicionPago = fcstructure.GetCondicionPago();
            //this.DataConsolidadoFactCompras = fcstructure.GetCantidadFactCompra();
            Buscar();
            this.CONSOLIDADO_TOTAL = this.DataFactCompra.Where(w => w.FC_ESTADO_DOC != 0).Sum(w => w.FC_IMPORTE_TOTAL_COMPRA);
            this.CONSOLIDADO_TOTAL_ANULADO = this.DataFactCompra.Where(w => w.FC_ESTADO_DOC == 0).Sum(w => w.FC_IMPORTE_TOTAL_COMPRA);
            this.CantAbastecido = fcstructure.GetDocAbastecido();
            this.CantEmitido = fcstructure.GetDocEmitido();
            this.CantAnulado = fcstructure.GetDocAnulado();
        }

        public void reiniciarDatos()
        {
            DataProductosCompra = new ObservableCollection<FactCompraDet>();
            DataProveedores = new List<Proveedor>();
            DataComboTipoDocumento = new List<TipoDoc>();
            DataAlmacen = new ObservableCollection<Almacen>();
            DataCondicionPago = new List<FactCompra>();
            DescTotal = "0";
            OpGrabadas = 0;
            Igv = 0;
            ImporteProducto = 0;
            Total_Pagado = "0";
            numeroSerie = "";
            Serie = "";
            this.FactCompra = new FactCompra();
            this.FactCompraDet = new FactCompraDet();
            GetLista();
        }

        public bool IsAlphaNumeric(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

    }
}

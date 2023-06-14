using Capa_Entidades.Models;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Stock;
using Capa_Negocio;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Stock;
using Capa_Presentacion_WPF.Views.Dialogs;
using ExportToExcel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Stock.Consumo_Interno
{
    public class ConsumoInternoViewModel : INotifyPropertyChanged
    {
        ConsumoInternoStructure cis = new ConsumoInternoStructure();
        #region Negocio
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Almacen neg_alm = new Neg_Almacen();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        #endregion
        #region Entidad
        private SCategoria _SCatSelected;
        private Categoria _CatSelected;
        private Grupo _GrupSelected;
        private Platos _PlatoSelected;
        private Almacen _AlmacenSelected;
        private Insumos _InsumoSelected;
        private TipoConsumo _TipoConsumoSelected;
        private TipoConsumo _TipoConsumo;
        public DateTime Fecha_Desde { get; set; } = DateTime.Now;
        public DateTime Fecha_Hasta { get; set; } = DateTime.Now;
        public TipoConsumo TipoConsumo
        {
            get => _TipoConsumo;
            set { _TipoConsumo = value; }
        }
        private Insumos _insumos;
        public Insumos Insumos
        {
            get => _insumos;
            set
            {
                _insumos = value;
            }
        }
        private Almacen _Almacen;
        public Almacen Almacen
        {
            get => _Almacen;
            set
            {
                _Almacen = value;
            }
        }
        private Platos _platos;
        public Platos Platos
        {
            get => _platos;
            set
            {
                _platos = value;
            }
        }
        private Empleado _Empleado;
        public Empleado Empleado
        {
            get => _Empleado;
            set
            {
                _Empleado = value;
            }
        }
        private ConsumoInterno _ConsumoInterno;
        public ConsumoInterno ConsumoInterno
        { get => _ConsumoInterno; set { _ConsumoInterno = value; } }
        #endregion
        #region Objetos
        public string Observacion { get; set; } = "";
        public decimal Importe_Total { get; set; }
        public decimal Precio_plato { get; set; }
        public decimal Cantidad_insumo { get; set; }
        public decimal StockInsumo { get; set; }
        private string _cant_plato;
        public string Cant_plato
        {
            get => _cant_plato;
            set
            {
                int temp = 0;
                if (value.ToString() == "")
                {
                    Importe_Total = Precio_plato * 0;
                }
                else
                {
                    if (int.TryParse(value.ToString(), out temp))
                    {
                        Importe_Total = Precio_plato * Convert.ToInt32(value);
                    }
                }
                _cant_plato = value;
            }
        }
        private string _operacion;
        public string Operacion
        {
            get => _operacion;
            set
            {
                if (value == "Lista")
                {
                    GetLista();
                }
                _operacion = value;
            }
        }
        public string Operacion_tipoconsumo { get; set; }
        #endregion
        #region SeleccionCombos
        private Empleado _EmpleadoSelected;
        public Empleado EmpleadoSelected
        {
            get => _EmpleadoSelected;
            set
            {
                if (value != null)
                {
                    Empleado.id = ((Empleado)value).id;
                }
                _EmpleadoSelected = value;
            }
        }
        public TipoConsumo TipoConsumoSelected
        {
            get => _TipoConsumoSelected;
            set
            {
                if (value != null)
                {
                    if (((TipoConsumo)value).ID == 1)
                    {
                        this.Operacion_tipoconsumo = "Carta";
                    }
                    else if (((TipoConsumo)value).ID == 2)
                    {
                        this.Operacion_tipoconsumo = "Insumo";
                    }
                    TipoConsumo.ID = ((TipoConsumo)value).ID;
                }
                _TipoConsumoSelected = value;
            }
        }
        public SCategoria SCatSelected
        {
            get => _SCatSelected;
            set
            {
                if (value != null)
                {
                    this.ComboCat = new List<Categoria>();
                    ComboCat = cis.GetCategoria(((SCategoria)value).idscat);
                    this.ComboGrupo = new List<Grupo>();
                    this.ComboPlato = new List<Platos>();
                }
                _SCatSelected = value;
            }
        }
        public Categoria CatSelected
        {
            get => _CatSelected;
            set
            {
                if (value != null)
                {
                    this.ComboGrupo = new List<Grupo>();
                    ComboGrupo = negCombo.GetComboGrupoxC(((Categoria)value).idcat);
                    this.ComboPlato = new List<Platos>();
                }
                _CatSelected = value;
            }
        }
        public Grupo GrupSelected
        {
            get => _GrupSelected;
            set
            {
                if (value != null)
                {
                    this.ComboPlato = new List<Platos>();
                    ComboPlato = negCombo.GetComboPlato(((Grupo)value).idgrup);
                }
                _GrupSelected = value;
            }
        }

        public Platos PlatoSelected
        {
            get => _PlatoSelected;
            set
            {
                if (value != null)
                {
                    Platos.idplato = ((Platos)value).idplato;
                    Platos.precplato = ((Platos)value).precplato;
                    Precio_plato = Convert.ToDecimal(Platos.precplato);
                    Importe_Total = Convert.ToDecimal(Platos.precplato) * Convert.ToDecimal((Cant_plato == "") ? "0" : Cant_plato);
                }
                _PlatoSelected = value;
            }
        }
        public Almacen AlmacenSelected
        {
            get => _AlmacenSelected;
            set
            {
                if (value != null)
                {
                    DataInsumos = cis.GetInsumos();
                    Almacen.idalm = ((Almacen)value).idalm;
                    DataInsumos = DataInsumos.Where(w => w.ID_ALMA == Almacen.idalm).ToList();
                }
                _AlmacenSelected = value;
            }
        }
        public Insumos InsumoSelected
        {
            get => _InsumoSelected;
            set
            {
                if (value != null)
                {
                    Insumos.idins = ((Insumos)value).idins;
                    StockInsumo = cis.GetStockInsumo(((Insumos)value).ID);
                }
                _InsumoSelected = value;
            }
        }

        #endregion
        #region Listas
        public List<SCategoria> ComboSuperCat { get; set; }
        public List<Categoria> ComboCat { get; set; }
        public List<Grupo> ComboGrupo { get; set; }
        public List<Platos> ComboPlato { get; set; }
        public List<Insumos> DataInsumos { get; set; }
        public List<Almacen> DataAlmacen { get; set; }
        public List<Empleado> DataEmpleados { get; set; }
        public List<ConsumoInterno> DataConsumoInterno { get; set; }
        public List<TipoConsumo> DataTipoConsumo { get; set; }
        #endregion
        #region Commands
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand ExportarPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
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
        public ConsumoInternoViewModel()
        {
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ExportarPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.Operacion = "Lista";
            this.Operacion_tipoconsumo = "Ninguno";
           
        }
        private void Buscar()
        {
            DataConsumoInterno = cis.GetConsumoInternoFecha(Fecha_Desde, Fecha_Hasta);
        }
        private void Eliminar(object id) {
            ConsumoInterno.ID = Convert.ToInt32(id);
            bool res = neg_alm.SetConsumoInterno(3,ConsumoInterno,Empleado, TipoConsumo, Platos, Insumos,0, Cantidad_insumo, Almacen.idalm, Observacion);
            if (res) {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
            }
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
            saveFileDialog1.FileName = "Reporte Consumo Interno" + DateTime.Now.ToString("yyyyMMddHHmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("EMPLEADO");
            dt.Columns.Add("CANTIDAD");
            dt.Columns.Add("PLATO");
            dt.Columns.Add("INSUMO");
            dt.Columns.Add("FECHA REGISTRO");
            dt.Columns.Add("OBSERVACION");
            foreach (var p in DataConsumoInterno)
            {
                dt.Rows.Add(new object[] { p.EMPL_NOM, Math.Round(p.CI_CANT, 3), p.CAR_NOM, p.INS_NOM,p.CI_F_CREATE, p.CI_OBS});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "Reporte Consumo Interno");
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
            saveFileDialog1.FileName = "Reporte Consumo Interno" + DateTime.Now.ToString("yyyyMMddHHmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("EMPLEADO");
            dt.Columns.Add("CANTIDAD");
            dt.Columns.Add("PLATO");
            dt.Columns.Add("INSUMO");
            dt.Columns.Add("FECHA REGISTRO");
            dt.Columns.Add("OBSERVACION");
            foreach (var p in DataConsumoInterno)
            {
                dt.Rows.Add(new object[] { p.EMPL_NOM, Math.Round(p.CI_CANT,3), p.CAR_NOM, p.INS_NOM,p.CI_F_CREATE, p.CI_OBS });
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
                        CreateExcelFile.CreateExcelDocument(dt, ubicacion);
                    }
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }
        private void Guardar()
        {
            if (Operacion_tipoconsumo == "Ninguno")
            {
                var view = new MessageDialog { Mensaje = { Text = "Debe seleccionar el tipo de consumo" } };
                DialogHost.Show(view, "RootDialog");
                return;
            }
            else if (Operacion_tipoconsumo == "Carta")
            {
                if (Cant_plato == "0" || Convert.ToDecimal(Cant_plato) == 0 || Cant_plato == null)
                {
                    var view = new MessageDialog { Mensaje = { Text = "Ingrese la cantidad de platos" } };
                    DialogHost.Show(view, "RootDialog");
                    return;
                }
                else if (Platos.idplato == 0)
                {
                    var view = new MessageDialog { Mensaje = { Text = "Seleccione un plato" } };
                    DialogHost.Show(view, "RootDialog");
                }
                else
                {
                    ConsumoInterno.ID = 0;
                    bool res = neg_alm.SetConsumoInterno(1, ConsumoInterno, Empleado, TipoConsumo, Platos, Insumos, 
                        Convert.ToDecimal((Convert.ToDecimal(Cant_plato) == 0 || Cant_plato == null) ? "0" : Cant_plato), Cantidad_insumo, Almacen.idalm, Observacion);
                    if (res)
                    {
                        this.Operacion = "Lista";
                        GetLista();
                    }
                }
            }
            else if (Operacion_tipoconsumo == "Insumo")
            {
                if (Cantidad_insumo == 0)
                {
                    var view = new MessageDialog { Mensaje = { Text = "Ingrese la cantidad de insumo" } };
                    DialogHost.Show(view, "RootDialog");
                    return;
                }
                else if (Insumos.idins == 0)
                {
                    var view = new MessageDialog { Mensaje = { Text = "Seleccione un insumo" } };
                    DialogHost.Show(view, "RootDialog");
                }
                else
                {
                    ConsumoInterno.ID = 0;
                    bool res = neg_alm.SetConsumoInterno(2, ConsumoInterno, Empleado,
                        TipoConsumo, Platos, Insumos,
                        Convert.ToDecimal((Cant_plato == "0" || Cant_plato == null) ? "0" : Cant_plato),
                        Cantidad_insumo, Almacen.idalm, Observacion);
                    if (res)
                    {
                        this.Operacion = "Lista";
                        GetLista();
                    }
                }
            }
        }
        private void Nuevo()
        {
            this.ComboCat = new List<Categoria>();
            this.ComboGrupo = new List<Grupo>();
            this.ComboPlato = new List<Platos>();
            this.DataEmpleados = new List<Empleado>();
            this.DataTipoConsumo = new List<TipoConsumo>();
            this.DataInsumos = new List<Insumos>();
            this.DataAlmacen = new List<Almacen>();

            DataEmpleados = cis.GetEmpleados();
            DataTipoConsumo = cis.GetTipoConsumo();
            ComboSuperCat = cis.GetSuperCategoria();
            DataInsumos = cis.GetInsumos();
            DataAlmacen = cis.GetAlmacenes();

            this.Operacion = "Nuevo";
        }
        private void Cancelar()
        {
            this.ComboCat = new List<Categoria>();
            this.ComboGrupo = new List<Grupo>();
            this.ComboPlato = new List<Platos>();
            this.DataEmpleados = new List<Empleado>();
            this.DataTipoConsumo = new List<TipoConsumo>();
            this.DataInsumos = new List<Insumos>();
            this.DataAlmacen = new List<Almacen>();

            DataEmpleados = cis.GetEmpleados();
            DataTipoConsumo = cis.GetTipoConsumo();
            ComboSuperCat = cis.GetSuperCategoria();
            DataInsumos = cis.GetInsumos();
            DataAlmacen = cis.GetAlmacenes();

            this.Operacion_tipoconsumo = "Ninguno";
            this.Operacion = "Lista";
            Precio_plato = 0;
            Importe_Total = 0;
            Observacion = "";

        }
        private void GetLista()
        {
            this.ComboCat = new List<Categoria>();
            this.ComboGrupo = new List<Grupo>();
            this.ComboPlato = new List<Platos>();
            this.DataEmpleados = new List<Empleado>();
            this.DataTipoConsumo = new List<TipoConsumo>();
            this.DataInsumos = new List<Insumos>();



            //this.DataAlmacen = new List<Almacen>();
            this.Operacion_tipoconsumo = "Ninguno";
            Precio_plato = 0;
            Importe_Total = 0;

            Observacion = "";
             Fecha_Desde = DateTime.Now;
      Fecha_Hasta  = DateTime.Now;
        DataConsumoInterno = cis.GetConsumoInternoFecha(Fecha_Desde, Fecha_Hasta);
            //DataConsumoInterno = cis.GetConsumoInterno();
           


            DataEmpleados = cis.GetEmpleados();
            DataTipoConsumo = cis.GetTipoConsumo();
            ComboSuperCat = cis.GetSuperCategoria();
            DataInsumos = cis.GetInsumos();
            DataAlmacen = cis.GetAlmacenes();

            this.Platos = new Platos();
            this.Insumos = new Insumos();
            this.Empleado = new Empleado();
            this.TipoConsumo = new TipoConsumo();
            this.ConsumoInterno = new ConsumoInterno();
            this.Almacen = new Almacen();

            Cantidad_insumo = 0;
            Cant_plato = "0";
        }
    }
}
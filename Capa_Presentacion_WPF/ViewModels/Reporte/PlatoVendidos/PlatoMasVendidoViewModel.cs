using Capa_Entidades.Models.Carta;
using Capa_Negocio.Carta;
using Capa_Negocio.Funciones_Globales;
using Capa_Presentacion_WPF.Views.Dialogs;
using ExportToExcel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.PlatoVendidos
{
    public class PlatoMasVendidoViewModel : INotifyPropertyChanged
    {
        PlatosMasVendidosStructure pstructure = new PlatosMasVendidosStructure();
        Neg_Categoria_carta negCategoria = new Neg_Categoria_carta();
        Neg_Grupo negGrupo = new Neg_Grupo();
        #region Entidad

        #endregion
        #region Objetos
        private string _operacion;
        private Categoria _CatSelected;
        private Grupo _GrupSelected;

        public int idCategoriaSeleccionado { get; set; }
        public int idGrupoSeleccionado { get; set; } = 0;

        public string rbDia { get; set; }
        public string rbEntreDias { get; set; }
        public int Idradiobuton { get; set; }
        public bool checkedGrupo { get; set; } = false;
        public bool soloCat { get; set; } = true;

        public string bkgbuscar { get; set; }
        public string bkgbuscartodo { get; set; }
        public string frgbuscar { get; set; } = "#000";
        public string frgbuscartodo { get; set; } = "#000";

        public string visibleGrupo { get; set; } = "Visible";
        public string visibleCategoria { get; set; } = "Visible";
        #endregion
        #region GetSetObjetos
        public string Operacion
        {
            get => _operacion;
            set 
            {
                if (value == "Lista")
                {
                    getLista();
                }
                _operacion = value;
            }
        }
        #endregion
        #region SelectedObjetos
        public Categoria CatSelected
        {
            get => _CatSelected;
            set
            {
                if (value != null) 
                {
                    idCategoriaSeleccionado = ((Categoria)value).idcat;
                    ComboGrupo = negGrupo.GetGupoxCategoriaTodos(idCategoriaSeleccionado);
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
                    idGrupoSeleccionado = ((Grupo)value).idgrup;
                }
                _GrupSelected = value;
            }
        }
        #endregion
        #region Listas
        public ObservableCollection<Categoria> ComboCat { get; set; }
        public ObservableCollection<Grupo> ComboGrupo { get; set; }
        public List<Capa_Entidades.Models.Pedido.Pedidos> DataPlatosMasVendidos { get; set; }
        public List<Capa_Entidades.Models.Pedido.Pedidos> DataPlatosCriollosMarinos { get; set; }
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
        //fin

        private DateTime _Date = DateTime.Now;
        public event PropertyChangedEventHandler PropertyChanged_Date;
        private DateTime _startDate = DateTime.Now;
        public event PropertyChangedEventHandler PropertyChanged_startDate;
        private DateTime _FinishDate = DateTime.Now;
        public event PropertyChangedEventHandler PropertyChanged_finishdate;
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; OnPropertyChanged_Date("Date"); }
        }
        public void OnPropertyChanged_Date(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_Date;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged_date("StartDate"); }
        }
        public void OnPropertyChanged_date(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_startDate;
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
        #endregion
        #region Commands
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand MostrarTodoCommand { get; set; }
        public ICommand RbtTagCommand { get; set; }
        public ICommand CheckGrupoCommand { get; set; }
        public ICommand MostrarTolosLosPlatosCommand { get; set; }
        Funcion_Global negFuncionGlobal = new Funcion_Global();

        #endregion
        public PlatoMasVendidoViewModel()
        {
            valorbt();
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.RbtTagCommand = new ParamCommand(new Action<object>(CheckedRb));
            this.CheckGrupoCommand = new RelayCommand(new Action(CheckGrupo));
            this.MostrarTolosLosPlatosCommand = new RelayCommand(new Action(BuscarTodosLosPlatos));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ComboCat = new ObservableCollection<Categoria>();
            this.ComboGrupo = new ObservableCollection<Grupo>();
            this.DataPlatosMasVendidos = new List<Capa_Entidades.Models.Pedido.Pedidos>();
            this.Operacion = "Lista";
        }
        private void BuscarTodosLosPlatos()
        {
            DataPlatosMasVendidos = pstructure.GetPlatosMasVendidosTodos(Idradiobuton,Date, StartDate, FinishDate);
            DataPlatosCriollosMarinos = pstructure.GetPlatosMarinosCriollos(Idradiobuton, Date, StartDate, FinishDate);
            this.bkgbuscar = "#fff";
            this.bkgbuscartodo = "#2196f3";

            this.frgbuscar = "#000";
            this.frgbuscartodo = "#fff";
        }
        private void Buscar()
        {
            string mss = "";
            DataPlatosMasVendidos = pstructure.GetPlatosMasVendidos(Idradiobuton,idGrupoSeleccionado, idCategoriaSeleccionado, Date, StartDate, FinishDate);
            DataPlatosCriollosMarinos = pstructure.GetPlatosMarinosCriollos(Idradiobuton, Date, StartDate, FinishDate);
            if (soloCat == true)
            {
                visibleGrupo = "Visible";
                visibleCategoria = "Collapsed";
            }
            else {
                visibleGrupo = "Collapsed";
                visibleCategoria = "Collapsed";
            }
            this.bkgbuscar = "#2196f3";
            this.bkgbuscartodo = "#fff";
            this.frgbuscar = "#fff";
            this.frgbuscartodo = "#000";
        }
        private void CheckGrupo()
        {
            if (checkedGrupo == false) { checkedGrupo = false; soloCat = true; } else if (checkedGrupo == true) { checkedGrupo = true; soloCat = false; }

        }
        private void CheckedRb(object id)
        {
            this.Idradiobuton = Convert.ToInt32(id);
        }
        void valorbt()
        {
            this.rbDia = "1";
            this.rbEntreDias = "2";
        }
        private void getLista()
        {
            ComboCat = negCategoria.GetCategoriaTodos();
            this.Idradiobuton = 1;
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
            saveFileDialog1.FileName = "Historial_Platos_Vendidos";

            DataTable dt = new DataTable();
            dt.Columns.Add("PLATO");
            dt.Columns.Add("CANT.");
            dt.Columns.Add("IMPORTE");
            dt.Columns.Add("GRUPO");
            dt.Columns.Add("CATEGORIA");
            foreach (var p in DataPlatosMasVendidos)
            {
                dt.Rows.Add(new object[] { p.DP_DESCRIP, p.DP_CANT, p.DP_IMPORT, p.GR_NOM,p.CAT_NOM });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "Historial de Platos Vendidos");
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
            saveFileDialog1.FileName = "Historial_Platos_Vendidos";

            DataTable dt = new DataTable();
            dt.Columns.Add("PLATO");
            dt.Columns.Add("CANT.");
            dt.Columns.Add("IMPORTE");
            dt.Columns.Add("GRUPO");
            dt.Columns.Add("CATEGORIA");
            foreach (var p in DataPlatosMasVendidos)
            {
                dt.Rows.Add(new object[] { p.DP_DESCRIP, p.DP_CANT, p.DP_IMPORT, p.GR_NOM, p.CAT_NOM });
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
    }
}

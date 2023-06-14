using Capa_Entidades.Models.Ambientes;
using Capa_Negocio.Ambientes;
using Capa_Presentacion_WPF.ViewModels.Ambientes;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Capa_Negocio.Funciones_Globales;
using ExportToExcel;


namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Ambientes
{
    public class AmbientesViewModel : INotifyPropertyChanged
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        AmbientesStructure astructure = new AmbientesStructure();
        Neg_Ambientes negamb = new Neg_Ambientes();
        #region objetos
        private string operacion;
        private string _NombreAmbiente;
        private bool _EstadoAmbiente;
        private AmbientesItem _Ambientes;
        private bool _IsCheckedBusqueda;
        private int _alto;
        private int _ancho;
        public string TextoBuscar { get; set; }
        public int Alto
        {
            get => _alto;
            set
            {
                if (value != 0)
                {
                    List<AmbientesItem> _a = new List<AmbientesItem>();
                    _a = astructure.GetAmbientes();
                    ObservableCollection<AmbientesItem> a = new ObservableCollection<AmbientesItem>(_a);
                    DataAmbientes = a;
                    DataAmbientes.Select(s => { s.A_HEIGHT = value; return s; }).ToList();
                }
                _alto = value;
            }
        }
        public int Ancho
        {
            get => _ancho;
            set
            {
                if (value != 0)
                {
                    List<AmbientesItem> _a = new List<AmbientesItem>();
                    _a = astructure.GetAmbientes();
                    ObservableCollection<AmbientesItem> a = new ObservableCollection<AmbientesItem>(_a);
                    DataAmbientes = a;
                    DataAmbientes.Select(s => { s.A_WIDTH = value; return s; }).ToList();
                }
                _ancho = value;
            }
        }
        #endregion
        #region GetSetObjetos
        public string Operacion
        {
            get => operacion;
            set
            {
                operacion = value;
                if (operacion == "Lista") { getLista(); }
            }
        }
        public string NombreAmbiente
        {
            get => _NombreAmbiente;
            set
            {
                _NombreAmbiente = value;
            }
        }
        
        public bool EstadoAmbiente
        {
            get => _EstadoAmbiente;
            set
            {
                _EstadoAmbiente = value;
            }
        }
        public bool IsCheckedBusqueda
        {
            get => _IsCheckedBusqueda;
            set
            {
                _IsCheckedBusqueda = value;
            }
        }

        public AmbientesItem Ambientes
        { 
            get => _Ambientes;
            set
            {
                _Ambientes = value;
            }
        }
        #endregion
        #region Listas
        public ObservableCollection<AmbientesItem> DataAmbientes { get; set; }
        #endregion
        #region Commands
        public ICommand NuevoCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand DesignCommand { get; set; }
        public ICommand GuardarTamanioCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }


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
        public AmbientesViewModel()
        {
            this.Operacion = "Lista";
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.DesignCommand = new RelayCommand(new Action(Design));
            this.GuardarTamanioCommand = new RelayCommand(new Action(GuardarTamanio));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.Ambientes = new AmbientesItem();
        }

        private void Buscar()
        {
            List<AmbientesItem> _a = new List<AmbientesItem>();
            _a = astructure.GetAmbientes();
            ObservableCollection<AmbientesItem> a = new ObservableCollection<AmbientesItem>(_a);
            DataAmbientes = a;

            ObservableCollection<AmbientesItem> ls = new ObservableCollection<AmbientesItem>();
            ls = DataAmbientes;
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                List<AmbientesItem> _aa = new List<AmbientesItem>();
                _aa = astructure.GetAmbientes();
                ObservableCollection<AmbientesItem> aa = new ObservableCollection<AmbientesItem>(_aa);
                DataAmbientes = aa;
            }
            else
            {
                List<AmbientesItem> lista = ls
                    .Where(w =>
                    w.A_NOM.ToUpper().Contains(TextoBuscar.ToUpper())).ToList();
                ObservableCollection<AmbientesItem> o = new ObservableCollection<AmbientesItem>(lista);
                DataAmbientes = o;
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
            saveFileDialog1.FileName = "Ambientes " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE AMBIENTE");
            dt.Columns.Add("FECHA REGISTRO");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataAmbientes)
            {
                dt.Rows.Add(new object[] { p.ID, p.A_NOM, p.A_F_CREATE,(p.A_ACT == true) ? "Activo" : "Inactivo"});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE AMBIENTES");
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
            saveFileDialog1.FileName = "Ambientes " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE AMBIENTE");
            dt.Columns.Add("FECHA REGISTRO");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataAmbientes)
            {
                dt.Rows.Add(new object[] { p.ID, p.A_NOM, p.A_F_CREATE, (p.A_ACT == true) ? "Activo" : "Inactivo" });
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
        private void GuardarTamanio()
        {
            string _mensaje = "";
            Ambientes.A_HEIGHT = Alto;
            Ambientes.A_WIDTH = Ancho;
            bool res = negamb.SetTamañoAmbiente(Ambientes, ref _mensaje);
            if (res == false)
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = "No se pudo guardar los cambios" }
                };
                DialogHost.Show(view, "RootDialog");
                return;
            }
            this.Operacion = "Lista";
        }
        private void Design()
        {
            this.Operacion = "TableDesign";
            this.Alto = DataAmbientes.FirstOrDefault().A_HEIGHT;
            this.Ancho = DataAmbientes.FirstOrDefault().A_WIDTH;
        }

        public async void Eliminar(object ID) 
        {
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "Desea eliminar el registro ?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
                return;
            string _mensaje = "";
            this.Ambientes = new AmbientesItem();
            int id = Convert.ToInt32(ID);
            Ambientes.ID = id;
            bool result = negamb.SetAmbiente(3, Ambientes, ref _mensaje);
            var view = new MessageDialog
            {
                Mensaje = { Text = _mensaje }
            };
            await DialogHost.Show(view, "RootDialog");
            if (result)
            {
                getLista();
            }
        }
        public void Editar(object ID) 
        {
            this.Operacion = "Editar";
            int id = Convert.ToInt32(ID);
            this.Ambientes = this.DataAmbientes.Where(w => w.ID == id).FirstOrDefault();
            this.NombreAmbiente = Ambientes.A_NOM;
            this.EstadoAmbiente = Ambientes.A_ACT;
        }
        public void Nuevo() 
        {
            this.Operacion = "Nuevo";
            this.Ambientes = new AmbientesItem();
        }
        public void Cancelar() 
        {
            this.Operacion = "Lista";
        }
        public void getLista()
        {
            List<AmbientesItem> _a = new List<AmbientesItem>();
            _a = astructure.GetAmbientes();
            ObservableCollection<AmbientesItem> a = new ObservableCollection<AmbientesItem>(_a);
            DataAmbientes = a;

            this.EstadoAmbiente = true;
        }
        public void Guardar()
        {
            string _mensaje = "";
            int _id = 0;
            if (this.Operacion == "Editar") { _id = Ambientes.ID; }
            
            Ambientes.A_NOM = this.NombreAmbiente;
            Ambientes.A_ACT = this.EstadoAmbiente;
            bool res = negamb.SetAmbiente((_id == 0 ? 1 : 2), Ambientes, ref _mensaje);
            this.Operacion = "Lista";
            this.NombreAmbiente = "";
            this.EstadoAmbiente = true;
            getLista();
        }
    }
}

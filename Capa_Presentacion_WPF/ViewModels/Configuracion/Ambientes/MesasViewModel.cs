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
    public class MesasViewModel : INotifyPropertyChanged
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        MesasStructure mstructure = new MesasStructure();
        AmbientesStructure astructure = new AmbientesStructure();
        Neg_Mesa negmes = new Neg_Mesa();
        #region objetos
        private string operacion;
        private string _NombreMesa;
        private bool _EstadoMesa;
        private MesasItem _Mesas;
        private MesasItem _TipoMesaSelected;
        private AmbientesItem _AmbientesSelected;
        private MesasItem _MesaPadreSelected;
        private MesasItem _MesaEstadoSelected;
        private int _ancho;
        private int _alto;
        private int _Margin_Right;
        private int _Margin_Left;
        private int _Margin_Top;
        private int _Margin_Bottom;
        private string _margin_string;
        public string TextoBuscar { get; set; }
        
        private int tipoMesa;
        private string _VisibilityComboMesas;
        private bool _IsCheckedBusqueda;
        private bool _EnabledBusqueda;
        public int nroColumnas { get; set; }
        public int Alto
        {
            get => _alto;
            set
            {
                if (value != 0)
                {
                    List<MesasItem> _c = new List<MesasItem>();
                    _c = mstructure.GetMesasNoSubMesas(Mesas.M_ID_AMB);
                    ObservableCollection<MesasItem> c = new ObservableCollection<MesasItem>(_c);
                    DataMesasCombo = c;
                    DataMesasCombo.Select(s => { s.M_HEIGHT = value; return s; }).ToList();
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
                    List<MesasItem> _c = new List<MesasItem>();
                    _c = mstructure.GetMesasNoSubMesas(Mesas.M_ID_AMB);
                    ObservableCollection<MesasItem> c = new ObservableCollection<MesasItem>(_c);
                    DataMesasCombo = c;
                    DataMesasCombo.Select(s => { s.M_WIDTH = value; return s; }).ToList();
                }
                _ancho = value;
            }
        }
        public int Margin_Right 
        {
            get => _Margin_Right;
            set 
            {
                Margin_string = Margin_Left.ToString() + "," + Margin_Top.ToString() + "," + value.ToString() + "," + Margin_Bottom.ToString();
                _Margin_Right = value;
            }
        }
        public int Margin_Left 
        {
            get => _Margin_Left;
            set 
            {
                if (value != 0)
                {
                    Margin_string = value.ToString() + ","+ Margin_Top .ToString()+ "," + Margin_Right.ToString() + "," + Margin_Bottom.ToString();
                }
                _Margin_Left = value;
            }
        }
        public int Margin_Top
        {
            get => _Margin_Top;
            set 
            {
                Margin_string = Margin_Left.ToString() + "," + value.ToString() + "," + Margin_Right.ToString() + "," + Margin_Bottom.ToString();
                _Margin_Top = value;
            }
        }
        public int Margin_Bottom
        {
            get => _Margin_Bottom;
            set 
            {
                if (value != 0)
                {
                    Margin_string = value.ToString() + "," + Margin_Top.ToString() + "," + Margin_Right.ToString() + "," + value.ToString();
                }
                _Margin_Bottom = value;
            }
        }
        public string Margin_string 
        {
            get => _margin_string;
            set 
            {
                _margin_string = value;
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
        public string NombreMesa
        {
            get => _NombreMesa;
            set
            {
                _NombreMesa = value;
            }
        }

        public bool EstadoMesa
        {
            get => _EstadoMesa;
            set
            {
                _EstadoMesa = value;
            }
        }
        public int TipoMesa
        {
            get => tipoMesa;
            set
            {
                tipoMesa = value;
            }
        }
        public string VisibilityComboMesas
        {
            get => _VisibilityComboMesas;
            set
            {
                _VisibilityComboMesas = value;
            }
        }

        public MesasItem Mesas
        {
            get => _Mesas;
            set
            {
                _Mesas = value;
            }
        }
        public MesasItem TipoMesaSelected
        {
            get => _TipoMesaSelected;
            set
            {
                if (value != null)
                {
                    Mesas.ID_TM = ((MesasItem)value).ID_TM;
                    if (Mesas.ID_TM == 2)
                    {
                        this.VisibilityComboMesas = "Visible";
                        List<MesasItem> _c = new List<MesasItem>();
                        _c = mstructure.GetMesasNoSubMesas(Mesas.M_ID_AMB);
                        ObservableCollection<MesasItem> c = new ObservableCollection<MesasItem>(_c);
                        DataMesasCombo = c;
                    }
                    else if(Mesas.ID_TM == 1) {
                        Mesas.M_ID_PADRE = 0;
                        this.VisibilityComboMesas = "Hidden";
                    }
                }
                _TipoMesaSelected = value;
            }
        }
        public MesasItem MesaPadreSelected
        {
            get => _MesaPadreSelected;
            set
            {
                if (value != null)
                {
                    Mesas.M_ID_PADRE = ((MesasItem)value).ID;
                }
                _MesaPadreSelected = value;
            }
        }
        public AmbientesItem AmbientesSelected
        {
            get => _AmbientesSelected;
            set
            {
                if (value != null)
                {
                    Mesas.M_ID_AMB = ((AmbientesItem)value).ID;
                    List<MesasItem> _c = new List<MesasItem>();
                    _c = mstructure.GetMesasNoSubMesas(Mesas.M_ID_AMB);
                    ObservableCollection<MesasItem> c = new ObservableCollection<MesasItem>(_c);
                    DataMesasCombo = c;
                    this.Alto = DataMesas.FirstOrDefault().M_HEIGHT;
                    this.Ancho = DataMesas.FirstOrDefault().M_WIDTH;
                    this.Margin_Left = ((AmbientesItem)value).A_X;
                    this.Margin_Right = ((AmbientesItem)value).A_Y;
                    this.Margin_Top = ((AmbientesItem)value).A_TOP;
                    this.Margin_Bottom = ((AmbientesItem)value).A_BOTTOM;
                    this.nroColumnas = ((AmbientesItem)value).nrocolumnas;
                }
                _AmbientesSelected = value;
            }
        }
        public AmbientesItem AmbientesSelectedBuscar
        {
            get => _AmbientesSelected;
            set
            {
                if (value != null)
                {
                    Mesas.M_ID_AMB = ((AmbientesItem)value).ID;
                }
                _AmbientesSelected = value;
            }
        }
        public MesasItem MesaEstadoSelected
        {
            get => _MesaEstadoSelected;
            set
            {
                if (value != null)
                {
                    EstadoMesa = ((MesasItem)value).M_ACT;
                }
                _MesaEstadoSelected = value;
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
        public bool EnabledBusqueda
        {
            get => _EnabledBusqueda;
            set
            {
                _EnabledBusqueda = value;
            }
        }
        #endregion
        #region Listas
        public ObservableCollection<MesasItem> DataMesas { get; set; }
        public ObservableCollection<MesasItem> DataMesasCombo { get; set; }
        public ObservableCollection<MesasItem> DataTipoMesa { get; set; }
        public ObservableCollection<AmbientesItem> DataAmbientes { get; set; }
        public ObservableCollection<MesasItem> DataEstado { get; set; }
        #endregion
        #region Commands
        public ICommand NuevoCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand HabilitarBusquedaCommand { get; set; }
        public ICommand DesignCommand { get; set; }
        public ICommand GuardarTamanioCommand { get; set; }
        public ICommand BuscarxnombreCommand { get; set; }
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
        public MesasViewModel()
        {
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.HabilitarBusquedaCommand = new RelayCommand(new Action(HabilitarBusqueda));
            this.DesignCommand = new RelayCommand(new Action(Design));
            this.GuardarTamanioCommand = new RelayCommand(new Action(GuardarTamanio));
            this.BuscarxnombreCommand = new RelayCommand(new Action(BuscarxNombre));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.Mesas = new MesasItem();
            this.DataAmbientes = new ObservableCollection<AmbientesItem>();
            this.Operacion = "Lista";
        }

        private void BuscarxNombre()
        {
            List<MesasItem> _a = new List<MesasItem>();
            _a = mstructure.GetMesas();
            ObservableCollection<MesasItem> a = new ObservableCollection<MesasItem>(_a);
            DataMesas = a;

            ObservableCollection<MesasItem> ls = new ObservableCollection<MesasItem>();
            ls = DataMesas;
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                List<MesasItem> _mesas = new List<MesasItem>();
                _a = mstructure.GetMesas();
                ObservableCollection<MesasItem> mesas = new ObservableCollection<MesasItem>(_mesas);
                DataMesas = a;
            }
            else
            {
                List<MesasItem> lista = ls
                    .Where(w =>
                    w.A_NOM.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.M_TEXTO.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    (w.A_NOM + " " + w.M_TEXTO).ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    (w.A_NOM + w.M_TEXTO).ToUpper().Contains(TextoBuscar.ToUpper())
                    ).ToList();
                ObservableCollection<MesasItem> o = new ObservableCollection<MesasItem>(lista);
                DataMesas = o;
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
            saveFileDialog1.FileName = "Mesas " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE MESA");
            dt.Columns.Add("NOMBRE AMBIENTE");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataMesas)
            {
                dt.Rows.Add(new object[] { p.ID, p.M_TEXTO, p.A_NOM, (p.M_ACT == true) ? "Activo" : "Inactivo"});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE EMPLEADOS");
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
            saveFileDialog1.FileName = "Mesas " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE MESA");
            dt.Columns.Add("NOMBRE AMBIENTE");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataMesas)
            {
                dt.Rows.Add(new object[] { p.ID, p.M_TEXTO, p.A_NOM, (p.M_ACT == true) ? "Activo" : "Inactivo" });
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
            Mesas.NroColumnas = nroColumnas;
            Mesas.M_HEIGHT = Alto;
            Mesas.M_WIDTH = Ancho;
            Mesas.A_X = Margin_Left;
            Mesas.A_Y = Margin_Left;
            Mesas.A_TOP = Margin_Top;
            Mesas.A_BOTTOM = Margin_Bottom;
            bool res = negmes.SetTamañoAmbiente(Mesas, ref _mensaje);
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
        public void Design()
        {
            this.Operacion = "TableDesign";
        }
        public void HabilitarBusqueda()
        {
            if (IsCheckedBusqueda)
            {
                EnabledBusqueda = true;
            }
            else { EnabledBusqueda = false; getLista();}
        }
        public void Buscar()
        {
            List<MesasItem> _a = new List<MesasItem>();
            _a = mstructure.GetMesasxEstadoAmbiente(Mesas.M_ID_AMB, EstadoMesa);
            ObservableCollection<MesasItem> a = new ObservableCollection<MesasItem>(_a);
            DataMesas = a;
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
            int id = Convert.ToInt32(ID);
            Mesas.ID = id;
            string _mensaje = "";
            bool res = negmes.SetMesas(3, Mesas, ref _mensaje);
            getLista();

            if (res){}
            else {
                var view = new MessageDialog
                {
                    Mensaje = { Text = "Esta mesa tiene pedidos asociados, no se puede eliminar :)" }
                };
                DialogHost.Show(view, "RootDialog");
            }
        }
        public void Editar(object ID)
        {
            this.Operacion = "Editar";
            int id = Convert.ToInt32(ID);
            this.Mesas = this.DataMesas.Where(w => w.ID == id).FirstOrDefault();
            this.NombreMesa = Mesas.M_TEXTO;
            this.EstadoMesa = Mesas.M_ACT;
        }
        public void Nuevo()
        {
            this.Operacion = "Nuevo";
        }
        public void Cancelar()
        {
            this.Operacion = "Lista";
        }
        public void getLista()
        {
            try
            {
                List<MesasItem> _a = new List<MesasItem>();
                _a = mstructure.GetMesas();
                ObservableCollection<MesasItem> a = new ObservableCollection<MesasItem>(_a);
                DataMesas = a;

                List<MesasItem> _tip = new List<MesasItem>();
                _tip = mstructure.GetTipoMesa();
                ObservableCollection<MesasItem> tip = new ObservableCollection<MesasItem>(_tip);
                DataTipoMesa = tip;

                List<AmbientesItem> _amb = new List<AmbientesItem>();
                _amb = astructure.GetLogicalDrives();
                ObservableCollection<AmbientesItem> amb = new ObservableCollection<AmbientesItem>(_amb);
                DataAmbientes = amb;


                List<MesasItem> _c = new List<MesasItem>();
                _c = mstructure.GetEstadosMesas();
                ObservableCollection<MesasItem> c = new ObservableCollection<MesasItem>(_c);
                DataEstado = c;

                List<MesasItem> _cc = new List<MesasItem>();
                _cc = mstructure.GetMesasNoSubMesas(1);
                ObservableCollection<MesasItem> cc = new ObservableCollection<MesasItem>(_cc);
                DataMesasCombo = cc;


                this.VisibilityComboMesas = "Hidden";
                this.EstadoMesa = true;

                Mesas.M_ID_AMB = DataAmbientes.FirstOrDefault().ID;
                List<MesasItem> _aa = new List<MesasItem>();
                _aa = mstructure.GetMesasNoSubMesas(DataAmbientes.FirstOrDefault().ID);
                ObservableCollection<MesasItem> aa = new ObservableCollection<MesasItem>(_aa);
                DataMesasCombo = aa;
                this.Alto = DataMesas.FirstOrDefault().M_HEIGHT;
                this.Ancho = DataMesas.FirstOrDefault().M_WIDTH;
                this.Margin_Left = DataAmbientes.FirstOrDefault().A_X;
                this.Margin_Right = DataAmbientes.FirstOrDefault().A_Y;
                this.Margin_Top = DataAmbientes.FirstOrDefault().A_TOP;
                this.Margin_Bottom = DataAmbientes.FirstOrDefault().A_BOTTOM;
                this.nroColumnas = DataAmbientes.FirstOrDefault().nrocolumnas;
            }
            catch (Exception ex)
            {
               // negFuncionGlobal.Mensaje("SOS-FOOD - Error", "Error : " + ex.Message, 3);
            }
            
        }
        public void Guardar()
        {
            if (NombreMesa == null || NombreMesa == "")
            {
                var view = new MessageDialog 
                {
                    Mensaje = { Text = "Ingrese todos los campos"}
                };
                DialogHost.Show(view, "RootDialog");
                return;
            }
            string _mensaje = "";
            int _id = 0;
            if (this.Operacion == "Editar") { _id = Mesas.ID; }

            Mesas.M_NOM = this.NombreMesa.TrimStart();
            Mesas.M_ACT = this.EstadoMesa;
            bool res = negmes.SetMesas((_id == 0 ? 1 : 2), Mesas, ref _mensaje);
            if (res == false)
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = "Ups! Ocurrió un error, asegurese de poner los datos correctos" }
                };
                DialogHost.Show(view, "RootDialog");
                return;
            }
            this.Operacion = "Lista";
            this.NombreMesa = "";
            this.EstadoMesa = true;
            getLista();
        }
    }
}


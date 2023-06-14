using Capa_Datos.Stock;
using Capa_Entidades.Models.Stock;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Stock;
using Capa_Presentacion_WPF.Views.Dialogs;
using Capa_Presentacion_WPF.Views.Dialogs.AlmacenDefault;
using ExportToExcel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Stock
{
   public class AlmacenViewModel :IGeneric
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Almacen negAlmacen = new Neg_Almacen();
        private Almacen almacen;
        private string operacion;

        public ObservableCollection<Almacen> DataAlmacen { get; set; }
        public Almacen Almacen
        {
            get => almacen;
            set
            {
                almacen = value;
            }
        }
        public Almacen SelectedDataFile { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand GuardarEmpresaDefaultCommand { get; set; }
        public ICommand btnAbrirDialogCommand { get; set; }


        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
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
        public string TextoBuscar { get; set; }
        public object idalmacen { get; set; }
        public AlmacenViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.GuardarEmpresaDefaultCommand = new RelayCommand(new Action(AlmacenDefault));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.btnAbrirDialogCommand = new RelayCommand(new Action(AbrirDialog));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = this.SelectedDataFile;
                if (set != null)
                {
                    this.idalmacen = set.idalm;
                    //Application.Current.Properties["id_empre"] = set.idEmpr;

                    //ObservableCollection<BoletaFactura> DataBolFactura = new ObservableCollection<BoletaFactura>(DataBolFac);
                }

            });
        }

        private void Buscar()
        {
            DataAlmacen = negAlmacen.GetAlmacen();

            ObservableCollection<Almacen> ls = new ObservableCollection<Almacen>();
            ls = DataAlmacen;
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataAlmacen = negAlmacen.GetAlmacen();
            }
            else
            {
                List<Almacen> lista = ls
                    .Where(w =>
                    w.nomalm.ToUpper().Contains(TextoBuscar.ToUpper())).ToList();
                ObservableCollection<Almacen> o = new ObservableCollection<Almacen>(lista);
                DataAlmacen = o;
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
            saveFileDialog1.FileName = "Almacenes " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE ALMACEN");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataAlmacen)
            {
                dt.Rows.Add(new object[] { p.idalm, p.nomalm,(p.estadoalm == 1) ? "Activo" : "Inactivo" });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE ALMACENES");
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
            saveFileDialog1.FileName = "Almacenes " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE ALMACEN");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataAlmacen)
            {
                dt.Rows.Add(new object[] { p.idalm, p.nomalm, (p.estadoalm == 1) ? "Activo" : "Inactivo" });
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

        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.Almacen = this.DataAlmacen.Where(w => w.idalm == (int)id).FirstOrDefault();
        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            //DialogHost.OpenDialogCommand.Execute(DateTime.Now,null) ;
            this.Operacion = "Nuevo";
            this.Almacen = new Almacen();
        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(Almacen.nomalm))
            {
                Almacen almacen = this.Almacen;
                var _id = almacen.idalm;
                string _mensaje = "";

                bool result = negAlmacen.SetAlmacen((_id == 0 ? 1 : 2), almacen, ref _mensaje);
                var view = new MessageDialog
                {
                    Mensaje = { Text = _mensaje }
                };
                await DialogHost.Show(view, "RootDialog");
                if (result)
                {
                    this.Operacion = "Lista";
                }
            }
            return;

        }
        private async void Eliminar(object id)
        {
            try
            {
                var SiNo = new SiNoMessageDialog
                {
                    Mensaje = { Text = "Desea eliminar el registro ?" }
                };
                var x = await DialogHost.Show(SiNo, "RootDialog");
                if (!(bool)x)
                    return;
                string _mensaje = "";
                Almacen almacen = new Almacen();
                almacen.idalm = (int)id;
                bool result = negAlmacen.SetAlmacen(3, almacen, ref _mensaje);
                var view = new MessageDialog
                {
                    Mensaje = { Text = _mensaje }
                };
                await DialogHost.Show(view, "RootDialog");
                if (result)
                {
                    GetLista();
                }
            }
            catch (Exception ex)
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = "no es posible eliminar " +ex.ToString() }
                };
                await DialogHost.Show(view, "RootDialog");
            }
            
        }
        private async void AlmacenDefault()
        {
            string _mensaje = "";
            Almacen almacen = new Almacen();
            if (idalmacen != null)
            {
                almacen.idalm = (int)idalmacen;
                bool result = negAlmacen.SetAlmacen(4, almacen, ref _mensaje);
                var view = new MessageDialog
                {
                    Mensaje = { Text = _mensaje }
                };
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(view, "RootDialog");
                if (result)
                {
                    GetLista();
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe seleccionar un almacen", 2);
            }
        }
        private void AbrirDialog()
        {
            //Operacion = "Empresa por defecto";
            var view = new DialogAlmacenDefault
            {

            };
            DialogHost.Show(view, "RootDialog");
        }
        private void GetLista()
        {
            DataAlmacen = negAlmacen.GetAlmacen();
        }
        
    }
}

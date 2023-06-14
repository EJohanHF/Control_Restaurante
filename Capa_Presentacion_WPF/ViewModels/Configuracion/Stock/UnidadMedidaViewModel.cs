using Capa_Entidades.Models.Stock;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Stock;
using Capa_Presentacion_WPF.Views.Dialogs;
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
  public class UnidadMedidaViewModel :IGeneric
    {
        Neg_UnidaMedida negUM = new Neg_UnidaMedida();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        private UnidadMedida unidadmedida;
        private string operacion;

        public ObservableCollection<UnidadMedida> DataDataUnidadMedida { get; set; }
        public UnidadMedida UnidadMedida
        {
            get => unidadmedida;
            set
            {
                unidadmedida = value;
            }
        }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
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
        public UnidadMedidaViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
        }
        private void Buscar()
        {
            DataDataUnidadMedida = negUM.GetUnidadMedida();
            ObservableCollection<UnidadMedida> ls = new ObservableCollection<UnidadMedida>();
            ls = DataDataUnidadMedida;
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataDataUnidadMedida = negUM.GetUnidadMedida();
            }
            else
            {
                List<UnidadMedida> lista = ls
                    .Where(w =>
                    w.descum.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.denom.ToUpper().Contains(TextoBuscar.ToUpper())
                     ).ToList();
                ObservableCollection<UnidadMedida> o = new ObservableCollection<UnidadMedida>(lista);
                DataDataUnidadMedida = o;
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
            saveFileDialog1.FileName = "Insumos " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("U. MEDIDA");
            dt.Columns.Add("DESCRIPCION");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataDataUnidadMedida)
            {
                dt.Rows.Add(new object[] { p.idum, p.denom, p.descum, (p.estadoum == 1) ? "Activo" : "Inactivo" });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE INSUMOS");
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
            saveFileDialog1.FileName = "Insumos " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("U. MEDIDA");
            dt.Columns.Add("DESCRIPCION");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataDataUnidadMedida)
            {
                dt.Rows.Add(new object[] { p.idum, p.denom, p.descum, (p.estadoum == 1) ? "Activo" : "Inactivo" });
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
            this.UnidadMedida = this.DataDataUnidadMedida.Where(w => w.idum == (int)id).FirstOrDefault();
        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            //DialogHost.OpenDialogCommand.Execute(DateTime.Now,null) ;
            this.Operacion = "Nuevo";
            this.UnidadMedida = new UnidadMedida();
        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(UnidadMedida.descum) && !string.IsNullOrWhiteSpace(UnidadMedida.denom))
            {
                UnidadMedida unidadmedida = this.UnidadMedida;
                var _id = unidadmedida.idum;
                string _mensaje = "";

                bool result = negUM.SetUnidadMedida((_id == 0 ? 1 : 2), unidadmedida, ref _mensaje);
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
            else
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = "Debe ingresar todos los valores" }
                };
                await DialogHost.Show(view, "RootDialog");
                return;
            }
            

        }
        private async void Eliminar(object id)
        {
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "Desea eliminar el registro ?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
                return;
            string _mensaje = "";
            UnidadMedida unidadmedida = new UnidadMedida();
            unidadmedida.idum = (int)id;
            bool result = negUM.SetUnidadMedida(3, unidadmedida, ref _mensaje);
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

        private void GetLista()
        {
            DataDataUnidadMedida = negUM.GetUnidadMedida();
        }
    }
}

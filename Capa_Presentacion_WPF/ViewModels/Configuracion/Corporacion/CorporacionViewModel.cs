using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Configuracion;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Capa_Negocio.Funciones_Globales;
using ExportToExcel;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Empleados
{
  public  class CorporacionViewModel  : IGeneric
    {

        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Corporacion negCorp = new Neg_Corporacion();
        private Corporacion corporacion;
        private string operacion;

        public ObservableCollection<Corporacion> DataCorporacion { get; set; }
        
        public Corporacion Corporacion
        {
            get => corporacion;
            set
            {
                corporacion = value;
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
                if (value=="Lista")
                    GetLista();
                    operacion = value;

            }
        }
        public string TextoBuscar { get; set; }
        public CorporacionViewModel()
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
            ObservableCollection<Corporacion> ls = new ObservableCollection<Corporacion>();
            ls = DataCorporacion = negCorp.GetCorporacion();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataCorporacion = negCorp.GetCorporacion();
            }
            else
            {
                List<Corporacion> lista = ls
                    .Where(w =>
                    w.corp_nom.ToUpper().Contains(TextoBuscar.ToUpper())).ToList();
                ObservableCollection<Corporacion> o = new ObservableCollection<Corporacion>(lista);
                DataCorporacion = o;
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
            saveFileDialog1.FileName = "Coporaciones " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE DE LA CORPORACION");
            foreach (var p in DataCorporacion)
            {
                dt.Rows.Add(new object[] { p.id, p.corp_nom});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE CORPORACIONES");
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
            saveFileDialog1.FileName = "Coporaciones " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE DE LA CORPORACION");
            foreach (var p in DataCorporacion)
            {
                dt.Rows.Add(new object[] { p.id, p.corp_nom });
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
            this.Corporacion = this.DataCorporacion.Where(w => w.id == (int)id).FirstOrDefault();
        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            this.Operacion = "Nuevo";
            this.Corporacion = new Corporacion();
        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(Corporacion.corp_nom) )
            {
                Corporacion corporacion = this.Corporacion;
                var _id = corporacion.id;
                string _mensaje = "";
                bool result = negCorp.SetCorporacion((_id == 0 ? 1 : 2), corporacion, ref _mensaje);
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
            //var notify = new Notificacion
            //{
            //    Mensaje = { Text = "Desea eliminar el registro ?" }
            //};
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "Desea eliminar el registro ?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
                return;
            string _mensaje = "";
            Corporacion corporacion = new Corporacion();
            corporacion.id = (int)id;
            bool result = negCorp.SetCorporacion(3, corporacion, ref _mensaje);
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
            DataCorporacion = negCorp.GetCorporacion();
        }
    }
}

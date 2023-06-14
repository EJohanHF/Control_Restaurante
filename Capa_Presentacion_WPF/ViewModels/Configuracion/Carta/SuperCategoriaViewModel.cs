using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Carta;
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

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Carta
{
   public class SuperCategoriaViewModel : IGeneric
    {

        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_SuperCategoria negsCat = new Neg_SuperCategoria();
        private SCategoria supercategoria;
        private string operacion;

        public ObservableCollection<SCategoria> DatasCategoria { get; set; }

        public SCategoria SCategoria
        {
            get => supercategoria;
            set
            {
                supercategoria = value;
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
        public SuperCategoriaViewModel()
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
            ObservableCollection<SCategoria> ls = new ObservableCollection<SCategoria>();
            ls = DatasCategoria = negsCat.GetSCat();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DatasCategoria = negsCat.GetSCat();
            }
            else
            {
                List<SCategoria> lista = ls
                    .Where(w =>
                    w.nomscat.ToUpper().Contains(TextoBuscar.ToUpper())).ToList();
                ObservableCollection<SCategoria> o = new ObservableCollection<SCategoria>(lista);
                DatasCategoria = o;
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
            saveFileDialog1.FileName = "Super Categorias " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE DE SUPER CATEGORIA");
            foreach (var p in DatasCategoria)
            {
                dt.Rows.Add(new object[] { p.idscat, p.nomscat});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE SUPER CATEGORIAS");
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
            saveFileDialog1.FileName = "Super Categorias " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE DE SUPER CATEGORIA");
            foreach (var p in DatasCategoria)
            {
                dt.Rows.Add(new object[] { p.idscat, p.nomscat });
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
            this.SCategoria = this.DatasCategoria.Where(w => w.idscat == (int)id).FirstOrDefault();
        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            //DialogHost.OpenDialogCommand.Execute(DateTime.Now,null) ;
            this.Operacion = "Nuevo";
            this.SCategoria = new SCategoria();
        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(supercategoria.nomscat))
            {
                SCategoria scategoria = this.SCategoria;
                var _id = scategoria.idscat;
                string _mensaje = "";
                bool result = negsCat.SetsCategoria((_id == 0 ? 1 : 2), scategoria, ref _mensaje);
                if (!result)
                {
                    var view = new MessageDialog
                    {
                        Mensaje = { Text = _mensaje }
                    };
                    await DialogHost.Show(view, "RootDialog");
                }
                if (result)
                {
                    this.Operacion = "Lista";
                }
            }return;

            
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
            SCategoria scategoria = new SCategoria();
            scategoria.idscat = (int)id;
            bool result = negsCat.SetsCategoria(3, scategoria, ref _mensaje);
            if (!result)
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = _mensaje }
                };
                await DialogHost.Show(view, "RootDialog");
            }
            if (result)
            {
                GetLista();
            }
        }
        private void GetLista()
        {
            DatasCategoria = negsCat.GetSCat();
        }
    }
}

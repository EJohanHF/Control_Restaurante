using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Carta;
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

namespace Capa_Presentacion_WPF.ViewModels.Configuracion
{
    public class DescuentoViewModel : IGeneric
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Descuento negsDesc = new Neg_Descuento();
        private Descuento Descuentosep;
        private string operacion;

        public ObservableCollection<Descuento> DatasDescuento { get; set; }

        public Descuento Descuento
        {
            get => Descuentosep;
            set
            {
                Descuentosep = value;
            }
        }
        public ICommand EditarCommandDesc { get; set; }
        public ICommand EliminarCommandDesc { get; set; }
        public ICommand CancelarCommandDesc { get; set; }
        public ICommand NuevoCommandDesc { get; set; }
        public ICommand GuardarCommandDesc { get; set; }
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
        }public string TextoBuscar { get; set; }
        public DescuentoViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommandDesc = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommandDesc = new RelayCommand(new Action(Cancelar));
            this.NuevoCommandDesc = new RelayCommand(new Action(Nuevo));
            this.GuardarCommandDesc = new RelayCommand(new Action(Guardar));
            this.EliminarCommandDesc = new ParamCommand(new Action<object>(Eliminar));

            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.Descuentosep = new Descuento();
        }

        private void Buscar()
        {
            ObservableCollection<Descuento> ls = new ObservableCollection<Descuento>();
            ls = DatasDescuento = DatasDescuento;
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DatasDescuento = DatasDescuento;
            }
            else
            {
                List<Descuento> lista = ls
                    .Where(w =>
                    w.TD_DESCR.ToUpper().Contains(TextoBuscar.ToUpper())).ToList();
                ObservableCollection<Descuento> o = new ObservableCollection<Descuento>(lista);
                DatasDescuento = o;
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
            saveFileDialog1.FileName = "Tipos de Descuento " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("TIPO DE DESCUENTO");
            dt.Columns.Add("PORCENTAJE");
            dt.Columns.Add("ESTADO");
            foreach (var p in DatasDescuento)
            {
                dt.Rows.Add(new object[] { p.id, p.TD_DESCR, p.TD_PORCENTAJE,(p.TD_ACT == 1) ? "Activo" : "Inactivo"});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE TIPOS DE DESCUENTO");
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
            saveFileDialog1.FileName = "Tipos de Descuento " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("TIPO DE DESCUENTO");
            dt.Columns.Add("PORCENTAJE");
            dt.Columns.Add("ESTADO");
            foreach (var p in DatasDescuento)
            {
                dt.Rows.Add(new object[] { p.id, p.TD_DESCR, p.TD_PORCENTAJE, (p.TD_ACT == 1) ? "Activo" : "Inactivo" });
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
            this.Descuento = this.DatasDescuento.Where(w => w.id == (int)id).FirstOrDefault();
        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            //DialogHost.OpenDialogCommand.Execute(DateTime.Now,null) ;
            this.Operacion = "Nuevo";
            this.Descuento = new Descuento();
        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(Descuentosep.TD_DESCR))
            {
                Descuento sdesc = this.Descuentosep;
                var _id = sdesc.id;
                string _mensaje = "";
                bool result = negsDesc.SetDescuento((_id == 0 ? 1 : 2), Descuentosep, ref _mensaje);
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
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "¿Desea eliminar el registro?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
                return;
            string _mensaje = "";

            Descuentosep.id = (int)id;
            bool result = negsDesc.SetDescuento(3, Descuentosep, ref _mensaje);
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
            DatasDescuento = negsDesc.GetSDesc();
        }
    }
}

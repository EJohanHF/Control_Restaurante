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
    public class SuperJesusViewModel : IGeneric
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_SuperJesus negsCat = new Neg_SuperJesus();
        private SJesus superjesus;
        private string operacion;

        public ObservableCollection<SJesus> DatasJesus { get; set; }

        public SJesus SJesus
        {
            get => superjesus;
            set
            {
                superjesus = value;
            }
        }
        public ICommand EditarCommandListo { get; set; }
        public ICommand EliminarCommandLila { get; set; }
        public ICommand CancelarCommandListo { get; set; }
        public ICommand NuevoCommandListo { get; set; }
        public ICommand GuardarCommandListo { get; set; }
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
        public SuperJesusViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommandListo = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommandListo = new RelayCommand(new Action(Cancelar));
            this.NuevoCommandListo = new RelayCommand(new Action(Nuevo));
            this.GuardarCommandListo = new RelayCommand(new Action(Guardar));
            this.EliminarCommandLila = new ParamCommand(new Action<object>(Eliminar));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));

        }

        private void Buscar()
        {
            ObservableCollection<SJesus> ls = new ObservableCollection<SJesus>();
            ls = DatasJesus = negsCat.GetSCat();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DatasJesus = negsCat.GetSCat();
            }
            else
            {
                List<SJesus> lista = ls
                    .Where(w =>
                    w.deno_pago.ToUpper().Contains(TextoBuscar.ToUpper())).ToList();
                ObservableCollection<SJesus> o = new ObservableCollection<SJesus>(lista);
                DatasJesus = o;
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
            saveFileDialog1.FileName = "Tipos de Pago " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("DENOMINACION TIPO PAGO");
            dt.Columns.Add("ESTADO");
            foreach (var p in DatasJesus)
            {
                dt.Rows.Add(new object[] { p.id, p.deno_pago, (p.tp_act == 1) ? "Activo" : "Inactivo" });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE TIPOS DE PAGO");
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
            saveFileDialog1.FileName = "Tipos de Pago " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("DENOMINACION TIPO PAGO");
            dt.Columns.Add("ESTADO");
            foreach (var p in DatasJesus)
            {
                dt.Rows.Add(new object[] { p.id, p.deno_pago, (p.tp_act == 1) ? "Activo" : "Inactivo" });
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
            this.SJesus = this.DatasJesus.Where(w => w.id == (int)id).FirstOrDefault();
        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            //DialogHost.OpenDialogCommand.Execute(DateTime.Now,null) ;
            this.Operacion = "Nuevo";
            this.SJesus = new SJesus();
        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(superjesus.deno_pago))
            {
                SJesus sjesus = this.SJesus;
                var _id = sjesus.id;
                string _mensaje = "";
                bool result = negsCat.SetsJesus((_id == 0 ? 1 : 2), sjesus, ref _mensaje);
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
            SJesus sjesus = new SJesus();
            sjesus.id = (int)id;
            bool result = negsCat.SetsJesus(3, sjesus, ref _mensaje);
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
          DatasJesus = negsCat.GetSCat();
        }
    }
}

using Capa_Entidades;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio;
using Capa_Negocio.Carta;
using Capa_Negocio.Funciones_Globales;
using Capa_Presentacion_WPF.Views.Dialogs;
using Capa_Presentacion_WPF.Views.Dialogs.DialogVentaporDia;
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

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Carta
{
    public class DetDescripcionesViewModel : IGeneric
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Combo negCombo = new Neg_Combo();
        Neg_DetDescripciones negdetdesc = new Neg_DetDescripciones();
        private DetDescripciones detdescripciones;
        private string operacion;
        public ObservableCollection<DetDescripciones> DatasDetDescripciones { get; set; }
        public List<Ent_Combo> ComboTitle { get; set; }
        public DetDescripciones DetDescripciones
        {
            get => detdescripciones;
            set
            {
                detdescripciones = value;
            }
        }
        public ICommand EditarCommandDet { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand OpenDialogDescriptionCommand { get; set; }
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
        public DetDescripcionesViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommandDet = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ComboTitle = negCombo.GetComboTitle();
            this.OpenDialogDescriptionCommand = new RelayCommand(new Action(DailogDescripciones));

        }

        private void Buscar()
        {
            ObservableCollection<DetDescripciones> ls = new ObservableCollection<DetDescripciones>();
            ls = DatasDetDescripciones = negdetdesc.GetDetDes();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DatasDetDescripciones = negdetdesc.GetDetDes();
            }
            else
            {
                List<DetDescripciones> lista = ls
                    .Where(w =>
                    w.TITLE_DESCRIPTION.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.DET_DESCRIPTION.ToUpper().Contains(TextoBuscar.ToUpper())
                    ).ToList();
                ObservableCollection<DetDescripciones> o = new ObservableCollection<DetDescripciones>(lista);
                DatasDetDescripciones = o;
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
            saveFileDialog1.FileName = "Detalle de Descripciones " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("TITULO DE DESCRIPCION");
            dt.Columns.Add("DETALLE DE DESCRIPCION");
            foreach (var p in DatasDetDescripciones)
            {
                dt.Rows.Add(new object[] { p.ID, p.TITLE_DESCRIPTION, p.DET_DESCRIPTION});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE DETALLE DE DESCRIPCION");
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
            saveFileDialog1.FileName = "Detalle de Descripciones " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("TITULO DE DESCRIPCION");
            dt.Columns.Add("DETALLE DE DESCRIPCION");
            foreach (var p in DatasDetDescripciones)
            {
                dt.Rows.Add(new object[] { p.ID, p.TITLE_DESCRIPTION, p.DET_DESCRIPTION });
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



        private void DailogDescripciones()
        {
            var reporteF = new DialogXstore
            {

            };
            DialogHost.Show(reporteF, "RootDialog");
        }


        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.DetDescripciones = this.DatasDetDescripciones.Where(w => w.ID == (int)id).FirstOrDefault();
        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            //DialogHost.OpenDialogCommand.Execute(DateTime.Now,null) ;
            this.Operacion = "Nuevo";
            this.DetDescripciones = new DetDescripciones();
        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(detdescripciones.DET_DESCRIPTION))
            {
                DetDescripciones detdescripciones = this.DetDescripciones;
                var _id = detdescripciones.ID;
                string _mensaje = "";
                bool result = negdetdesc.SetsDetDescripciones((_id == 0 ? 1 : 2), detdescripciones, ref _mensaje);
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
            }
            return;


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
            DetDescripciones detdescripciones = new DetDescripciones();
            detdescripciones.ID = (int)id;
            bool result = negdetdesc.SetsDetDescripciones(3, detdescripciones, ref _mensaje);
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
            DatasDetDescripciones = negdetdesc.GetDetDes();
        }
    }
}

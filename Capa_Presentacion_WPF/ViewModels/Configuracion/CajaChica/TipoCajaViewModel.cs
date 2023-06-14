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
using Capa_Entidades;
using Capa_Negocio;
using Capa_Negocio.Funciones_Globales;
using System.IO;
using System.Windows.Forms;
using System.Data;
using ExportToExcel;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion
{
    public class TipoCajaViewModel : IGeneric
    {
        Neg_Combo negCombo = new Neg_Combo();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_TipoCaja negsTipoCaja = new Neg_TipoCaja();
        private TipoCaja TipoCajasep;
        private string operacion;

        public ObservableCollection<TipoCaja> DatasTipoCaja { get; set; }
        public List<Ent_Combo> ComboTipoCaja { get; set; }
        public TipoCaja TipoCaja

        {
            get => TipoCajasep;
            set
            {
                TipoCajasep = value;
            }
        }
        public ICommand EditarCommandDesc { get; set; }
        public ICommand EliminarCommandDesc { get; set; }
        public ICommand CancelarCommandTipoC { get; set; }
        public ICommand NuevoCommandDesc { get; set; }
        public ICommand GuardarCommandTipoC { get; set; }
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
        public TipoCajaViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommandDesc = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommandTipoC = new RelayCommand(new Action(Cancelar));
            this.NuevoCommandDesc = new RelayCommand(new Action(Nuevo));
            this.GuardarCommandTipoC = new RelayCommand(new Action(Guardar));
            this.EliminarCommandDesc = new ParamCommand(new Action<object>(Eliminar));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.TipoCajasep = new TipoCaja();
            this.ComboTipoCaja= negCombo.GetComboTipoCaja();
        }

        private void Buscar()
        {
            ObservableCollection<TipoCaja> ls = new ObservableCollection<TipoCaja>();
            ls = DatasTipoCaja = negsTipoCaja.GetSTipoCaja();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DatasTipoCaja = negsTipoCaja.GetSTipoCaja();
            }
            else
            {
                List<TipoCaja> lista = ls
                    .Where(w =>
                    w.TM_DESCR.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.MC_DESCR.ToUpper().Contains(TextoBuscar.ToUpper())
                    ).ToList();
                ObservableCollection<TipoCaja> o = new ObservableCollection<TipoCaja>(lista);
                DatasTipoCaja = o;
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
            saveFileDialog1.FileName = "Tipos de Movimiento Caja Chica " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("TIPO");
            dt.Columns.Add("MOVIMIENTO");
            dt.Columns.Add("ESTADO");
            foreach (var p in DatasTipoCaja)
            {
                dt.Rows.Add(new object[] { p.id, p.TM_DESCR, p.MC_DESCR, (p.MC_ACT == 1) ? "Activo" : "Inactivo" });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE TIPOS DE MOVIMIENTO DE CAJA CHICA");
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
            saveFileDialog1.FileName = "Tipos de Movimiento Caja Chica " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("TIPO");
            dt.Columns.Add("MOVIMIENTO");
            dt.Columns.Add("ESTADO");
            foreach (var p in DatasTipoCaja)
            {
                dt.Rows.Add(new object[] { p.id, p.TM_DESCR, p.MC_DESCR, (p.MC_ACT == 1) ? "Activo" : "Inactivo" });
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
            this.TipoCaja = this.DatasTipoCaja.Where(w => w.id == (int)id).FirstOrDefault();
        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            //DialogHost.OpenDialogCommand.Execute(DateTime.Now,null) ;
            this.Operacion = "Nuevo";
            this.TipoCaja = new TipoCaja();
        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(TipoCajasep.MC_DESCR))
            {
                TipoCaja sTipoCaja = this.TipoCajasep;
                var _id = sTipoCaja.id;
                string _mensaje = "";
                bool result = negsTipoCaja.SetTipoCaja((_id == 0 ? 1 : 2), TipoCajasep, ref _mensaje);
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

            TipoCajasep.id = (int)id;
            bool result = negsTipoCaja.SetTipoCaja(3, TipoCajasep, ref _mensaje);
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
            DatasTipoCaja = negsTipoCaja.GetSTipoCaja();
        }


    }
}

using Capa_Entidades.Models.Reportes.Pedido;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Reportes;
using ExportToExcel;
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

namespace Capa_Presentacion_WPF.ViewModels.Reporte.Pedidos
{
    public class Rpt_AdelantoViewModel :IGeneric
    {
        #region Negocio
        Neg_DpAdelantoxEmpleado negAdelanto = new Neg_DpAdelantoxEmpleado();
        #endregion
        #region Entidad
        public DpAdelantoxEmpleado SelectedDataFile { get; set; }
        private DpAdelantoxEmpleado histadel;
        public DpAdelantoxEmpleado DpAdelantoxEmpleado
        {
            get => histadel;
            set
            {
                histadel = value;
            }
        }
        #endregion
        #region Seleccion

        #endregion
        #region Objetos
        private string operacion;
        private DateTime fechadia;
        public string Descrip;
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
        public DateTime NombreDia
        {
            get
            {
                return this.DpAdelantoxEmpleado.dia == null ? DateTime.Now : this.DpAdelantoxEmpleado.dia;

            }
            set
            {
                if (value != null)
                {
                    this.DpAdelantoxEmpleado.dia = value;
                }
                fechadia = value;
            }
        }
        #endregion
        #region Commands
        public ICommand BuscarporfechasCommand { get; set; }
        public ICommand ExportarPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand RbtTagCommand { get; set; }
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
        #endregion
        #region Listas
        public ObservableCollection<DpAdelantoxEmpleado> DataAdelantoEmpleado { get; set; }
        public ObservableCollection<DpAdelantoxEmpleado> DataAdelantoEmpleado2 { get; set; }
        public ObservableCollection<DpAdelantoxEmpleado> DataTotalAdelantoEmpleado { get; set; }
        #endregion
        #region radiobuton
        public string rbbtdia { get; set; }
        public string rbbtmes { get; set; }
        public string rbbtdesdehasta { get; set; }
        public string Idradiobuton { get; set; }
        void valorbt()
        {
            this.rbbtdia = "1";
            this.rbbtmes = "2";
            this.rbbtdesdehasta = "3";
        }
        #endregion
        
        public Rpt_AdelantoViewModel()
        {
            this.Operacion = "Lista";
            valorbt();
            this.DpAdelantoxEmpleado = new DpAdelantoxEmpleado();
            this.BuscarporfechasCommand = new RelayCommand(new Action(buscarporfechas));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ExportarPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.RbtTagCommand = new ParamCommand(new Action<object>(IdRadiobuton));
            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = this.SelectedDataFile;
                if (set != null)
                {
                    string nom = set.EMPL_NOM;
                    List<DpAdelantoxEmpleado> dd = new List<DpAdelantoxEmpleado>();
                    dd = DataAdelantoEmpleado2.Where(w => w.EMPL_NOM == nom).ToList();
                    ObservableCollection<DpAdelantoxEmpleado> d = new ObservableCollection<DpAdelantoxEmpleado>(dd);
                    DataAdelantoEmpleado = d;
                }
            });
        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        private void buscarporfechas()
        {
            string desdet = Convert.ToDateTime(this.DpAdelantoxEmpleado.desde).ToString("yyyy-MM-dd");
            string hastat = Convert.ToDateTime(this.DpAdelantoxEmpleado.hasta).ToString("yyyy-MM-dd");
            string mes = Convert.ToDateTime(this.DpAdelantoxEmpleado.mes).ToString("yyyy-MM-dd");
            string dia = Convert.ToDateTime(this.DpAdelantoxEmpleado.dia).ToString("yyyy-MM-dd");
            if (this.Idradiobuton == "1")
            {
                this.DataAdelantoEmpleado = negAdelanto.GetAdelantoxEmpleado(null, null, null, dia);
                this.DataAdelantoEmpleado2 = negAdelanto.GetAdelantoxEmpleado(null, null, null, dia);
                this.DataTotalAdelantoEmpleado = negAdelanto.GetTotalAdelantoxEmpleado(null, null, dia);
                if (DataAdelantoEmpleado == null || DataAdelantoEmpleado.Count == 0)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay datos", 2);
                }
            }
            else
            {
                this.DataAdelantoEmpleado = negAdelanto.GetAdelantoxEmpleado(desdet, hastat, null, null);
                this.DataAdelantoEmpleado2 = negAdelanto.GetAdelantoxEmpleado(desdet, hastat, null, null);
                this.DataTotalAdelantoEmpleado = negAdelanto.GetTotalAdelantoxEmpleado(desdet, hastat, null);
                if (DataAdelantoEmpleado == null || DataAdelantoEmpleado.Count == 0)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay datos", 2);
                }
            }
        }
        private void IdRadiobuton(object id)
        {
            this.Idradiobuton = id.ToString();
        }
        private void ExportarPDF()
        {
            string fecha1 = Convert.ToDateTime(this.DpAdelantoxEmpleado.dia).ToString("yyyy-MM-dd");
            string fecha2 = Convert.ToDateTime(this.DpAdelantoxEmpleado.mes).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.DpAdelantoxEmpleado.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.DpAdelantoxEmpleado.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            if (this.Idradiobuton == "1")
            {
                saveFileDialog1.FileName = "Historial_AdelantoxEmpleado_" + fecha1;
            }
            else if (this.Idradiobuton == "2")
            {
                saveFileDialog1.FileName = "Historial_AdelantoxEmpleado_" + fecha2;
            }
            else
            {
                saveFileDialog1.FileName = "Historial_AdelantoxEmpleado_" + fecha3 + "_" + fecha4;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("EMPLEADO");
            dt.Columns.Add("MONTO");
            dt.Columns.Add("DESCRIPCION");
            dt.Columns.Add("FECHA");
            foreach (var p in DataAdelantoEmpleado)
            {
                dt.Rows.Add(new object[] { p.id, p.EMPL_NOM, p.CC_MONTO, p.CC_DESCR, p.CC_F_CREATE });
            }
            if (dt.Rows.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        string direccion = saveFileDialog1.InitialDirectory;
                        string nombrearchivo = saveFileDialog1.FileName;
                        // Code to write the stream goes here.

                        string ubicacion = saveFileDialog1.FileName;
                        myStream.Close();
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "Historial de Adelanto x Empleado");
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
            string fecha1 = Convert.ToDateTime(this.DpAdelantoxEmpleado.dia).ToString("yyyy-MM-dd");
            string fecha2 = Convert.ToDateTime(this.DpAdelantoxEmpleado.mes).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.DpAdelantoxEmpleado.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.DpAdelantoxEmpleado.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            if (this.Idradiobuton == "1")
            {
                saveFileDialog1.FileName = "Historial_AdelantoxEmpleado_" + fecha1;
            }
            else if (this.Idradiobuton == "2")
            {
                saveFileDialog1.FileName = "Historial_AdelantoxEmpleado_" + fecha2;
            }
            else
            {
                saveFileDialog1.FileName = "Historial_AdelantoxEmpleado_" + fecha3 + "_" + fecha4;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("EMPLEADO");
            dt.Columns.Add("MONTO");
            dt.Columns.Add("DESCRIPCION");
            dt.Columns.Add("FECHA");
            foreach (var p in DataAdelantoEmpleado)
            {
                dt.Rows.Add(new object[] { p.id, p.EMPL_NOM, p.CC_MONTO, p.CC_DESCR, p.CC_F_CREATE });
            }
            if (dt.Rows.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        string direccion = saveFileDialog1.InitialDirectory;
                        string nombrearchivo = saveFileDialog1.FileName;
                        // Code to write the stream goes here.

                        string ubicacion = saveFileDialog1.FileName;
                        myStream.Close();
                        CreateExcelFile.CreateExcelDocument(dt, ubicacion);
                        //DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }
        private void GetLista()
        {
            string desdet = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string hastat = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string mes = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string dia = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");

            this.DataAdelantoEmpleado = negAdelanto.GetAdelantoxEmpleado(null, null, null, null);
            this.DataAdelantoEmpleado2 = negAdelanto.GetAdelantoxEmpleado(null, null, null, null);
            this.DataTotalAdelantoEmpleado = negAdelanto.GetTotalAdelantoxEmpleado(null, null, null);
        }
    }
}

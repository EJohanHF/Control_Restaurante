using Capa_Entidades.Models.Reportes.Pedido;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Reportes;
using ExportToExcel;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public class HistorialDescuentoViewModel : IGeneric
    {
        Neg_DpDescuentos negHDesc = new Neg_DpDescuentos();
        private DpDescuento histdesct;
        private string operacion;
        
        private DateTime fechadia;
        public string Descrip;
        public ObservableCollection<DpDescuento> DataDescuentos { get; set; }
        public ObservableCollection<DpDescuento> DataTotalDescuentos { get; set; }
        public ObservableCollection<DpDescuento> DataDescuentos1 { get; set; }
        public ObservableCollection<DpDescuento> DataTipoDescuentos { get; set; }
        public DpDescuento SelectedDataFile { get; set; }
        public ICommand BuscarporfechasCommand { get; set; }
        public ICommand ExportarPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand RbtTagCommand { get; set; }
        public DpDescuento DpDescuento
        {
            get => histdesct;
            set
            {
                histdesct = value;
            }
        }

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
                return this.DpDescuento == null ? DateTime.Now : this.DpDescuento.dia;

            }
            set
            {
                if (value != null)
                {
                    this.DpDescuento.dia = value;
                }
                fechadia = value;
            }
        }
        public HistorialDescuentoViewModel()
        {
            this.Operacion ="Lista";
            valorbt();
            this.DpDescuento = new DpDescuento();
            this.BuscarporfechasCommand = new RelayCommand(new Action(buscarporfechas));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ExportarPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.RbtTagCommand = new ParamCommand(new Action<object>(IdRadiobuton));
            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = this.SelectedDataFile;
                if (set != null)
                {
                    int iddesc = set.idDescuento;
                    List<DpDescuento> dd = new List<DpDescuento>();
                    dd = DataTotalDescuentos.Where(w => w.TD_ID == iddesc).ToList();
                    ObservableCollection<DpDescuento> d = new ObservableCollection<DpDescuento>(dd);
                    DataDescuentos = d;
                }
            });
        }
        private void buscarporfechas()
        {
            string desdet = Convert.ToDateTime(this.DpDescuento.desde).ToString("yyyy-MM-dd");
            string hastat = Convert.ToDateTime(this.DpDescuento.hasta).ToString("yyyy-MM-dd");
            string mes = Convert.ToDateTime(this.DpDescuento.mes).ToString("yyyy-MM-dd");
            string dia = Convert.ToDateTime(this.DpDescuento.dia).ToString("yyyy-MM-dd");
            if (this.Idradiobuton == "1")
            {
                this.DataDescuentos = negHDesc.GetPdDescuento(null, null, null, dia);
                this.DataTotalDescuentos = negHDesc.GetPdDescuento(null, null, null, dia);
                this.DataTipoDescuentos = negHDesc.GetTipoPdDescuento(null, null, dia);
                if (DataDescuentos == null || DataDescuentos.Count == 0)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay datos", 2);
                }
            }
            else
            {
                this.DataDescuentos = negHDesc.GetPdDescuento(desdet, hastat, null, null);
                this.DataTotalDescuentos = negHDesc.GetPdDescuento(desdet, hastat, null, null);
                this.DataTipoDescuentos = negHDesc.GetTipoPdDescuento(desdet, hastat, null);
                if (DataDescuentos == null || DataDescuentos.Count == 0)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay datos", 2);
                }
            }
        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        private void IdRadiobuton(object id)
        {
            this.Idradiobuton = id.ToString();
        }
        private void ExportarPDF()
        {
            string fecha1 = Convert.ToDateTime(this.DpDescuento.dia).ToString("yyyy-MM-dd");
            string fecha2 = Convert.ToDateTime(this.DpDescuento.mes).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.DpDescuento.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.DpDescuento.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            if (this.Idradiobuton == "1")
            {
                saveFileDialog1.FileName = "Historial_Descuento_" + fecha1;
            }
            else if (this.Idradiobuton == "2")
            {
                saveFileDialog1.FileName = "Historial_Descuento_" + fecha2;
            }
            else
            {
                saveFileDialog1.FileName = "Historial_Descuento_" + fecha3 + "_" + fecha4;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("N° DIARIO");
            dt.Columns.Add("MESA");
            dt.Columns.Add("DESC");
            dt.Columns.Add("IMPORTE");
            dt.Columns.Add("T.PAGO");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("F. PED.");
            dt.Columns.Add("EMPLEADO");
            dt.Columns.Add("CARGO");
            dt.Columns.Add("F. CIERRE");
            dt.Columns.Add("N° CIERRE");
            foreach (var p in DataDescuentos)
            {
                dt.Rows.Add(new object[] { p.ID, p.PED_NUM_DIARIO, p.M_NOM, p.DP_DESCU, p.DP_IMPORT, p.DENO_PAGO, p.DESC_EST, p.DP_FEC_REG, p.EMPL_NOM, p.NOM_CARGO, p.DC_F_CREATE, p.IDDC });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "Historial de Descuento");
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
            string fecha1 = Convert.ToDateTime(this.DpDescuento.dia).ToString("yyyy-MM-dd");
            string fecha2 = Convert.ToDateTime(this.DpDescuento.mes).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.DpDescuento.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.DpDescuento.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            if (this.Idradiobuton == "1")
            {
                saveFileDialog1.FileName = "Historial_Descuento_" + fecha1;
            }
            else if (this.Idradiobuton == "2")
            {
                saveFileDialog1.FileName = "Historial_Descuento_" + fecha2;
            }
            else
            {
                saveFileDialog1.FileName = "Historial_Descuento_" + fecha3 + "_" + fecha4;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("PED_NUM_DIARIO");
            dt.Columns.Add("M_NOM");
            dt.Columns.Add("DP_DESCU");
            dt.Columns.Add("DP_IMPORT");
            dt.Columns.Add("DENO_PAGO");
            dt.Columns.Add("DESC_EST");
            dt.Columns.Add("DP_FEC_REG");
            dt.Columns.Add("EMPL_NOM");
            dt.Columns.Add("NOM_CARGO");
            dt.Columns.Add("DC_F_CREATE");
            dt.Columns.Add("IDDC");
            foreach (var p in DataDescuentos)
            {
                dt.Rows.Add(new object[] { p.ID, p.PED_NUM_DIARIO, p.M_NOM, p.DP_DESCU, p.DP_IMPORT, p.DENO_PAGO, p.DESC_EST, p.DP_FEC_REG, p.EMPL_NOM, p.NOM_CARGO, p.DC_F_CREATE, p.IDDC });
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

            this.DataDescuentos = negHDesc.GetPdDescuento(null, null, null, null);
            //this.DataTipoDescuentos = negHDesc.GetTipoPdDescuento(null, null, null, null);
        }
    }
}

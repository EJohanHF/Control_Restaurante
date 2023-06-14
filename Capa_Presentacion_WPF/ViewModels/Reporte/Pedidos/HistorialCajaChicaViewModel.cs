using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Reportes.VentasporDia;
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
    public  class HistorialCajaChicaViewModel:IGeneric
    {
        Neg_DpCajaChica negCajaCh = new Neg_DpCajaChica();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        private string operacion;
        private CajaChica cajachica;
        private DateTime fechadia;
        public string Descrip;
        public ObservableCollection<CajaChica> DataCajaChica { get; set; }
        public ICommand BuscarporfechasCommand { get; set; }
        public ICommand ExportarPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand RbtTagCommand { get; set; }
        public decimal Cc_ingreso { get; set; }
        public decimal Cc_egreso { get; set; }
        public decimal Cc_Saldo { get; set; }
        public CajaChica CajaChica
        {
            get => cajachica;
            set
            {
                cajachica= value;
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
                return this.CajaChica == null ? DateTime.Now : this.CajaChica.dia;

            }
            set
            {
                if (value != null)
                {
                    this.CajaChica.dia = value;
                }
                fechadia = value;
            }
        }
        public HistorialCajaChicaViewModel()
        {
            this.Operacion = "Lista";
            valorbt();
            this.CajaChica = new CajaChica();
            this.BuscarporfechasCommand = new RelayCommand(new Action(buscarporfechas));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ExportarPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.RbtTagCommand = new ParamCommand(new Action<object>(IdRadiobuton));
            //m_RowClickCommand = new DelegateCommand(() =>
            //{
            //    var set = this.SelectedDataFile;
            //    if (set != null)
            //    {
            //        this.Descrip = set.Descripcion;

            //        //Application.Current.Properties["id_empre"] = set.idEmpr;
            //        this.DataDescuentos = new ObservableCollection<DpDescuento>(DataDescuentos.Where(w => w.TD_DESCR == Descrip));

            //    }

            //});
        }
        private void buscarporfechas()
        {
            string desdet = Convert.ToDateTime(this.CajaChica.desde).ToString("yyyy-MM-dd");
            string hastat = Convert.ToDateTime(this.CajaChica.hasta).ToString("yyyy-MM-dd");
            string dia = Convert.ToDateTime(this.CajaChica.dia).ToString("yyyy-MM-dd");
            if (this.Idradiobuton == "1")
            {
                this.DataCajaChica = negCajaCh.GetCajaChica(null, null,  dia);
                if (DataCajaChica.Count != 0)
                {
                    Cc_ingreso =  DataCajaChica.Where(w => Convert.ToInt32(w.IDTMCAJA) == 1).Sum(s => Convert.ToDecimal(s.CC_MONTO));
                    Cc_egreso =  DataCajaChica.Where(w => Convert.ToInt32(w.IDTMCAJA) == 2).Sum(s => Convert.ToDecimal(s.CC_MONTO));
                    Cc_Saldo = Cc_ingreso - Cc_egreso;
                }
                else
                {
                    Cc_ingreso = 0;
                    Cc_egreso = 0;
                    Cc_Saldo = 0;

                }
            }
            else if (this.Idradiobuton == "2")
            {
                //this.DataDescuentos = negHDesc.GetPdDescuento(null, null, mes, null);
                //this.DataTipoDescuentos = negHDesc.GetTipoPdDescuento(desdet, hastat, null);
            }
            else
            {
                this.DataCajaChica = negCajaCh.GetCajaChica(desdet, hastat, null);
                if (DataCajaChica.Count == 0)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay datos", 2);
                }
                if (DataCajaChica.Count != 0)
                {
                    Cc_ingreso = DataCajaChica.Where(w => Convert.ToInt32(w.IDTMCAJA) == 1).Sum(s => Convert.ToDecimal(s.CC_MONTO));
                    Cc_egreso = DataCajaChica.Where(w => Convert.ToInt32(w.IDTMCAJA) == 2).Sum(s => Convert.ToDecimal(s.CC_MONTO));
                    Cc_Saldo = Cc_ingreso - Cc_egreso;
                }
                else
                {
                    Cc_ingreso = 0;
                    Cc_egreso = 0;
                    Cc_Saldo = 0;

                }
            }

        }
        private void IdRadiobuton(object id)
        {
            this.Idradiobuton = id.ToString();
        }



        private void ExportarPDF()
        {
            string fecha1 = Convert.ToDateTime(this.CajaChica.dia).ToString("yyyy-MM-dd");
            //string fecha2 = Convert.ToDateTime(this.CajaChica.mes).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.CajaChica.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.CajaChica.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            if (this.Idradiobuton == "1")
            {
                saveFileDialog1.FileName = "Historial_CajaChica_" + fecha1;
            }
            else if (this.Idradiobuton == "2")
            {
                //saveFileDialog1.FileName = "Historial_Descuento_" + fecha2;
            }
            else
            {
                saveFileDialog1.FileName = "Historial_CajaChica_" + fecha3 + "_" + fecha4;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("TIPO");
            dt.Columns.Add("DESCRIPCIÓN");
            dt.Columns.Add("MONTO");
            dt.Columns.Add("DESC. CAJA CHICA");
            dt.Columns.Add("REPONSABLE");
            dt.Columns.Add("FECHA");
            foreach (var p in DataCajaChica)
            {
                dt.Rows.Add(new object[] { p.TM_DESCR, p.MC_DESCR, p.CC_MONTO, p.CC_DESCR, p.EMPL_NOM, p.CC_F_CREATE });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "Historial de CajaChica");
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
            string fecha1 = Convert.ToDateTime(this.CajaChica.dia).ToString("yyyy-MM-dd");
            //string fecha2 = Convert.ToDateTime(this.CajaChica.mes).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.CajaChica.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.CajaChica.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            if (this.Idradiobuton == "1")
            {
                saveFileDialog1.FileName = "Historial_CajaChica_" + fecha1;
            }
            else if (this.Idradiobuton == "2")
            {
                //saveFileDialog1.FileName = "Historial_Descuento_" + fecha2;
            }
            else
            {
                saveFileDialog1.FileName = "Historial_CajaChica_" + fecha3 + "_" + fecha4;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("TIPO");
            dt.Columns.Add("DESCRIPCIÓN");
            dt.Columns.Add("MONTO");
            dt.Columns.Add("DESC. CAJA CHICA");
            dt.Columns.Add("REPONSABLE");
            dt.Columns.Add("FECHA");
            foreach (var p in DataCajaChica)
            {
                dt.Rows.Add(new object[] { p.TM_DESCR,p.MC_DESCR, p.CC_MONTO, p.CC_DESCR,p.EMPL_NOM,p.CC_F_CREATE });
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
         
        }
    }
}

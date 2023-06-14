using Capa_Entidades.Models.Reportes;
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
    class PdAnuladosViewModel : IGeneric
    {
        Neg_DpAnulados negAnul = new Neg_DpAnulados();
        private string operacion;
        private DpAnulados _dpAnulados;
        private DateTime desde;
        private DateTime hasta;
        private DateTime fechadia;
        private string nombredia;
        public ICommand BuscarporfechasCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand RbtTagCommand { get; set; }
        public ICommand ExportarPDFCommand { get; set; }
        public ObservableCollection<DpAnulados> DataAnualdos { get; set; }
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
                if (value != null)
                {
                    operacion = "Lista";
                    GetLista();
                }
                operacion = value;
            }
        }
        public DpAnulados DpAnulados
        {
            get => _dpAnulados;
            set
            {
                _dpAnulados = value;
            }
        }
        public DateTime Desde
        {
            get => desde;
            set
            {
                desde = value;
            }
        }
        public DateTime Hasta
        {
            get => hasta;
            set
            {
                hasta = value;
            }
        }
        public string SelectFormatday
        {
            get => nombredia;
            set
            {

                //DateTime fecha = Convert.ToDateTime(this.DpAnulados.dia);
                //value = fecha.ToString("dddd");
                nombredia = value;
            }
        }
        public DateTime NombreDia
        {
            get
            {
                return this.DpAnulados == null ? DateTime.Now : this.DpAnulados.dia;

            }
            set
            {
                if (value != null)
                {
                    this.DpAnulados.dia = value;
                }
                DateTime fecha = Convert.ToDateTime(this.DpAnulados.dia);
                SelectFormatday = fecha.ToString("dddd");
                fechadia = value;
            }
        }
        private ICommand m_RowClickCommand;
        public DpAnulados SelectedDataFile { get; set; }
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
                m_Action = action;
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
        public ObservableCollection<DpVentas> DataDetPedido { get; set; }
        public object IdPed { get; set; }
        public PdAnuladosViewModel()
        {
            this.Operacion = "Lista";
            valorbt();
            this.DpAnulados = new DpAnulados();
            this.BuscarporfechasCommand = new RelayCommand(new Action(buscarporfechas));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.RbtTagCommand = new ParamCommand(new Action<object>(IdRadiobuton));
            this.ExportarPDFCommand = new RelayCommand(new Action(ExportarPDF));
            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = SelectedDataFile;
                if (set != null)
                {
                    IdPed = set.IDPED;
                    DataDetPedido = negAnul.GetDetPedidoAnulado(Convert.ToString(IdPed));
                    //Application.Current.Properties["id_ped"] = set.id_ped;

                    //ObservableCollection<BoletaFactura> DataBolFactura = new ObservableCollection<BoletaFactura>(DataBolFac);
                }

            });
        }
        private void buscarporfechas()
        {
            string desdet = Convert.ToDateTime(this.DpAnulados.desde).ToString("yyyy-MM-dd");
            string hastat = Convert.ToDateTime(this.DpAnulados.hasta).ToString("yyyy-MM-dd");
            string mes = Convert.ToDateTime(this.DpAnulados.mes).ToString("yyyy-MM-dd");
            string dia = Convert.ToDateTime(this.DpAnulados.dia).ToString("yyyy-MM-dd");
            if (this.Idradiobuton == "1")
            {
                //MessageBox.Show("falta referenciar");
                this.DataAnualdos = negAnul.GetPdAnulados(null, null, null, dia);
            }
            else if (this.Idradiobuton == "2")
            {
                this.DataAnualdos = negAnul.GetPdAnulados(null, null, mes, null);
                //MessageBox.Show("falta referenciar");
            }
            else
            {
                this.DataAnualdos = negAnul.GetPdAnulados(desdet, hastat, null, null);
                if (DataAnualdos.Count == 0)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay datos", 2);
                }
            }

        }
        private void IdRadiobuton(object id)
        {
            this.Idradiobuton = id.ToString();
        }
        private void ExportarExcel()
        {
            string fecha1 = Convert.ToDateTime(this.DpAnulados.dia).ToString("yyyy-MM-dd");
            string fecha2 = Convert.ToDateTime(this.DpAnulados.mes).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.DpAnulados.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.DpAnulados.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            if (this.Idradiobuton == "1")
            {
                //MessageBox.Show("falta referenciar");
                saveFileDialog1.FileName = "Historial_Anulados_" + fecha1;
            }
            else if (this.Idradiobuton == "2")
            {
                saveFileDialog1.FileName = "Historial_Anulados_" + fecha2;
                //MessageBox.Show("falta referenciar");
            }
            else
            {
                saveFileDialog1.FileName = "Historial_Anulados_" + fecha3 + "_" + fecha4;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("IDPED");
            dt.Columns.Add("N° DIARIO");
            dt.Columns.Add("MESA");
            dt.Columns.Add("TOTAL");
            dt.Columns.Add("CAJA");
            dt.Columns.Add("EMPLEADO");
            dt.Columns.Add("USUARIO CARGO");
            dt.Columns.Add("NOM CLIENTE");
            foreach (var p in DataAnualdos)
            {
                dt.Rows.Add(new object[] { p.IDPED, p.PED_NUM_DIARIO, p.M_NOM, p.PED_TOTAL, p.USU_NOM, p.EMPL_NOM, p.NOM_CARGO, p.nomllevar });
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
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        private void GetLista()
        {
            string desdet = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string hastat = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string mes = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string dia = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            this.DataAnualdos = negAnul.GetPdAnulados(null, null, null, null);
        }
        private void ExportarPDF()
        {
            string fecha1 = Convert.ToDateTime(this.DpAnulados.dia).ToString("yyyy-MM-dd");
            string fecha2 = Convert.ToDateTime(this.DpAnulados.mes).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.DpAnulados.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.DpAnulados.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            if (this.Idradiobuton == "1")
            {
                //MessageBox.Show("falta referenciar");
                saveFileDialog1.FileName = "Historial_Anulados_" + fecha1;
            }
            else if (this.Idradiobuton == "2")
            {
                saveFileDialog1.FileName = "Historial_Anulados_" + fecha2;
                //MessageBox.Show("falta referenciar");
            }
            else
            {
                saveFileDialog1.FileName = "Historial_Anulados_" + fecha3 + "_" + fecha4;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("IDPED");
            dt.Columns.Add("N° DIARIO");
            dt.Columns.Add("MESA");
            dt.Columns.Add("TOTAL");
            dt.Columns.Add("CAJA");
            dt.Columns.Add("EMPLEADO");
            dt.Columns.Add("USUARIO CARGO");
            dt.Columns.Add("NOM CLIENTE");
            foreach (var p in DataAnualdos)
            {
                dt.Rows.Add(new object[] { p.IDPED, p.PED_NUM_DIARIO, p.M_NOM, p.PED_TOTAL, p.USU_NOM, p.EMPL_NOM, p.NOM_CARGO, p.nomllevar });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "Historial de Platos  Anulados");
                    }
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }

    }
}

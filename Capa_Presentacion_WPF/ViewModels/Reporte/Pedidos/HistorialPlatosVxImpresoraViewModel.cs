using Capa_Entidades;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Reportes.VentasporDia;
using ExportToExcel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.Pedidos
{
    public  class HistorialPlatosVxImpresoraViewModel:IGeneric
    {
        Neg_DpVentPlatosxImpresora negCajaCh = new Neg_DpVentPlatosxImpresora();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Detalle_pedido negDetVent = new Neg_Detalle_pedido();
        Neg_Parametros negParametros = new Neg_Parametros();
        private string operacion;
        private VentaPlatosxImpresora ventasximpresora;
        //private DateTime fechadia;
        private DateTime fechadia;
        private DateTime fechadesde;
        private DateTime fechahasta;
        public string Descrip;
        public Ent_Combo _selectedVentas;
        public ObservableCollection<VentaPlatosxImpresora> DataVentasxImpresora { get; set; }
        public ObservableCollection<VentaPlatosxImpresora> DataVentasxImpresora1 { get; set; }
        public List<Ent_Combo> ComboImpresora { get; set; }
        public ICommand BuscarporfechasCommand { get; set; }
        public ICommand ExportarPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand ImprimirCommand { get; set; }
        public ICommand RbtTagCommand { get; set; }
        public decimal T_Platos { get; set; }
        public decimal T_Monto { get; set; }
        public string T_Top1 { get; set; }
        public object IDIMPRE { get; set; }
        public VentaPlatosxImpresora VentasxImpresora
        {
            get => ventasximpresora;
            set
            {
                ventasximpresora = value;
            }
        }
        public Ent_Combo SelectedImpre
        {
            get => _selectedVentas;
            set
            {
                if (value != null)
                {
                    this.IDIMPRE = value.id;
                }
                _selectedVentas = value;
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
                return this.VentasxImpresora == null ? DateTime.Now : this.VentasxImpresora.dia;

            }
            set
            {
                if (value != null)
                {
                    this.VentasxImpresora.dia = value;
                }
                fechadia = value;
            }
        }
        public DateTime NombreDesde
        {
            get
            {
                return this.VentasxImpresora == null ? DateTime.Now : this.VentasxImpresora.desde;
            }
            set
            {
                if (value != null)
                {
                    this.VentasxImpresora.desde = value;
                }
                fechadesde = value;
            }
        }
        public DateTime NombreHasta
        {
            get
            {
                return this.VentasxImpresora == null ? DateTime.Now : this.VentasxImpresora.hasta;

            }
            set
            {
                if (value != null)
                {
                    this.VentasxImpresora.hasta = value;
                }
                fechahasta = value;
            }
        }
        public HistorialPlatosVxImpresoraViewModel()
        {
            this.Operacion = "Lista";
            valorbt();
            this.VentasxImpresora = new VentaPlatosxImpresora();
            this.BuscarporfechasCommand = new RelayCommand(new Action(buscarporfechas));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ExportarPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ImprimirCommand = new RelayCommand(new Action(Imprimir));
            this.RbtTagCommand = new ParamCommand(new Action<object>(IdRadiobuton));
            this.ComboImpresora = negCombo.GetComboImpresorasReporte();
            this.DataVentasxImpresora1 = new ObservableCollection<VentaPlatosxImpresora>();
        }
        private void buscarporfechas()
        {
            string desdet = Convert.ToDateTime(this.VentasxImpresora.desde).ToString("yyyy-MM-dd");
            string hastat = Convert.ToDateTime(this.VentasxImpresora.hasta).ToString("yyyy-MM-dd");
            string dia = Convert.ToDateTime(this.VentasxImpresora.dia).ToString("yyyy-MM-dd");
            if (this.Idradiobuton == "1")
            {
                if (IDIMPRE != null)
                {
                    this.DataVentasxImpresora = negCajaCh.GetVentaPlatosxImp(null, null, dia, Convert.ToInt32(IDIMPRE));
                    //this.DataVentasxImpresora = new ObservableCollection<VentaPlatosxImpresora>(DataVentasxImpresora.Where(w => w.IDIMP == Convert.ToInt32(IDIMPRE)));
                    if (DataVentasxImpresora.Count != 0)
                    {
                        T_Platos = DataVentasxImpresora.Where(w => w.IDIMP == Convert.ToInt32(IDIMPRE)).Sum(s => Convert.ToInt32(s.DP_CANT));
                        T_Monto = DataVentasxImpresora.Where(w => w.IDIMP == Convert.ToInt32(IDIMPRE)).Sum(s => Convert.ToDecimal(s.DP_IMPORT));
                        T_Top1 = DataVentasxImpresora.Where(w => w.IDIMP == Convert.ToInt32(IDIMPRE)).OrderByDescending(O=> O.DP_CANT).FirstOrDefault().DP_DESCRIP;
                    }
                    else
                    {
                        T_Platos = 0;
                        T_Monto = 0;
                        T_Top1 = "";
                    }
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Seleccione Impresora", 2);
                }
            }
            else
            {
                if (IDIMPRE != null)
                {
                    this.DataVentasxImpresora = negCajaCh.GetVentaPlatosxImp(desdet, hastat, null, Convert.ToInt32(IDIMPRE));
                    //this.DataVentasxImpresora = new ObservableCollection<VentaPlatosxImpresora>(DataVentasxImpresora.Where(w => w.IDIMP == Convert.ToInt32(IDIMPRE)));
                    if (DataVentasxImpresora.Count != 0)
                    {
                        T_Platos = DataVentasxImpresora.Where(w => w.IDIMP == Convert.ToInt32(IDIMPRE)).Sum(s => Convert.ToInt32(s.DP_CANT));
                        T_Monto = DataVentasxImpresora.Where(w => w.IDIMP == Convert.ToInt32(IDIMPRE)).Sum(s => Convert.ToDecimal(s.DP_IMPORT));
                        T_Top1 = DataVentasxImpresora.Where(w => w.IDIMP == Convert.ToInt32(IDIMPRE)).OrderByDescending(O => O.DP_CANT).FirstOrDefault().DP_DESCRIP;
                    }
                    else
                    {
                        T_Platos = 0;
                        T_Monto = 0;
                        T_Top1 = "";
                    }
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Seleccione Impresora", 2);
                }
            }
        }
        private void IdRadiobuton(object id)
        {
            this.Idradiobuton = id.ToString();
        }
        private void ExportarPDF()
        {
            string fecha1 = Convert.ToDateTime(this.VentasxImpresora.dia).ToString("yyyy-MM-dd");
            //string fecha2 = Convert.ToDateTime(this.CajaChica.mes).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.VentasxImpresora.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.VentasxImpresora.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            if (this.Idradiobuton == "1")
            {
                saveFileDialog1.FileName = "Historial_VentaPlatos_x_Impresora_" + fecha1;
            }
            else if (this.Idradiobuton == "2")
            {
                //saveFileDialog1.FileName = "Historial_Descuento_" + fecha2;
            }
            else
            {
                saveFileDialog1.FileName = "Historial_VentaPlatos_x_Impresora_" + fecha3 + "_" + fecha4;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("IMPRESORA");
            dt.Columns.Add("CANTIDAD");
            dt.Columns.Add("DESCRIPCIÓN");
            dt.Columns.Add("IMPORTE");
            foreach (var p in DataVentasxImpresora)
            {
                dt.Rows.Add(new object[] { p.IDDETPED, p.NOMEQUIPOIMP, p.DP_CANT,p.DP_DESCRIP,p.DP_IMPORT});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "Historial de Ventas Plato x Impresora");
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
            string fecha1 = Convert.ToDateTime(this.VentasxImpresora.dia).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.VentasxImpresora.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.VentasxImpresora.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            if (this.Idradiobuton == "1")
            {
                saveFileDialog1.FileName = "Historial_VentaPlatos_x_Impresora_" + fecha1;
            }
            else if (this.Idradiobuton == "2")
            {

            }
            else
            {
                saveFileDialog1.FileName = "Historial_VentaPlatos_x_Impresora_" + fecha3 + "_" + fecha4;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("IMPRESORA");
            dt.Columns.Add("CANTIDAD");
            dt.Columns.Add("DESCRIPCIÓN");
            dt.Columns.Add("IMPORTE");
            foreach (var p in DataVentasxImpresora)
            {
                dt.Rows.Add(new object[] { p.IDDETPED, p.NOMEQUIPOIMP, p.DP_CANT, p.DP_DESCRIP, p.DP_IMPORT });
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
        private void Imprimir() {
            try
            {
                string desdet = Convert.ToDateTime(this.VentasxImpresora.desde).ToString("yyyy-MM-dd");
                string hastat = Convert.ToDateTime(this.VentasxImpresora.hasta).ToString("yyyy-MM-dd");
                string dia = Convert.ToDateTime(this.VentasxImpresora.dia).ToString("yyyy-MM-dd");
                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();

                DataTable dt = new DataTable();
                if (this.Idradiobuton == "1")
                {
                    dt = negDetVent.GetPlatosVendidosxImpresora(null, null, dia, Convert.ToInt32(IDIMPRE));
                }
                else
                {
                    dt = negDetVent.GetPlatosVendidosxImpresora(desdet, hastat, null, Convert.ToInt32(IDIMPRE));
                }

                int c = Convert.ToInt32(Math.Round(Convert.ToDouble(dt.Rows.Count / 35), 0)) + 1;
                string idimp = "";
                string nomimp = "";
                string[] idsimp;
                string[] nomsimp;
                char[] spearatorScat = { '-' };
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        idimp += dt.Rows[i]["IDIMP"].ToString();
                        nomimp += dt.Rows[i]["impresora"].ToString();
                    }
                    else
                    {
                        idimp += "-" + dt.Rows[i]["IDIMP"].ToString();
                        nomimp += "-" + dt.Rows[i]["impresora"].ToString();
                    }
                }
                idsimp = idimp.Split(spearatorScat).ToArray();
                idsimp = idsimp.Distinct().ToArray();
                nomsimp = nomimp.Split(spearatorScat).ToArray();
                //DataTable dt2= negCategoria.GetCategoria2();
                string idcat = "";
                string nomcat = "";
                string[] idscat;
                string[] nomscat;
                char[] spearatorcat = { '-' };
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        idcat += dt.Rows[i]["IDCAT"].ToString();
                        nomcat += dt.Rows[i]["CAT_NOM"].ToString();
                    }
                    else
                    {
                        idcat += "-" + dt.Rows[i]["IDCAT"].ToString();
                        nomcat += "-" + dt.Rows[i]["CAT_NOM"].ToString();
                    }
                }
                idscat = idcat.Split(spearatorcat).ToArray();
                idscat = idscat.Distinct().ToArray();
                nomscat = nomcat.Split(spearatorcat).ToArray();
                nomscat = nomscat.Distinct().ToArray();
                int contador = 0;
                for (int p1 = 0; p1 < idsimp.Distinct().Count(); p1++)
                {
                    int pagina = 2;
                    string impresora = nomsimp[p1].ToString();
                    Ticket ticketPlatos = new Ticket();
                    if (dt.Rows.Count > 0)
                    {
                        ticketPlatos.AddHeaderLine("REPORTE PLATOS VENDIDOS X IMP");
                        ticketPlatos.AddHeaderLine("                             " + "1/" + c);
                        ticketPlatos.AddSubHeaderLine("");
                        if (this.Idradiobuton == "1")
                        {
                            ticketPlatos.AddSubHeaderLine("Dia: " + VentasxImpresora.dia.Date.ToString("dd-MM-yyyy"));
                        }
                        else
                        {
                            ticketPlatos.AddSubHeaderLine("Desde: " + VentasxImpresora.desde.Date.ToString("dd-MM-yyyy"));
                            ticketPlatos.AddSubHeaderLine("Hasta: " + VentasxImpresora.hasta.Date.ToString("dd-MM-yyyy"));
                        }
                        ticketPlatos.AddSubHeaderLine("");
                        ticketPlatos.AddSubHeaderLine("Total de Platos: " + T_Monto);
                        ticketPlatos.AddSubHeaderLine("Impresora: " + impresora);
                        Double total = 0.00;

                        for (int p2 = 0; p2 < idscat.Distinct().Count(); p2++)
                        {
                            total = 0.00;
                            string categoria = nomscat[p2].ToString().Trim();
                            ticketPlatos.AddReporteItemPlatos(categoria, "", "");

                            for (int j = 0; j < dt.Rows.Count; j++)
                            {

                                if (dt.Rows[j]["IDIMP"].ToString() == idsimp[p1].ToString() && dt.Rows[j]["IDCAT"].ToString() == idscat[p2].ToString())
                                {
                                    total += Convert.ToDouble((dt.Rows.Count > 0) ? dt.Rows[j]["cantidad"] : 0);
                                    string descripcion = (dt.Rows.Count > 0) ? dt.Rows[j]["carta"].ToString().Trim() : "";
                                    ticketPlatos.AddReporteItemPlatos(descripcion.Trim(), "", (dt.Rows.Count > 0) ? dt.Rows[j]["cantidad"].ToString().Trim() : "0");
                                    contador += 1;
                                }
                                if (contador == 35 || contador == 70 || contador == 105 || contador == 140 || contador == 175 || contador == 205)
                                {
                                    contador += 1;
                                    if (ticketPlatos.PrinterExists(ImpCaja))
                                    {
                                        ticketPlatos.PrintTicket(ImpCaja);
                                        ticketPlatos = new Ticket();
                                        ticketPlatos.AddHeaderLine("                             " + pagina + "/" + c);
                                        ticketPlatos.AddSubHeaderLine("");
                                        if (this.Idradiobuton == "1")
                                        {
                                            ticketPlatos.AddSubHeaderLine("Dia: " + VentasxImpresora.dia.Date.ToString("dd-MM-yyyy"));
                                        }
                                        else
                                        {
                                            ticketPlatos.AddSubHeaderLine("Desde: " + VentasxImpresora.desde.Date.ToString("dd-MM-yyyy"));
                                            ticketPlatos.AddSubHeaderLine("Hasta: " + VentasxImpresora.hasta.Date.ToString("dd-MM-yyyy"));
                                        }
                                        ticketPlatos.AddSubHeaderLine("");
                                        ticketPlatos.AddSubHeaderLine("Total de Platos: " + T_Monto);
                                        ticketPlatos.AddSubHeaderLine("Impresora: " + impresora);
                                        pagina = pagina + 1;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Impresora: " + negFuncionGlobal.ImpCaja() + " no está disponible.");
                                    }
                                }
                            }
                            ticketPlatos.AddReporteItemPlatos("Total : ", "", total.ToString());
                            ticketPlatos.AddReporteItemPlatos("", "", "");
                        }
                        ticketPlatos.AddFooterLine("");
                    }
                    if (ticketPlatos.PrinterExists(ImpCaja) != true)
                    {
                        MessageBox.Show("Impresora: " + ImpCaja + " no está disponible.");
                    }
                    else
                    {
                        ticketPlatos.PrintTicket(ImpCaja);
                    }
                }
            }
            catch (Exception ex)
            {
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error",ex.Message, 3);
            }
        }
        
        private void GetLista()
        {

        }
    }
}

using Capa_Entidades.Models.Reportes.Pedido;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Reportes;
using ExportToExcel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.Pedidos
{
    public class HistorialVentasMozoViewModel : IGeneric
    {
        Neg_VentasMozo negvMozo = new Neg_VentasMozo();
        Funcion_Global globales = new Funcion_Global();
        private DpVentaMozo vmozo;
        private string operacion;
        public ObservableCollection<DpVentaMozo> DataHistVentasMozo { get; set; }
        public ObservableCollection<DpVentaMozo> DataRankingMozo { get; set; }
        public ObservableCollection<DpVentaMozo> DataDetpedido { get; set; }
        public ObservableCollection<DpVentaMozo> DataDetprod { get; set; }
        public ObservableCollection<DpVentaMozo> DataDetprod2 { get; set; }
        public List<DpVentaMozo> DataTipoPago { get; set; }
        public DpVentaMozo SelectedDataFile { get; set; }
        public DpVentaMozo SelectedDataDetpedido { get; set; }
        public ICommand BuscarporfechasCommand { get; set; }
        public ICommand RbtTagCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        private ICommand m_RowClickCommand { get; set; }
        public ICommand ExportarPDFCommand { get; set; }
        public ICommand ImprimirCommand { get; set; }
        public string idcli { get; set; }
        public object idped { get; set; }
        public DpVentaMozo DpVentaMozo
        {
            get => vmozo;
            set
            {
                vmozo = value;
            }
        }
        public string Operacion
        {
            get => operacion;
            set
            {
                if (value == "Lista")
                {
                    GetLista();
                }

                operacion = value;

            }
        }
         
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
        #region tipo pago 
        public decimal TP_efectivo { get; set; }
        public decimal TP_tarjeta { get; set; }
        public decimal TP_total { get; set; }
        #endregion
        #region radiobuton
        public string rbbtdia { get; set; }
        public string rbbtmes { get; set; }
        public string rbbtdesdehasta { get; set; }
        public string Idradiobuton { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
        public string Dia { get; set; }



        void valorbt()
        {
            rbbtdia = "1";
            rbbtmes = "2";
            rbbtdesdehasta = "3";
        }
        #endregion
        public HistorialVentasMozoViewModel()
        {
            this.Operacion = "Lista";
            valorbt();
            this.DpVentaMozo = new DpVentaMozo();
            BuscarporfechasCommand = new RelayCommand(new Action(buscarporfechas));
            ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            ExportarPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.RbtTagCommand = new ParamCommand(new Action<object>(IdRadiobuton));
            ImprimirCommand = new RelayCommand(new Action(Imprimir));

            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = this.SelectedDataFile;
                var DetPed = this.SelectedDataDetpedido;
                if (set != null)
                {
                    this.idcli = set.RM_id_emp;

                    Desde = Convert.ToDateTime(DpVentaMozo.desde).ToString("yyyy-MM-dd");
                    Hasta = Convert.ToDateTime(DpVentaMozo.hasta).ToString("yyyy-MM-dd");
                    Dia = Convert.ToDateTime(DpVentaMozo.dia).ToString("yyyy-MM-dd");
                    if (Idradiobuton == "1")
                    {
                        this.DataHistVentasMozo = negvMozo.GetHVentasMozo(null, null,Dia, idcli);
                        this.DataDetprod = negvMozo.GetDetProducto(null, null, Dia, idcli);
                        this.DataTipoPago = negvMozo.GetTipoPago(null, null, Dia, idcli);
                        if (DataTipoPago.Count > 0)
                        {
                            //Data();
                        }
                    }
                    else if (Idradiobuton == "3")
                    {
                        this.DataHistVentasMozo = negvMozo.GetHVentasMozo(Desde, Hasta,null, idcli);
                        this.DataDetprod = negvMozo.GetDetProducto(Desde, Hasta, null, idcli);
                        this.DataTipoPago = negvMozo.GetTipoPago(Desde, Hasta, null, idcli);
                        if (DataTipoPago.Count > 0)
                        {
                            //Data();
                        }
                    }
                   
                    
                }
                if (DetPed != null)
                {

                    this.idped = DetPed.ID;
                    if (Idradiobuton == "1")
                    {
                        this.DataDetpedido = negvMozo.GetDepPedido(null, null, Dia, idped);
                    }
                    else if (Idradiobuton == "3")
                    {
                        this.DataDetpedido = negvMozo.GetDepPedido(Desde, Hasta, null, idped);
                    }
                }
            });
        }
        private void Imprimir()
        {
            try
            {
                if (Idradiobuton == "1")
                {
                    string desdet = Convert.ToDateTime(Dia).ToString("yyyy-MM-dd");
                    string hastat = Convert.ToDateTime(Dia).ToString("yyyy-MM-dd");
                    string dia = Convert.ToDateTime(Dia).ToString("yyyy-MM-dd");
                }
                else if (Idradiobuton == "3")
                {
                    string desdet = Convert.ToDateTime(Desde).ToString("yyyy-MM-dd");
                    string hastat = Convert.ToDateTime(Hasta).ToString("yyyy-MM-dd");
                    string dia = Convert.ToDateTime(Dia).ToString("yyyy-MM-dd");
                }
                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();

                DataTable dt_empleado = new DataTable();
                dt_empleado.Columns.Add("ID");
                dt_empleado.Columns.Add("ID_EMPLEADO");
                dt_empleado.Columns.Add("EMPLEADO");
                dt_empleado.Columns.Add("MONTO");

                DataTable dt_platos = new DataTable();

                foreach (var h in DataRankingMozo)
                {
                    DataRow dr = dt_empleado.NewRow();
                    dr["ID"] = h.ID;
                    dr["ID_EMPLEADO"] = h.RM_id_emp;
                    dr["EMPLEADO"] = h.RM_nom_emp;
                    dr["MONTO"] = h.RM_totalped;
                    dt_empleado.Rows.Add(dr);
                }
                int contador_paginas = dt_empleado.Rows.Count;
                int c = 1;

                Ticket ticket = new Ticket();
                ticket.AddHeaderLine("REPORTE DE VENTAS POR MOZO  " + c + "/" + contador_paginas);
                //ticket.AddHeaderLine("                                " + c + "/" + contador_paginas);
                ticket.AddSubHeaderLine("");

                foreach (DataRow dr in dt_empleado.Rows)
                {
                    if (Idradiobuton == "1")
                    {
                        this.DataDetprod2 = negvMozo.GetDetProducto(null, null, Dia, dr["ID_EMPLEADO"].ToString());
                    }
                    else if (Idradiobuton == "3")
                    {
                        this.DataDetprod2 = negvMozo.GetDetProducto(Desde, Hasta, null, dr["ID_EMPLEADO"].ToString());
                    }

                    int cc = Convert.ToInt32(Math.Round(Convert.ToDouble(DataDetprod2.Count() / 45), 0)) + 1;
                    int c_p = 1;
                    ticket.AddSubHeaderLine("Mozo(a): " + dr["EMPLEADO"].ToString() + " " + c_p + "/" + cc);
                    ticket.AddSubHeaderLine("");
                    ticket.AddSubHeaderLine("Platos vendidos: ");


                    int c_platos = 1;
                    foreach (var i in DataDetprod2)
                    {
                        ticket.AddReporteItemPlatos("[" + i.DProd_cant.ToString() + "] " + i.DProd_nom_carta, "", i.DProd_monto.ToString());
                        if (c_platos > 45)
                        {
                            if (ticket.PrinterExists(ImpCaja))
                            {
                                c_p = c_p + 1;
                                ticket.PrintTicket(ImpCaja);
                                ticket = new Ticket();
                                ticket.AddHeaderLine("REPORTE DE VENTAS POR MOZO  " + c + "/" + contador_paginas);
                                ticket.AddSubHeaderLine("");
                                ticket.AddSubHeaderLine("Mozo(a): " + dr["EMPLEADO"].ToString() + " " + c_p + "/" + cc);
                                ticket.AddSubHeaderLine("");
                                ticket.AddSubHeaderLine("Platos vendidos: ");
                            }
                            else
                            {
                                globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                                ticket = new Ticket();
                            }
                            c_platos = 1;
                        }

                        c_platos = c_platos + 1;

                    }
                    ticket.AddFooterLine("          Total: " + dr["MONTO"].ToString());
                    c = c + 1;
                    if (ticket.PrinterExists(ImpCaja))
                    {
                        ticket.PrintTicket(ImpCaja);
                        ticket = new Ticket();
                        ticket.AddHeaderLine("REPORTE DE VENTAS POR MOZO");
                        ticket.AddHeaderLine("                                " + c + "/" + contador_paginas);
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                        ticket = new Ticket();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void GetLista()
        {

            //string desdet = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //string hastat = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //string mes = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //string dia = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //this.DataHistVentasMozo = negvMozo.GetHVentasMozo(null, null,null);
            //this.DataRankingMozo = negvMozo.GetRankingMozo(null, null);
            //this.DataTipoPago = negvMozo.GetTipoPago(null, null);
        }

      
        private void IdRadiobuton(object id)
        {
            this.Idradiobuton = id.ToString();
        }
        private void buscarporfechas()
        {
            try
            {
                Desde = Convert.ToDateTime(DpVentaMozo.desde).ToString("yyyy-MM-dd");
                Hasta = Convert.ToDateTime(DpVentaMozo.hasta).ToString("yyyy-MM-dd");
                Dia = Convert.ToDateTime(DpVentaMozo.dia).ToString("yyyy-MM-dd");
                if (Idradiobuton == "1")
                {
                    this.DataRankingMozo = negvMozo.GetRankingMozo(null, null, Dia);
                    if (DataRankingMozo.Count == 0)
                    {
                        this.DataDetpedido = new ObservableCollection<DpVentaMozo>();
                        this.DataDetprod = new ObservableCollection<DpVentaMozo>();
                        this.DataHistVentasMozo = new ObservableCollection<DpVentaMozo>();
                        this.DataTipoPago = new List<DpVentaMozo>();
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay datos", 2);
                    }
                }
                else if (Idradiobuton == "3")
                {
                    this.DataRankingMozo = negvMozo.GetRankingMozo(Desde, Hasta, null);
                    if (DataRankingMozo.Count == 0)
                    {
                        this.DataDetpedido = new ObservableCollection<DpVentaMozo>();
                        this.DataDetprod = new ObservableCollection<DpVentaMozo>();
                        this.DataHistVentasMozo = new ObservableCollection<DpVentaMozo>();
                        this.DataTipoPago = new List<DpVentaMozo>();
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay datos", 2);
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Error: ", ex.Message, 3);
            }
            
            
        }

        private void ExportarExcel()
        {
            try
            {
                string fecha1 = Convert.ToDateTime(this.DpVentaMozo.dia).ToString("yyyy-MM-dd");
                //string fecha2 = Convert.ToDateTime(this.DpVentas.mes).ToString("yyyy-MM-dd");
                string fecha3 = Convert.ToDateTime(this.DpVentaMozo.desde).ToString("yyyy-MM-dd");
                string fecha4 = Convert.ToDateTime(this.DpVentaMozo.hasta).ToString("yyyy-MM-dd");

                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.Title = "Exportar Archivo a Excel";
                if (this.Idradiobuton == "1")
                {
                    saveFileDialog1.FileName = "Historial_de_Venta_" + fecha1;
                }
                else if (this.Idradiobuton == "3")
                {
                    saveFileDialog1.FileName = "Historial_de_Venta_" + fecha3 + "_" + fecha4;
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("N° DIARIO");
                dt.Columns.Add("MESA");
                dt.Columns.Add("SUBTOTAL");
                dt.Columns.Add("TIPO PAGO");
                dt.Columns.Add("ESTADO");
                dt.Columns.Add("F. PEDIDO");
                dt.Columns.Add("EMPLEADO");
                foreach (var p in DataHistVentasMozo)
                {
                    dt.Rows.Add(new object[] { p.ID, p.PED_NUM_DIARIO, p.M_NOM, p.PED_TOTAL, p.DENO_PAGO, p.DESC_EST, p.PED_FECH_PED, p.EMPL_NOM });
                }

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("EMPLEADO");
                dt1.Columns.Add("SUBTOTAL");
                dt1.Columns.Add("DESCUENTO");
                dt1.Columns.Add("TOTAL");
                foreach (var p in DataRankingMozo)
                {
                    dt1.Rows.Add(new object[] { p.RM_nom_emp, p.RM_subtotal, p.RM_descuento, p.RM_totalped });
                }
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("CANT");
                dt2.Columns.Add("DETALLE");
                dt2.Columns.Add("IMPORTE");
                dt2.Columns.Add("F. PEDIDO");
                foreach (var p in DataDetpedido)
                {
                    dt2.Rows.Add(new object[] { p.DP_Cant, p.DP_Detalle, p.DP_importe, p.DP_fech_ped });
                }
                DataTable dt3 = new DataTable();
                dt3.Columns.Add("EMPLEADO");
                dt3.Columns.Add("CANT");
                dt3.Columns.Add("CARTA");
                dt3.Columns.Add("MONTO");
                foreach (var p in DataDetprod)
                {
                    dt3.Rows.Add(new object[] { p.DProd_nom_emp, p.DProd_cant, p.DProd_nom_carta, p.DProd_monto });
                }
                /*     _detProd.DProd_nom_emp = row["EMPL_NOM"].ToString();
                        _detProd.DProd_cant = Convert.ToInt32(row["nro_platos"]);
                        _detProd.DProd_nom_carta = row["CAR_NOM"].ToString();
                        _detProd.DProd_monto = Math.Round(Convert.ToDecimal(row["total_dinero_platos"]),2);*/

                if (dt == null && dt1 == null && dt2 == null && dt3 == null)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe seleccionar un mozo y su detalle", 2);
                    return;
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
                            //CreateExcelFile.CreateExcelDocument(dt, ubicacion);


                            DataSet dataSet1 = new DataSet("Pedidos por Mozo");
                            dataSet1.Tables.Add(dt);

                            DataSet dataSet2 = new DataSet("Ranking de Mozo");
                            dataSet2.Tables.Add(dt1);

                            DataSet dataSet3 = new DataSet("Detalle de Pedido");
                            dataSet3.Tables.Add(dt2);

                            DataSet dataSet4 = new DataSet("Platos Vendidos");
                            dataSet4.Tables.Add(dt3);

                            List<DataSet> dataSets = new List<DataSet>();
                            dataSets.Add(dataSet1);
                            dataSets.Add(dataSet2);
                            dataSets.Add(dataSet3);
                            dataSets.Add(dataSet4);


                            negFuncionGlobal.DataSetsToExcel(dataSets, ubicacion);
                        }
                    }
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
                }
            }
            catch (Exception ex)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", ex.Message, 2);

            }
          
        }
        private void ExportarPDF()
        {
            string fecha1 = Convert.ToDateTime(this.DpVentaMozo.dia).ToString("yyyy-MM-dd");
            //string fecha2 = Convert.ToDateTime(this.DpVentas.mes).ToString("yyyy-MM-dd");
            string fecha3 = Convert.ToDateTime(this.DpVentaMozo.desde).ToString("yyyy-MM-dd");
            string fecha4 = Convert.ToDateTime(this.DpVentaMozo.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            if (this.Idradiobuton == "1")
            {
                saveFileDialog1.FileName = "Historial_de_Venta_" + fecha1;
            }
            else if (this.Idradiobuton == "3")
            {
                saveFileDialog1.FileName = "Historial_de_Venta_" + fecha3 + "_" + fecha4;
            }
            //else
            //{
            //    saveFileDialog1.FileName = "Historial_de_Venta_" + fecha2;
            //}

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("N° DIARIO");
            dt.Columns.Add("MESA");
            dt.Columns.Add("SUBTOTAL");
            dt.Columns.Add("TIPO PAGO");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("F. PEDIDO");
            dt.Columns.Add("EMPLEADO");
            foreach (var p in DataHistVentasMozo)
            {
                dt.Rows.Add(new object[] { p.ID, p.PED_NUM_DIARIO, p.M_NOM, p.PED_TOTAL, p.DENO_PAGO, p.DESC_EST, p.PED_FECH_PED, p.EMPL_NOM });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "Historial de Ventas por Mozo");
                    }
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }

        Funcion_Global negFuncionGlobal = new Funcion_Global();
    }
}

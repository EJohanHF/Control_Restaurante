using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Reportes.DetalleporDia;
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
using System.Windows.Forms;
using System.Windows.Input;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.Pedidos
{
    public class RptVentasxCanalViewModel : IGeneric
    {
        #region Negocio
        Neg_VentasCanal negVentasCanal = new Neg_VentasCanal();
        Funcion_Global globales = new Funcion_Global();
        #endregion
        #region Objetos
        public DateTime Desde { get; set; } = DateTime.Now;
        public DateTime Hasta { get; set; } = DateTime.Now;
        public int IdCanal { get; set; } = 0;
        public int IdTipoPago { get; set; } = 0;
        public string NombreCanal { get; set; }
        public decimal TOTAL { get; set; }
        #endregion
        #region Listas
        public ObservableCollection<Ambiente> DataCanal { get; set; }
        public ObservableCollection<SJesus> DataTipoPago { get; set; }
        public ObservableCollection<Detalle_Pedido> DataVentas { get; set; }
        public ObservableCollection<Detalle_Pedido> DataPlatosVendidos { get; set; }
        #endregion
        #region Seleccion
        private SJesus _TipoPagoSelected;
        public SJesus TipoPagoSelected
        {
            get => _TipoPagoSelected;
            set
            {
                if (value != null)
                {
                    IdTipoPago = ((SJesus)value).id;
                }
                _TipoPagoSelected = value;
            }
        }
        private Ambiente _CanalSelected;
        public Ambiente CanalSelected
        {
            get => _CanalSelected;
            set
            {
                if (value != null)
                {
                    this.IdCanal = ((Ambiente)value).id;
                    this.NombreCanal = ((Ambiente)value).nombre;
                }
                _CanalSelected = value;
            }
        }
        #endregion
        #region Commands
        public ICommand BuscarCommand { get; set; }
        public ICommand ImprimirCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        #endregion
        #region Entidad
        private SJesus _tipoPago;
        public SJesus TipoPago
        {
            get => _tipoPago;
            set
            {
                _tipoPago = value;
            }
        }
        #endregion
        public RptVentasxCanalViewModel()
        {
            getLista();
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ImprimirCommand = new RelayCommand(new Action(Imprimir));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportaExcel));
        }
        private void ExportaExcel()
        {
            try
            {
                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.Title = "Exportar Archivo a Excel";
                saveFileDialog1.FileName = "Reporte Platos vendidos" + DateTime.Now.ToString("yyyyMMddHHmmss") + " Canal " + NombreCanal;

                DataTable dt = new DataTable();
                dt.Columns.Add("CANTIDAD");
                dt.Columns.Add("PLATO");
                dt.Columns.Add("IMPORTE");

                foreach (var p in DataPlatosVendidos)
                {
                    dt.Rows.Add(new object[] { p.CANTIDAD, p.PLATO, p.IMPORTE });
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
                    globales.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void Imprimir()
        {
            try
            {
                if (IdCanal == 0) { NombreCanal = "TODOS"; }
                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
                #region PLATOS VENDIDOS POR DIA X CANAL DE VENTA

                DataTable dt_ventas_x_canal = new DataTable();
                dt_ventas_x_canal.Columns.Add("ID");
                dt_ventas_x_canal.Columns.Add("CANTIDAD");
                dt_ventas_x_canal.Columns.Add("PLATO");
                dt_ventas_x_canal.Columns.Add("IMPORTE");
                dt_ventas_x_canal.Columns.Add("DESCUENTO");
                dt_ventas_x_canal.Columns.Add("TOTALINCDESCUENTO");

                foreach (var i in DataPlatosVendidos)
                {
                    DataRow dr = dt_ventas_x_canal.NewRow();
                    dr["ID"] = i.id_carta;
                    dr["CANTIDAD"] = i.CANTIDAD;
                    dr["PLATO"] = i.PLATO;
                    dr["IMPORTE"] = i.IMPORTE;
                    dr["DESCUENTO"] = i.DESCUENTO;
                    dr["TOTALINCDESCUENTO"] = i.TOTALINCDESCUENTO;
                    dt_ventas_x_canal.Rows.Add(dr);
                }

                Ticket ticket = new Ticket();
                ticket.AddHeaderLine_2("PLATOS VENDIDOS DEL DIA");
                ticket.AddHeaderLine_2("Canal de venta: " + NombreCanal.ToUpper());
                ticket.AddHeaderLine_2("----------------------------");
                ticket.AddHeaderLine_2("Cajero: " + System.Windows.Application.Current.Properties["NomUsuario"].ToString());
                ticket.AddSubHeaderLine("Desde: " + Desde.ToString("dd/MM/yyyy"));
                ticket.AddSubHeaderLine("Hasta: " + Hasta.ToString("dd/MM/yyyy"));
                ticket.AddHeaderLine_2("----------------------------");
                int j = 0;
                int pagina = 1;
                decimal importe_total = 0;
                decimal descuentos = 0;
                decimal importecondescuentos = 0;
                foreach (DataRow dr_platos in dt_ventas_x_canal.Rows)
                {
                    ticket.AddItemSinRecorte2("[" + Convert.ToInt32(dr_platos["CANTIDAD"]).ToString() + "] " + dr_platos["PLATO"].ToString(), "", dr_platos["IMPORTE"].ToString());
                    j = j + 1;
                    importe_total = importe_total + Convert.ToDecimal(dr_platos["IMPORTE"]);
                    descuentos = Convert.ToDecimal(dr_platos["DESCUENTO"]);
                    importecondescuentos = importe_total - descuentos;
                }
                ticket.AddDocElectronicoAnuladosTotal("TOTAL", "", importecondescuentos.ToString());
                ticket.AddDocElectronicoAnuladosTotal("DESCUENTOS", "", descuentos.ToString());
                ticket.AddDocElectronicoAnuladosTotal("TOTAL INC DESCUENTOS", "", importe_total.ToString());

                if (ticket.PrinterExists(ImpCaja))
                {
                    ticket.PrintTicket(ImpCaja);
                    ticket = new Ticket();
                }

                #endregion
            }
            catch (Exception ex)
            {
            }
        }
        private void Buscar()
        {
            try
            {
                //DataVentas = new ObservableCollection<Detalle_Pedido>();
                DataVentas = negVentasCanal.getVentasCanal(this.Desde, this.Hasta, this.IdCanal, this.IdTipoPago);
                //DataPlatosVendidos = new ObservableCollection<Detalle_Pedido>();
                DataPlatosVendidos = negVentasCanal.getPlatosVendidos(this.Desde, this.Hasta, this.IdCanal);
                this.TOTAL = Math.Round(DataVentas.Sum(s => Convert.ToDecimal(s.total_ped)), 2);
            }
            catch (Exception ex)
            {
                globales.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }
        private void getLista()
        {
            try
            {
                DataCanal = negVentasCanal.getComboCanal();
                DataTipoPago = negVentasCanal.getComboTipoPago();
                DataVentas = negVentasCanal.getVentasCanal(this.Desde, this.Hasta, this.IdCanal, this.IdTipoPago);
                DataPlatosVendidos = negVentasCanal.getPlatosVendidos(this.Desde, this.Hasta, this.IdCanal);
                this.TOTAL = Math.Round(DataVentas.Sum(s => Convert.ToDecimal(s.total_ped)), 2);
            }
            catch (Exception ex)
            {
                globales.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }  
        }
    }
}

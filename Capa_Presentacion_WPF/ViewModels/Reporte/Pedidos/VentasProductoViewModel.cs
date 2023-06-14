using Capa_Entidades;
using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Facturacion_Electronica;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio;
using Capa_Negocio.Carta;
using Capa_Negocio.Facturacion_Electronica;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Pedido;
using Capa_Negocio.Reportes;
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
    public class VentasProductoViewModel : IGeneric
    {
        #region Negocio
        Neg_VentasCanal negVentasCanal = new Neg_VentasCanal();
        Neg_Tip_Doc_Electronico negTipoDocElectrectronico = new Neg_Tip_Doc_Electronico();
        Neg_Estado_Financiero negEstadoFinanciero = new Neg_Estado_Financiero();
        Neg_Pedido negpedido = new Neg_Pedido();
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Detalle_pedido negDetVent = new Neg_Detalle_pedido();
        Funcion_Global globales = new Funcion_Global();
        Neg_Grupo negGrupo = new Neg_Grupo();
        Neg_Platos negPlatos = new Neg_Platos();
        #endregion
        #region Entidad

        #endregion
        #region Seleccion
        private Grupo _GrupoSelected;
        public Grupo GrupoSelected {
            get => _GrupoSelected;
            set {
                if(value != null)
                {
                    this.IdGrupoSelected = ((Grupo)value).idgrup;
                    DataPlatos = negPlatos.getPlatosCombo(this.IdGrupoSelected);
                }
                _GrupoSelected = value;
            }
        }
        private Platos _PlatosSelected;
        public Platos PlatosSelected {
            get => _PlatosSelected;
            set {
                if(value != null)
                {

                    this.IdPlatoSelected = ((Platos)value).idplato;
                }
                _PlatosSelected = value;
            }
        }
        private Ent_Combo _CajaSelected;
        public Ent_Combo CajaSelected {
            get => _CajaSelected;
            set {
                if (value != null) {
                    IdCajaSelected = Convert.ToInt32(((Ent_Combo)value).id);
                }
                _CajaSelected = value;
            }
        }
        private Ambiente _CanalSelected;
        public Ambiente CanalSelected {
            get => _CanalSelected;
            set {
                if (value != null) {
                    IdCanalSelected = ((Ambiente)value).id;
                }
                _CanalSelected = value;
            }
        }
        private Tipo_Doc_Electronico _ComprobanteSelected;
        public Tipo_Doc_Electronico ComprobanteSelected {
            get => _ComprobanteSelected;
            set {
                if (value != null){
                    IdComprobanteSelected = ((Tipo_Doc_Electronico)value).VALOR_TIPO_DOC;
                }
                _ComprobanteSelected = value;
            }
        }
        #endregion
        #region Objetos
        public int cantidadRegistros { get; set; } = 0;
        public DateTime Desde { get; set; } = DateTime.Now;
        public DateTime Hasta { get; set; } = DateTime.Now;
        public DateTime HoraInicio { get; set; } = DateTime.Now;
        public DateTime HoraFin { get; set; } = DateTime.Now;
        public int IdGrupoSelected { get; set; }
        public int IdPlatoSelected { get; set; }
        public int IdCajaSelected { get; set; }
        public int IdCanalSelected { get; set; }
        public string IdComprobanteSelected { get; set; }
        public bool isCheckedNoIncluirOfertas { get; set; } = false;
        public bool idCheckedVerCortesias { get; set; } = false;
        public bool isCheckedTodos { get; set; } = false;
        public decimal CONSOLIDADO_SUBTOTAL { get; set; } = 0;
        public decimal CONSOLIDADO_TOTAL { get; set; } = 0;
        public decimal CONSOLIDADO_TOTALTRIBUTARIO { get; set; } = 0;
        public decimal CONSOLIDADO_ICBPER { get; set; } = 0;
        public decimal CONSOLIDADO_IGV { get; set; } = 0;
        public decimal CONSOLIDADO_OPGRAVADAS { get; set; } = 0;
        public decimal CONSOLIDADO_RC { get; set; } = 0;
        #endregion
        #region Commands
        public ICommand AplicarFiltrosCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand ExportarPDFCommand { get; set; }
        #endregion
        #region Listas
        public ObservableCollection<Ambiente> DataCanal { get; set; }
        public List<Tipo_Doc_Electronico> DataComprobante { get; set; }
        public List<Ent_Combo> ComboCajas { get; set; }
        public ObservableCollection<Grupo> DataGrupos { get; set; }
        public ObservableCollection<Platos> DataPlatos { get; set; }
        public ObservableCollection<Detalle_Pedido> DataVentas { get; set; }
        #endregion
        public VentasProductoViewModel() {
            getLista();
            this.AplicarFiltrosCommand = new RelayCommand(new Action(AplicarFiltros));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportaExcel));
            this.ExportarPDFCommand = new RelayCommand(new Action(ExportarPDF));
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
            saveFileDialog1.FileName = "Ventas por Producto " + DateTime.Now.ToString("ddMMyyyy HHmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("Producto");
            dt.Columns.Add("GRUPO");
            dt.Columns.Add("CANTIDAD");
            dt.Columns.Add("PRECIO VENTA");
            dt.Columns.Add("DESCUENTO");
            dt.Columns.Add("SALON");
            dt.Columns.Add("LLEVAR");
            dt.Columns.Add("DELIVERY");

            foreach (var p in DataVentas)
            {
                dt.Rows.Add(new object[] { p.PLATO, p.GRUPO, p.CANTIDAD, p.DProd_pre_uni, p.DESCUENTO, p.SALON, p.LLEVAR, p.DELIVERY});
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
                        globales.ExportDataTableToPdf(dt, ubicacion, "Ventas por Producto");
                    }
                }
            }
            else
            {
                globales.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }
        private void ExportaExcel()
        {
            try
            {
                if (DataVentas != null || DataVentas.Count != 0)
                {
                    Stream myStream;
                    System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                    saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
                    saveFileDialog1.FilterIndex = 2;
                    saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";
                    saveFileDialog1.RestoreDirectory = true;
                    saveFileDialog1.Title = "Exportar Archivo a Excel";
                    saveFileDialog1.FileName = "Ventas por Producto " + DateTime.Now.ToString("ddMMyyyy HHmmss");

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Producto");
                    dt.Columns.Add("GRUPO");
                    dt.Columns.Add("CANTIDAD");
                    dt.Columns.Add("PRECIO VENTA");
                    dt.Columns.Add("DESCUENTO");
                    dt.Columns.Add("SALON");
                    dt.Columns.Add("LLEVAR");
                    dt.Columns.Add("DELIVERY");

                    foreach (var p in DataVentas)
                    {
                        dt.Rows.Add(new object[] { p.PLATO, p.GRUPO, p.CANTIDAD, p.DProd_pre_uni, p.DESCUENTO, p.SALON, p.LLEVAR, p.DELIVERY });
                    }

                    if (dt.Rows.Count > 0)
                    {
                        if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
                else
                {
                    globales.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void AplicarFiltros()
        {
            try
            {
                int idplato = 0;
                if (isCheckedTodos == true)
                {
                    idplato = 0;
                }
                else {
                    idplato = IdPlatoSelected;
                }

                DataVentas = negpedido.getVentaProductos(IdCajaSelected, IdComprobanteSelected, IdCanalSelected, idplato, Desde, Hasta, HoraInicio, HoraFin);

                if (idCheckedVerCortesias == true) {
                    List<Detalle_Pedido> dd = new List<Detalle_Pedido>();
                    dd = DataVentas.Where(w => w.DESCUENTO == Convert.ToDecimal(w.DProd_pre_uni)).ToList();
                    ObservableCollection<Detalle_Pedido> _dd = new ObservableCollection<Detalle_Pedido>(dd);
                    DataVentas = _dd;
                }
                if (isCheckedNoIncluirOfertas == true) {
                    List<Detalle_Pedido> dd = new List<Detalle_Pedido>();
                    dd = DataVentas.Where(w => w.DESCUENTO == 0).ToList();
                    ObservableCollection<Detalle_Pedido> _dd = new ObservableCollection<Detalle_Pedido>(dd);
                    DataVentas = _dd;
                }

                DataTable dtConsolidado = new DataTable();
                dtConsolidado = negpedido.getVentaProductosConsolidado(IdCajaSelected, IdComprobanteSelected, IdCanalSelected, IdPlatoSelected, Desde, Hasta, HoraInicio, HoraFin);
                CONSOLIDADO_SUBTOTAL = Math.Round(Convert.ToDecimal(dtConsolidado.Rows[0]["SUBTOTAL"]), 2);
                decimal total, descuento;
                total = Math.Round(DataVentas.Sum(s => s.CANTIDAD * Convert.ToDecimal(s.DProd_pre_uni)), 2);
                descuento = Math.Round(DataVentas.Sum(s => Convert.ToDecimal(s.DESCUENTO)), 2);
                CONSOLIDADO_TOTAL = Math.Round((total - descuento),2);
                //CONSOLIDADO_TOTAL = Math.Round(DataVentas.Sum(s => s.SALON + s.DELIVERY + s.LLEVAR),2);
                CONSOLIDADO_TOTALTRIBUTARIO = Math.Round(Convert.ToDecimal(dtConsolidado.Rows[0]["TOTALT"]), 2);
                CONSOLIDADO_ICBPER = Math.Round(Convert.ToDecimal(dtConsolidado.Rows[0]["ICBPER"]), 2);
                CONSOLIDADO_IGV = Math.Round(Convert.ToDecimal(dtConsolidado.Rows[0]["IGV"]), 2);
                CONSOLIDADO_OPGRAVADAS = Math.Round(Convert.ToDecimal(dtConsolidado.Rows[0]["SUBTOTAL"]), 2);
                cantidadRegistros = DataVentas.Count;
            }
            catch (Exception ex)
            {
            }
        }
        private void getLista()
        {
            try
            {
                string horainicio = "00:00:00";
                string horafin = "23:59:59";

                this.HoraInicio = Convert.ToDateTime(horainicio);
                this.HoraFin = Convert.ToDateTime(horafin);

                DataCanal = negVentasCanal.getComboCanal();
                DataComprobante = negTipoDocElectrectronico.GetTipDocElectronico();
                ComboCajas = negCombo.getCajas();
                DataGrupos = negGrupo.getGruposCombo();
            }
            catch (Exception ex)
            {
            }
        }
    }
}

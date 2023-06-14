using Capa_Entidades;
using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Facturacion_Electronica;
using Capa_Entidades.Models.Pedido;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio;
using Capa_Negocio.Facturacion_Electronica;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Pedido;
using Capa_Negocio.Reportes;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Presentacion_WPF.Views.Dialogs.DialogVentaporDia;
using ExportToExcel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.Pedidos
{
    public class InformeVentasViewModel : IGeneric
    {
        #region Negocio
        Neg_VentasCanal negVentasCanal = new Neg_VentasCanal();
        Neg_Tip_Doc_Electronico negTipoDocElectrectronico = new Neg_Tip_Doc_Electronico();
        Neg_Estado_Financiero negEstadoFinanciero = new Neg_Estado_Financiero();
        Neg_Pedido negpedido = new Neg_Pedido();
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Detalle_pedido negDetVent = new Neg_Detalle_pedido();
        Funcion_Global globales = new Funcion_Global();
        #endregion
        #region Objetos
        public string Operacion { get; set; }
        public DateTime Desde { get; set; } = DateTime.Now;
        public DateTime Hasta { get; set; } = DateTime.Now;
        public DateTime HoraInicio { get; set; } = DateTime.Now;
        public DateTime HoraFin { get; set; } = DateTime.Now;
        public int Id_Caja_Seleccion { get; set; }
        public int Id_CanalVenta_Seleccion { get; set; }
        public string Id_Comprobante_Seleccion { get; set; }
        public int Id_Turno_Seleccion { get; set; }
        public int Id_Estado_Seleccion { get; set; }
        public string Serie { get; set; } = "";
        public string Correlativo { get; set; } = "";
        public int Id_TipoEmision_Seleccion { get; set; }
        public decimal CONSOLIDADO_SUBTOTAL { get; set; } = 0;
        public decimal CONSOLIDADO_TOTAL { get; set; } = 0;
        public decimal CONSOLIDADO_TOTALTRIBUTARIO { get; set; } = 0;
        public decimal CONSOLIDADO_ICBPER { get; set; } = 0;
        public decimal CONSOLIDADO_IGV { get; set; } = 0;
        public decimal CONSOLIDADO_OPGRAVADAS { get; set; } = 0;
        public decimal CONSOLIDADO_RC { get; set; } = 0;
        public int nroRegistros { get; set; } = 0;
        #endregion
        #region Listas
        public ObservableCollection<Ambiente> DataCanal { get; set; }
        public List<Tipo_Doc_Electronico> DataComprobante { get; set; }
        public ObservableCollection<Estado_Financiero> DataEstado { get; set; }
        public ObservableCollection<Detalle_Pedido> DataVentas { get; set; }
        public List<Ent_Combo> ComboCajas { get; set; }
        public ObservableCollection<Detalle_Pedido> DataDetprod { get; set; }
        #endregion
        #region Entidad
        private Ambiente _Ambiente;
        public Ambiente Ambiente {
            get => _Ambiente;
            set {
                _Ambiente = value;
            }
        }
        private Tipo_Doc_Electronico _Tipo_Doc_Electronico;
        public Tipo_Doc_Electronico Tipo_Doc_Electronico {
            get => _Tipo_Doc_Electronico;
            set {
                _Tipo_Doc_Electronico = value;
            }
        }
        private Estado_Financiero _Estado_Financiero;
        public Estado_Financiero Estado_Financiero {
            get => _Estado_Financiero;
            set {
                _Estado_Financiero = value;
            }
        }
        #endregion
        #region Seleccion
        private Ambiente _CanalSelected;
        public Ambiente CanalSelected {
            get => _CanalSelected;
            set {
                if (value != null) {
                    Id_CanalVenta_Seleccion = ((Ambiente)value).id;
                }
                _CanalSelected = value;
            }
        }
        private Tipo_Doc_Electronico _ComprobanteSelected;
        public Tipo_Doc_Electronico ComprobanteSelected {
            get => _ComprobanteSelected;
            set {
                if (value != null) {
                    Id_Comprobante_Seleccion = ((Tipo_Doc_Electronico)value).VALOR_TIPO_DOC;
                }
                _ComprobanteSelected = value;
            }
        }
        private Estado_Financiero _EstadoSelected;
        public Estado_Financiero EstadoSelected {
            get => _EstadoSelected;
            set {
                if (value != null) {
                    Id_Estado_Seleccion = ((Estado_Financiero)value).ID;
                }
                _EstadoSelected = value;
            }
        }
        private Ent_Combo _CajaSelected;
        public Ent_Combo CajaSelected {
            get => _CajaSelected;
            set {

                if (value != null)
                {
                    Id_Caja_Seleccion = Convert.ToInt32(((Ent_Combo)value).id);
                }
                _CajaSelected = value;
            }
        }
        #endregion
        #region Commands
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand ExportarPDFCommand { get; set; }
        public ICommand ImprimirCommand { get; set; }
        public ICommand AplicarFiltrosCommand { get; set; }
        public ICommand GenerarInformeCommand { get; set; }
        public ICommand AbrirDialogDetPedido { get; set; }
        #endregion
        public InformeVentasViewModel() {
            getLista();
            DataVentas = new ObservableCollection<Detalle_Pedido>();
            this.AplicarFiltrosCommand = new RelayCommand(new Action(AplicarFiltros));
            this.AbrirDialogDetPedido = new ParamCommand(new Action<object>(DialogDetPed));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportaExcel));
            this.ExportarPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ImprimirCommand = new RelayCommand(new Action(Imprimir));
        }
        #region Exportar
        private void Imprimir()
        {
            Ticket ticketPlatos = new Ticket();

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
            saveFileDialog1.FileName = "Informe de ventas " + DateTime.Now.ToString("ddMMyyyy HHmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("N° Diario");
            dt.Columns.Add("Mesa");
            dt.Columns.Add("Descuento");
            dt.Columns.Add("Subtotal");
            dt.Columns.Add("Precio total");
            dt.Columns.Add("F. Pedido");
            dt.Columns.Add("Estado Financiero");
            dt.Columns.Add("Caja");
            dt.Columns.Add("Cliente");
            dt.Columns.Add("Telefono");
            dt.Columns.Add("Tipo Descuento");
            dt.Columns.Add("Forma de pago");
            dt.Columns.Add("Datos de Tarjeta");
            dt.Columns.Add("Mozo");
            dt.Columns.Add("Usuario");
            dt.Columns.Add("Serie de Documento");
            dt.Columns.Add("Importe");

            foreach (var p in DataVentas)
            {
                dt.Rows.Add(new object[] { p.id_ped, p.nom_mesa, p.desc_ped, p.subtotal_ped, p.total_ped, p.f_ped, p.nom_estado_f, p.PED_NOM_EQUIPO, p.nro_personas, p.nomllevar, p.telefcli, p.nom_tip_desc, p.nom_fpago,
                    p.nro_tarjeta, p.nom_emple, p.nom_usu, p.nom_tipo_Doc, p.importe_Total_Doc_Elec});
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
                        globales.ExportDataTableToPdf(dt, ubicacion, "Informe de ventas");
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
                    saveFileDialog1.FileName = "Informe de ventas " + DateTime.Now.ToString("ddMMyyyy HHmmss");

                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID");
                    dt.Columns.Add("N° Diario");
                    dt.Columns.Add("Mesa");
                    dt.Columns.Add("Descuento");
                    dt.Columns.Add("Subtotal");
                    dt.Columns.Add("Precio total");
                    dt.Columns.Add("F. Pedido");
                    dt.Columns.Add("Estado Financiero");
                    dt.Columns.Add("Caja");
                    dt.Columns.Add("Cliente");
                    dt.Columns.Add("Telefono");
                    dt.Columns.Add("Tipo Descuento");
                    dt.Columns.Add("Forma de pago");
                    dt.Columns.Add("Datos de Tarjeta");
                    dt.Columns.Add("Mozo");
                    dt.Columns.Add("Usuario");
                    dt.Columns.Add("Serie de Documento");
                    dt.Columns.Add("Importe");

                    foreach (var p in DataVentas)
                    {
                        dt.Rows.Add(new object[] { p.id_ped, p.nom_mesa, p.desc_ped, p.subtotal_ped, p.total_ped, p.f_ped, p.nom_estado_f, p.PED_NOM_EQUIPO, p.nro_personas, p.nomllevar, p.telefcli, p.nom_tip_desc, p.nom_fpago,
                    p.nro_tarjeta, p.nom_emple, p.nom_usu, p.nom_tipo_Doc, p.importe_Total_Doc_Elec});
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
                else {
                    globales.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        private async void DialogDetPed(object idped) {
            System.Windows.Application.Current.Properties["N"] = idped;
            this.DataDetprod = negDetVent.GetDetProducto(System.Windows.Application.Current.Properties["N"].ToString());
            System.Windows.Application.Current.Properties["detPEdido"] = DataDetprod;
            var SiNo = new DialogDetPedido {};
            var x = await DialogHost.Show(SiNo, "RootDialog");
        }
        private void AplicarFiltros() {
            try
            {
                DataVentas = negpedido.getInformeVentas(Id_Caja_Seleccion, Id_Comprobante_Seleccion, Id_Estado_Seleccion, Id_CanalVenta_Seleccion, Serie, Correlativo, Desde, Hasta, HoraInicio, HoraFin);
                CONSOLIDADO_SUBTOTAL = DataVentas.Where(w => Convert.ToInt32(w.id_estado_f) != 3 && Convert.ToInt32(w.id_estado_f) != 4).Sum(s => Convert.ToDecimal(s.subtotal_ped));
                CONSOLIDADO_ICBPER = DataVentas.Sum(s => s.icbper);
                CONSOLIDADO_IGV = DataVentas.Sum(s => s.igv);
                CONSOLIDADO_OPGRAVADAS = DataVentas.Sum(s => s.totalOPGravadas);
                CONSOLIDADO_RC = DataVentas.Sum(s => s.rc);
                CONSOLIDADO_TOTAL = DataVentas.Where(w => Convert.ToInt32(w.id_estado_f) != 3 && Convert.ToInt32(w.id_estado_f) != 4).Sum(s => Convert.ToDecimal(s.total_ped));
                CONSOLIDADO_TOTALTRIBUTARIO = DataVentas.Sum(s => Convert.ToDecimal(s.importe_Total_Doc_Elec));
               
                nroRegistros = DataVentas.Count;
            }
            catch (Exception ex)
            {
            }
        }
        private void getLista() {
            try
            {
                string horainicio = "00:00:00";
                string horafin = "23:59:59";

                this.HoraInicio = Convert.ToDateTime(horainicio);
                this.HoraFin = Convert.ToDateTime(horafin);

                DataCanal = negVentasCanal.getComboCanal();
                DataComprobante = negTipoDocElectrectronico.GetTipDocElectronico();
                DataEstado = negEstadoFinanciero.getEstadoFinanciero();
                ComboCajas = negCombo.getCajas();
            }
            catch (Exception ex)
            {
            }
        }
    }
}

using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Entidades.Models.Reportes.Pedido;
using Capa_Entidades.Models.Web_Service;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Reportes.VentasporDia;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.Pedidos
{
    public class ReporteCierreCajaViewModel : IGeneric
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Descripcion, int Reserved);
        Neg_Parametros negParametros = new Neg_Parametros();
        Neg_CierresDias negCierre = new Neg_CierresDias();
        Neg_CierreParcial negCierre_ = new Neg_CierreParcial();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Ent_CierresDias> cierres { get; set; }
        private string operacion;
        private DateTime desde;
        private DateTime hasta;
        private DateTime fechadia;
        private string nombredia;
        public string rbbtdia { get; set; }
        public string rbbtmes { get; set; }
        public string rbbtdesdehasta { get; set; }
        public string Idradiobuton { get; set; }
        public ICommand BuscarporfechasCommand { get; set; }
        public ICommand RbtTagCommand { get; set; }
        public ICommand EnviarCommand { get; set; }
        public ICommand ReimprimirCommand { get; set; }
        public decimal PM_Nventas { get; set; }
        private string _promedio;
        public string PM_Promedio
        {
            get => _promedio;
            set
            {
                _promedio = value;
            }
        }
        private DpVentas dpventas;

        public DpVentas DpVentas
        {
            get => dpventas;
            set
            {
                dpventas = value;
            }
        }
        public DateTime NombreDia
        {
            get
            {
                return DpVentas == null ? DateTime.Now : (DateTime)DpVentas.dia;

            }
            set
            {
                if (value != null)
                {
                    DpVentas.dia = value;
                }
                //DateTime fecha = Convert.ToDateTime(this.DpDescuento.dia;);
                //SelectFormatday = fecha.ToString("dddd");
                fechadia = value;
            }
        }
        public ReporteCierreCajaViewModel()
        {
            DpVentas = new DpVentas();
            cierres = new ObservableCollection<Ent_CierresDias>();
            valorbt();
            BuscarporfechasCommand = new RelayCommand(new Action(buscarporfechas));
            RbtTagCommand = new ParamCommand(new Action<object>(IdRadiobuton));
            this.EnviarCommand = new ParamCommand(new Action<object>(Enviar));
            this.ReimprimirCommand = new ParamCommand(new Action<object>(Reimprimir));
        }
        public List<VentasDia> dataPromedioMesas { get; set; }
        Neg_VentasDia negVentaD = new Neg_VentasDia();
        private void Reimprimir(object idCierre) {
            this.dataPromedioMesas = negVentaD.GetVentasDia_promedio_mesa2(Convert.ToInt32(idCierre));
            this.PM_Promedio = this.dataPromedioMesas.Select(w => w.PM_promedio).ToList().FirstOrDefault();
            this.PM_Nventas = this.dataPromedioMesas.Select(w => Convert.ToDecimal(w.PM_nventas)).ToList().FirstOrDefault();
            ImprimirTickets(Convert.ToInt32(idCierre));
        }
        private void ImprimirTickets(int idCierre)
        {
            string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
            #region REPORTE PLATOS X COMENTARIO Y ANULADOS
            /*****************************************************REPORTE PLATOS X COMENTARIO Y ANULADOS***************************************/
            DataTable dt = new DataTable();
            dt = negCierre_.GetAplatosXComentario2(idCierre);
            if (dt.Rows.Count > 0)
            {
                string comentario_raw = "";
                string[] comentario;
                char[] spearator = { '-' };
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        comentario_raw += dt.Rows[i]["COMENTARIO"].ToString();
                    }
                    else
                    {
                        comentario_raw += "-" + dt.Rows[i]["COMENTARIO"].ToString();
                    }

                }
                comentario = comentario_raw.Split(spearator).ToArray();
                comentario = comentario.Distinct().ToArray();
                for (int p = 0; p < comentario.Distinct().Count(); p++)
                {
                    Ticket ticket = new Ticket();
                    ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    ticket.AddHeaderLine_2("PRODUCTOS ANULADOS");
                    ticket.AddHeaderLine("");

                    ticket.AddSubHeaderLine(comentario[p].ToString());
                    ticket.AddSubHeaderLine("");
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["COMENTARIO"].ToString() == comentario[p].ToString())
                        {
                            string cantidad = (dt.Rows.Count > 0) ? dt.Rows[j]["CANTIDAD"].ToString() : "0.00";
                            string descripcion = (dt.Rows.Count > 0) ? dt.Rows[j]["DESCRIPCION"].ToString() : "0.00";
                            ticket.AddItemSinRecorte("[" + cantidad + "]" + descripcion, "", (dt.Rows.Count > 0) ? dt.Rows[j]["IMPORTE"].ToString() : "0.00");

                        }
                    }
                    ticket.AddFooterLine("");

                    
                    if (ticket.PrinterExists(ImpCaja))
                    {
                        ticket.PrintTicket(ImpCaja);
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                    }
                }

            }
            /*****************************************************FIN **********************************************************/
            #endregion
            #region REPORTE VENTA POR DIAS
            /******************REPORTE VENTA POR DIAS **************/
            DataTable dttipag = new DataTable();
            dttipag = negCierre_.GetTipoPago2(idCierre);
            string denotg_row = "";
            string[] denotg;
            char[] spearatordeno = { '-' };
            for (int i = 0; i < dttipag.Rows.Count; i++)
            {
                if (i == 0)
                {
                    denotg_row += dttipag.Rows[i]["DENO_PAGO"].ToString();
                }
                else
                {
                    denotg_row += "-" + dttipag.Rows[i]["DENO_PAGO"].ToString();
                }
            }
            denotg = denotg_row.Split(spearatordeno).ToArray();
            denotg = denotg.Distinct().ToArray();

            DataTable dt2 = new DataTable();
            dt2 = negCierre_.GetreporteVetasDia2(idCierre);
            Ticket ticket2 = new Ticket();

            ticket2.AddHeaderLine_2("REPORTE DE VENTA DEL DIA");
            ticket2.AddSubHeaderLine("");
            ticket2.AddSubHeaderLine("FECHA INICIO:" + dt2.Rows[0]["FECHAINICIO"].ToString());
            ticket2.AddSubHeaderLine("FECHA FIN:" + dt2.Rows[0]["FECHAFIN"].ToString());
            ticket2.AddSubHeaderLine("");


            int nroPersonas = (dt2.Rows.Count > 0) ? Convert.ToInt32(dt2.Rows[0]["TOTAL_PERSONAS"]) : 0;
            ticket2.AddReporteItem("Mesas Atendidas", "ESTADISTICAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_MESAS"].ToString() : "0.00");
            ticket2.AddReporteItem("Personas", "ESTADISTICAS", nroPersonas.ToString());

            ticket2.AddReporteItem("Promedio Mesas", "ESTADISTICAS", "S/. " + PM_Promedio.ToString());
            ticket2.AddReporteItem("Promedio Personas", "ESTADISTICAS", (PM_Nventas == 0 || nroPersonas == 0) ? "0" : Math.Round((PM_Nventas / nroPersonas), 2).ToString());
            ticket2.AddReporteItem("", "ESTADISTICAS", "");
            ticket2.AddReporteItem("Monto Delivery", "ESTADISTICAS", "S/. " + ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_DELIVERY_IMPORTE"].ToString() : "0.00").ToString().ToString());
            ticket2.AddReporteItem("Monto Recojo", "ESTADISTICAS", "S/. " + ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_RECOJO_IMPORTE"].ToString() : "0.00").ToString().ToString());
            ticket2.AddReporteItem("Cantidad de Delivery", "ESTADISTICAS", ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_DELIVERY"].ToString() : "0").ToString());
            ticket2.AddReporteItem("Cantidad de Recojo", "ESTADISTICAS", ((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL_RECOJO"].ToString() : "0").ToString());

            Double saldo = Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["SALDO_CAJA"].ToString() : "0.00");

            ticket2.AddReporteItemventas("CAJA CHICA", "", "");
            ticket2.AddReporteItemventas("Ingresos", "CAJA CHICA", (dt2.Rows.Count > 0) ? dt2.Rows[0]["INGRESO_CAJA"].ToString() : "0.00");
            ticket2.AddReporteItemventas("Egresos", "CAJA CHICA", (dt2.Rows.Count > 0) ? dt2.Rows[0]["EGRESO_CAJA"].ToString() : "0.00");
            ticket2.AddReporteItemventas("Saldo", "CAJA CHICA", (dt2.Rows.Count > 0) ? dt2.Rows[0]["SALDO_CAJA"].ToString() : "0.00");

            //ticket2.AddReporteItemventas("TOTAL CAJA : ", "CAJA CHICA", (dt2.Rows.Count > 0) ? dt2.Rows[0]["CAJA"].ToString() : "0.00");

            ticket2.AddReporteItemventas("----------------------------------", "", "");

            ticket2.AddReporteItemventas("COBRADO", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL"].ToString() : "0.00");
            ticket2.AddReporteItemventas("X COBRAR", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["POR_PAGAR"].ToString() : "0.00");
            object totalventas = 0;
            totalventas = Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL"] : 0.00) + Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["POR_PAGAR"] : 0.00);
            ticket2.AddReporteItemventas("TOTAL", "VENTAS", totalventas.ToString());
            ticket2.AddReporteItemventas("", "VENTAS", "");

            ticket2.AddReporteItemventas("Vendido", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["SUBTOTAL"].ToString() : "0.00");
            ticket2.AddReporteItemventas("Descuento", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["DESCUENTO"].ToString() : "0.00");
            ticket2.AddReporteItemventas("Importe Total de la Venta ", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL"].ToString() : "0.00");

            double montoefe = 0.00;

            bool pago = false;
            for (int p1 = 0; p1 < denotg.Distinct().Count(); p1++)
            {
                if (denotg[p1].ToString().Trim() != "")
                {
                    if (dttipag.Rows[p1]["DENO_PAGO"].ToString() == "EFECTIVO")
                    {
                        montoefe = Convert.ToDouble((dttipag.Rows.Count > 0) ? dttipag.Rows[p1]["PED_TOTAL"].ToString() : "0.00");
                    }
                    string nomcat = denotg[p1].ToString();
                    ticket2.AddDocElectronicoAnuladosTotal(dttipag.Rows[p1]["DENO_PAGO"].ToString(), "TIPO PAGO", (dttipag.Rows.Count > 0) ? dttipag.Rows[p1]["PED_TOTAL"].ToString() : "0.00");
                    pago = true;
                }
            }
            if (pago == true)
            {
                ticket2.AddFooterLine("");

                //ticket2.AddDocElectronicoAnuladosTotal("INGRESO CAJA CHICA", "", (dt2.Rows.Count > 0) ? dt2.Rows[0]["INGRESO_CAJA"].ToString() : "0.00");
                //Double total,totalventa,totcaja;
                //totcaja = Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["INGRESO_CAJA"].ToString() : "0.00");
                //total = Convert.ToDouble(dttipag.Rows[0]["MONTOTOTAL"].ToString());
                //total = totcaja + total;
                ticket2.AddDocElectronicoAnuladosTotal("TOTAL:", "", dttipag.Rows[0]["MONTOTOTAL"].ToString());
            }
            Double totalcaja = 0.00;
            totalcaja = montoefe + saldo;
            //ticket2.AddReporteItemventas("----------------------------------", "", "");
            ticket2.AddDocElectronicoAnuladosTotal("", "", "");
            ticket2.AddDocElectronicoAnuladosTotal("EFECTIVO : ", "CAJA", montoefe.ToString());
            ticket2.AddDocElectronicoAnuladosTotal("CAJA CHICA: ", "CAJA", saldo.ToString());
            ticket2.AddDocElectronicoAnuladosTotal("TOTAL : ", "CAJA", totalcaja.ToString());
            ticket2.AddFooterLine("");
            
            if (ticket2.PrinterExists(ImpCaja))
            {
                ticket2.PrintTicket(ImpCaja);
            }
            else
            {
                globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
            }
            /******************FIN *********************/
            #endregion
            #region DOCUMENTO ELECTRONICO
            /*****************************************************DOCUMENTO ELECTRONICO***************************************/


            DataTable dt3 = new DataTable();
            dt3 = negCierre_.GetreporteDocumentoElect2(idCierre);
            Ticket ticket3 = new Ticket();
            ticket3.AddTitleLine("REPORTE DE DOCUMENTOS");
            ticket3.AddTitleLine("ELECTRONICO");
            ticket3.AddSubHeaderLine("");

            ticket3.AddDocElectronico((dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALBOLETA"].ToString() : "0", "Boletas", (dt3.Rows.Count > 0) ? dt3.Rows[0]["MONTOBOL"].ToString() : "0.00");
            ticket3.AddDocElectronico((dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALFACTURA"].ToString() : "0", "Facturas", (dt3.Rows.Count > 0) ? dt3.Rows[0]["MONTOFACT"].ToString() : "0.00");
            ticket3.AddDocElectronicoTotal("TOTAL B/F", "", (dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALBF"].ToString() : "0.00");
            ticket3.AddDocElectronicoTotal("TOTAL S/B", "", (dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALSB"].ToString() : "0.00");

            ticket3.AddTitleLine2("REPORTE DE DOCUMENTOS");
            ticket3.AddTitleLine2("ELECTRONICOS ANULADOS");

            ticket3.AddDocElectronicoAnulados((dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALBOLANULADO"].ToString() : "0", "Boletas", (dt3.Rows.Count > 0) ? dt3.Rows[0]["MONTOBOLANULADO"].ToString() : "0.00");
            ticket3.AddDocElectronicoAnulados((dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALFACANULADO"].ToString() : "0", "Facturas", (dt3.Rows.Count > 0) ? dt3.Rows[0]["MONTOFACANULADO"].ToString() : "0.00");
            ticket3.AddDocElectronicoAnuladosTotal("TOTAL B/F", "", (dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALMONTOANULADO"].ToString() : "0.00");
            ticket3.AddFooterLine("");
            
            if (ticket3.PrinterExists(ImpCaja))
            {
                ticket3.PrintTicket(ImpCaja);
            }
            else
            {
                globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
            }

            /*****************************************************FIN **********************************************************/
            #endregion
            #region PLATOS VENDIDOS POR DIA
            /***************************************PLATOS VENDIDOS POR DIA**********************************************************/

            DataTable dtscat = new DataTable();
            dtscat = negCierre_.Getsupercat2(idCierre);
            string nomscat_row = "";
            string[] nomscat;
            char[] spearatorScat = { '-' };
            for (int i = 0; i < dtscat.Rows.Count; i++)
            {
                if (i == 0)
                {
                    nomscat_row += dtscat.Rows[i]["SCAT_NOM"].ToString();
                }
                else
                {
                    nomscat_row += "-" + dtscat.Rows[i]["SCAT_NOM"].ToString();
                }
            }
            nomscat = nomscat_row.Split(spearatorScat).ToArray();
            nomscat = nomscat.Distinct().ToArray();
            for (int p1 = 0; p1 < nomscat.Distinct().Count(); p1++)
            {
                int pagina = 2;
                string nomcat = nomscat[p1].ToString();
                DataTable dtVdia = new DataTable();
                dtVdia = negCierre_.GetreportetotalPedidosDia2(nomscat[p1].ToString(),idCierre);
                int c = Convert.ToInt32(Math.Round(Convert.ToDouble(dtVdia.Rows.Count / 35), 0)) + 1;
                if (dtVdia.Rows.Count > 0)
                {
                    Ticket ticketPlatos = new Ticket();
                    //ticketPlatos.AddSubHeaderLine("Fecha/Hora: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    ticketPlatos.AddSubHeaderLine("Fecha/Hora: " + cierres.Where(w => w.ID == idCierre).FirstOrDefault().DC_F_CREATE.ToString());
                    ticketPlatos.AddHeaderLine_2("PLATOS VENDIDOS DEL DIA");
                    ticketPlatos.AddSubHeaderLine("");
                    ticketPlatos.AddSubHeaderLine(nomscat[p1].ToString() + "                   " + "1/" + c);
                    ticketPlatos.AddSubHeaderLine("");
                    for (int j = 0; j < dtVdia.Rows.Count; j++)
                    {
                        if (dtVdia.Rows[j]["SUPERCAT"].ToString() == nomscat[p1].ToString())
                        {
                            string cantidad = (dtVdia.Rows.Count > 0) ? dtVdia.Rows[j]["CANTIDAD"].ToString() : "0.00";
                            string descripcion = (dtVdia.Rows.Count > 0) ? dtVdia.Rows[j]["DESCRIPCION"].ToString() : "0.00";
                            ticketPlatos.AddItemSinRecorte(cantidad + " " + descripcion, "", (dtVdia.Rows.Count > 0) ? dtVdia.Rows[j]["MONTO"].ToString() : "0.00");
                        }
                        if (j == 35 || j == 70 || j == 105 || j == 140 || j == 175 || j == 205)
                        {
                            if (ticketPlatos.PrinterExists(ImpCaja))
                            {
                                ticketPlatos.PrintTicket(ImpCaja);
                                ticketPlatos = new Ticket();
                                ticketPlatos.AddSubHeaderLine("Fecha/Hora: " + cierres.Where(w => w.ID == idCierre).FirstOrDefault().DC_F_CREATE.ToString());
                                ticketPlatos.AddSubHeaderLine("");
                                ticketPlatos.AddSubHeaderLine(nomscat[p1].ToString() + "                   " + pagina + "/" + c);
                                pagina = pagina + 1;

                            }
                            else
                            {
                                globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                                ticketPlatos = new Ticket();

                            }
                        }
                    }
                    ticketPlatos.AddDocElectronicoAnuladosTotal("TOTAL", "", (dtVdia.Rows.Count > 0) ? dtVdia.Rows[0]["TOTAL"].ToString() : "0.00");
                    ticketPlatos.AddFooterLine("");
                    if (ticketPlatos.PrinterExists(ImpCaja))
                    {
                        ticketPlatos.PrintTicket(ImpCaja);
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                    }
                }

            }

            /*****************************************************FIN **********************************************************/
            #endregion
            #region REPORTE CAJA
            /**********************************************************REPORTE CAJA****************************************/
            DataTable caja = new DataTable();
            caja = negCierre_.GetReporteCajaDia2(idCierre);
            if (caja.Rows.Count > 0)
            {
                string tipo_raw = "";
                string tipoMOVIMIENTO = "";
                string[] tipo;
                string[] tipo2;
                char[] separador = { '-' };
                char[] separador2 = { '-' };
                for (int i = 0; i < caja.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        tipo_raw += caja.Rows[i]["CC_ID_TIPO"].ToString();
                    }
                    else
                    {
                        tipo_raw += "-" + caja.Rows[i]["CC_ID_TIPO"].ToString();
                    }
                }
                tipo = tipo_raw.Split(separador).ToArray();
                tipo = tipo.Distinct().ToArray();


                for (int i = 0; i < caja.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        tipoMOVIMIENTO += caja.Rows[i]["TIPO"].ToString() + caja.Rows[i]["CC_ID_TIPO"].ToString();
                    }
                    else
                    {
                        tipoMOVIMIENTO += "-" + caja.Rows[i]["TIPO"].ToString() + caja.Rows[i]["CC_ID_TIPO"].ToString();
                    }
                }
                tipo2 = tipoMOVIMIENTO.Split(separador2).ToArray();
                tipo2 = tipo2.Distinct().ToArray();

                Ticket ticketcaja = new Ticket();

                ticketcaja.AddHeaderLine_2("REPORTE CAJA");
                ticketcaja.AddHeaderLine("");
                ticketcaja.AddSubHeaderLine("         " + Convert.ToDateTime(cierres.Where(w => w.ID == idCierre).FirstOrDefault().DC_F_CREATE.ToString()).ToShortDateString());
                ticketcaja.AddSubHeaderLine("");
                for (int p = 0; p < tipo.Distinct().Count(); p++)
                {

                    ticketcaja.AddSubHeaderLine((tipo[p].ToString() == "1") ? "INGRESO : " : "EGRESO :");
                    ticketcaja.AddSubHeaderLine("");
                    for (int k = 0; k < tipo2.Distinct().Count(); k++)
                    {
                        if (tipo2[k].ToString().Substring(tipo2[k].Length - 1, 1) == tipo[p].ToString())
                        {
                            ticketcaja.AddSubHeaderLine(tipo2[k].ToString().Substring(0, tipo2[k].Length - 1));
                            string dotted = "";
                            for (int x = 0; x < Convert.ToInt32(negParametros.MaxCharDescriptionTicket()); x++)
                            {
                                dotted += "-";
                            }
                            ticketcaja.AddSubHeaderLine(dotted);
                        }

                        //ticketcaja.AddSubHeaderLine("");
                        for (int j = 0; j < caja.Rows.Count; j++)
                        {
                            if (caja.Rows[j]["TIPO"].ToString() == tipo2[k].ToString().Substring(0, tipo2[k].Length - 1) && caja.Rows[j]["CC_ID_TIPO"].ToString() == (tipo[p].ToString()))
                            {
                                //ticketcaja.AddReporteItemventas(caja.Rows[j]["DESCRIPCION"].ToString(), "", (caja.Rows.Count > 0) ? caja.Rows[j]["MONTO"].ToString() : "0.00");
                                ticketcaja.AddSubHeaderLine(caja.Rows[j]["DESCRIPCION"].ToString() + " : " + Convert.ToString((caja.Rows.Count > 0) ? caja.Rows[j]["MONTO"].ToString() : "0.00"));
                            }
                        }
                        ticketcaja.AddSubHeaderLine("");


                    }

                    ticketcaja.AddSubHeaderLine("");

                }
                //ticketcaja.AddDocElectronicoAnuladosTotal("TOTAL MONTO", "", (caja.Rows.Count > 0) ? "S/" + caja.Rows[0]["MONTO"].ToString() : "0.00");
                ticketcaja.AddDocElectronicoAnuladosTotal("INGRESO", "", (caja.Rows.Count > 0) ? "S/" + caja.Rows[0]["INGRESO_CAJA"].ToString() : "0.00");
                ticketcaja.AddDocElectronicoAnuladosTotal("EGRESO", "", (caja.Rows.Count > 0) ? "S/" + caja.Rows[0]["EGRESO_CAJA"].ToString() : "0.00");
                ticketcaja.AddDocElectronicoAnuladosTotal("TOTAL", "", (caja.Rows.Count > 0) ? "S/" + caja.Rows[0]["SALDO_CAJA"].ToString() : "0.00");
                ticketcaja.AddFooterLine("");

                if (ticketcaja.PrinterExists(ImpCaja))
                {
                    ticketcaja.PrintTicket(ImpCaja);
                }
                else
                {
                    globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                }
            }
            /*****************************************************FIN **********************************************************/
            #endregion


            //}
        }
        public ObservableCollection<Empresa> empresa { get; set; }
        public ObservableCollection<EnvioCierre> VentaDia { get; set; }
        Neg_CierreParcial negCierres = new Neg_CierreParcial();
        Neg_Empresa negEmpresa = new Neg_Empresa();
        public void Enviar(object id)
        {
            int Desc;
            string verificar = InternetGetConnectedState(out Desc, 0).ToString();
            if ((Convert.ToBoolean(verificar) == true))
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                DataTable DetallePagos = new DataTable();
                DetallePagos = negCierres.GetDetallePagosCierre(id.ToString());
                VentaDia = negCierres.GetVentaDiaCierre(id.ToString());
                empresa = new ObservableCollection<Empresa>();

                string json = "";
                empresa = negEmpresa.GetEmpresa();

                json = negCierres.GetCierreJson();
                var jsonDatareq = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(VentaDia, Formatting.Indented));
                //foreach (var e in correo)
                //{
                try
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        values["id_empresa"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                        values["fecha"] = VentaDia.First().FECHA.ToString("yyyy/MM/dd");
                        values["monto_total"] = VentaDia.First().TOTALVENTA;
                        values["monto_descuento"] = (Convert.ToDecimal(VentaDia.First().VBRUTA) - Convert.ToDecimal(VentaDia.First().TOTALVENTA)).ToString();
                        values["total_ingresos_caja"] = VentaDia.First().INGRESO;
                        values["total_egresos_caja"] = VentaDia.First().EGRESO;
                        values["total_efectivo"] = DetallePagos.Rows[0]["EFECTIVO"].ToString();
                        values["total_visa"] = DetallePagos.Rows[0]["VISA"].ToString();
                        values["total_mastercard"] = DetallePagos.Rows[0]["MASTERCARD"].ToString();
                        values["total_boletas"] = VentaDia.First().TOTALBOLE;
                        values["cantidad_boletas"] = VentaDia.First().BOLECANT;
                        values["total_facturas"] = VentaDia.First().TOTALFACT;
                        values["cantidad_facturas"] = VentaDia.First().FACTCANT;
                        values["id_cierre"] = id.ToString();
                        values["nombre_empresa"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().empr_nom.ToString();
                        values["correo"] = json;
                        var response = client.UploadValues(negParametros.EnvioCierre(), values);
                        var responseString = Encoding.Default.GetString(response);
                        //var WebService = new ConsultaWebService();
                        //WebService = JsonConvert.DeserializeObject<ConsultaWebService>(responseString);
                        //if (WebService.cambio_info == 0)
                        //{

                        //}
                    }
                    using (var client = new WebClient())
                    {
                        DataTable dtTopPlatos = new DataTable();
                        dtTopPlatos = negCierres.GetTopPlatosCierre(id.ToString());
                        if (dtTopPlatos.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtTopPlatos.Rows.Count; j++)
                            {
                                var values = new NameValueCollection();
                                values["id_business"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                                values["fecha"] = VentaDia.First().FECHA.ToString("yyyy/MM/dd");
                                values["id_carta"] = dtTopPlatos.Rows[j]["DP_ID_CARTA"].ToString();
                                values["nom_carta"] = dtTopPlatos.Rows[j]["DP_DESCRIP"].ToString();
                                values["cant"] = dtTopPlatos.Rows[j]["DP_CANT"].ToString();
                                values["importe"] = dtTopPlatos.Rows[j]["DP_IMPORT"].ToString();
                                values["id_cierre"] = id.ToString();
                                var response = client.UploadValues(negParametros.SetPlatosVendidosHistorial(), values);
                                var responseString = Encoding.Default.GetString(response);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    //globales.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
                }
            }
                
        }
        public void buscarporfechas()
        {
            string desdet = Convert.ToDateTime(DpVentas.desde).ToString("yyyy-MM-dd");
            string hastat = Convert.ToDateTime(DpVentas.hasta).ToString("yyyy-MM-dd");
            string mes = Convert.ToDateTime(DpVentas.mes).ToString("yyyy-MM-dd");
            string dia = Convert.ToDateTime(DpVentas.dia).ToString("yyyy-MM-dd");
            if (Idradiobuton == "1")
            {
                cierres = negCierre.GET_CIERRES_X_DIA(dia);
            }else if (Idradiobuton == "3")
            {
                cierres = negCierre.GET_CIERRES_ENTRE_DIA(desdet,hastat);
            }

        }
        private void IdRadiobuton(object id)
        {
            Idradiobuton = id.ToString();
            
        }
        void valorbt()
        {
            rbbtdia = "1";
            rbbtmes = "2";
            rbbtdesdehasta = "3";
        }
    }
}

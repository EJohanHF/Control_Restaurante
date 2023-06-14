using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Facturacion_Electronica;
using Capa_Entidades.Models.Reportes.Rpt_BoletayFactura;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Facturacion_Electronica;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Reportes.Report_BoletayFactura;
using Capa_Presentacion_WPF.Views.Dialogs;
using ExportToExcel;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using ThoughtWorks.QRCode.Codec;
using tickets;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.Report_BoletayFactura
{
    public class VM_Rpt_BoletayFactura : INotifyPropertyChanged
    {
        #region INotify
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));

        }
        #endregion
        BoletaFacturaStructure bfstructure = new BoletaFacturaStructure();
        #region Negocio
        Neg_BoletayFactura negBolFact = new Neg_BoletayFactura();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Parametros negParametros = new Neg_Parametros();
        Neg_Doc_Electronico negDocElectronico = new Neg_Doc_Electronico();
        Neg_Empresa negEmpr = new Neg_Empresa();
        Funcion_Global globales = new Funcion_Global();
        #endregion
        #region Entidad
        private Ent_RptBoletayFactura _Ent_RptBoletayFactura;
        public Ent_RptBoletayFactura Ent_RptBoletayFactura
        {
            get => _Ent_RptBoletayFactura;
            set
            {
                _Ent_RptBoletayFactura = value;
            }
        }

        #endregion
        #region Objetos
        private string _operacion;
        public string NomEmpr { get; set; }
        public string RucEmpr { get; set; }
        public string DirEmpr { get; set; }
        public string DistEmpr { get; set; }
        public string NomTipDoc { get; set; }
        public int id { get; set; }
        public string idTipoDocSeleccionado { get; set; }
        public int idTipoEstadoDoc { get; set; } = 1;
        public bool ischeckedTipoDoc { get; set; } = false;
        public string TextoBuscar { get; set; } = "";
        public decimal CONSOLIDADO_IGV { get; set; } = 0;
        public decimal CONSOLIDADO_RC { get; set; } = 0;
        public decimal CONSOLIDADO_ICBPER { get; set; } = 0;
        public decimal CONSOLIDADO_TOTAL { get; set; } = 0;
        public decimal CONSOLIDADO_OPGRAVADAS { get; set; } = 0;
        public int CantRegistrosDocElec { get; set; } = 0;

        //

        private DateTime _startDate = DateTime.Now;
        public event PropertyChangedEventHandler PropertyChanged_date;
        private DateTime _FinishDate = DateTime.Now;
        public event PropertyChangedEventHandler PropertyChanged_finishdate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged_date("StartDate"); }
        }
        public void OnPropertyChanged_date(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_date;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
        public DateTime FinishDate
        {
            get { return _FinishDate; }
            set { _FinishDate = value; OnPropertyChanged_finishdate("FinishDate"); }
        }
        public void OnPropertyChanged_finishdate(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_finishdate;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
        private string _nroBuscarDoc;
        public event PropertyChangedEventHandler PropertyChanged_nroBuscarDoc;
        public string nroBuscarDoc
        {
            get { return _nroBuscarDoc; }
            set
            {
                int temp = 0;
                if (value.ToString() == "")
                {
                    getLista();
                }
                else { if (int.TryParse(value.ToString(), out temp)) { _nroBuscarDoc = value; } }

                OnPropertyChanged_nroBuscarDoc("nroBuscarDoc");
            }
        }

        public void OnPropertyChanged_nroBuscarDoc(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged_nroBuscarDoc;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));


            //codigo
        }

        #endregion
        #region GetSetObjetos
        public string Operacion
        {
            get => _operacion;
            set
            {
                _operacion = value;
                if (value == "Lista")
                {
                    getLista();
                }
            }
        }
        #endregion
        #region Seleccion
        private Tipo_Doc_Electronico _TipoDocSelected;
        public Tipo_Doc_Electronico TipoDocSelected
        {
            get => _TipoDocSelected;
            set
            {
                if (value != null)
                {
                    idTipoDocSeleccionado = ((Tipo_Doc_Electronico)value).VALOR_TIPO_DOC;
                }
                _TipoDocSelected = value;
            }
        }
        private Estado_Doc_Electronico _EstDocSelected;
        public Estado_Doc_Electronico EstDocSelected
        {
            get => _EstDocSelected;
            set
            {
                if (value != null)
                {
                    idTipoEstadoDoc = ((Estado_Doc_Electronico)value).DOC_ESTADO;
                }
                _EstDocSelected = value;
            }
        }
        #endregion
        #region Listas
        public ObservableCollection<Ent_RptBoletayFactura> DataBoletaFactura { get; set; }
        public ObservableCollection<Ent_RptBoletayFactura> DataConsolidado { get; set; }
        public ObservableCollection<Tipo_Doc_Electronico> TipDocElectr { get; set; }
        public Neg_Tip_Doc_Electronico negTipDocElec = new Neg_Tip_Doc_Electronico();
        static ObservableCollection<Empresa> empresa = new ObservableCollection<Empresa>();
        public ObservableCollection<Ent_RptBoletayFactura> DataEstadoDocumento = new ObservableCollection<Ent_RptBoletayFactura>();
        public ObservableCollection<Estado_Doc_Electronico> DataEstDocumento { get; set; }
        #endregion
        #region Commands
        public ICommand AnularDocCommand { get; set; }
        public ObservableCollection<Data_Documento_Electronico> DataDocumentoElectronico { get; set; }
        Neg_Data_Documento_Electronico negDataDocumentoElectronico = new Neg_Data_Documento_Electronico();
        public ICommand ReimprimirDocCommand { get; set; }
        public ICommand IsCheckedTipoDocCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand GenerarFE { get; set; }
        public ICommand ReImprimirTodoCommand { get; set; }
        public ICommand BuscarSerieCommand { get; set; }
        public ICommand GenerarR { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand DownloadPdfCommand { get; set; }
        int _existe = 0;
        #endregion
        public VM_Rpt_BoletayFactura()
        {
            this.AnularDocCommand = new ParamCommand(new Action<object>(AnularDoc));
            this.ReimprimirDocCommand = new ParamCommand(new Action<object>(imprimir_CPE));
            this.DownloadPdfCommand = new ParamCommand(new Action<object>(DownloadPdf));
            this.ReImprimirTodoCommand = new RelayCommand(new Action(imprimir_CPE_todo));
            this.IsCheckedTipoDocCommand = new RelayCommand(new Action(IsCheckedTipoDoc));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.GenerarFE = new RelayCommand(new Action(GeneraFE));
            this.BuscarSerieCommand = new RelayCommand(new Action(BuscarSerie));
            this.GenerarR = new RelayCommand(new Action(GenerarResumen));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            DataBoletaFactura = new ObservableCollection<Ent_RptBoletayFactura>();
            Ent_RptBoletayFactura = new Ent_RptBoletayFactura();
            DataEstDocumento = bfstructure.GetEstadoDocumentos();
            this.Operacion = "Lista";
        }
        public void DownloadPdf(object iddoc)
        {
            string URL = negParametros.URLEasyfact();
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            saveFileDialog1.FileName = "Reporte Documentos Electronico" + DateTime.Now.ToString("yyyyMMddHHmmss");

            DataTable dt = new DataTable();
            dt = negDocElectronico.GetDocElectronico(Convert.ToInt32(iddoc));

            string html = Properties.Resources.Plantilla.ToString();
            html = html.Replace("@RAZONSOCIALEMISOR", dt.Rows[0]["EMPR_NOM"].ToString());
            html = html.Replace("@DIRECCIONEMPRESA", dt.Rows[0]["EMPR_DIREC"].ToString());
            html = html.Replace("@DEPARTAMENTO", dt.Rows[0]["nomdepa"].ToString().ToUpper() + " - " + dt.Rows[0]["nomprov"].ToString().ToUpper() + " - " + dt.Rows[0]["nomdist"].ToString().ToUpper());
            html = html.Replace("@TELEFONO", dt.Rows[0]["EMPR_TEL"].ToString());
            html = html.Replace("@RUC", dt.Rows[0]["numeroDocIdentidadEmisor"].ToString());
            html = html.Replace("@TIPODOCUMENTO", dt.Rows[0]["NOM_TIPO_DOC"].ToString());
            html = html.Replace("@SERIE", dt.Rows[0]["serieNumero"].ToString());
            html = html.Replace("@FECHAEMISION", dt.Rows[0]["fechaEmision"].ToString());
            html = html.Replace("@RAZONSOCIALRECEPTOR", dt.Rows[0]["razonSocialReceptor"].ToString());
            html = html.Replace("@NRODOCUMENTORECEPTOR", dt.Rows[0]["numeroDocIdentidadReceptor"].ToString());
            html = html.Replace("@DIRECCIONRECEPTOR", dt.Rows[0]["direccionReceptor"].ToString());

            html = html.Replace("@OpGravadas", dt.Rows[0]["totalOPGravadas"].ToString());
            html = html.Replace("@Exoneradas", dt.Rows[0]["totalOPExoneradas"].ToString());
            html = html.Replace("@Igv", dt.Rows[0]["sumatoriaIGV"].ToString());
            html = html.Replace("@OtroCargo", dt.Rows[0]["sumatoriaOtrosCargos"].ToString());
            html = html.Replace("@Icbper", dt.Rows[0]["sumatoriaImpuestoBolsas"].ToString());
            html = html.Replace("@TotalCobrar", dt.Rows[0]["importeTotalVenta"].ToString());


            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

            qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M;
            qrCodeEncoder.QRCodeScale = Convert.ToInt32(3);
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor);
            qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor);

            string textoQR = "";
            textoQR += dt.Rows[0]["numeroDocIdentidadEmisor"].ToString() + "|";
            textoQR += dt.Rows[0]["tipoDocumento"].ToString() + "|";
            textoQR += dt.Rows[0]["serieNumero"].ToString().Substring(0, 4) + "|";
            textoQR += dt.Rows[0]["serieNumero"].ToString().Substring(5) + "|";
            textoQR += dt.Rows[0]["sumatoriaIGV"].ToString() + "|";
            textoQR += dt.Rows[0]["importeTotalVenta"].ToString() + "|";
            textoQR += dt.Rows[0]["fechaEmision"].ToString() + "|";


            Bitmap bitmap = qrCodeEncoder.Encode(textoQR, System.Text.Encoding.UTF8);
            System.Drawing.Image qr = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["EMPR_LOGO"]));
            iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(qr, System.Drawing.Imaging.ImageFormat.Png);

            //html = html.Replace("@imagen", dt.Rows[0]["montoEnLetras"].ToString());

            html = html.Replace("@MONTOENLETRAS", dt.Rows[0]["montoEnLetras"].ToString());
            html = html.Replace("@RUTADESCARGAR", URL);

            string filas = String.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + dr["cantidadItem"].ToString() + "</td>";
                filas += "<td>NIU</td>";
                filas += "<td>" + dr["descripcionItem"].ToString() + "</td>";
                filas += "<td>" + dr["valorVentaItem"].ToString() + "</td>";
                filas += "<td>" + dr["_precioUnitario"].ToString() + "</td>";
                filas += "<td>" + dr["descuentoItem"].ToString() + "</td>";
                filas += "<td>" + dr["montoTotalItem"].ToString() + "</td>";
                filas += "</tr>";
            }

            html = html.Replace("@FILAS", filas);


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog1.FileName, FileMode.Create))
                {
                    Document pdf = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdf, stream);
                    pdf.Open();
                    pdf.Add(new Phrase(""));
                    System.Drawing.Image logo = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["EMPR_LOGO"]));
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(logo, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(90, 70);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;
                    img.SetAbsolutePosition(pdf.LeftMargin, pdf.Top - 60);
                    pdf.Add(img);

                    //Bitmap bitmap = qrCodeEncoder.Encode(textoQR, System.Text.Encoding.UTF8);
                    //System.Drawing.Image qr = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["EMPR_LOGO"]));
                    //iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(qr, System.Drawing.Imaging.ImageFormat.Png);
                    //img2.ScaleToFit(90, 70);
                    //img2.Alignment = iTextSharp.text.Image.UNDERLYING;
                    //img2.SetAbsolutePosition(pdf.LeftMargin, pdf.Top - 60);
                    ////img.SetAbsolutePosition(0, 0);
                    //pdf.Add(img2);

                    using (StringReader sr = new StringReader(html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdf, sr);
                    }
                    pdf.Close();
                    stream.Close();

                }

            }
        }
        private void GeneraFE()
        {
            //int i = 1;
            foreach (var item2 in this.DataBoletaFactura)
            {
                this.DataDocumentoElectronico = negDataDocumentoElectronico.GetDataDocumentoElectronicoxId(item2.id_doc_electronico);
                if (DataDocumentoElectronico.Count == 0)
                {}
                else
                {
                    try
                    {
                        string rutaFacturacion = negParametros.rutaFacturacion(); //S
                        String linea_cabecera = negDocElectronico.getCabecera(item2.id_doc_electronico);
                        String linea_detalle = negDocElectronico.getDetalle(item2.id_doc_electronico);
                        String linea_tributo = negDocElectronico.getTributo(item2.id_doc_electronico);

                        //string str_nombre_fichero = "";

                        //if (i < 10)
                        //{
                        //    str_nombre_fichero = item2.numeroDocIdentidadEmisor + "-07-" + item2.serieNumero.Substring(0, 5) + "0000000" + i;
                        //}
                        //else if (i < 100 && i > 10)
                        //{
                        //    str_nombre_fichero = item2.numeroDocIdentidadEmisor + "-07-" + item2.serieNumero.Substring(0, 5) + "000000" + i;
                        //}
                        //else if (i < 1000 && i > 100)
                        //{
                        //    str_nombre_fichero = item2.numeroDocIdentidadEmisor + "-07-" + item2.serieNumero.Substring(0, 5) + "00000" + i;
                        //}

                        string str_nombre_fichero = item2.numeroDocIdentidadEmisor + "-" + item2.tipoDocumento + "-" + item2.serieNumero;

                        //string fichero2 = rutaFacturacion + str_nombre_fichero + ".NOT";
                        string fichero2 = rutaFacturacion + str_nombre_fichero + ".cab";
                        string ficheroDetalle = rutaFacturacion + str_nombre_fichero + ".det";
                        string ficherotri = rutaFacturacion + str_nombre_fichero + ".tri";

                        System.IO.StreamWriter a = new System.IO.StreamWriter(fichero2);
                        a.WriteLine(linea_cabecera);
                        a.Close();

                        System.IO.StreamWriter b = new System.IO.StreamWriter(ficheroDetalle);
                        b.WriteLine(linea_detalle);
                        b.Close();

                        System.IO.StreamWriter c = new System.IO.StreamWriter(ficherotri);
                        c.WriteLine(linea_tributo);
                        c.Close();
                        //i = i + 1;

                    }
                    catch (Exception ex)
                    {
                        //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);

                    }
                }
            }
        }
        private void BuscarSerie()
        {
            try
            {
                if (TextoBuscar == "" || TextoBuscar == null) {
                    getLista();
                    return;
                }
                if (StartDate == null || FinishDate == null)
                {
                    globales.Mensaje("SOS-FOOD - Información:", "Fechas invalidas", 2);
                    return;
                }

                List<Ent_RptBoletayFactura> _bf = new List<Ent_RptBoletayFactura>();
                _bf = bfstructure.GetDocElectronicosxFiltroSerie(TextoBuscar);
                ObservableCollection<Ent_RptBoletayFactura> bf = new ObservableCollection<Ent_RptBoletayFactura>(_bf);
                DataBoletaFactura = bf;
                if (DataBoletaFactura.Count() == 0)
                {
                    globales.Mensaje("SOS-FOOD - Información:", "No hay datos", 2);
                    this.CONSOLIDADO_OPGRAVADAS = 0;
                    this.CONSOLIDADO_IGV = 0;
                    this.CONSOLIDADO_RC = 0;
                    this.CONSOLIDADO_ICBPER = 0;
                    this.CONSOLIDADO_TOTAL = 0;
                    this.CONSOLIDADO_TOTAL = 0;
                    this.CantRegistrosDocElec = 0;
                    return;
                }
                //CONSOLIDADO
                //DataConsolidado = bfstructure.GetConsolidadoDocElectronicos(StartDate, FinishDate);
                this.CONSOLIDADO_OPGRAVADAS = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.totalOPGravadas);
                this.CONSOLIDADO_IGV = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.sumatoriaIGV);
                this.CONSOLIDADO_RC = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.sumatoriaOtrosCargos);
                this.CONSOLIDADO_ICBPER = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.sumatoriaImpuestoBolsas);
                this.CONSOLIDADO_TOTAL = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.importeTotal);
                this.CantRegistrosDocElec = DataBoletaFactura.Count();
            }
            catch (Exception)
            {
                globales.Mensaje("SOS-FOOD - Error: ", "No se pudo encontrar datos", 3);
               //throw;
            }

        }
        private void Buscar()
        {
            if (idTipoDocSeleccionado == null)
            {
                globales.Mensaje("SOS-FOOD - Información:", "Debe seleccionar el tipo de documento", 2);
            }
            if (StartDate == null || FinishDate == null || idTipoDocSeleccionado == null)
            {
                return;
            }

            List<Ent_RptBoletayFactura> _bf = new List<Ent_RptBoletayFactura>();
            _bf = bfstructure.GetDocElectronicosxFiltro(StartDate, FinishDate, idTipoDocSeleccionado, idTipoEstadoDoc);
            ObservableCollection<Ent_RptBoletayFactura> bf = new ObservableCollection<Ent_RptBoletayFactura>(_bf);
            DataBoletaFactura = bf;

            if (DataBoletaFactura.Count() == 0)
            {
                globales.Mensaje("SOS-FOOD - Información:", "No hay datos", 2);
                this.CONSOLIDADO_OPGRAVADAS = 0;
                this.CONSOLIDADO_IGV = 0;
                this.CONSOLIDADO_RC = 0;
                this.CONSOLIDADO_ICBPER = 0;
                this.CONSOLIDADO_TOTAL = 0;
                this.CantRegistrosDocElec = 0;
                return;
            }
            //CONSOLIDADO
            //DataConsolidado = bfstructure.GetConsolidadoDocElectronicos(StartDate, FinishDate);
            this.CONSOLIDADO_OPGRAVADAS = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.totalOPGravadas);
            this.CONSOLIDADO_IGV = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.sumatoriaIGV);
            this.CONSOLIDADO_RC = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.sumatoriaOtrosCargos);
            this.CONSOLIDADO_ICBPER = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.sumatoriaImpuestoBolsas);
            this.CONSOLIDADO_TOTAL = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.importeTotal);
            this.CantRegistrosDocElec = DataBoletaFactura.Count();

        }
        private void GenerarResumen()
        {
            try
            {
                if (StartDate == null)
                {
                    return;
                }
                empresa = negEmpr.GetEmpresa();

                string rutaFacturacion = negParametros.rutaFacturacion(); //S
                String linea_resumen = negDocElectronico.getResumen(StartDate.ToString("yyyy-MM-dd"));
                String linea_resumen_trd = negDocElectronico.getResumenTrd(StartDate.ToString("yyyy-MM-dd"));
                Random rnd = new Random();
                int number = rnd.Next(100, 999);
                string str_nombre_fichero = empresa.ToList().FirstOrDefault().empr_ruc + "-RC-" + DateTime.Now.ToString("yyyyMMdd") + "-" + number.ToString();

                string ficheroResumen = rutaFacturacion + str_nombre_fichero + ".RDI";
                string ficheroResumenTrd = rutaFacturacion + str_nombre_fichero + ".TRD";

                //string fichero = negParametros.RutaFePrueba() + str_nombre_fichero + ".INPUT.txt";
                System.IO.StreamWriter a = new System.IO.StreamWriter(ficheroResumen);
                a.WriteLine(linea_resumen);
                a.Close();

                System.IO.StreamWriter b = new System.IO.StreamWriter(ficheroResumenTrd);
                b.WriteLine(linea_resumen_trd);
                b.Close();
                globales.Mensaje("SOS-FOOD - Información:", "Resumen de comprobantes generado correctamente", 2);
            }
            catch (Exception)
            {
                globales.Mensaje("SOS-FOOD - Error:", "Algo salió mal al generar el resumen de comprobantes", 3);
            }
        }
        private void IsCheckedTipoDoc()
        {
            if (ischeckedTipoDoc == false)
            {
                getLista();
            }
        }
        private async void AnularDoc(object id_doc_electronico)
        {
            int idDocumento = Convert.ToInt32(id_doc_electronico);
            Ent_RptBoletayFactura.id_doc_electronico = idDocumento;

            string nroDocumentoEmisor = DataBoletaFactura.Where(w => w.id_doc_electronico == idDocumento).FirstOrDefault().numeroDocIdentidadEmisor;
            DateTime fechaEmision = DataBoletaFactura.Where(w => w.id_doc_electronico == idDocumento).FirstOrDefault().fechaEmision;
            string tipoDocumentoEmisor = DataBoletaFactura.Where(w => w.id_doc_electronico == idDocumento).FirstOrDefault().tipoDocIdentidadEmisor;
            string fechaGeneracion = String.Format(DateTime.Today.Date.ToString(), "yyyy-MM-dd");
            string tipodocumento = DataBoletaFactura.Where(w => w.id_doc_electronico == idDocumento).FirstOrDefault().tipoDocumento;
            string serie = DataBoletaFactura.Where(w => w.id_doc_electronico == idDocumento).FirstOrDefault().serieNumero;

            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "Desea anular el documento " + serie + " ?" }
            };
            var y = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)y)
            {
                return;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("fecGeneracion");
            dt.Columns.Add("fecComunicacion");
            dt.Columns.Add("tipDocBaja");
            dt.Columns.Add("numDocBaja");
            dt.Columns.Add("desMotivoBaja");
            dt.Rows.Add(fechaEmision.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), tipodocumento, serie, "ERROR DE DIGITACION");

            List<string> aAnulacion = new List<string>();
            aAnulacion.Add(fechaEmision.ToString("yyyy-MM-dd"));
            aAnulacion.Add(DateTime.Now.ToString("yyyy-MM-dd"));
            aAnulacion.Add(tipodocumento);
            aAnulacion.Add(serie);
            aAnulacion.Add("ERROR DE DIGITACION");

            string linea_anulacion = string.Join("|", aAnulacion.ToArray());
            Random numAleatorio = new Random();

            //string nombre_fichero = nroDocumentoEmisor + "-RA-" + DateTime.Now.ToString("yyyyMMdd") + "-" + System.Convert.ToString(numAleatorio.Next(100, 999));

            //string fichero = negParametros.rutaFacturacion() + nombre_fichero + ".CBA";

            //System.IO.StreamWriter a = new System.IO.StreamWriter(fichero);
            //a.WriteLine(linea_anulacion);
            //a.Close();

            try
            {
                var x = new MessageDialog
                {
                    Mensaje = { Text = "El documento " + serie + " fue anulado con exito" }
                };
                var x2 = await DialogHost.Show(x, "RootDialog");
                bool result = negBolFact.SetFacturaBoleta(3, Ent_RptBoletayFactura);
                
                if (ischeckedTipoDoc == true)
                {
                    Buscar();
                }
                else
                {
                    getLista();
                }

                if (!result)
                {
                    globales.Mensaje("SOS-FOOD - Error:", "Error al anular el documento", 3);
                }
            }
            catch (Exception)
            {
                globales.Mensaje("SOS-FOOD - Error:", "Error al anular el documento", 3);
               //throw;
            }
        }
        public ClsRespuestaTicketElectronico _fileReader(string ruta)
        {
            string fileReader = "";
            fileReader = File.ReadAllText(ruta);
            ClsRespuestaTicketElectronico respuesta_fe = new ClsRespuestaTicketElectronico();
            respuesta_fe = JsonConvert.DeserializeObject<ClsRespuestaTicketElectronico>(fileReader);
            return respuesta_fe;
        }
        int qrBackColor = Color.FromArgb(255, 255, 255, 255).ToArgb();
        int qrForeColor = Color.FromArgb(255, 0, 0, 0).ToArgb();
        public async void imprimir_CPE_todo()
        {
            try
            {
                if (DataBoletaFactura.Count() == 0)
                {
                    globales.Mensaje("SOS-FOOD - Información:", "No hay datos", 2);
                    return;
                }
                var SiNo = new SiNoMessageDialog
                {
                    Mensaje = { Text = "¿Desea imprimir copias de todos los comprobantes electrónicos que se muestran en pantalla?" }
                };
                var x = await DialogHost.Show(SiNo, "RootDialog");
                if (!(bool)x)
                    return;
                foreach (Ent_RptBoletayFactura bf in DataBoletaFactura.Where(w => w.Doc_Estado != false))
                {
                    int id_CPE = bf.id_doc_electronico;

                    string tipDocnto = DataBoletaFactura.Where(w => w.id_doc_electronico == id_CPE).FirstOrDefault().tipoDocumento;
                    this.NomTipDoc = this.TipDocElectr.Where(t => t.VALOR_TIPO_DOC == tipDocnto).ToList().FirstOrDefault().NOM_TIPO_DOC;

                    empresa = negEmpr.GetEmpresa();
                    this.NomEmpr = empresa.ToList().FirstOrDefault().empr_nom;
                    this.RucEmpr = empresa.ToList().FirstOrDefault().empr_ruc;
                    this.DirEmpr = empresa.ToList().FirstOrDefault().empr_direc;
                    this.DistEmpr = empresa.ToList().FirstOrDefault().empr_depa + "-" + empresa.ToList().FirstOrDefault().empr_prov + "-" + empresa.ToList().FirstOrDefault().empr_dist;

                    if (negFuncionGlobal.ImpCaja().Trim() == "")
                    {
                        return;
                    }

                    DataTable dt = new DataTable();
                    string URL = negParametros.URLEasyfact();
                    dt = negDocElectronico.GetDocElectronico(id_CPE);
                    Ticket ticket = new Ticket();
                    ticket.MaxChar = Convert.ToInt32(negParametros.NroColumnasTicket());
                    ticket.MaxCharDescription = Convert.ToInt32(negParametros.MaxCharDescriptionTicket());
                    ticket.MaxCharDescrip = negParametros.margenMaxDescrip();

                    ObservableCollection<Empresa> dtEmpresa = new ObservableCollection<Empresa>();
                    dtEmpresa = negEmpr.GetEmpresa();
                    System.Drawing.Image logo = (Bitmap)((new ImageConverter()).ConvertFrom(dtEmpresa.FirstOrDefault().empr_logo));
                    ticket.MargenLogo = negParametros.margenLogoTicket();
                    ticket.MaxCharDescrip = negParametros.margenMaxDescrip();
                    ticket.HeaderImage = logo;

                    ticket.AddHeaderLine(this.RucEmpr.ToUpper());
                    ticket.AddHeaderLine(this.NomEmpr.ToUpper());
                    ticket.AddHeaderLine(this.DirEmpr.ToUpper());
                    ticket.AddHeaderLine(this.DistEmpr.ToUpper());
                    // ticket.AddHeaderLine("JR. CAPAC YUPANQUI NRO. 2730 INT. 101 (A 1 CDRA DE WONG 2 DE MAYO S.I.)")
                    // ticket.AddHeaderLine("LIMA - LIMA - LINCE")

                    ticket.AddTitleLine("");
                    ticket.AddTitleLine(this.NomTipDoc.ToUpper());
                    ticket.AddTitleLine(dt.Rows[0]["serieNumero"].ToString());
                    ticket.AddTitleLine("");

                    ticket.AddSubHeaderLine("FECHA: " + dt.Rows[0]["fechaEmision"].ToString());
                    ticket.AddSubHeaderLine("NRO. ORDEN: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                    ticket.AddSubHeaderLine("MESA: " + dt.Rows[0]["M_NOM"].ToString() + "/MOZO: " + dt.Rows[0]["EMPL_NOM"].ToString().ToUpper());
                    ticket.AddSubHeaderLineBold("Cliente: " + dt.Rows[0]["razonSocialReceptor"].ToString());
                    ticket.AddSubHeaderLineBold("Nro. Doc : " + dt.Rows[0]["numeroDocIdentidadReceptor"].ToString());
                    ticket.AddSubHeaderLineBold("Dirección : " + dt.Rows[0]["direccionReceptor"].ToString());
                    ticket.AddSubHeaderLineBold("");

                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        ticket.AddItem("[" + dt.Rows[i]["cantidadItem"].ToString() + "]" + dt.Rows[i]["descripcionItem"].ToString(), dt.Rows[i]["_precioUnitario"].ToString(), dt.Rows[i]["_valorVentaSinIGV"].ToString());
                    }

                    decimal total = (decimal)((decimal)dt.Rows[0]["importeTotalVenta"] + (decimal)dt.Rows[0]["totalDescuentos"]);

                    decimal rc = negParametros.RC();
                    decimal igv = negParametros.IGV();

                    string IGV = (igv * 100).ToString();
                    string RC = (rc * 100).ToString();


                    ticket.AddTotal("OP. GRAVADAS", "S/    " + dt.Rows[0]["totalOPGravadas"].ToString());
                    ticket.AddTotal("OP. EXONERADAS", "S/    " + dt.Rows[0]["totalOPExoneradas"].ToString());
                    ticket.AddTotal("IGV (" + IGV + "%)", "S/    " + dt.Rows[0]["sumatoriaIGV"].ToString());
                    ticket.AddTotal("RC (" + RC + "%)", "S/    " + dt.Rows[0]["sumatoriaOtrosCargos"].ToString());
                    ticket.AddTotal("ICBPER", "S/    " + dt.Rows[0]["sumatoriaImpuestoBolsas"].ToString());
                    ticket.AddTotal("", "-----------");
                    ticket.AddTotal("SUB TOTAL", "S/    " + total);
                    ticket.AddTotal("TOTAL DESCUENTOS", "S/    " + dt.Rows[0]["totalDescuentos"].ToString());
                    ticket.AddTotal("TOTAL A COBRAR", "S/    " + dt.Rows[0]["importeTotalVenta"].ToString());
                    ticket.AddTotal("", "");

                    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

                    qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
                    // qrCodeEncoder.QRCodeEncodeMode = Codec.QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC
                    qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M;
                    qrCodeEncoder.QRCodeScale = Convert.ToInt32(3);
                    qrCodeEncoder.QRCodeVersion = 0;
                    qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor);
                    qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor);

                    string textoQR = "";
                    textoQR += this.RucEmpr.ToUpper() + "|";
                    textoQR += this.TipDocElectr.Where(t => t.VALOR_TIPO_DOC == tipDocnto).ToList().FirstOrDefault().VALOR_TIPO_DOC + "|";
                    textoQR += dt.Rows[0]["serieNumero"].ToString().Substring(0, 4) + "|";
                    textoQR += dt.Rows[0]["serieNumero"].ToString().Substring(5) + "|";
                    textoQR += dt.Rows[0]["sumatoriaIGV"].ToString() + "|";
                    textoQR += dt.Rows[0]["importeTotalVenta"].ToString() + "|";
                    textoQR += dt.Rows[0]["fechaEmision"].ToString() + "|";
                    // textoQR &= dt(0)("razonSocialReceptor").ToString & "|"
                    //textoQR += dt(0)("hash").ToString;

                    Bitmap bitmap = qrCodeEncoder.Encode(textoQR, System.Text.Encoding.UTF8);
                    ticket.QrImage = bitmap;

                    ticket.AddFooterLine("MONTO EN LETRAS: " + dt.Rows[0]["montoEnLetras"].ToString());
                    ticket.AddFooterLine("Representación impresa de comprobante de pago electrónico.");
                    ticket.AddFooterLine("Puede descargar este documento en el enlace");
                    ticket.AddFooterLine(URL);
                    ticket.AddFooterLine("");
                    ticket.AddFooterLine(negFuncionGlobal.PublicidadFacturacion());

                    //ticket.AddFooterLine("MONTO EN LETRAS: " + dt.Rows[0]["montoEnLetras"].ToString());
                    ////ticket.AddFooterLine("    HASH: " + dt(0)("hash").ToString);
                    //ticket.AddFooterLine("Representación impresa de comprobante de pago electrónico.");
                    //ticket.AddFooterLine("Puede descargar este documento en el enlace");
                    //ticket.AddFooterLine(URL);
                    //ticket.AddFooterLine("");
                    //ticket.AddFooterLine("Desarrollado por https://sos-tic.com/");
                    string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
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
            catch (Exception ex)
            {
                globales.Mensaje("SOS-FOOD - Error:", "Error al imprimir las copias de documentos electronicos" + ex.Message, 3);
                return;
            }
        }
        public void imprimir_CPE(object iddoc)
        {
            try
            {
                int id_CPE = Convert.ToInt32(iddoc);

                string tipDocnto = DataBoletaFactura.Where(w => w.id_doc_electronico == id_CPE).FirstOrDefault().tipoDocumento;
                this.NomTipDoc = this.TipDocElectr.Where(t => t.VALOR_TIPO_DOC == tipDocnto).ToList().FirstOrDefault().NOM_TIPO_DOC;

                empresa = negEmpr.GetEmpresa();
                this.NomEmpr = empresa.ToList().FirstOrDefault().empr_nom;
                this.RucEmpr = empresa.ToList().FirstOrDefault().empr_ruc;
                this.DirEmpr = empresa.ToList().FirstOrDefault().empr_direc;
                this.DistEmpr = empresa.ToList().FirstOrDefault().empr_depa + "-" + empresa.ToList().FirstOrDefault().empr_prov + "-" + empresa.ToList().FirstOrDefault().empr_dist;

                if (negFuncionGlobal.ImpCaja().Trim() == "")
                {
                    return;
                }

                DataTable dt = new DataTable();
                string URL = negParametros.URLEasyfact();
                dt = negDocElectronico.GetDocElectronico(id_CPE);
                Ticket ticket = new Ticket();
                ticket.MaxChar = Convert.ToInt32(negParametros.NroColumnasTicket());
                ticket.MaxCharDescription = Convert.ToInt32(negParametros.MaxCharDescriptionTicket());
                ticket.MaxCharDescrip = negParametros.margenMaxDescrip();

                ObservableCollection<Empresa> dtEmpresa = new ObservableCollection<Empresa>();
                dtEmpresa = negEmpr.GetEmpresa();
                ticket.MaxCharDescrip = negParametros.margenMaxDescrip();
                if (Convert.ToBoolean(negParametros.logoFacturacion()) == true) {
                    System.Drawing.Image logo = (Bitmap)((new ImageConverter()).ConvertFrom(dtEmpresa.FirstOrDefault().empr_logo));
                    ticket.MargenLogo = negParametros.margenLogoTicket();
                    ticket.HeaderImage = logo;
                }


                ticket.AddHeaderLine(dt.Rows[0]["numeroDocIdentidadEmisor"].ToString());
                ticket.AddHeaderLine(dt.Rows[0]["EMPR_NOM"].ToString());
                ticket.AddHeaderLine(dt.Rows[0]["EMPR_DIREC"].ToString());
                ticket.AddHeaderLine(dt.Rows[0]["nomdepa"].ToString() + " - " + dt.Rows[0]["nomprov"].ToString() + " - " + dt.Rows[0]["nomdist"].ToString());
                // ticket.AddHeaderLine("JR. CAPAC YUPANQUI NRO. 2730 INT. 101 (A 1 CDRA DE WONG 2 DE MAYO S.I.)")
                // ticket.AddHeaderLine("LIMA - LIMA - LINCE")

                ticket.AddTitleLine("");
                ticket.AddTitleLine(dt.Rows[0]["NOM_TIPO_DOC"].ToString());
                ticket.AddTitleLine(dt.Rows[0]["serieNumero"].ToString());
                ticket.AddTitleLine("");

                DateTime fecha = Convert.ToDateTime(dt.Rows[0]["fechaEmision"].ToString());
                string fecha2 = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                ticket.AddSubHeaderLine("FECHA: " + fecha2);
                ticket.AddSubHeaderLine("NRO. ORDEN: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                ticket.AddSubHeaderLine("MESA: " + dt.Rows[0]["M_NOM"].ToString() + "/MOZO: " + dt.Rows[0]["EMPL_NOM"].ToString().ToUpper());
                ticket.AddSubHeaderLineBold("Cliente: " + dt.Rows[0]["razonSocialReceptor"].ToString());
                ticket.AddSubHeaderLineBold("Nro. Doc : " + dt.Rows[0]["numeroDocIdentidadReceptor"].ToString());
                ticket.AddSubHeaderLineBold("Dirección : " + dt.Rows[0]["direccionReceptor"].ToString());
                ticket.AddSubHeaderLineBold("");

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    ticket.AddItem("[" + dt.Rows[i]["cantidadItem"].ToString() + "]" + dt.Rows[i]["descripcionItem"].ToString(), dt.Rows[i]["_precioUnitario"].ToString(), dt.Rows[i]["montoTotalItem"].ToString());
                }

                decimal total = (decimal)((decimal)dt.Rows[0]["importeTotalVenta"] + (decimal)dt.Rows[0]["totalDescuentos"]);

                decimal rc = negParametros.RC();
                decimal igv = negParametros.IGV();

                string IGV = (igv * 100).ToString();
                string RC = (rc * 100).ToString();


                ticket.AddTotal("OP. GRAVADAS", "S/    " + dt.Rows[0]["totalOPGravadas"].ToString());
                ticket.AddTotal("OP. EXONERADAS", "S/    " + dt.Rows[0]["totalOPExoneradas"].ToString());
                ticket.AddTotal("IGV (" + IGV + "%)", "S/    " + dt.Rows[0]["sumatoriaIGV"].ToString());
                ticket.AddTotal("RC (" + RC + "%)", "S/    " + dt.Rows[0]["sumatoriaOtrosCargos"].ToString());
                ticket.AddTotal("ICBPER", "S/    " + dt.Rows[0]["sumatoriaImpuestoBolsas"].ToString());
                ticket.AddTotal("", "-----------");
                ticket.AddTotal("SUB TOTAL", "S/    " + total);
                ticket.AddTotal("TOTAL DESCUENTOS", "S/    " + dt.Rows[0]["totalDescuentos"].ToString());
                ticket.AddTotal("TOTAL A COBRAR", "S/    " + dt.Rows[0]["importeTotalVenta"].ToString());
                ticket.AddTotal("", "");

                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

                qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
                // qrCodeEncoder.QRCodeEncodeMode = Codec.QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC
                qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M;
                qrCodeEncoder.QRCodeScale = Convert.ToInt32(3);
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor);
                qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor);
            
                string textoQR = "";
                textoQR += this.RucEmpr.ToUpper() + "|";
                textoQR += this.TipDocElectr.Where(t => t.VALOR_TIPO_DOC == tipDocnto).ToList().FirstOrDefault().VALOR_TIPO_DOC + "|";
                textoQR += dt.Rows[0]["serieNumero"].ToString().Substring(0, 4) + "|";
                textoQR += dt.Rows[0]["serieNumero"].ToString().Substring(5) + "|";
                textoQR += dt.Rows[0]["sumatoriaIGV"].ToString() + "|";
                textoQR += dt.Rows[0]["importeTotalVenta"].ToString() + "|";
                textoQR += dt.Rows[0]["fechaEmision"].ToString() + "|";
                // textoQR &= dt(0)("razonSocialReceptor").ToString & "|"
                //textoQR += dt(0)("hash").ToString;

                Bitmap bitmap = qrCodeEncoder.Encode(textoQR, System.Text.Encoding.UTF8);
                ticket.QrImage = bitmap;

                ticket.AddFooterLine("MONTO EN LETRAS: " + dt.Rows[0]["montoEnLetras"].ToString());
                ticket.AddFooterLine("Representación impresa de comprobante de pago electrónico.");
                ticket.AddFooterLine("Puede descargar este documento en el enlace");
                ticket.AddFooterLine(URL);
                ticket.AddFooterLine("");
                ticket.AddFooterLine(negFuncionGlobal.PublicidadFacturacion());

                //ticket.AddFooterLine("MONTO EN LETRAS: " + dt.Rows[0]["montoEnLetras"].ToString());
                ////ticket.AddFooterLine("    HASH: " + dt(0)("hash").ToString);
                //ticket.AddFooterLine("Representación impresa de comprobante de pago electrónico.");
                //ticket.AddFooterLine("Puede descargar este documento en el enlace");
                //ticket.AddFooterLine(URL);
                //ticket.AddFooterLine("");
                //ticket.AddFooterLine("Desarrollado por https://sos-tic.com/");
                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
                if (ticket.PrinterExists(ImpCaja))
                {
                    ticket.PrintTicket(ImpCaja);
                }
                else
                {
                    globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                }
            }
            catch (Exception ex)
            {
                globales.Mensaje("SOS-FOOD - Error:", "Error al imprimir la copia del documento electronico" + ex.Message, 3);
                return;
            }
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
            saveFileDialog1.FileName = "Reporte Documentos Electronicos" + DateTime.Now.ToString("yyyyMMddHHmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID DOC ELECTRONICO");
            dt.Columns.Add("SERIE NUMERO");
            dt.Columns.Add("TIPO DOCUMENTO");
            dt.Columns.Add("FECHA EMISION");
            dt.Columns.Add("NUMERO DOC IDENTIDAD EMISOR");
            dt.Columns.Add("TIPODOC IDENTIDAD EMISOR");
            dt.Columns.Add("NUMERO DOC IDENTIDAD RECEPTOR");
            dt.Columns.Add("RAZON SOCIAL RECEPTOR");
            dt.Columns.Add("DIRECCION RECEPTOR");
            dt.Columns.Add("CORREO RECEPTOR");
            dt.Columns.Add("TIPO DOC IDENTIDAD RECEPTOR");
            dt.Columns.Add("TELEFONO");
            dt.Columns.Add("TOTAL OP GRAVADAS");
            dt.Columns.Add("TOTAL OP EXONERADAS");
            dt.Columns.Add("TOTAL OP NOGRAVADAS");
            dt.Columns.Add("TOTAL OP GRATUITAS");
            dt.Columns.Add("SUMATORIA IGV");
            dt.Columns.Add("IMPORTE TOTAL");
            dt.Columns.Add("IMPORTE TOTAL VENTA");
            dt.Columns.Add("TOTAL DESCUENTOS");
            dt.Columns.Add("DESCUENTOS GLOBALES");
            dt.Columns.Add("SUMATORIA OTROS CARGOS");
            dt.Columns.Add("PORCENTAJE OTROS CARGOS");
            dt.Columns.Add("SUMATORIA IMPUESTO BOLSAS");
            dt.Columns.Add("MONTOENLETRAS");
            dt.Columns.Add("ID PEDIDO");
            dt.Columns.Add("DOC ESTADO");

            foreach (var p in DataBoletaFactura)
            {
                dt.Rows.Add(new object[] { p.id_doc_electronico,p.serieNumero,
                    p.tipoDocumento,p.fechaEmision,p.numeroDocIdentidadEmisor,p.tipoDocIdentidadEmisor,p.numeroDocIdentidadReceptor,
                    p.razonSocialReceptor,p.direccionReceptor,p.correoReceptor,p.tipoDocIdentidadReceptor,p.telefono,p.totalOPGravadas,
                    p.totalOPExoneradas,p.totalOPNoGravadas,p.totalOPGratuitas,p.sumatoriaIGV,p.importeTotal,p.importeTotalVenta,
                    p.totalDescuentos,p.descuentosGlobales,p.sumatoriaOtrosCargos,p.porcentajeOtrosCargos,p.sumatoriaImpuestoBolsas,
                    p.montoEnLetras,p.idPedido,p.Doc_Estado});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "Reporte de Documentos Electrónicos");
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
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            saveFileDialog1.FileName = "Reporte Documentos Electronicos" + DateTime.Now.ToString("yyyyMMddHHmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID DOC ELECTRONICO");
            dt.Columns.Add("SERIE NUMERO");
            dt.Columns.Add("TIPO DOCUMENTO");
            dt.Columns.Add("FECHA EMISION");
            dt.Columns.Add("NUMERO DOC IDENTIDAD EMISOR");
            dt.Columns.Add("TIPODOC IDENTIDAD EMISOR");
            dt.Columns.Add("NUMERO DOC IDENTIDAD RECEPTOR");
            dt.Columns.Add("RAZON SOCIAL RECEPTOR");
            dt.Columns.Add("DIRECCION RECEPTOR");
            dt.Columns.Add("CORREO RECEPTOR");
            dt.Columns.Add("TIPO DOC IDENTIDAD RECEPTOR");
            dt.Columns.Add("TELEFONO");
            dt.Columns.Add("TOTAL OP GRAVADAS");
            dt.Columns.Add("TOTAL OP EXONERADAS");
            dt.Columns.Add("TOTAL OP NOGRAVADAS");
            dt.Columns.Add("TOTAL OP GRATUITAS");
            dt.Columns.Add("SUMATORIA IGV");
            dt.Columns.Add("IMPORTE TOTAL");
            dt.Columns.Add("IMPORTE TOTAL VENTA");
            dt.Columns.Add("TOTAL DESCUENTOS");
            dt.Columns.Add("DESCUENTOS GLOBALES");
            dt.Columns.Add("SUMATORIA OTROS CARGOS");
            dt.Columns.Add("PORCENTAJE OTROS CARGOS");
            dt.Columns.Add("SUMATORIA IMPUESTO BOLSAS");
            dt.Columns.Add("MONTOENLETRAS");
            dt.Columns.Add("ID PEDIDO");
            dt.Columns.Add("DOC ESTADO");

            foreach (var p in DataBoletaFactura)
            {
                dt.Rows.Add(new object[] { p.id_doc_electronico,p.serieNumero,
                    p.tipoDocumento,p.fechaEmision,p.numeroDocIdentidadEmisor,p.tipoDocIdentidadEmisor,p.numeroDocIdentidadReceptor,
                    p.razonSocialReceptor,p.direccionReceptor,p.correoReceptor,p.tipoDocIdentidadReceptor,p.telefono,p.totalOPGravadas,
                    p.totalOPExoneradas,p.totalOPNoGravadas,p.totalOPGratuitas,p.sumatoriaIGV,p.importeTotal,p.importeTotalVenta,
                    p.totalDescuentos,p.descuentosGlobales,p.sumatoriaOtrosCargos,p.porcentajeOtrosCargos,p.sumatoriaImpuestoBolsas,
                    p.montoEnLetras,p.idPedido,p.Doc_Estado});
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
        private void ExportarExcel2()
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            saveFileDialog1.FileName = "Reporte Documentos Electronicos" + DateTime.Now.ToString("yyyyMMddHHmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID DOC ELECTRONICO");
            dt.Columns.Add("SERIE NUMERO");
            dt.Columns.Add("TIPO DOCUMENTO");
            dt.Columns.Add("FECHA EMISION");
            dt.Columns.Add("NUMERO DOC IDENTIDAD EMISOR");
            dt.Columns.Add("TIPODOC IDENTIDAD EMISOR");
            dt.Columns.Add("NUMERO DOC IDENTIDAD RECEPTOR");
            dt.Columns.Add("RAZON SOCIAL RECEPTOR");
            dt.Columns.Add("DIRECCION RECEPTOR");
            dt.Columns.Add("CORREO RECEPTOR");
            dt.Columns.Add("TIPO DOC IDENTIDAD RECEPTOR");
            dt.Columns.Add("TELEFONO");
            dt.Columns.Add("TOTAL OP GRAVADAS");
            dt.Columns.Add("TOTAL OP EXONERADAS");
            dt.Columns.Add("TOTAL OP NOGRAVADAS");
            dt.Columns.Add("TOTAL OP GRATUITAS");
            dt.Columns.Add("SUMATORIA IGV");
            dt.Columns.Add("IMPORTE TOTAL");
            dt.Columns.Add("IMPORTE TOTAL VENTA");
            dt.Columns.Add("TOTAL DESCUENTOS");
            dt.Columns.Add("DESCUENTOS GLOBALES");
            dt.Columns.Add("SUMATORIA OTROS CARGOS");
            dt.Columns.Add("PORCENTAJE OTROS CARGOS");
            dt.Columns.Add("SUMATORIA IMPUESTO BOLSAS");
            dt.Columns.Add("MONTOENLETRAS");
            dt.Columns.Add("ID PEDIDO");
            dt.Columns.Add("DOC ESTADO");

            foreach (var p in DataBoletaFactura)
            {
                dt.Rows.Add(new object[] { p.id_doc_electronico,p.serieNumero,
                    p.tipoDocumento,p.fechaEmision,p.numeroDocIdentidadEmisor,p.tipoDocIdentidadEmisor,p.numeroDocIdentidadReceptor,
                    p.razonSocialReceptor,p.direccionReceptor,p.correoReceptor,p.tipoDocIdentidadReceptor,p.telefono,p.totalOPGravadas,
                    p.totalOPExoneradas,p.totalOPNoGravadas,p.totalOPGratuitas,p.sumatoriaIGV,p.importeTotal,p.importeTotalVenta,
                    p.totalDescuentos,p.descuentosGlobales,p.sumatoriaOtrosCargos,p.porcentajeOtrosCargos,p.sumatoriaImpuestoBolsas,
                    p.montoEnLetras,p.idPedido,p.Doc_Estado});
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
        private void getLista()
        {
            try
            {
                List<Ent_RptBoletayFactura> _bf = new List<Ent_RptBoletayFactura>();
                _bf = bfstructure.GetDocElectronicosxFiltro(DateTime.Now, DateTime.Now, "0", 1001);
                ObservableCollection<Ent_RptBoletayFactura> bf = new ObservableCollection<Ent_RptBoletayFactura>(_bf);
                DataBoletaFactura = bf;
                this.Operacion = "Lista";

                List<Tipo_Doc_Electronico> _tp = new List<Tipo_Doc_Electronico>();
                _tp = negTipDocElec.GetTipDocElectronico();
                ObservableCollection<Tipo_Doc_Electronico> tp = new ObservableCollection<Tipo_Doc_Electronico>(_tp);
                this.TipDocElectr = tp;

                DataEstDocumento = bfstructure.GetEstadoDocumentos();

                if (DataBoletaFactura.Count() == 0)
                {
                    globales.Mensaje("SOS-FOOD - Información:", "No hay datos", 2);
                    this.CONSOLIDADO_OPGRAVADAS = 0;
                    this.CONSOLIDADO_IGV = 0;
                    this.CONSOLIDADO_RC = 0;
                    this.CONSOLIDADO_ICBPER = 0;
                    this.CONSOLIDADO_TOTAL = 0;
                    this.CantRegistrosDocElec = 0;
                    return;
                }
                //CONSOLIDADO
                //DataConsolidado = bfstructure.GetConsolidadoDocElectronicos(StartDate, FinishDate);
                this.CONSOLIDADO_OPGRAVADAS = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.totalOPGravadas);
                this.CONSOLIDADO_IGV = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.sumatoriaIGV);
                this.CONSOLIDADO_RC = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.sumatoriaOtrosCargos);
                this.CONSOLIDADO_ICBPER = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.sumatoriaImpuestoBolsas);
                this.CONSOLIDADO_TOTAL = DataBoletaFactura.Where(w => w.Doc_Estado != false).Sum(s => s.importeTotal);
                this.CantRegistrosDocElec = DataBoletaFactura.Count();
            }
            catch (Exception ex)
            {
                globales.Mensaje("SOS-FOOD - Error:", "Error al cargar los datos" + ex.Message, 3);
                return;
            }
        }
    }
}

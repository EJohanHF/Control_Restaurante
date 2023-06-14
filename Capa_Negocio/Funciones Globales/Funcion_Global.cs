using Capa_Datos.Funciones_Globales;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Font = iTextSharp.text.Font;
using System.Configuration;
using System.Net;
using Capa_Negocio.Parametros;

namespace Capa_Negocio.Funciones_Globales
{
   
    public class Funcion_Global
    {
        //Neg_Parametros negParametros = new Neg_Parametros();
        public bool isactive { get; set; }
        private string mensajesnak;
        private readonly NotificationManager _notificationManager = new NotificationManager();
        //private readonly INotificationManager _notificationWindow { get;}
        public string Title { get; set; }
        public string Message { get; set; }

        private readonly Random _random = new Random();
        #region SnackBar
        public bool IsActive
        {
            get => isactive;
            set
            {
                //this.usua = Pagar.idusuario;

                if (value == true)
                {
                    var ofSatckbar = (TimeSpan.FromMilliseconds(9000));
                    if (ofSatckbar == (TimeSpan.FromMilliseconds(9000)))
                    {
                        isactive = false;
                    }
                }
                isactive = value;
            }
        }
        public string mensajeSnack
        {
            get => mensajesnak;
            set
            {
                mensajesnak = value;
            }
        }

        public void ocultarSnackBar(TimeSpan tiempo)
        {
            IsActive = false;
        }
        #endregion
        Func_Globales global = new Func_Globales();
        public string ImpCaja()
        {
            string impresora = "";
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            string caja2 = ConfigurationManager.AppSettings["caja2"].ToString();
            if (Modulo == "1" && caja2 == "NO")
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = global.ImpCaja();
                impresora = dt.Rows[0][2].ToString();
            }
            else if (Modulo == "1" && caja2 == "SI")
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = global.ImpCaja();
                impresora = dt.Rows[0][6].ToString();
            }
            else
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = global.ImpCaja();
                impresora = dt.Rows[0][5].ToString();
            }
                
           
            return impresora;
        }
        public string ImpCajaGlobal()
        {
            string impresora = "";
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = global.ImpCaja();
                impresora = dt.Rows[0][2].ToString();
            return impresora;
        }
        public void DesbloquearMesas()
        {
            DataTable dt = new DataTable();
            dt = global.DesbloquearMesas();
            if (dt ==null)
            {
                //Mensaje("SOS-FOOD - Error","Error al desbloquear mesas", 3);
            }
        }
        public void BloquearMesaWeb(string mesa)
        {
            DataTable dt = new DataTable();
            dt = global.BloquearMesaWeb(mesa);
            if (dt == null)
            {
                //Mensaje("SOS-FOOD - Error", "Error al bloquear mesas", 3);
            }
        }
        public void DesBloquearMesaWeb(string mesa)
        {
            DataTable dt = new DataTable();
            dt = global.DesbloquearMesaWeb(mesa);
            if (dt == null)
            {
                //Mensaje("SOS-FOOD - Error", "Error al desbloquear mesas", 3);
            }
        }
        public bool EstMesaWeb()
        {
            bool est;
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = global.EstMesaWeb();
            est = Convert.ToBoolean(dt.Rows[0][0].ToString());
            return est;
        }
        public string MesaWeb()
        {
            string mesa;
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = global.EstMesaWeb();
            mesa = dt.Rows[0][1].ToString();
            return mesa;
        }
        public void MensajeSnack(string mensaje)
        {
            contador = 0;
            IsActive = true;
            mensajeSnack = mensaje;

            Timer timer = new Timer(5000);
            timer.AutoReset = true;

            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_elapsed);

            timer.Start();
        }
        public bool ValidarInternet()
        {
            bool internet = false;
            try
            {
                //System.Uri Url = new System.Uri("http://www.google.com/");
                //System.Net.WebRequest WebRequest;
                //WebRequest = System.Net.WebRequest.Create(Url);
                //WebRequest.Timeout = 5000;
                //System.Net.WebResponse objetoResp;


                //objetoResp = WebRequest.GetResponse();
                //objetoResp.Close();
                internet = true;
            }
            catch (Exception e)
            {
                //Estado = "No se pudo conectar a Internet " + e.Message;
                //Estado = "Sin conexion a internet \n Prueba a: \n " +
                //    "• Comprobar los cables de red, el módem y el router \n" +
                //    "• Volver a conectarte a una red Wi-Fi \n"+ e.Message;

                //Estado = "Por el momento no cuenta con internet. \n Por favor ingrese manualmente los datos de su cliente.";
                internet = false;
                
            }
            return internet;
        }
        public void Mensaje(string titulo,string mensaje,int tipo)
        {
            try
            {
                //Tipos
                // 1 => Correcto
                // 2 => Advertencia
                // 3 => Error
                // 4 => Neutral
                //titulo = negParametros.getNombreSistema();
                string sistema = "";
                DataTable dt = new DataTable();
                dt = global.SelectParametros();
                sistema = dt.Rows[30][2].ToString();
                titulo = sistema;
                if (tipo == 1)
                {
                    titulo = titulo + " - ";
                }
                var content = new NotificationContent
                {
                    Title = titulo,
                    Message = mensaje,
                    Type = (NotificationType)tipo
                };
                _notificationManager.Show(content);
            }
            catch (Exception ex)
            {
            }
        }
        public string PublicidadPreCuenta() {
            string mensaje = "";
            try
            {
                

                DataTable dt = new DataTable();
                dt = global.SelectParametros();
                mensaje = dt.Rows[31][2].ToString();
                return mensaje;
            }
            catch (Exception ex)
            {
                return mensaje;
            }
        }
        public string PublicidadFacturacion()
        {
            string mensaje = "";
            try
            {
                DataTable dt = new DataTable();
                dt = global.SelectParametros();
                mensaje = dt.Rows[32][2].ToString();
                return mensaje;
            }
            catch (Exception ex)
            {
                return mensaje;
            }
        }
        public static int contador = 0;
        private void timer_elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (contador == 1)
            {
                IsActive = false;
            }
            if (contador == 0)
            {
                contador += 1;
            }
        }
        #region Metodo exportar PDF
        public void ExportDataTableToPdf(DataTable dt, String Ubicacion, string Titulo)
        {
            System.IO.FileStream fs = new FileStream(Ubicacion, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document(iTextSharp.text.PageSize.A4.Rotate(), 05, 05, 05, 05);
            //Document document = new Document(PageSize.A4, 27, 27, 30, 30);
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            //document.SetPageSize(iTextSharp.text.PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            //Report Header
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntHead = new Font(bfntHead, 16, 1, new BaseColor(2, 80, 125));
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk(Titulo.ToUpper(), fntHead));
            document.Add(prgHeading);

            //Author
            Paragraph prgAuthor = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntAuthor = new Font(btnAuthor, 8, 2, BaseColor.GRAY);
            prgAuthor.Alignment = Element.ALIGN_RIGHT;
            prgAuthor.Add(new Chunk("SOS-FOOD", fntAuthor));
            prgAuthor.Add(new Chunk("\n Fecha : " + DateTime.Now.ToShortDateString(), fntAuthor));
            document.Add(prgAuthor);

            //Add a line seperation
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F,new  BaseColor(2, 80, 125), Element.ALIGN_LEFT, 1)));
            document.Add(p);

            //Add line break
            document.Add(new Chunk("\n", fntHead));

            //Write the table
            PdfPTable table = new PdfPTable(dt.Columns.Count);
            table.TotalWidth = 820f;
            table.LockedWidth = true;
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            float[] widths;
            if (dt.Columns.Count==14)
            {
                 widths = new float[] { 100f, 180f, 210f, 120f, 120f, 120f, 200f, 200f, 300f, 250f, 180f, 250f, 100f, 100f };
                table.SetWidths(widths);
            }
            else if (dt.Columns.Count==12)
            {
                 widths = new float[] { 100f, 180f, 220f, 130f, 130f, 220f, 200f, 200f, 250f, 250f, 200f, 120f };
                table.SetWidths(widths);
            }
            else if (dt.Columns.Count == 9)
            {
                widths = new float[] { 100f, 150f, 200f, 120f, 350f, 120f, 250f, 200f, 250f };
                table.SetWidths(widths);
            }
            else if (dt.Columns.Count == 8)
            {
                widths = new float[] { 100f, 150f, 200f, 120f, 350f, 120f, 250f, 200f };
                table.SetWidths(widths);
            }
            else if (dt.Columns.Count == 7)
            {
                widths = new float[] { 70f, 150f, 100f, 250f, 70f, 150f, 150f }; 
                table.SetWidths(widths);
            }
            table.HorizontalAlignment = Element.ALIGN_CENTER;

            //Table header
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 11, 1, BaseColor.WHITE);
            

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = new BaseColor(3, 91, 141);
                cell.AddElement(new Chunk(dt.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);
            }
            //table Data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    table.AddCell(dt.Rows[i][j].ToString());
                    
                }
            }

            document.Add(table);
            document.Close();
            writer.Close();
            fs.Close();
        }
        #endregion
        #region Metodo Excel Multiples tablas en un ahoja
        public void DataSetsToExcel(List<DataSet> dataSets, string fileName)
        {
            Microsoft.Office.Interop.Excel.Application xlApp =
                      new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkbook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Sheets xlSheets = null;
            Worksheet xlWorksheet = null;

            foreach (DataSet dataSet in dataSets)
            {
                System.Data.DataTable dataTable = dataSet.Tables[0];
                int rowNo = dataTable.Rows.Count;
                int columnNo = dataTable.Columns.Count;
                int colIndex = 0;

                //Create Excel Sheets
                xlSheets = xlWorkbook.Sheets;
                xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                               Type.Missing, Type.Missing, Type.Missing);
                xlWorksheet.Name = dataSet.DataSetName;

                //Generate Field Names
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    colIndex++;
                    xlApp.Cells[1, colIndex] = dataColumn.ColumnName;
                }

                object[,] objData = new object[rowNo, columnNo];

                //Convert DataSet to Cell Data
                for (int row = 0; row < rowNo; row++)
                {
                    for (int col = 0; col < columnNo; col++)
                    {
                        objData[row, col] = dataTable.Rows[row][col];
                    }
                }

                //Add the Data
                Range range = xlWorksheet.Range[xlApp.Cells[2, 1], xlApp.Cells[rowNo + 1, columnNo]];
                range.Value2 = objData;

                //Format Data Type of Columns 
                colIndex = 0;
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    colIndex++;
                    string format = "@";
                    switch (dataColumn.DataType.Name)
                    {
                        case "Boolean":
                            break;
                        case "Byte":
                            break;
                        case "Char":
                            break;
                        case "DateTime":
                            format = "dd/mm/yyyy";
                            break;
                        case "Decimal":
                            format = "$* #,##0.00;[Red]-$* #,##0.00";
                            break;
                        case "Double":
                            break;
                        case "Int16":
                            format = "0";
                            break;
                        case "Int32":
                            format = "0";
                            break;
                        case "Int64":
                            format = "0";
                            break;
                        case "SByte":
                            break;
                        case "Single":
                            break;
                        case "TimeSpan":
                            break;
                        case "UInt16":
                            break;
                        case "UInt32":
                            break;
                        case "UInt64":
                            break;
                        default: //String
                            break;
                    }
                    //Format the Column according to Data Type
                    xlWorksheet.Range[xlApp.Cells[2, colIndex],
                          xlApp.Cells[rowNo + 1, colIndex]].NumberFormat = format;
                }
            }

    //Remove the Default Worksheet
    ((Worksheet)xlApp.ActiveWorkbook.Sheets[xlApp.ActiveWorkbook.Sheets.Count]).Delete();

            //Save
            xlWorkbook.SaveAs(fileName,
                System.Reflection.Missing.Value,
                System.Reflection.Missing.Value,
                System.Reflection.Missing.Value,
                System.Reflection.Missing.Value,
                System.Reflection.Missing.Value,
                XlSaveAsAccessMode.xlNoChange,
                System.Reflection.Missing.Value,
                System.Reflection.Missing.Value,
                System.Reflection.Missing.Value,
                System.Reflection.Missing.Value,
                System.Reflection.Missing.Value);

            xlWorkbook.Close();
            xlApp.Quit();
            GC.Collect();
        }
        #endregion
    }
}

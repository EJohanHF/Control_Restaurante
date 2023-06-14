using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;


namespace tickets
{
    public class Ticket
    {
        private ArrayList headerLines = new ArrayList();
        private ArrayList headerLines_2 = new ArrayList();

        private ArrayList titleLines = new ArrayList();

        private ArrayList titleLines2 = new ArrayList();

        private ArrayList subHeaderLines = new ArrayList();
        private ArrayList subHeaderLinesMesaGrande = new ArrayList();
        private ArrayList subHeaderLinesTelefono = new ArrayList();
        private ArrayList subHeaderLinesEnorme = new ArrayList();
        private ArrayList subHeaderLinesEnormeComanda = new ArrayList();
        private ArrayList subHeaderLinesPuertoEncantado = new ArrayList();
        private ArrayList subHeaderLinesPuertoEncantadoBold = new ArrayList();

        private ArrayList subHeaderLinesBold = new ArrayList();

        private ArrayList items = new ArrayList();
        private ArrayList itemsPuertoEncantado = new ArrayList();
        private ArrayList items2 = new ArrayList();
        private ArrayList items3 = new ArrayList();
        private ArrayList items4 = new ArrayList();

        private ArrayList itemsReporte = new ArrayList();

        private ArrayList itemsReporteVentas = new ArrayList();
        private ArrayList itemsReportePlatos = new ArrayList();

        private ArrayList itemsReporteDetallePago = new ArrayList();

        private ArrayList itemsDocElectronicos = new ArrayList();

        private ArrayList itemsDocElectronicosTotal = new ArrayList();

        private ArrayList itemsDocElectronicosAnulados = new ArrayList();

        private ArrayList itemsDocElectronicosAnuladosTotal = new ArrayList();

        private ArrayList itemsProductosAnulados = new ArrayList();

        private ArrayList itemsComanda = new ArrayList();
        private ArrayList itemsComandaRecutacu = new ArrayList();

        private ArrayList itemsAnulaciones = new ArrayList();

        private ArrayList totales = new ArrayList();
        private ArrayList totalesPuertoEncantado = new ArrayList();

        private ArrayList footerLines = new ArrayList();
        private ArrayList footerLinesPublidad = new ArrayList();

        private ArrayList footer2Lines = new ArrayList();
        private ArrayList footer2LinesAnulacion = new ArrayList();
        private ArrayList DrawFooter2_Mesa_lines = new ArrayList();

        private Image headerImage;

        private Image qrImage;

        private int count;

        private int maxChar = 35;

        private int margenLogo = 22;

        private int margenQR = 22;

        private int maxCharDescription = 20;
        //public int maxCharDescription = 16; // recutacu - salazar - rancho de robertin

        private int imageHeight;

        private int maxCharDescrip = 32;

        private float leftMargin;

        private float topMargin = 3f;

        private string fontName = "Lucida Console";

        private int fontSize = 9;

        private Font printFont;

        private SolidBrush myBrush = new SolidBrush(Color.Black);

        private Graphics gfx;

        private string line;

        private int indexItem = 0;

        public Image HeaderImage
        {
            get
            {
                return this.headerImage;
            }
            set
            {
                if (this.headerImage != value)
                {
                    this.headerImage = value;
                }
            }
        }

        public Image QrImage
        {
            get
            {
                return this.qrImage;
            }
            set
            {
                if (this.qrImage != value)
                {
                    this.qrImage = value;
                }
            }
        }

        public int MaxChar
        {
            get
            {
                return this.maxChar;
            }
            set
            {
                if (value != this.maxChar)
                {
                    this.maxChar = value;
                }
            }
        }

        public int MargenQR
        {
            get
            {
                return this.margenQR;
            }
            set
            {
                if (value != this.margenQR)
                {
                    this.margenQR = value;
                }
            }
        }

        public int MargenLogo
        {
            get
            {
                return this.margenLogo;
            }
            set
            {
                if (value != this.margenLogo)
                {
                    this.margenLogo = value;
                }
            }
        }

        public int MaxCharDescription
        {
            get
            {
                return this.maxCharDescription;
            }
            set
            {
                if (value != this.maxCharDescription)
                {
                    this.maxCharDescription = value;
                }
            }
        }
        public int MaxCharDescrip
        {
            get
            {
                return this.maxCharDescrip;
            }
            set
            {
                if (value != this.MaxCharDescrip)
                {
                    this.maxCharDescrip = value;
                }
            }
        }

        public int FontSize
        {
            get
            {
                return this.fontSize;
            }
            set
            {
                if (value != this.fontSize)
                {
                    this.fontSize = value;
                }
            }
        }

        public string FontName
        {
            get
            {
                return this.fontName;
            }
            set
            {
                if (value != this.fontName)
                {
                    this.fontName = value;
                }
            }
        }

        public void AddHeaderLine(string line)
        {
            this.headerLines.Add(line);
        }

        public void AddHeaderLine_2(string line)
        {
            this.headerLines_2.Add(line);
        }

        public void AddTitleLine(string line)
        {
            this.titleLines.Add(line);
        }
        public void AddTitleLine2(string line)
        {
            this.titleLines2.Add(line);
        }
        public void AddSubHeaderLineMesaGrande(string line)
        {
            this.subHeaderLinesMesaGrande.Add(line);
        }
        public void AddSubHeaderLine(string line)
        {
            this.subHeaderLines.Add(line);
        }
        public void AddSubHeaderLineTelefono(string line)
        {
            this.subHeaderLinesTelefono.Add(line);
        }
        public void AddSubHeaderLineEnorme(string line)
        {
            this.subHeaderLinesEnorme.Add(line);
        }
        public void AddSubHeaderLineEnormeComanda(string line)
        {
            this.subHeaderLinesEnormeComanda.Add(line);
        }
        public void AddSubHeaderLinePuertoEncantado(string line)
        {
            this.subHeaderLinesPuertoEncantado.Add(line);
        }
        public void AddSubHeaderLinePuertoEncantadoBold(string line)
        {
            this.subHeaderLinesPuertoEncantadoBold.Add(line);
        }
        public void AddSubHeaderLineBold(string line)
        {
            this.subHeaderLinesBold.Add(line);
        }
        public void AddItemAnulaciones(string serienumero, string importetotal, string nombreusuario)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsAnulaciones.Add(newItem.GenerateItem(serienumero, importetotal, nombreusuario));
        }
        public void AddItem(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.items.Add(newItem.GenerateItem(cantidad, item, price));
        }
        public void AddItemPuertoEncantado(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsPuertoEncantado.Add(newItem.GenerateItem(cantidad, item, price));
        }
        public void AddLineSimpleRpt(string carta, string cantidad)
        {
            OrderItem newItem = new OrderItem('?');
            this.items.Add(newItem.GenerateItemRptPI(carta,cantidad));
        }
        public void AddReporteItem(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsReporte.Add(newItem.GenerateItem(cantidad, item, price));
        }
        public void AddReporteItemventas(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsReporteVentas.Add(newItem.GenerateItem(cantidad, item, price));
        }
        public void AddReporteItemPlatos(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsReportePlatos.Add(newItem.GenerateItem(cantidad, item, price));
        }
        public void AddReporteItemDetallePagos(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsReporteDetallePago.Add(newItem.GenerateItem(cantidad, item, price));
        }
        public void AddDocElectronico(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsDocElectronicos.Add(newItem.GenerateItem(cantidad, item, price));
        }
        public void AddDocElectronicoTotal(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsDocElectronicosTotal.Add(newItem.GenerateItem(cantidad, item, price));
        }
        public void AddDocElectronicoAnulados(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsDocElectronicosAnulados.Add(newItem.GenerateItem(cantidad, item, price));
        }
        public void AddDocElectronicoAnuladosTotal(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsDocElectronicosAnuladosTotal.Add(newItem.GenerateItem(cantidad, item, price));
        }
        public void AddProductosAnulados(string cantidad, string descripcion, string precio)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsProductosAnulados.Add(newItem.GenerateItem(cantidad, descripcion, precio));
        }
        public void AddItemSinRecorte(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.items2.Add(newItem.GenerateItemSinCorte(cantidad, item, price));
        }
        public void AddItemSinRecorte2(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.items3.Add(newItem.GenerateItemSinCorte(cantidad, item, price));
        }
        public void AddItemSinRecorte3(string cantidad, string item, string price)
        {
            OrderItem newItem = new OrderItem('?');
            this.items4.Add(newItem.GenerateItemSinCorte(cantidad, item, price));
        }
        public void AddItemComanda(string cantidad, string item)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsComanda.Add(newItem.GenerateItem(cantidad, item,""));
        }
        public void AddItemComandaRecutacu(string cantidad, string item)
        {
            OrderItem newItem = new OrderItem('?');
            this.itemsComandaRecutacu.Add(newItem.GenerateItem(cantidad, item,""));
        }

        public void AddTotal(string name, string price)
        {
            OrderTotal newTotal = new OrderTotal('?');
            this.totales.Add(newTotal.GenerateTotal(name, price));
        }
        public void AddTotalPuertoEncantado(string name, string price)
        {
            OrderTotal newTotal = new OrderTotal('?');
            this.totalesPuertoEncantado.Add(newTotal.GenerateTotal(name, price));
        }

        public void AddFooterLinePub(string line)
        {
            this.footerLinesPublidad.Add(line);
        }
        public void AddFooterLine(string line)
        {
            this.footerLines.Add(line);
        }

        public void AddFooter2Line(string line)
        {
            this.footer2Lines.Add(line);
        }
        public void AddFooter2LineAnulacion(string line)
        {
            this.footer2LinesAnulacion.Add(line);
        }
        public void DrawFooter2_Mesa(string line)
        {
            this.DrawFooter2_Mesa_lines.Add(line);
        }

        private string AlignRightText(int lenght)
        {
            string espacios = "";
            int spaces = this.maxChar - lenght;
            for (int x = 0; x < spaces; x++)
            {
                espacios += " ";
            }
            return espacios;
        }
        private string AlignCenterText(int lenght)
        {
            string espacios = "";
            int spaces = (this.maxChar - lenght)/2;
            for (int x = 0; x < spaces; x++)
            {
                espacios += " ";
            }
            return espacios;
        }

        private string DottedLine()
        {
            string dotted = "";
            for (int x = 0; x < this.maxChar; x++)
            {
                dotted += "-";
            }
            return dotted;
        }

        public bool PrinterExists(string impresora)
        {
            foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            {
                if (impresora == strPrinter)
                {
                    return true;
                }
            }
            return false;
        }

        public void PrintTicket(string impresora)
        {
            try
            {
                this.printFont = new Font(this.fontName, (float)this.fontSize, FontStyle.Regular);
                PrintDocument pr = new PrintDocument();
                pr.PrinterSettings.PrinterName = impresora;
                pr.PrintPage += new PrintPageEventHandler(this.pr_PrintPage);
                pr.Print();
            }
            catch (Exception ex)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Error", "Error al imprimir : " + ex.Message, 3);
            }
            
        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public bool PrintTicket2(string impresora)
        {
            try
            {
                this.printFont = new Font(this.fontName, (float)this.fontSize, FontStyle.Regular);
                PrintDocument pr = new PrintDocument();
                pr.PrinterSettings.PrinterName = impresora;
                pr.PrintPage += new PrintPageEventHandler(this.pr_PrintPage);
                pr.Print();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void pr_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            this.gfx = e.Graphics;
            this.DrawImage();
            if (headerLines_2.Count > 0)
            {
                this.DrawHeader_2();
            }
            this.DrawHeader();
            this.DrawTitle();         
            if (headerLines.Count > 0)
            {
                this.DrawSubHeaderBold();
            }
            //DrawSubHeaderEnorme(); // pez y la parrilla
            //this.DrawSubHeader();
            this.DrawSubHeaderMesaGrande();
            this.DrawSubHeader();
            this.DrawSubHeaderPuertoEncantado();
            this.DrawSubHeaderPuertoEncantadoBold();
            if (items.Count > 0)
            {
                this.DrawItems();
            }
            if (itemsPuertoEncantado.Count > 0)
            {
                this.DrawItemsPuertoEncantado();
            }
            if (items2.Count > 0)
            {
                this.DrawItems2();
            }
            if (items3.Count > 0)
            {
                this.DrawItems3();
            }
            if (items4.Count > 0)
            {
                this.DrawItems4();
            }
            if (itemsComanda.Count>0)
            {
                this.DrawItemsComanda();
            }
            if (itemsComandaRecutacu.Count>0)
            {
                this.DrawItemsComandaRecutacu();
            }
            if (itemsAnulaciones.Count > 0)
            {
                this.DrawItemsAnulaciones();
            }
            if (itemsReporte.Count > 0)
            {
                this.DrawItemsReporte();
            }
            if (itemsReporteVentas.Count > 0)
            {
                this.DrawItemsReporteVentas();
            }
            if (itemsReporteDetallePago.Count > 0)
            {
                this.DrawItemsReporteDetallePagos();
            }
            if (itemsDocElectronicos.Count > 0)
            {
                this.DrawItemsDocElectronicos();
            }
            if (itemsDocElectronicosTotal.Count > 0)
            {
                this.DrawItemsDocElectronicosTotal();
            }
            this.DrawTitle2();
            if (itemsDocElectronicosAnulados.Count > 0)
            {
                this.DrawItemsDocElectronicosÀnulados();
            }
            if (itemsDocElectronicosAnuladosTotal.Count > 0)
            {
                this.DrawItemsDocElectronicosÀnuladosTotal();
            }
            if (itemsProductosAnulados.Count > 0)
            {
                this.DrawCantidadDescImporte();
            }
            if (itemsReportePlatos.Count > 0)
            {
                this.DrawPlatos();
            }
            if (totales.Count > 0) {
                this.DrawTotales();
            }
            if (totalesPuertoEncantado.Count > 0) {
                this.DrawTotalesPuertoEncantado();
            }
           
            this.DrawQrImage();
            if (footerLinesPublidad.Count > 0)
            {
                //this.DrawFooterPub();
            }
            if (footerLines.Count > 0)
            {
                this.DrawFooter();
            }
            if (footer2Lines.Count > 0)
            {
                this.DrawFooter2();
            }
            if (footer2LinesAnulacion.Count > 0)
            {
                this.DrawFooterAnulacion();
            }
            if (DrawFooter2_Mesa_lines.Count > 0)
            {
                this.DrawFooter2_Mesa();
            }
            this.DrawSubHeaderEnormeComanda();
            if (this.headerImage != null)
            {
                this.HeaderImage.Dispose();
                this.headerImage.Dispose();
            }
            if (this.qrImage != null)
            {
                this.QrImage.Dispose();
                this.QrImage.Dispose();
            }
        }

        private float YPosition()
        {
            return this.topMargin + ((float)this.count * this.printFont.GetHeight(this.gfx) + (float)this.imageHeight);
        }

        private float YPosition_comanda()
        {
            return this.topMargin + (float)1.5 + ((float)this.count * this.printFont.GetHeight(this.gfx) + (float)this.imageHeight);
        }

        Image YenidenBoyutlandir(Image resim)// resizing image method 

        {
            Image yeniResim = new Bitmap(150, 156);
            using (Graphics abc = Graphics.FromImage((Bitmap)yeniResim))
            {
                abc.DrawImage(resim, new System.Drawing.Rectangle(0, 0, 150, 156));
                //abc.DrawImage(resim, new System.Drawing.Rectangle(0, 0, 120, 126));
            }
            return yeniResim;
        }
        private void DrawImage()
        {
            if (this.headerImage != null)
            {
                try
                {
                    this.gfx.DrawImage(YenidenBoyutlandir(this.headerImage), new Point((int)this.leftMargin + margenLogo, (int)this.YPosition()));
                    //this.gfx.DrawImage(this.headerImage, new Point((int)this.maxChar, (int)this.YPosition()));
                    double height = (double)YenidenBoyutlandir(this.headerImage).Height / 58.0 * 15.0;
                    this.imageHeight = (int)Math.Round(height) + 3;
                }
                catch (Exception)
                {
                }
            }
        }
        private void DrawQrImage()
        {
            if (this.qrImage != null)
            {
                try
                {
                    this.gfx.DrawImage(this.qrImage, new Point((int)this.leftMargin + margenQR, (int)this.YPosition()));
                    double height = (double)this.qrImage.Height / 58.0 * 15.0;
                    this.imageHeight += (int)Math.Round(height) + 3;
                }
                catch (Exception)
                {
                }
            }
        }
        private void DrawHeader()
        {
            Font printFont = new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold);


            foreach (string header in this.headerLines)
            {
                if (header.Length > this.maxChar)
                {
                    int currentChar = 0;
                    for (int headerLenght = header.Length; headerLenght > this.maxChar; headerLenght -= this.maxChar)
                    {
                        this.line = AlignCenterText(header.Substring(currentChar, this.maxChar).Length) + header.Substring(currentChar, this.maxChar);
                        this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxChar;
                    }
                    this.line = AlignCenterText(header.Length) + header;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = AlignCenterText(header.Length) + header;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
            }
           // this.DrawEspacio();
        }

        private void DrawHeader_2()
        {
            Font printFont = new Font(this.fontName, (float)(this.fontSize + 3), FontStyle.Bold);


            foreach (string header in this.headerLines_2)
            {
                if (header.Length > this.maxChar - 5)
                {
                    int currentChar = 0;
                    for (int headerLenght = header.Length; headerLenght > this.maxChar - 5; headerLenght -= this.maxChar -5)
                    {
                        this.line =  header.Substring(currentChar, this.maxChar - 5);
                        this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxChar - 5;
                    }
                    this.line = header;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line =  header;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
            }
            // this.DrawEspacio();
        }

        private void DrawTitle()
        {   
            Font printFont = new Font(this.fontName, (float)(this.fontSize ), FontStyle.Regular);


            foreach (string title in this.titleLines)
            {
                if (title.Length > this.maxChar)
                {
                    int currentChar = 0;
                    for (int headerLenght = title.Length; headerLenght > this.maxChar; headerLenght -= this.maxChar)
                    {
                        this.line = AlignCenterText(title.Substring(currentChar, this.maxChar).Length) + title.Substring(currentChar, this.maxChar);
                        this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxChar;
                    }
                    this.line = AlignCenterText(title.Length) + title;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = AlignCenterText(title.Length) + title;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
            }
            // this.DrawEspacio();
        }

        private void DrawTitle2()
        {
            Font printFont = new Font(this.fontName, (float)(this.fontSize), FontStyle.Regular);


            foreach (string title in this.titleLines2)
            {
                if (title.Length > this.maxChar)
                {
                    int currentChar = 0;
                    for (int headerLenght = title.Length; headerLenght > this.maxChar; headerLenght -= this.maxChar)
                    {
                        this.line = AlignCenterText(title.Substring(currentChar, this.maxChar).Length) + title.Substring(currentChar, this.maxChar);
                        this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxChar;
                    }
                    this.line = AlignCenterText(title.Length) + title;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = AlignCenterText(title.Length) + title;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
            }
            // this.DrawEspacio();
        }

        private void DrawSubHeaderMesaGrande()
        {
            Font printFont = new Font(this.fontName, (float)(this.fontSize + 7), FontStyle.Bold);
            //maxCharDescrip = 50; //puerto encantado
            //Font printFont = new Font(this.fontName, (float)(this.fontSize - 2), FontStyle.Regular); //puerto encantado
            foreach (string subHeader in this.subHeaderLinesMesaGrande)
            {
                if (subHeader.Length > this.maxCharDescrip)
                {
                    int currentChar = 0;
                    for (int subHeaderLenght = subHeader.Length; subHeaderLenght > this.maxCharDescrip; subHeaderLenght -= this.maxCharDescrip)
                    {
                        this.line = subHeader;
                        this.gfx.DrawString(this.line.Substring(currentChar, this.maxCharDescrip), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescrip;
                    }
                    this.line = subHeader;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = subHeader;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;                                        
                }
            }
        }
        private void DrawSubHeader()
        {
            Font printFont = new Font(this.fontName, (float)(this.fontSize + 1.5), FontStyle.Regular);
            //maxCharDescrip = 50; //puerto encantado
            //Font printFont = new Font(this.fontName, (float)(this.fontSize - 2), FontStyle.Regular); //puerto encantado
            foreach (string subHeader in this.subHeaderLines)
            {
                if (subHeader.Length > this.maxCharDescrip)
                {
                    int currentChar = 0;
                    for (int subHeaderLenght = subHeader.Length; subHeaderLenght > this.maxCharDescrip; subHeaderLenght -= this.maxCharDescrip)
                    {
                        this.line = subHeader;
                        this.gfx.DrawString(this.line.Substring(currentChar, this.maxCharDescrip), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescrip;
                    }
                    this.line = subHeader;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = subHeader;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;                                        
                }
            }

            Font printFont2 = new Font(this.fontName, (float)(this.fontSize + 2), FontStyle.Bold);
            foreach (string subHeader in this.subHeaderLinesTelefono)
            {
                if (subHeader.Length > this.maxCharDescrip)
                {
                    int currentChar = 0;
                    for (int subHeaderLenght = subHeader.Length; subHeaderLenght > this.maxCharDescrip; subHeaderLenght -= this.maxCharDescrip)
                    {
                        this.line = subHeader;
                        this.gfx.DrawString(this.line.Substring(currentChar, this.maxCharDescrip), printFont2, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescrip;
                    }
                    this.line = subHeader;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont2, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = subHeader;
                    this.gfx.DrawString(this.line, printFont2, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;                                        
                }
            }
            //this.DrawEspacio();
        }
        private void DrawSubHeaderEnorme()
        {
            this.fontName = "Arial Black";
            Font printFont = new Font(this.fontName, (float)(this.fontSize + 15), FontStyle.Bold);
            //maxCharDescrip = 50; //puerto encantado
            //Font printFont = new Font(this.fontName, (float)(this.fontSize - 2), FontStyle.Regular); //puerto encantado
            foreach (string subHeader in this.subHeaderLinesEnorme)
            {
                if (subHeader.Length > this.maxCharDescrip)
                {
                    int currentChar = 0;
                    for (int subHeaderLenght = subHeader.Length; subHeaderLenght > this.maxCharDescrip; subHeaderLenght -= this.maxCharDescrip)
                    {
                        this.line = subHeader;
                        this.gfx.DrawString(this.line.Substring(currentChar, this.maxCharDescrip), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescrip;
                    }
                    this.line = subHeader;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = subHeader;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;                                        
                }
            }
            this.DrawEspacio();
            this.DrawEspacio();
            this.DrawEspacio();
            this.fontName = "Lucida Console";
        }
        private void DrawSubHeaderEnormeComanda()
        {
            this.fontName = "Arial Black";
            Font printFont = new Font(this.fontName, (float)(this.fontSize + 20), FontStyle.Bold);
            //maxCharDescrip = 50; //puerto encantado
            //Font printFont = new Font(this.fontName, (float)(this.fontSize - 2), FontStyle.Regular); //puerto encantado
            foreach (string subHeader in this.subHeaderLinesEnormeComanda)
            {
                if (subHeader.Length > this.maxCharDescrip)
                {
                    int currentChar = 0;
                    for (int subHeaderLenght = subHeader.Length; subHeaderLenght > this.maxCharDescrip; subHeaderLenght -= this.maxCharDescrip)
                    {
                        this.line = subHeader;
                        this.gfx.DrawString(this.line.Substring(currentChar, this.maxCharDescrip), printFont, this.myBrush, this.leftMargin + 15, this.YPosition() - 13, new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescrip;
                    }
                    this.line = subHeader;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin + 15, this.YPosition() - 13, new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = subHeader;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin + 15, this.YPosition() - 13, new StringFormat());
                    this.count++;                                        
                }
            }
            this.DrawEspacio();
            this.DrawEspacio();
            this.DrawEspacio();
            this.fontName = "Lucida Console";
    }
        
        private void DrawSubHeaderPuertoEncantado()
        {
            maxCharDescrip = 50;
            Font printFont = new Font(this.fontName, (float)(this.fontSize), FontStyle.Regular);
            foreach (string subHeader in this.subHeaderLinesPuertoEncantado)
            {
                if (subHeader.Length > this.maxCharDescrip)
                {
                    int currentChar = 0;
                    for (int subHeaderLenght = subHeader.Length; subHeaderLenght > this.maxCharDescrip; subHeaderLenght -= this.maxCharDescrip)
                    {
                        this.line = subHeader;
                        this.gfx.DrawString(this.line.Substring(currentChar, this.maxCharDescrip), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescrip;
                    }
                    this.line = subHeader;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = subHeader;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;                                        
                }
            }
            //this.DrawEspacio();
        }
        private void DrawSubHeaderPuertoEncantadoBold()
        {
            maxCharDescrip = 50;
            Font printFont = new Font(this.fontName, (float)(this.fontSize), FontStyle.Bold);
            foreach (string subHeader in this.subHeaderLinesPuertoEncantadoBold)
            {
                if (subHeader.Length > this.maxCharDescrip)
                {
                    int currentChar = 0;
                    for (int subHeaderLenght = subHeader.Length; subHeaderLenght > this.maxCharDescrip; subHeaderLenght -= this.maxCharDescrip)
                    {
                        this.line = subHeader;
                        this.gfx.DrawString(this.line.Substring(currentChar, this.maxCharDescrip), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescrip;
                    }
                    this.line = subHeader;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = subHeader;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;                                        
                }
            }
            //this.DrawEspacio();
        }

        private void DrawSubHeaderBold()
        {
            Font printFont = new Font(this.fontName, (float)(this.fontSize -1), FontStyle.Bold);
            foreach (string subHeader in this.subHeaderLinesBold)
            {
                if (subHeader.Length > this.maxCharDescrip)
                {
                    int currentChar = 0;
                    for (int subHeaderLenght = subHeader.Length; subHeaderLenght > this.maxCharDescrip; subHeaderLenght -= this.maxCharDescrip)
                    {
                        this.line = subHeader;
                        this.gfx.DrawString(this.line.Substring(currentChar, this.maxCharDescrip), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescrip;
                    }
                    this.line = subHeader;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = subHeader;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
            }
        }
        private void DrawItems()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.gfx.DrawString("[CANT]DESCRIPCION      P.UN  IMPORTE", new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;

            // PARA MI TRADICION
            this.fontSize = 5;
            // PARA MI TRADICION

            //this.DrawEspacio();
            foreach (string item in this.items)
            {

                //this.line = ordIt.GetItemCantidad(item);
                //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPriceUni(item);
                this.line = this.AlignRightText(12) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemNameTicket(item);
                this.leftMargin = 0f;
                if (name.Length > this.maxCharDescription)
                {
                    int currentChar = 0;
                    for (int itemLenght = name.Length; itemLenght > this.maxCharDescription; itemLenght -= this.maxCharDescription)
                    {
                        this.line = ordIt.GetItemNameTicket(item);
                        this.gfx.DrawString("" + this.line.Substring(currentChar, this.maxCharDescription), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescription;
                    }
                    this.line = ordIt.GetItemNameTicket(item);
                    this.gfx.DrawString("" + this.line.Substring(currentChar, this.line.Length - currentChar), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.gfx.DrawString("" + ordIt.GetItemNameTicket(item), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }

            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;

            // PARA MI TRADICION
            this.fontSize = 9;
            // PARA MI TRADICION

            //this.DrawEspacio();
        }
        private void DrawItemsPuertoEncantado()
        {
            
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.gfx.DrawString("[CANT]DESCRIPCION             P.UN  IMPORTE", new Font(this.fontName, (float)(this.fontSize - 2), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;

            // PARA MI TRADICION
            this.fontSize = 5;
            // PARA MI TRADICION

            //this.DrawEspacio();
           // this.printFont = new Font(this.fontName, (float)this.fontSize - 2, FontStyle.Regular);
            foreach (string item in this.itemsPuertoEncantado)
            {

                //this.line = ordIt.GetItemCantidad(item);
                //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPriceUni(item);
                this.line = this.AlignRightText(9) + this.line;
                this.gfx.DrawString(this.line, new Font(this.fontName, (float)(this.fontSize + 3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length - 3) + this.line;
                this.gfx.DrawString(this.line, new Font(this.fontName, (float)(this.fontSize + 3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemNameTicket(item);
                this.leftMargin = 0f;
                this.maxCharDescription = 22;
                if (name.Length > this.maxCharDescription)
                {
                    int currentChar = 0;
                    for (int itemLenght = name.Length; itemLenght > this.maxCharDescription; itemLenght -= this.maxCharDescription)
                    {
                        this.line = ordIt.GetItemNameTicket(item);
                        this.gfx.DrawString("" + this.line.Substring(currentChar, this.maxCharDescription), new Font(this.fontName, (float)(this.fontSize + 3), FontStyle.Regular), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescription;
                    }
                    this.line = ordIt.GetItemNameTicket(item);
                    this.gfx.DrawString("" + this.line.Substring(currentChar, this.line.Length - currentChar), new Font(this.fontName, (float)(this.fontSize + 3), FontStyle.Regular), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.gfx.DrawString("" + ordIt.GetItemNameTicket(item), new Font(this.fontName, (float)(this.fontSize + 3), FontStyle.Regular), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }

            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, new Font(this.fontName, (float)(this.fontSize + 3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;

            // PARA MI TRADICION
            this.fontSize = 9;
            // PARA MI TRADICION

            //this.DrawEspacio();
        }
        private void DrawItems2()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.gfx.DrawString("[CANT]DESCRIPCION            IMPORTE", new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.fontSize = 9;
            //this.DrawEspacio();
            foreach (string item in this.items2)
            {
                //this.line = ordIt.GetItemCantidad(item);
                //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                //this.line = ordIt.GetItemPriceUni(item);
                //this.line = this.AlignRightText(13) + this.line;
                //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemNameTicket(item);
                this.leftMargin = 0f;
                if (name.Length > this.maxCharDescription)
                {
                    int currentChar = 0;
                    for (int itemLenght = name.Length; itemLenght > this.maxCharDescription; itemLenght -= this.maxCharDescription)
                    {
                        this.line = ordIt.GetItemNameTicket(item);
                        this.gfx.DrawString("" + this.line.Substring(currentChar, this.maxCharDescription), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescription;
                    }
                    this.line = ordIt.GetItemNameTicket(item);
                    this.gfx.DrawString("" + this.line.Substring(currentChar, this.line.Length - currentChar), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.gfx.DrawString("" + ordIt.GetItemNameTicket(item), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }

            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
        }
        private void DrawItems3()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.gfx.DrawString("CANT PLATO                  IMPORTE", new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.fontSize = 7;
            //this.DrawEspacio();
            foreach (string item in this.items3)
            {
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemNameTicket(item);
                this.leftMargin = 0f;
                if (name.Length > this.maxCharDescription + 8)
                {
                    int currentChar = 0;
                    for (int itemLenght = name.Length; itemLenght > this.maxCharDescription; itemLenght -= this.maxCharDescription)
                    {
                        this.line = ordIt.GetItemNameTicket(item);
                        this.gfx.DrawString("" + this.line.Substring(currentChar, this.maxCharDescription), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescription;
                    }
                    this.line = ordIt.GetItemNameTicket(item);
                    this.gfx.DrawString("" + this.line.Substring(currentChar, this.line.Length - currentChar), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.gfx.DrawString("" + ordIt.GetItemNameTicket(item), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }

            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
        }
        private void DrawItems4()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.gfx.DrawString("PRODUCTO                      CANTIDAD", new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.fontSize = 7;
            //this.DrawEspacio();
            foreach (string item in this.items4)
            {
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemNameTicket(item);
                this.leftMargin = 0f;
                if (name.Length > this.maxCharDescription + 8)
                {
                    int currentChar = 0;
                    for (int itemLenght = name.Length; itemLenght > this.maxCharDescription; itemLenght -= this.maxCharDescription)
                    {
                        this.line = ordIt.GetItemNameTicket(item);
                        this.gfx.DrawString("" + this.line.Substring(currentChar, this.maxCharDescription), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescription;
                    }
                    this.line = ordIt.GetItemNameTicket(item);
                    this.gfx.DrawString("" + this.line.Substring(currentChar, this.line.Length - currentChar), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.gfx.DrawString("" + ordIt.GetItemNameTicket(item), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }

            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
        }
        private void DrawItemsReporte()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            string titulo = "";
            foreach (string item in this.itemsReporte)
            {
                titulo = ordIt.GetItemName(item);
            }
                this.gfx.DrawString(titulo, new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
            foreach (string item in this.itemsReporte)
            {
                this.line = ordIt.GetItemCantidad(item);
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemName(item);
                this.leftMargin = 0f;
                    this.count++;
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.DrawEspacio();
        }
        private void DrawItemsReporteVentas()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //string titulo = "";
            //foreach (string item in this.itemsReporteVentas)
            //{
            //    titulo = ordIt.GetItemName(item);
            //}
            //this.gfx.DrawString(titulo, new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            //this.count++;
            //this.line = this.DottedLine();
            //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            //this.count++;
            //this.DrawEspacio();
            foreach (string item in this.itemsReporteVentas)
            {
                this.line = ordIt.GetItemCantidad(item);
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemName(item);
                this.leftMargin = 0f;
                this.count++;
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.DrawEspacio();
        }
        private void DrawItemsReporteDetallePagos()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            string titulo = "";
            foreach (string item in this.itemsReporteDetallePago)
            {
                titulo = ordIt.GetItemName(item);
            }
            this.gfx.DrawString(titulo, new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
            foreach (string item in this.itemsReporteDetallePago)
            {
                this.line = ordIt.GetItemCantidad(item);
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemName(item);
                this.leftMargin = 0f;
                this.count++;
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.DrawEspacio();
        }
       
        private void DrawItemsDocElectronicos()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.gfx.DrawString("N°    DOC ELECT.               MONTO", new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
            foreach (string item in this.itemsDocElectronicos)
            {
                this.line = ordIt.GetItemCantidad(item);
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemName(item);
                this.leftMargin = 0f;
                if (name.Length > this.maxCharDescription)
                {
                    int currentChar = 0;
                    for (int itemLenght = name.Length; itemLenght > this.maxCharDescription; itemLenght -= this.maxCharDescription)
                    {
                        this.line = ordIt.GetItemName(item);
                        this.gfx.DrawString("      " + this.line.Substring(currentChar, this.maxCharDescription), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescription;
                    }
                    this.line = ordIt.GetItemName(item);
                    this.gfx.DrawString("      " + this.line.Substring(currentChar, this.line.Length - currentChar), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.gfx.DrawString("      " + ordIt.GetItemName(item), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            //this.line = this.DottedLine();
            //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            //this.count++;
            //this.DrawEspacio();
        }
        private void DrawItemsDocElectronicosTotal()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //string titulo = "";
            //foreach (string item in this.itemsReporteDetallePago)
            //{
            //    titulo = ordIt.GetItemName(item);
            //}
            //this.gfx.DrawString(titulo, new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            //this.count++;
            //this.line = this.DottedLine();
            //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            //this.count++;
            //this.DrawEspacio();
            foreach (string item in this.itemsDocElectronicosTotal)
            {
                this.line = ordIt.GetItemCantidad(item);
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemName(item);
                this.leftMargin = 0f;
                this.count++;
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.DrawEspacio();
        }

        private void DrawItemsDocElectronicosÀnulados()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.gfx.DrawString("N°    DOC ELECT.               MONTO", new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
            foreach (string item in this.itemsDocElectronicosAnulados)
            {
                this.line = ordIt.GetItemCantidad(item);
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemName(item);
                this.leftMargin = 0f;
                if (name.Length > this.maxCharDescription)
                {
                    int currentChar = 0;
                    for (int itemLenght = name.Length; itemLenght > this.maxCharDescription; itemLenght -= this.maxCharDescription)
                    {
                        this.line = ordIt.GetItemName(item);
                        this.gfx.DrawString("      " + this.line.Substring(currentChar, this.maxCharDescription), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescription;
                    }
                    this.line = ordIt.GetItemName(item);
                    this.gfx.DrawString("      " + this.line.Substring(currentChar, this.line.Length - currentChar), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.gfx.DrawString("      " + ordIt.GetItemName(item), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            //this.line = this.DottedLine();
            //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            //this.count++;
            //this.DrawEspacio();
        }
        private void DrawItemsDocElectronicosÀnuladosTotal()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //string titulo = "";
            //foreach (string item in this.itemsReporteDetallePago)
            //{
            //    titulo = ordIt.GetItemName(item);
            //}
            //this.gfx.DrawString(titulo, new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            //this.count++;
            //this.line = this.DottedLine();
            //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            //this.count++;
            //this.DrawEspacio();
            foreach (string item in this.itemsDocElectronicosAnuladosTotal)
            {
                this.line = ordIt.GetItemCantidad(item);
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemName(item);
                this.leftMargin = 0f;
                this.count++;
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.DrawEspacio();
        }
        private void DrawCantidadDescImporte()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.gfx.DrawString("CANT  DESCRIPCION           IMPORTE", new Font(this.fontName,(float)(this.fontSize - 0.3),FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
            foreach (string item in this.items)
            {
                this.line = ordIt.GetItemCantidad(item);
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemName(item);
                this.leftMargin = 0f;
                if (name.Length > this.maxCharDescription)
                {
                    int currentChar = 0;
                    for (int itemLenght = name.Length; itemLenght > this.maxCharDescription; itemLenght -= this.maxCharDescription)
                    {
                        this.line = ordIt.GetItemName(item);
                        this.gfx.DrawString("      " + this.line.Substring(currentChar, this.maxCharDescription), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescription;
                    }
                    this.line = ordIt.GetItemName(item);
                    this.gfx.DrawString("      " + this.line.Substring(currentChar, this.line.Length - currentChar), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;   
                }
                else
                {
                    this.gfx.DrawString("      " + ordIt.GetItemName(item), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }

            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
        }
        private void DrawItemsAnulaciones()
        {
            Font printFont = new Font(this.fontName, (float)(this.fontSize + 3.5), FontStyle.Bold);
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.gfx.DrawString("CANT  DESCRIPCION                 ", new Font(this.fontName, (float)(this.fontSize + 3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
            
          

            foreach (string item in this.itemsAnulaciones)
            {
                this.line = ordIt.GetItemCantidad(item);
                this.gfx.DrawString(this.line , printFont, this.myBrush, this.leftMargin, YPosition() + (float)(2 * indexItem), new StringFormat());
                
                string name = ordIt.GetItemName(item);
                this.leftMargin = 0f;
                if (name.Length > this.maxCharDescription -1 )
                {
                    int currentChar = 0;
                    for (int itemLenght = name.Length; itemLenght > this.maxCharDescription - 1; itemLenght -= this.maxCharDescription - 1)
                    {
                        this.line = ordIt.GetItemName(item);
                        this.gfx.DrawString("      " + this.line.Substring(currentChar, this.maxCharDescription - 1), printFont, this.myBrush, this.leftMargin, YPosition() + (float)(2 * indexItem), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescription - 1;
                    }
                    this.line = ordIt.GetItemName(item);
                    this.gfx.DrawString("      " + this.line.Substring(currentChar, this.line.Length - currentChar) , printFont, this.myBrush, this.leftMargin, YPosition() + (float)(2 * indexItem), new StringFormat());
                    this.count++;
                }
                else               
                {
                    this.gfx.DrawString("      " + ordIt.GetItemName(item), printFont, this.myBrush, this.leftMargin, YPosition() + (float)(2 * indexItem), new StringFormat());
                    this.count++;
                }
                indexItem++;
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition() + (float)2 * indexItem, new StringFormat());
            this.count++;
            //this.DrawEspacio();
        }

        private void DrawItemsComanda()
        {
            //Font printFont = new Font(this.fontName, (float)(this.fontSize + 7), FontStyle.Bold); //recutacu - salazar infantas
            //Font printFont = new Font(this.fontName, (float)(this.fontSize + 4.5), FontStyle.Bold); //rancho robertin
            Font printFont = new Font(this.fontName, (float)(this.fontSize + 3.5), FontStyle.Bold);
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.gfx.DrawString("CANT  DESCRIPCION                 ", new Font(this.fontName, (float)(this.fontSize + 3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
            foreach (string item in this.itemsComanda)
            {
                this.line = ordIt.GetItemCantidad(item);
                this.gfx.DrawString(this.line , printFont, this.myBrush, this.leftMargin, YPosition() + (float)(2 * indexItem), new StringFormat());
                
                string name = ordIt.GetItemName(item);
                this.leftMargin = 0f;
                if (name.Length > this.maxCharDescription -1 )
                {
                    int currentChar = 0;
                    for (int itemLenght = name.Length; itemLenght > this.maxCharDescription - 1; itemLenght -= this.maxCharDescription - 1)
                    {
                        this.line = ordIt.GetItemName(item);
                        this.gfx.DrawString("     " + this.line.Substring(currentChar, this.maxCharDescription - 1), printFont, this.myBrush, this.leftMargin, YPosition() + (float)(2 * indexItem), new StringFormat());
                        this.count++;
                        this.DrawEspacio(); //recutacu - salazar infantas
                        currentChar += this.maxCharDescription - 1;
                    }
                    this.line = ordIt.GetItemName(item);
                    this.gfx.DrawString("     " + this.line.Substring(currentChar, this.line.Length - currentChar) , printFont, this.myBrush, this.leftMargin, YPosition() + (float)(2 * indexItem), new StringFormat());
                    this.count++;
                }
                else               
                {
                    this.gfx.DrawString("     " + ordIt.GetItemName(item), printFont, this.myBrush, this.leftMargin, YPosition() + (float)(2 * indexItem), new StringFormat());
                    this.count++;
                }
                indexItem++;
            }
            this.leftMargin = 0f;
            this.DrawEspacio();
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition() + (float)2 * indexItem, new StringFormat());
            this.count++;
            //this.DrawEspacio();
        }
        private void DrawItemsComandaRecutacu()
        {
            Font printFont = new Font(this.fontName, (float)(this.fontSize + 7), FontStyle.Bold); //recutacu - salazar
            //Font printFont = new Font(this.fontName, (float)(this.fontSize + 4.5), FontStyle.Bold); //rancho robertin
            //Font printFont = new Font(this.fontName, (float)(this.fontSize + 3.5), FontStyle.Bold);
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.gfx.DrawString("CANT  DESCRIPCION                 ", new Font(this.fontName, (float)(this.fontSize + 3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
            this.maxCharDescription = 16;
            foreach (string item in this.itemsComandaRecutacu)
            {
                this.line = ordIt.GetItemCantidad(item);
                this.gfx.DrawString(this.line , printFont, this.myBrush, this.leftMargin, YPosition() + (float)(2 * indexItem), new StringFormat());
                
                string name = ordIt.GetItemName(item);
                this.leftMargin = 0f;
                if (name.Length > this.maxCharDescription -1 )
                {
                    int currentChar = 0;
                    for (int itemLenght = name.Length; itemLenght > this.maxCharDescription - 1; itemLenght -= this.maxCharDescription - 1)
                    {
                        this.line = ordIt.GetItemName(item);
                        this.gfx.DrawString("   " + this.line.Substring(currentChar, this.maxCharDescription - 1), printFont, this.myBrush, this.leftMargin, YPosition() + (float)(2 * indexItem), new StringFormat());
                        this.count++;
                        this.DrawEspacio(); //recutacu - salazar
                        currentChar += this.maxCharDescription - 1;
                    }
                    this.line = ordIt.GetItemName(item);
                    this.gfx.DrawString("   " + this.line.Substring(currentChar, this.line.Length - currentChar) , printFont, this.myBrush, this.leftMargin, YPosition() + (float)(2 * indexItem), new StringFormat());
                    this.count++;
                }
                else               
                {
                    this.gfx.DrawString("      " + ordIt.GetItemName(item), printFont, this.myBrush, this.leftMargin, YPosition() + (float)(2 * indexItem), new StringFormat());
                    this.count++;
                }
                indexItem++;
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition() + (float)2 * indexItem, new StringFormat());
            this.count++;
            //this.DrawEspacio();
        }
        private void DrawTotales()
        {
            OrderTotal ordTot = new OrderTotal('?');
            foreach (string total in this.totales)
            {
                this.line = ordTot.GetTotalCantidad(total);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.leftMargin = 0f;
                this.line = "      " + ordTot.GetTotalName(total);
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.count++;
            }
            this.leftMargin = 0f;
            this.DrawEspacio();
            //this.DrawEspacio();
        }
        private void DrawTotalesPuertoEncantado()
        {
            OrderTotal ordTot = new OrderTotal('?');
            foreach (string total in this.totalesPuertoEncantado)
            {
                this.line = ordTot.GetTotalCantidad(total);
                this.line = this.AlignRightText(this.line.Length - 8) + this.line;
                this.gfx.DrawString(this.line, new Font(this.fontName, (float)(this.fontSize - 2), FontStyle.Regular), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.leftMargin = 0f;
                this.line = "           " + ordTot.GetTotalName(total);
                this.gfx.DrawString(this.line, new Font(this.fontName, (float)(this.fontSize - 2), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.count++;
            }
            this.leftMargin = 0f;
            this.DrawEspacio();
            //this.DrawEspacio();
        }

        private void DrawFooterPub()
        {
            int maxChar = this.maxChar - 9;
            Font printFont = new Font(this.fontName, (float)(this.fontSize + 3), FontStyle.Bold);

            foreach (string footer in this.footerLinesPublidad)
            {
                if (footer.Length > maxChar)
                {
                    int currentChar = 0;
                    for (int footerLenght = footer.Length; footerLenght > maxChar; footerLenght -= maxChar)
                    {
                        this.line = footer;
                        this.gfx.DrawString(this.line.Substring(currentChar, maxChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += maxChar;
                    }
                    this.line = footer;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = footer;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
            }
            this.leftMargin = 0f;
            this.DrawEspacio();
        }
        private void DrawFooter()
        {
            int maxChar = this.maxChar + 7;
            Font printFont = new Font(this.fontName, (float)(this.fontSize - 1.5), FontStyle.Regular);

            foreach (string footer in this.footerLines)
            {
                if (footer.Length > maxChar)
                {
                    int currentChar = 0;
                    for (int footerLenght = footer.Length; footerLenght > maxChar; footerLenght -= maxChar)
                    {
                        this.line = footer;
                        this.gfx.DrawString(this.line.Substring(currentChar, maxChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += maxChar;
                    }
                    this.line = footer;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = footer;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
            }
            this.leftMargin = 0f;
            this.DrawEspacio();
        }
        private void DrawFooter2()
        {
            int maxChar = this.maxChar -6;
            Font printFont = new Font(this.fontName , (float)(this.fontSize + 4), FontStyle.Bold);

            foreach (string footer in this.footer2Lines)
            {
                if (footer.Length > maxChar)
                {
                    int currentChar = 0;
                    for (int footerLenght = footer.Length; footerLenght > maxChar; footerLenght -= maxChar)
                    {
                        this.line = footer;
                        this.gfx.DrawString(this.line.Substring(currentChar, maxChar), printFont, this.myBrush, this.leftMargin, this.YPosition() + (float)1.5 * indexItem , new StringFormat());
                        this.count++;
                        currentChar += maxChar;
                    }
                    this.line = footer;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition() + (float)1.5 * indexItem, new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = footer;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition() + (float)1.5 * indexItem, new StringFormat());
                    this.count++;
                }
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
        }
        private void DrawFooterAnulacion()
        {
            int maxChar = this.maxChar -6;
            Font printFont = new Font(this.fontName , (float)(this.fontSize + 5), FontStyle.Bold);

            foreach (string footer in this.footer2LinesAnulacion)
            {
                if (footer.Length > maxChar)
                {
                    int currentChar = 0;
                    for (int footerLenght = footer.Length; footerLenght > maxChar; footerLenght -= maxChar)
                    {
                        this.line = footer;
                        this.gfx.DrawString(this.line.Substring(currentChar, maxChar), printFont, this.myBrush, this.leftMargin, this.YPosition() + (float)1.5 * indexItem , new StringFormat());
                        this.count++;
                        currentChar += maxChar;
                    }
                    this.line = footer;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition() + (float)1.5 * indexItem, new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = footer;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition() + (float)1.5 * indexItem, new StringFormat());
                    this.count++;
                }
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
        }
        private void DrawFooter2_Mesa()
        {
            int maxChar = this.maxChar -6;
            Font printFont = new Font(this.fontName , (float)(this.fontSize + 7), FontStyle.Bold);

            foreach (string footer in this.DrawFooter2_Mesa_lines)
            {
                if (footer.Length > maxChar)
                {
                    int currentChar = 0;
                    for (int footerLenght = footer.Length; footerLenght > maxChar; footerLenght -= maxChar)
                    {
                        this.line = footer;
                        this.gfx.DrawString(this.line.Substring(currentChar, maxChar), printFont, this.myBrush, this.leftMargin, this.YPosition() + (float)1.5 * indexItem , new StringFormat());
                        this.count++;
                        currentChar += maxChar;
                    }
                    this.line = footer;
                    this.gfx.DrawString(this.line.Substring(currentChar, this.line.Length - currentChar), printFont, this.myBrush, this.leftMargin, this.YPosition() + (float)1.5 * indexItem, new StringFormat());
                    this.count++;
                }
                else
                {
                    this.line = footer;
                    this.gfx.DrawString(this.line, printFont, this.myBrush, this.leftMargin, this.YPosition() + (float)1.5 * indexItem, new StringFormat());
                    this.count++;
                }
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
        }

        private void DrawEspacio()
        {
            this.line = "";
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, (float)(this.YPosition()), new StringFormat());
            this.count++;
        }


        private void DrawPlatos()
        {
            OrderItem ordIt = new OrderItem('?');
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.gfx.DrawString("PLATO                     CANTIDAD", new Font(this.fontName, (float)(this.fontSize - 0.3), FontStyle.Bold), this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            this.fontSize = 9;
            //this.DrawEspacio();
            foreach (string item in this.itemsReportePlatos)
            {
                //this.line = ordIt.GetItemCantidad(item);
                //this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPriceUni(item);
                this.line = this.AlignRightText(13) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                this.line = ordIt.GetItemPrice(item);
                this.line = this.AlignRightText(this.line.Length) + this.line;
                this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                string name = ordIt.GetItemNameTicket(item);
                this.leftMargin = 0f;
                if (name.Length > this.maxCharDescription)
                {
                    int currentChar = 0;
                    for (int itemLenght = name.Length; itemLenght > this.maxCharDescription; itemLenght -= this.maxCharDescription)
                    {
                        this.line = ordIt.GetItemNameTicket(item);
                        this.gfx.DrawString("" + this.line.Substring(currentChar, this.maxCharDescription), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                        this.count++;
                        currentChar += this.maxCharDescription;
                    }
                    this.line = ordIt.GetItemNameTicket(item);
                    this.gfx.DrawString("" + this.line.Substring(currentChar, this.line.Length - currentChar), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
                else
                {
                    this.gfx.DrawString("" + ordIt.GetItemNameTicket(item), this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
                    this.count++;
                }
            }
            this.leftMargin = 0f;
            //this.DrawEspacio();
            this.line = this.DottedLine();
            this.gfx.DrawString(this.line, this.printFont, this.myBrush, this.leftMargin, this.YPosition(), new StringFormat());
            this.count++;
            //this.DrawEspacio();
        }
    }
}

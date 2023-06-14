using Capa_Entidades.Models.Stock;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;
using Capa_Negocio.Funciones_Globales;
using SpreadsheetLight;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.Kardex
{
    public class KardexViewModel : INotifyPropertyChanged
    {
        KardeStructure kstructure = new KardeStructure();
        #region Objetos
        private string _operacion;
        public decimal StockInicial { get; set; } = 0;
        public decimal CantidadEntradas { get; set; } = 0;
        public decimal CantidadSalidas { get; set; } = 0;
        public decimal StockFinal { get; set; } = 0;
        public int[] idMovimientos = null;
        public int[] idAlmacenes = null;
        public string TextoTipoMovimientoSeleccionados { get; set; }
        public string ForegroundComboTipoMovimiento { get; set; }
        public string TextoAlmacenSeleccionados { get; set; }
        public string ForegroundComboAlmacen { get; set; }
        public int CantRegDataGrilla { get; set; }
        #endregion
        #region GetSetObjetos
        public string Operacion
        {
            get=> _operacion;
            set 
            {
                if (value == "Lista")
                {
                    GetLista();
                }
                _operacion = value;
            }
        }
        #endregion
        #region Negocio
        Neg_Combo negCombo = new Neg_Combo();
        Funcion_Global globales = new Funcion_Global();
        #endregion
        #region Lista
        public List<MovimientoInsumoAlmacen> DataMovimientoInsumoAlmacen { get; set; }
        public List<Insumos> DataInsumos { get; set; }
        public List<TipoMovimientoInsumo> DataTipoMovimientoInsumo { get; set; }
        public List<TipoMovimientoInsumo> DataTipoMovimientoInsumo2 { get; set; }
        public List<Almacen> DataAlmacen { get; set; }
        public List<Almacen> DataCantidadRegistros { get; set; }
        public List<TipoMovimientoInsumo> DataIdMovimientos { get; set; }
        #endregion
        #region Entidad
        private Insumos _InsumoSelected;
        private TipoMovimientoInsumo _TipoMovimientoInsumoSelected;
        private Almacen _AlmacenSelected;
        private Almacen _CantidadSelected;
        private Insumos _Insumos;
        private TipoMovimientoInsumo _TipoMovimientoInsumo;
        private Almacen _Almacen;
        public Insumos Insumos
        {
            get => _Insumos;
            set 
            {
                _Insumos = value;
            }
        }
        public TipoMovimientoInsumo TipoMovimientoInsumo
        {
            get => _TipoMovimientoInsumo;
            set 
            {
                _TipoMovimientoInsumo = value;
            }
        }
        public Almacen Almacen
        {
            get => _Almacen;
            set 
            {
                _Almacen = value;
            }
        }
        #endregion
        #region SeleccionObtetos
        public Insumos InsumoSelected
        {
            get => _InsumoSelected;
            set
            {
                if (value != null)
                {
                    Insumos.idins = ((Insumos)value).idins;
                    _InsumoSelected = value;
                }
               
            }
        }
        public Almacen CantidadSelected
        {
            get => _CantidadSelected;
            set
            {
                if (value != null)
                {
                    Almacen.idCantidadRegistros = ((Almacen)value).idCantidadRegistros;
                    Almacen.cantidadRegistros = ((Almacen)value).cantidadRegistros;
                    _CantidadSelected = value;
                }
               
            }
        }
        #endregion
        #region INotify
        //Para el INotifyPropertyChanged sea Inicializado.
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
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
        #endregion
        #region Commands
        public ICommand BuscarCommand { get; set; }
        public ICommand BuscarGeneralCommand { get; set; }
        public ICommand MomivientoSelectedCommand { get; set; }
        public ICommand AlmacenSelectedCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        #endregion
        public KardexViewModel()
        {
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.BuscarGeneralCommand = new RelayCommand(new Action(BuscarGeneral));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportaExcel));
            this.MomivientoSelectedCommand = new ParamCommand(new Action<object>(MomivientoSelectedC));
            this.AlmacenSelectedCommand = new ParamCommand(new Action<object>(AlmacenSelectedC));

            this.DataInsumos = new List<Insumos>();
            this.DataTipoMovimientoInsumo = new List<TipoMovimientoInsumo>();
            this.Insumos = new Insumos();
            this.TipoMovimientoInsumo = new TipoMovimientoInsumo();
            this.DataCantidadRegistros = new List<Almacen>();
            this.Almacen = new Almacen();
            this.DataMovimientoInsumoAlmacen = new List<MovimientoInsumoAlmacen>();
            this.DataAlmacen = new List<Almacen>();
            this.DataIdMovimientos = new List<TipoMovimientoInsumo>();
            this.Operacion = "Lista";
        }
        private void ExportaExcel() {
            if (DataMovimientoInsumoAlmacen.Count == 0) {
                globales.Mensaje("SOS-FOOD Información", "No hay datos a exportar", 2);
                return;
            }
            #region Ubicacion del Archivo Excel
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            saveFileDialog1.FileName = "Reporte Kardex por Insumo_" + DateTime.Now.ToString("ddMMyyyy HHmmss");
            string ubicacion = "";
            if (saveFileDialog1.ShowDialog() == true)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    string direccion = saveFileDialog1.InitialDirectory;
                    string nombrearchivo = saveFileDialog1.FileName;
                    ubicacion = saveFileDialog1.FileName;
                    myStream.Close();
                }
            }
            #endregion
            SLDocument sl = new SLDocument();
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Reporte");
            sl.SetCellValue("A1", "Informe de Kardex");
            
            #region Estilos
            //Cabecera
            SLStyle estiloCabecera = sl.CreateStyle();
            estiloCabecera.Font.FontName = "Arial";
            estiloCabecera.Font.FontSize = 13;
            estiloCabecera.Font.Bold = true;
            estiloCabecera.Font.FontColor = System.Drawing.Color.OrangeRed;

            //Titulos
            SLStyle estiloT = sl.CreateStyle();
            estiloT.Font.FontName = "Arial";
            estiloT.Font.FontSize = 11;
            estiloT.Font.Bold = true;

            //Colores
            SLStyle ecventas = sl.CreateStyle(); ecventas.Font.FontColor = System.Drawing.Color.FromArgb(0, 150, 62);
            SLStyle eccajachica = sl.CreateStyle(); eccajachica.Font.FontColor = System.Drawing.Color.FromArgb(0, 166, 182);
            SLStyle ecpagos = sl.CreateStyle(); ecpagos.Font.FontColor = System.Drawing.Color.FromArgb(109, 1, 232);
            SLStyle ectotalcaja = sl.CreateStyle(); ectotalcaja.Font.FontColor = System.Drawing.Color.FromArgb(2, 153, 93);
            SLStyle ecboletadyfacturas = sl.CreateStyle(); ecboletadyfacturas.Font.FontColor = System.Drawing.Color.FromArgb(9, 101, 235);
            SLStyle ecindicadores = sl.CreateStyle(); ecindicadores.Font.FontColor = System.Drawing.Color.FromArgb(0, 130, 151);

            //Montos
            SLStyle estiloM = sl.CreateStyle();
            estiloM.Font.FontName = "Arial";
            estiloM.Font.FontSize = 10;
            estiloM.Font.Bold = true;

            //Bordes
            SLStyle border = new SLStyle();
            border.Border.LeftBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            border.Border.LeftBorder.Color = System.Drawing.Color.Black;
            border.Border.TopBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            border.Border.BottomBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            border.Border.RightBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            #endregion
            sl.SetCellStyle("A1", estiloCabecera);
            sl.MergeWorksheetCells("A1", "C1");

            sl.SetCellValue("A3", "Fecha");
            sl.SetCellValue("B3", "Motivo");
            sl.SetCellValue("C3", "Almacen");
            sl.SetCellValue("D3", "Und. Medida");
            sl.SetCellValue("E3", "Entrada");
            sl.SetCellValue("F3", "Salida");
            sl.SetCellValue("G3", "Devolucion");
            sl.SetCellValue("H3", "Stock");
            sl.SetCellValue("I3", "Costo Unitario");
            sl.SetCellStyle("A3","I3", estiloT);

            int fila = 4;

            foreach (var i in DataMovimientoInsumoAlmacen) {
                sl.SetCellValue("A" + fila, i.MI_F_CREATE);
                sl.SetCellValue("B" + fila, i.MOV_DESCR + " - " +i.MI_DESCR);
                sl.SetCellValue("C" + fila, i.ALM_NOM);
                sl.SetCellValue("D" + fila, i.TM_DESC);
                sl.SetCellValue("E" + fila, i.ENTRADA);
                sl.SetCellValue("F" + fila, i.SALIDA);
                sl.SetCellValue("G" + fila, i.DEVOLUCION);
                sl.SetCellValue("H" + fila, i.STOCK);
                sl.SetCellValue("I" + fila, i.INS_PRECIO);
                fila += 1;
            }
            sl.SetCellStyle("A4", "I" + (fila - 1), border);

            sl.AutoFitColumn("A", "K");
            sl.SaveAs(ubicacion);

        }
        public void AlmacenSelectedC(object id)
        {
            int _id = (int)id;
            bool ischeck = this.DataAlmacen.Where(w => w.idalm == _id).FirstOrDefault().ischeck;
            if (ischeck == false){this.DataAlmacen.Where(w => w.idalm == _id).FirstOrDefault().ischeck = false;}
            else{this.DataAlmacen.Where(w => w.idalm == _id).FirstOrDefault().ischeck = true;}
            string[] texto = this.DataAlmacen.Where(w => w.ischeck == true).Select(s => s.nomalm).ToArray();
            string h = "";
            for (int i = 0; i <= texto.Count() - 1; i++)
            {
                if (h == ""){h = texto[i].ToString();}
                else{h = h + ", " + texto[i].ToString();}
            }
            ForegroundComboAlmacen = "Black";
            TextoAlmacenSeleccionados = h;
            idAlmacenes = DataAlmacen.Where(w => w.ischeck == true).Select(s => s.idalm).ToArray();
            DataInsumos = kstructure.GetInsumos_x_Almacen(idAlmacenes);
        }
        public void MomivientoSelectedC(object id)
        {
            int _id = (int)id;
            bool ischeck = this.DataTipoMovimientoInsumo.Where(w => w.ID == _id).FirstOrDefault().ischeck;
            if (ischeck == false){this.DataTipoMovimientoInsumo.Where(w => w.ID == _id).FirstOrDefault().ischeck = false;}
            else{this.DataTipoMovimientoInsumo.Where(w => w.ID == _id).FirstOrDefault().ischeck = true;}

            string[] texto = this.DataTipoMovimientoInsumo.Where(w => w.ischeck == true).Select(s => s.MOV_DESCR).ToArray();
            string h = "";
            for (int i = 0; i <= texto.Count() - 1; i++)
            {
                if (h == ""){h = texto[i].ToString();}
                else{h = h + ", " + texto[i].ToString();}
            }
            ForegroundComboTipoMovimiento = "Black";
            TextoTipoMovimientoSeleccionados = h;
        }
        private void BuscarGeneral()
        {
            idMovimientos = DataTipoMovimientoInsumo.Where(w => w.ischeck == true).Select(s => s.ID).ToArray();
            if (idMovimientos.Count() == 0)
            {
                ForegroundComboTipoMovimiento = "Red";
                TextoTipoMovimientoSeleccionados = "Seleccione tipo de Movimiento";
                return;
            }
            DataMovimientoInsumoAlmacen = kstructure.GetDataGeneralGeneral(Insumos.idins, idMovimientos, Almacen.idalm, StartDate, FinishDate, (Almacen.cantidadRegistros == "TODOS") ? 0 : Convert.ToInt32(Almacen.cantidadRegistros), Almacen.idCantidadRegistros);
        }
        private void Buscar()
        {
            idMovimientos = DataTipoMovimientoInsumo.Where(w => w.ischeck == true).Select(s => s.ID).ToArray();
            idAlmacenes = DataAlmacen.Where(w => w.ischeck == true).Select(s => s.idalm).ToArray();
            int[] hola = { 0 };

            if (idMovimientos.Count() == 0)
            {
                ForegroundComboTipoMovimiento = "Red";
                TextoTipoMovimientoSeleccionados = "Seleccione tipo de Movimiento";
                return;
            }
            if (idAlmacenes.Count() == 0)
            {
                ForegroundComboAlmacen = "Red";
                TextoAlmacenSeleccionados = "Seleccione Almacen";
                return;
            }
            DataMovimientoInsumoAlmacen = kstructure.GetDataGeneral(Insumos.idins, idMovimientos, idAlmacenes, StartDate, FinishDate, (Almacen.cantidadRegistros == "TODOS") ? 0 : Convert.ToInt32(Almacen.cantidadRegistros), Almacen.idCantidadRegistros);

            List<MovimientoInsumoAlmacen> data = new List<MovimientoInsumoAlmacen>();
            data = kstructure.GetDataGeneral(Insumos.idins, hola, idAlmacenes, StartDate, FinishDate, (Almacen.cantidadRegistros == "TODOS") ? 0 : Convert.ToInt32(Almacen.cantidadRegistros), Almacen.idCantidadRegistros);
            
            if (data.Count == 0)
            {
                StockInicial = 0;
                CantidadEntradas = 0;
                CantidadSalidas = 0;
                StockFinal = 0;
                return;
            }
            StockInicial = kstructure.GetStockCierreInsumo(Insumos.idins, Almacen.idalm, StartDate, FinishDate);
            CantidadEntradas = data.Sum(s => s.ENTRADA);
            CantidadSalidas = data.Sum(s => s.SALIDA);
            decimal cant_devoluciones = data.Sum(s => s.DEVOLUCION);
            CantidadSalidas = CantidadSalidas - cant_devoluciones;
            //StockFinal = data
            //    .OrderByDescending(o => o.MI_F_CREATE)
            //    .Take(1)
            //    .FirstOrDefault().STOCK;

            StockFinal = StockInicial + CantidadEntradas - CantidadSalidas;
            CantRegDataGrilla = DataMovimientoInsumoAlmacen.Count();
        }
        private void GetLista()
        {
            DataTipoMovimientoInsumo = kstructure.GetTipoMovimientoInsumoAlmacen();

            #region Texto Aparece Combo Tipo Movimiento
            string[] texto = this.DataTipoMovimientoInsumo.Where(w => w.ischeck == true).Select(s => s.MOV_DESCR).ToArray();
            string text = "";
            for (int a = 0; a <= texto.Count() - 1; a++)
            {
                if (text == "") { text = texto[a].ToString(); }
                else { text = text + ", " + texto[a].ToString(); }
            }
            ForegroundComboTipoMovimiento = "Black";
            TextoTipoMovimientoSeleccionados = text;
            #endregion

            DataAlmacen = kstructure.GetAlmacen();
            #region Texto Aparece Combo Almacen
            string[] textAlm = this.DataAlmacen.Where(w => w.ischeck == true).Select(s => s.nomalm).ToArray();
            string txtalm = "";
            for (int y = 0; y <= textAlm.Count() - 1; y++)
            {
                if (txtalm == "") { txtalm = textAlm[y].ToString(); }
                else { txtalm = txtalm + ", " + textAlm[y].ToString(); }
            }
            ForegroundComboAlmacen = "Black";
            TextoAlmacenSeleccionados = txtalm;
            #endregion

            idAlmacenes = DataAlmacen.Where(w => w.ischeck == true).Select(s => s.idalm).ToArray();
            DataInsumos = kstructure.GetInsumos_x_Almacen(idAlmacenes);
            int i = 1;
            int h = 0;
            Almacen alm = new Almacen();
            alm.idCantidadRegistros = 1;
            alm.cantidadRegistros = "TODOS";
            DataCantidadRegistros.Add(alm);

            while (h <= 100)
            {
                Almacen a = new Almacen();
                a.idCantidadRegistros = i + 1;
                h = h + 50;
                a.cantidadRegistros = h.ToString();
                DataCantidadRegistros.Add(a);
            }
        }
    }
}

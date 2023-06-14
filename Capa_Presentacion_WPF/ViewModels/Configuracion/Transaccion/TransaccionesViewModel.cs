using Capa_Entidades.Models.Transacciones;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Transaccion;
using ExportToExcel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Transaccion
{
  public  class TransaccionesViewModel : IGeneric
    {
        Neg_Transacciones negTra = new Neg_Transacciones();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public ObservableCollection<Transacciones> DataTransacciones { get; set; }
        public ObservableCollection<Transacciones> _lstransaccion = new ObservableCollection<Transacciones>();
        public Transacciones transacciones;
        private string operacion;
        public ObservableCollection<Transacciones> Lstransacciones
        {
            get { return _lstransaccion; }
            set { _lstransaccion = value; }
        }
        private ICollectionView _transaccionColletion;
        private string _filtro = string.Empty;

        //private bool Filter(object bus)
        //{
        //    Transacciones Transacciones = bus as Transacciones;
        //    if (!string.IsNullOrEmpty(transacciones) &&
        //    {

        //    }
        //}
        //public string Filtro
        //{
        //    get { return _filtro; }
        //    set {
        //        ( _filtro, value);
        //        Filtro.First
        //        }
        //}



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
        public Transacciones Transacciones
        {
            get => transacciones;
            set
            {
                transacciones = value;
            }
        }
        public string TextoBuscar { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public TransaccionesViewModel()
        {
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.Operacion = "Lista";

        }

        private void Buscar()
        {
            DataTransacciones = negTra.GetTransacciones();

            ObservableCollection<Transacciones> ls = new ObservableCollection<Transacciones>();
            ls = DataTransacciones;
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataTransacciones = negTra.GetTransacciones();
            }
            else
            {
                List<Transacciones> lista = ls
                    .Where(w =>
                    w.TIPO_OPERACION.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.NOM_TABLA.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.NOM_CAMPO.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.VALOR_ORIGINAL.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.VALOR_NUEVO.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.FECH_HORA_TRAN.ToString().ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.USU_NOM.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.NOM_ROL.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.NOM_EQUIPO.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.IP_LOCAL.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.ACCION.ToUpper().Contains(TextoBuscar.ToUpper())
                    ).ToList();
                ObservableCollection<Transacciones> o = new ObservableCollection<Transacciones>(lista);
                DataTransacciones = o;
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
            saveFileDialog1.FileName = "Auditoria " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("OPERACION");
            dt.Columns.Add("TABLA");
            dt.Columns.Add("ID CAMPO");
            dt.Columns.Add("NOMBRE CAMPO");
            dt.Columns.Add("VALOR ORIGINAL");
            dt.Columns.Add("VALOR NUEVO");
            dt.Columns.Add("FEC. TRAN");
            dt.Columns.Add("ID USUARIO");
            dt.Columns.Add("NOMBRE USUARIO");
            dt.Columns.Add("ID ROL");
            dt.Columns.Add("NOMBRE ROL");
            dt.Columns.Add("NOMBRE EQUIPO");
            dt.Columns.Add("IP LOCAL");
            dt.Columns.Add("ACCION");
            foreach (var p in DataTransacciones)
            {
                dt.Rows.Add(new object[] { p.ID, p.TIPO_OPERACION, p.NOM_TABLA,p.ID_CAMPO,p.NOM_CAMPO,p.VALOR_ORIGINAL,p.VALOR_NUEVO,p.FECH_HORA_TRAN,p.IDUSU,p.USU_NOM,p.IDROL,p.NOM_ROL,p.IP_LOCAL,p.ACCION});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE AUDITORIA");
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
            saveFileDialog1.FileName = "Auditoria " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("OPERACION");
            dt.Columns.Add("TABLA");
            dt.Columns.Add("ID CAMPO");
            dt.Columns.Add("NOMBRE CAMPO");
            dt.Columns.Add("VALOR ORIGINAL");
            dt.Columns.Add("VALOR NUEVO");
            dt.Columns.Add("FEC. TRAN");
            dt.Columns.Add("ID USUARIO");
            dt.Columns.Add("NOMBRE USUARIO");
            dt.Columns.Add("ID ROL");
            dt.Columns.Add("NOMBRE ROL");
            dt.Columns.Add("NOMBRE EQUIPO");
            dt.Columns.Add("IP LOCAL");
            dt.Columns.Add("ACCION");
            foreach (var p in DataTransacciones)
            {
                dt.Rows.Add(new object[] { p.ID, p.TIPO_OPERACION, p.NOM_TABLA, p.ID_CAMPO, p.NOM_CAMPO, p.VALOR_ORIGINAL, p.VALOR_NUEVO, p.FECH_HORA_TRAN, p.IDUSU, p.USU_NOM, p.IDROL, p.NOM_ROL, p.IP_LOCAL, p.ACCION });
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

        private void GetLista()
        {
            DataTransacciones =negTra.GetTransacciones();
        }
       
    }
}

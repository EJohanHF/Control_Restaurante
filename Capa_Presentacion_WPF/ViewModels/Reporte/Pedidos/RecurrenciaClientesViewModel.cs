using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Pedido;
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
    public class RecurrenciaClientesViewModel : IGeneric
    {
        #region Negocio
        Neg_Pedido neg_ped = new Neg_Pedido();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        #endregion
        #region Objects
        private string _operacion;
        public string TextoBuscar { get; set; }
        #endregion
        #region GetSetObjects
        public string Operacion
        {
            get => _operacion;
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
        #region Lista
        public ObservableCollection<Capa_Entidades.Models.Pedido.Pedidos> DataRecurrenciaClientes { get; set; }
        #endregion
        #region Commands
        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        #endregion

        public RecurrenciaClientesViewModel()
        {

            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.Operacion = "Lista";
        }

        private void Buscar()
        {
            ObservableCollection<Capa_Entidades.Models.Pedido.Pedidos> ls = new ObservableCollection<Capa_Entidades.Models.Pedido.Pedidos>();
            ls = DataRecurrenciaClientes = neg_ped.GetRecurrenciaClientes();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataRecurrenciaClientes = neg_ped.GetRecurrenciaClientes();
            }
            else
            {
                List<Capa_Entidades.Models.Pedido.Pedidos> lista = ls
                    .Where(w =>
                    w.NOMBRE_CLIENTE.ToUpper().Contains(TextoBuscar.ToUpper())
                    ).ToList();
                ObservableCollection<Capa_Entidades.Models.Pedido.Pedidos> o = new ObservableCollection<Capa_Entidades.Models.Pedido.Pedidos>(lista);
                DataRecurrenciaClientes = o;
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
            saveFileDialog1.FileName = "Recurrencia de Clientes " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID CLIENTE");
            dt.Columns.Add("NOMBRE CLIENTE");
            dt.Columns.Add("FECHA PRIMER CONSUMO");
            dt.Columns.Add("FECHA ULTIMO CONSUMO");
            dt.Columns.Add("CANTIDAD DE CONSUMO");
            dt.Columns.Add("IMPORTE DE CONSUMO");
            foreach (var p in DataRecurrenciaClientes)
            {
                dt.Rows.Add(new object[] { p.ID_CLIENTE, p.NOMBRE_CLIENTE, p.PRIMER_CONSUMO,p.ULTIMO_CONSUMO,p.CANTIDAD_CONSUMO,p.IMPORTE_CONSUMO});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE RECURRENCIA DE CLIENTES");
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
            saveFileDialog1.FileName = "Recurrencia de Clientes " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID CLIENTE");
            dt.Columns.Add("NOMBRE CLIENTE");
            dt.Columns.Add("FECHA PRIMER CONSUMO");
            dt.Columns.Add("FECHA ULTIMO CONSUMO");
            dt.Columns.Add("CANTIDAD DE CONSUMO");
            dt.Columns.Add("IMPORTE DE CONSUMO");
            foreach (var p in DataRecurrenciaClientes)
            {
                dt.Rows.Add(new object[] { p.ID_CLIENTE, p.NOMBRE_CLIENTE, p.PRIMER_CONSUMO, p.ULTIMO_CONSUMO, p.CANTIDAD_CONSUMO, p.IMPORTE_CONSUMO });
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
            DataRecurrenciaClientes = neg_ped.GetRecurrenciaClientes();
        }
    }
}

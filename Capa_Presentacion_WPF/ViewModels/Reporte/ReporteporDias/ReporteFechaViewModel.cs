using Capa_Entidades;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Presentacion_WPF.Views.Dialogs;
using ExportToExcel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.ReporteporDias
{
  public class ReporteFechaViewModel : IGeneric
    {
        Neg_Combo negCombo = new Neg_Combo();
        ReporFecha reporteF = new ReporFecha();
        Neg_CierreParcial negCierre = new Neg_CierreParcial();
        CreateExcelFile exportExcel = new CreateExcelFile();
        private string operacion;
        private ReporFecha reportefech;
        private DateTime desde ;
        private DateTime hasta;
        private string filename;

        public ObservableCollection<Pagar> DataDetallePagar { get; set; }
        public List<Ent_Combo> ComboFormaPago{ get; set; }
        public List<Ent_Combo> ComboTipoMoneda{ get; set; }
        public ReporFecha ReporFecha
        {
            get => reportefech;
            set
            {
                reportefech = value;
            }
        }
        public DateTime Desde
        {
            get => desde;
            set
            {
                desde = value;
            }
        }
        public DateTime Hasta
        {
            get =>  hasta;
            set
            {
                //if (value!=null)
                //{
                //    this.ReporFecha.hasta = value;
                //}
                
                hasta = value;
            }
        }
        public string FileName
        {
            get => filename;
            set
            {
                filename = value;
            }
        }
        public ICommand ExportarporFechaCommand { get; set; }
        public string Operacion
        {
            get => operacion;
            set
            {
                if (value == "Lista")
                    
                operacion = value;

            }
        }
        public ReporteFechaViewModel()
        {
            this.Operacion = "Lista";
            this.ExportarporFechaCommand = new RelayCommand(new Action(Exportar));
            //this.Desde = DateTime.Now;
            //this.Hasta = DateTime.Now;
            this.ReporFecha = new ReporFecha();
        }
        private void Exportar()
        {
            string desdet = Convert.ToDateTime(this.ReporFecha.desde).ToString("yyyy-MM-dd");
            string hastat = Convert.ToDateTime(this.ReporFecha.hasta).ToString("yyyy-MM-dd");

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            saveFileDialog1.FileName = "ReporteVentas_" + desdet + "_" + hastat;
            DataTable dt = new DataTable();
            dt = negCierre.GetReporteFecha(desdet, hastat);
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
        Funcion_Global negFuncionGlobal = new Funcion_Global();
    }
}

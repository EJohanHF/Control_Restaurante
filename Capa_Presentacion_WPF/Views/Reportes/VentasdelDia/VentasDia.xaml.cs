using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Capa_Presentacion_WPF.ViewModels.Reporte;
using Capa_Presentacion_WPF.ViewModels.Reporte.ReporteporDias;
using Capa_Presentacion_WPF.Views.Dialogs.DialogVentaporDia;
using LiveCharts;
using LiveCharts.Wpf;
using MaterialDesignThemes.Wpf;

namespace Capa_Presentacion_WPF.Views.Reportes.VentasdelDia
{
    /// <summary>
    /// Lógica de interacción para VentasDia.xaml
    /// </summary>
    public partial class VentasDia : UserControl
    {
        public VentasDia()
        {
            InitializeComponent();
            

            this.DataContext = new VentasDiaViewModel();
            
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = new VentasDiaViewModel();
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
           
            var BolFac = new DialogBoleta_Factura
            {

            };
            var x = await DialogHost.Show(BolFac, "RootDialog");
        }

        



        private void BtnAnularPlato_Click(object sender, RoutedEventArgs e)
        {
            //var cellInfo = DatagridPed.SelectedCells[7];

            //var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;

            ////string EstadoPago;
            ////EstadoPago = det.nom_estado_f ;
            ////= Application.Current.Properties["EstadoF"].ToString();
            //Application.Current.Properties["OperPagoCel"] = "ANULAR PLATO";
            //if (content == "PAGADO" || content == "PENDIENTE")
            //{
            //    var opencampago = new DialogAnularPlato
            //    {

            //    };
            //    DialogHost.Show(opencampago, "RootDialog");
            //}
        }

       

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartPoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartPoint.SeriesView;
            selectedSeries.PushOut = 9;
        }


        private async void BtnPagar_Click(object sender, RoutedEventArgs e)
        {
            var BolFac = new DialogPago
            {

            };
            var x = await DialogHost.Show(BolFac, "RootDialog");

        }

        private void btnAnularPedido_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

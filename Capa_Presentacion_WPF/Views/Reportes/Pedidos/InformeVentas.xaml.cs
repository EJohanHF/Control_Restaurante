using Capa_Presentacion_WPF.ViewModels.Reporte.Pedidos;
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

namespace Capa_Presentacion_WPF.Views.Reportes.Pedidos
{
    /// <summary>
    /// Interaction logic for InformeVentas.xaml
    /// </summary>
    public partial class InformeVentas : UserControl
    {
        public InformeVentas()
        {
            InitializeComponent();
            
            this.DataContext = new InformeVentasViewModel();
            cboCanales.SelectedIndex = 0;
            cboCajas.SelectedIndex = 0;
        }

        private void CombinedDialogOpenedEventHandler(object sender, MaterialDesignThemes.Wpf.DialogOpenedEventArgs eventArgs)
        {

        }

        private void CombinedDialogClosingEventHandler(object sender, MaterialDesignThemes.Wpf.DialogOpenedEventArgs eventArgs)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //if (this.DataContext != null)
            //{
            //    ((InformeVentasViewModel)this.DataContext).Id_Caja_Seleccion = ((PasswordBox)sender).Password;
            //}
            cboCanales.SelectedIndex = 0;
            cboCajas.SelectedIndex = 0;
            cboComprobantes.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cboCanales.SelectedIndex = 0;
            cboCajas.SelectedIndex = 0;
            cboComprobantes.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
            //txtNumero.Clear();
            //txtSerie.Clear();
        }
    }
}

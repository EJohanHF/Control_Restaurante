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
    /// Lógica de interacción para VentasProducto.xaml
    /// </summary>
    public partial class VentasProducto : UserControl
    {
        public VentasProducto()
        {
            InitializeComponent();
            this.DataContext = new VentasProductoViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cboCanales.SelectedIndex = 0;
            cboCajas.SelectedIndex = 0;
            cboComprobantes.SelectedIndex = 0;
            cboPlatos.SelectedIndex = 0;
            cboGrupos.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cboCanales.SelectedIndex = 0;
            cboCajas.SelectedIndex = 0;
            cboComprobantes.SelectedIndex = 0;
            cboPlatos.SelectedIndex = 0;
            cboGrupos.SelectedIndex = 0;
        }
    }
}

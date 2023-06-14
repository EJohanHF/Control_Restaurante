using Capa_Presentacion_WPF.ViewModels.Configuracion.Empleados;
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

namespace Capa_Presentacion_WPF.Views.Configuracion
{
    /// <summary>
    /// Lógica de interacción para Corporacion.xaml
    /// </summary>
    public partial class Corporacion : UserControl
    {
        public Corporacion()
        {
            InitializeComponent();
            this.DataContext = new CorporacionViewModel();
        }

        private void Txtnom_KeyUp(object sender, KeyEventArgs e)
        {
            lblnom.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtnom.Text))
            {
                return;
            }
            else lblnom.Visibility = System.Windows.Visibility.Visible;
        }
    }
}

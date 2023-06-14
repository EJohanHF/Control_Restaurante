using Capa_Presentacion_WPF.ViewModels.Cuentas;
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

namespace Capa_Presentacion_WPF.Views.Cuentas
{
    /// <summary>
    /// Lógica de interacción para DividirCuentas.xaml
    /// </summary>
    public partial class DividirCuentas : UserControl
    {
        public DividirCuentas()
        {
            InitializeComponent();
            this.DataContext = new DividirCuentasViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Combito.SelectedIndex = 0;
        }
    }
}

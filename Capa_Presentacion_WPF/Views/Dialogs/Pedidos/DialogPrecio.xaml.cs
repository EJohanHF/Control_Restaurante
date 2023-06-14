using Capa_Presentacion_WPF.ViewModels.Dialogs;
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

namespace Capa_Presentacion_WPF.Views.Dialogs.Pedidos
{
    /// <summary>
    /// Lógica de interacción para DialogPrecio.xaml
    /// </summary>
    public partial class DialogPrecio : UserControl
    {
        public DialogPrecio()
        {
            InitializeComponent();
            this.DataContext = new DialogPrecioViewModel();
            TecladoNumerico.punto = true;
            TecladoNumerico.txtbox = txtPrecio;
            txtPrecio.Focus();
        }

        private void txtPrecio_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textb = txtPrecio;
            TecladoNumerico.txtbox = textb;
            TecladoNumerico._focusedControl = (Control)sender;
        }
    }
}

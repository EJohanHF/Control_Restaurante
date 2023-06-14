using Capa_Presentacion_WPF.ViewModels.Dialogs.Facturacion_Electronica;
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
    /// Lógica de interacción para DialogNroPersonas.xaml
    /// </summary>
    public partial class DialogNroPersonas : UserControl
    {
        public DialogNroPersonas()
        {
            InitializeComponent();
            this.DataContext = new DialogNroPersonasViewModel();
            TecladoNumerico.punto = true;
            TecladoNumerico.txtbox = txtNroPersonas;
            txtNroPersonas.Focus();
        }

        private void txtNroPersonas_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textb = txtNroPersonas;
            TecladoNumerico.txtbox = textb;
            TecladoNumerico._focusedControl = (Control)sender;
        }

        private void teclado_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

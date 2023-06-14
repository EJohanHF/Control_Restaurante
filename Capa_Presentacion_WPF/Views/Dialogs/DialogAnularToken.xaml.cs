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

namespace Capa_Presentacion_WPF.Views.Dialogs
{
    /// <summary>
    /// Lógica de interacción para DialogAnularToken.xaml
    /// </summary>
    public partial class DialogAnularToken : UserControl
    {
        public DialogAnularToken()
        {
            InitializeComponent();
            this.DataContext = new DialogAnulacionxTokenViewModel();
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

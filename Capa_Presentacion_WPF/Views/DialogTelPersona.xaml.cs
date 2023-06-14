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

namespace Capa_Presentacion_WPF.Views
{
    /// <summary>
    /// Lógica de interacción para DialogTelPersona.xaml
    /// </summary>
    public partial class DialogTelPersona : UserControl
    {
        public DialogTelPersona()
        {
            InitializeComponent();
            Teclado.punto = true;
            //TecladoNumerico.txtbox = txtUsuario;
            Teclado.txtbox = txtTelPers;
            txtTelPers.Text = Teclado.txtbox.Text;
            this.DataContext = new DialogTelPerViewModel();
            txtTelPers.Focus();
        }

        private void teclado_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Teclado.punto = true;
            //TecladoNumerico.txtbox = txtUsuario;
            Teclado.txtbox = txtTelPers;
        }

        private void TxtTelPers_GotFocus(object sender, RoutedEventArgs e)
        {
            Teclado.txtbox = txtTelPers;
            Teclado._focusedControl = null;
        }
    }
}

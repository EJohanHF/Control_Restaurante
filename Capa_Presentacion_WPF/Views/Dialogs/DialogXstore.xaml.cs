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
    /// Lógica de interacción para DialogXstore.xaml
    /// </summary>
    public partial class DialogXstore : UserControl
    {
        public DialogXstore()
        {
            InitializeComponent();
            this.DataContext = new DialogDescripcionesViewModel();
    
           
        }
        

        private void teclado_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void txtEscritura(object sender, RoutedEventArgs e)
        {
            //TextBox txt = txtEscritura_p;
            Teclado.txtbox = txtEscritura_p;
            //Button folop = txtNumMesa;
            Teclado._focusedControl = null;
        }

    }
}

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
    /// Lógica de interacción para DialogClaveEmpleado.xaml
    /// </summary>
    public partial class DialogClaveEmpleado : UserControl
    {
        public DialogClaveEmpleado()
        {
            InitializeComponent();
            TecladoNumerico.punto = false;
            TecladoNumerico.txtpass = txtPassword;
            this.DataContext = new DialogClaveEmpleadoViewModel();
            txtPassword.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
     
        }

    }
}

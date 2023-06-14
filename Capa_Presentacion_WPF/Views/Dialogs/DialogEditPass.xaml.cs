using Capa_Presentacion_WPF.ViewModels.Configuracion.User;
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
    /// Lógica de interacción para DialogEditPass.xaml
    /// </summary>
    public partial class DialogEditPass : UserControl
    {
        public DialogEditPass()
        {
            InitializeComponent();
            this.DataContext = new UsuarioViewModel();
        }

        private void Binding_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

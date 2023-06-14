using Capa_Presentacion_WPF.ViewModels.Configuracion.Niveles;
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
    /// Lógica de interacción para Nivel.xaml
    /// </summary>
    public partial class Nivel : UserControl
    {
        public Nivel()
        {
            InitializeComponent();
            this.DataContext = new NivelViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtNombreDialog.Focus();
        }
    }
}

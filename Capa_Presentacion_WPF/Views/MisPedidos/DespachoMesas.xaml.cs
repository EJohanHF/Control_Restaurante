using Capa_Presentacion_WPF.ViewModels.MisPedidos;
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
using System.Windows.Threading;

namespace Capa_Presentacion_WPF.Views.MisPedidos
{
    /// <summary>
    /// Lógica de interacción para DespachoMesas.xaml
    /// </summary>
    public partial class DespachoMesas : UserControl
    {
        DispatcherTimer clocke = new DispatcherTimer();
        private double hOff;
        public DespachoMesas()
        {
            InitializeComponent();
            this.DataContext = new DespachoMesasViewModel();
        }
    }
}

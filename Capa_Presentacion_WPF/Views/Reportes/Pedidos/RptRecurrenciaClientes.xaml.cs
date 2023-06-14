using Capa_Presentacion_WPF.ViewModels.Reporte.Pedidos;
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

namespace Capa_Presentacion_WPF.Views.Reportes.Pedidos
{
    /// <summary>
    /// Lógica de interacción para RptRecurrenciaClientes.xaml
    /// </summary>
    public partial class RptRecurrenciaClientes : UserControl
    {
        public RptRecurrenciaClientes()
        {
            InitializeComponent();
            this.DataContext = new RecurrenciaClientesViewModel();
        }
    }
}

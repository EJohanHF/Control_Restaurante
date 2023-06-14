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

namespace Capa_Presentacion_WPF.ViewModels.Reporte.Pedidos
{
    /// <summary>
    /// Lógica de interacción para ReporteCierreCaja.xaml
    /// </summary>
    public partial class ReporteCierreCaja : UserControl
    {
        public ReporteCierreCaja()
        {
            InitializeComponent();
            this.DataContext = new ReporteCierreCajaViewModel();
        }
    }
}

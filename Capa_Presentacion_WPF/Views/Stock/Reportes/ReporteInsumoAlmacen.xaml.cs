using Capa_Presentacion_WPF.ViewModels.Configuracion.Stock.Reporte;
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

namespace Capa_Presentacion_WPF.Views.Stock.Reportes
{
    /// <summary>
    /// Lógica de interacción para ReporteInsumoAlmacen.xaml
    /// </summary>
    public partial class ReporteInsumoAlmacen : UserControl
    {
        public ReporteInsumoAlmacen()
        {
            InitializeComponent();
            this.DataContext = new MovimientoAlmacenViewModel();
        }
    }
}

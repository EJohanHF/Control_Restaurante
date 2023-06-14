using Capa_Presentacion_WPF.ViewModels.Reporte.Report_BoletayFactura;
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

namespace Capa_Presentacion_WPF.Views.Reportes.Report_BoletayFactura
{
    /// <summary>
    /// Lógica de interacción para RptBoletayFactura.xaml
    /// </summary>
    public partial class RptBoletayFactura : UserControl
    {
        public RptBoletayFactura()
        {
            InitializeComponent();
            this.DataContext = new VM_Rpt_BoletayFactura();
        }
    }
}

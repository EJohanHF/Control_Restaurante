using Capa_Presentacion_WPF.ViewModels.Configuracion.Stock.Consumo_Interno;
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

namespace Capa_Presentacion_WPF.Views.Consumo_Interno
{
    /// <summary>
    /// Lógica de interacción para ConsumoInterno.xaml
    /// </summary>
    public partial class ConsumoInterno : UserControl
    {
        public ConsumoInterno()
        {
            InitializeComponent();
            this.DataContext = new ConsumoInternoViewModel();
        }
    }
}

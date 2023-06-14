using Capa_Presentacion_WPF.ViewModels.Configuracion.Factura_Compra;
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

namespace Capa_Presentacion_WPF.Views.Factura_Compra
{
    /// <summary>
    /// Lógica de interacción para Fact_Compra.xaml
    /// </summary>
    public partial class Fact_Compra : UserControl
    {
        public Fact_Compra()
        {
            InitializeComponent();
            this.DataContext = new FactCompraViewModel();
        }
    }
}

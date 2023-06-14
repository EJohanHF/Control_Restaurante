using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Capa_Presentacion_WPF.Views.Transacciones
{
    /// <summary>
    /// Lógica de interacción para Rpt_Parametros.xaml
    /// </summary>
    public partial class Rpt_Parametros : UserControl
    {
        public Rpt_Parametros()
        {
            InitializeComponent();
            this.DataContext = new Capa_Presentacion_WPF.ViewModels.Configuracion.Transaccion.ParametrosViewModel();
        }
    }
}

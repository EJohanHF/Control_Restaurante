using Capa_Presentacion_WPF.ViewModels.Configuracion.Carta;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Receta;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Stock;
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

namespace Capa_Presentacion_WPF.Views.Receta
{
    /// <summary>
    /// Lógica de interacción para Receta.xaml
    /// </summary>
    public partial class Receta : UserControl
    {
        public Receta()
        {
            InitializeComponent();
            this.DataContext = new RecetaViewModel();
        }
    }
}

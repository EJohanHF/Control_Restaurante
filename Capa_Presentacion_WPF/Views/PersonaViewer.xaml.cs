using Capa_Presentacion_WPF.ViewModels;
using Notifications.Wpf;
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
using System.Windows.Shapes;

namespace Capa_Presentacion_WPF.Views
{
    /// <summary>
    /// Lógica de interacción para PersonaViewer.xaml
    /// </summary>
    public partial class PersonaViewer : Window
    {
        public PersonaViewer()
        {

            InitializeComponent();
            this.DataContext = new MenuStructureViewModel();
        }

    }
}

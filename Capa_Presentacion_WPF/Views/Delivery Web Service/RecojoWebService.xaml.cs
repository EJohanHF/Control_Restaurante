using Capa_Presentacion_WPF.ViewModels.DeliveryWebService;
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

namespace Capa_Presentacion_WPF.Views.Delivery_Web_Service
{
    /// <summary>
    /// Lógica de interacción para RecojoWebService.xaml
    /// </summary>
    public partial class RecojoWebService : UserControl
    {
        public RecojoWebService()
        {
            InitializeComponent();
            this.DataContext = new RecojoWebServiceViewModel();
        }
    }
}

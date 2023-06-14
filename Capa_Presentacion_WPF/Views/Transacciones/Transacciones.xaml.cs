using Capa_Presentacion_WPF.ViewModels.Configuracion.Transaccion;
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

namespace Capa_Presentacion_WPF.Views.Transacciones
{
    /// <summary>
    /// Lógica de interacción para Transacciones.xaml
    /// </summary>
    public partial class Transacciones : UserControl
    {
        public Transacciones()
        {
            InitializeComponent();
            this.DataContext = new TransaccionesViewModel();

            //this.DataContext = CollectionViewSource.GetDefaultView(items);
            //this.DataContext.Filter = w => ((string)w).Contains(SearchTextBox.Text);

            //MyDataGrid.ItemsSource = this.DataContext;
        }

        private void TextBox_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}

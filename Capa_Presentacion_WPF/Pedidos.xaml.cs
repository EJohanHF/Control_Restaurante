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
using System.Data;
using Capa_Entidades.Acceso;
using Capa_Negocio.Acceso;


namespace Capa_Presentacion_WPF
{
    /// <summary>
    /// Lógica de interacción para Pedidos.xaml
    /// </summary>
    public partial class Pedidos : Window
    {
        Neg_Corporacion objNegCor = new Neg_Corporacion();
        Ent_Corporacion objEntCor = new Ent_Corporacion();


        public Pedidos()
        {
            InitializeComponent();
        }

        void ListarCorporacion()
        {
            MenuItem objmenu = new MenuItem();
            objmenu.Header = "_1dad";
            objmenu.Header = "_2dad";
            this.ListMenu.Items.Add(objmenu);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItem objmenu = new MenuItem();
            /*objmenu.Header = "_1dad";
            objmenu.Header = "_2dad";*/
            //ListarCorporacion();
        }

        private class ObservableCollection<T>
        {
            public ObservableCollection()
            {
            }
        }
    }
}

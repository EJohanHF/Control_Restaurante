
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
using MaterialDesignThemes.Wpf;
using Capa_Presentacion_WPF.Util;

namespace Capa_Presentacion_WPF.UserControls
{
    /// <summary>
    /// Lógica de interacción para ucPrueba02.xaml
    /// </summary>
    public partial class ucPrueba02 : UserControl
    {
        private Window parentWindow;
    
        private int offsetX;
        private int offsetY;

        public ucPrueba02()
        {
            InitializeComponent();
        }

        //public ucPrueba02(Window parentWindow, Corner corner, int offsetX, int offsetY)
        //{
        //    this.parentWindow = parentWindow;
        //    this.corner = corner;
        //    this.offsetX = offsetX;
        //    this.offsetY = offsetY;
        //}

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //var x = await Dialog.Mostrar("Titulo de prueba", "Mensaje de prueba");
            //if ( Convert.ToBoolean(x))
            //{
            //    MessageBox.Show("Has pulsado Aceptar", "Prueba", MessageBoxButton.OK, MessageBoxImage.Information);
            //}else
            //{
            //    MessageBox.Show("Has pulsado Cancelar", "Prueba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //}
        }
    }
}

using Capa_Presentacion_WPF.ViewModels.Reporte.ReporteporDias;
using MaterialDesignThemes.Wpf;
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

namespace Capa_Presentacion_WPF.Views.Dialogs.DialogVentaporDia
{
    /// <summary>
    /// Lógica de interacción para DialogAnularPedido.xaml
    /// </summary>
    public partial class DialogAnularPedido : UserControl
    {
        public DialogAnularPedido()
        {
            InitializeComponent();
            TecladoNumerico.punto = false;
            TecladoNumerico.txtpass = txtPassword;
            this.DataContext = new AnularViewModel();
        }

        private void TxtPassword_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        //private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        //{
        //    Application.Current.Properties["AnularPedido"] = null;
        //    DialogHost.CloseDialogCommand.Execute(null, null);
        //}

      
    }
}

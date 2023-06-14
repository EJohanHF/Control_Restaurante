using Capa_Presentacion_WPF.ViewModels.Configuracion.Stock;
using Capa_Presentacion_WPF.ViewModels.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Capa_Presentacion_WPF.Views.Stock
{
    /// <summary>
    /// Lógica de interacción para Insumos.xaml
    /// </summary>
    public partial class Insumos : UserControl
    {
        public Insumos()
        {
            InitializeComponent();
            this.DataContext = new InsumoViewModel();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(txtinsumo.Text))
            //{
                
            //}
            //else
            //    lblinsumo.Visibility = System.Windows.Visibility.Visible;
        }

        private void CboUM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lblum.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CboAlm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lblalm.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtinsumo_KeyUp(object sender, KeyEventArgs e)
        {
            //lblinsumo.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CboCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           //lblcat.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtcantmin_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }

        private void Txtcond_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }
    }
}

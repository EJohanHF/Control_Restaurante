using Capa_Presentacion_WPF.ViewModels.Configuracion.Carta;
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

namespace Capa_Presentacion_WPF.Views.Carta
{
    /// <summary>
    /// Lógica de interacción para Categoria_Carta.xaml
    /// </summary>
    public partial class Categoria_Carta : UserControl
    {
        public Categoria_Carta()
        {
            InitializeComponent();
            this.DataContext = new CategoriaViewModel();


        }



        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void Txtdesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (cbocat.SelectedItem != null)
            {
                if (!string.IsNullOrWhiteSpace(txtcat.Text))
                {
                    if (imglogo.Source != null)
                    {
                        return;
                    }
                    else
                        lbllogo.Visibility = System.Windows.Visibility.Visible;
                }
                else
                    lblestadocat.Visibility = System.Windows.Visibility.Visible;
            }
            else
                lblestadocombo.Visibility = System.Windows.Visibility.Visible;
            
            
        }

        private void Txtcat_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Cbocat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblestadocombo.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtcat_KeyUp(object sender, KeyEventArgs e)
        {
            lblestadocat.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void BtnCargarLogo_Click(object sender, RoutedEventArgs e)
        {
            lbllogo.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}

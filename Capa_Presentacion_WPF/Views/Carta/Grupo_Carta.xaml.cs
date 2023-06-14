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
    /// Lógica de interacción para Grupo_Carta.xaml
    /// </summary>
    public partial class Grupo_Carta : UserControl
    {
        public Grupo_Carta()
        {
            InitializeComponent();
            this.DataContext = new GrupoViewModel();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (cboscat.SelectedItem != null)
            {
                if (cbocat.SelectedItem != null)
                {
                    if (!string.IsNullOrWhiteSpace(txtnom.Text))
                    {
                        if (imgpick.Source != null)
                        {
                            return;
                        }
                        else
                            lblimg.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                        lblestadonomgrupo.Visibility = System.Windows.Visibility.Visible;

                }
                else
                    lblestadocombocat.Visibility = System.Windows.Visibility.Visible;
                

            }
            else lblestadocomboscat.Visibility = System.Windows.Visibility.Visible;
            
        }

        private void Cboscat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblestadocomboscat.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Cbocat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblestadocombocat.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtnom_KeyUp(object sender, KeyEventArgs e)
        {
            //txtnom.Text.TrimStart(null);
            lblestadonomgrupo.Visibility = System.Windows.Visibility.Collapsed ;
        }

        private void BtnCargarLogo_Click(object sender, RoutedEventArgs e)
        {
            lblimg.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}

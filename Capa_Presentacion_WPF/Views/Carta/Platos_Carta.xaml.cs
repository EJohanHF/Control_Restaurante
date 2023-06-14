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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Capa_Presentacion_WPF.Views.Carta
{
    /// <summary>
    /// Lógica de interacción para Platos_Carta.xaml
    /// </summary>
    public partial class Platos_Carta : UserControl
    {
        public Platos_Carta()
        {
            InitializeComponent();
            this.DataContext = new PlatosViewModel();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Txtnom_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void Cbogrupo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             
        }
        private void cbocat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             
        }

        private void Cboscat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (cboscat.SelectedItem != null)
            {
                if (cbocat.SelectedItem != null)
                {
                    if (cbogrupo.SelectedItem != null)
                    {
                        if (!string.IsNullOrWhiteSpace(txtnom.Text))
                        {
                            return;
                        }
                     
                    }
                    

                }
                


            }
          


        }

        private void Cboimpresoras_Checked(object sender, RoutedEventArgs e)
        {
            //CheckBox cbSeleccionado = (CheckBox)sender;
            //bool valor = cbSeleccionado;
            //String id = cbSeleccionado.ID;
            //List<string> checkList = new  CheckBox (x => x.Checked).Select(x => x.id).ToList();
        }

        private void Btneditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.cboClasProdItem.SelectedIndex = 0;
        }
    }
}

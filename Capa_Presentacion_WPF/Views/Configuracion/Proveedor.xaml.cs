using Capa_Presentacion_WPF.ViewModels.Configuracion.Proveeedores;
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

namespace Capa_Presentacion_WPF.Views.Configuracion
{
    /// <summary>
    /// Lógica de interacción para Proveedor.xaml
    /// </summary>
    public partial class Proveedor : UserControl
    {
        public Proveedor()
        {
            InitializeComponent();
            this.DataContext = new ProveedorViewModel();
            //bloqueo(false);
        }

        private void CboTipoDoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTipoDoc.SelectedValue != null)
            {
                //lbltdoc.Visibility = System.Windows.Visibility.Collapsed;
                //lbldis.Visibility = System.Windows.Visibility.Collapsed;
                //lbldir.Visibility = System.Windows.Visibility.Collapsed;
                //lblnom.Visibility = System.Windows.Visibility.Collapsed;
                //lbltdoc.Visibility = System.Windows.Visibility.Collapsed;

                cbxEstado.IsChecked = true;
                bloqueo(true);
                if (cboTipoDoc.SelectedValue is null)
                {
                    cboTipoDoc.SelectedValue = 1;
                }
                if (cboTipoDoc.SelectedValue.ToString() == "1")
                {
                    btnComprobarDoc.IsEnabled = true;
                    ClearText();
                    txtNumDoc.IsEnabled = true;
                    txtNumDoc.Focus();
                    txtNumDoc.MaxLength = 8;
                    //lblestado.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (cboTipoDoc.SelectedValue.ToString() == "2")
                {
                    btnComprobarDoc.IsEnabled = true;
                    ClearText();
                    txtNumDoc.IsEnabled = true;
                    txtNumDoc.Focus();
                    txtNumDoc.MaxLength = 11;
                }
                if (cboTipoDoc.SelectedValue.ToString() == "3")
                {
                    btnComprobarDoc.IsEnabled = false;
                    ClearText();
                    txtNumDoc.IsEnabled = true;
                    txtNumDoc.Focus();
                    txtNumDoc.MaxLength = 12;
                    //lblestado.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (cboTipoDoc.SelectedValue.ToString() == "4")
                {
                    btnComprobarDoc.IsEnabled = false;
                    ClearText();
                    txtNumDoc.IsEnabled = true;
                    txtNumDoc.Focus();
                    txtNumDoc.MaxLength = 12;
                    //lblestado.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (cboTipoDoc.SelectedValue.ToString() == "5")
                {
                    btnComprobarDoc.IsEnabled = false;
                    ClearText();
                    txtNumDoc.IsEnabled = true;
                    txtNumDoc.Focus();
                    txtNumDoc.MaxLength = 15;
                    //lblestado.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
        private void ClearText()
        {
            txtnomcli.Clear();
            txtNumDoc.Clear();
            txtcor.Clear();
            txtdir.Clear();
            txtdist.Clear();
            txttelfinjo.Clear();
            txttelmov.Clear();

        }
        private void bloqueo(bool t)
        {
            txtnomcli.IsEnabled = t;
            txtcor.IsEnabled = t;
            txtNumDoc.IsEnabled = t;
            txtcor.IsEnabled = t;
            txtdir.IsEnabled = t;
            txtdist.IsEnabled = t;
            txttelfinjo.IsEnabled = t;
            txttelmov.IsEnabled = t;

        }
        private void TxtNumDoc_KeyUp(object sender, KeyEventArgs e)
        {
            //lbltdoc.Visibility = System.Windows.Visibility.Collapsed;
        }
        

        private void Txtnom_KeyUp(object sender, KeyEventArgs e)
        {
            //lblndoc.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtdir_KeyUp(object sender, KeyEventArgs e)
        {
            //lbldir.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtdist_KeyUp(object sender, KeyEventArgs e)
        {
            //lbldis.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void BtnComprobarDoc_Click(object sender, RoutedEventArgs e)
        {
              
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //if (cboTipoDoc.SelectedItem!=null)
            //{
            //    if (!string.IsNullOrWhiteSpace(txtNumDoc.Text))
            //    {
            //        if (!string.IsNullOrWhiteSpace(txtnomcli.Text))
            //        {
            //            if (!string.IsNullOrWhiteSpace(txtdir.Text))
            //            {
            //                if (!string.IsNullOrWhiteSpace(txtdist.Text))
            //                {
            //                    return;
            //                }
            //                //else lbldis.Visibility = System.Windows.Visibility.Visible;
            //            }
            //            else lbldir.Visibility = System.Windows.Visibility.Visible;
            //        }
            //        else lblnom.Visibility = System.Windows.Visibility.Visible;
            //    }
            //    else lblndoc.Visibility = System.Windows.Visibility.Visible;
            //}
            //else lbltdoc.Visibility = System.Windows.Visibility.Visible;
        }

        private void TxtNumDoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            //txtNumDoc.MaxLength = 11;
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }

        private void Txttelmov_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }

        private void Txttelfinjo_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

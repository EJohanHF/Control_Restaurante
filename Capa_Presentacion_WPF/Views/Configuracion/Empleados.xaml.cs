using Capa_Presentacion_WPF.Core;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class Empleados : UserControl
    {[DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Descripcion, int Reserved);
        public Empleados()
        {
            InitializeComponent();
            this.DataContext = new EmpleadosViewModel();
            //bloqueo(false);
            //btnComprobarDoc.IsEnabled = false;
            
        }

        private void CboTipoDoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTipoDoc.SelectedItem!=null)
            {
                 lbltdoc.Visibility = System.Windows.Visibility.Collapsed;

                cbxEstado.IsChecked = true;
                bloqueo(true);
                if(cboTipoDoc.SelectedValue  is null)
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
                    lblestado.Visibility = System.Windows.Visibility.Collapsed;
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
                    lblestado.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (cboTipoDoc.SelectedValue.ToString() == "4")
                {
                    btnComprobarDoc.IsEnabled = false;
                    ClearText();
                    txtNumDoc.IsEnabled = true;
                    txtNumDoc.Focus();
                    txtNumDoc.MaxLength = 12;
                    lblestado.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (cboTipoDoc.SelectedValue.ToString() == "5")
                {
                    btnComprobarDoc.IsEnabled = false;
                    ClearText();
                    txtNumDoc.IsEnabled = true;
                    txtNumDoc.Focus();
                    txtNumDoc.MaxLength = 15;
                    lblestado.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            return;
           
        }

        private void TxtNumDoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ClearText()
        {
            txtnomcli.Clear();
            txtapecli.Clear();
            //datepick.DisplayDate = DateTime.Today;
            cboTipoDoc.SelectedItem = "";
            txtNumDoc.Clear();
            F.IsChecked = false; 
            M.IsChecked = false; 
            //cbxEstado.IsChecked = false;
        }
        private void bloqueo(bool t)
        {
            txtnomcli.IsEnabled=t;
            txtapecli.IsEnabled = t;
            txtNumDoc.IsEnabled = t;
            F.IsEnabled = t;
            M.IsEnabled = t;
            cbxEstado.IsEnabled = t;
            datepick.IsEnabled = t;
        }

        private void CboCargo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboCargo.SelectedItem !=null)
            {
                lblcargo.Visibility = System.Windows.Visibility.Collapsed;
                if (cboCargo.SelectedIndex != -1)
                {
                    ClearText();
                }
            }
            return;


        }

        private void TxtNumDoc_KeyUp(object sender, KeyEventArgs e)
        {
            lblndoc.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (cboCargo.SelectedItem != null)
            {
                if (cboTipoDoc.SelectedItem != null)
                {
                    if (!string.IsNullOrWhiteSpace(txtNumDoc.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtnomcli.Text))
                        {
                            if (!string.IsNullOrWhiteSpace(txtapecli.Text))
                            {
                                if (!string.IsNullOrWhiteSpace(txtclave.Text))
                                {
                                    return;
                                }
                                else
                                {
                                    lblclave.Visibility = System.Windows.Visibility.Visible;
                                }
                            }
                            else
                                lblape.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                            lblnom.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                        lblndoc.Visibility = System.Windows.Visibility.Visible;
                }
                else
                    lbltdoc.Visibility = System.Windows.Visibility.Visible;
            }
            else lblcargo.Visibility = System.Windows.Visibility.Visible;
        }

        private void Txtnomcli_KeyUp(object sender, KeyEventArgs e)
        {
            lblnom.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtapecli_KeyUp(object sender, KeyEventArgs e)
        {
            lblape.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            //bloqueo(false);
            //ClearText();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //bloqueo(false);
            //ClearText();
        }

        private void Txtapecli_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }   
}

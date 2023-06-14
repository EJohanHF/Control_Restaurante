using Capa_Presentacion_WPF.ViewModels.Dialogs;
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

namespace Capa_Presentacion_WPF.Views.Dialogs.Pedidos
{
    /// <summary>
    /// Lógica de interacción para DialogCliente.xaml
    /// </summary>
    public partial class DialogCliente : UserControl
    {
        public DialogCliente()
        {
            InitializeComponent();
            this.DataContext = new DialogClienteViewModel();
            //bloqueo(false);
           // btnComprobarDoc.IsEnabled = false;
        }
        private void bloqueo(bool t)
        {
            txtnomcli.IsEnabled = t;
            //txtapecli.IsEnabled = t;
            txtndoc.IsEnabled = t;
            txtcorreo.IsEnabled = t;
            //txtdir.IsEnabled = t;
            txtdistr.IsEnabled = t;
            txtcalle.IsEnabled = t;
            txtref.IsEnabled = t;
            txttel.IsEnabled = t;
        }
        private void ClearText()
        {
            //txtnomcli.Clear();
            //txtapecli.Clear();
            //txtcorreo.Clear();
            //txtdir.Clear();
            //txtdistr.Clear();
            //txtcalle.Clear();
            //txtref.Clear();
            //txtndoc.Clear();
            //txttel.Clear();
        }
        private const char SignoDecimal = '.'; // Carácter separador decimal
        private string _prevTextBoxValue; // Variable que almacena el valor anterior del Textbox
        private void txtndoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (cboTipoDoc.SelectedItem != null)
                {
                    var textBox = (TextBox)sender;
                    // Comprueba si el valor del TextBox se ajusta a un valor válido
                    if (Regex.IsMatch(textBox.Text, @"^(?:\d+\.?\d*)?$"))
                    {
                        // Si es válido se almacena el valor actual en la variable privada
                        _prevTextBoxValue = textBox.Text;
                    }
                    else
                    {
                        // Si no es válido se recupera el valor de la variable privada con el valor anterior
                        // Calcula el nº de caracteres después del cursor para dejar el cursor en la misma posición
                        var charsAfterCursor = textBox.Text.Length - textBox.SelectionStart - textBox.SelectionLength;
                        // Recupera el valor anterior
                        textBox.Text = _prevTextBoxValue;
                        // Posiciona el cursor en la misma posición
                        textBox.SelectionStart = Math.Max(0, textBox.Text.Length - charsAfterCursor);
                    }
                }
            }
            catch (Exception ex)
            {

            }     
        }

        private void cboTipoDoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTipoDoc.SelectedItem != null)
            {
                txtndoc.Text = "0";
                bloqueo(true);
                if (cboTipoDoc.SelectedValue == null)
                {
                    cboTipoDoc.SelectedValue = 5;
                }
                if (cboTipoDoc.SelectedValue.ToString() == "1")
                {
                    btnComprobarDoc.IsEnabled = true;
                    ClearText();
                    txtndoc.IsEnabled = true;
                    txtndoc.Focus();
                    txtndoc.MaxLength = 8;
                }
                if (cboTipoDoc.SelectedValue.ToString() == "2")
                {
                    btnComprobarDoc.IsEnabled = true;
                    ClearText();
                    txtndoc.IsEnabled = true;
                    txtndoc.Focus();
                    txtndoc.MaxLength = 11;
                }
                if (cboTipoDoc.SelectedValue.ToString() == "3")
                {
                    btnComprobarDoc.IsEnabled = false;
                    ClearText();
                    txtndoc.IsEnabled = true;
                    txtndoc.Focus();
                    txtndoc.MaxLength = 12;
                }
                if (cboTipoDoc.SelectedValue.ToString() == "4")
                {
                    btnComprobarDoc.IsEnabled = false;
                    ClearText();
                    txtndoc.IsEnabled = true;
                    txtndoc.Focus();
                    txtndoc.MaxLength = 12;
                }
                if (cboTipoDoc.SelectedValue.ToString() == "5")
                {
                    btnComprobarDoc.IsEnabled = false;
                    ClearText();
                    txtndoc.IsEnabled = true;
                    txtndoc.Focus();
                    txtndoc.MaxLength = 15;
                }
            }
            return;
        }


        //private void Txtndoc_KeyUp(object sender, KeyEventArgs e)
        //{
        //}

        //private void Txttel_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
        //    ((TextBox)sender).Text = s;
        //}

        //private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        //{
        //    //if (cboTipoDoc.SelectedItem != null)
        //    //{
        //    //    if (!string.IsNullOrWhiteSpace(txtndoc.Text))
        //    //    {
        //            if (!string.IsNullOrWhiteSpace(txtnomcli.Text))
        //            {
        //                return;
        //            }

        //    //    }
        //    //    else
        //    //        lblndoc.Visibility = System.Windows.Visibility.Visible;


        //    //}
        //    //else lbltdoc.Visibility = System.Windows.Visibility.Visible;
        //}

        //private void Txtndoc_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (cboTipoDoc.SelectedItem != null)
        //        {
        //            if (cboTipoDoc.SelectedValue.ToString() == "1")
        //            {
        //                string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
        //                ((TextBox)sender).Text = s;

        //            }
        //            if (cboTipoDoc.SelectedValue.ToString() == "2")
        //            {
        //                string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
        //                ((TextBox)sender).Text = s;
        //            }
        //        }
        //        return;

        //    }
        //    catch (Exception)
        //    {
        //        return;
        //    }
        //}

        //private void btnComprobarDoc_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}

using Capa_Presentacion_WPF.ViewModels.Dialogs;
using Capa_Presentacion_WPF.ViewModels.Reporte.ReporteporDias;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Capa_Presentacion_WPF.Views.Dialogs.DialogVentaporDia
{
    /// <summary>
    /// Lógica de interacción para DialogPago.xaml
    /// </summary>
    public partial class DialogPago : UserControl
    {
        public DialogPago()
        {
            InitializeComponent();
            this.DataContext = new DialogPagarViewModel();
        }

        private void TxtPagarcon_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            //((TextBox)sender).Text = s;
            var textBox = (TextBox)sender;
            // Comprueba si el valor del TextBox se ajusta a un valor válido
            if (Regex.IsMatch(textBox.Text, @"^(?:\d+\.?\d*)?$"))
            {
                // Si es válido se almacena el valor actual en la variable privada
                _prevTextBoxValue = textBox.Text;
                if (textBox.Text.Trim().Length > 0)
                {
                    if (Convert.ToDecimal(textBox.Text.Trim()) > Convert.ToDecimal(lblSaldos.Content))
                    {
                        txtVuelto.Text = (Convert.ToDecimal(textBox.Text) - Convert.ToDecimal(lblSaldos.Content)).ToString();
                    }
                    else
                    {
                        txtVuelto.Text = "0";
                    }
                }
                else
                {
                    txtVuelto.Text = "0";
                }
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
        

        //private void Btnsalir_Click(object sender, RoutedEventArgs e)
        //{
        //    DialogHost.CloseDialogCommand.Execute(null, null);
        //}

        private void BtnPagar_Click(object sender, RoutedEventArgs e)
        {
            //DataView dv = DatagridDetpago.ItemsSource as DataView;
            //string tpago = cboTipoPago.SelectedItem.ToString();
            //string tmoney = cboTipoMoneda.SelectedItem.ToString();

            //DataTable dt = new DataTable();
            //DataRow dr = dt.NewRow();
            //dr[0] = "soles";
            //dr[1] = "efectivo";
            //dr[2] = "1.00";
            //dr[3] = txtPagarcon.Text;
            //dt.Rows.Add(dr);
            //DatagridDetpago.ItemsSource = dt.DefaultView;

            //var data = new Test
            //{
            //    Ejemplo1 = "Texto para la columna 1",
            //    Ejemplo2 = "Texto para la columna 2"
            //};

            //DatagridDetpago.Items.Add(data);
        }
        public class Test
        {
            public string Ejemplo1 { get; set; }
            public string Ejemplo2 { get; set; }
        }

        private void Txtamortizar_TextChanged(object sender, TextChangedEventArgs e)
        {
           /* string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["EstPago"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private const char SignoDecimal = '.'; // Carácter separador decimal
        private string _prevTextBoxValue; // Variable que almacena el valor anterior del Textbox
        private void txtPropina_TextChanged(object sender, TextChangedEventArgs e)
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
}
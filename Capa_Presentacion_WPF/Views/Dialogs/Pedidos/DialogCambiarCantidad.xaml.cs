﻿using Capa_Presentacion_WPF.ViewModels.Dialogs;
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
    /// Lógica de interacción para DialogCambiarCantidad.xaml
    /// </summary>
    public partial class DialogCambiarCantidad : UserControl
    {
        public DialogCambiarCantidad()
        {
            InitializeComponent();
            this.DataContext = new DialogCambiarCantidadViewModel();
            TecladoNumerico.punto = true;
            TecladoNumerico.txtbox = txtCantidadNueva;
            txtCantidadNueva.Focus();
        }

        private void teclado_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void txtCantidadNueva_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textb = txtCantidadNueva;
            TecladoNumerico.txtbox = textb;
            TecladoNumerico._focusedControl = (Control)sender;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private const char SignoDecimal = '.'; // Carácter separador decimal
        private string _prevTextBoxValue; // Variable que almacena el valor anterior del Textbox
        private void txtCantidadNueva_TextChanged(object sender, TextChangedEventArgs e)
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

﻿using Capa_Presentacion_WPF.ViewModels.Configuracion;
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

namespace Capa_Presentacion_WPF.Views.Configuracion
{
    /// <summary>
    /// Lógica de interacción para Super_Categoria.xaml
    /// </summary>
    public partial class Descuento : UserControl
    {
        public Descuento()
        {
            InitializeComponent();
            this.DataContext = new DescuentoViewModel();
        }

        private void Txtnom_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {

            

        }

        private void txtporcentaje_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtporcentaje.MaxLength = 11;
            string s = Regex.Replace(((TextBox)sender).Text, @"[^\d.]", "");
            ((TextBox)sender).Text = s;
        }

        private void Txtporcentaje_KeyUp(object sender, KeyEventArgs e)
        {
            
        }
    }
}

﻿using Capa_Presentacion_WPF.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para DialogNomPersona.xaml
    /// </summary>
    public partial class DialogNomPersona : UserControl
    {
        public DialogNomPersona()
        {
            InitializeComponent();
            Teclado.punto = true;
            //TecladoNumerico.txtbox = txtUsuario;
            Teclado.txtbox = txtNomPers;
            txtNomPers.Text = Teclado.txtbox.Text;
            this.DataContext = new DialogNomPerViewModel();
            txtNomPers.Focus();
        }

        private void teclado_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Teclado.punto = true;
            //TecladoNumerico.txtbox = txtUsuario;
            Teclado.txtbox = txtNomPers;
        }

        private void TxtNomPers_GotFocus(object sender, RoutedEventArgs e)
        {
            Teclado.txtbox = txtNomPers;
            Teclado._focusedControl = null;
        }
    }
}

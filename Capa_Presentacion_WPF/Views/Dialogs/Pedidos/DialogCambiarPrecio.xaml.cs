﻿using Capa_Presentacion_WPF.ViewModels.Dialogs;
using MaterialDesignThemes.Wpf;
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
    /// Lógica de interacción para DialogCambiarPrecio.xaml
    /// </summary>
    public partial class DialogCambiarPrecio : UserControl
    {
        public DialogCambiarPrecio()
        {
            InitializeComponent();
            TecladoNumerico.punto = false;
            TecladoNumerico.txtpass = txtPassword;
            this.DataContext = new DialogCambiarPrecioViewModel();
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
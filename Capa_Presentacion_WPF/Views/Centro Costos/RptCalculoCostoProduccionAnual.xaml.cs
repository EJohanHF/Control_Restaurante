﻿using Capa_Presentacion_WPF.ViewModels.Configuracion.Centro_Costos;
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

namespace Capa_Presentacion_WPF.Views.Centro_Costos
{
    /// <summary>
    /// Lógica de interacción para RptCalculoCostoProduccionAnual.xaml
    /// </summary>
    public partial class RptCalculoCostoProduccionAnual : UserControl
    {
        public RptCalculoCostoProduccionAnual()
        {
            InitializeComponent();
            this.DataContext = new RptCalculoCostoProduccionAnualViewModel();
        }
    }
}

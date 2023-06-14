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
using System.Windows.Threading;

namespace Capa_Presentacion_WPF
{
    /// <summary>
    /// Lógica de interacción para ucPrueba1.xaml
    /// </summary>
    public partial class ucPrueba1 : UserControl
    {
        DispatcherTimer clock = new DispatcherTimer();

        public ucPrueba1()
        {
            InitializeComponent();

            clock.Interval = TimeSpan.FromMilliseconds(100);
            clock.Tick += clock_Tick;
            clock.Start();
        }

        void clock_Tick(object sender, EventArgs e)
        {
            double milsec = DateTime.Now.Millisecond;
            double sec = DateTime.Now.Second;
            double min = DateTime.Now.Minute;
            double hr = DateTime.Now.Hour;
            seconds.LayoutTransform = new RotateTransform(((sec / 60) * 360) + ((milsec / 1000) * 6));
            minutes.LayoutTransform = new RotateTransform((min * 360 / 60) + ((sec / 60) * 6));
            hours.LayoutTransform = new RotateTransform((hr * 360 / 12) + (min / 2));

        }
    }
}

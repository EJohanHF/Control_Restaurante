using Capa_Negocio.Parametros;
using Capa_Presentacion_WPF.ViewModels.Ambientes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Capa_Presentacion_WPF.Views.Ambientes
{
    /// <summary>
    /// Lógica de interacción para Mesas.xaml
    /// </summary>
    /// 
    
    public partial class Mesas : UserControl
    {
        DispatcherTimer clocke = new DispatcherTimer();
        Neg_Parametros negpar = new Neg_Parametros();
        private double hOff;
        public Mesas()
        {
            InitializeComponent();
            string nombreicono = negpar.getIconoSistema();
            //string rutaIconoSistema = "../../Resources/" + nombreicono;
            string rutaIconoSistema = nombreicono;
            //SetConsoleIcon(rutaIconoSistema);
            this.DataContext = new MesasViewModel();
            MainWindow.OcultarMenu();

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string nombreicono = negpar.getIconoSistema();
            //string rutaIconoSistema = "../../Resources/" + nombreicono;
            string rutaIconoSistema = nombreicono;
            //SetConsoleIcon(rutaIconoSistema);
        }
        private void rectrechts_MouseEnter(object sender, MouseEventArgs e)
        {
            //scrollviewer1.ScrollToHorizontalOffset(scrollviewer1.HorizontalOffset + 120);
            //scrollviewer1.LineRight();
        }

        private void rectlinks_MouseEnter(object sender, MouseEventArgs e)
        {
            //scrollviewer1.ScrollToHorizontalOffset(scrollviewer1.HorizontalOffset - 120);
            //scrollviewer1.LineLeft();
        }

        private Point scrollMousePoint;

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            //scrollviewer1.ReleaseMouseCapture();
        }

        private void UIElement_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.Source == btn3)
            //{ btn3_Click(sender, e); }

            //scrollMousePoint = e.GetPosition(scrollviewer1);
            //hOff = scrollviewer1.HorizontalOffset;
            //scrollviewer1.CaptureMouse();
        }

        private void UIElement_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            //if (scrollviewer1.IsMouseCaptured)
            //{
            //    scrollviewer1.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(scrollviewer1).X));
            //}
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            //label2.Content = "I AM WORKING";
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            //label2.Content = "Click";
        }
        public static void SetConsoleIcon(string iconFilePath)
        {
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    if (!string.IsNullOrEmpty(iconFilePath))
                    {
                        System.Drawing.Icon icon = new System.Drawing.Icon(iconFilePath);
                        SetWindowIcon(icon);
                    }
                    //SetWindowIcon(Properties.Resources.iconososfood);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public enum WinMessages : uint
        {
            /// <summary>
            /// An application sends the WM_SETICON message to associate a new large or small icon with a window. 
            /// The system displays the large icon in the ALT+TAB dialog box, and the small icon in the window caption. 
            /// </summary>
            SETICON = 0x0080,
        }
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);
        private static void SetWindowIcon(System.Drawing.Icon icon)
        {
            try
            {
                IntPtr mwHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
                IntPtr result01 = SendMessage(mwHandle, (int)WinMessages.SETICON, 0, icon.Handle);
                IntPtr result02 = SendMessage(mwHandle, (int)WinMessages.SETICON, 1, icon.Handle);
            }
            catch (Exception ex)
            {
            }
        }
    }
}

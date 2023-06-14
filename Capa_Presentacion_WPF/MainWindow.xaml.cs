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
using Capa_Negocio.Acceso;
using Capa_Entidades.Acceso;
using Capa_Entidades.Principal;
using System.Windows.Media.Animation;
using MaterialDesignThemes.Wpf;
using Capa_Presentacion_WPF.Views;
using Capa_Presentacion_WPF.ViewModels;
using Capa_Presentacion_WPF.Views.Dialogs;
using Notifications.Wpf;
using System.Collections.ObjectModel;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Configuracion;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using Capa_Negocio.Pedido;
using Capa_Negocio.User;
using Capa_Entidades.Models.Usuario;
using Capa_Negocio.Parametros;

namespace Capa_Presentacion_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ObservableCollection<Empresa> empresa = new ObservableCollection<Empresa>();

        Neg_Empresa negEmpr = new Neg_Empresa();
        Neg_Pedido negPedido = new Neg_Pedido();
        Neg_Usuario negUser = new Neg_Usuario();
        Neg_Parametros negpar = new Neg_Parametros();
        public MainWindow()
        {
            InitializeComponent();
            string nombreicono = negpar.getIconoSistema();
            //string rutaIconoSistema = "../../Resources/" + nombreicono;
            string rutaIconoSistema = nombreicono;
            SetConsoleIcon(rutaIconoSistema);
            
            string currPrsName = Process.GetCurrentProcess().ProcessName;
            Process[] allProcessWithThisName = Process.GetProcessesByName(currPrsName);
            
            if (allProcessWithThisName.Length > 1)
            {
                this.Close();
            }
            else
            {
                empresa = negEmpr.GetEmpresa();
                this.Title = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().empr_nom_com;
                this.DataContext = new MenuStructureViewModel();
                SetConsoleIcon(rutaIconoSistema);
            }
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string nombreicono = negpar.getIconoSistema();
            //string rutaIconoSistema = "../../Resources/" + nombreicono;
            string rutaIconoSistema = nombreicono;
            SetConsoleIcon(rutaIconoSistema);
        }
        #region NomLogin
        private string usuarioini = "";
        public string Usuarioini
        {
            get => usuarioini;

            set
            {
                usuarioini = value;
                txtuser.Text = value;
                //txtuser.Text = usuarioini;
            }

        }
        #endregion
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            mostrarVentanta(menuItem.Tag.ToString());
        }

        //Metodo que muestra una nueva vista (UC) en la ventana
        public void mostrarVentanta(string _ucNombre)
        {
            //UserControl _uc = getUserControl(_ucNombre);
            //if( this.contentGrid.Children.Count > 0)
            //{
            //    if (this.contentGrid.Children[0].GetType().Name.ToString() != _uc.Tag.ToString())
            //    {
            //        this.contentGrid.Children.Clear();
            //        this.contentGrid.Children.Add(_uc);
            //    }
            //}else
            //{
            //    this.contentGrid.Children.Clear();
            //    this.contentGrid.Children.Add(_uc);
            //}

        }
        //Metodo que arma el men[u (Pendiente iconos y otras etiquetas)
        public void buildMenu(List<Ent_MenuItem_Principal> menu)
        {
            //foreach (var item in menu)
            //{
            //    MenuItem _menu = new MenuItem();
            //    _menu.Header = item.nombre;
            //    foreach (var item2 in item.items)
            //    {
            //        MenuItem _menu2 = new MenuItem();
            //        _menu2.Header = item2.nombre;
            //        _menu2.Tag = item2.userControl;
            //        _menu2.Click += MenuItem_Click;
            //        _menu.Items.Add(_menu2);
            //    }
            //    MenuPrincipal.Items.Add(_menu);
            //}
        }
        //Metodo que hace el match de string con uc | el metodo debe ir en una clase en la carpeta util
        //Cada UC que se crea debe ser agregado aqui, el string es el campo userControl en las tabla menus en la BD, 
        //El match es con la etiqueta Tag del UC
        public UserControl getUserControl(string _strUC)
        {
            if (_strUC == "ucPrueba02")
            {
                return new UserControls.ucPrueba02();
            }
            else if (_strUC == "ucPrueba03")
            {
                return new UserControls.ucPrueba03();
            }
            else
            {
                return new ucPrueba1();
            }
        }

        private void MenuToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (Menu.Width == 0)
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }
        }
        public static void OcultarMenu()
        {

        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuToggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void MainSosFood_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
            
        }

        protected override void OnClosed(EventArgs e)
        {
            if (Application.Current.Properties["MesaBloqueada"] != null)
            {
                if (Application.Current.Properties["MesaBloqueada"] == "SI")
                {
                    negPedido.ActualizarMesaLibre(Convert.ToInt32(Application.Current.Properties["CodMesa"]));
                    Application.Current.Properties["MesaBloqueada"] = "NO";
                }
            }
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            //MessageBox.Show("NO HAY CONEXION A CAJA \n EL SISTEMA PROCEDERA A CERRASE AL ACEPTAR ESTE MENSAJE \n ");
            if (Application.Current.Properties["MesaBloqueada"] != null)
            {
                if (Application.Current.Properties["MesaBloqueada"] == "SI")
                {
                    negPedido.ActualizarMesaLibre(Convert.ToInt32(Application.Current.Properties["CodMesa"]));
                    Application.Current.Properties["MesaBloqueada"] = "NO";
                }
            }
            Usuario usuarios = new Usuario();
            usuarios.idusu = Convert.ToInt32(Application.Current.Properties["IdUsuario"]);
            string _mensaje = "";
            negUser.SetUsuarioUpdEstado(1, usuarios, ref _mensaje);
            //base.OnClosing(e);
            e.Cancel = false;
        }
        //protected override void OnSourceInitialized(EventArgs e)
        //{
        //    base.OnSourceInitialized(e);
        //    HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
        //    if (hwndSource != null)
        //    {
        //        hwndSource.AddHook(HwndSourceHook);
        //    }
        //}
        //private bool allowClosing = false;
        //[DllImport("user32.dll")] private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        //[DllImport("user32.dll")] private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        //private const uint MF_BYCOMMAND = 0x00000000; private const uint MF_GRAYED = 0x00000001;
        //private const uint SC_CLOSE = 0xF060;
        //private const int WM_SHOWWINDOW = 0x00000018;
        //private const int WM_CLOSE = 0x10;
        //private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    switch (msg)
        //    {
        //        case WM_SHOWWINDOW:
        //            {
        //                IntPtr hMenu = GetSystemMenu(hwnd, false);
        //                if (hMenu != IntPtr.Zero)
        //                {
        //                    EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
        //                }
        //            }
        //            break;
        //        case WM_CLOSE:
        //            if (!allowClosing)
        //            {
        //                //handled = true;
        //            }
        //            break;
        //    }
        //    return IntPtr.Zero;
        //}

        private void CloseMain_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["MesaBloqueada"] != null)
            {
                if (Application.Current.Properties["MesaBloqueada"] == "SI")
                {
                    negPedido.ActualizarMesaLibre(Convert.ToInt32(Application.Current.Properties["CodMesa"]));
                    Application.Current.Properties["MesaBloqueada"] = "NO";
                }
            }
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        private void CloseMain2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseMain_Click_1(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["MesaBloqueada"] != null)
            {
                if (Application.Current.Properties["MesaBloqueada"] == "SI")
                {
                    negPedido.ActualizarMesaLibre(Convert.ToInt32(Application.Current.Properties["CodMesa"]));
                    Application.Current.Properties["MesaBloqueada"] = "NO";
                }
            }
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        private void CloseMain_Click_2(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["MesaBloqueada"] != null)
            {
                if (Application.Current.Properties["MesaBloqueada"] == "SI")
                {
                    negPedido.ActualizarMesaLibre(Convert.ToInt32(Application.Current.Properties["CodMesa"]));
                    Application.Current.Properties["MesaBloqueada"] = "NO";
                }
            }
            base.OnClosed(e);
            Application.Current.Shutdown();
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

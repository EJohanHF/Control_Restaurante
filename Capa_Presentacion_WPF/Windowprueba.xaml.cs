using Capa_Entidades.Acceso;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Sostic;
using Capa_Entidades.Models.Web_Service;
using Capa_Negocio.Acceso;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Sostic;
using Capa_Negocio.User;
using Capa_Presentacion_WPF.ViewModels;
using Capa_Presentacion_WPF.ViewModels.Configuracion.User;
using Capa_Presentacion_WPF.ViewModels.Dialogs;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Capa_Presentacion_WPF
{
    /// <summary>
    /// Lógica de interacción para Windowprueba.xaml
    /// </summary>
    public partial class Windowprueba : Window
    {
        public ObservableCollection<Empresa> DataEmpresa { get; set; }
        Neg_Empresa negEmp = new Neg_Empresa();
        ActivacionLicencia empresa = new ActivacionLicencia();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Parametros negpar = new Neg_Parametros();
        public Windowprueba()
        {
            InitializeComponent();
            //PruebaM5();
            string currPrsName = Process.GetCurrentProcess().ProcessName;
            Process[] allProcessWithThisName = Process.GetProcessesByName(currPrsName);

            string nombreicono = negpar.getIconoSistema();
            //string rutaIconoSistema = "../../Resources/" + nombreicono;
            string rutaIconoSistema = nombreicono;
            SetConsoleIcon(rutaIconoSistema);

            
            if (allProcessWithThisName.Length > 1)
            {
                // MessageBox.Show("SOS-FOOD Ya está abierto en su computadora");
                //Window window = null;

                //Control currentControl = System.Windows.Input.Keyboard.FocusedElement as Control;

                //if (currentControl != null)
                //    window = Window.GetWindow(currentControl);
                //window.Close();
                this.Close();
            }
            else {
                string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
                if (Modulo == "3")
                {
                    
                    DataTable usuarioactivo = new DataTable();
                    usuarioactivo = negUser.GetUsuarioActivo();
                    if (usuarioactivo != null)
                    {
                        if (usuarioactivo.Rows.Count <= 0)
                        {
                            CajaLogueo();
                        }
                    }
                    //Timer timer7 = new Timer(10000);
                    //timer7.AutoReset = true;
                    //timer7.Elapsed += new System.Timers.ElapsedEventHandler(ValidarSesionCaja);
                    //timer7.Start();

                    
                    SetConsoleIcon(rutaIconoSistema);

                    Application.Current.Properties["IdEmpleadoUsuario"] = null;
                    //negFuncionGlobal.DesBloquearMesaWeb("NO");
                    MainWindow Main = new MainWindow();
                    Main.Show();
                    this.Close();
                }
                else if (Modulo == "2")
                {
                    MainWindow Main = new MainWindow();
                    Main.Show();
                    this.Close();
                }
                else if (Modulo == "1")
                {
                    //negFuncionGlobal.DesBloquearMesaWeb("NO");
                    info_sostic();
                    ValidarLicencia();
                    this.DataContext = new LoginViewModel();
                    passwordBox.Focus();
                }
            }
            Window_Loaded(null, null);
        }
        private void ValidarSesionCaja(object sender, System.Timers.ElapsedEventArgs e)
        {
            DataTable usuarioactivo = new DataTable();
            usuarioactivo = negUser.GetUsuarioActivo();
            if (usuarioactivo != null)
            {
                if (usuarioactivo.Rows.Count <= 0)
                {
                    //var Dialog2 = new MessageDialog
                    //{
                    //    Mensaje = { Text = "NO HAY CONEXION A CAJA \n EL SISTEMA PROCEDERA A CERRASE AL ACEPTAR ESTE MENSAJE" }
                    //};
                    //DialogHost.Show(Dialog2, "RootDialog");
                    //MessageBoxDialog frm = new MessageBoxDialog();
                    //frm.ShowDialog();
                    //MessageBox.Show("NO HAY CONEXION A CAJA \n EL SISTEMA PROCEDERA A CERRASE AL ACEPTAR ESTE MENSAJE","Sin Conexión");
                    //this.Dispatcher.Invoke(() =>
                    //{
                    //    Application.Current.Shutdown();
                    //});
                    //base.OnClosed(e);
                   
                }
            }
        }
        private async void CajaLogueo()
        {
            MessageBox.Show("No hay ningun usuario logueado en caja.");
            
            //if (!(bool)x)
            //    return;
            Application.Current.Shutdown();
        }
        static List<Sos_Tic> sostic = new List<Sos_Tic>();
        Neg_SosTic negSosTic = new Neg_SosTic();
        public async void ValidarLicencia()
        {

            DateTime FechaVencimiento;
            int DiasVence;
            int DiasVence2;
            bool Activo;
            string strFV;
            DataEmpresa = negEmp.GetEmpresa();
            string codigo = DataEmpresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().EMPR_FV_LINCENCIA;
            string base64Encoded = codigo.Trim();
            string base64Decoded;
            byte[] data;
            data = System.Convert.FromBase64String(base64Encoded);
            base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);

            strFV = base64Decoded.Substring(base64Decoded.IndexOf("-")+1 ,base64Decoded.Length - base64Decoded.IndexOf("-")-1);

            FechaVencimiento = Convert.ToDateTime(strFV);
            //DiasVence = System.Convert.ToInt32(DateDiff("D", DateTime.Now, FechaVencimiento));
            DiasVence = (FechaVencimiento -DateTime.Now).Days;
            DiasVence2 = DateTime.Compare(FechaVencimiento,DateTime.Now.Date);
            
            if (DiasVence2 != 0 && DiasVence2 != -1)
            {
                DiasVence = DiasVence + 1;
            }
            sostic = negSosTic.GetSostic();
            if (DiasVence <= 5 && DiasVence != 0 && DiasVence2 != -1)
            {
                Activo = true;
                //var view = new MessageDialog
                //{
                //    Mensaje = { Text = "LE QUEDAN " + DiasVence.ToString() + " DIA(S) DE USO\n"+
                //                        "DEL SERVICIO DE SISTEMA O DE\n"+
                //                        "FACTURACIÓN ELECTRÓNICA"}
                //};
                //var x = await DialogHost.Show(view, "RootDialog");
                MessageBox.Show("LE QUEDAN " + DiasVence.ToString() + " DIA(S) DE USO\n" +
                                        "DEL SERVICIO DE SISTEMA O DE \n"+
                                        "FACTURACIÓN ELECTRÓNICA -  \n" +
                                        "COMUNIQUESE CON EL PROVEEDOR AL :\n"+
                                        "Cel. : " + sostic.FirstOrDefault().TELEFONO2 + "\n" +
                                        "Tel. : " + sostic.FirstOrDefault().TELEFONO1 + "\n" +
                                        "Correo : " + sostic.FirstOrDefault().CORREO1, "SOS-FOOD - !!! Advertencia");
            }
            else if(DiasVence == 0 && DiasVence2 != -1)
            {
                Activo = true;
                MessageBox.Show("HOY EL ULTIMO DIA(S) DE USO \n" +
                                        "DEL SERVICIO DE SISTEMA O DE \n" +
                                        "FACTURACIÓN ELECTRÓNICA -  \n" +
                                        "COMUNIQUESE CON EL PROVEEDOR AL :\n" +
                                        "Cel. : " + sostic.FirstOrDefault().TELEFONO2 + "\n" +
                                        "Tel. : " + sostic.FirstOrDefault().TELEFONO1 + "\n" +
                                        "Correo : " + sostic.FirstOrDefault().CORREO1, "SOS-FOOD - !!! Advertencia");
            }
            else if(DiasVence < 0)
            {
                Activo = false;
                MessageBox.Show("LA LICENCIA DE USO HA VENCIDO\n" +
                                        "DEL SERVICIO DE SISTEMA O DE \n" +
                                        "FACTURACIÓN ELECTRÓNICA -  \n" +
                                        "COMUNIQUESE CON EL PROVEEDOR AL :\n" +
                                        "Cel. : " + sostic.FirstOrDefault().TELEFONO2 + "\n" +
                                        "Tel. : " + sostic.FirstOrDefault().TELEFONO1 + "\n" +
                                        "Correo : " + sostic.FirstOrDefault().CORREO1, "SOS-FOOD - !!! Advertencia");
            }
            else
            {
                Activo = true;
            }

            if (Activo != true)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Application.Current.Shutdown();
                });
            }
        }
        static string key { get; set; } = "";
        public void PruebaM5()
        {
            var text = "2021-10-02";
            Console.WriteLine(text);

            var cipher = CreateMD5(text);
            Console.WriteLine(cipher);

            text = Decrypt(cipher);
            Console.WriteLine(text);

        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }
        public static string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
        public DataTable info_sostic()
        {
            string codigo = "";
            DataTable dt = new DataTable();
            DataEmpresa = negEmp.GetEmpresa();
            string codigo_cliente = DataEmpresa.Where(e=>e.EMPR_DEFAULT == "1").FirstOrDefault().codigo;
            try
            {
                WebRequest request = WebRequest.Create("http://api.sos-food.com/cliente/" + codigo_cliente); // "http://" & My.Settings.url_webservices & "/see/server/reporte/resumenPorSerie?fechaInicio=" & Format(Today.Date, "yyyy-MM-dd") & "&fechaFin=" & Format(Today.Date, "yyyy-MM-dd") & "")
                WebResponse response;
                // Dim credentials As String = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(My.Settings.up_fe))
                // request.Headers.Add("Authorization", "Basic " + credentials)
                request.Method = WebRequestMethods.Http.Get;
                response = request.GetResponse();
                // Dim reporte As New ClsInfoFE
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string strjson = reader.ReadToEnd();
                    // strjson = Replace(strjson, """single"":", """csingle"":")
                    empresa = JsonConvert.DeserializeObject<ActivacionLicencia>(strjson);
                }
                if (empresa.cambio == 1)
                {
                    //string text;
                    //text = Decrypt(empresa.codigo);
                    bool r = negEmp.ActualizarLicencia(empresa.codigo);
                    if (r == true)
                    {
                        WebRequest rpta = WebRequest.Create("http://api.sos-food.com/constatarcambio/" + codigo_cliente);
                        rpta.Method = WebRequestMethods.Http.Get;
                        WebResponse res;
                        res = rpta.GetResponse();   
                    }
                }
            }
            // DT = reporte.body.csingle.detalle
            catch (Exception ex)
            {
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", "Error: " + ex.Message, 3);
            }
            return dt;
        }
        private void TxtUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            TecladoNumerico.txtbox = txt;
            TecladoNumerico._focusedControl = null;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string nombreicono = negpar.getIconoSistema();
            //string rutaIconoSistema = "../../Resources/" + nombreicono;
            string rutaIconoSistema = nombreicono;
            SetConsoleIcon(rutaIconoSistema);


            TecladoNumerico.punto = false;

            //TecladoNumerico.txtbox = txtUsuario;
            TecladoNumerico.txtpass = passwordBox;
        }
        public int intentos;
        public string iplocal;
        public void Logueo()
        {
            Neg_Login neg_log = new Neg_Login();
            Funcion_Global negFuncionGlobal = new Funcion_Global();
            string mensaje = "";
            //if (intentos <= 3)
            //{

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {

                    //ip.ToString();

                }
                iplocal = ip.ToString();
            }
            string nomequipo = System.Net.Dns.GetHostName();
            //Ent_Usuario ent_usu = neg_log.login(cboUsuario.Text, passwordBox.Password, ref mensaje);
            Ent_Usuario ent_usu = neg_log.login2(cboUsuario.SelectedValue.ToString(), passwordBox.Password, nomequipo, iplocal, ref mensaje);

            if (mensaje != "")
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", mensaje, 2);
            }
            else
            {
                if (ent_usu == null)
                {
                    intentos += 1;
                    if (intentos == 4)
                    {
                        this.Close();
                    }
                    lblestado.Visibility = System.Windows.Visibility.Visible;
                    //MessageBox.Show("Usuario o Contraseña incorrecta , te quedan  " + (4 - intentos) + " intentos", "Acceso al Sistema", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    Boolean comparar = new Boolean();
                    comparar = BCrypt.Net.BCrypt.Verify(passwordBox.Password, ent_usu.usu_pass);

                    if (comparar == true)
                    {
                        Application.Current.Properties["NomUsuario"] = ent_usu.usu_nom;
                        Application.Current.Properties["IdUsuario"] = ent_usu.id;
                        Application.Current.Properties["IdRol"] = ent_usu.idrol;
                        Application.Current.Properties["IdEmpleadoUsuario"] = ent_usu.id_empl;
                        neg_log.ActualizarEstadoLogueado(ent_usu.id.ToString());
                        MainWindow Main = new MainWindow();
                        Main.Show();
                        this.Close();
                    }
                    else
                    {
                        lblestado.Visibility = System.Windows.Visibility.Visible;
                    }
                    //MessageBox.Show("Bienvenido " + ent_usu.usu_nom, "Acceso alSistema", MessageBoxButton.OK, Image.Information);
                    //// Corporacion vv = new Corporacion();
                    //MenuStructureViewModel vm = new MenuStructureViewModel();
                    //vm.NomUser= ent_usu.usu_nom;
                }
            }
        }
        Neg_Usuario negUser = new Neg_Usuario();
        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Logueo();
        }
        
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passw = (PasswordBox)sender;
            TecladoNumerico.txtpass = passw;
            TecladoNumerico._focusedControl = (Control)sender;
            //lblestado.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            lblestado.Visibility = System.Windows.Visibility.Collapsed;

            if (e.Key == Key.Enter)
                Logueo();
        }
        private void teclado_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void teclado_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
        private void cboUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                Application.Current.Shutdown();
            });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }
        public static void SetConsoleIcon(string iconFilePath)
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
            IntPtr mwHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            IntPtr result01 = SendMessage(mwHandle, (int)WinMessages.SETICON, 0, icon.Handle);
            IntPtr result02 = SendMessage(mwHandle, (int)WinMessages.SETICON, 1, icon.Handle);
        }// SetWindowIcon()
    }
}

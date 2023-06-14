using Capa_Entidades;
using Capa_Entidades.Acceso;
using Capa_Negocio;
using Capa_Negocio.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ServiceProcess;
using System.Configuration;
using Capa_Entidades.Models.Sostic;
using Capa_Negocio.Sostic;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.User
{
   public class LoginViewModel :IGeneric
    {
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Login neg_log = new Neg_Login();
        //Neg_Login neg
        public List<Ent_Combo> ComboUsuarios { get; set; }
        public Ent_Combo _username;

        
        public string pass { get; set; }

        public ICommand LogueoCommand { get; set; }

        public Byte[] LogoSosFood { get; set; }
        public string Modulo { get; set; }

        public string caja { get; set; }
        public string ppedido { get; set; }
        static List<Sos_Tic> sostic2 = new List<Sos_Tic>();
        Neg_SosTic sostic = new Neg_SosTic();


        public LoginViewModel()
        {
            //ServiceController sc = new ServiceController();
            //sc.ServiceName = "facturador-local";

            //if (sc.Status == ServiceControllerStatus.Running ||
            //    sc.Status == ServiceControllerStatus.StartPending)
            //{
            //    MessageBox.Show("Service is already running");
            //}
            //else
            //{
            //    try
            //    {
            //        sc.Start();
            //        MessageBox.Show("Start pending... ");
            //        sc.Start();
            //        sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 10));

            //        if (sc.Status == ServiceControllerStatus.Running)
            //        {
            //            MessageBox.Show("Service started successfully.");
            //        }
            //        else
            //        {
            //            MessageBox.Show("Service not started.");
            //            MessageBox.Show("  Current State: {0}", sc.Status.ToString("f"));
            //        }
            //    }
            //    catch (InvalidOperationException)
            //    {
            //        MessageBox.Show("Could not start the service.");
            //    }
            //}


            //this.LogueoCommand = new RelayCommand(new Action(Loguear));

            //
            sostic2 = sostic.GetSostic();
            var logo_ = sostic2.First();
            Byte[] logo = logo_.LOGO_SOSFOOD;
            this.LogoSosFood = logo;
            //
            this.ComboUsuarios = negCombo.GetUsuarios();
            Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            if(Modulo == "1")
            {
                caja = "Visible";
                ppedido = "Collapsed";
            }
            else
            {
                caja = "Collapsed";
                ppedido = "Visible";
            }
        }


        public int intentos;
        //public void Loguear()
        //{
        //    string mensaje = "";
        //    //if (intentos <= 3)
        //    //{


        //    Ent_Usuario ent_usu = neg_log.login(username, pass, ref mensaje);

        //    if (mensaje != "" && intentos > 3)
        //    {
        //        // MessageBox.Show(mensaje, "Acceso al Sistema", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    else
        //    {
        //        if (ent_usu == null)
        //        {
        //            intentos += 1;
        //            if (intentos == 4)
        //            {
        //                MainWindow Main = new MainWindow();
        //                Main.Close();
        //            }
        //            //lblestado.Visibility = System.Windows.Visibility.Visible;
        //            //MessageBox.Show("Usuario o Contraseña incorrecta , te quedan  " + (4 - intentos) + " intentos", "Acceso al Sistema", MessageBoxButton.OK, MessageBoxImage.Error);

        //        }
        //        else
        //        {
        //            MainWindow Main = new MainWindow();
        //            Main.Show();

        //            Main.Close();
        //        }
        //    }

        //}
        private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}

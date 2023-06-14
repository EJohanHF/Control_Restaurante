using Capa_Presentacion_WPF.ViewModels.Configuracion.Carta;
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
using System.Management;
using System.Windows.Interactivity;
using System.Drawing.Printing;
using System.Threading;
using Microsoft.Win32;
using Capa_Negocio.Funciones_Globales;

namespace Capa_Presentacion_WPF.Views.Carta
{
    /// <summary>
    /// Lógica de interacción para Impresora_Carta.xaml
    /// </summary>
    public partial class Impresora_Carta : UserControl
    {
        public Impresora_Carta()
        {
            InitializeComponent();
            this.DataContext = new ImpresoraViewModel();

            //foreach (string prtn in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            //{
            //    cboImpresoras.Items.Add(prtn);
            //}
           
            
        }
       
    //private void BtnComprobarImpresora_Click(object sender, RoutedEventArgs e)
    //    {
    //        try
    //        {
    //            PrintDocument pd = new PrintDocument();

    //            string s_Default_Printer = pd.PrinterSettings.PrinterName;
    //            txtestado.Text = EstaEnLineaLaImpresora(cboImpresoras.Text).ToString();
    //            if (Convert.ToBoolean(txtestado.Text) == true)
    //            {
    //                txtestado.Text = "Activo";
    //            }
    //            else
    //            {
    //                txtestado.Text = "No Activo";
    //            }
    //            string printerName = Convert.ToString(cboImpresoras.SelectedItem);
    //            string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", printerName);
    //            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
    //            ManagementObjectCollection coll = searcher.Get();

    //            foreach (ManagementObject printer in coll)
    //            {
    //                string portName = printer["PortName"].ToString();
    //                txtip.Text = portName;
    //                if (portName.StartsWith("IP_"))
    //                {
    //                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", string.Format("Printer IP Address: {0}", portName.Substring(3)), 2);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            negFuncionGlobal.Mensaje("SOS-FOOD - Error",ex.Message, 3);
    //        }
            
    //    }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        private void portName(string printerPortName)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Control\Print\Monitors\Standard TCP/IP Port\Ports\" + printerPortName, RegistryKeyPermissionCheck.Default, System.Security.AccessControl.RegistryRights.QueryValues);
            if (key != null)
            {
                String IP = (String)key.GetValue("IPAddress", String.Empty, RegistryValueOptions.DoNotExpandEnvironmentNames);
            }

        }

        public bool EstaEnLineaLaImpresora(string printerName)
        {
            string str = "";
            bool online = false;

            ManagementScope scope = new ManagementScope(ManagementPath.DefaultPath);

            scope.Connect();

            //Consulta para obtener las impresoras, en la API Win32
            SelectQuery query = new SelectQuery("select * from Win32_Printer");

            ManagementClass m = new ManagementClass("Win32_Printer");

            ManagementObjectSearcher obj = new ManagementObjectSearcher(scope, query);

            //Obtenemos cada instancia del objeto ManagementObjectSearcher
            using (ManagementObjectCollection printers = m.GetInstances())
                foreach (ManagementObject printer in printers)
                {
                    if (printer != null)
                    {
                        //Obtenemos la primera impresora en el bucle
                        str = printer["Name"].ToString().ToLower();
                        if (str.Equals(printerName.ToLower()))
                        {

                            //Una vez encontrada verificamos en estado de ésta
                            if (printer["WorkOffline"].ToString().ToLower().Equals("true") || printer["PrinterStatus"].Equals(7))
                                //Fuera de línea
                                online = false;
                            else
                                //En línea
                                online = true;
                        }
                    }
                    else
                        throw new Exception("No fueron encontradas impresoras instaladas en el equipo");
                }
            return online;
        }

        private void CboImpresoras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblimpresora.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtip_KeyUp(object sender, KeyEventArgs e)
        {
            lblip.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Txtequipo_KeyUp(object sender, KeyEventArgs e)
        {
            lblequipo.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void BtGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (cboImpresoras.SelectedItem != null)
            {
                if (txtip.Text != string.Empty)
                {
                    if (!string.IsNullOrWhiteSpace(txtequipo.Text))
                    {
                        return;
                    }
                    else
                        lblequipo.Visibility = System.Windows.Visibility.Visible;
                }
                else
                    lblip.Visibility = System.Windows.Visibility.Visible;
            }
            else lblimpresora.Visibility = System.Windows.Visibility.Visible;
        }

        //private void btnprueba(object sender, RoutedEventArgs e)
        //{
        //    portName(txtip.Tex)
        //}
    }
}

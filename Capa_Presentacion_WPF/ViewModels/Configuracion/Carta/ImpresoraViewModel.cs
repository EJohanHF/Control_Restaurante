using Capa_Entidades.Models.Carta;
using Capa_Negocio.Carta;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Management;
using System.Drawing.Printing;
using System.Threading;
using Microsoft.Win32;
using Capa_Negocio.Funciones_Globales;
using Capa_Entidades;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Carta
{
    public class ImpresoraViewModel : IGeneric
    {
        Neg_Impresora negImp = new Neg_Impresora();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        private Impresora impresora;
        private string operacion;
        public ObservableCollection<Impresora> DataImpresora { get; set; }
        public List<Ent_Combo> comboImpresora  { get; set; }
        public Impresora Impresora
        {
            get => impresora;
            set
            {
                impresora = value;
            }
        }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand BuscarImpresoraCommand { get; set; }
        public string com_nomimp { get; set; }
        public string impre_esatdo;
        public string impre_ip;
        //private Impresora _impreselected;
        public string NombreImp { get; set; }
        public string Impre_IP 
        {
            get => impre_ip;
            set
            {
                if (value != null)
                {
                    this.Impresora.ipimp = value;
                }
                impre_ip = value;
            }
        }
        public string Impre_Estado 
        {
            get => impre_esatdo;
            set
            {
                if (value != null)
                {
                    this.Impresora.estadoimp = value;
                }
                impre_esatdo = value;
            }
        }
        public string Operacion
        {
            get => operacion;
            set
            {
                if (value == "Lista")
                    GetLista();
                operacion = value;
            }
        }
        public ImpresoraViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.BuscarImpresoraCommand = new RelayCommand(new Action(BuscarImpresora));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.comboImpresora = negImp.GetListaImpresoras();


        }
        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.Impresora = this.DataImpresora.Where(w => w.idimp == (int)id).FirstOrDefault();
            this.Impre_IP = this.Impresora.ipimp;
            this.Impre_Estado = this.Impresora.estadoimp;
        }
        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            this.Operacion = "Nuevo";
            this.Impresora = new Impresora();
            this.Impre_IP = "";
            this.Impre_Estado = "";
        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(Impresora.nomimp) && !string.IsNullOrWhiteSpace(Impresora.estadoimp) && !string.IsNullOrWhiteSpace(Impresora.ipimp) && !string.IsNullOrWhiteSpace(Impresora.nomequipoimp))
            {
                Impresora impresora = this.Impresora;
                var _id = impresora.idimp;
                string _mensaje = "";
                bool result = negImp.SetImpresora((_id == 0 ? 1 : 2), impresora, ref _mensaje);
                if (!result)
                {
                    var view = new MessageDialog
                    {
                        Mensaje = { Text = _mensaje }
                    };
                    await DialogHost.Show(view, "RootDialog");
                }
                if (result)
                {
                    this.Operacion = "Lista";
                }
            }return;
            
        }
        private async void Eliminar(object id)
        {
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "Deseas eliminar el registro ?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
                return;
            string _mensaje = "";
            Impresora impresora = new Impresora();
            impresora.idimp = (int)id;
            bool result = negImp.SetImpresora(3, impresora, ref _mensaje);
            if (!result)
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = _mensaje }
                };
                await DialogHost.Show(view, "RootDialog");
            }
            if (result)
            {
                GetLista();
            }
        }
        private void GetLista()
        {
            DataImpresora = negImp.GetImpre();
        }
         private void BuscarImpresora()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                string s_Default_Printer = pd.PrinterSettings.PrinterName;
                NombreImp = EstaEnLineaLaImpresora(Impresora.nomimp).ToString();
                if (Convert.ToBoolean(NombreImp) == true)
                {
                    Impre_Estado = "Activo";
                }
                else
                {
                    Impre_Estado = "No Activo";
                }
                string printerName = Convert.ToString(com_nomimp);
                string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", printerName);
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                ManagementObjectCollection coll = searcher.Get();

                foreach (ManagementObject printer in coll)
                {
                    string portName = printer["PortName"].ToString();
                    Impre_IP = portName;
                    if (portName.StartsWith("IP_"))
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", string.Format("Printer IP Address: {0}", portName.Substring(3)), 2);
                    }
                }
            }
            catch (Exception ex)
            {
               // negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
        }
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




    }
}

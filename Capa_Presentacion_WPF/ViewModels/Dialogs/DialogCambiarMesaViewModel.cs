using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Delivery_Web_Service;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Pedido;
using Capa_Presentacion_WPF.ViewModels.Ambientes;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogCambiarMesaViewModel : IGeneric
    {
        private AmbientesItem _Ambientes;
        public AmbientesItem Ambientes
        {
            get => _Ambientes;
            set
            {
                _Ambientes = value;
            }
        }
        public string txtMesa { get; set; }
        public string NomAmb { get; set; }
        public string TituloDialogCambiarUnificar {get;set;}
        public string BotonEnabled { get; set; }
        AmbientesStructure directoryStructure = new AmbientesStructure();
        public ObservableCollection<AmbientesItemViewModel> DataAmbientes { get; set; }
        //private AmbientesItemViewModel _yourSelectedItem { get; set; }
        private AmbientesItemViewModel _yourSelectedItem;
        Neg_Pedido negPedido = new Neg_Pedido();
        public ICommand CerrarDialog { get; set; }
        public ICommand CambMesa { get; set; }
        public DialogCambiarMesaViewModel()
        {
            BotonEnabled = "True";
            this.TituloDialogCambiarUnificar = Application.Current.Properties["TituloDialogCambiarUnificarMesa"].ToString();
            this.CerrarDialog = new RelayCommand(new Action(CerrarDialogo));
            //this.CambMesa = new RelayCommand(new Action(CambiarMesa));
            this.CambMesa = new ParamCommand(new Action<object>(CambiarMesa));
            var children = directoryStructure.GetLogicalDrives();
            this.DataAmbientes = new ObservableCollection<AmbientesItemViewModel>(
                children.Select(d => new AmbientesItemViewModel(d.ID, d.A_NOM, d.A_X, d.A_Y, d.A_WIDTH, d.A_HEIGHT, d.A_TEXTO, d.A_ACT, d.A_F_CREATE)));
            //this.NomAmb = "HOLA";
            this.txtMesa = "";
        }
        public void CerrarDialogo()
        {
            Application.Current.Properties["CambioMesa"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public bool ValidarInternet()
        {
            bool internet = false;
            System.Uri Url = new System.Uri("https://www.google.com/");

            System.Net.WebRequest WebRequest;
            WebRequest = System.Net.WebRequest.Create(Url);
            System.Net.WebResponse objetoResp;

            try
            {
                objetoResp = WebRequest.GetResponse();
                objetoResp.Close();
                internet = true;

            }
            catch (Exception e)
            {
                //Estado = "No se pudo conectar a Internet " + e.Message;
                //Estado = "Sin conexion a internet \n Prueba a: \n " +
                //    "• Comprobar los cables de red, el módem y el router \n" +
                //    "• Volver a conectarte a una red Wi-Fi \n"+ e.Message;

                //Estado = "Por el momento no cuenta con internet. \n Por favor ingrese manualmente los datos de su cliente.";
                internet = false;
                //string Estado = "Estimado Cliente:\n" +
                //    "actualmente usted no cuenta con conexión a internet por favor registre manualmente los datos de su cliente.";
                //Estado = "OR CTMRE NO TIENES INTERNET, REGISTRA TODO CON MANUELA NOMAS PAPI";
                //Mensaje("SOS-FOOD - Informacion", Estado, 2);

            }
            return internet;
        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public void CambiarMesa(object parameter)
        {
            try
            {
                BotonEnabled = "False";
                var textbox = parameter as TextBox;
                var texto = textbox.Text;
                this.txtMesa = texto;
                string NomAmbiente = "";
                string NumeroMes = "";
                string NombreMesa = "";
                NomAmbiente = NomAmb;
                NumeroMes = txtMesa;
                NombreMesa = NomAmbiente + " " + NumeroMes;
                DataTable dt = new DataTable();
                dt = negPedido.GET_MESA_EXISTE(NombreMesa);
                string id_pedido = "0";
                if (Application.Current.Properties["Id_PedidoCambio"] != null)
                {
                    id_pedido =  Application.Current.Properties["Id_PedidoCambio"].ToString();
                }
                if (dt.Rows.Count != 0)
                {
                    if (dt.Rows[0][2].ToString() == "0" && this.TituloDialogCambiarUnificar == "UNIFICAR MESA")
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "La mesa " + NombreMesa + " esta desocupada", 2);
                        Application.Current.Properties["CambioMesa"] = null;
                        BotonEnabled = "True";
                        return;
                    }else if (dt.Rows[0][2].ToString() == "1" && this.TituloDialogCambiarUnificar == "CAMBIAR MESA")
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "La mesa " + NombreMesa + " esta ocupada", 2);
                        Application.Current.Properties["CambioMesa"] = null;
                        BotonEnabled = "True";
                        return;
                    }
                    else if (dt.Rows[0][2].ToString() == "0" && this.TituloDialogCambiarUnificar == "CAMBIAR MESA")
                    {
                        int codMesaAntes = (int)Application.Current.Properties["CodMesaCambio"];
                        int codMesaNuevo = (int)dt.Rows[0][0];
                        int cod_pedido = (int)Application.Current.Properties["Id_PedidoCambio"];
                        string mensaje = "";
                        bool result = negPedido.CambioMesa(1,codMesaAntes, codMesaNuevo, cod_pedido, ref mensaje);
                       
                        if (result)
                        {
                            CambioMesaWebService(id_pedido, codMesaNuevo.ToString());
                            Application.Current.Properties["CambioMesa"] = "1";
                            DialogHost.CloseDialogCommand.Execute(null, null);
                        }
                    }
                    else if (dt.Rows[0][2].ToString() != "0" && this.TituloDialogCambiarUnificar == "UNIFICAR MESA")
                    {
                        int codMesaAntes = (int)Application.Current.Properties["CodMesaCambio"];
                        int codMesaNuevo = (int)dt.Rows[0][0];
                        int cod_pedido = (int)Application.Current.Properties["Id_PedidoCambio"];
                        string mensaje = "";
                        bool result = negPedido.CambioMesa(2,codMesaAntes, codMesaNuevo, cod_pedido, ref mensaje);
                        if (result)
                        {
                            CambioMesaWebService(id_pedido, codMesaNuevo.ToString());

                            Application.Current.Properties["CambioMesa"] = "1";
                            DialogHost.CloseDialogCommand.Execute(null, null);
                        }
                        else {
                            negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", NombreMesa + " No esta disponible \n puede ser que no exista o contenga sub mesas", 2);
                            BotonEnabled = "True";
                        }
                    }
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", NombreMesa + " No esta disponible \n puede ser que no exista o contenga sub mesas", 2);
                    BotonEnabled = "True";
                    Application.Current.Properties["CambioMesa"] = null;
                }
            }
            catch (Exception ex)
            {
                BotonEnabled = "True";
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            
        }
        public ObservableCollection<Empresa> empresa { get; set; }
        Neg_Empresa negEmpresa = new Neg_Empresa();
        Neg_Parametros negParametros = new Neg_Parametros();
        Neg_DeliveryWebService negDeliveryWebService = new Neg_DeliveryWebService();
        public void CambioMesaWebService(string id_pedido,string cod_mesa)
        {
            bool internet = negFuncionGlobal.ValidarInternet();
            if (internet)
                {
                    DataTable dt2 = negPedido.GetDeliveryxPedidoApp(Convert.ToInt32(id_pedido));
                    if (dt2 != null)
                    {
                        if (dt2.Rows.Count > 0)
                        {
                            empresa = negEmpresa.GetEmpresa();
                            int Desc;
                            
                                using (var client = new WebClient())
                                {
                                    var values = new NameValueCollection();
                                    //values["token"] = "app963";
                                    values["orderid"] = dt2.Rows[0]["id"].ToString();
                                    values["id_business"] = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().codigo;
                                    values["cod_mesa"] = cod_mesa;
                                    var response = client.UploadValues(negParametros.CambioMesaWebService(), values);
                                    var responseString = Encoding.Default.GetString(response);
                                }

                        negDeliveryWebService.sp_cambiar_mesa_pedido_delivery(Convert.ToInt32(dt2.Rows[0]["id"]), cod_mesa);
                        bool result4 = negDeliveryWebService.sp_delivery_pedido(Convert.ToInt32(dt2.Rows[0]["id"]), Convert.ToInt32(id_pedido));
                    }
                    }
                }
        }
        public AmbientesItemViewModel YourSelectedItem
        {
            get => _yourSelectedItem;

            set
            {
                _yourSelectedItem = value;
                this.NomAmb = _yourSelectedItem.A_NOM;
            }
        }
       
    }
}
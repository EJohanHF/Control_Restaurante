using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Ambientes;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Delivery_Web_Service;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Pedido;
using Capa_Presentacion_WPF.Views.Dialogs;
using Capa_Presentacion_WPF.Views.Dialogs.Pedidos;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.DeliveryWebService
{
    public class MesaWebServiceViewModel : IGeneric
    {
        public ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService> DateCartaDelivery { get; set; }
        public ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService> DateCartaDeliveryVerificacion { get; set; }
        public ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService> DateCartaDeliveryTotal { get; set; }
        public List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebServiceDetalles> dataDeliveryWebServiceDet { get; set; }
        Neg_DeliveryWebService negDeliveryWebService = new Neg_DeliveryWebService();
        public ICommand AbrirDialogDetPedido { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Cliente negCli = new Neg_Cliente();
        public ObservableCollection<Cliente> DataCliente { get; set; }
        Neg_Pedido negPedido = new Neg_Pedido();
        Neg_Mesa negMesa = new Neg_Mesa();
        public DateTime FechaDesde { get; set; } = DateTime.Now;
        public DateTime FechaHasta { get; set; } = DateTime.Now;
        Neg_Parametros negParametros = new Neg_Parametros();

        public MesaWebServiceViewModel()
        {
            this.DateCartaDelivery = negDeliveryWebService.GetMesaWebService();
            this.DateCartaDeliveryTotal = negDeliveryWebService.GetMesaWebServiceTotal();
            this.AbrirDialogDetPedido = new ParamCommand(new Action<object>(DialogDetPed));
            this.CancelarCommand = new ParamCommand(new Action<object>(RechazarPedido));
            this.GuardarCommand = new ParamCommand(new Action<object>(AceptarPedido));
            this.DataCliente = negCli.GetCliente();
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            Timer timer3 = new Timer(10000);
            timer3.AutoReset = true;
            timer3.Elapsed += new System.Timers.ElapsedEventHandler(timer_elapsed_mesaweb);
            timer3.Start();
        }
        private void timer_elapsed_mesaweb(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.DateCartaDelivery = negDeliveryWebService.GetMesaWebService();
            }
            catch (Exception) { }
        }
        private void Buscar()
        {
            this.DateCartaDelivery = negDeliveryWebService.GetMesaWebServicexFecha(FechaDesde, FechaHasta);
        }
        private async void RechazarPedido(object id)
        {
            try
            {

                bool internet = negFuncionGlobal.ValidarInternet();
                if (internet == false)
                {
                    string Estado = "Estimado Cliente:\n" +
                                    "actualmente usted no cuenta con conexión a internet por favor registre manualmente los datos de su cliente.";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", Estado, 2);
                    return;
                }
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    //values["token"] = "app963";
                    values["orderid"] = id.ToString();
                    values["id_business"] = this.DateCartaDelivery.Where(d => d.cod_orden == Convert.ToInt32(id)).FirstOrDefault().id_business.ToString();
                    values["status"] = "RECHAZADO";
                    var response = client.UploadValues(negParametros.UpdateState(), values);
                    //var responseString = Encoding.Default.GetString(response);
                    //var WebService = new RespuestaWebService();
                    //responseString = responseString.Replace("}", "},");
                    //WebService = JsonConvert.DeserializeObject<RespuestaWebService>(responseString);

                    //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                }
                bool result = negDeliveryWebService.sp_rechazar_pedido(Convert.ToInt32(id));
                this.DateCartaDelivery = negDeliveryWebService.GetMesaWebService();
            }
            catch (Exception ex)
            {

                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
        }
        public string NomTipDoc { get; set; }
        public string NomTipIde { get; set; }
        private async void AceptarPedido(object id)
        {
            try
            {

                bool internet = negFuncionGlobal.ValidarInternet();
                if (internet == false)
                {
                    string Estado = "Estimado Cliente:\n" +
                                    "actualmente usted no cuenta con conexión a internet.";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", Estado, 2);
                    return;
                }
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    //values["token"] = "app963";
                    values["order_id"] = id.ToString();

                    var response = client.UploadValues(negParametros.EstadoPedidoWebService(), values);
                    var responseString = Encoding.Default.GetString(response);
                    var WebService = new List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
                    //responseString = responseString.Replace("}", "},");
                    WebService = JsonConvert.DeserializeObject<List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>>(responseString);

                    if (WebService != null)
                    {
                        if (WebService.Count == 1)
                        {
                            if (WebService.FirstOrDefault().status == "RECHAZADO")
                            {
                                bool result2 = negDeliveryWebService.sp_rechazar_pedido(Convert.ToInt32(id));
                                this.DateCartaDelivery = negDeliveryWebService.GetDeliveryWebService();
                                var mm = new MessageDialog
                                {
                                    Mensaje = { Text = "El Pedido fue RECHAZADO." }
                                };
                                var awaiting = DialogHost.Show(mm, "RootDialog");
                                return;
                            }
                            if (WebService.FirstOrDefault().status == "PEDIDO ANULADO")
                            {
                                bool result2 = negDeliveryWebService.sp_rechazar_pedido(Convert.ToInt32(id));
                                this.DateCartaDelivery = negDeliveryWebService.GetDeliveryWebService();
                                var mm = new MessageDialog
                                {
                                    Mensaje = { Text = "El Pedido fue ANULADO." }
                                };
                                var awaiting = DialogHost.Show(mm, "RootDialog");
                                return;
                            }
                        }
                    }
                }
                //Application.Current.Properties["IdMesaDelivery"] = null;
                //var DialogDelivery = new DialogMesaDelivery
                //{

                //};
                //var x = await DialogHost.Show(DialogDelivery, "RootDialog");
                //if (Application.Current.Properties["IdMesaDelivery"] != null)
                //{
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string cod_mesa = "0";
                string opcion = "I";
                if (this.DateCartaDelivery.Where(d => d.cod_orden == Convert.ToInt32(id)).FirstOrDefault().tipo_sosfood == "I")
                {
                    opcion = "I";
                    string mesa = "";
                    mesa = this.DateCartaDelivery.Where(d => d.cod_orden == Convert.ToInt32(id)).FirstOrDefault().nombre_mesa;
                    DataTable dt2 = new DataTable();
                    dt2 = negMesa.GetMesaDisponible(mesa);

                    if (dt2 == null)
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Le mesa no existe o no esta disponible actualmente", 2);
                        return;
                    }
                    if (dt2.Rows.Count == 0)
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Le mesa no existe o no esta disponible actualmente", 2);
                        return;
                    }
                    if (dt2.Rows[0]["M_EST"].ToString() == "0")
                    {
                        opcion = "I";
                    }
                    else
                    {
                        opcion = "U";
                    }
                    cod_mesa = dt2.Rows[0]["ID"].ToString();
                }
                else
                {
                    if (this.DateCartaDeliveryTotal.Where(d => d.cod_orden == Convert.ToInt32(id) && d.tipo_sosfood == "I" && d.idpedido == 0).Count() == 0)
                    {
                        opcion = "I";
                        string mesa = "";
                        mesa = this.DateCartaDelivery.Where(d => d.cod_orden == Convert.ToInt32(id)).FirstOrDefault().nombre_mesa;
                        DataTable dt2 = new DataTable();
                        dt2 = negMesa.GetMesaDisponible(mesa);

                        if (dt2 == null)
                        {
                            negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Le mesa no existe o no esta disponible actualmente", 2);
                            return;
                        }
                        if (dt2.Rows.Count == 0)
                        {
                            negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Le mesa no existe o no esta disponible actualmente", 2);
                            return;
                        }
                        if (dt2.Rows[0]["M_EST"].ToString() == "0")
                        {
                            opcion = "I";
                        }
                        else
                        {
                            opcion = "U";
                        }
                        cod_mesa = dt2.Rows[0]["ID"].ToString();
                    }
                    else
                    {
                        opcion = "U";
                        string mesa = "";
                        mesa = this.DateCartaDelivery.Where(d => d.cod_orden == Convert.ToInt32(id)).FirstOrDefault().nombre_mesa;
                        //DataTable dt2 = new DataTable();
                        //dt2 = negMesa.GetMesaDisponible(mesa);
                        List<MesasItem> mesas = new List<MesasItem>();
                        mesas = negMesa.GetMesas();
                        cod_mesa = mesas.Where(m => m.ID.ToString().ToUpper() == mesa.ToString().ToUpper()).FirstOrDefault().ID.ToString();
                    }
                }

               


                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    //values["token"] = "app963";
                    values["orderid"] = id.ToString();

                    if (opcion == "I")
                    {
                        var response = client.UploadValues(negParametros.DetPedidos(), values);
                        var responseString = Encoding.Default.GetString(response);
                        var WebService = new List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebServiceDetalles>();
                        //responseString = responseString.Replace("}", "},");
                        WebService = JsonConvert.DeserializeObject<List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebServiceDetalles>>(responseString);

                        //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                        this.dataDeliveryWebServiceDet = WebService;
                    }
                    else
                    {
                        var response = client.UploadValues(negParametros.DetPedidosSosFood(), values);
                        var responseString = Encoding.Default.GetString(response);
                        var WebService = new List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebServiceDetalles>();
                        //responseString = responseString.Replace("}", "},");
                        WebService = JsonConvert.DeserializeObject<List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebServiceDetalles>>(responseString);

                        //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                        this.dataDeliveryWebServiceDet = WebService;
                    }

                }

                string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
                if (Modulo == "3")
                {
                    Application.Current.Properties["IdEmpleadoUsuario"] = null;
                    var Dialog = new DialogClaveEmpleado
                    {

                    };
                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                    if (Application.Current.Properties["IdEmpleado"] == null)
                    {
                        return;
                    }
                }
                this.DateCartaDeliveryVerificacion = negDeliveryWebService.GetMesaWebService();
                if (this.DateCartaDeliveryVerificacion != null)
                {
                    if (this.DateCartaDeliveryVerificacion.Where(c => c.cod_orden == Convert.ToInt32(id)).Count() == 0)
                    {
                        //negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "El Pedido ya fue realizado.", 2);
                        var mm = new MessageDialog
                        {
                            Mensaje = { Text = "El Pedido ya fue realizado." }
                        };
                        var awaiting = DialogHost.Show(mm, "RootDialog");
                        this.DateCartaDelivery = negDeliveryWebService.GetMesaWebService();
                        return;
                    }
                }
                //this.NomTipDoc = this.TipDocElectr.Where(t => t.ID == id).ToList().FirstOrDefault().NOM_TIPO_DOC;

                DataTable dt = new DataTable();

                dt.Columns.Add("DP_ID_CARTA");
                dt.Columns.Add("DP_DESCRIP");
                dt.Columns.Add("DP_CANT");
                dt.Columns.Add("DP_PRE_UNI");
                dt.Columns.Add("DP_DESCU");
                dt.Columns.Add("DP_IMPORT");
                dt.Columns.Add("DP_COMENTARIO");
                dt.Columns.Add("DP_ID_CARTA_SN");
                dt.Columns.Add("EST_ENTREGADO");
                foreach (var item in this.dataDeliveryWebServiceDet.Where(d => d.id_order == id.ToString()))
                {
                    dt.Rows.Add(item.id_carta, item.itemname, item.itemquantity, item.itemprice, 0, item.itemtotal, item.descripcion, 0,0);
                }
                string _mensaje = "";
                string _mensaje2 = "";
                string Nro_Personas = "1";
                int IdCliente = 0;
                string NomCliente = "";
                string TelfCliente = "";
                //Validar Cliente
                //var numdoc = DataCliente.Where(w => w.ndoc == Cliente.ndoc).ToList();
                //if (numdoc.Count == 0)
                //{
                Cliente cliente = new Cliente();
                cliente.denominacion = this.DateCartaDelivery.Where(d => d.cod_orden == Convert.ToInt32(id)).FirstOrDefault().fname;
                cliente.dircli = this.DateCartaDelivery.Where(d => d.cod_orden == Convert.ToInt32(id)).FirstOrDefault().address;
                cliente.telcli = this.DateCartaDelivery.Where(d => d.cod_orden == Convert.ToInt32(id)).FirstOrDefault().mobile;
                cliente.ndoc = this.DateCartaDelivery.Where(d => d.cod_orden == Convert.ToInt32(id)).FirstOrDefault().nro_identidad_cliente;
                cliente.idtd = Convert.ToInt32(this.DateCartaDelivery.Where(d => d.cod_orden == Convert.ToInt32(id)).FirstOrDefault().tipo_doc_identidad);
                cliente.estadocli = 1;

                if (this.DataCliente.Where(c => c.telcli == this.DateCartaDelivery.Where(d => d.cod_orden == Convert.ToInt32(id)).FirstOrDefault().mobile).Count() != 0)
                {
                    cliente.idcli = this.DataCliente.Where(d => d.telcli == this.DateCartaDelivery.Where(de => de.cod_orden == Convert.ToInt32(id)).FirstOrDefault().mobile).FirstOrDefault().idcli;
                    bool result2 = negCli.SetCliente(2, cliente, ref _mensaje2);
                    if (result2)
                    {
                        this.DataCliente = negCli.GetCliente();
                        IdCliente = this.DataCliente.Where(d => d.telcli == this.DateCartaDelivery.Where(de => de.cod_orden == Convert.ToInt32(id)).FirstOrDefault().mobile).FirstOrDefault().idcli;
                        NomCliente = this.DataCliente.Where(d => d.telcli == this.DateCartaDelivery.Where(de => de.cod_orden == Convert.ToInt32(id)).FirstOrDefault().mobile).FirstOrDefault().denominacion;
                        TelfCliente = this.DataCliente.Where(d => d.telcli == this.DateCartaDelivery.Where(de => de.cod_orden == Convert.ToInt32(id)).FirstOrDefault().mobile).FirstOrDefault().telcli;
                    }
                }
                else
                {
                    bool result2 = negCli.SetCliente(1, cliente, ref _mensaje2);
                    if (result2)
                    {
                        this.DataCliente = negCli.GetCliente();
                        IdCliente = this.DataCliente.Where(d => d.telcli == this.DateCartaDelivery.Where(de => de.cod_orden == Convert.ToInt32(id)).FirstOrDefault().mobile).FirstOrDefault().idcli;
                        NomCliente = this.DataCliente.Where(d => d.telcli == this.DateCartaDelivery.Where(de => de.cod_orden == Convert.ToInt32(id)).FirstOrDefault().mobile).FirstOrDefault().denominacion;
                        TelfCliente = this.DataCliente.Where(d => d.telcli == this.DateCartaDelivery.Where(de => de.cod_orden == Convert.ToInt32(id)).FirstOrDefault().mobile).FirstOrDefault().telcli;
                    }
                }
                int result = 0;
                if (Modulo == "3")
                {
                    result = negPedido.SetPedido(Convert.ToInt32(cod_mesa), Convert.ToInt32(Application.Current.Properties["IdUsuario"]), Convert.ToInt32(Application.Current.Properties["IdEmpleado"]), 
                        Convert.ToInt32(IdCliente), dt, opcion, Nro_Personas, 0, NomCliente, TelfCliente, ConfigurationManager.AppSettings["NombreEquipo"].ToString(),"0", ref _mensaje);
                }
                else
                {
                    result = negPedido.SetPedido(Convert.ToInt32(cod_mesa), Convert.ToInt32(Application.Current.Properties["IdUsuario"]), Convert.ToInt32(Application.Current.Properties["IdEmpleadoUsuario"]), 
                        Convert.ToInt32(IdCliente), dt, opcion, Nro_Personas, 0, NomCliente, TelfCliente, ConfigurationManager.AppSettings["NombreEquipo"].ToString(),"0", ref _mensaje);
                }
                

                if (result != 0)
                {
                    bool result4 = negDeliveryWebService.sp_delivery_pedido(Convert.ToInt32(id), result);
                    Imprimir(result);
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        //values["token"] = "app963";
                        values["orderid"] = id.ToString();
                        var response = client.UploadValues(negParametros.UpdateStateSosFood(), values);
                        var responseString = Encoding.Default.GetString(response);



                        //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                    }


                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        //values["token"] = "app963";
                        values["orderid"] = id.ToString();
                        values["id_business"] = this.DateCartaDelivery.Where(d => d.cod_orden == Convert.ToInt32(id)).FirstOrDefault().id_business.ToString();
                        values["status"] = "SU PEDIDO ESTA SIENDO ELABORADO";
                        var response = client.UploadValues(negParametros.UpdateState(), values);
                        var responseString = Encoding.Default.GetString(response);

                        bool result3 = negDeliveryWebService.sp_aceptar_pedido(Convert.ToInt32(id));
                        if (result3)
                        {

                        }



                        //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                    }

                }


                //}

                //else
                //{
                //    string _mensaje1 = "El cliente ya fue registrado ";
                //    var view = new MessageDialog
                //    {
                //        Mensaje = { Text = _mensaje1 }
                //    };
                //    await DialogHost.Show(view, "RootDialog");
                //}


                else
                {
                    return;
                }


                this.DateCartaDelivery = negDeliveryWebService.GetMesaWebService();
            }
            catch (Exception ex)
            {

                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
        }
        private void Imprimir(int cod_pedido)
        {
            DataTable impresora = new DataTable();
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            impresora = negPedido.GetImpresoraxPedido(cod_pedido, Convert.ToInt32(Modulo));
            for (int i = 0; i < impresora.Rows.Count; i++)
            {
                DataTable pedido = new DataTable();
                pedido = negPedido.GET_DETALLE_X_PEDIDO(cod_pedido, Convert.ToInt32(impresora.Rows[i]["IDIMP"].ToString()));
                string cliente = "";
                string descripcion = "";
                string cod_diario = "";
                string nombre_mesa = "";
                string nombre_empleado = "";
                string idpedido = "";
                Ticket ticket = new Ticket();
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    idpedido = pedido.Rows[0]["ID"].ToString().ToUpper();
                    cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComanda(pedido.Rows[j]["DP_CANT"].ToString().ToUpper(), descripcion);
                }

                ticket.AddHeaderLine_2(cliente.ToString());
                ticket.AddSubHeaderLine("COD DIARIO Nº:" + cod_diario.ToUpper());
                ticket.AddSubHeaderLine("PEDIDO Nº: " + idpedido.ToUpper());
                ticket.AddSubHeaderLine("Atendido Por: " + nombre_empleado.ToUpper());
                ticket.AddHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                ticket.AddFooter2Line(nombre_mesa.ToString().ToUpper());
                if (ticket.PrinterExists(impresora.Rows[i]["NOMIMP"].ToString()))
                {
                    ticket.PrintTicket(impresora.Rows[i]["NOMIMP"].ToString());
                }
                else {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[i]["NOMIMP"].ToString() + " no está disponible.", 2);
                }
            }
            negPedido.ActualizarDetPedidoImp(cod_pedido);
        }
        private async void DialogDetPed(object id)
        {

            Application.Current.Properties["cod_orden"] = id;
            var DialogDelivery = new DialogDetPedidoDelivery
            {

            };
            var x = await DialogHost.Show(DialogDelivery, "RootDialog");

        }
    }
}
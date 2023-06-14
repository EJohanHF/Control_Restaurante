using Caliburn.Micro;
using Capa_Datos.Acceso;
using Capa_Entidades;
using Capa_Entidades.Acceso;
using Capa_Entidades.Models;
using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Pedido;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Entidades.Models.Sostic;
using Capa_Entidades.Models.Usuario;
using Capa_Entidades.Models.Web_Service;
using Capa_Negocio.Acceso;
using Capa_Negocio.Ambientes;
using Capa_Negocio.Carta;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Delivery_Web_Service;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Pedido;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Negocio.Sostic;
using Capa_Negocio.User;
using Capa_Negocio.WebService;
using Capa_Presentacion_WPF.Core;
using Capa_Presentacion_WPF.ViewModels.Dialogs;
using Capa_Presentacion_WPF.Views.Ambientes;
using Capa_Presentacion_WPF.Views.Dialogs;
using Capa_Presentacion_WPF.Views.Dialogs.DialogVentaporDia;
using Capa_Presentacion_WPF.Views.Mensaje;
using DocumentFormat.OpenXml.Presentation;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Notifications.Wpf;
using Notifications.Wpf.Caliburn.Micro.Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ThoughtWorks.QRCode.Codec;
using tickets;
using WMPLib;

namespace Capa_Presentacion_WPF.ViewModels
{
    public class MenuStructureViewModel : IGeneric
    {
        #region Public Properties

        public ObservableCollection<MenuItemViewModel> Items { get; set; }
        public ObservableCollection<Usuario> DataUsuario { get; set; }
        public ObservableCollection<Usuario> UsuariosActivos { get; set; }

        public object UserControl { get; set; }
        public string NomUser { get; set; }
        public string IDUser { get; set; }
        public Byte[] LogoSosFood { get; set; }
        private string operacion;
        private Usuario useredit;
        public string TipoModulo { get; set; }
        public string TipoModuloMesa { get; set; }
        public string TipoModuloPedido { get; set; }
        public string NombreImpresora { get; set; }
        private Ent_Usuario usuario;
        public Ent_Usuario Usuario
        {
            get => usuario;
            set
            {
                usuario = value;
            }
        }
        Ent_Usuario _usu = null;

        public Usuario Useredit
        {
            get => useredit;
            set
            {
                useredit = value;
            }
        }
        MenuStructure directoryStructure = new MenuStructure();
        Neg_Usuario negUser = new Neg_Usuario();
        Neg_Login neg_log = new Neg_Login();
        Neg_Empresa negEmpr = new Neg_Empresa();
        Neg_SosTic sostic = new Neg_SosTic();
        Neg_Pedido negPedido = new Neg_Pedido();
        Neg_Parametros negParametros = new Neg_Parametros();
        static List<Sos_Tic> sostic2 = new List<Sos_Tic>();
        static ObservableCollection<Empresa> empresa = new ObservableCollection<Empresa>();
        public ObservableCollection<ConsultaMensaje> DataMensaje { get; set; }
        public Neg_ConsultaMensaje mensaje = new Neg_ConsultaMensaje();
        private ConsultaMensaje mensaje2;
        public ConsultaMensaje Mensaje
        {
            get => mensaje2;
            set
            {
                mensaje2 = value;
            }
        }
        public string Contador { get; set; }
        public string ContadorDelivery { get; set; }
        public string ContadorRecojo { get; set; }
        public string ContadorMesa { get; set; }
        public string ColorMesa { get; set; }
        public string ColorLetraMesa { get; set; }
        public string ColorDelivery { get; set; }
        public string ColorLetraDelivery { get; set; }
        public string ColorRecojo { get; set; }
        public string ColorLetraRecojo { get; set; }
        public String Modulo { get; set; }
        #endregion
        #region Commands
        public ICommand UcsCommand { get; set; }
        public ICommand CloseSesionCommand { get; set; }
        public ICommand EditPassCommand { get; set; }
        public ICommand VerMensaje { get; set; }
        public ICommand DesbloquearMesas { get; set; }
        public ICommand AbrirGavetaCommand { get; set; }
        public ICommand GenerarQR { get; set; }
        public ICommand VerPedidosEntregadosCommand { get; set; }
        public ICommand VerPedidosPendientesCommand { get; set; }
        public ICommand DeliveryComand { get; set; }
        public ICommand RecojoComand { get; set; }
        public ICommand MesaComand { get; set; }
        public ICommand PedidoComand { get; set; }
        public ICommand VerPedidosCocinaCommand { get; set; }
        #endregion

        Neg_Parametros negpar = new Neg_Parametros();
        public MenuStructureViewModel(string vista)
        {
            //if (negFuncionGlobal.MesaWeb() == "NO")
            //{
            //    negFuncionGlobal.DesBloquearMesaWeb("NO");
            //}
            if (Application.Current.Properties["MesaBloqueada"] != null)
            {
                if (Application.Current.Properties["MesaBloqueada"] == "SI")
                {
                    negPedido.ActualizarMesaLibre(Convert.ToInt32(Application.Current.Properties["CodMesa"]));
                    Application.Current.Properties["MesaBloqueada"] = "NO";
                }
            }
            if (vista == "Empleados")
            {
                this.UserControl = new Views.Configuracion.Empleados();
            }
            else if (vista == "Empresa")
            {
                this.UserControl = new Views.Configuracion.Empresa();
            }
            else if (vista == "Corporacion")
            {
                this.UserControl = new Views.Configuracion.Corporacion();
            }
            else if (vista == "Mant_Sup_Cat")
            {
                this.UserControl = new Views.Carta.Super_Categoria();
            }
            else if (vista == "Mant_Cat")
            {
                this.UserControl = new Views.Carta.Categoria_Carta();
            }
            else if (vista == "Mant_Platos")
            {
                this.UserControl = new Views.Carta.Platos_Carta();
            }
            else if (vista == "Usuarios")
            {
                this.UserControl = new Views.User.Usuario_Food();
            }
            else if (vista == "Rol_Cargo")
            {
                this.UserControl = new Views.Configuracion.Rol_Cargo();
            }
            else if (vista == "Roles")
            {
                this.UserControl = new Views.Configuracion.Roles();
            }
            else if (vista == "Ambientes")
            {
                this.UserControl = new Views.Ambientes.Mesas();
            }
            else if (vista == "Receta")
            {
                this.UserControl = new Views.Receta.Receta();
            }
            else if (vista == "Ventas_del_día")
            {
                this.UserControl = new Views.Reportes.VentasdelDia.VentasDia();
            }
            else if (vista == "SubReceta")
            {
                this.UserControl = new Views.Receta.SubReceta();
            }
            else if (vista == "Factura_Compra")
            {
                this.UserControl = new Views.Factura_Compra.Fact_Compra();
            }
            else if (vista == "Metodo_pago")
            {
                this.UserControl = new Views.Configuracion.Super_Jesus();
            }
            else if (vista == "descuento")
            {
                this.UserControl = new Views.Configuracion.Descuento();
            }

            else if (vista == "tipo_mov")
            {
                this.UserControl = new Views.Configuracion.TipoCaja();
            }
            else if (vista == "rows_caja")
            {
                this.UserControl = new Views.Configuracion.Caja();
            }
            else if (vista == "Costos_Fijos")
            {
                this.UserControl = new Views.Centro_Costos.CostosFijos();
            }
            else if (vista == "Costos_Variables")
            {
                this.UserControl = new Views.Centro_Costos.CostosVariables();
            }
            else if (vista == "Mant_Ambientes")
            {
                this.UserControl = new Views.Configuracion.Ambientes();
            }
            else if (vista == "Mant_Mesas")
            {
                this.UserControl = new Views.Configuracion.Mesas();
            }
            else if (vista == "Reporte_General")
            {
                this.UserControl = new Views.Centro_Costos.ReporteGeneral();
            }
            else if (vista == "Nivel")
            {
                this.UserControl = new Views.Configuracion.Nivel();
            }
            else if (vista == "Rpt_InsumoAlmacén")
            {
                this.UserControl = new Views.Stock.Reportes.ReporteInsumoAlmacen();
            }
            else if (vista == "Mov_Almacen")
            {
                this.UserControl = new Views.Stock.MovimientoAlmacen();
            }
            else if (vista == "Historial_anulaciones")
            {
                this.UserControl = new Views.Reportes.Pedidos.RptAnulaciones();
            }
            else if (vista == "Historial Descuento")
            {
                this.UserControl = new Views.Reportes.Pedidos.HistorialDescuento();
            }
            else if (vista == "Historial de Ventas")
            {
                this.UserControl = new Views.Reportes.Pedidos.HistorialdeVentas();
            }
            else if (vista == "RptPrecioCostoPlato")
            {
                this.UserControl = new Views.Centro_Costos.RptPrecioCostoPlato();
            }
            else if (vista == "RptPrecioVentaPlato")
            {
                this.UserControl = new Views.Centro_Costos.RptPrecioVentaPlato();
            }
            else if (vista == "descripciones")
            {
                this.UserControl = new Views.Carta.Descripciones();
            }
            else if (vista == "det_descripciones")
            {
                this.UserControl = new Views.Carta.DetDescripciones();
            }
            else if (vista == "RptBoletayFactura")
            {
                this.UserControl = new Views.Reportes.Report_BoletayFactura.RptBoletayFactura();
            }
            else if (vista == "Historial Venta Mozo")
            {
                this.UserControl = new Views.Reportes.Pedidos.HistorialVentaMozo();
            }
            else if (vista == "TiposCostos")
            {
                this.UserControl = new Views.Centro_Costos.TiposCostos();
            }
            else if (vista == "PuntoEquilibrio")
            {
                this.UserControl = new Views.Centro_Costos.PuntoEquilibrio();
            }
            else if (vista == "RptKardex")
            {
                this.UserControl = new Views.Reportes.Kardex.RptKardex();
            }
            else if (vista == "RptKardexGeneral")
            {
                this.UserControl = new Views.Reportes.Kardex.RptKardexGeneral();
            }
            else if (vista == "RptPlatoMasVendido")
            {
                this.UserControl = new Views.Reportes.PlatoMasVendido.RptPlatoMasVendido();
            }
            else if (vista == "Historial de Caja Chica")
            {
                this.UserControl = new Views.Reportes.Pedidos.HistorialCajaChica();
            }
            else if (vista == "Historial Vent. de Platos x Imp.")
            {
                this.UserControl = new Views.Reportes.Pedidos.HistorialPlatosVendidosxImpresora();
            }
            else if (vista == "RptRecurrenciaClientes")
            {
                this.UserControl = new Views.Reportes.Pedidos.RptRecurrenciaClientes();
            }
            else if (vista == "ConsumoInterno")
            {
                this.UserControl = new Views.Consumo_Interno.ConsumoInterno();
            }
            else if (vista == "IngresarReservasView")
            {
                this.UserControl = new Views.ViewsReservas.IngresarReservasView();
            }
            else if (vista == "RptReservas")
            {
                this.UserControl = new Views.ViewsReservas.RptReservas();
            }
            else if (vista == "Reporte_de_Cierres")
            {
                this.UserControl = new ViewModels.Reporte.Pedidos.ReporteCierreCaja();
            }
            else if (vista == "PagoApp")
            {
                this.UserControl = new Views.Configuracion.MantenimientoAppSosDelivery();
            }
            else if (vista == "EntregaApp")
            {
                this.UserControl = new Views.Configuracion.MantenimientoAppSosDeliveryEntrega();
            }
            else if (vista == "Documentos")
            {
                this.UserControl = new Views.Carta.Documentos();
            }
            else if (vista == "ReporteCierreCaja")
            {
                this.UserControl = new ViewModels.Reporte.Pedidos.ReporteCierreCaja();
            }
            else if (vista == "Rpt_Parametros")
            {
                this.UserControl = new Views.Transacciones.Rpt_Parametros();
            }
            else if (vista == "RptImpuestos")
            {
                ValidarImpuestos();
            }
            else if (vista == "Mermas")
            {
                this.UserControl = new Views.Consumo_Interno.Mermas();
            }
            else if (vista == "RptVentasxCanal")
            {
                this.UserControl = new Views.Reportes.Pedidos.RptVentasxCanal();
            }
            else if (vista == "InformeVentas")
            {
                this.UserControl = new Views.Reportes.Pedidos.InformeVentas();
            }
            else if (vista == "Despacho")
            {
                this.UserControl = new Views.MisPedidos.DespachoMesas();
            }
            else if (vista == "Rpt_Adelanto")
            {
                this.UserControl = new Views.Reportes.Pedidos.Rpt_Adelanto();
            }
            else if (vista == "VentasProducto")
            {
                this.UserControl = new Views.Reportes.Pedidos.VentasProducto();
            }
            else if (vista == "TipoCambio")
            {
                this.UserControl = new Views.Configuracion.TipoCambio();
            }
        }
        public async void ValidarImpuestos()
        {
            Application.Current.Properties["EstPedidoAnulacion"] = "PENDIENTE";
            Application.Current.Properties["OperPagoCel"] = "PARAMETROS";
            Application.Current.Properties["AutorizoCambio"] = null;
            var opencampago = new DialogAnularPedido
            {

            };
            var x3 = await DialogHost.Show(opencampago, "RootDialog");

            if (Application.Current.Properties["AutorizoCambio"] != null)
            {
                this.UserControl = new Views.Transacciones.RptImpuestos();
            }
            else
            {
                return;
            }
        }
        Funcion_Global funcion = new Funcion_Global();
        public void VerRecojo()
        {
            if (Application.Current.Properties["MesaBloqueada"] != null)
            {
                if (Application.Current.Properties["MesaBloqueada"] == "SI")
                {
                    negPedido.ActualizarMesaLibre(Convert.ToInt32(Application.Current.Properties["CodMesa"]));
                    Application.Current.Properties["MesaBloqueada"] = "NO";
                }
            }
            this.UserControl = new Views.Delivery_Web_Service.RecojoWebService();
        }
        public void VerMesa()
        {
            //if (negFuncionGlobal.EstMesaWeb() == true)
            //{
            //    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "El modulo de Pedidos Mesa Web esta siendo utilizada en otro equipo", 2);
            //    return;
            //}
            //if (Modulo == "3")
            //{
            //    negFuncionGlobal.BloquearMesaWeb("SI");
            //}
            //else if(Modulo == "1")
            //{
            //    negFuncionGlobal.BloquearMesaWeb("NO");
            //}
            if (Application.Current.Properties["MesaBloqueada"] != null)
            {
                if (Application.Current.Properties["MesaBloqueada"] == "SI")
                {
                    negPedido.ActualizarMesaLibre(Convert.ToInt32(Application.Current.Properties["CodMesa"]));
                    Application.Current.Properties["MesaBloqueada"] = "NO";
                }
            }
            this.UserControl = new Views.Delivery_Web_Service.MesaWebService();
        }
        public void VerPedido()
        {
            if (Application.Current.Properties["MesaBloqueada"] != null)
            {
                if (Application.Current.Properties["MesaBloqueada"] == "SI")
                {
                    negPedido.ActualizarMesaLibre(Convert.ToInt32(Application.Current.Properties["CodMesa"]));
                    Application.Current.Properties["MesaBloqueada"] = "NO";
                }
            }
            this.UserControl = new Views.Ambientes.Mesas();
        }
        public void VerDelivery()
        {
            if (Application.Current.Properties["MesaBloqueada"] != null)
            {
                if (Application.Current.Properties["MesaBloqueada"] == "SI")
                {
                    negPedido.ActualizarMesaLibre(Convert.ToInt32(Application.Current.Properties["CodMesa"]));
                    Application.Current.Properties["MesaBloqueada"] = "NO";
                }
            }
            this.UserControl = new Views.Delivery_Web_Service.DeliveryWebService();
        }
        #region Constructor
        public MenuStructureViewModel()
        {
            //Timer timer10 = new Timer(100);
            //timer10.AutoReset = true;
            //timer10.Elapsed += new System.Timers.ElapsedEventHandler(VerifyConnection);
            //timer10.StartIcon

            string nombreicono = negpar.getIconoSistema();
            string rutaIconoSistema = nombreicono;
            SetConsoleIcon(rutaIconoSistema);

            double TiempoEnvioDocElectronico = negParametros.TiempoEnvioDocElectronico();
            DataMensaje = mensaje.GeMensaje();
            this.Contador = DataMensaje.Count().ToString();
            this.VerMensaje = new ParamCommand(new Action<object>(MostrarMensaje));
            this.DeliveryComand = new RelayCommand(new Action(VerDelivery));
            this.RecojoComand = new RelayCommand(new Action(VerRecojo));
            this.MesaComand = new RelayCommand(new Action(VerMesa));
            this.PedidoComand = new RelayCommand(new Action(VerPedido));
            this.VerPedidosCocinaCommand = new RelayCommand(new Action(VerPedidosCocina));
            Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            bool internet = negFuncionGlobal.ValidarInternet();
            if (internet)
            {
                ConsumirWebService();
            }
            else {
                sostic2 = sostic.GetSostic();
                var logo_ = sostic2.First();
                Byte[] logo = logo_.LOGO_SOSFOOD;
                this.LogoSosFood = logo;
            }
            if (Modulo == "3")
            {
                TipoModulo = "Collapsed";
                TipoModuloMesa = "Visible";
                TipoModuloPedido = "Visible";
                this.UserControl = new Views.Ambientes.Mesas();
            }
            else if (Modulo == "2")
            {
                TipoModulo = "Collapsed";
                TipoModuloMesa = "Collapsed";
                TipoModuloPedido = "Collapsed";
                NombreImpresora = ConfigurationManager.AppSettings["NombreImpresoraPedido"].ToString();
                this.UserControl = new Views.MisPedidos.MisPedidos();
                this.VerPedidosEntregadosCommand = new RelayCommand(new Action(VerPedidosEntregados));
                this.VerPedidosPendientesCommand = new RelayCommand(new Action(VerPedidosPendientes));
            }
            else if(Modulo == "1")
            {
                TipoModulo = "Visible";
                TipoModuloMesa = "Visible";
                TipoModuloPedido = "Collapsed";
                Messenger.Default.Register<string>(this, GetUserControl);
                Messenger.Default.Register<string>(this, GetUsuario);
                var children = directoryStructure.GetLogicalDrives();
                this.Items = new ObservableCollection<MenuItemViewModel>(
                children.Select(drive => new MenuItemViewModel(drive.idPadre, drive.id, (bool)drive.value, 0)));
                //this.CloseSesionCommand = new RelayCommand(new Action(CerrarSesion));
                this.EditPassCommand = new RelayCommand(new Action(EditarPass));
                this.CloseSesionCommand = new ParamCommand(new Action<object>(CerrarSesion));
                this.NomUser = System.Windows.Application.Current.Properties["NomUsuario"].ToString();
                this.IDUser = Application.Current.Properties["IdUsuario"].ToString();
                this.DesbloquearMesas = new RelayCommand(new Action(DesbMesas));
                this.AbrirGavetaCommand = new RelayCommand(new Action(AbrirGaveta));
                this.GenerarQR = new RelayCommand(new Action(GeneraQR));
                NombreImpresora = ConfigurationManager.AppSettings["NombreImpresoraPedido"].ToString();
                this.VerPedidosEntregadosCommand = new RelayCommand(new Action(VerPedidosEntregados));
                this.VerPedidosPendientesCommand = new RelayCommand(new Action(VerPedidosPendientes));
            }
            if (Modulo == "1")
            {
                internet = negFuncionGlobal.ValidarInternet();
                if (internet)
                {
                    ConsumirWebService();
                    //ConsultarDeliveryWebService();
                    //ConsultarRecojoWebService();
                    //ConsultarMesaWebService();
                }

                Timer timer2 = new Timer(TiempoEnvioDocElectronico);
                timer2.AutoReset = true;
                timer2.Elapsed += new System.Timers.ElapsedEventHandler(timer_elapsed_mensaje);
                timer2.Start();

                //PARA SISTEMA CON TABLETS
                //Timer timer1 = new Timer(5000);
                //timer1.AutoReset = true;
                //timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer_impresora);
                //timer1.Start();

                //Timer timer5 = new Timer(1000);
                //timer5.AutoReset = true;
                //timer5.Elapsed += new System.Timers.ElapsedEventHandler(timer_cuenta);
                //timer5.Start();
                //PARA SISTEMA CON TABLETS

                Timer timer6 = new Timer(60000);
                timer6.AutoReset = true;
                timer6.Elapsed += new System.Timers.ElapsedEventHandler(timer_elapsed_faltantes);
                timer6.Start();
            }
            if (Modulo == "3")
            {
                Timer timer3 = new Timer(15000);
                timer3.AutoReset = true;
                timer3.Elapsed += new System.Timers.ElapsedEventHandler(timer_elapsed_mesaweb);
                timer3.Start();
            }
            Timer timer4 = new Timer(5000);
            timer4.AutoReset = true;
            timer4.Elapsed += new System.Timers.ElapsedEventHandler(timer_mensaje_parpadeante);
            timer4.Start();
        }
        #endregion
        private void ConsultaMensajes()
        {
            var codigo = directoryStructure.ObtenerCodigo();
            var token = directoryStructure.ObtenerToken();
            try
            {
                int Desc;
                string verificar = InternetGetConnectedState(out Desc, 0).ToString();
                bool internet = negFuncionGlobal.ValidarInternet();
                if (internet)
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        values["cod_empresa"] = codigo;
                        values["token"] = token;
                        var response = client.UploadValues(negParametros.RutaServicioWebMensaje(), values);
                        var responseString = Encoding.Default.GetString(response);
                        var WebService = new ConsultaMensaje();
                        WebService = JsonConvert.DeserializeObject<ConsultaMensaje>(responseString);
                        if (WebService == null)
                        {

                        }
                        else
                        {
                            if (responseString != "")
                            {
                                if (WebService.cod_business == codigo && WebService.received == "0")
                                {
                                    bool result = mensaje.SetMensaje(WebService);
                                }
                            }

                        }



                    }

                    DataMensaje = mensaje.GeMensaje();
                    this.Contador = DataMensaje.Count().ToString();
                }
                else
                {
                    DataMensaje = mensaje.GeMensaje();
                    this.Contador = DataMensaje.Count().ToString();
                }
            }
            catch (Exception ex)
            {
                DataMensaje = mensaje.GeMensaje();
                this.Contador = DataMensaje.Count().ToString();
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
        }
        public void ContadorMensajes()
        {
            try
            {
                DataMensaje = mensaje.GeMensaje();
                this.Contador = DataMensaje.Count().ToString();
                if (DataMensaje.Count() > 0)
                {
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "sonido.mp3";
                    WindowsMediaPlayer wplayer = new WindowsMediaPlayer();
                    wplayer.URL = ruta;
                    wplayer.controls.play();
                }
            }
            catch (Exception ex) { }
        }
        private void VerPedidosCocina() { 
            
        }
        private void timer_mensaje_parpadeante(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                this.DateMesa = negDeliveryWebService.GetMesaWebService();
                this.DateRecojo = negDeliveryWebService.GetRecojoWebService();
                this.DateDelivery = negDeliveryWebService.GetDeliveryWebService();
                if (DateMesa != null && DateMesa.Where(d => d.status == "EN PROCESO").Count() > 0)
                {
                    //Storyboard blinkAnimation = Application.Current.TryFindResource("blinkAnimation") as Storyboard;
                    //if (blinkAnimation != null)
                    //{
                    //    blinkAnimation.Begin();
                    //}
                    if (ColorMesa != null)
                    {
                        if (ColorMesa == "Red")
                        {
                            ColorMesa = "White";
                            ColorLetraMesa = "Black";
                        }
                        else if (ColorMesa == "White")
                        {
                            ColorMesa = "Red";
                            ColorLetraMesa = "White";
                        }

                    }
                    else
                    {
                        ColorMesa = "Red";
                        ColorLetraMesa = "White";
                    }

                }
                else
                {
                    ColorMesa = "White";
                    ColorLetraMesa = "Black";
                }
                if (DateRecojo.Where(d => d.status == "EN PROCESO").Count() > 0)
                {
                    //Storyboard blinkAnimation = Application.Current.TryFindResource("blinkAnimation") as Storyboard;
                    //if (blinkAnimation != null)
                    //{
                    //    blinkAnimation.Begin();
                    //}
                    if (ColorRecojo != null)
                    {
                        if (ColorRecojo == "Red")
                        {
                            ColorRecojo = "White";
                            ColorLetraRecojo = "Black";
                        }
                        else if (ColorRecojo == "White")
                        {
                            ColorRecojo = "Red";
                            ColorLetraRecojo = "White";
                        }

                    }
                    else
                    {
                        ColorRecojo = "Red";
                        ColorLetraRecojo = "White";
                    }

                }
                else
                {
                    ColorRecojo = "White";
                    ColorLetraRecojo = "Black";
                }
                if (DateDelivery.Where(d => d.status == "EN PROCESO").Count() > 0)
                {
                    //Storyboard blinkAnimation = Application.Current.TryFindResource("blinkAnimation") as Storyboard;
                    //if (blinkAnimation != null)
                    //{
                    //    blinkAnimation.Begin();
                    //}
                    if (ColorDelivery != null)
                    {
                        if (ColorDelivery == "Red")
                        {
                            ColorDelivery = "White";
                            ColorLetraDelivery = "Black";
                        }
                        else if (ColorDelivery == "White")
                        {
                            ColorDelivery = "Red";
                            ColorLetraDelivery = "White";
                        }

                    }
                    else
                    {
                        ColorDelivery = "Red";
                        ColorLetraDelivery = "White";
                    }

                }
                else
                {
                    ColorDelivery = "White";
                    ColorLetraDelivery = "Black";
                }
                ContadorMensajes();
            }
            catch (Exception)
            {
            }
        }
        private void VerPedidosEntregados()
        {
            this.UserControl = new Views.MisPedidos.Rpt_Pedidos();
        }
        private void VerPedidosPendientes()
        {
            this.UserControl = new Views.MisPedidos.MisPedidos();
        }
        private void timer_impresora(object sender, ElapsedEventArgs e)
        {
            try
            {
                DataTable pedidosimprimprimir = new DataTable();
                pedidosimprimprimir = negPlatos.GetPedidoPendienteImprimir();
                if (pedidosimprimprimir != null)
                {
                    for (int i = 0; i < pedidosimprimprimir.Rows.Count; i++)
                    {
                        Imprimir(Convert.ToInt32(pedidosimprimprimir.Rows[i]["ID"]));
                    }
                }
            }
            catch (Exception ex) {
                var h = "";
            }
        }
        private void timer_cuenta(object sender, ElapsedEventArgs e)
        {
            try
            {
                DataTable pedidosimprimprimir = new DataTable();
                pedidosimprimprimir = negPlatos.GetCuentaPendienteImprimir();
                if (pedidosimprimprimir != null)
                {
                    for (int i = 0; i < pedidosimprimprimir.Rows.Count; i++)
                    {
                        ImprimirCuenta(Convert.ToInt32(pedidosimprimprimir.Rows[i]["idpedido"]));
                    }

                }
            }
            catch (Exception) { }
        }
        Neg_Detalle_pedido negDetVent = new Neg_Detalle_pedido();
        private void ImprimirCuenta(int idpedido)
        {
            DataTable impresora = new DataTable();
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
            impresora = negPlatos.GetImpresoraxDocumento(1); //Precuenta
            for (int i = 0; i < impresora.Rows.Count; i++)
            {
                DataTable dt = new DataTable();
                dt = negDetVent.GetCuentaPedido(idpedido);
                Ticket ticket = new Ticket();

                System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["EMPR_LOGO"]));

                ticket.MargenLogo = negParametros.margenLogoTicket();
                ticket.MaxCharDescrip = negParametros.margenMaxDescrip();
                ticket.HeaderImage = x;
                //ticket.AddHeaderLine(dt.Rows[0]["EMPR_NOM_COM"].ToString());
                //ticket.AddHeaderLine("");
                //ticket.AddHeaderLine("RUC: " + dt.Rows[0]["EMPR_RUC"].ToString());
                //ticket.AddHeaderLine(dt.Rows[0]["EMPR_NOM"].ToString());
                //ticket.AddHeaderLine(dt.Rows[0]["EMPR_URB"].ToString());
                //ticket.AddHeaderLine(dt.Rows[0]["nomdist"].ToString());
                //ticket.AddHeaderLine("Telef.: " + dt.Rows[0]["EMPR_TEL"].ToString());
                //ticket.AddTitleLine("");
                ticket.AddSubHeaderLine("NRO. Orden: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                ticket.AddSubHeaderLine("Atendido Por: " + dt.Rows[0]["EMPL_NOM"].ToString());
                ticket.AddSubHeaderLine("Mesa: " + dt.Rows[0]["M_NOM"].ToString());
                ticket.AddSubHeaderLine("");

                if (dt.Rows[0]["M_NOM"].ToString().Contains("LLEVAR") || dt.Rows[0]["M_NOM"].ToString().Contains("RECOJO"))
                {
                    ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["nomllevar"].ToString());
                    ticket.AddSubHeaderLine("Telefono : " + dt.Rows[0]["telefcli"].ToString());
                }
                else
                {
                    ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["C_NOM"].ToString());
                }


                ticket.AddSubHeaderLine("Nro Documento: " + dt.Rows[0]["C_NRO_DOC"].ToString());
                if (dt.Rows[0]["M_NOM"].ToString().Contains("DELIVERY"))
                {
                    ticket.AddSubHeaderLine("Direccion: " + dt.Rows[0]["C_DIREC"].ToString());
                    ticket.AddSubHeaderLine("Telefono: " + dt.Rows[0]["C_TEL"].ToString());
                }

                ticket.AddSubHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    //ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                    if (dt.Rows[j]["DP_COMENTARIO"].ToString().Trim() == "")
                    {
                        ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                    }
                    else
                    {
                        ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + "c/ " + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                    }
                }
                ticket.AddTotal("SUB TOTAL", dt.Rows[0]["PED_SUBTOTAL"].ToString());
                ticket.AddTotal("DESC.", dt.Rows[0]["PED_DESCUENTO"].ToString());
                ticket.AddTotal("", "---------");
                ticket.AddTotal("TOTAL", dt.Rows[0]["PED_TOTAL"].ToString());
                ticket.AddFooterLine("");
                ticket.AddTotal("", "---------");

                ticket.AddFooterLine("");
                ticket.AddFooterLine("Este documento no es comprobante de pago.");
                if (ticket.PrinterExists(ImpCaja))
                {
                    ticket.PrintTicket(ImpCaja);
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                }
                negPedido.ActualizarCuentaPedidoImp(idpedido);
            }
        }
        private void Imprimir(int cod_pedido)
        {
            string clienteCom = "";
            string cod_diarioCom = "";
            string nombre_mesaCom = "";
            string nombre_empleadoCom = "";

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
                string telefono = "";
                Ticket ticket = new Ticket();
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    idpedido = pedido.Rows[0]["ID"].ToString().ToUpper();
                    if (pedido.Rows[j]["M_NOM"].ToString().ToUpper().Contains("LLEVAR") || pedido.Rows[j]["M_NOM"].ToString().ToUpper().Contains("RECOJO"))
                    {
                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                        clienteCom = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    else
                    {
                        cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                        clienteCom = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                    }
                    telefono = pedido.Rows[0]["C_TEL"].ToString().ToUpper();
                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComanda(pedido.Rows[j]["DP_CANT"].ToString().ToUpper(), descripcion);

                    cod_diarioCom = pedido.Rows[0]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesaCom = pedido.Rows[0]["M_NOM"].ToString().ToUpper();
                    nombre_empleadoCom = pedido.Rows[0]["EMPL_NOM"].ToString().ToUpper();
                }
                //ticket.AddHeaderLine_2(cliente.ToString());

                //tickrnoet.AddFooter2Line(nombre_mesa.ToString().ToUpper());
                ticket.AddSubHeaderLine("COD DIARIO Nº:" + cod_diario.ToUpper());
                ticket.AddSubHeaderLine("PEDIDO Nº: " + idpedido.ToUpper());
                ticket.AddSubHeaderLine("Atendido Por: " + nombre_empleado.ToUpper());
                ticket.AddHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                // ticket.AddFooter2Line("Mozo: " + nombre_empleado.ToUpper());
                ticket.AddFooter2Line("");
                ticket.AddFooter2Line(nombre_mesa.ToString().ToUpper());

                /*
                //RECUTACU
                ticket.AddFooter2Line("");
                ticket.AddFooter2Line("Atendido por: ");
                ticket.AddFooter2Line("");
                ticket.AddFooter2Line(nombre_empleado.ToUpper());
                */
                if (cliente.ToString().Trim() != "")
                {
                    ticket.AddFooter2Line("");
                    ticket.AddFooter2Line(cliente.ToString().ToUpper());
                }
                if (telefono.ToString().Trim() != "")
                {
                    ticket.AddFooter2Line("");
                    ticket.AddFooter2Line(telefono.ToString().ToUpper());
                }

                if (ticket.PrinterExists(impresora.Rows[i]["NOMIMP"].ToString()))
                {
                    ticket.PrintTicket(impresora.Rows[i]["NOMIMP"].ToString());
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[i]["NOMIMP"].ToString() + " no está disponible.", 2);
                }
            }
            negPedido.ActualizarDetPedidoImp(cod_pedido);
        }
        private async void MostrarMensaje(object id)
        {

            Application.Current.Properties["IdMensaje"] = id;
            var mensaje2 = new Mensaje
            {

            };
            var x = await DialogHost.Show(mensaje2, "RootDialog");
            DataMensaje = mensaje.GeMensaje();
            this.Contador = DataMensaje.Count().ToString();
        }
        private async void DesbMesas()
        {
            var mensaje2 = new SiNoMessageDialog
            {
                Mensaje = { Text = "Al ACEPTAR se habilitarán las mesas que estén siento manipuladas o " +
                "atendidas en este momento. Es posible que la mesa esté abierta en un Punto de Pedido. " +
                "¿Desea desbloquear todas las mesas?"}
            };
            var x = await DialogHost.Show(mensaje2, "RootDialog");
            if ((bool)x)
            {
                funcion.DesbloquearMesas();
            }
        }
        private void AbrirGaveta() {
            string s;
            System.Windows.Forms.PrintDialog pd = new System.Windows.Forms.PrintDialog();
            //s = Strings.Chr(27) + Strings.Chr(112) + Strings.Chr(0) + Strings.Chr(50) + Strings.Chr(250);
            s = "" + (char)27 + (char)112 + (char)0 + (char)50 + (char)250;
            pd.PrinterSettings = new PrinterSettings();
            string impresora = ConfigurationManager.AppSettings["ImpCaja"].ToString();  // nombre de la impresora 
            RawPrinterHelper.SendStringToPrinter(impresora, s);
        }
        Neg_Platos negPlatos = new Neg_Platos();
        private void GeneraQR()
        {
            DataTable impresora = new DataTable();
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            impresora = negPlatos.GetImpresoraxDocumento(1); //Precuenta
            int qrBackColor = Color.FromArgb(255, 255, 255, 255).ToArgb();
            int qrForeColor = Color.FromArgb(255, 0, 0, 0).ToArgb();
            Ticket ticket = new Ticket();
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

            qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
            // qrCodeEncoder.QRCodeEncodeMode = Codec.QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC
            qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M;
            qrCodeEncoder.QRCodeScale = Convert.ToInt32(4);
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor);
            qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor);

            string textoQR = "";
            textoQR = "https://play.google.com/store/apps/details?id=com.sostic.developer.sosdelivery";
            // textoQR &= dt(0)("razonSocialReceptor").ToString & "|"
            //textoQR += dt(0)("hash").ToString;

            Bitmap bitmap = qrCodeEncoder.Encode(textoQR, System.Text.Encoding.UTF8);
            ticket.QrImage = bitmap;
            if (Modulo == "1")
            {
                if (ticket.PrinterExists(impresora.Rows[0]["NOMIMP"].ToString()))
                {
                    ticket.PrintTicket(impresora.Rows[0]["NOMIMP"].ToString());
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[0]["NOMIMP"].ToString() + " no está disponible.", 2);
                }
            }
            else if (Modulo == "3")
            {
                if (ticket.PrinterExists(impresora.Rows[0]["NOMIMPPPEDIDO"].ToString()))
                {
                    ticket.PrintTicket(impresora.Rows[0]["NOMIMPPPEDIDO"].ToString());
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[0]["NOMIMPPPEDIDO"].ToString() + " no está disponible.", 2);
                }
            }
        }
        private void timer_elapsed_mensaje(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                int Desc;
                string verificar = InternetGetConnectedState(out Desc, 0).ToString();
                bool internet = negFuncionGlobal.ValidarInternet();
                if (internet)
                {
                    EnviarDocEletronico();
                    ConsultarDeliveryWebService();
                    ConsultarRecojoWebService();
                    ConsultarMesaWebService();
                    DataReporteWebService();
                    PlatosWebService();
                    ConsultaMensajes();
                }
                else
                {
                    sostic2 = sostic.GetSostic();
                    var logo_ = sostic2.First();
                    Byte[] logo = logo_.LOGO_SOSFOOD;
                    this.LogoSosFood = logo;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void PlatosWebService()
        {
            bool internet = negFuncionGlobal.ValidarInternet();
            if (internet)
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string codigo_cliente = directoryStructure.ObtenerCodigo();
                try
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        //values["token"] = "app963";
                        values["id_business"] = codigo_cliente;
                        var response = client.UploadValues(negParametros.PlatosWebServiceUpdate(), values);
                        var responseString = Encoding.Default.GetString(response);
                        //bool envio = negCierre.sp_actualizar_envio(CabeceraBoleta.Rows[0]["id_doc_electronico"].ToString());
                        var WebService = new List<PlatoUpdateWebService>();
                        WebService = JsonConvert.DeserializeObject<List<PlatoUpdateWebService>>(responseString);
                        if (WebService != null)
                        {
                            foreach (var item in WebService)
                            {
                                bool envio = negCierre.sp_actualizar_plato_web_service(item.id_carta,item.nom_carta,item.precio_carta);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
                }
            }
        }
        private void timer_elapsed_mesaweb(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                int Desc;
                string verificar = InternetGetConnectedState(out Desc, 0).ToString();
                bool internet = negFuncionGlobal.ValidarInternet();
                if (internet)
                {
                    ConsultarMesaWebService();
                }
                else
                {
                    sostic2 = sostic.GetSostic();
                    var logo_ = sostic2.First();
                    Byte[] logo = logo_.LOGO_SOSFOOD;
                    this.LogoSosFood = logo;
                }
                ConsultarUsuarioActivoCaja();
            }
            catch (Exception)
            {
            }
        }
        private void timer_elapsed_faltantes(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                bool internet = negFuncionGlobal.ValidarInternet();
                if (internet)
                {
                    VerificarDocFaltantes();
                }
            }
            catch (Exception)
            {
            }
        }
        public void EnviarDocEletronico()
        {
            try
            {
                DataTable CabeceraBoleta = new DataTable();
                CabeceraBoleta = negCierre.GetCabeceraBoleta();
                //bool internet = negFuncionGlobal.ValidarInternet();
                //if (internet)
                //{
                    if (CabeceraBoleta.Rows.Count > 0)
                    {
                        using (var client = new WebClient())
                        {
                            var values = new NameValueCollection();
                            values["id_doc_electronico"] = CabeceraBoleta.Rows[0]["id_doc_electronico"].ToString();
                            values["serieNumero"] = CabeceraBoleta.Rows[0]["serieNumero"].ToString();
                            values["tipoDocumento"] = CabeceraBoleta.Rows[0]["tipoDocumento"].ToString();
                            values["fechaEmision"] = CabeceraBoleta.Rows[0]["fechaEmision"].ToString();
                            values["numeroDocIdentidadEmisor"] = CabeceraBoleta.Rows[0]["numeroDocIdentidadEmisor"].ToString();
                            values["tipoDocIdentidadEmisor"] = CabeceraBoleta.Rows[0]["tipoDocIdentidadEmisor"].ToString();
                            values["numeroDocIdentidadReceptor"] = CabeceraBoleta.Rows[0]["numeroDocIdentidadReceptor"].ToString();
                            values["razonSocialReceptor"] = CabeceraBoleta.Rows[0]["razonSocialReceptor"].ToString();
                            values["direccionReceptor"] = CabeceraBoleta.Rows[0]["direccionReceptor"].ToString();
                            values["correoReceptor"] = CabeceraBoleta.Rows[0]["correoReceptor"].ToString();
                            values["tipoDocIdentidadReceptor"] = CabeceraBoleta.Rows[0]["tipoDocIdentidadReceptor"].ToString();
                            values["telefono"] = CabeceraBoleta.Rows[0]["telefono"].ToString();
                            values["idCliente"] = CabeceraBoleta.Rows[0]["idCliente"].ToString();
                            values["totalOPGravadas"] = CabeceraBoleta.Rows[0]["totalOPGravadas"].ToString();
                            values["totalOPExoneradas"] = CabeceraBoleta.Rows[0]["totalOPExoneradas"].ToString();
                            values["totalOPNoGravadas"] = CabeceraBoleta.Rows[0]["totalOPNoGravadas"].ToString();
                            values["totalOPGratuitas"] = CabeceraBoleta.Rows[0]["totalOPGratuitas"].ToString();
                            values["sumatoriaIGV"] = CabeceraBoleta.Rows[0]["sumatoriaIGV"].ToString();
                            values["sumatoriaISC"] = CabeceraBoleta.Rows[0]["sumatoriaISC"].ToString();
                            values["importeTotal"] = CabeceraBoleta.Rows[0]["importeTotal"].ToString();
                            values["importeTotalVenta"] = CabeceraBoleta.Rows[0]["importeTotalVenta"].ToString();
                            values["totalDescuentos"] = CabeceraBoleta.Rows[0]["totalDescuentos"].ToString();
                            values["descuentosGlobales"] = CabeceraBoleta.Rows[0]["descuentosGlobales"].ToString();
                            values["sumatoriaOtrosCargos"] = CabeceraBoleta.Rows[0]["sumatoriaOtrosCargos"].ToString();
                            values["porcentajeOtrosCargos"] = CabeceraBoleta.Rows[0]["porcentajeOtrosCargos"].ToString();
                            values["sumatoriaImpuestoBolsas"] = CabeceraBoleta.Rows[0]["sumatoriaImpuestoBolsas"].ToString();
                            values["tipoMoneda"] = CabeceraBoleta.Rows[0]["tipoMoneda"].ToString();
                            values["codigoPaisReceptor"] = CabeceraBoleta.Rows[0]["codigoPaisReceptor"].ToString();
                            values["codigoTipoOperacion"] = CabeceraBoleta.Rows[0]["codigoTipoOperacion"].ToString();
                            values["montoEnLetras"] = CabeceraBoleta.Rows[0]["montoEnLetras"].ToString();
                            values["idPedido"] = CabeceraBoleta.Rows[0]["idPedido"].ToString();
                            values["Doc_Estado"] = CabeceraBoleta.Rows[0]["Doc_Estado"].ToString();
                            values["Doc_id_cierre"] = CabeceraBoleta.Rows[0]["Doc_id_cierre"].ToString();
                            values["items"] = negCierre.GetDetalleJson(CabeceraBoleta.Rows[0]["id_doc_electronico"].ToString());
                            //values["opGratuitas"] = negCierre.GetDetalleJson(CabeceraBoleta.Rows[0]["opGratuitas"].ToString());
                            //values["opExoneradas"] = negCierre.GetDetalleJson(CabeceraBoleta.Rows[0]["opExoneradas"].ToString());
                            //values["ISC"] = negCierre.GetDetalleJson(CabeceraBoleta.Rows[0]["ISC"].ToString());
                            var response = client.UploadValues(negParametros.EnvioDoc(), values);
                            var responseString = Encoding.Default.GetString(response);
                            //var WebService = new List<DocElectronicos>();
                            //WebService = JsonConvert.DeserializeObject<List<DocElectronicos>>(responseString);
                            //if (WebService != null)
                            //{
                            //    foreach (var item in WebService)
                            //    {
                            //        bool envio = negCierre.sp_actualizar_envio(CabeceraBoleta.Rows[0]["id_doc_electronico"].ToString());
                            //    }
                            //}    
                            bool envio = negCierre.sp_actualizar_envio(CabeceraBoleta.Rows[0]["id_doc_electronico"].ToString());
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                var s = "";
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }

        }
        public void VerificarDocFaltantes()
        {
            bool internet = negFuncionGlobal.ValidarInternet();
            if (internet)
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string codigo_cliente = directoryStructure.ObtenerRuc();
                try
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        //values["token"] = "app963";
                        values["ruc"] = codigo_cliente;
                        var response = client.UploadValues(negParametros.DocElectronicosFaltantes(), values);
                        var responseString = Encoding.Default.GetString(response);
                        //bool envio = negCierre.sp_actualizar_envio(CabeceraBoleta.Rows[0]["id_doc_electronico"].ToString());
                        var WebService = new List<DocElectronicos>();
                        WebService = JsonConvert.DeserializeObject<List<DocElectronicos>>(responseString);
                        if (WebService != null)
                        {
                            foreach (var item in WebService)
                            {
                                bool envio = negCierre.sp_actualizar_faltante(item.serieNumero);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
                }
            }
        }
        public ObservableCollection<Empresa> correo { get; set; }
        public ObservableCollection<EnvioCierre> VentaDia { get; set; }
        Neg_CierreParcial negCierre = new Neg_CierreParcial();
        Neg_Empresa negEmpresa = new Neg_Empresa();
        Neg_Mesa neg_Mesa = new Neg_Mesa();
        public List<MesasItem> mesasItem = new List<MesasItem>();
        public List<VentasDia> dataVentasDiavsVentasPasadas { get; set; }
        Neg_VentasDia negVentaD = new Neg_VentasDia();
        public void DataReporteWebService()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            int Desc;
            string verificar = InternetGetConnectedState(out Desc, 0).ToString();
            bool internet = negFuncionGlobal.ValidarInternet();
            if (internet)
            {
                // Envio cierre de caja
                DataTable DetallePagos = new DataTable();
                DetallePagos = negCierre.GetDetallePagos();
                VentaDia = negCierre.GetVentaDia();
                mesasItem = neg_Mesa.GetMesas();
                this.dataVentasDiavsVentasPasadas = negVentaD.GetVDiavsVSanterior(ConfigurationManager.AppSettings["NombreEquipo"].ToString());
                empresa = new ObservableCollection<Empresa>();

                string json = "";
                empresa = negEmpresa.GetEmpresa();

                json = negCierre.GetCierreJson();
                var jsonDatareq = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(VentaDia, Formatting.Indented));
                //foreach (var e in correo)
                //{
                bool solicitud = false;
                try
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();

                        values["fecha"] = DateTime.Now.ToString("yyyy-MM-dd");
                        values["id_empresa"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                        var response = client.UploadValues(negParametros.SolicitarSosManager(), values);

                        var responseString = Encoding.Default.GetString(response);

                        if (responseString.Trim() == "imposible conectarse:")
                        {
                            return;
                        }

                        var WebService = new RespuestaWebService();
                        WebService = JsonConvert.DeserializeObject<RespuestaWebService>(responseString);
                        if (WebService != null)
                        {
                            if (WebService.mensaje.ToUpper() == "SI HAY REGISTROS")
                            {
                                solicitud = true;
                            }
                            else if (WebService.mensaje.ToUpper() == "NO HAY REGISTROS")
                            {
                                solicitud = false;
                            }
                            else
                            {
                                solicitud = false;
                            }
                        }
                        else
                        {
                            solicitud = false;
                        }

                    }
                    if (solicitud == true)
                    {
                        using (var client = new WebClient())
                        {
                            var values = new NameValueCollection();

                            values["fecha"] = DateTime.Now.ToString("yyyy/MM/dd");
                            values["id_empresa"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                            values["id_usuario"] = Application.Current.Properties["IdUsuario"].ToString();
                            values["venta_semana"] = dataVentasDiavsVentasPasadas.First().VDVA_monto_SA.ToString();
                            values["venta_hoy"] = VentaDia.First().TOTALVENTA;
                            values["venta_visa"] = DetallePagos.Rows[0]["VISA"].ToString();
                            values["venta_mastercard"] = DetallePagos.Rows[0]["MASTERCARD"].ToString();
                            values["venta_efectivo"] = DetallePagos.Rows[0]["EFECTIVO"].ToString();
                            values["venta_creditos"] = DetallePagos.Rows[0]["EFECTIVO"].ToString();
                            values["monto_boletas"] = VentaDia.First().TOTALBOLE;
                            values["monto_facturas"] = VentaDia.First().TOTALFACT;
                            values["monto_sin_bf"] = (Convert.ToDecimal(VentaDia.First().TOTALVENTA) - (Convert.ToDecimal(VentaDia.First().TOTALBOLE) + Convert.ToDecimal(VentaDia.First().TOTALFACT))).ToString();
                            values["mesas_ocupadas"] = mesasItem.Where(m => m.M_EST != 0 && m.M_ACT == true).Count().ToString();
                            values["mesas_disponibles"] = mesasItem.Where(m => m.M_EST == 0 && m.M_ACT == true).Count().ToString();
                            values["fecha_hora_upload"] = DateTime.Now.ToString("yyyy/MM/dd");
                            var response = client.UploadValues(negParametros.EnvioDataSosManager(), values);

                            var responseString = Encoding.Default.GetString(response);
                            //var WebService = new ConsultaWebService();
                            //WebService = JsonConvert.DeserializeObject<ConsultaWebService>(responseString);
                            //if (WebService.cambio_info == 0)
                            //{

                            //}
                        }
                        using (var client = new WebClient())
                        {
                            DataTable dtTopPlatos = new DataTable();
                            dtTopPlatos = negCierre.GetTopPlatos();
                            if (dtTopPlatos.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtTopPlatos.Rows.Count; j++)
                                {
                                    var values = new NameValueCollection();
                                    values["id_business"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                                    values["fecha"] = DateTime.Now.ToString("yyyy/MM/dd");
                                    values["id_carta"] = dtTopPlatos.Rows[j]["DP_ID_CARTA"].ToString();
                                    values["nom_carta"] = dtTopPlatos.Rows[j]["DP_DESCRIP"].ToString();
                                    values["cant"] = dtTopPlatos.Rows[j]["DP_CANT"].ToString();
                                    values["importe"] = dtTopPlatos.Rows[j]["DP_IMPORT"].ToString();
                                    var response = client.UploadValues(negParametros.SetPlatosVendidos(), values);
                                    var responseString = Encoding.Default.GetString(response);
                                }
                            }
                        }
                        using (var client = new WebClient())
                        {
                            DataTable dtTopPlatos = new DataTable();
                            dtTopPlatos = negCierre.GetMesa();
                            if (dtTopPlatos.Rows.Count > 0)
                            {
                                ObservableCollection<MesaWebService> items_web = new ObservableCollection<MesaWebService>();
                                for (int j = 0; j < dtTopPlatos.Rows.Count; j++)
                                {
                                    MesaWebService new_item = new MesaWebService();
                                    new_item.id_business = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                                    new_item.fecha = DateTime.Now.ToString("yyyy/MM/dd");
                                    new_item.M_NOM = dtTopPlatos.Rows[j]["M_NOM"].ToString();
                                    new_item.m_est = dtTopPlatos.Rows[j]["m_est"].ToString();
                                    new_item.PED_TOTAL = dtTopPlatos.Rows[j]["PED_TOTAL"].ToString();
                                    new_item.PED_HORAS = dtTopPlatos.Rows[j]["PED_HORAS"].ToString();
                                    new_item.NUM_PERSONAS = dtTopPlatos.Rows[j]["NUM_PERSONAS"].ToString();
                                    items_web.Add(new_item);
                                }
                                string json_items = JsonConvert.SerializeObject(items_web);
                                var values = new NameValueCollection();
                                values["items"] = json_items;
                                var response = client.UploadValues(negParametros.GuardarMesas(), values);
                                var responseString = Encoding.Default.GetString(response);
                            }
                        }
                        using (var client = new WebClient())
                        {
                            DataTable dtTopPlatos = new DataTable();
                            dtTopPlatos = negCierre.GetPlatosWebService();
                            if (dtTopPlatos.Rows.Count > 0)
                            {
                                ObservableCollection<PlatoWebService> items_web = new ObservableCollection<PlatoWebService>();
                                for (int j = 0; j < dtTopPlatos.Rows.Count; j++)
                                {
                                    PlatoWebService new_item = new PlatoWebService();
                                    new_item.id_business = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                                    new_item.fecha = DateTime.Now.ToString("yyyy/MM/dd");
                                    new_item.IDCARTA = dtTopPlatos.Rows[j]["IDCARTA"].ToString();
                                    new_item.CAR_NOM = dtTopPlatos.Rows[j]["CAR_NOM"].ToString();
                                    new_item.CAR_PRECIO = dtTopPlatos.Rows[j]["CAR_PRECIO"].ToString();
                                    items_web.Add(new_item);
                                }
                                string json_items = JsonConvert.SerializeObject(items_web);
                                var values = new NameValueCollection();
                                values["items"] = json_items;
                                var response = client.UploadValues(negParametros.PlatosWebService(), values);
                                var responseString = Encoding.Default.GetString(response);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                    //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
                }
                ////////////////////////////
            }
        }
        public ObservableCollection<Empresa> DataEmpresa { get; set; }
        Neg_DeliveryWebService negDeliveryWebService = new Neg_DeliveryWebService();
        public ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService> DateCartaDelivery { get; set; }
        public ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService> DateCartaDeliveryDB { get; set; }
        public ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService> DateDelivery { get; set; }
        public ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService> DateRecojo { get; set; }
        public ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService> DateMesa { get; set; }
        public void ConsultarDeliveryWebService()
        {
            int Desc;
            string verificar = InternetGetConnectedState(out Desc, 0).ToString();
            bool internet = negFuncionGlobal.ValidarInternet();
            if (internet)
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                DateCartaDeliveryDB = new ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
                DateCartaDelivery = new ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
                string codigo_cliente = directoryStructure.ObtenerCodigo();
                try
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        //values["token"] = "app963";
                        values["id_business"] = codigo_cliente;

                        var response = client.UploadValues(negParametros.DeliveryApp(), values);
                        var responseString = Encoding.Default.GetString(response);
                        var WebService = new List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
                        //responseString = responseString.Replace("}", "},");
                        WebService = JsonConvert.DeserializeObject<List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>>(responseString);

                        //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                        this.DateCartaDeliveryDB = negDeliveryWebService.GetDeliveryWebService();
                        if (WebService != null)
                        {
                            if (WebService.Count == 1)
                            {
                                {
                                    if (WebService.FirstOrDefault().cod_orden == 0)
                                    {
                                        return;
                                    }
                                }
                            }
                            foreach (var item in WebService)

                            {
                                bool existe = false;
                                foreach (var item2 in this.DateCartaDeliveryDB)
                                {
                                    if (item.cod_orden == item2.cod_orden)
                                    {
                                        existe = true;
                                    }
                                }
                                if (existe == false)
                                {
                                    bool result = negDeliveryWebService.sp_insert_pedido(WebService.Where(d => d.cod_orden == item.cod_orden).FirstOrDefault());
                                }
                            }
                        }


                        this.DateCartaDeliveryDB = negDeliveryWebService.GetDeliveryWebService();
                        this.ContadorDelivery = DateCartaDeliveryDB.Where(d => d.status == "EN PROCESO").Count().ToString();
                        string ruta = AppDomain.CurrentDomain.BaseDirectory + "campana.mp3";
                        if (DateCartaDeliveryDB.Where(d => d.status == "EN PROCESO").Count() > 0)
                        {
                            ColorDelivery = "Red";
                            ColorLetraDelivery = "White";
                            WindowsMediaPlayer wplayer = new WindowsMediaPlayer();
                            wplayer.URL = ruta;
                            wplayer.controls.play();
                            //negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Tienes " + this.ContadorDelivery + " pedidos pendientes de delivery por atender.", 2);
                        }
                        else
                        {
                            //Storyboard blinkAnimation = Application.Current.TryFindResource("blinkAnimation") as Storyboard;
                            //if (blinkAnimation != null)
                            //{
                            //    blinkAnimation.Stop();
                            //}
                            ColorDelivery = "White";
                            ColorLetraDelivery = "Black";
                        }
                        //if (WebService.cambio_info == 0)
                        //{
                        //    bool result = sostic.SetSostic(WebService);
                        //}
                        //sostic2 = sostic.GetSostic();
                        //var logo_ = sostic2.First();
                        //Byte[] logo = logo_.LOGO_SOSFOOD;
                        //this.LogoSosFood = logo;
                    }
                }
                catch (Exception ex)
                {

                    //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
                }
            }
            else
            {
                this.DateCartaDeliveryDB = negDeliveryWebService.GetDeliveryWebService();
                this.ContadorDelivery = DateCartaDeliveryDB.Where(d => d.status == "EN PROCESO").Count().ToString();
            }
        }
        public void ConsultarRecojoWebService()
        {
            int Desc;
            string verificar = InternetGetConnectedState(out Desc, 0).ToString();
            bool internet = negFuncionGlobal.ValidarInternet();
            if (internet)
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                DateCartaDeliveryDB = new ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
                DateCartaDelivery = new ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
                string codigo_cliente = directoryStructure.ObtenerCodigo();
                try
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        //values["token"] = "app963";
                        values["id_business"] = codigo_cliente;

                        var response = client.UploadValues(negParametros.RecojoApp(), values);
                        var responseString = Encoding.Default.GetString(response);
                        var WebService = new List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
                        //responseString = responseString.Replace("}", "},");
                        WebService = JsonConvert.DeserializeObject<List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>>(responseString);

                        //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                        this.DateCartaDeliveryDB = negDeliveryWebService.GetRecojoWebService();
                        if (WebService != null)
                        {
                            if (WebService.Count == 1)
                            {
                                {
                                    if (WebService.FirstOrDefault().cod_orden == 0)
                                    {
                                        return;
                                    }
                                }
                            }
                            foreach (var item in WebService)

                            {
                                bool existe = false;
                                foreach (var item2 in this.DateCartaDeliveryDB)
                                {
                                    if (item.cod_orden == item2.cod_orden)
                                    {
                                        existe = true;
                                    }
                                }
                                if (existe == false)
                                {
                                    bool result = negDeliveryWebService.sp_insert_pedido(WebService.Where(d => d.cod_orden == item.cod_orden).FirstOrDefault());
                                }
                            }
                        }


                        this.DateCartaDeliveryDB = negDeliveryWebService.GetRecojoWebService();
                        this.ContadorRecojo = DateCartaDeliveryDB.Where(d => d.status == "EN PROCESO").Count().ToString();
                        string ruta = AppDomain.CurrentDomain.BaseDirectory + "campana.mp3";
                        if (DateCartaDeliveryDB.Where(d => d.status == "EN PROCESO").Count() > 0)
                        {
                            ColorRecojo = "Red";
                            ColorLetraRecojo = "White";
                            WindowsMediaPlayer wplayer = new WindowsMediaPlayer();
                            wplayer.URL = ruta;
                            wplayer.controls.play();
                            //negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Tienes " + this.ContadorRecojo + " pedidos pendientes de recojo por atender.", 2);
                        }
                        else
                        {
                            ColorRecojo = "White";
                            ColorLetraRecojo = "Black";
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                this.DateCartaDeliveryDB = negDeliveryWebService.GetRecojoWebService();
                this.ContadorRecojo = DateCartaDeliveryDB.Where(d => d.status == "EN PROCESO").Count().ToString();
            }
        }
        public void ConsultarMesaWebService()
        {
            int Desc;
            string verificar = InternetGetConnectedState(out Desc, 0).ToString();
            bool internet = negFuncionGlobal.ValidarInternet();
            if (internet)
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                DateCartaDeliveryDB = new ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
                DateCartaDelivery = new ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
                string codigo_cliente = directoryStructure.ObtenerCodigo();
                try
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        //values["token"] = "app963";
                        values["id_business"] = codigo_cliente;

                        var response = client.UploadValues(negParametros.MesaApp(), values);
                        var responseString = Encoding.Default.GetString(response);
                        var WebService = new List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
                        //responseString = responseString.Replace("}", "},");
                        WebService = JsonConvert.DeserializeObject<List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>>(responseString);

                        //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                        this.DateCartaDeliveryDB = negDeliveryWebService.GetMesaWebService();
                        if (WebService != null)
                        {
                            if (WebService.Count == 1)
                            {
                                {
                                    if (WebService.FirstOrDefault().cod_orden == 0)
                                    {
                                        return;
                                    }
                                }
                            }
                            foreach (var item in WebService)

                            {
                                bool existe = false;
                                foreach (var item2 in this.DateCartaDeliveryDB)
                                {
                                    if (item.cod_orden == item2.cod_orden)
                                    {
                                        existe = true;
                                    }
                                }
                                if (existe == false)
                                {
                                    bool result = negDeliveryWebService.sp_insert_pedido(WebService.Where(d => d.cod_orden == item.cod_orden).FirstOrDefault());
                                }
                            }
                        }

                        this.DateCartaDeliveryDB = negDeliveryWebService.GetMesaWebService();
                        this.ContadorMesa = DateCartaDeliveryDB.Where(d => d.status == "EN PROCESO").Count().ToString();
                        string ruta = AppDomain.CurrentDomain.BaseDirectory + "campana.mp3";
                        //if (DateCartaDeliveryDB.Where(d => d.status == "EN PROCESO").Count() > 0)
                        if (Convert.ToInt32(ContadorMesa) > 0)
                        {
                            ColorMesa = "Red";
                            ColorLetraMesa = "White";
                            WindowsMediaPlayer wplayer = new WindowsMediaPlayer();
                            wplayer.URL = ruta;
                            wplayer.controls.play();
                        }
                        else
                        {
                            ColorMesa = "White";
                            ColorLetraMesa = "Black";
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                this.DateCartaDeliveryDB = negDeliveryWebService.GetMesaWebService();
                this.ContadorMesa = DateCartaDeliveryDB.Where(d => d.status == "EN PROCESO").Count().ToString();
            }
        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Descripcion, int Reserved);
        public void ConsumirWebService()
        {
            int Desc;
            string verificar = InternetGetConnectedState(out Desc, 0).ToString();
            bool internet = negFuncionGlobal.ValidarInternet();
            if (internet)
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                empresa = negEmpr.GetEmpresa();
                try
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        values["cod_empresa"] = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().codigo;
                        values["token"] = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().token;
                        var response = client.UploadValues(negParametros.RutaServicioWebSostic(), values);
                        var responseString = Encoding.Default.GetString(response);
                        var WebService = new ConsultaWebService();
                        if (responseString.Trim() != "")
                        {
                            WebService = JsonConvert.DeserializeObject<ConsultaWebService>(responseString);
                            if (WebService != null)
                            {
                                if (WebService.cambio_info == 0)
                                {
                                    bool result = sostic.SetSostic(WebService);
                                }
                            }
                        }
                        sostic2 = sostic.GetSostic();
                        var logo_ = sostic2.First();
                        Byte[] logo = logo_.LOGO_SOSFOOD;
                        this.LogoSosFood = logo;
                    }
                }
                catch (Exception ex)
                {
                    sostic2 = sostic.GetSostic();
                    var logo_ = sostic2.First();
                    Byte[] logo = logo_.LOGO_SOSFOOD;
                    this.LogoSosFood = logo;
                    //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
                }
            }
            else
            {
                sostic2 = sostic.GetSostic();
                var logo_ = sostic2.First();
                Byte[] logo = logo_.LOGO_SOSFOOD;
                this.LogoSosFood = logo;
            }
        }
        public string Operacion
        {
            get => operacion;
            set
            {
                operacion = value;

            }
        }
        public async void CerrarSesion(object id)
        {
            string _mensaje = "";
            Usuario usuarios = new Usuario();
            usuarios.idusu = Convert.ToInt32(id);
            bool result = negUser.SetUsuarioUpdEstado(1, usuarios, ref _mensaje);
            if (result)
            {
                GetCurrentFocusedWindow();
            }
        }
        public static Window GetCurrentFocusedWindow()
        {
            Window window = null;
            System.Windows.Controls.Control currentControl = System.Windows.Input.Keyboard.FocusedElement as System.Windows.Controls.Control;
            if (currentControl != null)
                window = Window.GetWindow(currentControl);
            var viewmodel = new Windowprueba();
            viewmodel.Show();
            window.Close();
            return window;
        }
        private async void EditarPass()
        {
            var SiNo = new DialogEditPass
            {
                //Mensaje = { Text = "Deseas realizar esta acción ?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)Application.Current.Properties["CambioContraseña"])
            {
                return;
            }
            else
            {

            }

        }
        public bool isMessageShow { get; set; } = false;
        private async void ConsultarUsuarioActivoCaja()
        {
            if (isMessageShow == true)
            {
                return;
            }
            UsuariosActivos = negUser.getUsuariosActivos();
            if (UsuariosActivos.Count == 0)
            {
                Application.Current.Dispatcher.Invoke((Action)async delegate {
                    isMessageShow = true;
                    var Dialog2 = new MessageDialog
                    {
                        Mensaje = { Text = "Se ha perdido la conexión con el equipo en Caja. Por favor verificar el estado de la red." }
                    };
                    var x = await DialogHost.Show(Dialog2, "RootDialog");
                    
                    if (Convert.ToBoolean(x)) {
                        //GetCurrentFocusedWindow();

                        Application.Current.Shutdown();
                    }
                });
                
                //MessageBox.Show("NO HAY CONEXION A CAJA \n EL SISTEMA PROCEDERA A CERRASE AL ACEPTAR ESTE MENSAJE");
                //MessageBoxDialog frm = new MessageBoxDialog();
                //frm.ShowDialog();
                //Application.Current.Shutdown();
                //GetCurrentFocusedWindow();
            }
        }
        private void nomLogueo()
        {

        }
        private void GetUsuario(string obj)
        {
            this.Usuario.usu_nom = obj;
        }
        private void GetUserControl(string obj)
        {
            //if (negFuncionGlobal.MesaWeb() == "NO")
            //{
            //    negFuncionGlobal.DesBloquearMesaWeb("NO");
            //}
            if (Application.Current.Properties["MesaBloqueada"] != null)
            {
                if (Application.Current.Properties["MesaBloqueada"] == "SI")
                {
                    negPedido.ActualizarMesaLibre(Convert.ToInt32(Application.Current.Properties["CodMesa"]));
                    Application.Current.Properties["MesaBloqueada"] = "NO";
                }
            }
            if (obj == "Empleados")
            {
                this.UserControl = new Views.Configuracion.Empleados();
            }
            else if (obj == "Empresa")
            {
                this.UserControl = new Views.Configuracion.Empresa();
            }
            else if (obj == "Corporacion")
            {
                this.UserControl = new Views.Configuracion.Corporacion();
            }
            else if (obj == "Mant_Sup_Cat")
            {
                this.UserControl = new Views.Carta.Super_Categoria();
            }
            else if (obj == "Mant_Cat")
            {
                this.UserControl = new Views.Carta.Categoria_Carta();
            }
            else if (obj == "Mant_Grupo")
            {
                this.UserControl = new Views.Carta.Grupo_Carta();
            }
            else if (obj == "Mant_Platos")
            {
                this.UserControl = new Views.Carta.Platos_Carta();
            }
            else if (obj == "Mant_Impresora")
            {
                this.UserControl = new Views.Carta.Impresora_Carta();//
            }
            else if (obj == "Mant_Clientes")
            {
                this.UserControl = new Views.Configuracion.Cliente();//Mant_Clientes
            }
            else if (obj == "Usuarios")
            {
                this.UserControl = new Views.User.Usuario_Food();
            }
            else if (obj == "Rol_Cargo")
            {
                this.UserControl = new Views.Configuracion.Rol_Cargo();
            }
            else if (obj == "Mant_Almacen")
            {
                this.UserControl = new Views.Stock.Almacen();
            }
            else if (obj == "Mant_UM")
            {
                this.UserControl = new Views.Stock.UnidadMedida();
            }
            else if (obj == "Mant_Insumo")
            {
                this.UserControl = new Views.Stock.Insumos();
            }
            else if (obj == "Mant_Proveedores")
            {
                this.UserControl = new Views.Configuracion.Proveedor();
            }
            else if (obj == "Roles")
            {
                this.UserControl = new Views.Configuracion.Roles();
            }
            else if (obj == "Transacciones")
            {
                this.UserControl = new Views.Transacciones.Transacciones();
            }
            else if (obj == "Ambientes")
            {
                this.UserControl = new Views.Ambientes.Mesas();
            }
            else if (obj == "Receta")
            {
                this.UserControl = new Views.Receta.Receta();
            }
            else if (obj == "Ventas_del_día")
            {
                this.UserControl = new Views.Reportes.VentasdelDia.VentasDia();
            }
            else if (obj == "SubReceta")
            {
                this.UserControl = new Views.Receta.SubReceta();
            }
            else if (obj == "Factura_Compra")
            {
                this.UserControl = new Views.Factura_Compra.Fact_Compra();
            }
            else if (obj == "Metodo_pago")
            {
                this.UserControl = new Views.Configuracion.Super_Jesus();
            }
            else if (obj == "descuento")
            {
                this.UserControl = new Views.Configuracion.Descuento();
            }
            else if (obj == "tipo_mov")
            {
                this.UserControl = new Views.Configuracion.TipoCaja();
            }
            else if (obj == "rows_caja")
            {
                this.UserControl = new Views.Configuracion.Caja();
            }
            else if (obj == "Costos_Fijos")
            {
                this.UserControl = new Views.Centro_Costos.CostosFijos();
            }
            else if (obj == "Costos_Variables")
            {
                this.UserControl = new Views.Centro_Costos.CostosVariables();
            }
            else if (obj == "Mant_Ambientes")
            {
                this.UserControl = new Views.Configuracion.Ambientes();
            }
            else if (obj == "Mant_Mesas")
            {
                this.UserControl = new Views.Configuracion.Mesas();
            }
            else if (obj == "Reporte_General")
            {
                this.UserControl = new Views.Centro_Costos.ReporteGeneral();
            }
            else if (obj == "Nivel")
            {
                this.UserControl = new Views.Configuracion.Nivel();
            }
            else if (obj == "Rpt_InsumoAlmacén")
            {
                this.UserControl = new Views.Stock.Reportes.ReporteInsumoAlmacen();
            }
            else if (obj == "Mov_Almacen")
            {
                this.UserControl = new Views.Stock.MovimientoAlmacen();
            }
            else if (obj == "Historial_anulaciones")
            {
                this.UserControl = new Views.Reportes.Pedidos.RptAnulaciones();
            }
            else if (obj == "Rpt_Descuento")
            {
                this.UserControl = new Views.Reportes.Pedidos.HistorialDescuento();
            }
            else if (obj == "Historial_Ventas")
            {
                this.UserControl = new Views.Reportes.Pedidos.HistorialdeVentas();
            }
            else if (obj == "RptPrecioCostoPlato")
            {
                this.UserControl = new Views.Centro_Costos.RptPrecioCostoPlato();
            }
            else if (obj == "RptPrecioVentaPlato")
            {
                this.UserControl = new Views.Centro_Costos.RptPrecioVentaPlato();
            }
            else if (obj == "descripciones")
            {
                this.UserControl = new Views.Carta.Descripciones();
            }
            else if (obj == "det_descripciones")
            {
                this.UserControl = new Views.Carta.DetDescripciones();
            }
            else if (obj == "RptBoletayFactura")
            {
                this.UserControl = new Views.Reportes.Report_BoletayFactura.RptBoletayFactura();
            }
            else if (obj == "Rpt_Venta_Mozo")
            {
                this.UserControl = new Views.Reportes.Pedidos.HistorialVentaMozo();
            }
            else if (obj == "TiposCostos")
            {
                this.UserControl = new Views.Centro_Costos.TiposCostos();
            }
            else if (obj == "PuntoEquilibrio")
            {
                this.UserControl = new Views.Centro_Costos.PuntoEquilibrio();
            }
            else if (obj == "RptKardex")
            {
                this.UserControl = new Views.Reportes.Kardex.RptKardex();
            }
            else if (obj == "RptKardexGeneral")
            {
                this.UserControl = new Views.Reportes.Kardex.RptKardexGeneral();
            }
            else if (obj == "RptPlatoMasVendido")
            {
                this.UserControl = new Views.Reportes.PlatoMasVendido.RptPlatoMasVendido();
            }
            else if (obj == "Historial_Caja_Chica")
            {
                this.UserControl = new Views.Reportes.Pedidos.HistorialCajaChica();
            }
            else if (obj == "Historial_Vent_latos_x_mp")
            {
                this.UserControl = new Views.Reportes.Pedidos.HistorialPlatosVendidosxImpresora();
            }
            else if (obj == "RptRecurrenciaClientes")
            {
                this.UserControl = new Views.Reportes.Pedidos.RptRecurrenciaClientes();
            }
            else if (obj == "ConsumoInterno")
            {
                this.UserControl = new Views.Consumo_Interno.ConsumoInterno();
            }
            else if (obj == "IngresarReservasView")
            {
                this.UserControl = new Views.ViewsReservas.IngresarReservasView();
            }
            else if (obj == "RptReservas")
            {
                this.UserControl = new Views.ViewsReservas.RptReservas();
            }
            else if (obj == "Reporte_de_Cierres")
            {
                this.UserControl = new ViewModels.Reporte.Pedidos.ReporteCierreCaja();
            }
            else if (obj == "PagoApp")
            {
                this.UserControl = new Views.Configuracion.MantenimientoAppSosDelivery();
            }
            else if (obj == "EntregaApp")
            {
                this.UserControl = new Views.Configuracion.MantenimientoAppSosDeliveryEntrega();
            }
            else if (obj == "Documentos")
            {
                this.UserControl = new Views.Carta.Documentos();
            }
            else if (obj == "ReporteCierreCaja")
            {
                this.UserControl = new ViewModels.Reporte.Pedidos.ReporteCierreCaja();
            }
            else if (obj == "Rpt_Parametros")
            {
                this.UserControl = new Views.Transacciones.Rpt_Parametros();
            }
            else if (obj == "RptImpuestos")
            {
                ValidarImpuestos();
            }
            else if (obj == "Mermas")
            {
                this.UserControl = new Views.Consumo_Interno.Mermas();
            }
            else if (obj == "RptVentasxCanal")
            {
                this.UserControl = new Views.Reportes.Pedidos.RptVentasxCanal();
            }
            else if (obj == "InformeVentas")
            {
                this.UserControl = new Views.Reportes.Pedidos.InformeVentas();
            }
            else if (obj == "Despacho")
            {
                this.UserControl = new Views.MisPedidos.DespachoMesas();
            }
            else if (obj == "Rpt_Adelanto")
            {
                this.UserControl = new Views.Reportes.Pedidos.Rpt_Adelanto();
            }
            else if (obj == "VentasProducto")
            {
                this.UserControl = new Views.Reportes.Pedidos.VentasProducto();
            }
            else if (obj == "TipoCambio")
            {
                this.UserControl = new Views.Configuracion.TipoCambio();
            }
        }
        private void VerifyConnection(object sender, System.Timers.ElapsedEventArgs e) {
            string connetionString = null;
            SqlConnection cnn = new SqlConnection(Variables.sql_conexion);
            try
            {
                cnn.Open();
                cnn.Close();
            }
            catch
            {
                Application.Current.Dispatcher.Invoke((Action)async delegate {
                    isMessageShow = true;
                    var Dialog2 = new MessageDialog
                    {
                        Mensaje = { Text = "Se ha perdido la conexión con el equipo en Caja. Por favor verificar el estado de la red." }
                    };
                    var x = await DialogHost.Show(Dialog2, "RootDialog");

                    if (Convert.ToBoolean(x))
                    {
                        //GetCurrentFocusedWindow();

                        Application.Current.Shutdown();
                    }
                });
                //return false;
            }
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
        }
    }
}

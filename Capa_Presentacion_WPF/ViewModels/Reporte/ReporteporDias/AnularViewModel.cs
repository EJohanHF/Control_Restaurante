using Capa_Entidades;
using Capa_Entidades.Acceso;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Entidades.Models.Usuario;
using Capa_Negocio;
using Capa_Negocio.Acceso;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Negocio.User;
using Capa_Presentacion_WPF.Views.Dialogs;
using Capa_Presentacion_WPF.Views.Dialogs.DialogVentaporDia;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.ReporteporDias
{
    public class AnularViewModel : IGeneric
    {
        Neg_Pagar negPagar = new Neg_Pagar();
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Usuario negUser = new Neg_Usuario();
        Neg_Detalle_pedido negDetVent = new Neg_Detalle_pedido();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Pagar> DataDetallepedido { get; set; }
        public ObservableCollection<Detalle_Pedido> DataDetallePedido { get; set; }
        public List<Ent_Combo> ComboUsuario { get; set; }
        public Pagar SelectedDetPedDataFile { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand OpenDialogAnularPlatoCommand { get; set; }
        public ICommand GuardarAnularPedidoCommand { get; set; }
        public ICommand GuardarAnularplatoCommand { get; set; }
        private ICommand m_RowClickCommand;
        public decimal totals { get; set; }
        public decimal amortizars { get; set; }
        private Usuario usuario;
        public bool _IsEnabledbtnguardar { get; set; }
        public bool _IsEnabledbtnpagar { get; set; }
        public bool isactive { get; set; }
        private string operacion_detPago;
        private Pagar pagar;
        private string mensajesnak;
        private bool selected;
        private string _EstadoPago;
        private string pedido;
        public int id_pedido { get; set; }
        public string NombreEquipo { get; set; }
        public Detalle_Pedido SelectedDataFile { get; set; }
        public bool Enviar_Correo_Anulacion { get; set; } = false;


        List<Pagar> list = new List<Pagar>();

        #region SnackBar
        public bool IsActive
        {
            get => isactive;
            set
            {
                //this.usua = Pagar.idusuario;

                if (value == true)
                {
                    var ofSatckbar = (TimeSpan.FromMilliseconds(9000));
                    if (ofSatckbar == (TimeSpan.FromMilliseconds(9000)))
                    {
                        isactive = false;
                    }
                }
                isactive = value;
            }
        }
        public string mensajeSnack
        {
            get => mensajesnak;
            set
            {
                mensajesnak = value;
            }
        }

        public void ocultarSnackBar(TimeSpan tiempo)
        {
            IsActive = false;
        }
        #endregion

        #region Click Selected From Datagrid DetalleVentaDia

        public ICommand RowClickCommand
        {
            get
            {
                return m_RowClickCommand;
            }
        }
        #endregion
        public Usuario Usuario
        {
            get => usuario;
            set
            {
                usuario = value;
            }
        }
        public string EstPedido { get; set; }
        public string Operacion_detPago
        {
            get => operacion_detPago;
            set
            {
                if (value == "ANULAR PLATO")
                {
                    GetListaDetPago();
                }
                if (value == "ANULAR PEDIDO")
                {
                    TXTotrosIsEnabled = false;
                    GetLista();
                }
                operacion_detPago = value;
            }
        }
        public string Pedido
        {
            get => pedido;
            set
            {
                pedido = value;
            }
        }
        public string EstadoPago
        {
            get => _EstadoPago;
            set
            {
                _EstadoPago = value;
            }
        }
        public bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                Selected = value;

                //foreach (var id in this.)
                //    .Selected = value;
            }

        }
        #region Text RadioButtons
        private bool ischeck;
        private string rbtext1;
        private string rbtext2;
        private string rbtext3;
        private string rbtext4;
        private string rbtext5;
        private string id1;
        private string id2;
        private string id3;
        private string id4;
        private string result;
        private string id5;
        private string otros;
        private bool txtotrosIsEnabled;

        public string Rbtext1
        {
            get => rbtext1;
            set
            {
                rbtext1 = value;
            }
        }
        public string Rbtext2
        {
            get => rbtext2;
            set
            {
                rbtext2 = value;
            }
        }
        public string Rbtext3
        {
            get => rbtext3;
            set
            {
                rbtext3 = value;

            }
        }
        public string Rbtext4
        {
            get => rbtext4;
            set
            {
                rbtext4 = value;
            }
        }
        public string Rbtext5
        {
            get => rbtext5;
            set
            {
                rbtext5 = value;
            }
        }
        public void textRB()
        {
            this.Rbtext1 = "Error Digitacion";
            this.Rbtext2 = "Mala Elaboracion";
            this.Rbtext3 = "Tardó el plato";
            this.Rbtext4 = "Cambio de producto";
            this.Rbtext5 = "Otros";

        }
        public void tag()
        {
            this.Id1 = "1";
            this.Id2 = "2";
            this.Id3 = "3";
            this.Id4 = "4";
            this.Id5 = "5";
        }
        public bool Ischecked
        {
            get => ischeck;
            set
            {
                ischeck = value;
            }
        }

        public string Id1
        {
            get => id1;
            set
            {
                if (value != null)
                {
                    this.id1 = "1";
                }
                id1 = value;
            }
        }
        public string Id2
        {
            get => id2;
            set
            {
                if (value != null)
                {
                    this.id2 = "2";
                }
                id2 = value;
            }
        }
        public string Id3
        {
            get => id3;
            set
            {
                if (value != null)
                {
                    this.id3 = "3";
                }
                id3 = value;
            }
        }
        public string Id4
        {
            get => id4;
            set
            {
                if (value != null)
                {
                    this.id4 = "4";
                }
                id4 = value;
            }
        }
        public string Id5
        {
            get => id5;
            set
            {
                if (value != null)
                {
                    this.id5 = "5";
                }
                id5 = value;
            }
        }
        public string Otros
        {
            get => otros;
            set
            {
                otros = value;

            }
        }
        public bool TXTotrosIsEnabled
        {
            get => txtotrosIsEnabled;
            set
            {
                txtotrosIsEnabled = value;
            }
        }
        public string Result
        {
            get => result;
            set
            {
                result = value;
            }
        }
        #endregion
        public ICommand ObtenerNombre { get; set; }
        public ICommand CerrarDialog { get; set; }
        public AnularViewModel()
        {

            Enviar_Correo_Anulacion = negParametros.EnvioUploadAnulacion();
            this.NombreEquipo = ConfigurationManager.AppSettings["NombreEquipo"].ToString();
            textRB();
            tag();
            //txtotrosIsEnabled = true;
            this.Operacion_detPago = "ANULAR PLATO";
            if (Application.Current.Properties["OperPagoCel"] != null)
            {
                this.Operacion_detPago = Application.Current.Properties["OperPagoCel"].ToString();
                TXTotrosIsEnabled = false;
            }
            EstPedido = Application.Current.Properties["EstPedidoAnulacion"].ToString();
            this.EliminarCommand = new ParamCommand(new Action<object>(EliminarDetPagar));
            this.ComboUsuario = negCombo.GetUsuariosLogin();
            this.OpenDialogAnularPlatoCommand = new ParamCommand(new Action<object>(AnularPlato));
            //this.GuardarAnularPedidoCommand = new RelayCommand(new Action(AnularPedido));
            this.GuardarAnularPedidoCommand = new ParamCommand(new Action<object>(AnularPedido));
            this.GuardarAnularplatoCommand = new RelayCommand(new Action(GuardarAnularPlato));
            this.EliminarCommand = new ParamCommand(new Action<object>(EliminarDetPagar));
            this.ObtenerNombre = new ParamCommand(new Action<object>(Nombre));
            this.Usuario = new Usuario();
            this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = this.SelectedDataFile;
                if (set != null)
                {
                    if (Application.Current.Properties["id_pedido"] == null)
                    {

                    }
                    this.EstadoPago = set.nom_estado_f.ToString();
                    this.id_pedido = set.id_ped;


                    Application.Current.Properties["id_pedido"] = set.id_ped;
                    Application.Current.Properties["Totals"] = set.total_ped;
                    Application.Current.Properties["Usuario"] = set.id_usu;
                    Application.Current.Properties["Pedido"] = set.id_ped;
                    Application.Current.Properties["EstadoPago"] = set.nom_estado_f;

                    //this.DataBolFac = negBolFac.GetBoletaFactura(this.id_pedido);

                    //ObservableCollection<BoletaFactura> DataBolFactura = new ObservableCollection<BoletaFactura>(DataBolFac);
                }

            });
            if (Application.Current.Properties["id_pedido"] != null)
            {
                this.Pedido = Application.Current.Properties["id_pedido"].ToString();
            }
            if (Application.Current.Properties["EstadoF"] != null)
            {
                this.EstadoPago = Application.Current.Properties["EstadoF"].ToString();
            }
        }
        private void CloseDialog()
        {
            Application.Current.Properties["AutorizoCambio"] = null;
            Application.Current.Properties["AnularPedido"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public void Nombre(object nom)
        {
            if (nom.ToString() == "1")
            {
                this.Result = Rbtext1;
                TXTotrosIsEnabled = false;
            }
            if (nom.ToString() == "2")
            {
                this.Result = Rbtext2;
                TXTotrosIsEnabled = false;
            }
            if (nom.ToString() == "3")
            {
                this.Result = Rbtext3;
                TXTotrosIsEnabled = false;
            }
            if (nom.ToString() == "4")
            {
                this.Result = Rbtext4;
                TXTotrosIsEnabled = false;
            }
            if (nom.ToString() == "5")
            {
                TXTotrosIsEnabled = true;
                Pagar pagar = new Pagar();
                this.Result = Otros;
            }

        }

        public class DelegateCommand : ICommand
        {
            private Action m_Action;
            public DelegateCommand(Action action)
            {
                this.m_Action = action;
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                m_Action();
            }
        }

        private void AnularPlato(object idpag)
        {
            //Application.Current.Properties["OperPagoCel"] = "CAMBIAR PAGO";
            if (EstadoPago == "PAGADO" || EstadoPago == "PENDIENTE")
            {
                var opencampago = new
                {

                };
                DialogHost.Show(opencampago, "RootDialog");
            }
        }
        private void EliminarDetPagar(object iddet)
        {
            if (DataDetallepedido.Count > 0)
            {
                if (operacion_detPago == "ANULAR PLATO")
                {
                    //RemoveVlans();
                    this.DataDetallepedido.Remove(this.DataDetallepedido.Where(w => w.id == (int)iddet).FirstOrDefault());
                }
                else
                {

                }
            }
        }
        private void GuardarAnularPlato()
        {
            if (!string.IsNullOrWhiteSpace(Usuario.claveusu))
            {
                Neg_Login neg_log = new Neg_Login();
                var _id = Usuario.idusu;
                string mensaje = "";

                Ent_Usuario ent_usu = neg_log.login(_id, Usuario.claveusu, ref mensaje);
                Boolean comparar = new Boolean();
                comparar = BCrypt.Net.BCrypt.Verify(Usuario.claveusu, ent_usu.usu_pass);

                if (comparar == true)
                {
                    string _mensaje = "";
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID");
                    dt.Columns.Add("idpago");
                    foreach (Pagar item in this.DataDetallepedido)
                    {
                        DataRow row = dt.NewRow();
                        row["ID"] = item.id;
                        row["idpago"] = item.idpago;
                        dt.Rows.Add(row);

                        bool result = negPagar.SetPagar((_id == 0 ? 1 : 2), pagar, ref _mensaje, NombreEquipo, dt);
                        var view = new MessageDialog
                        {
                            Mensaje = { Text = _mensaje }
                        };
                    }
                    globales.Mensaje("SOS-FOOD - Informacion", "contrasenia correcta", 2);
                }
                else globales.Mensaje("SOS-FOOD - Informacion", "contrasenia incorrecta", 2);
                //this.Mensajeuser = "Contraseña inválido";

                    //if (result == true)
                    //{
                    //    DialogHost.CloseDialogCommand.Execute(null, null);
                    //}
            }
            //else this.Mensajeuser = "Insgrese Contraseñas ";
        }
        public ObservableCollection<Empresa> empresa { get; set; }
        Neg_Empresa negEmpresa = new Neg_Empresa();
        Neg_Parametros negParametros = new Neg_Parametros();
        public List<Capa_Entidades.Models.Web_Service.RespuestaWebService> dataDeliveryWebServiceSosFood { get; set; }
        private async void AnularPedido(object parameter)
        {
            try { 
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;
                Usuario.claveusu = password;
                if (!string.IsNullOrWhiteSpace(Usuario.claveusu) && Usuario.idusu >= 0)
                {
                    Neg_Login neg_log = new Neg_Login();
                    var _id = Usuario.idusu;
                    string mensaje = "";

                    Ent_Usuario ent_usu = neg_log.login(_id, Usuario.claveusu, ref mensaje);
                    Boolean comparar = new Boolean();
                    comparar = BCrypt.Net.BCrypt.Verify(Usuario.claveusu, ent_usu.usu_pass);

                    if (comparar == true)
                    {
                        if (this.Operacion_detPago == "ANULAR PLATO")
                        {
                            Application.Current.Properties["AnularPedido"] = "1";
                            Application.Current.Properties["ComentarioPLato"] = this.Result;
                            Application.Current.Properties["IdUsuarioAnulacion"] = ent_usu.id;
                            DialogHost.CloseDialogCommand.Execute(null, null);
                            
                            //}
                            //else
                            //{
                            //    Application.Current.Properties["AnularPedido"] = "1";
                            //}
                        }
                        else if (this.Operacion_detPago == "PARAMETROS")
                        {
                            Application.Current.Properties["AutorizoCambio"] = "SI";
                            DialogHost.CloseDialogCommand.Execute(null, null);

                            //}
                            //else
                            //{
                            //    Application.Current.Properties["AnularPedido"] = "1";
                            //}
                        }
                        else
                        {
                            
                            //rb();
                            if (Application.Current.Properties["RegistrarPedido"] != null)
                            {
                                Application.Current.Properties["AnularPedido"] = "1";
                                Application.Current.Properties["ComentarioPLato"] = this.Result;
                                Application.Current.Properties["IdUsuarioAnulacion"] = ent_usu.id;
                                DialogHost.CloseDialogCommand.Execute(null, null);
                                return;
                            }
                            else
                            {
                                if (EstPedido == "PENDIENTE")
                                {
                                    Pagar pagar = new Pagar();
                                    if (this.Rbtext1 == null)
                                    {
                                        pagar.comentario = this.Rbtext1;
                                    }
                                    else
                                    {
                                        pagar.comentario = this.Result;
                                    }
                                    pagar.idpedido = this.Pedido;
                                    string _mensaje = "ANULACION DE PEDIDO";
                                    bool result = negPagar.SetAnularPedido(1, pagar, ref _mensaje, ent_usu.id);
                                    if (result)
                                    {
                                        if (negParametros.ImpComAnuladaCaja())
                                        {
                                            ImpCuentaAnuladoCaja();
                                        }



                                        if (Enviar_Correo_Anulacion == true)
                                        {
                                            // EnvioAnulacionPedido
                                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                            string json = "";
                                            empresa = negEmpresa.GetEmpresa();
                                          //  json = negCierre.GetCierreJson();

                                            using (var client = new WebClient())
                                            {
                                                var values = new NameValueCollection();
                                                values["id_empresa"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                                                values["fecha"] = DateTime.Now.ToString("yyyy/MM/dd");
                                                values["nombre_empresa"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().empr_nom.ToString();
                                                values["correo"] = json;
                                                values["comentario"] = this.Result;
                                                values["nombre_empleado"] = "";
                                                values["cod_pedido"] = ent_usu.id.ToString();
                                                values["mozo"] = "";
                                                var response = client.UploadValues(negParametros.UploadAnulacion(), values);
                                                var responseString = Encoding.Default.GetString(response);
                                            }


                                        }



                                        /*llhssseb*/
                                        DialogHost.CloseDialogCommand.Execute(null, null);
                                        globales.Mensaje("SOS-FOOD - Informacion", _mensaje, 2);
                                        /*llhssseb*/
                                    }
                                }else if (EstPedido == "PAGADO")
                                {
                                    //if (ent_usu.estadopriv == 1)
                                    //{
                                    //    Pagar pagar = new Pagar();
                                    //    if (this.Rbtext1 == null)
                                    //    {
                                    //        pagar.comentario = this.Rbtext1;
                                    //    }
                                    //    else
                                    //    {
                                    //        pagar.comentario = this.Result;
                                    //    }
                                    //    pagar.idpedido = this.Pedido;
                                    //    string _mensaje = "";
                                    //    bool result = negPagar.SetAnularPedido(1, pagar, ref _mensaje, ent_usu.id);
                                    //    if (result)
                                    //    {
                                    //        ImpCuentaAnuladoCaja();
                                    //        /*llhssseb*/
                                    //        DialogHost.CloseDialogCommand.Execute(null, null);
                                    //        globales.Mensaje("SOS-FOOD - Informacion", _mensaje, 2);
                                    //        /*llhssseb*/
                                    //    }
                                    //}
                                    //else
                                    //{
                                        DialogHost.CloseDialogCommand.Execute(null, null);
                                        var openpago = new DialogAnularToken
                                        {

                                        };
                                        var x = await DialogHost.Show(openpago, "RootDialog");
                                        if(Application.Current.Properties["ValidacionToken"] == null)
                                        {
                                            return;
                                        }
                                        if (Application.Current.Properties["ValidacionToken"].ToString() == "SI")
                                        {
                                            empresa = negEmpresa.GetEmpresa();
                                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                            using (var client = new WebClient())
                                            {
                                                var values = new NameValueCollection();
                                                //values["token"] = "app963";
                                                values["id_business"] = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().codigo;

                                                var response = client.UploadValues(negParametros.SetToken(), values);
                                                var responseString = Encoding.Default.GetString(response);
                                                var WebService = new List<Capa_Entidades.Models.Web_Service.RespuestaWebService>();
                                                //responseString = responseString.Replace("}", "},");
                                                WebService = JsonConvert.DeserializeObject<List<Capa_Entidades.Models.Web_Service.RespuestaWebService>>(responseString);

                                                //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                                                this.dataDeliveryWebServiceSosFood = WebService;

                                            }
                                            if (dataDeliveryWebServiceSosFood == null)
                                            {
                                                Application.Current.Properties["ValidacionToken"] = null;
                                                globales.Mensaje("SOS-FOOD - Informacion", "El token no se pudo validar.", 2);
                                                return;
                                            }
                                            if (dataDeliveryWebServiceSosFood.Count == 0)
                                            {
                                                Application.Current.Properties["ValidacionToken"] = null;
                                                globales.Mensaje("SOS-FOOD - Informacion", "El token no se pudo validar.", 2);
                                                return;
                                            }
                                            if (dataDeliveryWebServiceSosFood.Count > 0)
                                            {
                                                if (dataDeliveryWebServiceSosFood.FirstOrDefault().mensaje == "OK")
                                                {
                                                    Pagar pagar = new Pagar();
                                                    if (this.Rbtext1 == null)
                                                    {
                                                        pagar.comentario = this.Rbtext1;
                                                    }
                                                    else
                                                    {
                                                        pagar.comentario = this.Result;
                                                    }
                                                    pagar.idpedido = this.Pedido;
                                                    string _mensaje = "";
                                                    bool result = negPagar.SetAnularPedido(1, pagar, ref _mensaje, ent_usu.id);
                                                    if (result)
                                                    {
                                                    if (negParametros.ImpComAnuladaCaja())
                                                    {
                                                        ImpCuentaAnuladoCaja();
                                                    }

                                                    /*llhssseb*/
                                                    DialogHost.CloseDialogCommand.Execute(null, null);
                                                        globales.Mensaje("SOS-FOOD - Informacion", _mensaje, 2);
                                                        /*llhssseb*/
                                                    }
                                                }
                                                else
                                                {
                                                    Application.Current.Properties["ValidacionToken"] = null;
                                                    globales.Mensaje("SOS-FOOD - Informacion", "El token no se pudo validar.", 2);
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                Application.Current.Properties["ValidacionToken"] = null;
                                                globales.Mensaje("SOS-FOOD - Informacion", "El token no se pudo validar.", 2);
                                                return;
                                            }
                                        }
                                    //}
                                }
                            }
                        }
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "contraseña incorrecta", 2);
                    }
                }
            }
            catch (Exception ex)
            { 
                globales.Mensaje("SOS-FOOD - Informacion", "Error al anular el pedido.", 2);
                return;
            }
        }
        void RemoveVlans()
        {
            if (DataDetallepedido != null)
            {
                DataDetallepedido.Remove(SelectedDetPedDataFile);
                DataDetallepedido.Select(s => s.id).ToList().FirstOrDefault();
            }
        }
        private void GetListaDetPago()
        {
            int idpedido = 0;
            idpedido = Convert.ToInt32(Application.Current.Properties["id_pedido"]);
            DataDetallepedido = negPagar.GetDetallePago(idpedido);

        }
        private void GetLista()
        {
            DataDetallePedido = negDetVent.GetDetallepedido(ConfigurationManager.AppSettings["NombreEquipo"].ToString());
            //DataBolFac2 = negBolFac.GetBoletaFactura();
        }
        private void ImpCuentaAnuladoCaja()
        {
            string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
            this.id_pedido = Convert.ToInt32(Application.Current.Properties["id_pedido"]);
            if (this.id_pedido == 0)
            {
                /*aqui va el snackbar*/
                return;
            }
            else
            {
                DataTable dt = new DataTable();
                dt = negDetVent.GetComandaAnuladaPedido(this.id_pedido);
                Ticket ticket = new Ticket();

                ticket.AddHeaderLine_2("PEDIDO ANULADO N°: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                ticket.AddHeaderLine("");
               
                ticket.AddSubHeaderLine("Atendido Por: " + dt.Rows[0]["EMPL_NOM"].ToString());
                ticket.AddSubHeaderLine("Mesa: " + dt.Rows[0]["M_NOM"].ToString());
                ticket.AddSubHeaderLine("");

                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    ticket.AddItemComanda(dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString(), dt.Rows[j]["DP_DESCRIP"].ToString());
                }
               
                ticket.AddFooterLine("");

                ticket.AddFooterLine("");

                if (ticket.PrinterExists(ImpCaja))
                {
                    ticket.PrintTicket(ImpCaja);
                }
                else
                {
                    globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                }
            }


        }
    }
}

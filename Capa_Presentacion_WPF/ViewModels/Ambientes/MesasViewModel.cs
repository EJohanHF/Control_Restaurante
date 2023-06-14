using Capa_Entidades;
using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Nivel;
using Capa_Entidades.Models.Parametros;
using Capa_Entidades.Models.Pedido;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Entidades.Models.Reservas;
using Capa_Entidades.Models.Usuario;
using Capa_Entidades.Models.Web_Service;
using Capa_Negocio;
using Capa_Negocio.Ambientes;
using Capa_Negocio.Carta;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Delivery_Web_Service;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Pedido;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Negocio.Reservas;
using Capa_Negocio.Stock;
using Capa_Negocio.User;
using Capa_Negocio.WebService;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Carta;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Carta.ItemsViewModel;
using Capa_Presentacion_WPF.ViewModels.Dialogs;
using Capa_Presentacion_WPF.ViewModels.Reservas;
using Capa_Presentacion_WPF.Views;
using Capa_Presentacion_WPF.Views.Cuentas;
using Capa_Presentacion_WPF.Views.Dialogs;
using Capa_Presentacion_WPF.Views.Dialogs.DialogVentaporDia;
using Capa_Presentacion_WPF.Views.Dialogs.Facturacion_Electronica;
using Capa_Presentacion_WPF.Views.Dialogs.Pedidos;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ThoughtWorks.QRCode.Codec;
using tickets;
using static Capa_Presentacion_WPF.ViewModels.Dialogs.DialogClienteViewModel;

namespace Capa_Presentacion_WPF.ViewModels.Ambientes
{
    public class MesasViewModel : IGeneric
    {

        Neg_Parametros negParametros = new Neg_Parametros();
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Descripcion, int Reserved);
        AmbientesStructure directoryStructure = new AmbientesStructure();
        ReservasStructure rs = new ReservasStructure();
        #region CategoriaCarta
        Neg_Categoria_carta negsCatgCart = new Neg_Categoria_carta();
        public ObservableCollection<CategoriaCartaItemViewModel> DatasCategoriacarta { get; set; }
        #endregion
        public string HabilitarPedir { get; set; }
        #region GrupoCarta
        Neg_Grupo negsGrupo = new Neg_Grupo();
        public ObservableCollection<GrupoCartaItemViewModel> DatasGrupo { get; set; }
        #endregion
        #region Negocio
        Neg_Platos negPlatos = new Neg_Platos();
        Neg_Pedido negPedido = new Neg_Pedido();
        Neg_Pagar negPagar = new Neg_Pagar();
        Neg_Insumos negInsumo = new Neg_Insumos();
        Funcion_Global globales = new Funcion_Global();
        Neg_Mesa negEmp = new Neg_Mesa();
        Neg_Ambientes netAmb = new Neg_Ambientes();
        Neg_Empresa negEmpresa = new Neg_Empresa();
        Neg_Detalle_pedido negDetVent = new Neg_Detalle_pedido();
        Neg_Parametros neg_par = new Neg_Parametros();
        Neg_CierreParcial negCierre = new Neg_CierreParcial();
        Neg_Usuario negUser = new Neg_Usuario();
        Neg_Reservas neg_reserv = new Neg_Reservas();
        Neg_Combo negCombo = new Neg_Combo();
        #endregion
        #region Listas
        public ObservableCollection<Platos> DataPlatos { get; set; }
        public ObservableCollection<Platos> DataPlatosGeneral { get; set; }
        public ObservableCollection<Platos> DataPlatosBusqueda { get; set; }
        public ObservableCollection<Platos> DataPlatosActivos { get; set; }
        public ObservableCollection<Platos> DataPedido { get; set; }
        public ObservableCollection<Platos> DataPedidoTemp { get; set; }
        public ObservableCollection<Pedidos> Pedido { get; set; }
        public ObservableCollection<Detalle_Pedido> DataDetallePedido { get; set; }
        public ObservableCollection<Empresa> empresa { get; set; }
        public ObservableCollection<Empresa> correo { get; set; }
        public ObservableCollection<Usuario> DataUsuario { get; set; }
        public bool isRbChecked { get; set; }
        public object UserControl { get; set; }
        public ObservableCollection<MesasItemViewModel> DataMesas { get; set; }
        public ObservableCollection<MesasItemViewModel> DataSubMesas { get; set; }
        public ObservableCollection<AmbientesItemViewModel> DataAmbientes { get; set; }
        static ObservableCollection<Parametros> parametros = new ObservableCollection<Parametros>();
        public ICollectionView ContractsListCollectionView { get; private set; }

        public List<Ent_Reserva> _reserva { get; private set; }
        public List<Ent_Reserva> reserva { get; private set; }
        public ObservableCollection<Prioridades> DataPrioridades { get; private set; }
        #endregion
        public String DataChips { get; set; }
        public Mesa Mesa
        {
            get => mesa;
            set
            {
                mesa = value;
            }
        }
        #region Commands
        public ICommand TraerMesasCommand { get; set; }
        public ICommand DatosEmpresa { get; set; }
        public ICommand RegistrarPedidoCommand { get; set; }
        public ICommand EntrarGrupoCommand { get; set; }
        public ICommand VolverCategoriaCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand GenerarQRCommand { get; set; }
        public ICommand CambiarEstadoDeliveryCommand { get; set; }
        public ICommand GuardarPedido { get; set; }
        public ICommand MyCommand { get; set; }
        public ICommand CargaPlatos { get; set; }
        public ICommand PasarPlato { get; set; }
        public ICommand MostrarPlatos { get; set; }
        public ICommand EliminarPlato { get; set; }
        public ICommand RbChecked { get; set; }
        public ICommand EliminaPlatoPedido { get; set; }
        public ICommand ReimprimirPlato { get; set; }
        public ICommand DescPedido { get; set; }
        public ICommand DialogNumPersonas { get; set; }
        public ICommand DialogNomPers { get; set; }
        public ICommand DialogMotorizado { get; set; }
        public ICommand DialogTelPers { get; set; }
        public ICommand OpenDialogPagarCommand { get; set; }
        public ICommand CerrarDialog { get; set; }
        public ICommand AnularPedido { get; set; }
        public ICommand QuitarDesc { get; set; }
        public ICommand CambMesa { get; set; }
        public ICommand AbrirBoletaElectronica { get; set; }
        public ICommand AbrirFacturaElectronica { get; set; }
        public ICommand AgregarDescrip { get; set; }
        public ICommand BorrarDescrip { get; set; }
        public ICommand UnificarMesaCommand { get; set; }
        public ICommand ImprimirCuentaCommand { get; set; }
        public ICommand DividirCuentasCommand { get; set; }
        public ICommand AgregarPrioridadCommand { get; set; }
        public ICommand EnterCommand { get; set; }
        public ICommand AbrirDialogUltimoPedidoDeliveryCommand { get; set; }
        private ICommand m_RowClickCommand;
        public ICommand RowClickCommand
        {
            get
            {
                return m_RowClickCommand;
            }
        }
        private ICommand m_RowClickCantidadCommand;
        public ICommand RowClickCantidadCommand
        {
            get
            {
                return m_RowClickCantidadCommand;
            }
        }
        private ICommand m_RowClickColumna;
        public ICommand RowClickColumna
        {
            get
            {
                return m_RowClickColumna;
            }
        }


        private ICommand m_RowPrioridadClickCommand;
        public ICommand RowPrioridadClickCommand
        {
            get
            {
                return m_RowPrioridadClickCommand;
            }
        }

        private ICommand m_RowPrioridadDoubleClickCommand;
        public ICommand RowPrioridadDoubleClickCommand
        {
            get
            {
                return m_RowPrioridadDoubleClickCommand;
            }
        }
        #endregion
        #region Objetos

        public string NombreEquipo { get; set; }
        public string VisibilityOjo { get; set; }
        public String Datetime { get; set; }
        public List<string> TextoChip;
        public List<Platos> PlatosList;
        public List<Platos> PlatosList2;
        public List<Ent_Combo> ComboMotorizado { get; set; }
        public Platos SelectedDataFile { get; set; }
        public string VisibilityPP { get; set; }
        public string VisibilityRP { get; set; }
        public Platos SelectedDataColumna { get; set; }
        public Prioridades Prioridades { get; set; }
        public bool IdCheLlevar { get; set; } = false;

        public Platos SelectedCantidadFile { get; set; }
        public ICommand DialogDescPlato { get; set; }
        public ICommand ResetPlatoCommand { get; set; }
        public ICommand MoverPlato { get; set; }

        private string prioridadtext = "";
        public string PrioridadText
        {
            get => prioridadtext;
            set
            {
                if (value != null)
                {
                    prioridadtext = value;
                }
            }
        }
        public string Operacion
        {
            get => operacion;
            set
            {
                try
                {
                    if (value == "Lista")
                    {
                        GetAmbientes();
                        int idAmbiente = 1;
                        if (Application.Current.Properties["IdAmbiente"] == null)
                        {
                            idAmbiente = DataAmbientes.FirstOrDefault().ID;
                            Application.Current.Properties["IdAmbiente"] = idAmbiente.ToString();
                        }
                        else
                        {
                            idAmbiente = Convert.ToInt32(Application.Current.Properties["IdAmbiente"]);
                        }
                        GetMesas(idAmbiente);
                        GetDatosEmpresa(1);
                    }
                    operacion = value;
                }
                catch (Exception)
                {
                    //globales.Mensaje("SOS-FOOD - Error: ", "No hay data de ambientes ni mesas", 3);
                }

            }
        }
        private Platos platos;
        public Platos Platos
        {
            get => platos;
            set
            {
                platos = value;
            }
        }
        public string _buscador { get; set; }
        public string Buscador
        {
            get => _buscador;
            set
            {
                if (value != null && value.Trim() != "")
                {
                    this.DataPlatos = new ObservableCollection<Platos>(this.DataPlatosBusqueda.Where(p => p.nomplato.ToUpper().Contains(value.ToString().ToUpper())).ToList());
                }
                else
                {
                    //this.DataPlatos = new ObservableCollection<Platos>();
                    this.DataPlatos = negPlatos.GetPlatosMasVendidos();
                }
                _buscador = value;
            }
        }
        public string isGrupoCarta { get; set; }
        public string ComentarioPedido { get; set; }
        public double WidthPantalla { get; set; }
        public double HeigthPantalla { get; set; }
        public int Width { get; set; }
        public string isCategoriaCarta { get; set; }
        public string NombreAmbiente { get => nombreAmbiente; set { nombreAmbiente = value; } }
        public string VisilibilityDesarrolladopor { get; set; }
        public Byte[] LogoEmpresa { get => logoEmpresa; set { logoEmpresa = value; } }
        public Byte[] LogoSosTic { get => logoStic; set { logoStic = value; } }
        public Byte[] LogoSosFood { get => logoSfood; set { logoSfood = value; } }
        public string Telefono1 { get => telefono1; set { telefono1 = value; } }
        public string Telefono2 { get => telefono2; set { telefono2 = value; } }
        public string Correo1 { get => correo1; set { correo1 = value; } }
        public bool SubMesa { get => subMesa; set { subMesa = value; } }
        public bool IsOpenDialogSubMesa { get => isOpenDialogSubMesa; set { isOpenDialogSubMesa = value; } }
        public string MesaPadre { get => mesaPadre; set { mesaPadre = value; } }
        public string VisibilityArrow { get => visibilityArrow; set { visibilityArrow = value; } }
        public string CodGrupo { get; set; }
        public string NombreMesa { get; set; }
        public string CodMesa { get; set; }
        public string Fecha_Registro { get; set; }
        public string Fecha_Transcurrido { get; set; }
        public string total { get; set; }
        public decimal total_numero { get; set; }
        public string SubTotalPedido { get; set; }
        public string DescuentoPedido { get; set; }

        public string TotalPedido { get; set; }
        public string IdAmbiente { get; set; }
        public string OrdenItem { get; set; }
        public string Nombre_Mozo { get; set; }
        public string Nro_Personas { get; set; }
        public string NroDiario { get; set; }
        public string BotonPedir { get; set; }

        public string BotonFactura { get; set; }
        public string BotonBoleta { get; set; }
        public string BotonDescuento { get; set; }

        public string BotonQR { get; set; }
        public string BotonEstadoDelivery { get; set; }
        public int nroColumnas { get; set; }

        private Mesa mesa;
        private string operacion;
        private string nombreAmbiente;
        private Byte[] logoEmpresa;
        public string NombreEmpresa { get; set; }
        private Byte[] logoStic;
        private Byte[] logoSfood;
        private string telefono1;
        private string telefono2;
        private string correo1;
        private bool subMesa;
        private bool isOpenDialogSubMesa;
        private string mesaPadre;
        private string visibilityArrow;
        public string NomCliente { get; set; }
        public string Motorizado { get; set; }
        public string TelCliente { get; set; }
        public string IdCliente { get; set; }
        public string ClienteDelivery { get; set; }
        public string ClienteDelivery2 { get; set; }
        public string NomMozoMesa { get; set; }
        public int idPedido { get; set; }
        int id_tipo_pago { get; set; }
        string nombre_tipo_pago { get; set; }
        decimal monto { get; set; }
        decimal vuelto { get; set; }
        decimal _totaltotal { get; set; }
        String totaltotal { get; set; }
        String subtotal { get; set; }
        List<string> idCartaSubNivel { get; set; }
        public bool FacturacionEnable { get; set; } = true;
        public string TCambio { get; set; }
        public string TDolar { get; set; }
        public DataTable dt_info_delivery { get; set; }
        public DataTable dt_info_pago_cliente { get; set; }
        public bool Enviar_Correo_Anulacion { get; set; } = false;
        public int CountPrintLlevar { get; set; }
        public int CountPrintDelivery { get; set; }
        #endregion
        public MesasViewModel()
        {
            this.NombreEquipo = ConfigurationManager.AppSettings["NombreEquipo"].ToString();
            this.Operacion = "Lista";
            this.VisibilityPP = "Visible";
            DateTime thisDay = DateTime.Today;
            this.Datetime = thisDay.ToString();
            this.VisibilityOjo = "Collapsed";
            this.TraerMesasCommand = new ParamCommand(new Action<object>(TraerMesas));
            this.RegistrarPedidoCommand = new ParamCommand(new Action<object>(RegistrarPedido));
            this.EntrarGrupoCommand = new ParamCommand(new Action<object>(CargarGrupo));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.GenerarQRCommand = new RelayCommand(new Action(GenerarQR));
            this.CambiarEstadoDeliveryCommand = new RelayCommand(new Action(cambiarEstDelivery));
            this.QuitarDesc = new RelayCommand(new Action(QuitarDescuento));
            this.DescPedido = new RelayCommand(new Action(DescuenPedido));
            this.DialogNumPersonas = new RelayCommand(new Action(NumPersonas));
            this.DialogNomPers = new RelayCommand(new Action(NomPersonas));
            this.DialogMotorizado = new RelayCommand(new Action(NomMotorizado));
            this.DialogTelPers = new RelayCommand(new Action(TelPersonas));
            this.CerrarDialog = new RelayCommand(new Action(CerrarDialogo));
            this.GuardarPedido = new RelayCommand(new Action(RealizarPedido));
            this.VolverCategoriaCommand = new RelayCommand(new Action(VolverCategoria));
            this.MyCommand = new ParamCommand(new Action<object>(Command));
            this.CargaPlatos = new ParamCommand(new Action<object>(CargarCarta));
            this.MostrarPlatos = new ParamCommand(new Action<object>(MostrarCarta));
            this.PasarPlato = new ParamCommand(new Action<object>(PasarPlatos));
            this.ImprimirCuentaCommand = new RelayCommand(new Action(ImpCuenta));
            this.DividirCuentasCommand = new RelayCommand(new Action(DividirCuentas));
            this.EliminarPlato = new ParamCommand(new Action<object>(EliminarPlatos));
            this.RbChecked = new ParamCommand(new Action<object>(PonerLLevar));
            this.EliminaPlatoPedido = new ParamCommand(new Action<object>(EliminarPlatoPedido));
            this.ReimprimirPlato = new ParamCommand(new Action<object>(ReImprimir));
            this.OpenDialogPagarCommand = new RelayCommand(new Action(AbrirDailogPagar));
            this.EnterCommand = new RelayCommand(new Action(Enter));
            this.AbrirDialogUltimoPedidoDeliveryCommand = new RelayCommand(new Action(AbrirDialogUltimoPedidoDelivery));
            this.AnularPedido = new RelayCommand(new Action(AnularPed));
            this.ComboMotorizado = negCombo.GetComboMotorizado();
            this.NombreMesa = "";
            var categoriacarta = negsCatgCart.GetCategoria();
            this.Buscador = "";
            FacturacionEnable = Convert.ToBoolean(negParametros.FacturacionElectronica());
            this.DatasCategoriacarta = new ObservableCollection<CategoriaCartaItemViewModel>(
                categoriacarta.Select(ct => new CategoriaCartaItemViewModel(ct.idcat, ct.idscat.ToString(), ct.nomscat, ct.nomcat, Convert.ToDecimal(ct.desccat), ct.imagencat, ct.columna)));
            ListCollectionView lcv = new ListCollectionView(DatasCategoriacarta);
            lcv.GroupDescriptions.Add(new PropertyGroupDescription("NOMSCAT"));
            ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(lcv);
            this.idCartaSubNivel = new List<string>();
            this.CodGrupo = "0";
            this.DataPedido = new ObservableCollection<Platos>();
            this.DataPedidoTemp = new ObservableCollection<Platos>();
            this.Pedido = new ObservableCollection<Pedidos>();
            this.total = "TOTAL : S/. 0.00";
            this.MesaPadre = "";
            this.TotalPedido = "TOTAL : S/. 0.00";
            this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
            this.DescuentoPedido = "DESCUENTO : S/. 0.00";
            this.WidthPantalla = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.HeigthPantalla = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.DataPlatosGeneral = negPlatos.GetPlatosActivos();
            this.DataPlatosBusqueda = negPlatos.GetPlatosActivos();
            this.DataPlatos = this.DataPlatosBusqueda;
            if (WidthPantalla == 1024 && HeigthPantalla == 768)
            {
                this.Width = 100;
            }
            else
            {
                this.Width = 500;
            }
            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = this.SelectedDataFile;
                if (set != null)
                {

                    PasarPlatos(set.idplato);
                }
            });
            m_RowClickCantidadCommand = new CantidadCommand(() =>
            {
                var set = this.SelectedCantidadFile;
                if (set != null)
                {
                    Application.Current.Properties["CantidadActual"] = set.cantidad;
                    Application.Current.Properties["Orden"] = set.orden;
                    Cantidad();

                }
            });
            m_RowClickColumna = new ColumnaCommand(() =>
            {
                var set = this.SelectedCantidadFile;
                if (set != null)
                {
                    Application.Current.Properties["Orden"] = set.orden;

                }
            });
            this.CambMesa = new RelayCommand(new Action(CambiarMesa));
            this.AgregarDescrip = new RelayCommand(new Action(AgregarDescripcion));
            this.AbrirBoletaElectronica = new RelayCommand(new Action(BoletaElectronica));
            this.AbrirFacturaElectronica = new RelayCommand(new Action(FacturaElectronica));
            Application.Current.Properties["ClienteLlevar"] = "";
            //Estado Mesa
            Timer timer = new Timer(5000);
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_elapsed);
            timer.Start();
            //FIN
            Timer tiempo = new Timer(100);
            tiempo.AutoReset = true;
            tiempo.Elapsed += new System.Timers.ElapsedEventHandler(timer_tiempo);
            tiempo.Start();
            this.BorrarDescrip = new RelayCommand(new Action(BorrarDescripcion));
            this.DialogDescPlato = new ParamCommand(new Action<object>(DescPlatos));
            this.ResetPlatoCommand = new ParamCommand(new Action<object>(ResetPlato));
            this.UnificarMesaCommand = new RelayCommand(new Action(UnificarMesa));
            //this.DataMesas = new ObservableCollection<MesasItemViewModel>();
            this.MoverPlato = new ParamCommand(new Action<object>(MoverPlatos));
            this.AgregarPrioridadCommand = new RelayCommand(new Action(AgregarPrioridad));
            this.DataPrioridades = negPedido.GetPrioridades();
            m_RowPrioridadClickCommand = new DelegateCommand(() =>
            {
                var set = this.Prioridades;
                if (set != null)
                {
                    int id_prioridad = set.ID;
                    string descripcion = set.DESCR;
                }
            });
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            m_RowPrioridadDoubleClickCommand = new DelegateCommandDoubleClick(() =>
            {
                var set = this.Prioridades;
                if (Modulo == "1")
                {
                    if (set != null)
                    {
                        bool x = negPedido.EliminarPrioridad(set.ID);
                        if (x)
                        {
                            this.DataPrioridades = negPedido.GetPrioridades();
                        }
                    }
                }
            });
            if (Modulo == "1")
            {
                VisibilityPP = "Visible";
            }
            else if (Modulo == "3")
            {
                VisibilityPP = "Collapsed";
            }
        }
        private void Enter() {
            try
            {
                if (this.Buscador.Trim() != "")
                {
                    int c = this.DataPlatosBusqueda.Where(p => p.cbarplato.ToUpper() == Buscador.ToUpper()).Count();
                    if (c > 0)
                    {
                        var id = this.DataPlatosBusqueda.Where(p => p.cbarplato.ToUpper() == Buscador.ToUpper()).FirstOrDefault().idplato.ToString();
                        if (id != null)
                        {
                            List<Platos> nn = new List<Platos>();
                            nn = DataPlatosBusqueda.Where(p => p.cbarplato.ToUpper() == Buscador.ToUpper()).ToList();
                            ObservableCollection<Platos> n = new ObservableCollection<Platos>(nn);
                            DataPlatos = n;
                            PasarPlatos(id);
                        }
                        this.Buscador = "";
                    }
                    this.Buscador = "";
                }
            }
            catch (Exception ex)
            {
            }
        }
        private async void UnificarMesa()
        {
            if (Pedido.Count != 0)
            {
                Application.Current.Properties["Id_PedidoCambio"] = this.Pedido.ToList().FirstOrDefault().DP_ID_PED;
                Application.Current.Properties["CodMesaCambio"] = Application.Current.Properties["CodMesa"];
                Application.Current.Properties["TituloDialogCambiarUnificarMesa"] = "UNIFICAR MESA";
                var DialogCambiarMesa = new DialogCambiarMesa { };
                var x = await DialogHost.Show(DialogCambiarMesa, "RootDialog");
                if (Application.Current.Properties["CambioMesa"] != null)
                {
                    Cancelar();
                    directoryStructure = new AmbientesStructure();
                    if (Application.Current.Properties["IdAmbiente"] == null)
                    {
                        int idAmbiente = DataAmbientes.FirstOrDefault().ID;
                        Application.Current.Properties["IdAmbiente"] = idAmbiente.ToString();
                    }
                    GetMesas(Application.Current.Properties["IdAmbiente"].ToString());
                }
            }
            else
            {
                var Dialog2 = new MessageDialog
                {
                    Mensaje = { Text = "Esta mesa no tiene pedidos" }
                };
                var x = await DialogHost.Show(Dialog2, "RootDialog");
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
        private void CerrarDialogo()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            this.MesaPadre = "";
        }
        public ObservableCollection<Detalle_Pedido> DataDetprod { get; set; }
        private async void AbrirDialogUltimoPedidoDelivery()
        {
            try
            {
                if (this.NombreMesa.Contains("DELIVERY"))
                {
                    int IdPedido2 = 0;
                    if (this.Pedido.Count != 0)
                    {
                        IdPedido2 = this.Pedido.FirstOrDefault().DP_ID_PED;
                    }
                    int idcli = Convert.ToInt32(IdCliente);
                    DataTable pedido = negPedido.getPedidoxCliente(idcli, IdPedido2);
                    int idPed = Convert.ToInt32(pedido.Rows[0]["DP_ID_PED"]);

                    Application.Current.Properties["N"] = idPed;
                    this.DataDetprod = negDetVent.GetDetProducto(Application.Current.Properties["N"].ToString());
                    Application.Current.Properties["detPEdido"] = DataDetprod;
                    var SiNo = new DialogDetPedido { };
                    var x = await DialogHost.Show(SiNo, "RootDialog");
                }
            }
            catch (Exception ex)
            {
            }
        }
        private async void BoletaElectronica()
        {
            try
            {
                if (Pedido.Count != 0)
                {
                    int id = this.Pedido.ToList().FirstOrDefault().DP_ID_PED;
                    ValidarTieneDoc(id);
                    Application.Current.Properties["IdPedidoDoc"] = id;
                    Application.Current.Properties["TipDocElectronico"] = 1;
                    Application.Current.Properties["IdClienteDel"] = this.IdCliente;
                    Application.Current.Properties["ClienteLlevar"] = this.NomCliente;
                    var BoletaElectronica = new DialogFacturacionElectronica { };
                    var x3 = await DialogHost.Show(BoletaElectronica, "RootDialog");
                }
                else
                {
                    var Dialog = new MessageDialog { Mensaje = { Text = "Debe haber pedidos" } };
                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                    return;
                }
            }
            catch (Exception ex)
            {
                //globales.Mensaje("SOS-FOOD - Error", ex.Message, 2);
            }
        }
        private async void FacturaElectronica()
        {
            try
            {
                if (Pedido.Count != 0)
                {
                    int id = this.Pedido.ToList().FirstOrDefault().DP_ID_PED;
                    ValidarTieneDoc(id);
                    Application.Current.Properties["IdPedidoDoc"] = id;
                    Application.Current.Properties["TipDocElectronico"] = 2;
                    Application.Current.Properties["IdClienteDel"] = this.IdCliente;
                    Application.Current.Properties["ClienteLlevar"] = this.NomCliente;
                    var FacturaElectronica = new DialogFacturacionElectronica { };
                    var x3 = await DialogHost.Show(FacturaElectronica, "RootDialog");
                }
                else
                {
                    var Dialog = new MessageDialog { Mensaje = { Text = "Debe haber pedidos" } };
                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                    return;
                }
            }
            catch (Exception ex)
            {
                // globales.Mensaje("SOS-FOOD - Error", ex.Message, 2);
            }
        }
        private void ValidarTieneDoc(int id_pedido)
        {
            DataTable doc_x_pedido = negPedido.getBoletaxPedido(id_pedido);
            if (doc_x_pedido.Rows.Count > 0)
            {
                String boletas = "";
                for (int i = 0; i < doc_x_pedido.Rows.Count; i++)
                {
                    if (boletas == "")
                    {
                        boletas = doc_x_pedido.Rows[i]["serieNumero"].ToString();
                    }
                    else
                    {
                        boletas = boletas + ", " + doc_x_pedido.Rows[i]["serieNumero"].ToString();
                    }
                }
                globales.Mensaje("SOS-FOOD Mensaje:", "Este pedido tiene documento(s) emitido(s): \n " + boletas, 2);
            }
        }
        private async void AnularPed()
        {
            if (Pedido.Count != 0)
            {
                Application.Current.Properties["EstPedidoAnulacion"] = "PENDIENTE";
                int id = this.Pedido.ToList().FirstOrDefault().DP_ID_PED;
                Application.Current.Properties["OperPagoCel"] = "ANULAR PEDIDO";
                Application.Current.Properties["RegistrarPedido"] = "1";
                Application.Current.Properties["EmpleadoMozo"] = this.Nombre_Mozo;
                DialogHost.CloseDialogCommand.Execute(null, null);
                var opencampago = new DialogAnularPedido
                {

                };
                var x3 = await DialogHost.Show(opencampago, "RootDialog");

                if (Application.Current.Properties["AnularPedido"] != null)
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    var SiNo = new SiNoMessageDialog
                    {
                        Mensaje = { Text = "Esta seguro de eliminar el pedido" }
                    };
                    var x = await DialogHost.Show(SiNo, "RootDialog");
                    if ((bool)x)
                    {
                        if (Application.Current.Properties["ComentarioPLato"] == null)
                        {
                            Application.Current.Properties["ComentarioPLato"] = "Error Digitacion";
                        }
                        string comentario = Application.Current.Properties["ComentarioPLato"].ToString();
                        Pagar pagar = new Pagar();
                        pagar.comentario = comentario;
                        pagar.idpedido = id.ToString();
                        string _mensaje = "";
                        int idusuarioanulacion;
                        idusuarioanulacion = Convert.ToInt32(Application.Current.Properties["IdUsuarioAnulacion"]);
                        bool result = negPagar.SetAnularPedido(1, pagar, ref _mensaje, idusuarioanulacion);
                        if (result)
                        {
                            if (negParametros.ImpComAnuladaPlato())
                            {
                                ImprimirComandaAnulada(id);
                            }
                            if (negParametros.ImpComAnuladaCaja())
                            {
                                ImpComandaAnuladoCaja(id);
                            }
                            //ImprimirComandaAnuladaRecutacu(id);
                            //ImpComandaAnuladoCajaRecutacu(id);
                            Cancelar();
                            directoryStructure = new AmbientesStructure();
                            if (Application.Current.Properties["IdAmbiente"] == null)
                            {
                                int idAmbiente = DataAmbientes.FirstOrDefault().ID;
                                Application.Current.Properties["IdAmbiente"] = idAmbiente.ToString();
                            }
                            GetMesas(Application.Current.Properties["IdAmbiente"].ToString());
                            //DialogHost.CloseDialogCommand.Execute(null, null);


                            if (Enviar_Correo_Anulacion == true)
                            {

                            // EnvioAnulacionPedido
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            string json = "";
                            empresa = negEmpresa.GetEmpresa();
                            json = negCierre.GetCierreJson();

                            using (var client = new WebClient())
                            {
                                var values = new NameValueCollection();
                                values["id_empresa"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo.ToString();
                                values["fecha"] = DateTime.Now.ToString("yyyy/MM/dd");
                                values["nombre_empresa"] = empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().empr_nom.ToString();
                                values["correo"] = json;
                                values["comentario"] = comentario;
                                values["nombre_empleado"] = Application.Current.Properties["NomUsuario"].ToString();
                                values["cod_pedido"] = id.ToString();
                                var response = client.UploadValues(negParametros.UploadAnulacion(), values);
                                var responseString = Encoding.Default.GetString(response);
                            }


                            }


                        }
                    }
                }
            }
            else
            {
                var Dialog = new MessageDialog
                {
                    Mensaje = { Text = "Debe haber pedidos para pedido" }
                };
                var x2 = await DialogHost.Show(Dialog, "RootDialog");
                Application.Current.Properties["RegistrarPedido"] = null;
                Application.Current.Properties["AnularPedido"] = null;
            }
        }
        private async void QuitarDescuento()
        {
            if (Pedido.Count != 0)
            {

                if (this.DescuentoPedido == "DESCUENTO : S/. 0.00")
                {
                    var Dialog = new MessageDialog
                    {
                        Mensaje = { Text = "No hay descuentos por quitar" }
                    };
                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                    return;
                }
                else
                {
                    var Dialog = new SiNoMessageDialog
                    {
                        Mensaje = { Text = "Desea quitar el descuento?" }
                    };
                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                    if ((bool)x2)
                    {
                        int id = this.Pedido.ToList().FirstOrDefault().DP_ID_PED;
                        bool result = negPagar.QuitarDescuento(id);
                        if (result)
                        {

                            this.Pedido = negPedido.GetPedidoxId(Convert.ToInt32(id));
                            if (this.Pedido != null && this.Pedido.Count != 0)
                            {
                                this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                                this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                                this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                                this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                                TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                                this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                                this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                                this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                                this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                            }
                            else
                            {
                                this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                                this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                                this.TotalPedido = "TOTAL : S/. 0.00";
                                this.Fecha_Registro = "";
                                this.Fecha_Transcurrido = "";
                                this.Nombre_Mozo = "";
                                this.Nro_Personas = "1";
                                this.NroDiario = "";
                            }
                        }
                    }
                }

            }
            else
            {
                var Dialog2 = new MessageDialog
                {
                    Mensaje = { Text = "Esta mesa no tiene pedidos" }
                };
                var x = await DialogHost.Show(Dialog2, "RootDialog");
            }
        }
        private async void CambiarMesa()
        {
            if (Pedido.Count != 0)
            {
                Application.Current.Properties["Id_PedidoCambio"] = this.Pedido.ToList().FirstOrDefault().DP_ID_PED;
                Application.Current.Properties["CodMesaCambio"] = Application.Current.Properties["CodMesa"];
                Application.Current.Properties["TituloDialogCambiarUnificarMesa"] = "CAMBIAR MESA";
                var DialogCambiarMesa = new DialogCambiarMesa
                {

                };
                var x = await DialogHost.Show(DialogCambiarMesa, "RootDialog");
                if (Application.Current.Properties["CambioMesa"] != null)
                {
                    Cancelar();
                    directoryStructure = new AmbientesStructure();
                    if (Application.Current.Properties["IdAmbiente"] == null)
                    {
                        int idAmbiente = DataAmbientes.FirstOrDefault().ID;
                        Application.Current.Properties["IdAmbiente"] = idAmbiente.ToString();
                    }
                    GetMesas(Application.Current.Properties["IdAmbiente"].ToString());
                }
            }
            else
            {
                var Dialog2 = new MessageDialog
                {
                    Mensaje = { Text = "Esta mesa no tiene pedidos" }
                };
                var x = await DialogHost.Show(Dialog2, "RootDialog");
            }
        }
        public class CantidadCommand : ICommand
        {
            private Action m_Action;
            public CantidadCommand(Action action)
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
        public class ColumnaCommand : ICommand
        {
            private Action m_Action;
            public ColumnaCommand(Action action)
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
        private void Command(object text)
        {
            DataChips = text.ToString();
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = text.ToString() }
            };
            DialogHost.Show(SiNo, "RootDialog");
        }
        private void Cancelar()
        {
            try
            {
                this.Operacion = "Lista";
                this.NombreMesa = "";
                //var categoriacarta = negsCatgCart.GetCategoria();
                //this.DatasCategoriacarta = new ObservableCollection<CategoriaCartaItemViewModel>(
                //    categoriacarta.Select(ct => new CategoriaCartaItemViewModel(ct.idcat, ct.idscat, ct.nomscat, ct.nomcat, ct.desccat, ct.imagencat)));
                this.CodGrupo = "0";
                this.DataPedido = new ObservableCollection<Platos>();
                this.DataPlatos = new ObservableCollection<Platos>();
                this.Pedido = new ObservableCollection<Pedidos>();
                this.total = "TOTAL : S/. 0.00";
                this.MesaPadre = "";
                this.Mesa = new Mesa();
                Application.Current.Properties["MesaBloqueada"] = "NO";
                negPedido.ActualizarMesaLibre(Convert.ToInt32(Application.Current.Properties["CodMesa"]));
                directoryStructure = new AmbientesStructure();
                if (Application.Current.Properties["IdAmbiente"] == null)
                {
                    int idAmbiente = DataAmbientes.FirstOrDefault().ID;
                    Application.Current.Properties["IdAmbiente"] = idAmbiente.ToString();
                }
                GetMesas(Application.Current.Properties["IdAmbiente"].ToString());
                Application.Current.Properties["Orden"] = null;
                HabilitarPedir = "True";
            }
            catch (Exception ex)
            {
            }
        }
        private void cambiarEstDelivery()
        {
            if (Pedido != null)
            {
                if (Pedido.Count > 0)
                {
                    string id_pedido = Pedido.FirstOrDefault().DP_ID_PED.ToString();
                    DataTable dt = negPedido.GetDeliveryxPedidoApp(Convert.ToInt32(id_pedido));
                    if (dt == null)
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Debe ser un pedido de la App SosDelivery Peru.", 2);
                        return;
                    }
                    if (dt.Rows.Count == 0)
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Debe ser un pedido de la App SosDelivery Peru.", 2);
                        return;
                    }
                    empresa = negEmpresa.GetEmpresa();
                    if (this.NombreMesa.Contains("LLEVAR"))
                    {
                        int Desc;
                        string verificar = InternetGetConnectedState(out Desc, 0).ToString();
                        if ((Convert.ToBoolean(verificar) == true))
                        {
                            using (var client = new WebClient())
                            {
                                var values = new NameValueCollection();
                                //values["token"] = "app963";
                                values["orderid"] = dt.Rows[0]["id"].ToString();
                                values["id_business"] = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().codigo;
                                values["status"] = "SU PEDIDO ESTA LISTO PARA RECOGER";
                                var response = client.UploadValues(negParametros.UpdateState(), values);
                                var responseString = Encoding.Default.GetString(response);
                                negDeliveryWebService.sp_cambiar_estado_pedido_delivery(Convert.ToInt32(dt.Rows[0]["id"]), "SU PEDIDO ESTA LISTO PARA RECOGER");
                                globales.Mensaje("SOS-FOOD - Informacion", "Estado del Pedido : SU PEDIDO ESTA LISTO PARA RECOGER", 2);
                            }
                        }
                        else
                        {
                            globales.Mensaje("SOS-FOOD - Informacion", "No hay conexion a internet", 2);
                        }

                    }
                    if (this.NombreMesa.Contains("DELIVERY"))
                    {
                        int Desc;
                        string verificar = InternetGetConnectedState(out Desc, 0).ToString();
                        if ((Convert.ToBoolean(verificar) == true))
                        {
                            using (var client = new WebClient())
                            {
                                var values = new NameValueCollection();
                                //values["token"] = "app963";
                                values["orderid"] = dt.Rows[0]["id"].ToString();
                                values["id_business"] = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().codigo;
                                values["status"] = "SU PEDIDO ESTA EN CAMINO";
                                var response = client.UploadValues(negParametros.UpdateState(), values);
                                var responseString = Encoding.Default.GetString(response);
                                negDeliveryWebService.sp_cambiar_estado_pedido_delivery(Convert.ToInt32(dt.Rows[0]["id"]), "SU PEDIDO ESTA EN CAMINO");
                                globales.Mensaje("SOS-FOOD - Informacion", "Estado del Pedido : SU PEDIDO ESTA EN CAMINO", 2);
                            }
                        }
                        else
                        {
                            globales.Mensaje("SOS-FOOD - Informacion", "No hay conexion a internet", 2);
                        }
                    }


                }
                else
                {
                    globales.Mensaje("SOS-FOOD - Informacion", "Debe haber pedidos.", 2);
                }
            }
            else
            {
                globales.Mensaje("SOS-FOOD - Informacion", "Debe haber pedidos.", 2);
            }
        }
        private void GenerarQR()
        {
            DataTable impresora = new DataTable();
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            impresora = negPlatos.GetImpresoraxDocumento(1); //Precuenta
            string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
            for (int i = 0; i < impresora.Rows.Count; i++)
            {
                empresa = new ObservableCollection<Empresa>();
                correo = new ObservableCollection<Empresa>();
                empresa = negEmpresa.GetEmpresa();
                List<MesasItem> mesa = directoryStructure.GetMesas();
                int qrBackColor = Color.FromArgb(255, 255, 255, 255).ToArgb();
                int qrForeColor = Color.FromArgb(255, 0, 0, 0).ToArgb();
                Ticket ticket = new Ticket();

                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                ticket.AddSubHeaderLine("MESA: " + this.NombreMesa);
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

                qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
                // qrCodeEncoder.QRCodeEncodeMode = Codec.QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC
                qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M;
                qrCodeEncoder.QRCodeScale = Convert.ToInt32(4);
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor);
                qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor);

                string textoQR = "";
                textoQR += empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo + "/";
                textoQR += empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().empr_nom_com + "/";
                textoQR += mesa.Where(m => m.M_NOM == this.NombreMesa).FirstOrDefault().ID + "/";
                textoQR += "SF_" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                //textoQR &= dt(0)("razonSocialReceptor").ToString & "|"
                //textoQR += dt(0)("hash").ToString;

                Bitmap bitmap = qrCodeEncoder.Encode(textoQR, System.Text.Encoding.UTF8);
                ticket.QrImage = bitmap;
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
        private async void NumPersonas()
        {
            if (negParametros.NroPersonas() == "1")
            {
                Application.Current.Properties["NroPersonasActual"] = this.Nro_Personas;
                Application.Current.Properties["TipoNroPersonas"] = "1";
                var nropersonas = new DialogNroPersonas
                {

                };
                var x3 = await DialogHost.Show(nropersonas, "RootDialog");
                if (Application.Current.Properties["NroPersonasNuevo"] != null)
                {
                    this.Nro_Personas = Application.Current.Properties["NroPersonasNuevo"].ToString();
                }
            }
        }
        private async void NomPersonas()
        {
            bool llevar = Convert.ToBoolean(Application.Current.Properties["LlevarCliente"]);
            Application.Current.Properties["NomLlevarCliente"] = this.NomCliente;
            if (llevar == true)
            {
                var nompersonas = new DialogNomPersona
                {

                };
                var x3 = await DialogHost.Show(nompersonas, "RootDialog");
                if (Application.Current.Properties["NomLlevarCliente"] != null)
                {
                    this.NomCliente = Application.Current.Properties["NomLlevarCliente"].ToString();
                    if (Pedido.Count != 0)
                    {
                        negPedido.setNomCliente(Pedido.FirstOrDefault().DP_ID_PED, NomCliente);
                    }
                }
            }
        }
        private async void NomMotorizado()
        {
            if (this.NombreMesa.Contains("DELIVERY"))
            {
                if (Pedido.Count != 0)
                {
                    var nommotorizado = new DialogMotorizado
                    {

                    };
                    var x3 = await DialogHost.Show(nommotorizado, "RootDialog");
                    if (Application.Current.Properties["GuardaMotorizado"] != null)
                    {
                        if ((bool)Application.Current.Properties["GuardaMotorizado"] == true)
                        {
                            this.Motorizado = Application.Current.Properties["NomMotorizado"].ToString();
                            if (Pedido.Count != 0)
                            {
                                negPedido.setMotorizado(Pedido.FirstOrDefault().DP_ID_PED, Motorizado);
                            }
                        }
                    }
                }
            }
        }
        private async void TelPersonas()
        {
            bool llevar = Convert.ToBoolean(Application.Current.Properties["LlevarCliente"]);
            Application.Current.Properties["TelLlevarCliente"] = this.TelCliente;
            if (llevar == true)
            {
                var nompersonas = new DialogTelPersona
                {

                };
                var x3 = await DialogHost.Show(nompersonas, "RootDialog");
                if (Application.Current.Properties["TelLlevarCliente"] != null)
                {
                    this.TelCliente = Application.Current.Properties["TelLlevarCliente"].ToString();
                }
            }
        }
        private async void MoverPlatos(object id)
        {

            if (Pedido.Count != 0)
            {
                if (this.DescuentoPedido != "DESCUENTO : S/. 0.00")
                {
                    var Dialog = new MessageDialog
                    {
                        Mensaje = { Text = "No debe haber DESCUENTO para trasladar platos." }
                    };
                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                    return;
                }
                DataTable dt4 = new DataTable();
                dt4 = negPedido.sp_verificar_plato_cuenta(Convert.ToInt32(id));

                if (dt4 != null)
                {
                    if (dt4.Rows.Count > 0)
                    {
                        var Dialog = new MessageDialog
                        {
                            Mensaje = { Text = "No puede mover un plato que esta vinculado a una SubCuenta.En todo caso elimine la subcuenta asociada." }
                        };
                        var x2 = await DialogHost.Show(Dialog, "RootDialog");
                        return;
                    }
                }
                if (Pedido.Count == 1)
                {
                    if (Pedido.ToList().First().DP_CANT > 1)
                    {
                        if (negParametros.ClaveMozoMoverPlato() == "1")
                        {
                            Application.Current.Properties["OperPagoCel"] = "ANULAR PLATO";
                            DialogHost.CloseDialogCommand.Execute(null, null);
                            var opencampago = new DialogAnularPedido
                            {

                            };
                            var x3 = await DialogHost.Show(opencampago, "RootDialog");
                            if (Application.Current.Properties["AnularPedido"] == null)
                            {
                                return;
                            }
                        }
                        Application.Current.Properties["IdMesaBase"] = Convert.ToInt32(CodMesa);
                        var cambiomesa = new DialogComboMesa
                        {

                        };
                        var x5 = await DialogHost.Show(cambiomesa, "RootDialog");
                        if (Application.Current.Properties["IdMesaSeleccionada"] == null)
                        {
                            return;
                        }
                        decimal cantidad = this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault().DP_CANT;
                        if (cantidad > 1)
                        {
                            Application.Current.Properties["CantidadCombo"] = cantidad;
                            Application.Current.Properties["Solo"] = "SI";
                            var combocantidad = new DialogComboCantidad
                            {

                            };
                            var x4 = await DialogHost.Show(combocantidad, "RootDialog");
                            if (Application.Current.Properties["CantidadComboAnulado"] == null)
                            {
                                DialogHost.CloseDialogCommand.Execute(null, null);
                                return;
                            }
                            else
                            {
                                DialogHost.CloseDialogCommand.Execute(null, null);
                                var SiNo = new SiNoMessageDialog
                                {
                                    Mensaje = { Text = "Esta seguro de mover el plato?" }
                                };
                                var x = await DialogHost.Show(SiNo, "RootDialog");
                                if ((bool)x)
                                {
                                    if (this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault() != null)
                                    {
                                        int idPed = this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault().DP_ID_PED;
                                        bool result = false;
                                        string _mensaje = "";
                                        int idusuarioanulacion;
                                        idusuarioanulacion = Convert.ToInt32(Application.Current.Properties["IdUsuarioAnulacion"]);
                                        if (Application.Current.Properties["TipoMesa"].ToString() == "OCUPADO")
                                        {
                                            result = negPedido.CambiarPlatoMesa(Convert.ToInt32(id), Convert.ToDecimal(Application.Current.Properties["CantidadComboAnulado"]), idusuarioanulacion, Convert.ToInt32(Application.Current.Properties["IdMesaSeleccionada"]));
                                        }
                                        else
                                        {
                                            result = negPedido.QuitarPlatoMesa(Convert.ToInt32(id), Convert.ToDecimal(Application.Current.Properties["CantidadComboAnulado"]), idusuarioanulacion, Convert.ToInt32(Application.Current.Properties["IdMesaSeleccionada"]));
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
                                            foreach (var item in this.Pedido.Where(w => w.ID == (int)id))
                                            {
                                                dt.Rows.Add(item.DP_ID_CARTA, item.DP_DESCRIP, Application.Current.Properties["CantidadComboAnulado"], item.DP_PRE_UNI, 0, item.DP_PRE_UNI * Convert.ToDecimal(Application.Current.Properties["CantidadComboAnulado"]), "", item.DP_ID_CARTA_SN, item.EST_ENTREGADO);
                                            }
                                            string _mensaje2 = "";
                                            string opcion = "I";

                                            Application.Current.Properties["NroPersonasActual"] = this.Nro_Personas;

                                            int result2 = negPedido.SetPedidoSinInsumo(Convert.ToInt32(Application.Current.Properties["IdMesaSeleccionada"]), Convert.ToInt32(Application.Current.Properties["IdUsuario"]), Convert.ToInt32(Application.Current.Properties["IdEmpleado"]), Convert.ToInt32(IdCliente), dt, opcion, this.Nro_Personas, isReserva, ref _mensaje2, Convert.ToInt32(id), this.NomCliente, this.TelCliente,NombreEquipo,Motorizado);
                                            if (result2 != 0)
                                            {

                                            }
                                            else
                                            {
                                                globales.Mensaje("SOS-FOOD - Informacion", "Error al insertar el pedido", 2);
                                            }
                                        }


                                        if (result == true)
                                        {
                                            this.Pedido = negPedido.GetPedidoxId(Convert.ToInt32(idPed));
                                            if (this.Pedido != null && this.Pedido.Count != 0)
                                            {
                                                this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                                                this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                                                this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                                                this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                                                TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                                                this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                                                this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                                                this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                                                this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                                            }
                                            else
                                            {
                                                this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                                                this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                                                this.TotalPedido = "TOTAL : S/. 0.00";
                                                this.Fecha_Registro = "";
                                                this.Fecha_Transcurrido = "";
                                                this.Nombre_Mozo = "";
                                                this.Nro_Personas = "1";
                                                this.NroDiario = "";
                                            }
                                        }
                                    }
                                    Application.Current.Properties["AnularPedido"] = null;

                                }
                            }
                        }
                        else
                        {
                            var Dialog = new MessageDialog
                            {
                                Mensaje = { Text = "Solo queda un plato" }
                            };
                            var x2 = await DialogHost.Show(Dialog, "RootDialog");
                        }

                    }
                    else
                    {
                        var Dialog = new MessageDialog
                        {
                            Mensaje = { Text = "Solo queda un plato" }
                        };
                        var x2 = await DialogHost.Show(Dialog, "RootDialog");
                    }

                }
                else
                {
                    if (negParametros.ClaveMozoMoverPlato() == "1")
                    {
                        Application.Current.Properties["OperPagoCel"] = "ANULAR PLATO";
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        var opencampago = new DialogAnularPedido
                        {

                        };
                        var x3 = await DialogHost.Show(opencampago, "RootDialog");
                        if (Application.Current.Properties["AnularPedido"] == null)
                        {
                            return;
                        }
                    }
                    Application.Current.Properties["IdMesaBase"] = Convert.ToInt32(CodMesa);
                    var cambiomesa = new DialogComboMesa
                    {

                    };
                    var x5 = await DialogHost.Show(cambiomesa, "RootDialog");
                    if (Application.Current.Properties["IdMesaSeleccionada"] == null)
                    {
                        return;
                    }

                    decimal cantidad = this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault().DP_CANT;
                    if (cantidad > 1)
                    {
                        Application.Current.Properties["CantidadCombo"] = cantidad;
                        Application.Current.Properties["Solo"] = "NO";
                        var combocantidad = new DialogComboCantidad
                        {

                        };
                        var x4 = await DialogHost.Show(combocantidad, "RootDialog");
                        if (Application.Current.Properties["CantidadComboAnulado"] == null)
                        {
                            DialogHost.CloseDialogCommand.Execute(null, null);
                            return;
                        }
                        else
                        {
                            DialogHost.CloseDialogCommand.Execute(null, null);
                            var SiNo = new SiNoMessageDialog
                            {
                                Mensaje = { Text = "Esta seguro de mover el plato" }
                            };
                            var x = await DialogHost.Show(SiNo, "RootDialog");
                            if ((bool)x)
                            {
                                if (this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault() != null)
                                {
                                    int idPed = this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault().DP_ID_PED;
                                    bool result = false;
                                    string _mensaje = "";
                                    int idusuarioanulacion;
                                    idusuarioanulacion = Convert.ToInt32(Application.Current.Properties["IdUsuarioAnulacion"]);
                                    if (Application.Current.Properties["TipoMesa"].ToString() == "OCUPADO")
                                    {
                                        result = negPedido.CambiarPlatoMesa(Convert.ToInt32(id), Convert.ToDecimal(Application.Current.Properties["CantidadComboAnulado"]), idusuarioanulacion, Convert.ToInt32(Application.Current.Properties["IdMesaSeleccionada"]));
                                    }
                                    else
                                    {
                                        result = negPedido.QuitarPlatoMesa(Convert.ToInt32(id), Convert.ToDecimal(Application.Current.Properties["CantidadComboAnulado"]), idusuarioanulacion, Convert.ToInt32(Application.Current.Properties["IdMesaSeleccionada"]));
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
                                        foreach (var item in this.Pedido.Where(w => w.ID == (int)id))
                                        {
                                            dt.Rows.Add(item.DP_ID_CARTA, item.DP_DESCRIP, Application.Current.Properties["CantidadComboAnulado"], item.DP_PRE_UNI, 0, item.DP_PRE_UNI * Convert.ToDecimal(Application.Current.Properties["CantidadComboAnulado"]), "", item.DP_ID_CARTA_SN, item.EST_ENTREGADO);
                                        }
                                        string _mensaje2 = "";
                                        string opcion = "I";

                                        Application.Current.Properties["NroPersonasActual"] = this.Nro_Personas;

                                        int result2 = negPedido.SetPedidoSinInsumo(Convert.ToInt32(Application.Current.Properties["IdMesaSeleccionada"]), Convert.ToInt32(Application.Current.Properties["IdUsuario"]), Convert.ToInt32(Application.Current.Properties["IdEmpleado"]), Convert.ToInt32(IdCliente), dt, opcion, this.Nro_Personas, isReserva, ref _mensaje2, Convert.ToInt32(id), this.NomCliente, this.TelCliente, NombreEquipo, Motorizado);
                                        if (result2 != 0)
                                        {

                                        }
                                        else
                                        {
                                            globales.Mensaje("SOS-FOOD - Informacion", "Error al insertar el pedido", 2);
                                        }
                                    }

                                    if (result == true)
                                    {

                                        this.Pedido = negPedido.GetPedidoxId(Convert.ToInt32(idPed));
                                        if (this.Pedido != null && this.Pedido.Count != 0)
                                        {
                                            this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                                            this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                                            this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                                            this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                                            TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                                            this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                                            this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                                            this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                                            this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                                        }
                                        else
                                        {
                                            this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                                            this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                                            this.TotalPedido = "TOTAL : S/. 0.00";
                                            this.Fecha_Registro = "";
                                            this.Fecha_Transcurrido = "";
                                            this.Nombre_Mozo = "";
                                            this.Nro_Personas = "1";
                                            this.NroDiario = "";
                                        }
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        var SiNo = new SiNoMessageDialog
                        {
                            Mensaje = { Text = "Esta seguro de mover el plato" }
                        };
                        var x = await DialogHost.Show(SiNo, "RootDialog");
                        if ((bool)x)
                        {
                            if (this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault() != null)
                            {
                                int idPed = this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault().DP_ID_PED;
                                bool result = false;
                                string _mensaje = "";
                                int idusuarioanulacion;
                                idusuarioanulacion = Convert.ToInt32(Application.Current.Properties["IdUsuarioAnulacion"]);
                                Application.Current.Properties["CantidadComboAnulado"] = this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault().DP_CANT;
                                if (Application.Current.Properties["TipoMesa"].ToString() == "OCUPADO")
                                {
                                    result = negPedido.CambiarPlatoMesa(Convert.ToInt32(id), Convert.ToDecimal(Application.Current.Properties["CantidadComboAnulado"]), idusuarioanulacion, Convert.ToInt32(Application.Current.Properties["IdMesaSeleccionada"]));
                                }
                                else
                                {
                                    result = negPedido.QuitarPlatoMesa(Convert.ToInt32(id), Convert.ToDecimal(Application.Current.Properties["CantidadComboAnulado"]), idusuarioanulacion, Convert.ToInt32(Application.Current.Properties["IdMesaSeleccionada"]));
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
                                    foreach (var item in this.Pedido.Where(w => w.ID == (int)id))
                                    {
                                        dt.Rows.Add(item.DP_ID_CARTA, item.DP_DESCRIP, Application.Current.Properties["CantidadComboAnulado"], item.DP_PRE_UNI, 0, item.DP_PRE_UNI * Convert.ToDecimal(Application.Current.Properties["CantidadComboAnulado"]), "", item.DP_ID_CARTA_SN, item.EST_ENTREGADO);
                                    }
                                    string _mensaje2 = "";
                                    string opcion = "I";
                                    Application.Current.Properties["NroPersonasActual"] = this.Nro_Personas;
                                    int result2 = negPedido.SetPedidoSinInsumo(Convert.ToInt32(Application.Current.Properties["IdMesaSeleccionada"]), Convert.ToInt32(Application.Current.Properties["IdUsuario"]), Convert.ToInt32(Application.Current.Properties["IdEmpleado"]), Convert.ToInt32(IdCliente), dt, opcion, this.Nro_Personas, isReserva, ref _mensaje2, Convert.ToInt32(id), this.NomCliente, this.TelCliente, NombreEquipo, Motorizado);
                                    if (result2 != 0) { }
                                    else
                                    {
                                        globales.Mensaje("SOS-FOOD - Informacion", "Error al insertar el pedido", 2);
                                    }
                                }

                                if (result == true)
                                {
                                    this.Pedido = negPedido.GetPedidoxId(Convert.ToInt32(idPed));
                                    if (this.Pedido != null && this.Pedido.Count != 0)
                                    {
                                        this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                                        this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                                        this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                                        this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                                        TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                                        this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                                        this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                                        this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                                        this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                                    }
                                    else
                                    {
                                        this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                                        this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                                        this.TotalPedido = "TOTAL : S/. 0.00";
                                        this.Fecha_Registro = "";
                                        this.Fecha_Transcurrido = "";
                                        this.Nombre_Mozo = "";
                                        this.Nro_Personas = "1";
                                        this.NroDiario = "";
                                    }
                                }
                            }
                            Application.Current.Properties["AnularPedido"] = null;
                        }
                    }

                }
            }
            else
            {
                var Dialog = new MessageDialog
                {
                    Mensaje = { Text = "Debe haber pedidos para pedido" }
                };
                var x2 = await DialogHost.Show(Dialog, "RootDialog");
            }
            Application.Current.Properties["AnularPedido"] = null;
        }
        private async void ResetPlato(object id)
        {
            int id_carta = this.Pedido.Where(p => p.ID == Convert.ToInt32(id)).FirstOrDefault().DP_ID_CARTA;
            decimal pre_carta = this.Pedido.Where(p => p.ID == Convert.ToInt32(id)).FirstOrDefault().DP_PRE_UNI;
            this.DataPlatosBusqueda = negPlatos.GetPlatosActivos();
            if (Convert.ToDecimal(this.DataPlatosBusqueda.Where(c=>c.idplato== id_carta).FirstOrDefault().precplato) != pre_carta)
            {
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "El precio a nivel de plato ha sufrido una variación. ¿Desea regresarlo a su valor original?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if ((bool)x)
            {
                bool todogood = negPedido.ResetearPlatoPedido(Convert.ToInt32(id));
                if (todogood == false)
                {
                    globales.Mensaje("SOS-FOOD - Error: ", "No se pudo resetar este plato", 3);
                    return;
                }
                else
                {
                    this.Pedido = negPedido.GetPedidoxId(this.Pedido.FirstOrDefault().DP_ID_PED);
                    if (this.Pedido != null && this.Pedido.Count != 0)
                    {
                        this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                        this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                        this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                        this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                        TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                        this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                        this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                        this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                        this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                    }
                    else
                    {
                        this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                        this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                        this.TotalPedido = "TOTAL : S/. 0.00";
                        this.Fecha_Registro = "";
                        this.Fecha_Transcurrido = "";
                        this.Nombre_Mozo = "";
                        this.Nro_Personas = "1";
                        this.NroDiario = "";
                    }
                }
            }
            }
        }

        private void AgregarPrioridad()
        {
            if (PrioridadText.Trim() == "")
            {
                return;
            }
            bool x = negPedido.SetPrioridades(PrioridadText);
            if (x)
            {
                PrioridadText = "";
                this.DataPrioridades = negPedido.GetPrioridades();
            }
            else
            {
                return;
            }
        }
        private async void DescPlatos(object id)
        {
            DataTable dt4 = new DataTable();
            dt4 = negPedido.sp_verificar_plato_cuenta(Convert.ToInt32(id));

            if (dt4 != null)
            {
                if (dt4.Rows.Count > 0)
                {
                    var Dialog = new MessageDialog
                    {
                        Mensaje = { Text = "No puede hacer un descuento a un plato que esta vinculado a una SubCuenta.En todo caso elimine la subcuenta asociada." }
                    };
                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                    return;
                }
            }
            decimal descuento_det = this.Pedido.Where(w => w.ID == Convert.ToInt32(id)).FirstOrDefault().DP_DESCU;
            if (descuento_det > 0)
            {
                var SiNo = new SiNoMessageDialog
                {
                    Mensaje = { Text = "El plato ya tiene descuento ¿Desea quitarlo?" }
                };
                var x = await DialogHost.Show(SiNo, "RootDialog");
                if ((bool)x)
                {
                    bool todobien = negPedido.QuitarDescuentoPlato(Convert.ToInt32(id));
                    if (todobien)
                    {
                        this.Pedido = negPedido.GetPedidoxId(this.Pedido.FirstOrDefault().DP_ID_PED);
                        if (this.Pedido != null && this.Pedido.Count != 0)
                        {
                            this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                            this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                            this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                            this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                            TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                            this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                            this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                            this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                            this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                        }
                        else
                        {
                            this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                            this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                            this.TotalPedido = "TOTAL : S/. 0.00";
                            this.Fecha_Registro = "";
                            this.Fecha_Transcurrido = "";
                            this.Nombre_Mozo = "";
                            this.Nro_Personas = "1";
                            this.NroDiario = "";
                        }
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Error: ", "No se pudo quitar el descuento del plato", 3);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }


            Application.Current.Properties["OperacionDetPago"] = "CAMBIAR PRECIO PLATO";
            Application.Current.Properties["IdDetPedido"] = id;
            Application.Current.Properties["PrePlato"] = this.Pedido.Where(p => p.ID == Convert.ToInt32(id)).FirstOrDefault().DP_IMPORT;
            var descuento = new DialogCambiarPrecio { };
            var x3 = await DialogHost.Show(descuento, "RootDialog");
            if (Application.Current.Properties["DescuentoCorrecto"] != null)
            {
                this.Pedido = negPedido.GetPedidoxId(this.Pedido.FirstOrDefault().DP_ID_PED);
                if (this.Pedido != null && this.Pedido.Count != 0)
                {
                    this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                    this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                    this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                    this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                    TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                    this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                    this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                    this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                    this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                }
                else
                {
                    this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                    this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                    this.TotalPedido = "TOTAL : S/. 0.00";
                    this.Fecha_Registro = "";
                    this.Fecha_Transcurrido = "";
                    this.Nombre_Mozo = "";
                    this.Nro_Personas = "1";
                    this.NroDiario = "";
                }
            }
        }
        private async void DescuenPedido()
        {
            if (this.Pedido != null && this.Pedido.Count != 0)
            {
                Application.Current.Properties["TotalPedido"] = this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                Application.Current.Properties["Id_Pedido"] = this.Pedido.ToList().FirstOrDefault().DP_ID_PED.ToString();
                Application.Current.Properties["SubTotalPedido"] = this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                Application.Current.Properties["OperacionDetPago"] = "CAMBIAR PRECIO TOTAL";
                var descuento = new DialogCambiarPrecio
                {

                };
                var x3 = await DialogHost.Show(descuento, "RootDialog");
                if (Application.Current.Properties["DescuentoCorrecto"] != null)
                {
                    this.Pedido = negPedido.GetPedidoxId(Convert.ToInt32(Application.Current.Properties["Id_Pedido"]));
                    if (this.Pedido != null && this.Pedido.Count != 0)
                    {
                        this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                        this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                        this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                        this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                        TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                        this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                        this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                        this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                        this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                    }
                    else
                    {
                        this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                        this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                        this.TotalPedido = "TOTAL : S/. 0.00";
                        this.Fecha_Registro = "";
                        this.Fecha_Transcurrido = "";
                        this.Nombre_Mozo = "";
                        this.Nro_Personas = "1";
                        this.NroDiario = "";
                    }
                }
            }

        }
        private async void RealizarPedido()
        {

            CountPrintLlevar = negParametros.CountPrintLlevar();
            CountPrintDelivery = negParametros.CountPrintDelivery();

            try
            {
                HabilitarPedir = "False";
                if (this.DataPedido.Count != 0)
                {
                    if (this.NombreMesa.Contains("LLEVAR"))
                    {
                        if(this.NomCliente.Trim() == "")
                        {
                            HabilitarPedir = "True";
                            var mm = new MessageDialog
                            {
                                Mensaje = { Text = "Debe ingresar el nombre del cliente." }
                            };
                            var awaiting = DialogHost.Show(mm, "RootDialog");
                            return;
                        }
                    }

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
                    foreach (var item in this.DataPedido)
                    {
                        dt.Rows.Add(item.idplato, item.nomplato, item.cantidad, item.precplato, 0, item.importe, item.comentario, item.idCartaSubNivel, 0);
                    }
                    string _mensaje = "";
                    string opcion = "I";
                    if (this.Pedido.Count > 0)
                    {
                        opcion = "U";
                    }
                    else
                    {
                        if (!this.NombreMesa.Contains("DELIVERY") && !this.NombreMesa.Contains("LLEVAR") && !this.NombreMesa.Contains("RECOJO"))
                        {
                            if (negParametros.NroPersonas() == "2")
                            {
                                Application.Current.Properties["NroPersonasActual"] = this.Nro_Personas;
                                Application.Current.Properties["TipoNroPersonas"] = "2";
                                var nropersonas = new DialogNroPersonas { };
                                var x3 = await DialogHost.Show(nropersonas, "RootDialog");
                                if (Application.Current.Properties["NroPersonasNuevo"] != null)
                                {
                                    this.Nro_Personas = Application.Current.Properties["NroPersonasNuevo"].ToString();
                                }
                            }
                        }
                    }
                    bool di = false;
                    if (this.NombreMesa.Contains("DELIVERY"))
                    {
                        if (Pedido.Count == 0)
                        {
                            int l = Convert.ToInt32(this.total.Length);
                            int h = total.IndexOf("S/. ");
                            totaltotal = total.Substring(h + 4);
                            _totaltotal = Convert.ToDecimal(totaltotal);
                            Application.Current.Properties["MontoTotalPedido"] = _totaltotal;
                        }
                        else
                        {
                            int l1 = Convert.ToInt32(this.total.Length);
                            int h1 = total.IndexOf("S/. ");
                            subtotal = total.Substring(h1 + 4);

                            decimal _subtotal = Convert.ToDecimal(subtotal);

                            int l2 = Convert.ToInt32(this.TotalPedido.Length);
                            int h2 = TotalPedido.IndexOf("S/. ");
                            totaltotal = TotalPedido.Substring(h2 + 4);
                            _totaltotal = Convert.ToDecimal(totaltotal);
                            _totaltotal = _totaltotal + _subtotal;
                            Application.Current.Properties["MontoTotalPedido"] = _totaltotal;
                        }


                        var view = new DialogPagoCliente { };
                        var x = await DialogHost.Show(view, "RootDialog");
                        bool validacion = Convert.ToBoolean(Application.Current.Properties["DeliveryMetPago"]);
                        if (validacion)
                        {
                            di = true;
                        }
                    }
                    if (this.NombreMesa.Contains("DELIVERY"))
                    {
                        if (di)
                        {
                            id_tipo_pago = Convert.ToInt32(Application.Current.Properties["TipoPagoDialogPagoCliente"]);
                            nombre_tipo_pago = Application.Current.Properties["TipoPagoNombreDialogPagoCliente"].ToString();
                            monto = Convert.ToDecimal(Application.Current.Properties["MontoDialogPagoCliente"]);
                            vuelto = Convert.ToDecimal(Application.Current.Properties["VueltoDialogPagoCliente"]);


                            if (monto < Convert.ToDecimal(_totaltotal))
                            {
                                HabilitarPedir = "True";
                                var mm = new MessageDialog
                                {
                                    Mensaje = { Text = "El monto a cobrar no puede ser menor al importe total del pedido" }
                                };
                                var awaiting = DialogHost.Show(mm, "RootDialog");
                                return;
                            }
                        }
                        else
                        {
                            HabilitarPedir = "True";
                            return;
                        }
                    }

                    int result = negPedido.SetPedido(Convert.ToInt32(CodMesa), Convert.ToInt32(Application.Current.Properties["IdUsuario"]), Convert.ToInt32(Application.Current.Properties["IdEmpleado"]),
                        Convert.ToInt32(IdCliente), dt, opcion, this.Nro_Personas, isReserva, this.NomCliente, this.TelCliente, this.NombreEquipo, Motorizado, ref _mensaje);

                    if (result != 0)
                    {
                        if (this.NombreMesa.Contains("DELIVERY"))
                        {
                            if (di)
                            {
                                id_tipo_pago = Convert.ToInt32(Application.Current.Properties["TipoPagoDialogPagoCliente"]);
                                nombre_tipo_pago = Application.Current.Properties["TipoPagoNombreDialogPagoCliente"].ToString();
                                monto = Convert.ToDecimal(Application.Current.Properties["MontoDialogPagoCliente"]);
                                vuelto = Convert.ToDecimal(Application.Current.Properties["VueltoDialogPagoCliente"]);

                                if (monto < Convert.ToDecimal(_totaltotal))
                                {
                                    HabilitarPedir = "True";
                                    var mm = new MessageDialog
                                    {
                                        Mensaje = { Text = "El monto a cobrar no puede ser menor al importe total del pedido" }
                                    };
                                    var awaiting = DialogHost.Show(mm, "RootDialog");
                                    return;
                                }
                                if (Pedido.Count == 0)
                                {
                                    negPedido.SetPagoCliente(1, result, id_tipo_pago, monto, vuelto);
                                }
                                else
                                {
                                    negPedido.SetPagoCliente(2, result, id_tipo_pago, monto, vuelto);
                                }
                                idPedido = result;
                                
                                    //Quitar para piscis solo caja imprime automaticamente
                                    ImpCuentaDelivery2(); //Quitar para chinito los olivos
                                 
                            }
                            else
                            {
                                HabilitarPedir = "True";
                                return;
                            }
                        }

                        //Si es con tablet comentar el Imprimir y activar timers
                        
                        Imprimir(result);
                        
                        //ImprimirRecutacu(result);
                        //ImprimirRanchoRobertin(result);
                        string mensaje = "";
                        string mensaje2 = "";
                        foreach (var item in this.DataPedido)
                        {
                            mensaje2 = negInsumo.ALERTA_INSUMO(item.idplato);
                            if (mensaje2.Trim() != "")
                            {
                                if (mensaje.Trim() != "")
                                {
                                    mensaje = mensaje + "\r\n" + mensaje2;
                                }
                                else
                                {
                                    mensaje = mensaje2;
                                }
                            }
                        }
                        if (mensaje.Trim() != "")
                        {
                            var Dialog = new MessageDialog
                            {
                                Mensaje = { Text = mensaje }
                            };
                            var x2 = await DialogHost.Show(Dialog, "RootDialog");
                        }

                        if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                        {
                            HabilitarPedir = "True";
                            this.DataPedido = new ObservableCollection<Platos>();
                            this.DataPlatos = new ObservableCollection<Platos>();
                            this.Pedido = new ObservableCollection<Pedidos>();
                            this.Pedido = negPedido.GetPedidoxId(Convert.ToInt32(result));
                            if (this.Pedido != null && this.Pedido.Count != 0)
                            {
                                string tcambio = negParametros.GetTipoCambio();
                                decimal cambio = Convert.ToDecimal(tcambio);
                                
                                this.TDolar = " TOTAL : $ 0.00";
                                this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                                this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                                this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                                this.TCambio = "T/C : " + tcambio;
                                this.TDolar = " TOTAL : $ " + Convert.ToDecimal(this.Pedido.ToList().FirstOrDefault().PED_TOTAL / cambio).ToString("N2");
                                this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                                TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                                this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                                this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                                this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                                this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                                this.IdCliente = this.Pedido.ToList().FirstOrDefault().PED_ID_CLIENTE.ToString();
                                this.NomCliente = this.Pedido.ToList().FirstOrDefault().C_NOM.ToString();
                                this.NomCliente = this.Pedido.ToList().FirstOrDefault().nomllevar.ToString();
                                this.TelCliente = this.Pedido.ToList().FirstOrDefault().telllevar.ToString();
                                this.ClienteDelivery = "Visible";
                                ImprimirCuenta(this.Pedido.ToList().FirstOrDefault().DP_ID_PED); // Quitar para Chinito los olivos
                               
                                

                                
                            }
                        }
                        else
                        {
                            Cancelar();
                        }
                    }
                }
                else
                {
                    HabilitarPedir = "True";
                    var Dialog = new MessageDialog
                    {
                        Mensaje = { Text = "Debe haber pedidos" }
                    };
                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                    return;
                }
            }
            catch (Exception ex)
            {
                HabilitarPedir = "True";
                //globales.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
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
                int nroPersonas = 0;
                Ticket ticket = new Ticket();
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    idpedido = pedido.Rows[0]["ID"].ToString().ToUpper();
                    if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                    {
                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                        clienteCom = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    else if (this.NombreMesa.Contains("DELIVERY"))
                    {
                        cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                        clienteCom = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                    }
                    else
                    {

                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                        clienteCom = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    nroPersonas = Convert.ToInt32(pedido.Rows[j]["PED_NRO_PERSONAS"]);
                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComanda(pedido.Rows[j]["DP_CANT"].ToString().ToUpper(), descripcion);

                    cod_diarioCom = pedido.Rows[0]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesaCom = pedido.Rows[0]["M_NOM"].ToString().ToUpper();
                    nombre_empleadoCom = pedido.Rows[0]["EMPL_NOM"].ToString().ToUpper();
                }
                //if (cliente.ToString().Trim() != "")
                //{
                //    ticket.AddHeaderLine_2(cliente.ToString());
                //    ticket.AddHeaderLine_2("");
                //}
                ticket.DrawFooter2_Mesa(""); // SALAZAR INFANTAS , CHINITO LOS OLIVOS
                ticket.DrawFooter2_Mesa(nombre_mesa.ToString().ToUpper()); // SALAZAR INFANTAS , CHINITO LOS OLIVOS
                //if (this.NombreMesa.Contains("DELIVERY"))
                //{
                //    ticket.AddSubHeaderLineEnormeComanda("         " + cod_diario.ToUpper());
                //}
                //else {
                //    ticket.AddSubHeaderLine("COD DIARIO Nº:" + cod_diario.ToUpper());
                //}
                ticket.AddSubHeaderLine("COD DIARIO Nº:" + cod_diario.ToUpper());
                ticket.AddSubHeaderLine("PEDIDO Nº: " + idpedido.ToUpper());
                ticket.AddSubHeaderLine("ATENDIDO POR: " + nombre_empleado.ToUpper());
                ticket.AddHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                //ticket.AddFooter2Line("Mozo: " + nombre_empleado.ToUpper());
                //ticket.AddFooter2Line("");

                if (cliente.ToString().Trim() != "")
                {
                    ticket.AddFooter2Line(cliente.ToString().ToUpper());
                    ticket.AddFooter2Line("");
                    ticket.AddFooter2Line(pedido.Rows[0]["C_TEL"].ToString().ToUpper());
                }
                if (negParametros.NroPersonas() == "2")
                {
                    ticket.AddFooter2Line("Nro Personas: " + nroPersonas.ToString());
                }

                try
                {
                    if (ticket.PrinterExists(impresora.Rows[i]["NOMIMP"].ToString()))
                    {
                        ticket.PrintTicket(impresora.Rows[i]["NOMIMP"].ToString());
                    }
                    else
                    {
                        //globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[i]["NOMIMP"].ToString() + " no está disponible.", 2);
                    }
                }
                catch (Exception ex)
                {
                    // globales.Mensaje("SOS-FOOD - Informacion", ex.Message, 2);
                }

            }
            negPedido.ActualizarDetPedidoImp(cod_pedido);
            bool imprimir_subniveles = negParametros.ImprimirSubNiveles();
            if (imprimir_subniveles)
            {
                ObservableCollection<Pedidos> pedido = new ObservableCollection<Pedidos>();
                pedido = negPedido.GetPedidoxId(Convert.ToInt32(cod_pedido));
                string PED_NUM_DIARIO = pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                string EMPL_NOM = pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                if (DataPedido.Count != 0)
                {
                    DataTable impresoras_plato = new DataTable();

                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("CANTIDAD");
                    //dt.Columns.Add("ID_CARTA");
                    //dt.Columns.Add("NOM_CARTA");

                    foreach (var i in DataPedido.Where(w => w.idCartaSubNivel != "").ToList())
                    {
                        string[] idcarta;
                        char[] spearator = { ',' };
                        idcarta = i.idCartaSubNivel.Split(spearator).ToArray();
                        idcarta = idcarta.Distinct().ToArray();
                        //DataRow row = dt.NewRow();
                        //row["CANTIDAD"] = i.cantidad;
                        //row["ID_CARTA"] = i.idCartaSubNivel;
                        //row["NOM_CARTA"] = i.comentario;
                        //dt.Rows.Add(row);
                        for (int p1 = 0; p1 < idcarta.Distinct().Count(); p1++)
                        {
                            impresoras_plato = negPedido.GetImpresoraxPlato(Convert.ToInt32(idcarta[p1].ToString()));
                            negPedido.DescontarStockSubNivel(Convert.ToInt32(idcarta[p1].ToString()));
                            this.DataPlatosActivos = negPlatos.GetPlatosActivos();
                            string nomplat = DataPlatosActivos.Where(w => w.idplato == Convert.ToInt32(idcarta[p1].ToString())).FirstOrDefault().nomplato;
                            for (int h = 0; h < impresoras_plato.Rows.Count; h++)
                            {
                                Ticket ticket2 = new Ticket();
                                //ticket2.AddItemComanda("[1.00]  " + impresoras_plato.Rows[i]["CAR_NOM"].ToString(), ""); // ese es
                                ticket2.AddItemComanda(i.cantidad, i.nomplato + " : " + nomplat); // ese es
                                ticket2.AddHeaderLine_2(clienteCom.ToString());
                                ticket2.AddSubHeaderLine("COD DIARIO Nº:" + PED_NUM_DIARIO);
                                ticket2.AddSubHeaderLine("PEDIDO Nº: " + cod_pedido.ToString().ToUpper());
                                ticket2.AddSubHeaderLine("Atendido Por: " + EMPL_NOM);
                                ticket2.AddHeaderLine("");
                                ticket2.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                                ticket2.AddFooter2Line(nombre_mesaCom.ToString().ToUpper());
                                if (ticket2.PrinterExists(impresoras_plato.Rows[h]["NOMIMP"].ToString()))
                                {
                                    ticket2.PrintTicket(impresoras_plato.Rows[h]["NOMIMP"].ToString());
                                }
                                h = h + 1;
                            }
                        }
                    }

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    impresoras_plato = negPedido.GetImpresoraxPlato(Convert.ToInt32(dt.Rows[i]["ID_CARTA"].ToString()));
                    //    negPedido.DescontarStockSubNivel(Convert.ToInt32(dt.Rows[i]["ID_CARTA"].ToString()));
                    //    for (int h = 0; h < impresoras_plato.Rows.Count; h++)
                    //    {
                    //        Ticket ticket2 = new Ticket();
                    //        //ticket2.AddItemComanda("[1.00]  " + impresoras_plato.Rows[i]["CAR_NOM"].ToString(), ""); // ese es
                    //        ticket2.AddItemComanda(dt.Rows[i]["CANTIDAD"].ToString(), dt.Rows[i]["NOM_CARTA"].ToString()); // ese es
                    //        ticket2.AddHeaderLine_2(clienteCom.ToString());
                    //        ticket2.AddSubHeaderLine("COD DIARIO Nº:" + cod_diarioCom.ToUpper());
                    //        ticket2.AddSubHeaderLine("PEDIDO Nº: " + cod_pedido.ToString().ToUpper());
                    //        ticket2.AddSubHeaderLine("Atendido Por: " + nombre_empleadoCom.ToUpper());
                    //        ticket2.AddHeaderLine("");
                    //        ticket2.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    //        ticket2.AddFooter2Line(nombre_mesaCom.ToString().ToUpper());
                    //        if (ticket2.PrinterExists(impresoras_plato.Rows[h]["NOMIMP"].ToString()))
                    //        {
                    //            ticket2.PrintTicket(impresoras_plato.Rows[h]["NOMIMP"].ToString());
                    //        }
                    //        h = h + 1;
                    //    }
                    //}
                }
            }
        }
        private void ReImprimir(object cod_det_pedido)
        {
            string clienteCom = "";
            string cod_diarioCom = "";
            string nombre_mesaCom = "";
            string nombre_empleadoCom = "";
            string idpedidoCom = "";

            DataTable impresora = new DataTable();
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            impresora = negPedido.GetReImpresoraxIdPedido(int.Parse(cod_det_pedido.ToString()), Convert.ToInt32(Modulo));
            for (int i = 0; i < impresora.Rows.Count; i++)
            {
                DataTable pedido = new DataTable();
                pedido = negPedido.GET_REIMP_DETALLE_X_PEDIDO(int.Parse(cod_det_pedido.ToString()), Convert.ToInt32(impresora.Rows[i]["IDIMP"].ToString()));
                string cliente = "";
                string descripcion = "";
                string cod_diario = "";
                string nombre_mesa = "";
                string nombre_empleado = "";
                string idpedido = "";
                int nroPersonas = 0;
                Ticket ticket = new Ticket();
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    idpedidoCom = pedido.Rows[0]["ID"].ToString().ToUpper();
                    idpedido = pedido.Rows[0]["ID"].ToString().ToUpper();
                    if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                    {
                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                        clienteCom = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    else if (this.NombreMesa.Contains("DELIVERY"))
                    {
                        cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                        clienteCom = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                    }
                    else
                    {

                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                        clienteCom = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    nroPersonas = Convert.ToInt32(pedido.Rows[j]["PED_NRO_PERSONAS"]);
                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComanda(pedido.Rows[j]["DP_CANT"].ToString().ToUpper(), descripcion);

                    cod_diarioCom = pedido.Rows[0]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesaCom = pedido.Rows[0]["M_NOM"].ToString().ToUpper();
                    nombre_empleadoCom = pedido.Rows[0]["EMPL_NOM"].ToString().ToUpper();
                }
                //if (cliente.ToString().Trim() != "")
                //{
                //    ticket.AddHeaderLine_2(cliente.ToString());
                //    ticket.AddHeaderLine_2("");
                //}
                ticket.DrawFooter2_Mesa(""); // SALAZAR INFANTAS , CHINITO LOS OLIVOS
                ticket.DrawFooter2_Mesa(nombre_mesa.ToString().ToUpper()); // SALAZAR INFANTAS , CHINITO LOS OLIVOS
                //if (this.NombreMesa.Contains("DELIVERY"))
                //{
                //    ticket.AddSubHeaderLineEnormeComanda("         " + cod_diario.ToUpper());
                //}
                //else {
                //    ticket.AddSubHeaderLine("COD DIARIO Nº:" + cod_diario.ToUpper());
                //}
                ticket.AddSubHeaderLine("COD DIARIO Nº:" + cod_diario.ToUpper());
                ticket.AddSubHeaderLine("PEDIDO Nº: " + idpedido.ToUpper());
                ticket.AddSubHeaderLine("ATENDIDO POR: " + nombre_empleado.ToUpper());
                ticket.AddHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                //ticket.AddFooter2Line("Mozo: " + nombre_empleado.ToUpper());
                //ticket.AddFooter2Line("");

                if (cliente.ToString().Trim() != "")
                {
                    ticket.AddFooter2Line(cliente.ToString().ToUpper());
                    ticket.AddFooter2Line("");
                    ticket.AddFooter2Line(pedido.Rows[0]["C_TEL"].ToString().ToUpper());
                }
                if (negParametros.NroPersonas() == "2")
                {
                    ticket.AddFooter2Line("Nro Personas: " + nroPersonas.ToString());
                }

                try
                {
                    if (ticket.PrinterExists(impresora.Rows[i]["NOMIMP"].ToString()))
                    {
                        ticket.PrintTicket(impresora.Rows[i]["NOMIMP"].ToString());
                    }
                    else
                    {
                        //globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[i]["NOMIMP"].ToString() + " no está disponible.", 2);
                    }
                }
                catch (Exception ex)
                {
                    // globales.Mensaje("SOS-FOOD - Informacion", ex.Message, 2);
                }

            }
            negPedido.ActualizarDetPedidoReImp(int.Parse(cod_det_pedido.ToString()));
            bool imprimir_subniveles = negParametros.ImprimirSubNiveles();
            if (imprimir_subniveles)
            {
                if (DataPedido.Count != 0)
                {
                    DataTable impresoras_plato = new DataTable();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("CANTIDAD");
                    dt.Columns.Add("ID_CARTA");
                    dt.Columns.Add("NOM_CARTA");

                    foreach (var i in DataPedido.Where(w => w.idCartaSubNivel != "").ToList())
                    {
                        DataRow row = dt.NewRow();
                        row["CANTIDAD"] = i.cantidad;
                        row["ID_CARTA"] = i.idCartaSubNivel;
                        row["NOM_CARTA"] = i.comentario;
                        dt.Rows.Add(row);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        impresoras_plato = negPedido.GetImpresoraxPlato(Convert.ToInt32(dt.Rows[i]["ID_CARTA"].ToString()));
                        negPedido.DescontarStockSubNivel(Convert.ToInt32(dt.Rows[i]["ID_CARTA"].ToString()));
                        for (int h = 0; h < impresoras_plato.Rows.Count; h++)
                        {
                            Ticket ticket2 = new Ticket();
                            //ticket2.AddItemComanda("[1.00]  " + impresoras_plato.Rows[i]["CAR_NOM"].ToString(), ""); // ese es
                            ticket2.AddItemComanda(dt.Rows[i]["CANTIDAD"].ToString(), dt.Rows[i]["NOM_CARTA"].ToString()); // ese es
                            ticket2.AddHeaderLine_2(clienteCom.ToString());
                            ticket2.AddSubHeaderLine("COD DIARIO Nº:" + cod_diarioCom.ToUpper());
                            ticket2.AddSubHeaderLine("PEDIDO Nº: " + idpedidoCom.ToString().ToUpper());
                            ticket2.AddSubHeaderLine("Atendido Por: " + nombre_empleadoCom.ToUpper());
                            ticket2.AddHeaderLine("");
                            ticket2.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                            ticket2.AddFooter2Line(nombre_mesaCom.ToString().ToUpper());
                            if (ticket2.PrinterExists(impresoras_plato.Rows[h]["NOMIMP"].ToString()))
                            {
                                ticket2.PrintTicket(impresoras_plato.Rows[h]["NOMIMP"].ToString());
                            }
                            h = h + 1;
                        }
                    }
                }
            }
        }
        private void ImprimirRecutacu(int cod_pedido)
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
                Ticket ticket = new Ticket();
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    idpedido = pedido.Rows[0]["ID"].ToString().ToUpper();
                    if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                    {
                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                        clienteCom = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    else
                    {
                        cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                        clienteCom = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                    }
                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComandaRecutacu(pedido.Rows[j]["DP_CANT"].ToString().ToUpper(), descripcion);

                    cod_diarioCom = pedido.Rows[0]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesaCom = pedido.Rows[0]["M_NOM"].ToString().ToUpper();
                    nombre_empleadoCom = pedido.Rows[0]["EMPL_NOM"].ToString().ToUpper();
                }

                if (cliente.ToString().Trim() != "")
                {
                    ticket.AddHeaderLine_2(cliente.ToString());
                    ticket.AddHeaderLine_2("");
                }

                ticket.AddSubHeaderLine("COD DIARIO Nº:" + cod_diario.ToUpper());
                ticket.AddSubHeaderLine("PEDIDO Nº: " + idpedido.ToUpper());
                ticket.AddHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                //RECUTACU
                ticket.AddFooter2Line(nombre_mesa.ToString().ToUpper());
                ticket.AddFooter2Line("");
                ticket.AddFooter2Line("Atendido por: ");
                ticket.AddFooter2Line("");
                ticket.AddFooter2Line(nombre_empleado.ToUpper());

                try
                {
                    if (ticket.PrinterExists(impresora.Rows[i]["NOMIMP"].ToString()))
                    {
                        ticket.PrintTicket(impresora.Rows[i]["NOMIMP"].ToString());
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[i]["NOMIMP"].ToString() + " no está disponible.", 2);
                    }
                }
                catch (Exception ex)
                {
                    globales.Mensaje("SOS-FOOD - Informacion", ex.Message, 2);
                }

            }
            negPedido.ActualizarDetPedidoImp(cod_pedido);
            bool imprimir_subniveles = negParametros.ImprimirSubNiveles();
            if (imprimir_subniveles)
            {
                if (DataPedido.Count != 0)
                {
                    DataTable impresoras_plato = new DataTable();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("CANTIDAD");
                    dt.Columns.Add("ID_CARTA");

                    foreach (var i in DataPedido.Where(w => w.idCartaSubNivel != "").ToList())
                    {
                        DataRow row = dt.NewRow();
                        row["CANTIDAD"] = i.cantidad;
                        row["ID_CARTA"] = i.idCartaSubNivel;
                        dt.Rows.Add(row);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        impresoras_plato = negPedido.GetImpresoraxPlato(Convert.ToInt32(dt.Rows[i]["ID_CARTA"].ToString()));
                        negPedido.DescontarStockSubNivel(Convert.ToInt32(dt.Rows[i]["ID_CARTA"].ToString()));
                        for (int h = 0; h < impresoras_plato.Rows.Count; h++)
                        {
                            Ticket ticket2 = new Ticket();
                            //ticket2.AddItemComanda("[1.00]  " + impresoras_plato.Rows[i]["CAR_NOM"].ToString(), ""); // ese es
                            ticket2.AddItemComanda(dt.Rows[i]["CANTIDAD"].ToString() + " " + impresoras_plato.Rows[h]["CAR_NOM"].ToString(), ""); // ese es
                            ticket2.AddHeaderLine_2(clienteCom.ToString());
                            ticket2.AddSubHeaderLine("COD DIARIO Nº:" + cod_diarioCom.ToUpper());
                            ticket2.AddSubHeaderLine("PEDIDO Nº: " + cod_pedido.ToString().ToUpper());
                            ticket2.AddSubHeaderLine("Atendido Por: " + nombre_empleadoCom.ToUpper());
                            ticket2.AddHeaderLine("");
                            ticket2.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                            ticket2.AddFooter2Line(nombre_mesaCom.ToString().ToUpper());
                            if (ticket2.PrinterExists(impresoras_plato.Rows[h]["NOMIMP"].ToString()))
                            {
                                ticket2.PrintTicket(impresoras_plato.Rows[h]["NOMIMP"].ToString());
                            }
                            else
                            {
                                globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresoras_plato.Rows[h]["NOMIMP"].ToString() + " no está disponible.", 2);
                            }
                            h = h + 1;
                        }
                    }
                }
            }
        }
        private void ImprimirRanchoRobertin(int cod_pedido)
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
                Ticket ticket = new Ticket();
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    idpedido = pedido.Rows[0]["ID"].ToString().ToUpper();
                    if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                    {
                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                        clienteCom = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    else
                    {
                        cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                        clienteCom = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                    }

                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComanda(pedido.Rows[j]["DP_CANT"].ToString().ToUpper(), descripcion);


                    cod_diarioCom = pedido.Rows[0]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesaCom = pedido.Rows[0]["M_NOM"].ToString().ToUpper();
                    nombre_empleadoCom = pedido.Rows[0]["EMPL_NOM"].ToString().ToUpper();
                }

                ticket.AddHeaderLine_2(cliente.ToString());

                //ticket.AddFooter2Line(nombre_mesa.ToString().ToUpper());
                ticket.AddSubHeaderLine("COD DIARIO Nº:" + cod_diario.ToUpper());
                //ticket.AddSubHeaderLine("PEDIDO Nº: " + idpedido.ToUpper());
                //ticket.AddSubHeaderLine("Atendido Por: " + nombre_empleado.ToUpper());
                ticket.AddHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                ticket.AddFooter2Line("MOZO: " + nombre_empleado.ToUpper());
                ticket.AddFooter2Line("");
                ticket.AddFooter2Line(nombre_mesa.ToString().ToUpper());

                try
                {
                    if (ticket.PrinterExists(impresora.Rows[i]["NOMIMP"].ToString()))
                    {
                        ticket.PrintTicket(impresora.Rows[i]["NOMIMP"].ToString());
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[i]["NOMIMP"].ToString() + " no está disponible.", 2);
                    }
                }
                catch (Exception)
                {

                }

            }
            negPedido.ActualizarDetPedidoImp(cod_pedido);
            bool imprimir_subniveles = negParametros.ImprimirSubNiveles();
            if (imprimir_subniveles)
            {
                if (DataPedido.Count != 0)
                {
                    DataTable impresoras_plato = new DataTable();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("CANTIDAD");
                    dt.Columns.Add("ID_CARTA");

                    foreach (var i in DataPedido.Where(w => w.idCartaSubNivel != "").ToList())
                    {
                        DataRow row = dt.NewRow();
                        row["CANTIDAD"] = i.cantidad;
                        row["ID_CARTA"] = i.idCartaSubNivel;
                        dt.Rows.Add(row);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        impresoras_plato = negPedido.GetImpresoraxPlato(Convert.ToInt32(dt.Rows[i]["ID_CARTA"].ToString()));
                        negPedido.DescontarStockSubNivel(Convert.ToInt32(dt.Rows[i]["ID_CARTA"].ToString()));
                        for (int h = 0; h < impresoras_plato.Rows.Count; h++)
                        {
                            Ticket ticket2 = new Ticket();
                            //ticket2.AddItemComanda("[1.00]  " + impresoras_plato.Rows[i]["CAR_NOM"].ToString(), ""); // ese es
                            ticket2.AddItemComanda(dt.Rows[i]["CANTIDAD"].ToString() + " " + impresoras_plato.Rows[h]["CAR_NOM"].ToString(), ""); // ese es
                            ticket2.AddHeaderLine_2(clienteCom.ToString());
                            ticket2.AddSubHeaderLine("COD DIARIO Nº:" + cod_diarioCom.ToUpper());
                            ticket2.AddSubHeaderLine("PEDIDO Nº: " + cod_pedido.ToString().ToUpper());
                            ticket2.AddSubHeaderLine("Atendido Por: " + nombre_empleadoCom.ToUpper());
                            ticket2.AddHeaderLine("");
                            ticket2.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                            ticket2.AddFooter2Line(nombre_mesaCom.ToString().ToUpper());
                            if (ticket2.PrinterExists(impresoras_plato.Rows[h]["NOMIMP"].ToString()))
                            {
                                ticket2.PrintTicket(impresoras_plato.Rows[h]["NOMIMP"].ToString());
                            }
                            else
                            {
                                globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresoras_plato.Rows[h]["NOMIMP"].ToString() + " no está disponible.", 2);
                            }
                            h = h + 1;
                        }
                    }
                }
            }
        }
        private void ImprimirComandaAnulada(int cod_pedido)
        {
            DataTable impresora = new DataTable();
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            impresora = negPedido.GetImpresoraxPedidoAnulado(cod_pedido, Convert.ToInt32(Modulo));
            for (int i = 0; i < impresora.Rows.Count; i++)
            {
                DataTable pedido = new DataTable();
                pedido = negPedido.GET_DETALLE_X_PEDIDO_ANULADA(cod_pedido, Convert.ToInt32(impresora.Rows[i]["IDIMP"].ToString()));
                string cliente = "";
                string descripcion = "";
                string cod_diario = "";
                string nombre_mesa = "";
                string nombre_empleado = "";
                Ticket ticket = new Ticket();
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                    {
                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    else
                    {
                        cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();

                    }

                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComanda(pedido.Rows[j]["DP_CANT"].ToString().ToUpper(), descripcion);
                }
                if (cliente.ToString().Trim() != "")
                {
                    ticket.AddHeaderLine_2(cliente.ToString());
                    ticket.AddHeaderLine_2("");
                }
                //ticket.AddHeaderLine_2("MESA: " + nombre_mesa.ToUpper());
                ticket.AddHeaderLine_2("");
                ticket.AddHeaderLine_2("PEDIDO ANULADO Nº:" + cod_diario.ToUpper());
                ticket.AddHeaderLine("");
                ticket.AddSubHeaderLine("ATENDIDO POR: " + nombre_empleado.ToUpper());
                ticket.AddSubHeaderLine("MESA: " + nombre_mesa.ToUpper());
                ticket.AddSubHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                if (ticket.PrinterExists(impresora.Rows[i]["NOMIMP"].ToString()))
                {
                    ticket.PrintTicket(impresora.Rows[i]["NOMIMP"].ToString());
                }
                else
                    globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[i]["NOMIMP"].ToString() + " no está disponible.", 2);
            }
            //negPedido.ActualizarDetPedidoImp(cod_pedido);
        }
        private void ImprimirComandaAnuladaRecutacu(int cod_pedido)
        {
            string clienteCom = "";
            string cod_diarioCom = "";
            string nombre_mesaCom = "";
            string nombre_empleadoCom = "";
            DataTable impresora = new DataTable();
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            impresora = negPedido.GetImpresoraxPedidoAnulado(cod_pedido, Convert.ToInt32(Modulo));
            for (int i = 0; i < impresora.Rows.Count; i++)
            {
                DataTable pedido = new DataTable();
                pedido = negPedido.GET_DETALLE_X_PEDIDO_ANULADA(cod_pedido, Convert.ToInt32(impresora.Rows[i]["IDIMP"].ToString()));
                string cliente = "";
                string descripcion = "";
                string cod_diario = "";
                string nombre_mesa = "";
                string nombre_empleado = "";
                string idpedido = "";
                Ticket ticket = new Ticket();
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                    {
                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    else
                    {
                        cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();

                    }

                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComandaRecutacu(pedido.Rows[j]["DP_CANT"].ToString().ToUpper(), descripcion);
                }

                if (cliente.ToString().Trim() != "")
                {
                    ticket.AddHeaderLine_2(cliente.ToString());
                    ticket.AddHeaderLine_2("");
                }
                ticket.AddHeaderLine_2("");
                //ticket.AddHeaderLine("");
                //ticket.AddSubHeaderLine("MESA: " + nombre_mesa.ToUpper());
                ticket.AddSubHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                //RECUTACU
                ticket.AddFooter2Line(nombre_mesa.ToString().ToUpper());
                ticket.AddFooter2Line("");
                ticket.AddFooter2Line("Atendido por: " + nombre_empleado.ToUpper());
                ticket.AddFooter2Line("");
                ticket.AddFooter2LineAnulacion("PEDIDO ANULADO Nº:" + cod_diario.ToUpper());

                if (ticket.PrinterExists(impresora.Rows[i]["NOMIMP"].ToString()))
                {
                    ticket.PrintTicket(impresora.Rows[i]["NOMIMP"].ToString());
                }
                else
                {
                    globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[i]["NOMIMP"].ToString() + " no está disponible.", 2);
                }
            }
            //negPedido.ActualizarDetPedidoImp(cod_pedido);
        }
        private void ImprimirComandaPlatoAnulada(int cod_det_ped)
        {
            DataTable impresora = new DataTable();
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            impresora = negPedido.GetImpresoraxIdDetPedido(cod_det_ped, Convert.ToInt32(Modulo));
            for (int i = 0; i < impresora.Rows.Count; i++)
            {
                DataTable pedido = new DataTable();
                pedido = negPedido.GET_DETALLE_X_PLATO_ANULADA(cod_det_ped, Convert.ToInt32(impresora.Rows[i]["IDIMP"].ToString()));
                string cliente = "";
                string descripcion = "";
                string cod_diario = "";
                string nombre_mesa = "";
                string nombre_empleado = "";
                Ticket ticket = new Ticket();
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                    {
                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    else
                    {
                        cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                    }

                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComanda(pedido.Rows[j]["DP_CANT"].ToString().ToUpper(), descripcion);
                }
                if (cliente.ToString().Trim() != "")
                {
                    ticket.AddHeaderLine_2(cliente.ToString());
                    ticket.AddHeaderLine_2("");
                }
                //ticket.AddHeaderLine_2("Mesa: " + nombre_mesa.ToUpper());
                //ticket.AddHeaderLine_2("");
                ticket.AddHeaderLine_2("PLATO ANULADO Nº:" + cod_diario.ToUpper());
                ticket.AddHeaderLine("");
                ticket.AddSubHeaderLine("ATENDIDO POR: " + nombre_empleado.ToUpper());
                ticket.AddSubHeaderLine("MESA: " + nombre_mesa.ToUpper());
                ticket.AddSubHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                if (ticket.PrinterExists(impresora.Rows[i]["NOMIMP"].ToString()))
                {
                    ticket.PrintTicket(impresora.Rows[i]["NOMIMP"].ToString());
                }
                else
                    globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[i]["NOMIMP"].ToString() + " no está disponible.", 2);
            }
            negPedido.ActualizarDetPedidoImpAnul(cod_det_ped);
        }
        private void ImprimirComandaPlatoAnuladaRecutacu(int cod_det_ped)
        {
            DataTable impresora = new DataTable();
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            impresora = negPedido.GetImpresoraxIdDetPedido(cod_det_ped, Convert.ToInt32(Modulo));
            for (int i = 0; i < impresora.Rows.Count; i++)
            {
                DataTable pedido = new DataTable();
                pedido = negPedido.GET_DETALLE_X_PLATO_ANULADA(cod_det_ped, Convert.ToInt32(impresora.Rows[i]["IDIMP"].ToString()));
                string cliente = "";
                string descripcion = "";
                string cod_diario = "";
                string nombre_mesa = "";
                string nombre_empleado = "";
                string idpedido = "";
                Ticket ticket = new Ticket();
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                    {
                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    else
                    {
                        cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();

                    }

                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComandaRecutacu(pedido.Rows[j]["DP_CANT"].ToString().ToUpper(), descripcion);
                }

                if (cliente.ToString().Trim() != "")
                {
                    ticket.AddHeaderLine_2(cliente.ToString());
                    ticket.AddHeaderLine_2("");
                }
                ticket.AddHeaderLine_2("");
                ticket.AddHeaderLine("");
                ticket.AddSubHeaderLine("MESA: " + nombre_mesa.ToUpper());
                ticket.AddSubHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                //RECUTACU

                ticket.AddFooter2Line(nombre_mesa.ToString().ToUpper());
                ticket.AddFooter2Line("");
                ticket.AddFooter2Line("Atendido por: " + nombre_empleado.ToUpper());
                ticket.AddFooter2Line("");
                ticket.AddFooter2LineAnulacion("PLATO ANULADO Nº:" + cod_diario.ToUpper());

                if (ticket.PrinterExists(impresora.Rows[i]["NOMIMP"].ToString()))
                {
                    ticket.PrintTicket(impresora.Rows[i]["NOMIMP"].ToString());
                }
                else
                {
                    globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[i]["NOMIMP"].ToString() + " no está disponible.", 2);
                }
            }
            negPedido.ActualizarDetPedidoImpAnul(cod_det_ped);
        }
        private void ImpComandaAnuladoCaja(int id_pedido)
        {
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
            if (id_pedido == 0)
            {
                /*aqui va el snackbar*/
                return;
            }
            else
            {
                DataTable dt = new DataTable();
                dt = negDetVent.GetComandaAnuladaPedido(id_pedido);
                Ticket ticket = new Ticket();
                //ticket.AddHeaderLine_2("MESA: " + dt.Rows[0]["M_NOM"].ToString());
                //ticket.AddHeaderLine_2("");
                if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                {
                    if (dt.Rows[0]["nomllevar"].ToString().Trim() != "")
                    {
                        ticket.AddHeaderLine_2(dt.Rows[0]["nomllevar"].ToString());
                        ticket.AddHeaderLine_2("");
                    }
                }
                else
                {
                    if (dt.Rows[0]["C_NOM"].ToString().Trim() != "")
                    {
                        ticket.AddHeaderLine_2(dt.Rows[0]["C_NOM"].ToString());
                        ticket.AddHeaderLine_2("");
                    }
                }
                ticket.AddHeaderLine_2("PEDIDO ANULADO Nº:" + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                ticket.AddHeaderLine("");

                ticket.AddSubHeaderLine("ATENDIDO POR: " + dt.Rows[0]["EMPL_NOM"].ToString());
                ticket.AddSubHeaderLine("MESA: " + dt.Rows[0]["M_NOM"].ToString());
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
        private void ImpComandaAnuladoCajaRecutacu(int id_pedido)
        {
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
            if (id_pedido == 0)
            {
                /*aqui va el snackbar*/
                return;
            }
            else
            {
                DataTable pedido = new DataTable();
                pedido = negDetVent.GetComandaAnuladaPedido(id_pedido);
                Ticket ticket = new Ticket();
                //ticket.AddHeaderLine_2("MESA: " + dt.Rows[0]["M_NOM"].ToString());
                //ticket.AddHeaderLine_2("");
                string cliente = "";
                string descripcion = "";
                string cod_diario = "";
                string nombre_mesa = "";
                string nombre_empleado = "";
                string idpedido = "";
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                    {
                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    else
                    {
                        cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();

                    }

                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComandaRecutacu(pedido.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString().ToUpper(), descripcion);
                }

                if (cliente.ToString().Trim() != "")
                {
                    ticket.AddHeaderLine_2(cliente.ToString());
                    ticket.AddHeaderLine_2("");
                }
                ticket.AddHeaderLine_2("");
                //ticket.AddHeaderLine("");
                //ticket.AddSubHeaderLine("MESA: " + nombre_mesa.ToUpper());
                ticket.AddSubHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                //RECUTACU

                ticket.AddFooter2Line(nombre_mesa.ToString().ToUpper());
                ticket.AddFooter2Line("");
                ticket.AddFooter2Line("Atendido por: " + nombre_empleado.ToUpper());
                ticket.AddFooter2Line("");
                ticket.AddFooter2LineAnulacion("PEDIDO ANULADO Nº:" + cod_diario.ToUpper());

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
        private void ImpComandaPlatoAnuladoCaja(int id_det_ped)
        {

            if (id_det_ped == 0)
            {
                /*aqui va el snackbar*/
                return;
            }
            else
            {
                DataTable dt = new DataTable();
                dt = negDetVent.GetComandaPlatoAnuladaPedido(id_det_ped);
                Ticket ticket = new Ticket();
                //ticket.AddHeaderLine_2("Mesa: " + dt.Rows[0]["M_NOM"].ToString());
                //ticket.AddHeaderLine_2("");
                if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                {
                    if (dt.Rows[0]["nomllevar"].ToString().Trim() != "")
                    {
                        ticket.AddHeaderLine_2(dt.Rows[0]["nomllevar"].ToString());
                        ticket.AddHeaderLine_2("");
                    }

                }
                else
                {
                    if (dt.Rows[0]["C_NOM"].ToString().Trim() != "")
                    {
                        ticket.AddHeaderLine_2(dt.Rows[0]["C_NOM"].ToString());
                        ticket.AddHeaderLine_2("");
                    }

                }
                ticket.AddHeaderLine_2("PLATO ANULADO N°: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                ticket.AddHeaderLine("");

                ticket.AddSubHeaderLine("ATENDIDO POR: " + dt.Rows[0]["EMPL_NOM"].ToString());
                ticket.AddSubHeaderLine("MESA: " + dt.Rows[0]["M_NOM"].ToString());
                ticket.AddSubHeaderLine("");

                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    ticket.AddItemComanda(dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString(), dt.Rows[j]["DP_DESCRIP"].ToString());
                }

                ticket.AddFooterLine("");

                ticket.AddFooterLine("");
                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
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
        private void ImpComandaPlatoAnuladoCajaRecutacu(int id_det_ped)
        {

            if (id_det_ped == 0)
            {
                /*aqui va el snackbar*/
                return;
            }
            else
            {
                DataTable pedido = new DataTable();
                pedido = negDetVent.GetComandaPlatoAnuladaPedido(id_det_ped);
                string cliente = "";
                string descripcion = "";
                string cod_diario = "";
                string nombre_mesa = "";
                string nombre_empleado = "";
                string idpedido = "";
                Ticket ticket = new Ticket();
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                    {
                        cliente = pedido.Rows[0]["nomllevar"].ToString().ToUpper();
                    }
                    else
                    {
                        cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();

                    }

                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComandaRecutacu(pedido.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString().ToUpper(), descripcion);
                }

                if (cliente.ToString().Trim() != "")
                {
                    ticket.AddHeaderLine_2(cliente.ToString());
                    ticket.AddHeaderLine_2("");
                }
                ticket.AddHeaderLine_2("");
                ticket.AddHeaderLine("");
                ticket.AddSubHeaderLine("MESA: " + nombre_mesa.ToUpper());
                ticket.AddSubHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                //RECUTACU

                ticket.AddFooter2Line(nombre_mesa.ToString().ToUpper());
                ticket.AddFooter2Line("");
                ticket.AddFooter2Line("Atendido por: " + nombre_empleado.ToUpper());
                ticket.AddFooter2Line("");
                ticket.AddFooter2LineAnulacion("PLATO ANULADO Nº:" + cod_diario.ToUpper());

                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
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
        private void VolverCategoria()
        {
            this.isGrupoCarta = "Hidden";
            this.isCategoriaCarta = "Visible";
        }
        public int contador { get; set; }
        private async void TraerMesas(object id)
        {
            //Delivery
            //this.contador = 10;
            //string nom = DataAmbientes.Where(a => a.ID == Convert.ToInt32(id)).FirstOrDefault().A_NOM;
            //if (nom == "DELIVERY")
            //{
            //    var _SiNo = new Capa_Presentacion_WPF.Views.Delivery_Web_Service.DeliveryWebService
            //    {

            //    };
            //    var _x = await DialogHost.Show(_SiNo, "RootDialog");
            //}

            this.Operacion = "Mesas";
            this.isRbChecked = true; //no sirve :c

            Application.Current.Properties["IdAmbiente"] = id.ToString();
            GetMesas(id);

            this.Mesa = new Mesa();
        }
        public object estadoCierre { get; set; }
        private void ImprimirTickets()
        {
            #region REPORTE PLATOS X COMENTARIO Y ANULADOS
            /*****************************************************REPORTE PLATOS X COMENTARIO Y ANULADOS***************************************/
            DataTable dt = new DataTable();
            dt = negCierre.GetAplatosXComentario(NombreEquipo);
            if (dt.Rows.Count > 0)
            {
                string comentario_raw = "";
                string[] comentario;
                char[] spearator = { '-' };
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        comentario_raw += dt.Rows[i]["COMENTARIO"].ToString();
                    }
                    else
                    {
                        comentario_raw += "-" + dt.Rows[i]["COMENTARIO"].ToString();
                    }

                }
                comentario = comentario_raw.Split(spearator).ToArray();
                comentario = comentario.Distinct().ToArray();
                for (int p = 0; p < comentario.Distinct().Count(); p++)
                {
                    Ticket ticket = new Ticket();
                    ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    ticket.AddHeaderLine_2("PRODUCTOS ANULADOS");
                    ticket.AddHeaderLine("");

                    ticket.AddSubHeaderLine(comentario[p].ToString());
                    ticket.AddSubHeaderLine("");
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["COMENTARIO"].ToString() == comentario[p].ToString())
                        {
                            string cantidad = (dt.Rows.Count > 0) ? dt.Rows[j]["CANTIDAD"].ToString() : "0.00";
                            string descripcion = (dt.Rows.Count > 0) ? dt.Rows[j]["DESCRIPCION"].ToString() : "0.00";
                            ticket.AddItemSinRecorte("[" + cantidad + "]" + descripcion, "", (dt.Rows.Count > 0) ? dt.Rows[j]["IMPORTE"].ToString() : "0.00");

                        }
                    }
                    ticket.AddFooterLine("");

                    if (ticket.PrinterExists(globales.ImpCaja()) != true)
                    {
                        MessageBox.Show("Impresora: " + globales.ImpCaja() + " no está disponible.");
                    }
                    else
                    {
                        ticket.PrintTicket(globales.ImpCaja());
                    }
                }

            }
            /*****************************************************FIN **********************************************************/
            #endregion
            #region REPORTE VENTA POR DIAS
            /*****************************************************REPORTE VENTA POR DIAS ***************************************/
            DataTable dttipag = new DataTable();
            dttipag = negCierre.GetTipoPago(NombreEquipo);
            string denotg_row = "";
            string[] denotg;
            char[] spearatordeno = { '-' };
            for (int i = 0; i < dttipag.Rows.Count; i++)
            {
                if (i == 0)
                {
                    denotg_row += dttipag.Rows[i]["DENO_PAGO"].ToString();
                }
                else
                {
                    denotg_row += "-" + dttipag.Rows[i]["DENO_PAGO"].ToString();
                }
            }
            denotg = denotg_row.Split(spearatordeno).ToArray();
            denotg = denotg.Distinct().ToArray();

            DataTable dt2 = new DataTable();
            dt2 = negCierre.GetreporteVetasDia(NombreEquipo);
            Ticket ticket2 = new Ticket();

            ticket2.AddHeaderLine_2("REPORTE DE VENTA DEL DIA");
            ticket2.AddSubHeaderLine("");
            ticket2.AddSubHeaderLine("FECHA INICIO: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            ticket2.AddSubHeaderLine("FECHA FIN: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            ticket2.AddSubHeaderLine("");
            ticket2.AddReporteItem("Ingresos", "CAJA CHICA", (dt2.Rows.Count > 0) ? dt2.Rows[0]["INGRESO_CAJA"].ToString() : "0.00");
            ticket2.AddReporteItem("Egresos", "CAJA CHICA", (dt2.Rows.Count > 0) ? dt2.Rows[0]["EGRESO_CAJA"].ToString() : "0.00");
            ticket2.AddReporteItem("SALDO", "CAJA CHICA", (dt2.Rows.Count > 0) ? dt2.Rows[0]["SALDO_CAJA"].ToString() : "0.00");

            ticket2.AddReporteItemventas("COBRADO", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL"].ToString() : "0.00");
            ticket2.AddReporteItemventas("X COBRAR", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["POR_PAGAR"].ToString() : "0.00");
            object totalventas = 0;
            totalventas = Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL"] : 0.00) + Convert.ToDouble((dt2.Rows.Count > 0) ? dt2.Rows[0]["POR_PAGAR"] : 0.00);
            ticket2.AddReporteItemventas("TOTAL", "VENTAS", totalventas.ToString());
            ticket2.AddReporteItemventas("", "VENTAS", "");

            ticket2.AddReporteItemventas("Total Vendido", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["SUBTOTAL"].ToString() : "0.00");
            ticket2.AddReporteItemventas("Total Descuento", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["DESCUENTO"].ToString() : "0.00");
            ticket2.AddReporteItemventas("Total ", "VENTAS", (dt2.Rows.Count > 0) ? dt2.Rows[0]["TOTAL"].ToString() : "0.00");

            bool pago = false;
            for (int p1 = 0; p1 < denotg.Distinct().Count(); p1++)
            {
                if (denotg[p1].ToString().Trim() != "")
                {
                    string nomcat = denotg[p1].ToString();
                    ticket2.AddDocElectronicoAnuladosTotal(dttipag.Rows[p1]["DENO_PAGO"].ToString(), "TIPO PAGO", (dttipag.Rows.Count > 0) ? dttipag.Rows[p1]["PED_TOTAL"].ToString() : "0.00");
                    pago = true;
                }


            }
            if (pago == true)
            {
                ticket2.AddFooterLine("");
                ticket2.AddDocElectronicoAnuladosTotal("TOTAL:", "", dttipag.Rows[0]["MONTOTOTAL"].ToString());
            }

            ticket2.AddFooterLine("");
            if (ticket2.PrinterExists(globales.ImpCaja()) != true)
            {
                MessageBox.Show("Impresora: " + globales.ImpCaja() + " no está disponible.");
            }
            else
            {
                ticket2.PrintTicket(globales.ImpCaja());
            }
            /*****************************************************FIN **********************************************************/
            #endregion
            #region DOCUMENTO ELECTRONICO
            /*****************************************************DOCUMENTO ELECTRONICO***************************************/


            DataTable dt3 = new DataTable();
            dt3 = negCierre.GetreporteDocumentoElect(NombreEquipo);
            Ticket ticket3 = new Ticket();
            ticket3.AddTitleLine("REPORTE DE DOCUMENTOS");
            ticket3.AddTitleLine("ELECTRONICO");
            ticket3.AddSubHeaderLine("");

            ticket3.AddDocElectronico((dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALBOLETA"].ToString() : "0", "Boletas", (dt3.Rows.Count > 0) ? dt3.Rows[0]["MONTOBOL"].ToString() : "0.00");
            ticket3.AddDocElectronico((dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALFACTURA"].ToString() : "0", "Facturas", (dt3.Rows.Count > 0) ? dt3.Rows[0]["MONTOFACT"].ToString() : "0.00");
            ticket3.AddDocElectronicoTotal("TOTAL B/F", "", (dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALBF"].ToString() : "0.00");
            ticket3.AddDocElectronicoTotal("TOTAL S/B", "", (dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALSB"].ToString() : "0.00");

            ticket3.AddTitleLine2("REPORTE DE DOCUMENTOS");
            ticket3.AddTitleLine2("ELECTRONICOS ANULADOS");

            ticket3.AddDocElectronicoAnulados((dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALBOLANULADO"].ToString() : "0", "Boletas", (dt3.Rows.Count > 0) ? dt3.Rows[0]["MONTOBOLANULADO"].ToString() : "0.00");
            ticket3.AddDocElectronicoAnulados((dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALFACANULADO"].ToString() : "0", "Facturas", (dt3.Rows.Count > 0) ? dt3.Rows[0]["MONTOFACANULADO"].ToString() : "0.00");
            ticket3.AddDocElectronicoAnuladosTotal("TOTAL B/F", "", (dt3.Rows.Count > 0) ? dt3.Rows[0]["TOTALMONTOANULADO"].ToString() : "0.00");
            ticket3.AddFooterLine("");
            if (ticket3.PrinterExists(globales.ImpCaja()) != true)
            {
                MessageBox.Show("Impresora: " + globales.ImpCaja() + " no está disponible.");
            }
            else
            {
                ticket3.PrintTicket(globales.ImpCaja());
            }

            /*****************************************************FIN **********************************************************/
            #endregion
            #region PLATOS VENDIDOS POR DIA
            /***************************************PLATOS VENDIDOS POR DIA**********************************************************/

            DataTable dtscat = new DataTable();
            dtscat = negCierre.Getsupercat();
            string nomscat_row = "";
            string[] nomscat;
            char[] spearatorScat = { '-' };
            for (int i = 0; i < dtscat.Rows.Count; i++)
            {
                if (i == 0)
                {
                    nomscat_row += dtscat.Rows[i]["SCAT_NOM"].ToString();
                }
                else
                {
                    nomscat_row += "-" + dtscat.Rows[i]["SCAT_NOM"].ToString();
                }
            }
            nomscat = nomscat_row.Split(spearatorScat).ToArray();
            nomscat = nomscat.Distinct().ToArray();
            for (int p1 = 0; p1 < nomscat.Distinct().Count(); p1++)
            {
                string nomcat = nomscat[p1].ToString();
                DataTable dtVdia = new DataTable();
                dtVdia = negCierre.GetreportetotalPedidosDia(nomscat[p1].ToString(), NombreEquipo);
                if (dtVdia.Rows.Count > 0)
                {
                    Ticket ticketPlatos = new Ticket();
                    ticketPlatos.AddSubHeaderLine("Fecha/Hora: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    ticketPlatos.AddHeaderLine_2("PLATOS VENDIDOS DEL DIA");
                    ticketPlatos.AddSubHeaderLine("");
                    ticketPlatos.AddSubHeaderLine(nomscat[p1].ToString());
                    ticketPlatos.AddSubHeaderLine("");
                    for (int j = 0; j < dtVdia.Rows.Count; j++)
                    {
                        if (dtVdia.Rows[j]["SUPERCAT"].ToString() == nomscat[p1].ToString())
                        {
                            string cantidad = (dtVdia.Rows.Count > 0) ? dtVdia.Rows[j]["CANTIDAD"].ToString() : "0.00";
                            string descripcion = (dtVdia.Rows.Count > 0) ? dtVdia.Rows[j]["DESCRIPCION"].ToString() : "0.00";
                            ticketPlatos.AddItemSinRecorte("[" + cantidad + "]" + descripcion, "", (dtVdia.Rows.Count > 0) ? dtVdia.Rows[j]["MONTO"].ToString() : "0.00");
                        }
                    }
                    ticketPlatos.AddDocElectronicoAnuladosTotal("TOTAL", "", (dtVdia.Rows.Count > 0) ? dtVdia.Rows[0]["TOTAL"].ToString() : "0.00");
                    ticketPlatos.AddFooterLine("");
                    if (ticketPlatos.PrinterExists(globales.ImpCaja()) != true)
                    {
                        MessageBox.Show("Impresora: " + globales.ImpCaja() + " no está disponible.");
                    }
                    else
                    {
                        ticketPlatos.PrintTicket(globales.ImpCaja());
                    }
                }

            }

            /*****************************************************FIN **********************************************************/
            #endregion
            #region REPORTE CAJA
            /**********************************************************REPORTE CAJA****************************************/
            DataTable caja = new DataTable();
            caja = negCierre.GetReporteCajaDia();
            if (caja.Rows.Count > 0)
            {
                string tipo_raw = "";
                string tipoMOVIMIENTO = "";
                string[] tipo;
                string[] tipo2;
                char[] separador = { '-' };
                char[] separador2 = { '-' };
                for (int i = 0; i < caja.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        tipo_raw += caja.Rows[i]["CC_ID_TIPO"].ToString();
                    }
                    else
                    {
                        tipo_raw += "-" + caja.Rows[i]["CC_ID_TIPO"].ToString();
                    }
                }
                tipo = tipo_raw.Split(separador).ToArray();
                tipo = tipo.Distinct().ToArray();


                for (int i = 0; i < caja.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        tipoMOVIMIENTO += caja.Rows[i]["TIPO"].ToString() + caja.Rows[i]["CC_ID_TIPO"].ToString();
                    }
                    else
                    {
                        tipoMOVIMIENTO += "-" + caja.Rows[i]["TIPO"].ToString() + caja.Rows[i]["CC_ID_TIPO"].ToString();
                    }
                }
                tipo2 = tipoMOVIMIENTO.Split(separador2).ToArray();
                tipo2 = tipo2.Distinct().ToArray();

                Ticket ticketcaja = new Ticket();

                ticketcaja.AddHeaderLine_2("REPORTE CAJA");
                ticketcaja.AddHeaderLine("");
                ticketcaja.AddSubHeaderLine("         " + DateTime.Now.ToShortDateString());
                ticketcaja.AddSubHeaderLine("");
                for (int p = 0; p < tipo.Distinct().Count(); p++)
                {

                    ticketcaja.AddSubHeaderLine((tipo[p].ToString() == "1") ? "INGRESO : " : "EGRESO :");
                    ticketcaja.AddSubHeaderLine("");
                    for (int k = 0; k < tipo2.Distinct().Count(); k++)
                    {
                        if (tipo2[k].ToString().Substring(tipo2[k].Length - 1, 1) == tipo[p].ToString())
                        {
                            ticketcaja.AddSubHeaderLine(tipo2[k].ToString().Substring(0, tipo2[k].Length - 1));
                            string dotted = "";
                            for (int x = 0; x < Convert.ToInt32(negParametros.MaxCharDescriptionTicket()); x++)
                            {
                                dotted += "-";
                            }
                            ticketcaja.AddSubHeaderLine(dotted);
                        }

                        //ticketcaja.AddSubHeaderLine("");
                        for (int j = 0; j < caja.Rows.Count; j++)
                        {
                            if (caja.Rows[j]["TIPO"].ToString() == tipo2[k].ToString().Substring(0, tipo2[k].Length - 1) && caja.Rows[j]["CC_ID_TIPO"].ToString() == (tipo[p].ToString()))
                            {
                                //ticketcaja.AddReporteItemventas(caja.Rows[j]["DESCRIPCION"].ToString(), "", (caja.Rows.Count > 0) ? caja.Rows[j]["MONTO"].ToString() : "0.00");
                                ticketcaja.AddSubHeaderLine(caja.Rows[j]["DESCRIPCION"].ToString() + " : " + Convert.ToString((caja.Rows.Count > 0) ? caja.Rows[j]["MONTO"].ToString() : "0.00"));
                            }
                        }
                        ticketcaja.AddSubHeaderLine("");


                    }

                    ticketcaja.AddSubHeaderLine("");

                }
                ticketcaja.AddDocElectronicoAnuladosTotal("TOTAL MONTO", "", (caja.Rows.Count > 0) ? "S/" + caja.Rows[0]["MONTO"].ToString() : "0.00");
                ticketcaja.AddDocElectronicoAnuladosTotal("INGRESO CAJA", "", (caja.Rows.Count > 0) ? "S/" + caja.Rows[0]["INGRESO_CAJA"].ToString() : "0.00");
                ticketcaja.AddDocElectronicoAnuladosTotal("EGRESO CAJA", "", (caja.Rows.Count > 0) ? "S/" + caja.Rows[0]["EGRESO_CAJA"].ToString() : "0.00");
                ticketcaja.AddDocElectronicoAnuladosTotal("TOTAL SALDO", "", (caja.Rows.Count > 0) ? "S/" + caja.Rows[0]["SALDO_CAJA"].ToString() : "0.00");
                ticketcaja.AddFooterLine("");

                if (ticketcaja.PrinterExists(globales.ImpCaja()) != true)
                {
                    MessageBox.Show("Impresora: " + globales.ImpCaja() + " no está disponible.");
                }
                else
                {
                    ticketcaja.PrintTicket(globales.ImpCaja());
                }
            }
            /*****************************************************FIN **********************************************************/
            #endregion
            empresa = new ObservableCollection<Empresa>();
            correo = new ObservableCollection<Empresa>();
            string json = "";
            empresa = negEmpresa.GetEmpresa();
            correo = negEmpresa.GetEmpresaCorreo(Convert.ToInt32(empresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().idEmpr));
            json = negCierre.GetCierreJson();
            byte[] jsonDatareq = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(json, Formatting.Indented));
            foreach (var e in correo)
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        values["valor1"] = "HOLA";
                        values["valor2"] = json;
                        values["valor3"] = e.correo;
                        var response = client.UploadValues("http://cierres.sos-food.com/test/", values);
                        var responseString = Encoding.Default.GetString(response);
                        //var WebService = new ConsultaWebService();
                        //WebService = JsonConvert.DeserializeObject<ConsultaWebService>(responseString);
                        //if (WebService.cambio_info == 0)
                        //{
                        //}
                    }
                }
                catch (Exception ex)
                {

                    //globales.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
                }
            }
        }
        public int isReserva = 0;
        private async void RegistrarPedido(object id)
        {
            Enviar_Correo_Anulacion = negParametros.EnvioUploadAnulacion();
     
            this.Buscador = "";
            this.Motorizado = "";
            HabilitarPedir = "True";
            this.DataPlatosBusqueda = negPlatos.GetPlatosActivos();
            if (!this.NombreMesa.Contains("DELIVERY"))
            {
                Application.Current.Properties["IdClienteDel"] = null;
            }
            try
            {
                string Modulo2 = ConfigurationManager.AppSettings["modulo"].ToString();
                if (Modulo2 == "1")
                {
                    VisibilityRP = "Visible";
                }
                else if (Modulo2 == "3")
                {
                    VisibilityRP = "Collapsed";
                }
                #region Validar si ha cerrado caja o no
                parametros = neg_par.GetParametros();
                int dia_limite = Convert.ToInt32(parametros.Where(w => w.NOM_PAR == "DiaLimite_CierreCaja").FirstOrDefault().VALOR_PAR);
                var hora_limite = parametros.Where(w => w.NOM_PAR == "HoraLimite_CierreCaja").FirstOrDefault().VALOR_PAR;
                var par_exists = directoryStructure.GetPedidoIsNull();
                if (par_exists >= Convert.ToInt32(hora_limite))
                {

                    this.DataUsuario = negUser.GetUsuario();
                    var SiNo = new SiNoMessageDialog
                    {
                        Mensaje = { Text = "No ha cerrado caja las ultimas " + par_exists + " horas \n ¿Desea cerrar caja ahora?" }
                    };
                    var x = await DialogHost.Show(SiNo, "RootDialog");
                    if ((bool)x)
                    {
                        DataDetallePedido = negDetVent.GetDetallepedido(NombreEquipo);
                        if (this.DataDetallePedido.Count != 0)
                        {
                            this.estadoCierre = this.DataDetallePedido.Where(s => s.id_estado_f.ToString() == "2").Count();
                            if (Convert.ToInt32(estadoCierre) == 0)
                            {
                                var _SiNo = new SiNoMessageDialog
                                {
                                    Mensaje = { Text = "Está seguro de cerrar caja?" }
                                };
                                var _x = await DialogHost.Show(_SiNo, "RootDialog");
                                if (!(bool)_x)
                                    return;
                                ImprimirTickets();
                                VentasDia ventadia = new VentasDia();
                                var user = this.DataUsuario.Where(w => w.estadousu == 1).FirstOrDefault().idusu;
                                ventadia.CJ_id_usu = user.ToString();
                                string _mensaje = "";
                                int result = negDetVent.SetCierreCaja((1), ventadia, ref _mensaje, NombreEquipo);

                                var view = new MessageDialog
                                {
                                    Mensaje = { Text = _mensaje }
                                };
                                await DialogHost.Show(view, "RootDialog");
                                if (result != 0)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                int validarCierre = this.DataDetallePedido.Where(w => Convert.ToInt32(w.id_estado_f) == 2).Count();
                                if (validarCierre > 0)
                                {
                                    globales.Mensaje("SOS-FOOD Información", "Tienes " + validarCierre + " pedidos por pagar", 3);
                                }
                            }
                        }
                        return;
                    }
                }
                #endregion

                #region Validar si es una mesa reservada y esta a 2 horas de la reserva y aun no tiene pedidos
                List<Ent_Reserva> _reserva = new List<Ent_Reserva>();
                List<Ent_Reserva> reserva = new List<Ent_Reserva>();
                _reserva = rs.GetReservas(DateTime.Now, DateTime.Now);
                int contar_reserva = _reserva.Where(w => w.R_ID_MESA == Convert.ToInt32(id) && w.R_ID_ESTADO == 1 && (w.R_F_RESERVA_DESDE - DateTime.Now).TotalHours <= 2).Count();
                if (contar_reserva > 0)
                {
                    reserva = _reserva.Where(w => w.R_ID_MESA == Convert.ToInt32(id) && w.R_ID_ESTADO == 1 && (w.R_F_RESERVA_DESDE - DateTime.Now).TotalHours <= 2).ToList();
                    string nombre_cliente = reserva.FirstOrDefault().C_NOMINA;
                    string nombre_mesa = reserva.FirstOrDefault().M_NOM;
                    string fecha_desde = reserva.FirstOrDefault().R_F_RESERVA_DESDE.ToString();
                    string fecha_hasta = reserva.FirstOrDefault().R_F_RESERVA_HASTA.ToString();
                    if (DataMesas.Where(w => w.ID == Convert.ToInt32(id)).FirstOrDefault().M_EST == 0)
                    {
                        var vista = new DialogMesaReservada
                        {
                            Mensaje = { Text = "La mesa " + nombre_mesa + " esta reservada para el cliente: \n" + nombre_cliente +
                                                " desde: " + fecha_desde + " hasta: " + fecha_hasta}
                        };
                        var vista_res = await DialogHost.Show(vista, "RootDialog");
                        if (!(bool)vista_res)
                        {
                            isReserva = 0;
                        }
                        else
                        {
                            isReserva = 1;
                            List<Platos> _p = new List<Platos>();
                            _p = rs.GetDetalleReserva(reserva.FirstOrDefault().ID);
                            ObservableCollection<Platos> p = new ObservableCollection<Platos>(_p);
                            this.DataPedido = p;
                        }
                    }

                }
                #endregion
                //Obtener ID del empleado que entro a la mesa.
                int id_empleado_pedido = 0;
                string nombre_empleado_pedido = "";
                DataTable dtempl = negPedido.getEmpleadoPedido(Convert.ToInt32(id));
                if (dtempl != null && dtempl.Rows.Count != 0)
                {
                    id_empleado_pedido = Convert.ToInt32(dtempl.Rows[0]["PED_ID_EMPL"]);
                    nombre_empleado_pedido = dtempl.Rows[0]["EMPL_NOM"].ToString();
                }
                ////////////////////////////////////////////////////////

                var submesas = directoryStructure.ObtenerSubMesa(id);
                this.DataSubMesas = new ObservableCollection<MesasItemViewModel>(
                submesas.Select(c => new MesasItemViewModel(c.ID, c.M_NOM, c.M_EST, c.M_ID_AMB, c.M_X, c.M_ATENDIDA,
                c.M_WIDTH, c.M_HEIGHT, c.M_TEXTO, c.M_TIPO, c.M_ACT, c.M_F_CREATE, c.color, c.M_ID_PADRE, c.M_F_MODIFICACION,
                c.NOMBRE_PADRE, c.PED_ID_CLIENTE, c.C_NOMINA, c.nomllevar, c.EMPL_NOM, c.mesa)));

                //if (Application.Current.Properties["IdEmpleado"] != null)
                //{
                string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
                if (Modulo == "1")
                {
                    BotonPedir = "Visible";
                    BotonFactura = "Visible";
                    BotonBoleta = "Visible";
                    BotonDescuento = "Visible";
                }
                else
                {
                    BotonPedir = "Collapsed";
                    BotonFactura = "Collapsed";
                    BotonBoleta = "Collapsed";
                    BotonDescuento = "Collapsed";
                }

                if (submesas.Count == 0)
                {
                    if (this.MesaPadre != "")
                    {
                        Application.Current.Properties["NomMesa"] = directoryStructure.NombreSubMesa(id);
                    }
                    else
                    {
                        Application.Current.Properties["NomMesa"] = directoryStructure.NombreMesa(id);
                    }

                    this.NombreMesa = Application.Current.Properties["NomMesa"].ToString();
                    if (this.NombreMesa.Contains("DELIVERY"))
                    {
                        VisibilityOjo = "Visible";
                    }
                    else
                    {
                        VisibilityOjo = "Collapsed";
                    }
                    if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("DELIVERY"))
                    {
                        BotonEstadoDelivery = "Visible";
                    }
                    else
                    {
                        BotonEstadoDelivery = "Collapsed";
                    }
                    if (!this.NombreMesa.Contains("LLEVAR") && !this.NombreMesa.Contains("DELIVERY"))
                    {
                        BotonQR = "Visible";
                    }
                    else
                    {
                        BotonQR = "Collapsed";
                    }
                    if (this.NombreMesa.Contains("DELIVERY"))
                    {
                        ClienteDelivery2 = "Visible";
                        NomMozoMesa = "Collapsed";
                    }
                    else
                    {
                        ClienteDelivery2 = "Collapsed";
                        NomMozoMesa = "Visible";
                    }
                    Application.Current.Properties["CodMesa"] = id;
                    this.CodMesa = Application.Current.Properties["CodMesa"].ToString();

                    DataTable mesa = new DataTable();
                    mesa = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                    if (mesa.Rows[0]["M_ATENDIDA"].ToString() == "1")
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                        return;
                    }
                    else
                    {
                        negPedido.ActualizarMesaOcupada(Convert.ToInt32(id));
                    }

                    this.DataPlatos = negPlatos.GetPlatosMasVendidos();
                    if (mesa.Rows[0]["M_EST"].ToString() != "0")
                    {
                        this.Pedido = negPedido.GetPedidoxMesa(Convert.ToInt32(id));
                        if (this.NombreMesa.Contains("DELIVERY"))
                        {
                            dt_info_delivery = negPedido.getInfoClienteDelivery(this.Pedido.FirstOrDefault().DP_ID_PED);
                            Application.Current.Properties["dt_info_delivery"] = dt_info_delivery;
                            //dt_info_pago_cliente = negPedido.getInfoPagoCliente(this.Pedido.FirstOrDefault().DP_ID_PED);
                        }
                        else
                        {
                            Application.Current.Properties["dt_info_delivery"] = null;
                        }
                        if (this.Pedido != null && this.Pedido.Count != 0)
                        {
                            if (negParametros.ClavePedir() == "1" && Modulo == "3")
                            {
                                Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                var Dialog = new DialogClaveEmpleado { };
                                var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                if (Application.Current.Properties["IdEmpleado"] == null)
                                {
                                    Cancelar();
                                    return;
                                }
                                //DataTable mesa2 = new DataTable();
                                //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                //{
                                //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                //    return;
                                //}
                            }
                            else if (negParametros.ClavePedir() == "2" && Modulo == "3")
                            {
                                Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                var Dialog = new DialogClaveEmpleado
                                {

                                };
                                var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                if (Application.Current.Properties["IdEmpleado"] == null)
                                {
                                    Cancelar();
                                    return;
                                }
                                //DataTable mesa2 = new DataTable();
                                //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                //{
                                //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                //    return;
                                //}
                            }
                            else if (negParametros.ClavePedir() == "3")
                            {
                                Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                var Dialog = new DialogClaveEmpleado
                                {

                                };
                                var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                if (Application.Current.Properties["IdEmpleado"] == null)
                                {
                                    Cancelar();
                                    return;
                                }
                                //DataTable mesa2 = new DataTable();
                                //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                //{
                                //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                //    return;
                                //}
                            }
                            else if (negParametros.ClavePedir() == "4" && Modulo == "3")
                            {
                                Application.Current.Properties["id_empleado_anterior"] = id_empleado_pedido;
                                Application.Current.Properties["nombre_empleado_anterior"] = nombre_empleado_pedido;
                                Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                var Dialog = new DialogClaveEmpleado { };
                                var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                if (Application.Current.Properties["IdEmpleado"] == null)
                                {
                                    Cancelar();
                                    return;
                                }
                                //DataTable mesa2 = new DataTable();
                                //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                //{
                                //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                //    return;
                                //}
                            }
                            else
                            {
                                Application.Current.Properties["IdEmpleado"] = this.Pedido.ToList().FirstOrDefault().PED_ID_EMPL.ToString();
                            }

                            string tcambio = negParametros.GetTipoCambio();
                            decimal cambio = Convert.ToDecimal(tcambio);

                            this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                            this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                            this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                            this.TCambio = "T/C : " + tcambio;
                            this.TDolar = " TOTAL : $ " + Convert.ToDecimal(this.Pedido.ToList().FirstOrDefault().PED_TOTAL / cambio).ToString("N2");
                            this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                            TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                            this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                            this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                            this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                            this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                            this.IdCliente = this.Pedido.ToList().FirstOrDefault().PED_ID_CLIENTE.ToString();
                            this.NomCliente = this.Pedido.ToList().FirstOrDefault().C_NOM.ToString();
                            Motorizado = this.Pedido.ToList().FirstOrDefault().MOTORIZADO.ToString();
                            Application.Current.Properties["LlevarCliente"] = false;
                            if (this.NombreMesa.Contains("DELIVERY"))
                            {
                                this.ClienteDelivery = "Visible";
                            }
                            else
                            {
                                this.ClienteDelivery = "Collapsed";
                            }
                            //if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                            //{
                            this.ClienteDelivery = "Visible";
                            this.NomCliente = this.Pedido.ToList().FirstOrDefault().nomllevar.ToString();
                            this.TelCliente = this.Pedido.ToList().FirstOrDefault().telllevar.ToString();
                            Application.Current.Properties["LlevarCliente"] = true;
                            //}
                        }
                        else
                        {
                            if (Application.Current.Properties["IdEmpleadoUsuario"] != null)
                            {
                                if (negParametros.ClavePedir() == "1" && Modulo == "3")
                                {
                                    Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                    var Dialog = new DialogClaveEmpleado
                                    {

                                    };
                                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                    if (Application.Current.Properties["IdEmpleado"] == null)
                                    {
                                        Cancelar();
                                        return;
                                    }
                                    //DataTable mesa2 = new DataTable();
                                    //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                    //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                    //{
                                    //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                    //    return;
                                    //}
                                }
                                else if (negParametros.ClavePedir() == "2")
                                {
                                    Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                    var Dialog = new DialogClaveEmpleado
                                    {

                                    };
                                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                    if (Application.Current.Properties["IdEmpleado"] == null)
                                    {
                                        Cancelar();
                                        return;
                                    }
                                    //DataTable mesa2 = new DataTable();
                                    //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                    //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                    //{
                                    //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                    //    return;
                                    //}
                                }
                                else if (negParametros.ClavePedir() == "3")
                                {
                                    Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                    var Dialog = new DialogClaveEmpleado
                                    {

                                    };
                                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                    if (Application.Current.Properties["IdEmpleado"] == null)
                                    {
                                        Cancelar();
                                        return;
                                    }
                                    //DataTable mesa2 = new DataTable();
                                    //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                    //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                    //{
                                    //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                    //    return;
                                    //}
                                }
                                else if (negParametros.ClavePedir() == "4" && Modulo == "3")
                                {
                                    Application.Current.Properties["id_empleado_anterior"] = id_empleado_pedido;
                                    Application.Current.Properties["nombre_empleado_anterior"] = nombre_empleado_pedido;
                                    Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                    var Dialog = new DialogClaveEmpleado { };
                                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                    if (Application.Current.Properties["IdEmpleado"] == null)
                                    {
                                        Cancelar();
                                        return;
                                    }
                                    //DataTable mesa2 = new DataTable();
                                    //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                    //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                    //{
                                    //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                    //    return;
                                    //}
                                }
                                else
                                {
                                    Application.Current.Properties["IdEmpleado"] = Application.Current.Properties["IdEmpleadoUsuario"];
                                }
                            }
                            else
                            {
                                Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                var Dialog = new DialogClaveEmpleado
                                {

                                };
                                var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                if (Application.Current.Properties["IdEmpleado"] == null)
                                {
                                    Cancelar();
                                    return;
                                }
                                //DataTable mesa2 = new DataTable();
                                //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                //{
                                //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                //    return;
                                //}

                            }
                            string tcambio = negParametros.GetTipoCambio();
                            decimal cambio = Convert.ToDecimal(tcambio);

                            this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                            this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                            this.TotalPedido = "TOTAL : S/. 0.00";
                            this.TCambio = "T/C : " + tcambio;
                            this.TDolar = " TOTAL : $ 0.00";
                            this.Fecha_Registro = "";
                            this.Fecha_Transcurrido = "";
                            this.IdCliente = null;
                            this.Nombre_Mozo = "";
                            this.Nro_Personas = "1";
                            this.NroDiario = "";
                            this.NomCliente = "";
                            this.Motorizado = "";
                            this.TelCliente = "";
                            Application.Current.Properties["LlevarCliente"] = false;
                            if (this.NombreMesa.Contains("DELIVERY"))
                            {
                                DataTable dt = new DataTable();
                                dt = negDetVent.GetCargarPlatosSelect();
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    this.DataPlatos = negPlatos.GetPlatosActivos();
                                    PasarPlatos(Convert.ToInt32(dt.Rows[j]["IDCARTA"]));
                                    this.DataPlatos = new ObservableCollection<Platos>();
                                }
                                this.ClienteDelivery = "Visible";
                                var delivery = new DialogCliente { };
                                var x = await DialogHost.Show(delivery, "RootDialog");
                                if (Application.Current.Properties["ClienteDelivery"] == null)
                                {
                                    NomCliente = "";
                                    Cancelar();
                                    return;
                                }
                                else
                                {
                                    NomCliente = Application.Current.Properties["ClienteDelivery"].ToString();
                                }
                                if (Application.Current.Properties["IdClienteDelivery"] == null)
                                {
                                    IdCliente = "0";
                                    Cancelar();
                                    return;
                                }
                                else
                                {
                                    IdCliente = Application.Current.Properties["IdClienteDelivery"].ToString();
                                }
                                //DataTable mesa2 = new DataTable();
                                //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                //{
                                //    this.ClienteDelivery = "Collapsed";
                                //    IdCliente = "0";
                                //    NomCliente = "";
                                //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                //    return;
                                //}
                            }
                            else
                            {
                                this.ClienteDelivery = "Collapsed";
                            }
                            //if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                            //{
                            this.ClienteDelivery = "Visible";
                            Application.Current.Properties["LlevarCliente"] = true;
                            //}
                        }
                    }
                    else
                    {
                        if (Application.Current.Properties["IdEmpleadoUsuario"] != null)
                        {

                            if (negParametros.ClavePedir() == "1" && Modulo == "3")
                            {
                                Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                var Dialog = new DialogClaveEmpleado
                                {

                                };
                                var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                if (Application.Current.Properties["IdEmpleado"] == null)
                                {
                                    Cancelar();
                                    return;
                                }
                                //DataTable mesa2 = new DataTable();
                                //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));
                                //todo se derummbo
                                //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                //{
                                //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                //    return;
                                //}
                            }
                            else if (negParametros.ClavePedir() == "2")
                            {
                                Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                var Dialog = new DialogClaveEmpleado
                                {

                                };
                                var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                if (Application.Current.Properties["IdEmpleado"] == null)
                                {
                                    Cancelar();
                                    return;
                                }
                                //DataTable mesa2 = new DataTable();
                                //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                //{
                                //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                //    return;
                                //}
                            }
                            else if (negParametros.ClavePedir() == "3")
                            {
                                Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                var Dialog = new DialogClaveEmpleado
                                {

                                };
                                var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                if (Application.Current.Properties["IdEmpleado"] == null)
                                {
                                    Cancelar();
                                    return;
                                }
                                //DataTable mesa2 = new DataTable();
                                //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                //{
                                //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                //    return;
                                //}
                            }
                            else if (negParametros.ClavePedir() == "4" && Modulo == "3")
                            {
                                Application.Current.Properties["id_empleado_anterior"] = id_empleado_pedido;
                                Application.Current.Properties["nombre_empleado_anterior"] = nombre_empleado_pedido;
                                Application.Current.Properties["IdEmpleadoUsuario"] = null;
                                var Dialog = new DialogClaveEmpleado { };
                                var x2 = await DialogHost.Show(Dialog, "RootDialog");
                                if (Application.Current.Properties["IdEmpleado"] == null)
                                {
                                    Cancelar();
                                    return;
                                }
                                //DataTable mesa2 = new DataTable();
                                //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                                //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                                //{
                                //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                                //    return;
                                //}
                            }
                            else
                            {
                                Application.Current.Properties["IdEmpleado"] = Application.Current.Properties["IdEmpleadoUsuario"];
                            }

                        }
                        else
                        {
                            Application.Current.Properties["IdEmpleadoUsuario"] = null;
                            var Dialog = new DialogClaveEmpleado
                            {

                            };
                            var x2 = await DialogHost.Show(Dialog, "RootDialog");
                            if (Application.Current.Properties["IdEmpleado"] == null)
                            {
                                Cancelar();
                                return;
                            }
                            //DataTable mesa2 = new DataTable();
                            //mesa2 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                            //if (mesa2.Rows[0]["M_ATENDIDA"].ToString() == "1")
                            //{
                            //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                            //    return;
                            //}
                        }
                        string tcambio = negParametros.GetTipoCambio();
                        decimal cambio = Convert.ToDecimal(tcambio);

                        this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                        this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                        this.TotalPedido = "TOTAL : S/. 0.00";
                        this.TCambio = "T/C : " + tcambio;
                        this.TDolar = " TOTAL : $ 0.00";
                        this.Fecha_Registro = "";

                        this.Fecha_Transcurrido = "";
                        this.IdCliente = null;
                        this.Nombre_Mozo = "";
                        this.Nro_Personas = "1";
                        this.NroDiario = "";
                        this.NomCliente = "";
                        this.Motorizado = "0";
                        this.TelCliente = "";
                        Application.Current.Properties["LlevarCliente"] = false;
                        if (this.NombreMesa.Contains("DELIVERY"))
                        {
                            DataTable dt = new DataTable();
                            dt = negDetVent.GetCargarPlatosSelect();
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                this.DataPlatos = negPlatos.GetPlatosActivos();
                                PasarPlatos(Convert.ToInt32(dt.Rows[j]["IDCARTA"]));
                                this.DataPlatos = new ObservableCollection<Platos>();
                            }
                            this.ClienteDelivery = "Visible";
                            var delivery = new DialogCliente
                            {

                            };
                            var x = await DialogHost.Show(delivery, "RootDialog");
                            if (Application.Current.Properties["ClienteDelivery"] == null)
                            {
                                NomCliente = "";
                                Cancelar();
                                return;
                            }
                            else
                            {
                                NomCliente = Application.Current.Properties["ClienteDelivery"].ToString();
                            }
                            if (Application.Current.Properties["IdClienteDelivery"] == null)
                            {
                                IdCliente = "0";
                                Cancelar();
                                return;
                            }
                            else
                            {
                                IdCliente = Application.Current.Properties["IdClienteDelivery"].ToString();
                            }

                            //DataTable mesa3 = new DataTable();
                            //mesa3 = negPedido.GET_MESA_OCUPADA(Convert.ToInt32(id));

                            //if (mesa3.Rows[0]["M_ATENDIDA"].ToString() == "1")
                            //{
                            //    this.ClienteDelivery = "Collapsed";
                            //    IdCliente = "0";
                            //    NomCliente = "";
                            //    globales.Mensaje("SOS-FOOD - Informacion", "La mesa esta siendo atendida", 2);
                            //    return;
                            //}
                        }
                        else
                        {
                            this.ClienteDelivery = "Collapsed";
                        }
                        //if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                        //{
                        this.ClienteDelivery = "Visible";
                        Application.Current.Properties["LlevarCliente"] = true;
                        //}
                    }
                    this.isGrupoCarta = "Hidden";
                    this.isCategoriaCarta = "Visible";
                    this.Operacion = "RegistrarPedido";
                    this.MesaPadre = "";
                    this.IsOpenDialogSubMesa = false;
                    negPedido.ActualizarMesaOcupada(Convert.ToInt32(id));
                    Application.Current.Properties["MesaBloqueada"] = "SI";
                    if (DatasCategoriacarta.Count() == 1)
                    {
                        CargarGrupo(DatasCategoriacarta.FirstOrDefault().ID);
                    }
                }
                else
                {
                    this.IsOpenDialogSubMesa = true;
                    this.Operacion = "SubMesas";
                    var nombrePadre = directoryStructure.NombreMesaPadre(id);
                    this.MesaPadre = nombrePadre;
                }
                this.Mesa = new Mesa();
                HabilitarPedir = "True";
            }
            catch (Exception ex)
            {
                HabilitarPedir = "True";
            }
        }

        private void CargarGrupo(object id)
        {
            var grupocarta = negsGrupo.GetGupoxCategoria(int.Parse(id.ToString()));
            this.DatasGrupo = new ObservableCollection<GrupoCartaItemViewModel>(
                grupocarta.Select(g => new GrupoCartaItemViewModel(g.idgrup, g.idscat, g.nomscat, g.idcat, g.nomcat, g.nomgrup, g.imagengrup, Convert.ToDecimal(g.descgrup))));
            if (grupocarta.Count == 0)
            {

            }
            else
            {
                if (grupocarta.Count == 1)
                {
                    DataPlatos = negPlatos.GetPlatosxGrupo(int.Parse(grupocarta.ToList().FirstOrDefault().idgrup.ToString()));
                    this.CodGrupo = grupocarta.ToList().FirstOrDefault().idgrup.ToString();
                    this.Mesa = new Mesa();
                }
                else
                {
                    this.isGrupoCarta = "Visible";
                    this.isCategoriaCarta = "Hidden";
                }
            }
            this.Mesa = new Mesa();
        }
        private void CargarCarta(object id)
        {
            DataPlatos = negPlatos.GetPlatosxGrupo(int.Parse(id.ToString()));
            this.CodGrupo = id.ToString();
            this.Mesa = new Mesa();
        }
        private void MostrarCarta(object id)
        {
            globales.Mensaje("SOS-FOOD - Informacion", id.ToString(), 2);
        }
        private void AgregarDescripcion(object id)
        {
            Platos pla = new Platos();
            var asd = this.DataPedido.Where(p => p.idplato == (int)id).FirstOrDefault();
            asd.comentario = "";
        }
        private async void PasarPlatos(object id)
        {
            try
            {
                this.WidthPantalla = System.Windows.SystemParameters.PrimaryScreenWidth;
                this.HeigthPantalla = System.Windows.SystemParameters.PrimaryScreenHeight;
                if (WidthPantalla == 1024 && HeigthPantalla == 768)
                {
                    this.Width = 100;
                }
                else
                {
                    this.Width = 500;
                }
                this.PlatosList = new List<Platos>();
                string cantidad = "1";
                Platos pla = new Platos();
                Platos pla2 = new Platos();
                int orden = 0;
                string textonivel = "";
                if (this.DataPedido.Where(w => w.idplato == Convert.ToInt32(id)).FirstOrDefault() != null)
                {
                    var asd2 = this.DataPedido.Where(p => p.idplato == Convert.ToInt32(id)).FirstOrDefault();
                    PlatosList.Add(asd2);
                    pla = PlatosList.ToList().FirstOrDefault();
                    if (this.DataPedido.Where(w => w.idplato == Convert.ToInt32(id)).FirstOrDefault() == null)
                    {
                        if (Convert.ToDecimal(pla.precplato) == 0)
                        {
                            var precio = new DialogPrecio()
                            {

                            };
                            var x4 = await DialogHost.Show(precio, "RootDialog");
                            if (Application.Current.Properties["NuevoPrecio0"] != null)
                            {
                                pla.precplato = Application.Current.Properties["NuevoPrecio0"].ToString();
                                pla.importe = (Convert.ToDecimal(pla.precplato) * Convert.ToDecimal(pla.cantidad)).ToString();
                            }
                            else
                            {
                                return;
                            }
                        }
                        if (pla.nivelplato != "0" && pla.nivelplato != null && pla.nivelplato != "1")
                        {
                            string[] idnivel;
                            char[] spearatorScat = { ',' };

                            idnivel = pla.nivelplato.Split(spearatorScat).ToArray();
                            idnivel = idnivel.Distinct().ToArray();

                            for (int i = 0; i <= idnivel.Count() - 1; i++)
                            {
                                if (idnivel[i].ToString() != "1" && idnivel[i].ToString() != "0" && idnivel[i].ToString() != null)
                                {
                                    Application.Current.Properties["OP_LDRP"] = "Opcion";
                                    Application.Current.Properties["ID_NIVEL_LDRP"] = idnivel[i].ToString();
                                    var x = new NivelDialog()
                                    {

                                    };
                                    var x2 = await DialogHost.Show(x, "RootDialog");
                                    if (Application.Current.Properties["NO_PASAR_NIVEL"] != null)
                                    {
                                        Application.Current.Properties["NO_PASAR_NIVEL"] = null;
                                        return;
                                    }
                                    if (textonivel.Trim() == "")
                                    {
                                        textonivel = Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
                                    }
                                    else
                                    {
                                        textonivel = textonivel + " - " + Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
                                    }
                                    Application.Current.Properties["TEXTO_NIVEL_LDRP"] = textonivel;
                                    /* agregar id de la carta del nivel menu / combo */
                                    if (NivelViewModel_.TextoNivel != null)
                                    {
                                        DataTable impresoras_plato = new DataTable();
                                        //aqui ya obtengo las impresoras del id del plato que le mando 
                                        foreach (Ent_Nivel e in NivelViewModel_.TextoNivel)
                                        {
                                            idCartaSubNivel.Add(e.ID_SN.ToString());
                                        }
                                        string[] tmp = idCartaSubNivel.ToArray();
                                        pla.idCartaSubNivel = string.Join(",", tmp);
                                        this.idCartaSubNivel = new List<string>();
                                    }
                                }
                            }
                        }
                        else
                        {
                            Application.Current.Properties["TEXTO_NIVEL_LDRP"] = "";
                        }
                        if (Application.Current.Properties["SEGUNDO_SOLO"] != null)
                        {
                            if (Convert.ToBoolean(Application.Current.Properties["SEGUNDO_SOLO"]) == true)
                            {
                                decimal desc = negPlatos.GetDescPlatoGrupo(pla.idplato);
                                pla.precplato = Convert.ToDecimal(pla.precplato) - desc;
                                pla.importe = (Convert.ToDecimal(pla.precplato) * Convert.ToDecimal(pla.cantidad)).ToString();
                            }
                        }
                        orden = this.DataPedido.Count();
                        orden += 1;
                        pla.orden = orden;
                        pla.comentario = Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
                        this.DataPedido.Add(pla);
                        this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                        this.total_numero = this.DataPedido.Sum(w => Convert.ToDecimal(w.importe));
                        this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
                    }
                    else
                    {
                        if (Convert.ToDecimal(pla.precplato) == 0)
                        {
                            var precio = new DialogPrecio() { };
                            var x4 = await DialogHost.Show(precio, "RootDialog");
                            if (Application.Current.Properties["NuevoPrecio0"] != null)
                            {
                                asd2.precplato = Application.Current.Properties["NuevoPrecio0"].ToString();
                            }
                            else
                            {
                                return;
                            }
                        }
                        if (pla.nivelplato != "0" && pla.nivelplato != null && pla.nivelplato != "1")
                        {
                            string[] idnivel;
                            char[] spearatorScat = { ',' };

                            idnivel = pla.nivelplato.Split(spearatorScat).ToArray();
                            idnivel = idnivel.Distinct().ToArray();

                            for (int i = 0; i <= idnivel.Count() - 1; i++)
                            {
                                if (idnivel[i].ToString() != "1" && idnivel[i].ToString() != "0" && idnivel[i].ToString() != null)
                                {
                                    Application.Current.Properties["OP_LDRP"] = "Opcion";
                                    Application.Current.Properties["ID_NIVEL_LDRP"] = idnivel[i].ToString();
                                    var x = new NivelDialog()
                                    {

                                    };
                                    var x2 = await DialogHost.Show(x, "RootDialog");
                                    if (Application.Current.Properties["NO_PASAR_NIVEL"] != null)
                                    {
                                        Application.Current.Properties["NO_PASAR_NIVEL"] = null;
                                        return;
                                    }
                                    if (textonivel.Trim() == "")
                                    {
                                        textonivel = Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
                                    }
                                    else
                                    {
                                        textonivel = textonivel + " - " + Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
                                    }
                                    Application.Current.Properties["TEXTO_NIVEL_LDRP"] = textonivel;
                                }
                            }
                            //Application.Current.Properties["OP_LDRP"] = "Opcion";
                            //Application.Current.Properties["ID_NIVEL_LDRP"] = pla.nivelplato;
                            //var x = new NivelDialog()
                            //{

                            //};
                            //var x2 = await DialogHost.Show(x, "RootDialog");
                            //if (Application.Current.Properties["NO_PASAR_NIVEL"] != null)
                            //{
                            //    Application.Current.Properties["NO_PASAR_NIVEL"] = null;
                            //    return;
                            //}
                        }
                        else
                        {
                            Application.Current.Properties["TEXTO_NIVEL_LDRP"] = "";
                        }
                        int ordenitem = 1;
                        foreach (var item in this.DataPedido.Where(w => w.idplato == Convert.ToInt32(id)))
                        {
                            if (item.comentario == Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString())
                            {
                                this.PlatosList = new List<Platos>();
                                asd2 = this.DataPedido.Where(p => p.orden == item.orden).FirstOrDefault();
                                ordenitem = item.orden;
                                PlatosList.Add(asd2);
                                pla = PlatosList.ToList().FirstOrDefault();
                            }
                        }
                        if (asd2.comentario == Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString())
                        {
                            int cant = 0;
                            cant = Convert.ToInt32(asd2.cantidad);
                            double imp = 0.00;
                            imp = Convert.ToDouble(asd2.precplato);
                            if (Application.Current.Properties["SEGUNDO_SOLO"] != null)
                            {
                                if (Convert.ToBoolean(Application.Current.Properties["SEGUNDO_SOLO"]) == true)
                                {
                                    decimal desc = negPlatos.GetDescPlatoGrupo(pla.idplato);
                                    imp = Convert.ToDouble(Convert.ToDecimal(asd2.precplato));
                                }
                            }
                            this.DataPedido.Remove(this.DataPedido.Where(w => w.idplato == Convert.ToInt32(id)).FirstOrDefault());
                            //orden = this.DataPedido.Count();
                            //orden += 1;
                            pla.cantidad = (cant + 1).ToString();
                            pla.importe = (imp * Convert.ToDouble(pla.cantidad)).ToString();
                            pla.comentario = Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
                            //pla.orden = this.DataPedido.Count();
                            this.DataPedido.Add(pla);
                            this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                            this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
                        }
                        else
                        {
                            //this.DataPlatos = negPlatos.GetPlatosActivos();
                            DataPlatos = negPlatos.GetPlatosxGrupo(int.Parse(this.CodGrupo));
                            if (DataPlatos == null)
                            {
                                this.DataPlatos = negPlatos.GetPlatosActivos();
                            }
                            if (DataPlatos.Count == 0)
                            {
                                this.DataPlatos = negPlatos.GetPlatosActivos();
                            }
                            var asd = this.DataPlatos.Where(p => p.idplato == Convert.ToInt32(id)).FirstOrDefault();

                            if (asd.cantidad is null)
                            {
                                asd.cantidad = cantidad;
                                asd.importe = asd.precplato.ToString();
                            }

                            this.PlatosList2 = new List<Platos>();
                            PlatosList2.Add(asd);
                            pla2 = PlatosList2.ToList().FirstOrDefault();

                            if (Application.Current.Properties["SEGUNDO_SOLO"] != null)
                            {
                                if (Convert.ToBoolean(Application.Current.Properties["SEGUNDO_SOLO"]) == true)
                                {
                                    decimal desc = negPlatos.GetDescPlatoGrupo(pla2.idplato);
                                    pla2.precplato = Convert.ToDecimal(pla2.precplato) - desc;
                                    pla2.importe = (Convert.ToDecimal(pla2.precplato) * Convert.ToDecimal(pla2.cantidad)).ToString();
                                }
                            }
                            orden = this.DataPedido.Count();
                            orden += 1;
                            pla2.orden = orden;
                            pla2.comentario = Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();

                            this.DataPedido.Add(pla2);
                            this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                            this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
                        }

                    }
                }
                else
                {
                    var asd = this.DataPlatos.Where(p => p.idplato == Convert.ToInt32(id)).FirstOrDefault();
                    if (asd.cantidad is null)
                    {
                        asd.cantidad = cantidad;
                        asd.importe = asd.precplato.ToString();
                    }
                    PlatosList.Add(asd);
                    pla = PlatosList.ToList().FirstOrDefault();
                    if (this.DataPedido.Where(w => w.idplato == Convert.ToInt32(id)).FirstOrDefault() == null)
                    {
                        if (Convert.ToDecimal(pla.precplato) == 0)
                        {
                            var precio = new DialogPrecio() { };
                            var x4 = await DialogHost.Show(precio, "RootDialog");
                            if (Application.Current.Properties["NuevoPrecio0"] != null)
                            {
                                pla.precplato = Application.Current.Properties["NuevoPrecio0"].ToString();
                                pla.importe = (Convert.ToDecimal(pla.precplato) * Convert.ToDecimal(pla.cantidad)).ToString();
                            }
                            else
                            {
                                return;
                            }
                        }
                        if (pla.nivelplato != "0" && pla.nivelplato != null && pla.nivelplato != "1")
                        {
                            string[] idnivel;
                            char[] spearatorScat = { ',' };

                            idnivel = pla.nivelplato.Split(spearatorScat).ToArray();
                            idnivel = idnivel.Distinct().ToArray();

                            for (int i = 0; i <= idnivel.Count() - 1; i++)
                            {
                                if (idnivel[i].ToString() != "1" && idnivel[i].ToString() != "0" && idnivel[i].ToString() != null)
                                {
                                    Application.Current.Properties["OP_LDRP"] = "Opcion";
                                    Application.Current.Properties["ID_NIVEL_LDRP"] = idnivel[i].ToString();
                                    var x = new NivelDialog()
                                    {

                                    };
                                    var x2 = await DialogHost.Show(x, "RootDialog");
                                    if (Application.Current.Properties["NO_PASAR_NIVEL"] != null)
                                    {
                                        Application.Current.Properties["NO_PASAR_NIVEL"] = null;
                                        return;
                                    }
                                    if (textonivel.Trim() == "")
                                    {
                                        textonivel = Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
                                    }
                                    else
                                    {
                                        textonivel = textonivel + " - " + Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
                                    }
                                    Application.Current.Properties["TEXTO_NIVEL_LDRP"] = textonivel;
                                    /* agregar id de la carta del nivel menu / combo */
                                    if (NivelViewModel_.TextoNivel != null)
                                    {
                                        DataTable impresoras_plato = new DataTable();
                                        //aqui ya obtengo las impresoras del id del plato que le mando 
                                        foreach (Ent_Nivel e in NivelViewModel_.TextoNivel)
                                        {
                                            idCartaSubNivel.Add(e.ID_SN.ToString());
                                        }
                                        string[] tmp = idCartaSubNivel.ToArray();
                                        pla.idCartaSubNivel = string.Join(",", tmp);
                                        this.idCartaSubNivel = new List<string>();
                                    }
                                }
                            }
                            //Application.Current.Properties["OP_LDRP"] = "Opcion";
                            //Application.Current.Properties["ID_NIVEL_LDRP"] = pla.nivelplato;
                            //DialogHost.CloseDialogCommand.Execute(null, null);
                            //var x = new NivelDialog()
                            //{

                            //};
                            //var x2 = await DialogHost.Show(x, "RootDialog");
                            //if (Application.Current.Properties["NO_PASAR_NIVEL"] != null)
                            //{
                            //    Application.Current.Properties["NO_PASAR_NIVEL"] = null;
                            //    return;
                            //}
                            ///* agregar id de la carta del nivel menu / combo */
                            //if (NivelViewModel_.TextoNivel != null)
                            //{
                            //    DataTable impresoras_plato = new DataTable();
                            //    //aqui ya obtengo las impresoras del id del plato que le mando 
                            //    foreach (Ent_Nivel e in NivelViewModel_.TextoNivel)
                            //    {
                            //        idCartaSubNivel.Add(e.ID_SN.ToString());
                            //    }
                            //    string[] tmp = idCartaSubNivel.ToArray();
                            //    pla.idCartaSubNivel = string.Join(",", tmp);
                            //    this.idCartaSubNivel = new List<string>();
                            //}
                        }
                        else
                        {
                            Application.Current.Properties["TEXTO_NIVEL_LDRP"] = "";
                        }
                        if (Application.Current.Properties["SEGUNDO_SOLO"] != null)
                        {
                            if (Convert.ToBoolean(Application.Current.Properties["SEGUNDO_SOLO"]) == true)
                            {
                                decimal desc = negPlatos.GetDescPlatoGrupo(pla.idplato);
                                pla.precplato = Convert.ToDecimal(pla.precplato) - desc;
                                pla.importe = (Convert.ToDecimal(pla.precplato) * Convert.ToDecimal(pla.cantidad)).ToString();
                            }
                        }
                        orden = this.DataPedido.Count();
                        orden += 1;
                        pla.orden = orden;
                        pla.comentario = Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
                        this.DataPedido.Add(pla);
                        this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                        this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
                    }
                    else
                    {
                        if (Convert.ToDecimal(pla.precplato) == 0)
                        {
                            var precio = new DialogPrecio()
                            {

                            };
                            var x4 = await DialogHost.Show(precio, "RootDialog");
                            if (Application.Current.Properties["NuevoPrecio0"] != null)
                            {
                                asd.importe = Application.Current.Properties["NuevoPrecio0"].ToString();
                            }
                            else
                            {
                                return;
                            }
                        }
                        if (pla.nivelplato != "0" && pla.nivelplato != null && pla.nivelplato != "1")
                        {
                            string[] idnivel;
                            char[] spearatorScat = { ',' };

                            idnivel = pla.nivelplato.Split(spearatorScat).ToArray();
                            idnivel = idnivel.Distinct().ToArray();

                            for (int i = 0; i <= idnivel.Count() - 1; i++)
                            {
                                if (idnivel[i].ToString() != "1" && idnivel[i].ToString() != "0" && idnivel[i].ToString() != null)
                                {
                                    Application.Current.Properties["OP_LDRP"] = "Opcion";
                                    Application.Current.Properties["ID_NIVEL_LDRP"] = idnivel[i].ToString();
                                    var x = new NivelDialog()
                                    {

                                    };
                                    var x2 = await DialogHost.Show(x, "RootDialog");
                                    if (Application.Current.Properties["NO_PASAR_NIVEL"] != null)
                                    {
                                        Application.Current.Properties["NO_PASAR_NIVEL"] = null;
                                        return;
                                    }
                                    if (textonivel.Trim() == "")
                                    {
                                        textonivel = Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();

                                    }
                                    else
                                    {
                                        textonivel = textonivel + " - " + Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
                                    }

                                    Application.Current.Properties["TEXTO_NIVEL_LDRP"] = textonivel;
                                }
                            }
                            //Application.Current.Properties["OP_LDRP"] = "Opcion";
                            //Application.Current.Properties["ID_NIVEL_LDRP"] = pla.nivelplato;
                            //var x = new NivelDialog()
                            //{

                            //};
                            //var x2 = await DialogHost.Show(x, "RootDialog");
                            //if (Application.Current.Properties["NO_PASAR_NIVEL"] != null)
                            //{
                            //    Application.Current.Properties["NO_PASAR_NIVEL"] = null;
                            //    return;
                            //}
                        }
                        else
                        {
                            Application.Current.Properties["TEXTO_NIVEL_LDRP"] = "";
                        }

                        int cant = 0;
                        cant = Convert.ToInt32(asd.cantidad);
                        double imp = 0.00;
                        imp = Convert.ToDouble(asd.importe);
                        if (Application.Current.Properties["SEGUNDO_SOLO"] != null)
                        {
                            if (Convert.ToBoolean(Application.Current.Properties["SEGUNDO_SOLO"]) == true)
                            {
                                decimal desc = negPlatos.GetDescPlatoGrupo(pla.idplato);
                                imp = Convert.ToDouble(Convert.ToDecimal(asd.importe));
                            }
                        }
                        if (pla.comentario == Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString())
                        {
                            this.DataPedido.Remove(this.DataPedido.Where(w => w.idplato == Convert.ToInt32(id)).FirstOrDefault());
                            //orden = this.DataPedido.Count();
                            //orden += 1;
                            pla.cantidad = (cant + 1).ToString();
                            pla.importe = (imp * Convert.ToDouble(pla.cantidad)).ToString();
                            pla.comentario = Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
                            //pla.orden = orden;
                            this.DataPedido.Add(pla);
                            this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                            this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
                        }
                        else
                        {
                            //this.DataPlatos = negPlatos.GetPlatosActivos();
                            DataPlatos = negPlatos.GetPlatosxGrupo(int.Parse(this.CodGrupo));
                            var asd2 = this.DataPlatos.Where(p => p.idplato == Convert.ToInt32(id)).FirstOrDefault();

                            if (asd2.cantidad is null)
                            {
                                asd2.cantidad = cantidad;
                                asd2.importe = asd2.precplato.ToString();
                            }

                            this.PlatosList2 = new List<Platos>();
                            PlatosList2.Add(asd);
                            pla2 = PlatosList2.ToList().FirstOrDefault();

                            if (Application.Current.Properties["SEGUNDO_SOLO"] != null)
                            {
                                if (Convert.ToBoolean(Application.Current.Properties["SEGUNDO_SOLO"]) == true)
                                {
                                    decimal desc = negPlatos.GetDescPlatoGrupo(pla2.idplato);
                                    pla2.precplato = Convert.ToDecimal(pla2.precplato) - desc;
                                    pla2.importe = (Convert.ToDecimal(pla2.precplato) * Convert.ToDecimal(pla2.cantidad)).ToString();
                                }
                            }
                            orden = this.DataPedido.Count();
                            orden += 1;
                            pla2.orden = orden;
                            pla2.comentario = Application.Current.Properties["TEXTO_NIVEL_LDRP"].ToString();
                            this.DataPedido.Add(pla2);
                            this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                            this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
                        }
                    }
                }

                Application.Current.Properties["SEGUNDO_SOLO"] = false;
                List<Platos> ll = new List<Platos>();
                ll = DataPedido.OrderByDescending(o => o.orden).ToList();
                ObservableCollection<Platos> lll = new ObservableCollection<Platos>(ll);
                DataPedido = lll;
            }
            catch (Exception ex)
            {
            }
        }

        private void EliminarPlatos(object id)
        {
            if (this.DataPedido.Where(w => w.orden == (int)id).FirstOrDefault() != null)
            {
                this.DataPedidoTemp = new ObservableCollection<Platos>();
                this.DataPlatos.Remove(this.DataPedido.Where(w => w.orden == (int)id).FirstOrDefault());
                this.DataPedido.Remove(this.DataPedido.Where(w => w.orden == (int)id).FirstOrDefault());
                int orden = 0;
                foreach (Platos item in this.DataPedido)
                {
                    orden += 1;
                    item.orden = orden;
                    this.DataPedidoTemp.Add(item);
                }
                this.DataPedido = this.DataPedidoTemp;
                this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
                this.DataPlatosBusqueda = negPlatos.GetPlatosActivos();
                this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                DataPlatos = negPlatos.GetPlatosxGrupo(int.Parse(this.CodGrupo));
                this.Mesa = new Mesa();
            }
        }
        private void PonerLLevar(object id)
        {
            try
            {

                var comentario2 = this.DataPedido.Where(w => w.orden == (int)id).FirstOrDefault().comentario;
                this.PlatosList = new List<Platos>();
                Platos pla = new Platos();
                var asd2 = this.DataPedido.Where(p => p.orden == (int)id).FirstOrDefault();
                PlatosList.Add(asd2);
                pla = PlatosList.ToList().FirstOrDefault();
                this.DataPedido.Remove(this.DataPedido.Where(w => w.orden == (int)id).FirstOrDefault());
                //orden = this.DataPedido.Count();
                //orden += 1;
                if (comentario2.Contains("LLEVAR"))
                {
                    pla.comentario = comentario2.Replace(" (LLEVAR)", "");
                    pla.ischeck = false;
                }
                else
                {
                    pla.comentario = comentario2 + " (LLEVAR)";
                    pla.ischeck = true;
                }

                //pla.orden = this.DataPedido.Count();
                this.DataPedido.Add(pla);
                this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
            }
            catch (Exception ex)
            {
                // globales.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }

        }
        private async void EliminarPlatoPedido(object id)
        {
            Application.Current.Properties["EstPedidoAnulacion"] = "PENDIENTE";
            if (Pedido.Count != 0)
            {
                if (this.DescuentoPedido != "DESCUENTO : S/. 0.00")
                {
                    var Dialog = new MessageDialog
                    {
                        Mensaje = { Text = "No debe haber DESCUENTO para eliminar platos" }
                    };
                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                    return;
                }

                DataTable dt = new DataTable();
                dt = negPedido.sp_verificar_plato_cuenta(Convert.ToInt32(id));

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        var Dialog = new MessageDialog
                        {
                            Mensaje = { Text = "No puede eliminar un plato que esta vinculado a una SubCuenta.En todo caso elimine la subcuenta asociada." }
                        };
                        var x2 = await DialogHost.Show(Dialog, "RootDialog");
                        return;
                    }
                }

                if (Pedido.Count == 1)
                {
                    if (Pedido.ToList().First().DP_CANT > 1)
                    {
                        Application.Current.Properties["OperPagoCel"] = "ANULAR PLATO";
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        var opencampago = new DialogAnularPedido
                        {

                        };
                        var x3 = await DialogHost.Show(opencampago, "RootDialog");

                        if (Application.Current.Properties["AnularPedido"] != null)
                        {
                            DialogHost.CloseDialogCommand.Execute(null, null);
                            decimal cantidad = this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault().DP_CANT;
                            if (cantidad > 1)
                            {
                                Application.Current.Properties["CantidadCombo"] = cantidad;
                                Application.Current.Properties["Solo"] = "SI";
                                var combocantidad = new DialogComboCantidad
                                {

                                };
                                var x4 = await DialogHost.Show(combocantidad, "RootDialog");
                                if (Application.Current.Properties["CantidadComboAnulado"] == null)
                                {
                                    DialogHost.CloseDialogCommand.Execute(null, null);
                                    return;
                                }
                                else
                                {
                                    DialogHost.CloseDialogCommand.Execute(null, null);
                                    var SiNo = new SiNoMessageDialog
                                    {
                                        Mensaje = { Text = "Esta seguro de eliminar el plato" }
                                    };
                                    var x = await DialogHost.Show(SiNo, "RootDialog");
                                    if ((bool)x)
                                    {
                                        if (this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault() != null)
                                        {
                                            int idPed = this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault().DP_ID_PED;

                                            bool result = false;
                                            string _mensaje = "";
                                            if (Application.Current.Properties["ComentarioPLato"] == null)
                                            {
                                                Application.Current.Properties["ComentarioPLato"] = "Error Digitacion";
                                            }
                                            int idusuarioanulacion;
                                            idusuarioanulacion = Convert.ToInt32(Application.Current.Properties["IdUsuarioAnulacion"]);
                                            string comentario = Application.Current.Properties["ComentarioPLato"].ToString();
                                            result = negPedido.EliminarPlatoPedidoCantidad(Convert.ToInt32(id), idPed, comentario, Convert.ToDecimal(Application.Current.Properties["CantidadComboAnulado"]), ref _mensaje, idusuarioanulacion);

                                            if (result == true)
                                            {
                                                if (negParametros.ImpComAnuladaPlato())
                                                {
                                                    ImprimirComandaPlatoAnulada(Convert.ToInt32(id));
                                                }
                                                if (negParametros.ImpComAnuladaCaja())
                                                {
                                                    ImpComandaPlatoAnuladoCaja(Convert.ToInt32(id));
                                                }
                                                //ImprimirComandaPlatoAnuladaRecutacu(Convert.ToInt32(id));
                                                //ImpComandaPlatoAnuladoCajaRecutacu(Convert.ToInt32(id));
                                                this.Pedido = negPedido.GetPedidoxId(Convert.ToInt32(idPed));
                                                if (this.Pedido != null && this.Pedido.Count != 0)
                                                {
                                                    this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                                                    this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                                                    this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                                                    this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                                                    TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                                                    this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                                                    this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                                                    this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                                                    this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                                                }
                                                else
                                                {
                                                    this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                                                    this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                                                    this.TotalPedido = "TOTAL : S/. 0.00";
                                                    this.Fecha_Registro = "";
                                                    this.Fecha_Transcurrido = "";
                                                    this.Nombre_Mozo = "";
                                                    this.Nro_Personas = "1";
                                                    this.NroDiario = "";
                                                }
                                            }
                                        }
                                        Application.Current.Properties["AnularPedido"] = null;
                                    }
                                }
                            }
                            else
                            {
                                DialogHost.CloseDialogCommand.Execute(null, null);
                                var SiNo = new SiNoMessageDialog
                                {
                                    Mensaje = { Text = "Esta seguro de eliminar el plato" }
                                };
                                var x = await DialogHost.Show(SiNo, "RootDialog");
                                if ((bool)x)
                                {
                                    if (this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault() != null)
                                    {
                                        int idPed = this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault().DP_ID_PED;
                                        bool result = false;
                                        string _mensaje = "";
                                        if (Application.Current.Properties["ComentarioPLato"] == null)
                                        {
                                            Application.Current.Properties["ComentarioPLato"] = "Error Digitacion";
                                        }
                                        string comentario = Application.Current.Properties["ComentarioPLato"].ToString();
                                        int idusuarioanulacion;
                                        idusuarioanulacion = Convert.ToInt32(Application.Current.Properties["IdUsuarioAnulacion"]);
                                        result = negPedido.EliminarPlatoPedido(Convert.ToInt32(id), idPed, comentario, ref _mensaje, idusuarioanulacion);

                                        if (result == true)
                                        {
                                            if (negParametros.ImpComAnuladaPlato())
                                            {
                                                ImprimirComandaPlatoAnulada(Convert.ToInt32(id));
                                            }
                                            if (negParametros.ImpComAnuladaCaja())
                                            {
                                                ImpComandaPlatoAnuladoCaja(Convert.ToInt32(id));
                                            }
                                            //ImprimirComandaPlatoAnuladaRecutacu(Convert.ToInt32(id));
                                            //ImpComandaPlatoAnuladoCajaRecutacu(Convert.ToInt32(id));
                                            this.Pedido = negPedido.GetPedidoxId(Convert.ToInt32(idPed));
                                            if (this.Pedido != null && this.Pedido.Count != 0)
                                            {
                                                this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                                                this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                                                this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                                                this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                                                TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                                                this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                                                this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                                                this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                                                this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                                            }
                                            else
                                            {
                                                this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                                                this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                                                this.TotalPedido = "TOTAL : S/. 0.00";
                                                this.Fecha_Registro = "";
                                                this.Fecha_Transcurrido = "";
                                                this.Nombre_Mozo = "";
                                                this.Nro_Personas = "1";
                                                this.NroDiario = "";
                                            }
                                        }
                                    }
                                    Application.Current.Properties["AnularPedido"] = null;
                                }
                            }
                        }
                    }
                    else
                    {
                        var Dialog = new MessageDialog
                        {
                            Mensaje = { Text = "Solo queda un plato" }
                        };
                        var x2 = await DialogHost.Show(Dialog, "RootDialog");
                    }

                }
                else
                {
                    Application.Current.Properties["OperPagoCel"] = "ANULAR PLATO";
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    var opencampago = new DialogAnularPedido
                    {

                    };
                    var x3 = await DialogHost.Show(opencampago, "RootDialog");

                    if (Application.Current.Properties["AnularPedido"] != null)
                    {
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        decimal cantidad = this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault().DP_CANT;
                        if (cantidad > 1)
                        {
                            Application.Current.Properties["CantidadCombo"] = cantidad;
                            Application.Current.Properties["Solo"] = "NO";
                            var combocantidad = new DialogComboCantidad
                            {

                            };
                            var x4 = await DialogHost.Show(combocantidad, "RootDialog");
                            if (Application.Current.Properties["CantidadComboAnulado"] == null)
                            {
                                DialogHost.CloseDialogCommand.Execute(null, null);
                                return;
                            }
                            else
                            {
                                DialogHost.CloseDialogCommand.Execute(null, null);
                                var SiNo = new SiNoMessageDialog
                                {
                                    Mensaje = { Text = "Esta seguro de eliminar el plato" }
                                };
                                var x = await DialogHost.Show(SiNo, "RootDialog");
                                if ((bool)x)
                                {
                                    if (this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault() != null)
                                    {
                                        int idPed = this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault().DP_ID_PED;

                                        bool result = false;
                                        string _mensaje = "";
                                        if (Application.Current.Properties["ComentarioPLato"] == null)
                                        {
                                            Application.Current.Properties["ComentarioPLato"] = "Error Digitacion";
                                        }
                                        int idusuarioanulacion;
                                        idusuarioanulacion = Convert.ToInt32(Application.Current.Properties["IdUsuarioAnulacion"]);
                                        string comentario = Application.Current.Properties["ComentarioPLato"].ToString();
                                        result = negPedido.EliminarPlatoPedidoCantidad(Convert.ToInt32(id), idPed, comentario, Convert.ToDecimal(Application.Current.Properties["CantidadComboAnulado"]), ref _mensaje, idusuarioanulacion);

                                        if (result == true)
                                        {
                                            if (negParametros.ImpComAnuladaPlato())
                                            {
                                                ImprimirComandaPlatoAnulada(Convert.ToInt32(id));
                                            }
                                            if (negParametros.ImpComAnuladaCaja())
                                            {
                                                ImpComandaPlatoAnuladoCaja(Convert.ToInt32(id));
                                            }
                                            //ImprimirComandaPlatoAnuladaRecutacu(Convert.ToInt32(id));
                                            //ImpComandaPlatoAnuladoCajaRecutacu(Convert.ToInt32(id));
                                            this.Pedido = negPedido.GetPedidoxId(Convert.ToInt32(idPed));
                                            if (this.Pedido != null && this.Pedido.Count != 0)
                                            {
                                                this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                                                this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                                                this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                                                this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                                                TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                                                this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                                                this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                                                this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                                                this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                                            }
                                            else
                                            {
                                                this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                                                this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                                                this.TotalPedido = "TOTAL : S/. 0.00";
                                                this.Fecha_Registro = "";
                                                this.Fecha_Transcurrido = "";
                                                this.Nombre_Mozo = "";
                                                this.Nro_Personas = "1";
                                                this.NroDiario = "";
                                            }
                                        }
                                    }
                                    Application.Current.Properties["AnularPedido"] = null;
                                }
                            }
                        }
                        else
                        {
                            DialogHost.CloseDialogCommand.Execute(null, null);
                            var SiNo = new SiNoMessageDialog
                            {
                                Mensaje = { Text = "Esta seguro de eliminar el plato" }
                            };
                            var x = await DialogHost.Show(SiNo, "RootDialog");
                            if ((bool)x)
                            {
                                if (this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault() != null)
                                {
                                    int idPed = this.Pedido.Where(w => w.ID == (int)id).FirstOrDefault().DP_ID_PED;

                                    bool result = false;
                                    string _mensaje = "";
                                    if (Application.Current.Properties["ComentarioPLato"] == null)
                                    {
                                        Application.Current.Properties["ComentarioPLato"] = "Error Digitacion";
                                    }
                                    string comentario = Application.Current.Properties["ComentarioPLato"].ToString();
                                    int idusuarioanulacion;
                                    idusuarioanulacion = Convert.ToInt32(Application.Current.Properties["IdUsuarioAnulacion"]);
                                    result = negPedido.EliminarPlatoPedido(Convert.ToInt32(id), idPed, comentario, ref _mensaje, idusuarioanulacion);

                                    if (result == true)
                                    {
                                        if (negParametros.ImpComAnuladaPlato())
                                        {
                                            ImprimirComandaPlatoAnulada(Convert.ToInt32(id));
                                        }
                                        if (negParametros.ImpComAnuladaCaja())
                                        {
                                            ImpComandaPlatoAnuladoCaja(Convert.ToInt32(id));
                                        }
                                        //ImprimirComandaPlatoAnuladaRecutacu(Convert.ToInt32(id));
                                        //ImpComandaPlatoAnuladoCajaRecutacu(Convert.ToInt32(id));
                                        this.Pedido = negPedido.GetPedidoxId(Convert.ToInt32(idPed));
                                        if (this.Pedido != null && this.Pedido.Count != 0)
                                        {
                                            this.SubTotalPedido = "SUB TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_SUBTOTAL.ToString();
                                            this.DescuentoPedido = "DESCUENTO : S/. " + this.Pedido.ToList().FirstOrDefault().PED_DESCUENTO.ToString();
                                            this.TotalPedido = "TOTAL : S/. " + this.Pedido.ToList().FirstOrDefault().PED_TOTAL.ToString();
                                            this.Fecha_Registro = this.Pedido.ToList().FirstOrDefault().DP_FEC_REG.ToLongTimeString();
                                            TimeSpan ts = DateTime.Now - this.Pedido.ToList().FirstOrDefault().DP_FEC_REG;
                                            this.Fecha_Transcurrido = ts.Days.ToString() + "d. " + ts.Hours.ToString() + "h. " + ts.Minutes.ToString() + "m. " + ts.Seconds.ToString() + "s. ";
                                            this.Nombre_Mozo = this.Pedido.ToList().FirstOrDefault().EMPL_NOM.ToString().ToUpper();
                                            this.Nro_Personas = this.Pedido.ToList().FirstOrDefault().PED_NRO_PERSONAS.ToString();
                                            this.NroDiario = this.Pedido.ToList().FirstOrDefault().PED_NUM_DIARIO.ToString();
                                        }
                                        else
                                        {
                                            this.SubTotalPedido = "SUB TOTAL : S/. 0.00";
                                            this.DescuentoPedido = "DESCUENTO : S/. 0.00";
                                            this.TotalPedido = "TOTAL : S/. 0.00";
                                            this.Fecha_Registro = "";
                                            this.Fecha_Transcurrido = "";
                                            this.Nombre_Mozo = "";
                                            this.Nro_Personas = "1";
                                            this.NroDiario = "";
                                        }
                                    }
                                }
                                Application.Current.Properties["AnularPedido"] = null;
                            }
                        }
                    }
                }
            }
            else
            {
                var Dialog = new MessageDialog
                {
                    Mensaje = { Text = "Debe haber pedidos para pedido" }
                };
                var x2 = await DialogHost.Show(Dialog, "RootDialog");
            }
        }
        private async void Elim()
        {
            var SiNo = new DialogEditPass
            {

            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
        }
        private async void Cantidad()
        {
            try
            {
                var CambiarCantidad = new DialogCambiarCantidad
                {

                };
                var x = await DialogHost.Show(CambiarCantidad, "RootDialog");
                if (Application.Current.Properties["NuevaCantidad"] != null)
                {
                    if (Convert.ToDecimal(Application.Current.Properties["NuevaCantidad"]) <= 0)
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "La cantidad no puede ser menor o igual a 0", 2);
                        return;

                    }
                    if (this.DataPedido.Where(w => w.orden == (int)(int)Application.Current.Properties["Orden"]).FirstOrDefault() != null)
                    {
                        this.PlatosList = new List<Platos>();
                        Platos pla = new Platos();
                        var asd2 = this.DataPedido.Where(p => p.orden == (int)Application.Current.Properties["Orden"]).FirstOrDefault();
                        PlatosList.Add(asd2);
                        pla = PlatosList.ToList().FirstOrDefault();
                        decimal cant = 0;
                        cant = Convert.ToDecimal(asd2.cantidad);
                        double imp = 0.00;
                        imp = Convert.ToDouble(asd2.precplato);
                        this.DataPedido.Remove(this.DataPedido.Where(w => w.orden == (int)Application.Current.Properties["Orden"]).FirstOrDefault());
                        //orden = this.DataPedido.Count();
                        //orden += 1;
                        pla.cantidad = Application.Current.Properties["NuevaCantidad"].ToString();
                        pla.importe = (imp * Convert.ToDouble(pla.cantidad)).ToString();
                        //pla.orden = this.DataPedido.Count();
                        this.DataPedido.Add(pla);
                        this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                        this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
                        Application.Current.Properties["CantidadActual"] = null;
                        Application.Current.Properties["Orden"] = null;
                    }

                }
            }
            catch (Exception ex)
            {
                //globales.Mensaje("SOS-FOOD - Error",ex.Message, 3);
            }

        }
        private async void AgregarDescripcion()
        {
            if (Application.Current.Properties["Orden"] != null)
            {
                Application.Current.Properties["Texto"] = this.DataPedido.Where(w => w.orden == (int)Application.Current.Properties["Orden"]).FirstOrDefault().comentario;
                var AgregarDescripcion = new DialogXstore
                {

                };
                var x = await DialogHost.Show(AgregarDescripcion, "RootDialog");
                if (Application.Current.Properties["Comentario"] != null)
                {
                    if (this.DataPedido.Where(w => w.orden == (int)Application.Current.Properties["Orden"]).FirstOrDefault() != null)
                    {
                        this.PlatosList = new List<Platos>();
                        Platos pla = new Platos();
                        var asd2 = this.DataPedido.Where(p => p.orden == (int)Application.Current.Properties["Orden"]).FirstOrDefault();
                        PlatosList.Add(asd2);
                        pla = PlatosList.ToList().FirstOrDefault();
                        this.DataPedido.Remove(this.DataPedido.Where(w => w.orden == (int)Application.Current.Properties["Orden"]).FirstOrDefault());
                        //orden = this.DataPedido.Count();
                        //orden += 1;
                        pla.comentario = Application.Current.Properties["Comentario"].ToString();

                        //pla.orden = this.DataPedido.Count();
                        this.DataPedido.Add(pla);
                        this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                        this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
                        Application.Current.Properties["Comentario"] = null;
                    }

                }
            }
            else
            {
                var Dialog = new MessageDialog
                {
                    Mensaje = { Text = "Debe seleccionar un plato primero" }
                };
                var x2 = await DialogHost.Show(Dialog, "RootDialog");
            }

        }
        private void BorrarDescripcion()
        {
            if (Application.Current.Properties["Orden"] != null)
            {
                if (this.DataPedido.Where(w => w.orden == (int)Application.Current.Properties["Orden"]).FirstOrDefault() != null)
                {
                    Application.Current.Properties["Texto"] = "";
                    this.PlatosList = new List<Platos>();
                    Platos pla = new Platos();
                    var asd2 = this.DataPedido.Where(p => p.orden == (int)Application.Current.Properties["Orden"]).FirstOrDefault();
                    PlatosList.Add(asd2);
                    pla = PlatosList.ToList().FirstOrDefault();
                    this.DataPedido.Remove(this.DataPedido.Where(w => w.orden == (int)Application.Current.Properties["Orden"]).FirstOrDefault());
                    //orden = this.DataPedido.Count();
                    //orden += 1;
                    pla.comentario = "";

                    //pla.orden = this.DataPedido.Count();
                    this.DataPedido.Add(pla);
                    this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                    this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
                    Application.Current.Properties["Comentario"] = null;
                }
            }
        }
        private void GetUserControl(string obj)
        {
            //this.UserControl = new Views.Configuracion.RegistrarPedido();
        }
        private void GetAmbientes()
        {
            var w = System.Windows.SystemParameters.PrimaryScreenWidth;
            var h = System.Windows.SystemParameters.PrimaryScreenHeight;

            var children = directoryStructure.GetLogicalDrives();
            this.DataAmbientes = new ObservableCollection<AmbientesItemViewModel>(
                children.Select(d => new AmbientesItemViewModel(d.ID, d.A_NOM, d.A_X, d.A_Y, d.A_WIDTH, d.A_HEIGHT, d.A_TEXTO, d.A_ACT, d.A_F_CREATE)));

        }
       
        private void GetMesas(object id)
        {
            try
            {
                var mesas = directoryStructure.GetLogicalDrivesMesas(id);
                this.DataMesas = new ObservableCollection<MesasItemViewModel>(
                    mesas.Select(c => new MesasItemViewModel(c.ID, c.M_NOM, c.M_EST, c.M_ID_AMB, c.M_X, c.M_ATENDIDA, c.M_WIDTH,
                    c.M_HEIGHT, c.M_TEXTO, c.M_TIPO, c.M_ACT, c.M_F_CREATE, c.color, c.M_ID_PADRE, c.M_F_MODIFICACION, c.NOMBRE_PADRE, c.PED_ID_CLIENTE, c.C_NOMINA, c.nomllevar, c.EMPL_NOM, c.mesa)));

                _reserva = rs.GetReservas(DateTime.Now, DateTime.Now);
                int contar_reserva = _reserva.Where(w => w.R_ID_ESTADO == 1 && (w.R_F_RESERVA_DESDE - DateTime.Now).TotalHours <= 2).Count();
                if (contar_reserva > 0)
                {
                    reserva = _reserva.Where(w => w.R_ID_ESTADO == 1 && (w.R_F_RESERVA_DESDE - DateTime.Now).TotalHours <= 2).ToList();
                    //this.DataMesas.Where(w => w.ID == reserva.FirstOrDefault().R_ID_MESA).FirstOrDefault().color = "#EEB810";

                    foreach (var h in reserva)
                    {
                        if (this.DataMesas.Where(w => w.ID == h.R_ID_MESA).Count() > 0)
                        {
                            if (this.DataMesas.Where(w => w.ID == h.R_ID_MESA && w.M_EST == 0).Count() > 0)
                            {
                                this.DataMesas.Where(w => w.ID == h.R_ID_MESA && w.M_EST == 0).FirstOrDefault().color = "#EEB810";
                            }
                            this.DataMesas.Where(w => w.ID == h.R_ID_MESA).FirstOrDefault().isreservada = 1;
                        }
                    }
                }

                neg_reserv.SetReservasEstados();

                //Obtener Nombre del Ambiente 
                var nom_amb = directoryStructure.GetLogicalDrivesNomAmb(Convert.ToInt32(id));
                this.nroColumnas = mesas.Select(s => s.NroColumnas).FirstOrDefault();
                this.NombreAmbiente = nom_amb.ToString();
                Application.Current.Properties["IdAmbiente"] = id.ToString();
            }
            catch (Exception ex)
            {
                //globales.Mensaje("SOS-FOOD - Error", ex.Message, 3);

            }
        }
        private void GetDatosEmpresa(int id_Empre)
        {
            try
            {
                var logoEmpresa = directoryStructure.ObtenerLogoEmpresa(1);
                var nombreEmpresa = directoryStructure.ObtenerNombreEmpresa(1);
                var logoSosFood = directoryStructure.ObtenerLogoSosFood();
                var logoSostic = directoryStructure.ObtenerLogoSosTic();
                var telefono1 = directoryStructure.ObtenerTelefono1();
                var telefono2 = directoryStructure.ObtenerTelefono2();
                var correo1 = directoryStructure.ObtenerCorreo();

                this.LogoEmpresa = logoEmpresa;
                this.LogoSosFood = logoSosFood;
                this.LogoSosTic = logoSostic;
                this.Telefono1 = telefono1;
                this.Telefono2 = telefono2;
                this.Correo1 = correo1;
                this.NombreEmpresa = nombreEmpresa;

                if (logoSostic.ToString() == "System.Byte[]")
                {
                    VisilibilityDesarrolladopor = "Collapsed";
                }
                else
                {
                    VisilibilityDesarrolladopor = "Visible";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void ImprimirCuenta(int idpedido)
        {
            DataTable impresora = new DataTable();
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
            impresora = negPlatos.GetImpresoraxDocumento(1); //Precuenta
            for (int i = 0; i < CountPrintLlevar; i++)
            {
                DataTable dt = new DataTable();
                dt = negDetVent.GetCuentaPedido(idpedido);
                Ticket ticket = new Ticket();
                ticket.MaxCharDescrip = negParametros.margenMaxDescrip();
                if (Convert.ToBoolean(negParametros.logoPreCuenta()) == true) { 
                    System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["EMPR_LOGO"]));
                    ticket.MargenLogo = negParametros.margenLogoTicket();
                    
                    ticket.HeaderImage = x;
                }
                
                ticket.AddSubHeaderLine("NRO. Orden: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                ticket.AddSubHeaderLine("Atendido Por: " + dt.Rows[0]["EMPL_NOM"].ToString());
                ticket.AddSubHeaderLine("Mesa: " + dt.Rows[0]["M_NOM"].ToString());
                ticket.AddSubHeaderLine("");

                if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                {
                    ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["nomllevar"].ToString());
                    ticket.AddSubHeaderLine("Telefono : " + dt.Rows[0]["telefcli"].ToString());
                }
                else
                {
                    ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["C_NOM"].ToString());
                    ticket.AddSubHeaderLine("Nro Documento: " + dt.Rows[0]["C_NRO_DOC"].ToString());
                }



                if (NombreMesa.Contains("DELIVERY"))
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
                        //ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + "c/ " + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                        //preZumo
                        ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + " " + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                    }

                    if (j == 35 || j == 70 || j == 105 || j == 140 || j == 175 || j == 205)
                    {
                        if (ticket.PrinterExists(ImpCaja))
                        {
                            ticket.PrintTicket(ImpCaja);
                            ticket = new Ticket();
                        }
                        else
                        {
                            globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                        }
                    }
                }
                //ticket.AddTotal("SUB TOTAL", dt.Rows[0]["PED_SUBTOTAL"].ToString());
                ticket.AddTotal("DESC.", dt.Rows[0]["PED_DESCUENTO"].ToString());
                ticket.AddTotal("", "---------");
                ticket.AddTotal("TOTAL", dt.Rows[0]["PED_TOTAL"].ToString());
                ticket.AddFooterLine("");
                ticket.AddTotal("", "---------");

                ticket.AddFooterLinePub("");
                ticket.AddFooterLinePub("  GRACIAS POR SU VISITA");
                ticket.AddFooterLinePub("");
                ticket.AddFooterLinePub("       Y SU PROPINA");
                ticket.AddFooterLine("Este documento no es comprobante de pago.");
                ticket.AddFooterLine("");
                ticket.AddFooterLine(globales.PublicidadPreCuenta());
                //ticket.AddFooterLine("   Sistema para restaurantes SOS-FOOD");
                //ticket.AddFooterLine("        Desarrollado por SOS-TIC");
                //ticket.AddFooterLine("   www.sos-food.com   www.sos-tic.com");
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
        private async void ImpCuentaDelivery2()
        {
            if (this.Pedido.Count == 0 && !this.NombreMesa.Contains("DELIVERY"))
            {
                var Dialog = new MessageDialog
                {
                    Mensaje = { Text = "Debe haber un pedido" }
                };
                var x2 = await DialogHost.Show(Dialog, "RootDialog");
            }
            else
            {
                DataTable impresora = new DataTable();
                string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
                impresora = negPlatos.GetImpresoraxDocumento(1); //Precuenta
                for (int i = 0; i < CountPrintDelivery; i++)
                {
                    DataTable dt = new DataTable();
                    dt = negDetVent.GetCuentaPedido((this.NombreMesa.Contains("DELIVERY") && this.Pedido.Count == 0) ? idPedido : this.Pedido.ToList().FirstOrDefault().DP_ID_PED);
                    Ticket ticket = new Ticket();
                    ticket.MaxCharDescrip = negParametros.margenMaxDescrip();
                    if (Convert.ToBoolean(negParametros.logoPreCuenta()) == true) { 
                        System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["EMPR_LOGO"]));
                        ticket.MargenLogo = negParametros.margenLogoTicket();
                        ticket.HeaderImage = x;
                    }
                    

                    ticket.AddSubHeaderLine("NRO. Orden: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                    ticket.AddSubHeaderLine("Atendido Por: " + dt.Rows[0]["EMPL_NOM"].ToString());
                    ticket.AddSubHeaderLine("Mesa: " + dt.Rows[0]["M_NOM"].ToString());
                    ticket.AddSubHeaderLine("Paga con: " + nombre_tipo_pago + " - S/" + monto);
                    ticket.AddSubHeaderLine("Vuelto: " + vuelto);
                    ticket.AddSubHeaderLine("");

                    ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["C_NOM"].ToString());
                    ticket.AddSubHeaderLine("Nro Documento: " + dt.Rows[0]["C_NRO_DOC"].ToString());
                    if (NombreMesa.Contains("DELIVERY"))
                    {
                        if (dt.Rows[0]["C_DISTR"].ToString() == "" && dt.Rows[0]["C_CALLE"].ToString() == "")
                        {
                            ticket.AddSubHeaderLine("Direccion: " + dt.Rows[0]["C_DIREC"].ToString());
                        }
                        else
                        {
                            ticket.AddSubHeaderLine("" + "");
                            ticket.AddSubHeaderLine("Distrito: " + dt.Rows[0]["C_DISTR"].ToString());
                            ticket.AddSubHeaderLine("" + "");
                            ticket.AddSubHeaderLine("Calle: " + dt.Rows[0]["C_CALLE"].ToString());
                            ticket.AddSubHeaderLine("" + "");
                            ticket.AddSubHeaderLine("Referencia: " + dt.Rows[0]["C_REF"].ToString());
                            ticket.AddSubHeaderLine("" + "");
                        }
                        ticket.AddSubHeaderLine("Telefono: " + dt.Rows[0]["C_TEL"].ToString());
                        ticket.AddSubHeaderLine("Motorizado: " + dt.Rows[0]["MOTORIZADO"].ToString());
                    }
                    ticket.AddSubHeaderLine("");
                    ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["DP_COMENTARIO"].ToString().Trim() == "")
                        {
                            ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                        }
                        else
                        {
                            //ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + "c/ " + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                            //preZumo ↓
                            ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + " " + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                        }
                        if (j == 35 || j == 70 || j == 105 || j == 140 || j == 175 || j == 205)
                        {
                            if (ticket.PrinterExists(ImpCaja))
                            {
                                ticket.PrintTicket(ImpCaja);
                                ticket = new Ticket();
                            }
                            else
                            {
                                globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                            }
                        }
                    }
                    ticket.AddTotal("DESC.", dt.Rows[0]["PED_DESCUENTO"].ToString());
                    ticket.AddTotal("", "---------");
                    ticket.AddTotal("TOTAL", dt.Rows[0]["PED_TOTAL"].ToString());
                    ticket.AddFooterLine("");
                    ticket.AddTotal("", "---------");

                    ticket.AddFooterLinePub("");
                    ticket.AddFooterLinePub("  GRACIAS POR SU VISITA");
                    ticket.AddFooterLinePub("");
                    ticket.AddFooterLinePub("       Y SU PROPINA");
                    ticket.AddFooterLine("Este documento no es comprobante de pago.");
                    ticket.AddFooterLine("");
                    ticket.AddFooterLine(globales.PublicidadPreCuenta());
                    //ticket.AddFooterLine("   Sistema para restaurantes SOS-FOOD");
                    //ticket.AddFooterLine("        Desarrollado por SOS-TIC");
                    //ticket.AddFooterLine("   www.sos-food.com   www.sos-tic.com");


                    if (ticket.PrinterExists(ImpCaja))
                    {
                        ticket.PrintTicket(ImpCaja);
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                    }

                    
                }
                Cancelar();
            }
        }
        private async void ImpCuentaDelivery()
        {
            if (this.Pedido.Count == 0 && !this.NombreMesa.Contains("DELIVERY"))
            {
                var Dialog = new MessageDialog
                {
                    Mensaje = { Text = "Debe haber un pedido" }
                };
                var x2 = await DialogHost.Show(Dialog, "RootDialog");
            }
            else
            {
                DataTable impresora = new DataTable();
                string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
                impresora = negPlatos.GetImpresoraxDocumento(1); //Precuenta
                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
                for (int i = 0; i < impresora.Rows.Count; i++)
                {
                    DataTable dt = new DataTable();
                    dt = negDetVent.GetCuentaPedido((this.NombreMesa.Contains("DELIVERY") && this.Pedido.Count == 0) ? idPedido : this.Pedido.ToList().FirstOrDefault().DP_ID_PED);
                    Ticket ticket = new Ticket();
                    if (Convert.ToBoolean(negParametros.logoPreCuenta()) == true) { 
                        System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["EMPR_LOGO"]));
                        ticket.MargenLogo = negParametros.margenLogoTicket();
                        ticket.HeaderImage = x;
                    }
                    ticket.MaxCharDescrip = negParametros.margenMaxDescrip();

                    //NORMAL
                    ticket.AddSubHeaderLineEnorme("NRO. Orden: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                    //ticket.AddSubHeaderLineEnorme("         " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                    ticket.AddSubHeaderLine("Mesa: " + dt.Rows[0]["M_NOM"].ToString());
                    ticket.AddSubHeaderLine("Atendido Por: " + dt.Rows[0]["EMPL_NOM"].ToString());
                    ticket.AddSubHeaderLine("" + "");
                    ticket.AddSubHeaderLine("" + "");
                    ticket.AddSubHeaderLine("Paga con: " + dt.Rows[0]["DENO_PAGO"].ToString() + " - S/" + dt.Rows[0]["PC_MONTO"].ToString());
                    ticket.AddSubHeaderLine("Vuelto: " + dt.Rows[0]["PC_VUELTO"].ToString());
                    ticket.AddSubHeaderLine("");
                    ticket.AddSubHeaderLine("");

                    ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["C_NOM"].ToString());
                    ticket.AddSubHeaderLine("" + "");
                    ticket.AddSubHeaderLine("Nro Documento:" + dt.Rows[0]["C_NRO_DOC"].ToString());
                    ticket.AddSubHeaderLine("" + "");
                    if (NombreMesa.Contains("DELIVERY"))
                    {
                        if (dt.Rows[0]["C_DISTR"].ToString() == "" && dt.Rows[0]["C_CALLE"].ToString() == "")
                        {
                            ticket.AddSubHeaderLine("Direccion: " + dt.Rows[0]["C_DIREC"].ToString());
                            ticket.AddSubHeaderLine("Telefono: " + dt.Rows[0]["C_TEL"].ToString());
                        }
                        else
                        {
                            ticket.AddSubHeaderLine("Distrito: " + dt.Rows[0]["C_DISTR"].ToString());
                            ticket.AddSubHeaderLine("" + "");
                            ticket.AddSubHeaderLine("Calle: " + dt.Rows[0]["C_CALLE"].ToString());
                            ticket.AddSubHeaderLine("" + "");
                            ticket.AddSubHeaderLine("Referencia: " + dt.Rows[0]["C_REF"].ToString());
                            ticket.AddSubHeaderLine("" + "");
                        }
                        //ticket.AddSubHeaderLineTelefono("" + "");
                        ticket.AddSubHeaderLine("Telefono: " + dt.Rows[0]["C_TEL"].ToString());
                        ticket.AddSubHeaderLine("Motorizado: " + dt.Rows[0]["MOTORIZADO"].ToString());
                    }
                    ticket.AddSubHeaderLine("" + "");
                    ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["DP_COMENTARIO"].ToString().Trim() == "")
                        {
                            ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                        }
                        else
                        {
                            //ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + "c/ " + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                            //preZumo
                            ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + " " + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                        }

                        if (j == 35 || j == 70 || j == 105 || j == 140 || j == 175 || j == 205)
                        {
                            if (ticket.PrinterExists(ImpCaja))
                            {
                                ticket.PrintTicket(ImpCaja);
                                ticket = new Ticket();
                            }
                            else
                            {
                                globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                            }
                        }
                    }
                    ticket.AddTotal("SUB TOTAL", dt.Rows[0]["PED_SUBTOTAL"].ToString());
                    ticket.AddTotal("DESC.", dt.Rows[0]["PED_DESCUENTO"].ToString());
                    ticket.AddTotal("", "---------");
                    ticket.AddTotal("TOTAL", dt.Rows[0]["PED_TOTAL"].ToString());
                    //ticket.AddFooterLine("")
                    ticket.AddTotal("", "---------"); ticket.AddFooterLine("");

                    ticket.AddFooterLinePub("");
                    ticket.AddFooterLinePub("  GRACIAS POR SU VISITA");
                    ticket.AddFooterLinePub("");
                    ticket.AddFooterLinePub("       Y SU PROPINA");
                    ticket.AddFooterLine("Este documento no es comprobante de pago.");
                    ticket.AddFooterLine("");
                    ticket.AddFooterLine(globales.PublicidadPreCuenta());
                    //ticket.AddFooterLine("   Sistema para restaurantes SOS-FOOD");
                    //ticket.AddFooterLine("        Desarrollado por SOS-TIC");
                    //ticket.AddFooterLine("   www.sos-food.com   www.sos-tic.com");

                    /*
                    ticket.AddSubHeaderLinePuertoEncantado("Nro. Orden:   " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                    ticket.AddSubHeaderLinePuertoEncantado("Mesa:         " + dt.Rows[0]["M_NOM"].ToString());
                    ticket.AddSubHeaderLinePuertoEncantado("Atendido Por: " + dt.Rows[0]["EMPL_NOM"].ToString());
                    ticket.AddSubHeaderLinePuertoEncantado("Paga con:     " + dt.Rows[0]["DENO_PAGO"].ToString() + " - S/" + dt.Rows[0]["PC_MONTO"].ToString());
                    ticket.AddSubHeaderLinePuertoEncantado("Vuelto:       " + dt.Rows[0]["PC_VUELTO"].ToString());
                    ticket.AddSubHeaderLinePuertoEncantado("");

                    if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                    {
                        ticket.AddSubHeaderLinePuertoEncantado("Cliente:      " + dt.Rows[0]["C_NOM"].ToString());
                        ticket.AddSubHeaderLinePuertoEncantado("Telefono:     " + dt.Rows[0]["telefcli"].ToString());
                    }
                    if (NombreMesa.Contains("DELIVERY"))
                    {
                        if (dt.Rows[0]["C_DISTR"].ToString() == "" && dt.Rows[0]["C_CALLE"].ToString() == "")
                        {
                            ticket.AddSubHeaderLinePuertoEncantado("Direccion:    " + dt.Rows[0]["C_DIREC"].ToString());
                            ticket.AddSubHeaderLinePuertoEncantado("");
                        }
                        else
                        {
                            ticket.AddSubHeaderLinePuertoEncantado("Distrito:     " + dt.Rows[0]["C_DISTR"].ToString());
                            ticket.AddSubHeaderLinePuertoEncantado("");
                            ticket.AddSubHeaderLinePuertoEncantado("Calle:        " + dt.Rows[0]["C_CALLE"].ToString());
                            ticket.AddSubHeaderLinePuertoEncantado("");
                            ticket.AddSubHeaderLinePuertoEncantado("Referencia:   " + dt.Rows[0]["C_REF"].ToString());
                            ticket.AddSubHeaderLinePuertoEncantado("");
                        }
                        ticket.AddSubHeaderLinePuertoEncantado("Telefono:     " + dt.Rows[0]["C_TEL"].ToString());
                    }
                    ticket.AddSubHeaderLinePuertoEncantado("Fecha/Hora:   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["DP_COMENTARIO"].ToString().Trim() == "")
                        {
                            ticket.AddItemPuertoEncantado("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                        }
                        else
                        {
                            ticket.AddItemPuertoEncantado("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + "c/ " + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                        }
                        if (j == 35 || j == 70 || j == 105 || j == 140 || j == 175 || j == 205)
                        {
                            if (ticket.PrinterExists(ImpCaja))
                            {
                                ticket.PrintTicket(ImpCaja);
                                ticket = new Ticket();
                            }
                            else
                            {
                                globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                            }
                        }
                    }
                    //FIN PUERTO ENCANTADO



                    //ticket.AddTotal("SUB TOTAL", dt.Rows[0]["PED_SUBTOTAL"].ToString());
                    ticket.AddTotalPuertoEncantado("DESC.", dt.Rows[0]["PED_DESCUENTO"].ToString());
                    ticket.AddTotalPuertoEncantado("", "---------");
                    ticket.AddTotalPuertoEncantado("TOTAL", dt.Rows[0]["PED_TOTAL"].ToString());
                    ticket.AddFooterLine("");


                    ticket.AddFooterLine("");
                    ticket.AddFooterLine("Este documento no es comprobante de pago.");
                    ticket.AddFooter2LineAnulacion("DELIVERY: (01)2461155");
                    */
                    if (ticket.PrinterExists(ImpCaja))
                    {
                        ticket.PrintTicket(ImpCaja);
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                    }
                }
                Cancelar();
            }
        }
        private async void DividirCuentas()
        {
            try
            {
                if (this.Pedido.Count == 0)
                {
                    var Dialog = new MessageDialog
                    {
                        Mensaje = { Text = "Debe haber un pedido" }
                    };
                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                }
                else
                {
                    Application.Current.Properties["id_cuen_pedido"] = this.Pedido.ToList().FirstOrDefault().DP_ID_PED;
                    var opendividirCuentas = new DividirCuentas { };
                    var x = await DialogHost.Show(opendividirCuentas, "RootDialog");
                    if (Application.Current.Properties["EstPagoCuenta"] != null)
                    {
                        if (Application.Current.Properties["EstPagoCuenta"].ToString().Trim() == "PAGADO")
                        {
                            Cancelar();
                        }
                        if (Application.Current.Properties["EstPagoCuenta"].ToString().Trim() == "PENDIENTE")
                        {
                            negPedido.ActualizarMesaLibre(Convert.ToInt32(this.CodMesa));
                            RegistrarPedido(this.CodMesa);
                        }
                    }
                }
            }
            catch (Exception ex) {}
        }
        private async void ImpCuenta()
        {
            try
            {
                if (this.Pedido.Count == 0)
                {
                    var Dialog = new MessageDialog
                    {
                        Mensaje = { Text = "Debe haber un pedido" }
                    };
                    var x2 = await DialogHost.Show(Dialog, "RootDialog");
                }
                else
                {
                    if (this.NombreMesa.Contains("DELIVERY"))
                    {
                        ImpCuentaDelivery();
                        return;
                    }

                    DataTable impresora = new DataTable();
                    string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
                    string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
                    impresora = negPlatos.GetImpresoraxDocumento(1); //Precuenta
                    for (int i = 0; i < impresora.Rows.Count; i++)
                    {
                        DataTable dt = new DataTable();
                        dt = negDetVent.GetCuentaPedido((this.NombreMesa.Contains("DELIVERY")) ? idPedido : this.Pedido.ToList().FirstOrDefault().DP_ID_PED);
                        Ticket ticket = new Ticket();
                        if (Convert.ToBoolean(negParametros.logoPreCuenta()) == true) {
                            System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["EMPR_LOGO"]));
                            ticket.MargenLogo = negParametros.margenLogoTicket();
                            ticket.HeaderImage = x;
                        }
                        

                        ticket.AddSubHeaderLine("NRO. Orden: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                        ticket.AddSubHeaderLine("Atendido Por: " + dt.Rows[0]["EMPL_NOM"].ToString());
                        ticket.AddSubHeaderLine("Mesa: " + dt.Rows[0]["M_NOM"].ToString());
                        ticket.AddSubHeaderLine("");

                        if (this.NombreMesa.Contains("LLEVAR") || this.NombreMesa.Contains("RECOJO"))
                        {
                            ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["nomllevar"].ToString());
                            ticket.AddSubHeaderLine("Telefono : " + dt.Rows[0]["telefcli"].ToString());
                        }
                        else
                        {
                            ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["nomllevar"].ToString());
                            ticket.AddSubHeaderLine("Telefono : " + dt.Rows[0]["telefcli"].ToString());
                        }

                        if (NombreMesa.Contains("DELIVERY"))
                        {
                            ticket.AddSubHeaderLine("Direccion: " + dt.Rows[0]["C_DIREC"].ToString());
                            ticket.AddSubHeaderLine("Telefono: " + dt.Rows[0]["C_TEL"].ToString());
                            ticket.AddSubHeaderLine("Motorizado: " + dt.Rows[0]["MOTORIZADO"].ToString());
                        }

                        ticket.AddSubHeaderLine("");
                        ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[j]["DP_COMENTARIO"].ToString().Trim() == "")
                            {
                                ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                            }
                            else
                            {
                                ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + ' ' + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                            }
                            if (j == 35 || j == 70 || j == 105 || j == 140 || j == 175 || j == 205)
                            {
                                if (ticket.PrinterExists(ImpCaja))
                                {
                                    ticket.PrintTicket(ImpCaja);
                                    ticket = new Ticket();
                                }
                                else
                                {
                                    globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                                }
                            }
                        }
                        ticket.AddTotal("SUB TOTAL", dt.Rows[0]["PED_SUBTOTAL"].ToString());
                        ticket.AddTotal("DESC.", dt.Rows[0]["PED_DESCUENTO"].ToString());
                        ticket.AddTotal("", "---------");
                        ticket.AddTotal("TOTAL", dt.Rows[0]["PED_TOTAL"].ToString());
                        //ticket.AddFooterLine("");
                        ticket.AddTotal("", "---------"); ticket.AddFooterLine("");

                        ticket.AddFooterLinePub("");
                        ticket.AddFooterLinePub("  GRACIAS POR SU VISITA");
                        ticket.AddFooterLinePub("");
                        ticket.AddFooterLinePub("       Y SU PROPINA");
                        ticket.AddFooterLine("Este documento no es comprobante de pago.");
                        ticket.AddFooterLine("");
                        ticket.AddFooterLine(globales.PublicidadPreCuenta());
                        //ticket.AddFooterLine("        Desarrollado por SOS-TIC");
                        //ticket.AddFooterLine("   www.sos-food.com   www.sos-tic.com");

                        if (ticket.PrinterExists(ImpCaja))
                        {
                            ticket.PrintTicket(ImpCaja);
                        }
                        else
                        {
                            globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                        }
                    }
                    Cancelar();
                }
            }
            catch (Exception ex)
            {
                //globales.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
        }
        public ObservableCollection<Pedidos> Pedido2 { get; set; }
        private async void AbrirDailogPagar()
        {
            if (this.Pedido.Count == 0)
            {
                var SiNo = new MessageDialog
                {
                    Mensaje = { Text = "Debe haber pedidos para pedido" }
                };
                var x = await DialogHost.Show(SiNo, "RootDialog");
            }
            else
            {
                this.Pedido2 = negPedido.GetPedidoxIdTotal(this.Pedido.ToList().FirstOrDefault().DP_ID_PED);
                string SubTotalPedido = "TOTAL : S/. " + this.Pedido2.ToList().FirstOrDefault().PED_TOTAL.ToString();
                Application.Current.Properties["EstadoPago"] = "PAGADO";
                Application.Current.Properties["OperPagoCel"] = "PAGAR";
                Application.Current.Properties["id_pedido"] = this.Pedido2.ToList().FirstOrDefault().DP_ID_PED;
                Application.Current.Properties["Totals"] = Convert.ToDecimal(SubTotalPedido.Replace("TOTAL : S/.", ""));
                Application.Current.Properties["Usuario"] = Application.Current.Properties["IdUsuario"];
                Application.Current.Properties["Pedido"] = this.Pedido2.ToList().FirstOrDefault().DP_ID_PED;
                Application.Current.Properties["EstadoF"] = "PENDIENTE";
                Application.Current.Properties["NomCliente"] = this.NomCliente;
                DataTable dt = new DataTable();
                dt = negPedido.sp_verificar_pedido_cuenta(this.Pedido.ToList().FirstOrDefault().DP_ID_PED);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Application.Current.Properties["PedidoCuenta"] = "SI";
                    }
                    else
                    {
                        Application.Current.Properties["PedidoCuenta"] = "NO";
                    }
                }
                else
                {
                    Application.Current.Properties["PedidoCuenta"] = "NO";
                }

                var openpago = new DialogPago { };
                var x = await DialogHost.Show(openpago, "RootDialog");
                if (Application.Current.Properties["EstPago"] != null)
                {
                    Cancelar();
                }
            }
        }
        private void timer_elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            directoryStructure = new AmbientesStructure();
            if (Application.Current.Properties["IdAmbiente"] == null)
            {
                int idAmbiente = DataAmbientes.FirstOrDefault().ID;
                Application.Current.Properties["IdAmbiente"] = idAmbiente.ToString();
            }
            GetMesas(Application.Current.Properties["IdAmbiente"].ToString());
            //ConsultarDeliveryWebService();
            this.DataPrioridades = negPedido.GetPrioridades();
        }
        public ObservableCollection<Empresa> DataEmpresa { get; set; }
        Neg_DeliveryWebService negDeliveryWebService = new Neg_DeliveryWebService();
        public ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService> DateCartaDelivery { get; set; }
        public ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService> DateCartaDeliveryDB { get; set; }
        public void ConsultarDeliveryWebService()
        {
            DataEmpresa = negEmpresa.GetEmpresa();
            DateCartaDeliveryDB = new ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
            DateCartaDelivery = new ObservableCollection<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
            string codigo_cliente = DataEmpresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo;
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    //values["token"] = "app963";
                    values["id_business"] = "7";

                    var response = client.UploadValues("https://80002.sos-delivery.com/web_service/api/v1/sosfood/pedidos.php", values);
                    var responseString = Encoding.Default.GetString(response);
                    var WebService = new List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>();
                    //responseString = responseString.Replace("}", "},");
                    WebService = JsonConvert.DeserializeObject<List<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>>(responseString);

                    //hola = JsonConvert.SerializeObject<Capa_Entidades.Models.Delivery_Web_Service.DeliveryWebService>(WebService.orders);
                    this.DateCartaDeliveryDB = negDeliveryWebService.GetDeliveryWebService();
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

                //globales.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
        }
        public string hora { get; set; }
        public string fecha { get; set; }
        private void timer_tiempo(object sender, System.Timers.ElapsedEventArgs e)
        {
            hora = DateTime.Now.ToString("hh:mm:ss tt");
            fecha = DateTime.Now.ToShortDateString();
        }
    }
}
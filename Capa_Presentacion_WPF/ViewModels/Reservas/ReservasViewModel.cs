using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Reservas;
using Capa_Negocio.Carta;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Reservas;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Carta.ItemsViewModel;
using Capa_Presentacion_WPF.Views.Dialogs;
using Capa_Presentacion_WPF.Views.Dialogs.Pedidos;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using static Capa_Presentacion_WPF.ViewModels.Ambientes.MesasViewModel;

namespace Capa_Presentacion_WPF.ViewModels.Reservas
{
    public class ReservasViewModel : IGeneric
    {
        ReservasStructure rs = new ReservasStructure();
        #region Negocio
        Neg_Reservas neg_re = new Neg_Reservas();
        Neg_Grupo negsGrupo = new Neg_Grupo();
        Funcion_Global globales = new Funcion_Global();
        Neg_Categoria_carta negsCatgCart = new Neg_Categoria_carta();
        Neg_Platos negPlatos = new Neg_Platos();
        #endregion
        #region Entidad
        private Cliente _Cliente;
        public Cliente Cliente
        {
            get => _Cliente;
            set {
                _Cliente = value;
            }
        }
        private MesasItem _MesasItem;
        public MesasItem MesasItem{get => _MesasItem;set { _MesasItem = value; }}

        private AmbientesItem _AmbientesItem;
        public AmbientesItem AmbientesItem { get => _AmbientesItem; set { _AmbientesItem = value; } }

        private Ent_Reserva _Ent_Reserva;
        public Ent_Reserva Ent_Reserva { get => _Ent_Reserva; set { _Ent_Reserva = value; } }
        #endregion
        #region Objetos
        public string VisibleAgregarProd { get; set; }
        private string _operacion;
        public string Operacion
        {
            get => _operacion;
            set 
            {
                if (value == "Lista")
                {
                    GetLista();
                }
                else if (value == "Nuevo")
                {
                    LlamarDialog();
                }
                _operacion = value;
            }
        }
        public string NomCliente { get; set; }
        public string IdCliente { get; set; }
        public string NroCliente { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public DateTime HoraStart { get; set; }
        public DateTime HoraFinish { get; set; }
        public decimal Amortizar { get; set; } = 0;
        public bool openDialog { get; set; }
        public string isGrupoCarta { get; set; }
        public string total { get; set; }
        public decimal total_numero { get; set; }
        public string CodGrupo { get; set; }
        public string isCategoriaCarta { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime FinishDate { get; set; } = DateTime.Now;

        #endregion
        #region Listas
        public List<Ent_Reserva> DataReservas { get; set; }
        public List<Ent_Reserva> DataTipoReserva { get; set; }
        public List<AmbientesItem> DataAmbiente { get; set; }
        public List<MesasItem> DataMesa { get; set; }
        public ICollectionView ContractsListCollectionView { get; private set; }
        public ObservableCollection<CategoriaCartaItemViewModel> DatasCategoriacarta { get; set; }
        public ObservableCollection<GrupoCartaItemViewModel> DatasGrupo { get; set; }
        public ObservableCollection<Platos> DataPlatos { get; set; }
        public Platos SelectedDataFile { get; set; }
        public List<Platos> PlatosList;
        public List<Platos> PlatosList2; 
        public ObservableCollection<Platos> DataPedido { get; set; }
        public ObservableCollection<Platos> DataPedidoTemp { get; set; }
        #endregion
        #region SeleccionCombos

        private AmbientesItem _AmbienteSelected;
        public AmbientesItem AmbienteSelected
        {
            get => _AmbienteSelected;
            set
            {
                if (value != null)
                {
                    this.DataMesa = rs.GetMesas(((AmbientesItem)value).ID);
                    this.AmbientesItem.ID = ((AmbientesItem)value).ID;
                }
                _AmbienteSelected = value;
            }
        }

        private MesasItem _MesaSelected;
        public MesasItem MesaSelected
        {
            get => _MesaSelected;
            set 
            {
                if (value != null)
                {
                    this.MesasItem.ID = ((MesasItem)value).ID;
                }
                _MesaSelected = value;
            }
        }
        private Ent_Reserva _TipoReservaSelected;
        public Ent_Reserva TipoReservaSelected
        {
            get => _TipoReservaSelected;
            set
            {
                if (value != null)
                {
                    Ent_Reserva.TR_ID = ((Ent_Reserva)value).TR_ID;
                }
                _TipoReservaSelected = value;
            }
        }
        #endregion
        #region Commands
        public ICommand CambiarClienteCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand AgregarProductosCommand { get; set; }
        public ICommand CerrarDialogCommand { get; set; }
        public ICommand EntrarGrupoCommand { get; set; }
        public ICommand VolverCategoriaCommand { get; set; }
        public ICommand CargaPlatos { get; set; }
        public ICommand EliminarPlato { get; set; }
        public ICommand BuscarCommand { get; set; }
        private ICommand m_RowClickCommand;
        public ICommand RowClickCommand
        {
            get
            {
                return m_RowClickCommand;
            }
        }
        #endregion
        public ReservasViewModel()
        {
            this.CambiarClienteCommand = new RelayCommand(new Action(CambiarCliente));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.AgregarProductosCommand = new RelayCommand(new Action(AgregarProductos));
            this.CerrarDialogCommand = new RelayCommand(new Action(CerrarDialog));
            this.EntrarGrupoCommand = new ParamCommand(new Action<object>(CargarGrupo));
            this.VolverCategoriaCommand = new RelayCommand(new Action(VolverCategoria));
            this.CargaPlatos = new ParamCommand(new Action<object>(CargarCarta));
            this.DataPedido = new ObservableCollection<Platos>();
            this.EliminarPlato = new ParamCommand(new Action<object>(EliminarPlatos));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = this.SelectedDataFile;
                if (set != null)
                {
                    PasarPlatos(set.idplato);
                }
            });
            this.Ent_Reserva = new Ent_Reserva();
            this.MesasItem = new MesasItem();
            this.AmbientesItem = new AmbientesItem();
            this.Cliente = new Cliente();
            TipoReservaSelected = new Ent_Reserva();
            MesaSelected = new MesasItem();
            AmbienteSelected = new AmbientesItem();
            DataPlatos = new ObservableCollection<Platos>();
            this.Operacion = "Lista";
        }
        private void Buscar() {
            DataReservas = rs.GetReservas(StartDate, FinishDate);
        }
        private void Editar(object id)
        {
            this.VisibleAgregarProd = "Collapsed";
            int _id = Convert.ToInt32(id);
            Ent_Reserva.ID = _id;
            Ent_Reserva.TR_ID = DataReservas.Where(w => w.ID == _id).First().TR_ID;
            Ent_Reserva = DataReservas.Where(w => w.ID == _id).FirstOrDefault();
            
            this.IdCliente = DataReservas.Where(w => w.ID == _id).First().R_ID_CLIENTE.ToString();
            this.NomCliente = DataReservas.Where(w => w.ID == _id).First().C_NOMINA;
            this.Fecha = DataReservas.Where(w => w.ID == _id).First().R_F_RESERVA_DESDE;
            this.HoraStart = DataReservas.Where(w => w.ID == _id).First().R_F_RESERVA_DESDE;
            this.HoraFinish = DataReservas.Where(w => w.ID == _id).First().R_F_RESERVA_HASTA;
            this.Amortizar = DataReservas.Where(w => w.ID == _id).First().R_AMORTIZADO;
            
            AmbientesItem.ID = DataReservas.Where(w => w.ID == _id).First().R_ID_AMBIENTE;
            MesasItem.ID = DataReservas.Where(w => w.ID == _id).First().R_ID_MESA;
            List<Platos> _p = new List<Platos>();
            _p = rs.GetDetalleReserva(_id);
            ObservableCollection<Platos> p = new ObservableCollection<Platos>(_p);
            this.DataPedido = p;
            this.Operacion = "Editar";
        }
        private async void Eliminar(object id)
        {
            var view = new SiNoMessageDialog
            {
                Mensaje = { Text = "¿Desea eliminar el registro?" }
            };
            var x = await DialogHost.Show(view, "RootDialog");
            if ((bool)x)
            {
                int _id = Convert.ToInt32(id);
                Ent_Reserva.ID = _id;
                DataTable dt = new DataTable();
                dt.Columns.Add("DR_ID_CARTA");
                dt.Columns.Add("DR_CANT");
                DataRow row = dt.NewRow();
                row["DR_ID_CARTA"] = "0";
                row["DR_CANT"] = "0";
                dt.Rows.Add(row);
                bool res = neg_re.SetReserva(3, Cliente, Ent_Reserva, MesasItem, DateTime.Now, DateTime.Now, 0,dt);
                if (res)
                {
                    GetLista();
                }
            }
        }
        private void Cancelar()
        {
            this.Operacion = "Lista";
            this.Ent_Reserva = new Ent_Reserva();
            this.MesasItem = new MesasItem();
            this.AmbientesItem = new AmbientesItem();
            this.Cliente = new Cliente();
            this.Fecha = DateTime.Now;
            this.HoraFinish = DateTime.Now;
            this.HoraStart = DateTime.Now;
            DataPlatos = new ObservableCollection<Platos>();
            TipoReservaSelected = new Ent_Reserva();
            AmbienteSelected = new AmbientesItem();
            MesaSelected = new MesasItem();
            IdCliente = "";
            NomCliente = "";
            Amortizar = 0;
        }
        private void Nuevo()
        {
            IdCliente = "";
            NomCliente = "";
            Amortizar = 0;
            this.Ent_Reserva = new Ent_Reserva();
            this.MesasItem = new MesasItem();
            this.AmbientesItem = new AmbientesItem();
            TipoReservaSelected = new Ent_Reserva();
            AmbienteSelected = new AmbientesItem();
            DataPlatos = new ObservableCollection<Platos>();
            MesaSelected = new MesasItem();
            this.Cliente = new Cliente();
            this.Fecha = DateTime.Now;
            this.HoraFinish = DateTime.Now;
            this.HoraStart = DateTime.Now;
            this.DataPedido = new ObservableCollection<Platos>();
            this.VisibleAgregarProd = "Visible";
            this.Operacion = "Nuevo";
        }
        private void Guardar()
        {
            int op = 0;
            int idUsuario = 0;
            Cliente.idcli = Convert.ToInt32(IdCliente);
            Ent_Reserva.R_AMORTIZADO = Amortizar;
            idUsuario = Convert.ToInt32(System.Windows.Application.Current.Properties["IdUsuario"]);
            DateTime FechaDesde = Convert.ToDateTime(Fecha.ToShortDateString() + " " + HoraStart.TimeOfDay);
            DateTime FechaHasta = Convert.ToDateTime(Fecha.ToShortDateString() + " " + HoraFinish.TimeOfDay);
            int c = 0;

            if (Fecha.Date < DateTime.Now.Date) {
                globales.Mensaje("SOS-FOOD Mensaje:", "La fecha no puede ser anterior a hoy", 2);
                return;
            }

            if (FechaHasta < FechaDesde) {
                globales.Mensaje("SOS-FOOD Mensaje:", "La hora final no puede ser menor a la hora de inicio de la reserva", 2);
                return;
            }

            if(Ent_Reserva.TR_ID == 0)
            {
                globales.Mensaje("SOS-FOOD Mensaje:", "Debe seleccionar un tipo de reserva", 2);
                return;
            }

            if (AmbientesItem.ID == 0)
            {
                globales.Mensaje("SOS-FOOD Mensaje:", "Debe seleccionar un ambiente", 2);
                return;
            }

            if (MesasItem.ID == 0)
            {
                globales.Mensaje("SOS-FOOD Mensaje:", "Debe seleccionar una mesa", 2);
                return;
            }

            c = DataReservas.Where(w => w.R_ID_MESA == MesasItem.ID && ((FechaDesde >= w.R_F_RESERVA_DESDE && FechaDesde <= w.R_F_RESERVA_HASTA && w.R_ID_ESTADO == 1) ||
                    (FechaHasta >= w.R_F_RESERVA_DESDE && FechaHasta <= w.R_F_RESERVA_HASTA && w.R_ID_ESTADO == 1))).Count();
            if (c > 0)
            {
                var x = new MessageDialog
                {
                    Mensaje = { Text = "Ya existe una reserva para esta mesa en el rango de hora que a colocado" }
                };
                DialogHost.Show(x, "RootDialog");
                return;
            }
            if (this.Operacion == "Nuevo"){op = 1;}
            else if(this.Operacion == "Editar") { op = 2;}
            //Detalle de platos
            DataTable dt = new DataTable();
            dt.Columns.Add("DR_ID_CARTA");
            dt.Columns.Add("DR_CANT");
            foreach (var i in DataPedido) {
                DataRow row = dt.NewRow();
                row["DR_ID_CARTA"] = i.idplato;
                row["DR_CANT"] = i.cantidad;
                dt.Rows.Add(row);
            }

            bool res = neg_re.SetReserva(op,Cliente,Ent_Reserva,MesasItem, FechaDesde, FechaHasta, idUsuario,dt);
            if (res)
            {
                var x = new MessageDialog
                {
                    Mensaje = { Text = "La reserva se ha guardado exitosamente" }
                };
                DialogHost.Show(x, "RootDialog");
            }
            this.DataPlatos = new ObservableCollection<Platos>();
            this.Operacion = "Lista";
        }
        private void CambiarCliente()
        {
            LlamarDialog();
        }
        private async void LlamarDialog()
        {
            var delivery = new DialogCliente{};
            var x = await DialogHost.Show(delivery, "RootDialog");
            if (Application.Current.Properties["ClienteDelivery"] == null){NomCliente = ""; Operacion = "Lista"; return;}
            else
            {
                NomCliente = Application.Current.Properties["ClienteDelivery"].ToString();
            }
            if (Application.Current.Properties["IdClienteDelivery"] == null){IdCliente = "0"; Operacion = "Lista"; return; }
            else
            {
                IdCliente = Application.Current.Properties["IdClienteDelivery"].ToString();
            }
            if (Application.Current.Properties["NroClienteDelivery"] == null) { NroCliente = "0"; Operacion = "Lista"; return; }
            else
            {
                NroCliente = Application.Current.Properties["NroClienteDelivery"].ToString();
            }
            
            Cliente.idcli = Convert.ToInt32(IdCliente);
        }
        #region Agregar Productos
        private async void PasarPlatos(object id)
        {
            this.PlatosList = new List<Platos>();
            string cantidad = "1";
            Platos pla = new Platos();
            Platos pla2 = new Platos();
            int orden = 0;
            if (this.DataPedido.Where(w => w.idplato == (int)id).FirstOrDefault() != null)
            {
                var asd2 = this.DataPedido.Where(p => p.idplato == (int)id).FirstOrDefault();
                PlatosList.Add(asd2);
                pla = PlatosList.ToList().FirstOrDefault();
                if (this.DataPedido.Where(w => w.idplato == (int)id).FirstOrDefault() == null)
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
                    int cant = 0;
                    cant = Convert.ToInt32(asd2.cantidad);
                    double imp = 0.00;
                    imp = Convert.ToDouble(asd2.precplato);
                    this.DataPedido.Remove(this.DataPedido.Where(w => w.idplato == (int)id).FirstOrDefault());
                    pla.cantidad = (cant + 1).ToString();
                    pla.importe = (imp * Convert.ToDouble(pla.cantidad)).ToString();
                    //pla.orden = this.DataPedido.Count();
                    this.DataPedido.Add(pla);
                    this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                    this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
                }
            }
            else
            {
                var asd = this.DataPlatos.Where(p => p.idplato == (int)id).FirstOrDefault();
                if (asd.cantidad is null)
                {
                    asd.cantidad = cantidad;
                    asd.importe = asd.precplato.ToString();
                }
                PlatosList.Add(asd);
                pla = PlatosList.ToList().FirstOrDefault();
                if (this.DataPedido.Where(w => w.idplato == (int)id).FirstOrDefault() == null)
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
                    orden = this.DataPedido.Count();
                    orden += 1;
                    pla.orden = orden;
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
                    int cant = 0;
                    cant = Convert.ToInt32(asd.cantidad);
                    double imp = 0.00;
                    imp = Convert.ToDouble(asd.importe);
                    this.DataPedido.Remove(this.DataPedido.Where(w => w.idplato == (int)id).FirstOrDefault());
                    pla.cantidad = (cant + 1).ToString();
                    pla.importe = (imp * Convert.ToDouble(pla.cantidad)).ToString();
                    this.DataPedido.Add(pla);
                    this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
                    this.DataPedido = new ObservableCollection<Platos>(DataPedido.OrderBy(i => i.orden));
                }
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
                this.total = "TOTAL : S/. " + this.DataPedido.Sum(w => Convert.ToDouble(w.importe)).ToString();
            }
        }

        private void CargarCarta(object id)
        {
            DataPlatos = negPlatos.GetPlatosxGrupo(int.Parse(id.ToString()));
            this.CodGrupo = id.ToString();
        }
        private void VolverCategoria()
        {
            this.isGrupoCarta = "Hidden";
            this.isCategoriaCarta = "Visible";
        }
        private void AgregarProductos()
        {
            this.openDialog = true;
        }
        private void CerrarDialog()
        {
            this.openDialog = false;
        }
        private void CargarGrupo(object id)
        {
            var grupocarta = negsGrupo.GetGupoxCategoria(int.Parse(id.ToString()));
            this.DatasGrupo = new ObservableCollection<GrupoCartaItemViewModel>(
                grupocarta.Select(g => new GrupoCartaItemViewModel(g.idgrup, g.idscat, g.nomscat, g.idcat, g.nomcat, g.nomgrup, g.imagengrup, Convert.ToDecimal(g.descgrup))));
            if (grupocarta.Count != 0)
            {
                if (grupocarta.Count == 1)
                {
                    DataPlatos = negPlatos.GetPlatosxGrupo(int.Parse(grupocarta.ToList().FirstOrDefault().idgrup.ToString()));
                    this.CodGrupo = grupocarta.ToList().FirstOrDefault().idgrup.ToString();
                    this.isGrupoCarta = "Hidden";
                }
                else
                {
                    this.isGrupoCarta = "Visible";
                    this.isCategoriaCarta = "Hidden";
                }
            }
        }
        #endregion
        private void GetLista()
        {
            this.DataAmbiente = rs.GetAmbientes();
            this.DataMesa = rs.GetMesas();
            this.DataTipoReserva = rs.GetTipoReservas();
            this.DataReservas = rs.GetReservas(DateTime.Now,DateTime.Now);
            //DataReservas = DataReservas.Where(w => w.R_F_RESERVA_DESDE.Date >= DateTime.Now.Date && w.R_F_RESERVA_HASTA.Date <= DateTime.Now.Date).ToList();

            var categoriacarta = negsCatgCart.GetCategoria();
            this.DatasCategoriacarta = new ObservableCollection<CategoriaCartaItemViewModel>(
                categoriacarta.Select(ct => new CategoriaCartaItemViewModel(ct.idcat, ct.idscat.ToString(), ct.nomscat, ct.nomcat, Convert.ToDecimal(ct.desccat), ct.imagencat, ct.columna)));
            ListCollectionView lcv = new ListCollectionView(DatasCategoriacarta);
            lcv.GroupDescriptions.Add(new PropertyGroupDescription("NOMSCAT"));
            ContractsListCollectionView = (ListCollectionView)CollectionViewSource.GetDefaultView(lcv);
        }
    }
}

using Capa_Entidades;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Funciones_Globales;
using Capa_Presentacion_WPF.Commands;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using MVVMListViewPagination.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogClienteViewModel : INotifyPropertyChanged
    {
        //#region INotifyPropertyChanged
        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        //#endregion
        #region NoitfyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Cliente> DataCliente { get; set; }
        Neg_Cliente negCli = new Neg_Cliente();
        Neg_Combo negCombo = new Neg_Combo();
        public bool EnabledBuscarDoc { get; set; } = false;
        private Ent_Combo _ComboTipoDocSelected;
        public Ent_Combo ComboTipoDocSelected
        {
            get => _ComboTipoDocSelected;
            set
            {
                if (value != null)
                {
                    if (Convert.ToInt32(((Ent_Combo)value).id) == 1 || Convert.ToInt32(((Ent_Combo)value).id) == 2)
                    {
                        EnabledBuscarDoc = true;
                    }
                    else
                    {
                        EnabledBuscarDoc = false;
                    }
                }
                _ComboTipoDocSelected = value;
            }
        }
        public ICommand Aceptar { get; set; }
        public ICommand BuscarCliente { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand CerrarDialog { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand SunatCommand { get; set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand FirstCommand { get; private set; }
        public ICommand LastCommand { get; private set; }
        public List<Ent_Combo> ComboTipoDoc { get; set; }
        public string Criterio { get; set; }
        public string Lista { get; set; }
        public string TituloCliente { get; set; }
        public int TipoOperacionCliente { get; set; }
        public string Grabar { get; set; }
        private Cliente cliente;
        private string denominac;
        private string direccion;
        private string distrito;
        private string calle;
        private string referencia;
        public string NombreBoton { get; set; }
        public Cliente Cliente
        {
            get => cliente;
            set
            {
                cliente = value;
            }
        }
        public string Denominac
        {
            get
            {
                return this.cliente == null ? "" : this.cliente.denominacion;
            }
            set
            {
                if (value != null)
                {
                    this.cliente.denominacion = value;
                }
                denominac = value;
            }
        }
        public string Direccion
        {
            get
            {
                return this.cliente == null ? "" : this.cliente.dircli;
            }
            set
            {
                if (value != null)
                {
                    this.cliente.dircli = value;
                }
                direccion = value;
            }
        }
        public string Distrito
        {
            get
            {
                return this.cliente == null ? "" : this.cliente.distritocli;
            }
            set
            {
                if (value != null)
                {
                    this.cliente.distritocli = value;
                }
                distrito = value;
            }
        }
        public string Calle
        {
            get
            {
                return this.cliente == null ? "" : this.cliente.callecli;
            }
            set
            {
                if (value != null)
                {
                    this.cliente.callecli = value;
                }
                calle = value;
            }
        }
        public string Referencia
        {
            get
            {
                return this.cliente == null ? "" : this.cliente.referenciacli;
            }
            set
            {
                if (value != null)
                {
                    this.cliente.referenciacli = value;
                }
                referencia = value;
            }
        }
        private ICommand m_RowClickCommand;
        public ICommand RowClickCommand
        {
            get
            {
                return m_RowClickCommand;
            }
        }
        private ICommand m_RowDoubleClickCommand;
        public ICommand RowDoubleClickCommand
        {
            get
            {
                return m_RowDoubleClickCommand;
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
        public class DelegateCommandDoubleClick : ICommand
        {
            private Action m_Action;
            public DelegateCommandDoubleClick(Action action)
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
        #region Fields And Properties
        private int itemPerPage = 15;
        private int itemcount;
        private int _currentPageIndex;
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set
            {
                _currentPageIndex = value;
                OnPropertyChanged("CurrentPage");
            }
        }
        public int CurrentPage
        {
            get { return _currentPageIndex + 1; }
        }

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            private set
            {
                _totalPages = value;
                OnPropertyChanged("TotalPage");
            }
        }

        public CollectionViewSource ViewList { get; set; }
        private ObservableCollection<Cliente> clienteList = new ObservableCollection<Cliente>();
        public ReadOnlyObservableCollection<Cliente> ClienteList
        {
            get { return new ReadOnlyObservableCollection<Cliente>(clienteList); }
        }
        #endregion
        #region Pagination Methods
        public void ShowNextPage()
        {
            try
            {
                CurrentPageIndex++;
                ViewList.View.Refresh();
            }
            catch (Exception ex)
            {

            }
            
        }

        public void ShowPreviousPage()
        {
            try
            {
                CurrentPageIndex--;
                ViewList.View.Refresh();
            }
            catch (Exception ex)
            {

            }
            
        }

        public void ShowFirstPage()
        {
            try
            {
                CurrentPageIndex = 0;
                ViewList.View.Refresh();
            }
            catch (Exception ex)
            {

            }
        }

        public void ShowLastPage()
        {
            try
            {
                CurrentPageIndex = TotalPages - 1;
                ViewList.View.Refresh();
            }
            catch (Exception)
            {

            } 
        }

        void view_Filter(object sender, FilterEventArgs e)
        {
            int index = ((Cliente)e.Item).contador - 1;
            if (index >= itemPerPage * CurrentPageIndex && index < itemPerPage * (CurrentPageIndex + 1))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void CalculateTotalPages()
        {
            if (itemcount % itemPerPage == 0)
            {
                TotalPages = (itemcount / itemPerPage);
            }
            else
            {
                TotalPages = (itemcount / itemPerPage) + 1;
            }
        }
        #endregion
        public Cliente SelectedDataFile { get; set; }
        public DialogClienteViewModel()
        {
            TituloCliente = "Agregar Cliente";
            Application.Current.Properties["ClienteDelivery"] = null;
            Application.Current.Properties["IdClienteDelivery"] = null;
            Application.Current.Properties["NroClienteDelivery"] = null;
            //DataCliente = negCli.GetCliente();
            Criterio = "";
            this.Aceptar = new RelayCommand(new Action(Seleccionar));
            this.BuscarCliente = new ParamCommand(new Action<object>(BuscCliente));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.SunatCommand = new ParamCommand(new Action<object>(ServicioSunat));
            this.CerrarDialog = new RelayCommand(new Action(CancelarDialog));
            NextCommand = new NextPageCommand(this);
            PreviousCommand = new PreviousPageCommand(this);
            FirstCommand = new FirstPageCommand(this);
            LastCommand = new LastPageCommand(this);
            this.ComboTipoDoc = negCombo.GetComboTipoDI();
            Lista = "Visible";
            Grabar = "Collapsed";
            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = this.SelectedDataFile;
                if (set != null)
                {
                    Application.Current.Properties["ClienteDelivery"] = set.denominacion;
                    Application.Current.Properties["IdClienteDelivery"] = set.idcli;
                    Application.Current.Properties["NroClienteDelivery"] = set.ndoc;
                }
            });
            m_RowDoubleClickCommand = new DelegateCommandDoubleClick(() =>
            {
                var set = this.SelectedDataFile;
                if (set != null)
                {
                    Cliente = new Cliente();
                    //Cliente.idtd = set.idtd;
                    //Cliente.ndoc = set.ndoc;
                    //Cliente.denominacion = set.denominacion;
                    Cliente = DataCliente.Where(w => w.idcli == set.idcli).FirstOrDefault();
                    if (DataCliente.Where(w => w.idcli == set.idcli).FirstOrDefault().idtd == 6)
                    {
                        Cliente.idtd = 2;
                    }
                    else if(DataCliente.Where(w => w.idcli == set.idcli).FirstOrDefault().idtd == 0)
                    {
                        Cliente.idtd = 5;
                    }
                    else
                    {
                        Cliente.idtd = DataCliente.Where(w => w.idcli == set.idcli).FirstOrDefault().idtd;
                    }
                    Denominac = Cliente.denominacion;
                    Distrito = Cliente.distritocli;
                    //if(Cliente.callecli == "")
                    //{
                    //    Calle = Cliente.dircli;
                    //}
                    //else
                    //{
                        Calle = Cliente.callecli;
                    //}
                    
                    Referencia = Cliente.referenciacli;
                    Lista = "Collapsed";
                    Grabar = "Visible";
                    TituloCliente = "Editar Cliente";
                    TipoOperacionCliente = 2; // agregar
                    NombreBoton = "Actualizar";

                    //SeleccionaCliente(set.idcli,set.denominacion,set.ndoc);
                }
            });
        }
        public void Seleccionar()
        {
            if(Application.Current.Properties["ClienteDelivery"] == null && Application.Current.Properties["IdClienteDelivery"] == null && Application.Current.Properties["NroClienteDelivery"] == null)
            {
                globales.Mensaje("SOS-FOOD - Informacion", "Seleccione un cliente", 2);
                return;
            }
            else
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }     
        }
        public void BuscCliente(object criterio)
        {
            //DataCliente = negCli.GetCliente();
            if (this.Criterio.Trim() != "")
            {
                this.DataCliente = negCli.GetClientexNombreTelefono(this.Criterio);
                this.clienteList = this.DataCliente;

                ViewList = new CollectionViewSource();
                ViewList.Source = ClienteList;
                ViewList.Filter += new FilterEventHandler(view_Filter);

                CurrentPageIndex = 0;
                itemcount = clienteList.Count;
                CalculateTotalPages();

                NextCommand = new NextPageCommand(this);
                PreviousCommand = new PreviousPageCommand(this);
                FirstCommand = new FirstPageCommand(this);
                LastCommand = new LastPageCommand(this);

                FirstCommand.Execute(this);

            }          
        }
        public void SeleccionaCliente(object id,object nombre, object nro)
        {
            Application.Current.Properties["ClienteDelivery"] = nombre;
            Application.Current.Properties["IdClienteDelivery"] = id;
            Application.Current.Properties["NroClienteDelivery"] = nro;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void Cancelar()
        {
            FirstCommand.Execute(this);
            this.Criterio = "";
            this.DataCliente = new ObservableCollection<Cliente>();
            ViewList = new CollectionViewSource();
            clienteList = new ObservableCollection<Cliente>();
            itemcount = clienteList.Count;
            CalculateTotalPages();
            Lista = "Visible";
            Grabar = "Collapsed";

            NextCommand = new NextPageCommand(this);
            PreviousCommand = new PreviousPageCommand(this);
            FirstCommand = new FirstPageCommand(this);
            LastCommand = new LastPageCommand(this);
        }
        private void CancelarDialog()
        {
            Application.Current.Properties["ClienteDelivery"] = null;
            Application.Current.Properties["IdClienteDelivery"] = null;
            Application.Current.Properties["NroClienteDelivery"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void Nuevo()
        {
            NombreBoton = "Guardar";
            this.Cliente = new Cliente();
            this.Cliente.idtd = 5;
            this.Cliente.ndoc = "0";
            Lista = "Collapsed";
            Grabar = "Visible";
            TituloCliente = "Agregar Cliente";
            TipoOperacionCliente = 1; // agregar
        }
        private async void Guardar()
        {
            try
            {
                //TipoOperacionCliente
                if ((!string.IsNullOrWhiteSpace(Cliente.denominacion) && !string.IsNullOrWhiteSpace(Cliente.telcli)))
                {
                    if (Cliente.idtd == null)
                    {
                        Cliente.idtd = 5;
                    }
                    if (Cliente.ndoc == null)
                    {
                        Cliente.ndoc = "0";
                    }
                    if (Cliente.ndoc.Trim() == "")
                    {
                        Cliente.ndoc = "0";
                    }
                    Cliente cliente = this.Cliente;
                    var _id = cliente.idcli;
                    string _mensaje = "";
                    int result = negCli.SetCliente2((_id == 0 ? 1 : 2), cliente, ref _mensaje);
                    globales.Mensaje("SOS-FOOD - Correcto", _mensaje, 1);
                    if (result > 0)
                    {
                        this.DataCliente = negCli.GetClientexId(result);
                        //if (_id != 0)
                        //{
                        //    this.DataCliente = negCli.GetClientexId(result);
                        //}
                        //else {
                        //    this.DataCliente = negCli.GetClientexTelefono(Cliente.telcli);
                        //}
                        //DataCliente = negCli.GetCliente();
                        Criterio = "";
                        Lista = "Visible";
                        Grabar = "Collapsed";
                        Application.Current.Properties["ClienteDelivery"] = this.DataCliente.LastOrDefault().denominacion;
                        Application.Current.Properties["IdClienteDelivery"] = this.DataCliente.LastOrDefault().idcli;
                        Application.Current.Properties["NroClienteDelivery"] = this.DataCliente.LastOrDefault().ndoc;
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                }
                //return;
            }
            catch(Exception ex)
            {
                //globales.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            /*}
            else
            {
                globales.Mensaje("SOS-FOOD - Informacion","El cliente ya fue registrado ",2);
            }*/
        }
        private async void ServicioSunat(object numdoc)
        {
            //string nombrecompleto;
            ServiceReference1.sosfoodSoapClient vf = new ServiceReference1.sosfoodSoapClient();
            try
            {
                bool internet = globales.ValidarInternet();
                if (internet == false)
                {
                    string Estado = "Estimado Cliente:\n" +
                                    "actualmente usted no cuenta con conexión a internet" +
                                    " por favor registre manualmente los datos de su cliente.";
                    globales.Mensaje("SOS-FOOD - Informacion", Estado, 2);
                    return;
                }
                //string nombrecompleto;
                if (!string.IsNullOrWhiteSpace(numdoc.ToString()))
                {
                    if (cliente.idtd == 1)
                    {
                        var h = vf.ConsultaDNI(numdoc.ToString());
                        this.Cliente.denominacion = h.ToString();
                        
                        //this.Cliente = this.Cliente;
                        //this.Denominac = nombrecompleto;
                        //string[] partes = nombrecompleto.Split(' ');
                        //string result = partes[0] + ' ' + partes[1];
                        //string result1 = partes[2] + ' ' + partes[3];
                        //this.Apellido = result;
                        //this.Nombre = result1;
                    }
                    else if (cliente.idtd == 2)
                    {
                        ServiceReference1.ClsClienteConsultaEN cn;
                        cn = vf.ConsultaRuc("0a682fbe-009d-4758-aad1-2ff1092ab7c2-838d6cd5-620a-4add-9632-a3b37c5ae216", numdoc.ToString());
                        this.Cliente.denominacion = cn.nombre_o_razon_social;
                        this.Cliente.callecli = cn.direccion_completa;
                    }
                }
                else return;
            }
            catch (Exception ex)
            {
                var SiNo = new SiNoMessageDialog
                {
                    Mensaje = { Text = "Problemas al cargar: " + ex.Message.ToString() + "" }
                };
                var x = await DialogHost.Show(SiNo, "RootDialog");
                if (!(bool)x)
                    return;
            }
        }
    }
}

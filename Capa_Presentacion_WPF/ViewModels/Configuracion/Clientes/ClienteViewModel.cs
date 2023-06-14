using Capa_Negocio.Configuracion;
using Capa_Entidades.Models.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using Capa_Negocio;
using Capa_Entidades;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Capa_Negocio.Funciones_Globales;
using ExportToExcel;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Clientes
{
  public  class ClienteViewModel :IGeneric
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Cliente negCli = new Neg_Cliente();
        Neg_Combo negCombo = new Neg_Combo();
        private Cliente cliente;
        private string operacion;
        private string denominac;
        private string direccion;
        private string distrito;
        private string calle;
        private string referencia;

        public ObservableCollection<Cliente> DataCliente { get; set; }

        public Cliente Cliente
        {
            get => cliente;
            set
            {
                cliente = value;
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
        public bool EnabledBuscarDoc { get; set; } = false;
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand SunatCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public List<Ent_Combo> ComboTipoDoc { get; set; }
        private Ent_Combo _ComboTipoDocSelected;
        public Ent_Combo ComboTipoDocSelected {
            get => _ComboTipoDocSelected;
            set {
                if (value != null) {
                    if (Convert.ToInt32(((Ent_Combo)value).id) == 1 || Convert.ToInt32(((Ent_Combo)value).id) == 2)
                    {
                        EnabledBuscarDoc = true;
                    }
                    else {
                        EnabledBuscarDoc = false;
                    }
                }
                _ComboTipoDocSelected = value;
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
        public string TextoBuscar { get; set; }
        public ClienteViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.SunatCommand = new ParamCommand(new Action<object>(ServicioSunat));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ComboTipoDoc = negCombo.GetComboTipoDI();
        }


        private void Buscar()
        {
            ObservableCollection<Cliente> ls = new ObservableCollection<Cliente>();
            ls = DataCliente = negCli.GetCliente();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataCliente = negCli.GetCliente();
            }
            else
            {
                List<Cliente> lista = ls
                    .Where(w =>
                    w.denominacion.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.ndoc.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.dircli.ToUpper().Contains(TextoBuscar.ToUpper())
                    ).ToList();
                ObservableCollection<Cliente> o = new ObservableCollection<Cliente>(lista);
                DataCliente = o;
            }
        }
        private void ExportarPDF()
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            saveFileDialog1.FileName = "Clientes " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMINACION");
            dt.Columns.Add("NRO DOCUMENTO");
            dt.Columns.Add("DIRECCION");
            dt.Columns.Add("TELEFONO");
            dt.Columns.Add("CORREO");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataCliente)
            {
                dt.Rows.Add(new object[] { p.idcli, p.denominacion, p.ndoc, p.dircli, p.telcli, p.corcli, (p.estadocli == 1) ? "Activo" : "Inactivo"});
            }
            if (dt.Rows.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        string direccion = saveFileDialog1.InitialDirectory;
                        string nombrearchivo = saveFileDialog1.FileName;
                        string ubicacion = saveFileDialog1.FileName;
                        myStream.Close();
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE CLIENTES");
                    }
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }

        private void ExportarExcel()
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            saveFileDialog1.FileName = "Clientes " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMINACION");
            dt.Columns.Add("NRO DOCUMENTO");
            dt.Columns.Add("DIRECCION");
            dt.Columns.Add("TELEFONO");
            dt.Columns.Add("CORREO");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataCliente)
            {
                dt.Rows.Add(new object[] { p.idcli, p.denominacion, p.ndoc, p.dircli, p.telcli, p.corcli, (p.estadocli == 1) ? "Activo" : "Inactivo" });
            }
            if (dt.Rows.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        string direccion = saveFileDialog1.InitialDirectory;
                        string nombrearchivo = saveFileDialog1.FileName;
                        string ubicacion = saveFileDialog1.FileName;
                        myStream.Close();
                        CreateExcelFile.CreateExcelDocument(dt, ubicacion);
                    }
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }
        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.Cliente = this.DataCliente.Where(w => w.idcli == (int)id).FirstOrDefault();
            this.Denominac = Cliente.denominacion;
        }
        private void Nuevo()
        { 
            this.Operacion = "Nuevo";
            this.Cliente = new Cliente();
        }
        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        
        private async void Guardar()
        {
            if (Cliente.idtd == 0) {
                Cliente.idtd = 5;
                Cliente.ndoc = "";
            }
            if (Cliente.ndoc == null) {
                Cliente.ndoc = "";
            }
            if (Cliente.idtd != 5 && Cliente.ndoc == "") {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese el numero de documento, por favor", 2);
                return;
            }
            
            if (!string.IsNullOrWhiteSpace(Cliente.denominacion))
            {
                //var numdoc = DataCliente.Where(w => w.ndoc != Cliente.ndoc).FirstOrDefault();
                Cliente cliente = this.Cliente;
                var _id = cliente.idcli;
                string _mensaje = "";
                if (_id >2)
                {
                    DataCliente = new ObservableCollection<Cliente>();
                }
                var numdoc = DataCliente.Where(w => w.ndoc == Cliente.ndoc && w.denominacion == Cliente.denominacion).ToList();
                if (numdoc.Count == 0 || Operacion != "Nuevo")
                {
                    bool result = negCli.SetCliente((_id == 0 ? 1 : 2), cliente, ref _mensaje);
                    var view = new MessageDialog
                    {
                        Mensaje = { Text = _mensaje }
                    };
                    await DialogHost.Show(view, "RootDialog");
                    if (result)
                    {
                        this.Operacion = "Lista";
                    }
                }
                else
                {
                    string _mensaje1 = "El cliente ya fue registrado ";
                    var view = new MessageDialog
                    {
                        Mensaje = { Text = _mensaje1 }
                    };
                    await DialogHost.Show(view, "RootDialog");
                }
            }
            return;
        }
        private async void Eliminar(object id)
        {
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "Desea eliminar el registro ?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
                return;
            string _mensaje = "";
            Cliente cliente = new Cliente();
            cliente.idcli = (int)id;
            bool result = negCli.SetCliente(3, cliente, ref _mensaje);
            var view = new MessageDialog
            {
                Mensaje = { Text = _mensaje }
            };
            await DialogHost.Show(view, "RootDialog");
            if (result)
            {
                GetLista();
            }
        }
        private void GetLista()
        {
            DataCliente = negCli.GetCliente();
        }
        private async void ServicioSunat(object numdoc)
        {
            //string nombrecompleto;
            ServiceReference1.sosfoodSoapClient vf = new ServiceReference1.sosfoodSoapClient();
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
                //string nombrecompleto;
                if (!string.IsNullOrWhiteSpace(numdoc.ToString()))
                {
                    if (cliente.idtd == 1)
                    {
                        this.Denominac = vf.ConsultaDNI(numdoc.ToString());
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
                        this.Denominac = cn.nombre_o_razon_social;
                        this.Direccion = cn.direccion_completa;
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

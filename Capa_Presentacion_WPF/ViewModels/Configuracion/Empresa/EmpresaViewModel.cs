using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Capa_Entidades.Models;
using Capa_Entidades;
using Capa_Negocio;
using System.Windows;
using Capa_Negocio.Configuracion;
using MaterialDesignThemes.Wpf;
using Capa_Presentacion_WPF.Views.Dialogs;
using Capa_Entidades.Models.Configuracion;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows.Threading;
using Capa_Presentacion_WPF.Views.Dialogs.EmpresaDefault;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Capa_Negocio.Funciones_Globales;
using System.Data;
using Capa_Presentacion_WPF.Views.Dialogs.Correos;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Capa_Negocio.Funciones_Globales;
using ExportToExcel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Capa_Negocio.Parametros;
using System.Collections.Specialized;
using System.Net;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using System.Text;
using System;
using System.Text;
using System.Text.RegularExpressions;
using java.net;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Empleados
{
    public class EmpresaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));

        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();

        Neg_Combo negCombo = new Neg_Combo();
        Neg_Empresa negEmp = new Neg_Empresa();
        Neg_Parametros negParametros = new Neg_Parametros();
        private Empresa empresa;
        private string operacion;
        private string direccionfact;
        private Ent_Combo _paisSelected;
        private Ent_Combo _dptoSelected;
        private Ent_Combo _provSelected;
        private Ent_Combo _diasSelected;
        private string _addemail;
        private string _ubigeo;
        private string _text;

        public string Ubigeo
        {
            get
            {
                return this.Empresa == null ? "" : this.Empresa.empr_ubig;
            }
            set
            {
                this.Empresa.empr_ubig = value;
                _ubigeo = value;
            }

        }
        public string AddEmail
        {
            get => _addemail;
            set
            {
                //this.Empresa.empr_cor = value;
                _addemail = value;
            }
        }
        public string OperacionCorreo { get; set; }
        public bool EnabledGridCorreos { get; set; } = true;
        public Empresa SelectedDataFile { get; set; }
        public ObservableCollection<Empresa> DataEmpresa { get; set; }
        public ObservableCollection<Empresa> DataListaCorreos { get; set; }
        
        public List<Ent_Combo> ComboCorporacion { get; set; }
        public List<Ent_Combo> ComboPais { get; set; }
        public List<Ent_Combo> ComboDepartamento { get; set; }
        public List<Ent_Combo> ComboProvincia { get; set; }
        public List<Ent_Combo> ComboDistrito { get; set; }
        public object Logo { get; set; }
        public object Logo2 { get; set; }
        public string CorreoSelected { get; set; }
        public object m_dialogObject { get; set; }
        public bool openDialog { get; set; }
        public object IDEMPRE { get; set; }
        public object EmpDefault { get; set; }

        //public class ListaCorreos
        //{
        //    public string correo { get; set; }
        //}
        public object DialogObject
        {
            get { return m_dialogObject; }
            set
            {
                if (m_dialogObject == value) return;
                m_dialogObject = value;
            }
        }
        public object NCorreos { get; set; }
        #region selected
        public Ent_Combo PaisSelected
        {
            get => _paisSelected;
            set
            {
                if (value != null)
                {
                    this.ComboDepartamento = negCombo.GetComboDepa(((Ent_Combo)value).id);
                }
                _paisSelected = value;
            }
        }
        public Ent_Combo DptoSelected
        {
            get => _dptoSelected;
            set
            {
                this.ComboProvincia = new List<Ent_Combo>();
                this.ComboDistrito = new List<Ent_Combo>();
                this.Ubigeo = "";
                if (value != null)
                {
                    this.ComboProvincia = negCombo.getNomProv(((Ent_Combo)value).id);
                }
                _dptoSelected = value;
            }
        }
        public Ent_Combo ProvSelected
        {
            get => _provSelected;
            set
            {
                this.ComboDistrito = new List<Ent_Combo>();
                this.Ubigeo = "";
                if (value != null)
                {
                    this.ComboDistrito = negCombo.GetNomDist(((Ent_Combo)value).id);
                }
                _provSelected = value;
            }
        }
        public Ent_Combo DistSelected
        {
            get => _diasSelected;
            set
            {
                if (value != null)
                {
                    var _ubi = ((Ent_Combo)value).id;
                    this.Ubigeo = _ubi;
                }
                _diasSelected = value;
            }
        }
        #endregion
        public Empresa Empresa
        {
            get => empresa;
            set
            {
                empresa = value;
            }
        }
        public ICommand EditarCommand { get; set; }
        public ICommand EditarListaCorreosCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand TiempoCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand CerrarDialogCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand btnImagenCommand { get; set; }
        public ICommand btnAbrirDialogCommand { get; set; }
        public ICommand EliminarListaCorreosCommand { get; set; }
        public ICommand GuardarEmpresaDefauktgCommand { get; set; }
        public ICommand ObtenerrutaCommand { get; set; }
        public ICommand GuardarCorreoCommand { get; set; }
        public ICommand AbrirDialogListaCorreoCommand { get; set; }
        public ICommand RappiCommand { get; set; }
        public ICommand SalirDialogCommand { get; set; }
        public ICommand ActualizarCorreoCommand { get; set; }
        public ICommand CancelarCorreoCommand { get; set; }

        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        private ICommand m_RowClickCommand;
        public ICommand RowClickCommand
        {
            get
            {
                return m_RowClickCommand;
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
        public string DireccionFactura
        {
            get
            {
                return this.Empresa == null ? "" : this.Empresa.empr_ruta_facelect;
            }
            set
            {
                
                if (value != null)
                {
                    this.Empresa.empr_ruta_facelect = value;
                }
                direccionfact = value;
            }
        }
        public string TextoBuscar { get; set; }
        public EmpresaViewModel()
        {
            this.Operacion = "Lista";
            this.OperacionCorreo = "Lista";
            this.EnabledGridCorreos = true;
            if (System.Windows.Application.Current.Properties["Carga"] == null)
            {
                DataListaCorreos = new ObservableCollection<Empresa>();
            }else if(System.Windows.Application.Current.Properties["Carga"] != null)
            {
                DataListaCorreos = new ObservableCollection<Empresa>();
                DataListaCorreos = (ObservableCollection<Empresa>)System.Windows.Application.Current.Properties["Carga"];
            }
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.EditarListaCorreosCommand = new ParamCommand(new Action<object>(EditarListaCorreos));
            this.AbrirDialogListaCorreoCommand = new ParamCommand(new Action<object>(abrirListacorreos));
            this.RappiCommand = new ParamCommand(new Action<object>(RappiWebService));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.CerrarDialogCommand = new RelayCommand(new Action(GuardarDialogCorreos));
            this.SalirDialogCommand = new RelayCommand(new Action(SalirDialog));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.ObtenerrutaCommand = new RelayCommand(new Action(ObtenerRuta));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.GuardarCorreoCommand = new RelayCommand(new Action(guardarCorreo));
            this.btnAbrirDialogCommand = new RelayCommand(new Action(AbrirDialog));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.TiempoCommand = new ParamCommand(new Action<object>(TiempoEspera));
            this.EliminarListaCorreosCommand = new ParamCommand(new Action<object>(QuitarCorreo));
            this.GuardarEmpresaDefauktgCommand = new RelayCommand(new Action(EmpresaDefault));
            this.ComboCorporacion = negCombo.GetCombo_Corp();
            this.ComboPais = negCombo.GetNomPais(); 
            this.ComboDepartamento = new List<Ent_Combo>();//negComboDepa.GetComboDepa();
            this.ComboProvincia = new List<Ent_Combo>(); // negComboProv.getNomProv();
            this.ComboDistrito = new List<Ent_Combo>();// negComboDist.GetNomDist();
            this.btnImagenCommand = new RelayCommand(new Action(CargarIMG));
            this.ActualizarCorreoCommand = new RelayCommand(new Action(ActualizarCorreo));
            this.CancelarCorreoCommand = new RelayCommand(new Action(CancelarCorreo));

            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));

            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = this.SelectedDataFile;
                if (set != null)
                {
                    this.IDEMPRE = set.idEmpr;
                    this.CorreoSelected = set.correo;
                    System.Windows.Application.Current.Properties["id_empre"] = set.idEmpr;

                    //ObservableCollection<BoletaFactura> DataBolFactura = new ObservableCollection<BoletaFactura>(DataBolFac);
                }

            });
        }
        class RappiToken
        {
            public string client_id { get; set; }
            public string client_secret { get; set; }
            public string audience { get; set; }
            public string grant_type { get; set; }
        }
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Descripcion, int Reserved);
        private void RappiWebService(object id)
        {
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
                        //values["token"] = "app963";
                        values["codigo"] = id.ToString();

                        var response = client.UploadValues(negParametros.RappiAbrirTienda(), values);
                        var responseString = Encoding.Default.GetString(response);
                    }
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "No hay conexion a internet", 2);
                }
                }catch (Exception ex)
            {

            }
        }
            private void CancelarCorreo() {
            this.OperacionCorreo = "Lista";
            this.EnabledGridCorreos = true;
        }
        private void ActualizarCorreo() {
            try
            {
                this.DataListaCorreos.Where(w => w.idcorreo == Convert.ToInt32(System.Windows.Application.Current.Properties["idcor"])).FirstOrDefault().correo = AddEmail.ToLower();
                this.OperacionCorreo = "Lista";
                this.EnabledGridCorreos = true;
                this.AddEmail = "";
            }
            catch (Exception ex)
            {
            }
        }
        private void SalirDialog() {
           
            this.OperacionCorreo = "Lista"; 
            this.EnabledGridCorreos = true;
            DialogHost.CloseDialogCommand.Execute(null, null);
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void Buscar()
        {
            ObservableCollection<Empresa> ls = new ObservableCollection<Empresa>();
            ls = DataEmpresa = negEmp.GetEmpresa();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataEmpresa = negEmp.GetEmpresa();
            }
            else
            {
                List<Empresa> lista = ls
                    .Where(w =>
                    w.empr_ruc.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.empr_nom.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.empr_nom_com.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.empr_corp.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.empr_direc.ToUpper().Contains(TextoBuscar.ToUpper()))
                    .ToList();
                ObservableCollection<Empresa> o = new ObservableCollection<Empresa>(lista);
                DataEmpresa = o;
            }
        }
        private void ExportarPDF()
        {
            Stream myStream;
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();

            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            saveFileDialog1.FileName = "Empresas " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("RUC");
            dt.Columns.Add("NOMBRE EMPRESA");
            dt.Columns.Add("NOMBRE COMERCIAL");
            dt.Columns.Add("TELEFONO");
            dt.Columns.Add("CORPORACION");
            dt.Columns.Add("CORREO");
            dt.Columns.Add("DISTRITO");
            dt.Columns.Add("DIRECCION");
            foreach (var p in DataEmpresa)
            {
                dt.Rows.Add(new object[] { p.idEmpr, p.empr_ruc,p.empr_nom, p.empr_nom_com, p.empr_tel, p.empr_corp, p.correo,p.empr_dist,p.empr_direc});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE EMPRESAS");
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
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            saveFileDialog1.FileName = "Empresas " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("RUC");
            dt.Columns.Add("NOMBRE EMPRESA");
            dt.Columns.Add("NOMBRE COMERCIAL");
            dt.Columns.Add("TELEFONO");
            dt.Columns.Add("CORPORACION");
            dt.Columns.Add("CORREO");
            dt.Columns.Add("DISTRITO");
            dt.Columns.Add("DIRECCION");
            foreach (var p in DataEmpresa)
            {
                dt.Rows.Add(new object[] { p.idEmpr, p.empr_ruc, p.empr_nom, p.empr_nom_com, p.empr_tel, p.empr_corp, p.correo, p.empr_dist, p.empr_direc });
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
        private void ObtenerRuta()
        {
            System.Windows.Forms.FolderBrowserDialog file = new System.Windows.Forms.FolderBrowserDialog();
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DireccionFactura = file.SelectedPath.ToString();
            }
        }
        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.Empresa = this.DataEmpresa.Where(w => w.idEmpr == (int)id).FirstOrDefault();
            this.Logo = this.Empresa.empr_logo;
            this.DireccionFactura = this.Empresa.empr_ruta_facelect;
            //this.DataListaCorreos = negEmp.GetEmpresaCorreo(Convert.ToInt32(id));

            //if (DataListaCorreos.Count > 0)
            //{
            //    if (DataListaCorreos.Count == 1)
            //    {
            //        this.AddEmail = DataListaCorreos.Select(s => s.correo).FirstOrDefault();
            //        DataListaCorreos.Clear();
            //        this.NCorreos = DataListaCorreos.Count();
            //    }
            //    else
            //    {
            //        this.NCorreos = DataListaCorreos.Count();
            //        this.AddEmail = "";
            //    }
            //}
        }
        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            //DialogHost.OpenDialogCommand.Execute(DateTime.Now,null) ;
            this.Operacion = "Nuevo";
            this.Empresa = new Empresa();
        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(Empresa.empr_ruc) && !string.IsNullOrWhiteSpace(Empresa.empr_nom)  && 
                !string.IsNullOrWhiteSpace(Empresa.empr_nom_com) && Empresa.idcorp != null&& Empresa.idpais != null&& Empresa.iddepa != null &&
                Empresa.idprov != null && Empresa.iddist != null && !string.IsNullOrWhiteSpace(Empresa.empr_nom) && !string.IsNullOrWhiteSpace(Empresa.empr_urb) && 
                !string.IsNullOrWhiteSpace(Empresa.empr_nom) && Empresa.empr_logo != null)
            {
                
                Empresa empresa = this.Empresa;
                var _id = empresa.idEmpr;
                this.AddEmail = empresa.empr_cor;
                this.Empresa.empr_ruta_facelect=this.DireccionFactura;
                string _mensaje = "";
               
                bool result = negEmp.SetEmpresa((_id == 0 ? 1 : 2), empresa, ref _mensaje);
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion: ", _mensaje, 2);
                if (result)
                {
                    this.Operacion = "Lista";
                }
            }return;
        }
        private async void TiempoEspera(object id)
        {
            System.Windows.Application.Current.Properties["Idempresa"] = id;
            var SiNo = new DialogEmpresaDelivery
            {
               
        };
            var x = await DialogHost.Show(SiNo, "RootDialog");
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
            Empresa empresa = new Empresa();
            empresa.idEmpr = (int)id;
            bool result = negEmp.SetEmpresa(3, empresa, ref _mensaje);
            negFuncionGlobal.Mensaje("SOS-FOOD - Informacion: ", _mensaje, 2);
            if (result)
            {
                GetLista(); 
            }
        }
        private void CargarIMG()
        {
            decimal maxTamanoImgKb = negParametros.MaxTamanoImgKb();
            Microsoft.Win32.OpenFileDialog getimg = new Microsoft.Win32.OpenFileDialog();
            getimg.InitialDirectory = "C:\\";
            getimg.Filter = "Archivos de imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG (*.png)|*.png";

            if (getimg.ShowDialog() == true)
            {
                try
                {
                    string RutaArchivo;
                    RutaArchivo = getimg.FileName.ToString();
                    /*
                     Hasta aqui muestra la imagen del directorio.
                     */
                    byte[] imagen = null;
                    imagen = System.IO.File.ReadAllBytes(RutaArchivo); //Convierto la imagen a byte[] para guardarlo en la base de datos.
                    decimal tamanio = imagen.Length;
                    if (tamanio <= maxTamanoImgKb)
                    {
                        this.Logo = RutaArchivo;
                        this.Empresa.empr_logo = imagen;
                    }
                    else
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "El peso de la imagen excede el peso maximo permitido", 2);
                        return;
                    }
                    //Empresa empresa = this.Empresa;
                    // var _id = empresa.empr_logo();
                    //this.Logo2 = LoadImage(imagen);// si la imagen viene de la base de datos en byte[] lo puedes levantar con ese metodo

                }
                catch (Exception ex)
                {
                    //negFuncionGlobal.Mensaje("SOS-FOOD - Error", "Error: No se puede carga la imagen " + ex.Message, 3);
                }
            }
        }
        private void GetLista()
        {
            DataEmpresa = negEmp.GetEmpresa();
        }
        #region Helpers
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        #endregion
        #region EmpresaDefault
        private void AbrirDialog()
        {
            //Operacion = "Empresa por defecto";
            var view = new DialogEmpresaDefault
            {

            };
            DialogHost.Show(view, "RootDialog");
        }
        private async void EmpresaDefault()
        {
            DataTable dt = new DataTable();
            string _mensaje = "";
            Empresa empresa = new Empresa();
            if (IDEMPRE != null)
            {
                empresa.idEmpr = (int)IDEMPRE;
                bool result = negEmp.SetEmpresa(4, empresa, ref _mensaje);
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion: ", _mensaje, 2);
                if (result)
                {
                    GetLista();
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe seleccionar una empresa ", 2);
            }
            
        }
        #endregion

        #region DialogCorreo
        private void guardarCorreo()
        {
            int hola = DataListaCorreos.Where(w => w.correo == AddEmail).ToList().Count();
            if (!string.IsNullOrWhiteSpace(AddEmail) || hola>0)
            {
                Empresa list = new Empresa();
                if (hola==0)
                {
                    if (email_bien_escrito(AddEmail) == true)
                    {
                        this.NCorreos = DataListaCorreos.Select(s => s.idcorreo).LastOrDefault();
                        list.idcorreo += Convert.ToInt32(NCorreos) + 1;
                        list.correo = this.AddEmail;
                        this.DataListaCorreos.Add(list);
                        if (DataListaCorreos.Count > 0)
                        {
                            this.AddEmail = "";
                        }
                    }
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-TIC Información", "El correo ya fue registrado", 2);
                }
            }
        }
        private void QuitarCorreo(object idcor)
        {
            DataListaCorreos.Where(w => w.idcorreo == Convert.ToInt32(idcor)).ToList().All(i => DataListaCorreos.Remove(i));
            DataListaCorreos.Select(s => s.idcorreo).ToList();
            if (DataListaCorreos.Count > 0)
            {
                this.AddEmail = "";
            }

        }
        private void EditarListaCorreos(object idcor)
        {
            this.OperacionCorreo = "Editar";
            this.EnabledGridCorreos = false ;
            System.Windows.Application.Current.Properties["idcor"] = idcor;
            this.AddEmail = DataListaCorreos.Where(w => w.idcorreo == Convert.ToInt32(idcor)).FirstOrDefault().correo;
            //DataListaCorreos.Where(w => w.idcorreo == Convert.ToInt32(idcor)).ToList().All(i => DataListaCorreos.Remove(i));
        }
        private void abrirListacorreos(object id)
        {
            System.Windows.Application.Current.Properties["id_empre"] = id;
            System.Windows.Application.Current.Properties["OpenListCorreo"] = "1";
            this.DataListaCorreos = negEmp.GetEmpresaCorreo(Convert.ToInt32(id));
            System.Windows.Application.Current.Properties["Carga"] = DataListaCorreos;
           
            this.EmpDefault = DataEmpresa.Where(w => w.idEmpr == Convert.ToInt32(id)).FirstOrDefault().empr_nom;
            this.openDialog = true;
            DialogObject = new Views.Dialogs.Correos.DialogListaCorreo();
        }
        private void GuardarDialogCorreos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CORREOS");
            foreach (Empresa item in DataListaCorreos)
            {
                DataRow row = dt.NewRow();
                row["CORREOS"] = item.correo;
                dt.Rows.Add(row);
            }
            Empresa empresa = new Empresa();
            empresa.idEmpr = Convert.ToInt32(System.Windows.Application.Current.Properties["id_empre"]);
            string _mensaje = "";
            bool result = negEmp.SetEmpresaCorrreo((1), empresa, ref _mensaje,dt);
            DialogHost.CloseDialogCommand.Execute(null, null);

            if (result)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion: ", "Correo(s) registrado(s) correctamente", 1);
                this.Operacion = "Lista";
                System.Windows.Application.Current.Properties["Carga"] = null;
            }
            else {

                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion: ", "No se pudo registrar el/los correos", 2);
            }
            this.OperacionCorreo = "Lista";
            this.EnabledGridCorreos = true;
        }
        private Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
 

internal static class StringHelper
    {
        //------------------------------------------------------------------------------------
        //	This method is used to replace calls to the 2-arg Java String.startsWith method.
        //------------------------------------------------------------------------------------
        public static bool StartsWith(this string self, string prefix, int toffset)
        {
            return self.IndexOf(prefix, toffset, StringComparison.Ordinal) == toffset;
        }

        //------------------------------------------------------------------------------
        //	This method is used to replace most calls to the Java String.split method.
        //------------------------------------------------------------------------------
        public static string[] Split(this string self, string regexDelimiter, bool trimTrailingEmptyStrings)
        {
            string[] splitArray = Regex.Split(self, regexDelimiter);

            if (trimTrailingEmptyStrings)
            {
                if (splitArray.Length > 1)
                {
                    for (int i = splitArray.Length; i > 0; i--)
                    {
                        if (splitArray[i - 1].Length > 0)
                        {
                            if (i < splitArray.Length)
                                Array.Resize(ref splitArray, i);

                            break;
                        }
                    }
                }
            }

            return splitArray;
        }

        //-----------------------------------------------------------------------------
        //	These methods are used to replace calls to some Java String constructors.
        //-----------------------------------------------------------------------------
        public static string NewString(sbyte[] bytes)
        {
            return NewString(bytes, 0, bytes.Length);
        }
        public static string NewString(sbyte[] bytes, int index, int count)
        {
            return Encoding.UTF8.GetString((byte[])(object)bytes, index, count);
        }
        public static string NewString(sbyte[] bytes, string encoding)
        {
            return NewString(bytes, 0, bytes.Length, encoding);
        }
        public static string NewString(sbyte[] bytes, int index, int count, string encoding)
        {
            return NewString(bytes, index, count, Encoding.GetEncoding(encoding));
        }
        public static string NewString(sbyte[] bytes, Encoding encoding)
        {
            return NewString(bytes, 0, bytes.Length, encoding);
        }
        public static string NewString(sbyte[] bytes, int index, int count, Encoding encoding)
        {
            return encoding.GetString((byte[])(object)bytes, index, count);
        }

        //--------------------------------------------------------------------------------
        //	These methods are used to replace calls to the Java String.getBytes methods.
        //--------------------------------------------------------------------------------
        public static sbyte[] GetBytes(this string self)
        {
            return GetSBytesForEncoding(Encoding.UTF8, self);
        }
        public static sbyte[] GetBytes(this string self, Encoding encoding)
        {
            return GetSBytesForEncoding(encoding, self);
        }
        public static sbyte[] GetBytes(this string self, string encoding)
        {
            return GetSBytesForEncoding(Encoding.GetEncoding(encoding), self);
        }
        private static sbyte[] GetSBytesForEncoding(Encoding encoding, string s)
        {
            sbyte[] sbytes = new sbyte[encoding.GetByteCount(s)];
            encoding.GetBytes(s, 0, s.Length, (byte[])(object)sbytes, 0);
            return sbytes;
        }
    }

}

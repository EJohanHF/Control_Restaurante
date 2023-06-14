using Capa_Entidades;
using Capa_Entidades.Models.Carta;
using Capa_Negocio;
using Capa_Negocio.Carta;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Carta.ItemsViewModel;
using Capa_Presentacion_WPF.Views.Carta;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Capa_Negocio.Funciones_Globales;
using ExportToExcel;
using Capa_Presentacion_WPF.ViewModels.Dialogs;
using Capa_Entidades.Models.Web_Service;
using Capa_Negocio.WebService;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Parametros;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Carta
{
    public class PlatosViewModel : IGeneric
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        static List<Ent_Combo> checkItem = new List<Ent_Combo>();
        PlatosStructure directoryStructure = new PlatosStructure();
        Neg_Parametros negParametros = new Neg_Parametros();
        #region EnlaceNegocio
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Platos negPlatos = new Neg_Platos();
        #endregion
        #region GrupoList Icommand
        private Platos platos;
        private string operacion;
        public byte[] Imagen { get; set; }
        private SCategoria _SCatSelected;
        private Categoria _CatSelected;
        private Ent_Combo _UnidadMedidaSelected;
        public string idUnidadMedida { get; set; }

        public ObservableCollection<Platos> DataPlatos { get; set; }
        //public List<Ent_Combo_S_Cat_Carta> ComboSCat { get; set; }
        public List<Categoria> ComboCat { get; set; }
        public List<SCategoria> CombosCat { get; set; }
        public List<Ent_Combo> ComboClasProdItem { get; set; }
        public List<Ent_Combo> ComboUnidadMedida { get; set; }
        public List<Grupo> ComboGrupo { get; set; }
        public List<Ent_Combo> ComboNiveles { get; set; }
        public List<Ent_Combo> Comboidimpresora { get; set; }
        public List<Impresora> Impresoras { get; set; }
        public bool isDelivery { get; set; }
        public bool isRC { get; set; }
        Neg_CartaDeliveryWebService negCartaDelivery = new Neg_CartaDeliveryWebService();
        //public DataTable dt_impresoras = new DataTable();

        public bool _deliveryCheck { get; set; }
        public bool deliveryCheck
        {
            get => _deliveryCheck;
            set
            {
                if (value == true)
                {
                    isDelivery = true;
                }
                else
                {
                    isDelivery = false;
                }
                _deliveryCheck = value;
            }
        }
        public bool _RCCheck { get; set; }
        public bool RCCheck
        {
            get => _RCCheck;
            set
            {
                if (value == true)
                {
                    isRC = true;
                }
                else
                {
                    isRC = false;
                }
                _RCCheck = value;
            }
        }

        public Ent_Combo selectcheck { get; set; }

        public SCategoria SCatSelected
        {
            get => _SCatSelected;
            set
            {
                if (value != null)
                {
                    this.ComboCat = negCombo.GetCategoriaxSC(((SCategoria)value).idscat);
                }
                _SCatSelected = value;


            }
        }
        public Categoria CatSelected
        {
            get => _CatSelected;
            set
            {
                if (value != null)
                {
                    this.ComboGrupo = negCombo.GetComboGrupoxC(((Categoria)value).idcat);
                }
                _CatSelected = value;


            }
        }
        public Platos Platos
        {
            get => platos;
            set
            {
                platos = value;

            }
        }

        public string IdGrupo { get; set; } = "0";
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand CloudCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand btnImagenCommand { get; set; }
        public ICommand DatoDelivery { get; set; }
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
        public string TextoBuscar { get; set; }
        #endregion
        #region Public Properties
        public ObservableCollection<PlatosItemViewModel> checkItems { get; set; }
        public string TextoNivelSeleccionados { get; set; }
        public string ForegroundComboNiveles { get; set; }
        #endregion
        #region Commands
        public ICommand UcsCommand { get; set; }
        public ICommand TraercheckImpreCommand { get; set; }
        public ICommand Ops { get; set; }
        public ICommand NivelesSelectedCommand { get; set; }
        #endregion
        private Grupo _CargarPlatosxGrupo;
        public Grupo CargarPlatosxGrupo
        {
            get => _CargarPlatosxGrupo;
            set
            {
                if (value != null)
                {
                    ObservableCollection<Platos> p = DataPlatos = negPlatos.GetPlatos();

                    List<Platos> plato = new List<Platos>();
                    plato = p.Where(w => w.idgrup == ((Grupo)value).idgrup.ToString()).ToList();
                    IdGrupo = ((Grupo)value).idgrup.ToString();
                    ObservableCollection<Platos> pp = new ObservableCollection<Platos>(plato);
                    DataPlatos = pp;
                }
                _CargarPlatosxGrupo = value;
            }
        }
        public PlatosViewModel()
        {
            #region PlatosCommand
            this.Operacion = "Lista";
            IdGrupo = "0";
            System.Windows.Application.Current.Properties["CartaDelivery"] = null;
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.CloudCommand = new RelayCommand(new Action(Cloud));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.TraercheckImpreCommand = new ParamCommand(new Action<object>(traercheck));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.btnImagenCommand = new RelayCommand(new Action(CargarIMG));
            this.DatoDelivery = new RelayCommand(new Action(GuardarPlatoDelivery));
            this.CombosCat = negCombo.GetComboSuperCategoria();
            this.ComboClasProdItem = negCombo.GetComboClasProdItem();
            this.ComboUnidadMedida = negCombo.GetCombo_UM();
            //this.ComboGrupo = negCombo.GetComboGrupo();
            this.Impresoras = new List<Impresora>();
            this.Platos = new Platos();
            this.NivelesSelectedCommand = new ParamCommand(new Action<object>(NivelesSelectedC));
            #endregion
            this.idUnidadMedida = "4";
            this.ComboNiveles = negCombo.GetComboNiveles();
            #region structureviewmodel

            var children = directoryStructure.GetLogicalDrives();
            this.checkItems = new ObservableCollection<PlatosItemViewModel>(
                children.Select(drive => new PlatosItemViewModel(drive.idchek, drive.nomchek, false)));

            #endregion

        }
        public void NivelesSelectedC(object id)
        {
            string _id = (string)id;
            bool ischeck = this.ComboNiveles.Where(w => w.id == _id).FirstOrDefault().ischeck;
            if (ischeck == false) { this.ComboNiveles.Where(w => w.id == _id).FirstOrDefault().ischeck = false; }
            else { this.ComboNiveles.Where(w => w.id == _id).FirstOrDefault().ischeck = true; }

            string[] texto = this.ComboNiveles.Where(w => w.ischeck == true).Select(s => s.nombre).ToArray();
            string h = "";
            for (int i = 0; i <= texto.Count() - 1; i++)
            {
                if (h == "") { h = texto[i].ToString(); }
                else { h = h + ", " + texto[i].ToString(); }
            }
            ForegroundComboNiveles = "Black";
            TextoNivelSeleccionados = h;
        }
        public async void GuardarPlatoDelivery()
        {
            if (this.Platos == null)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Rellene todos los campos correctamente.", 2);
                return;
            }
            if (this.Platos.nomplato == null)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Rellene todos los campos correctamente.", 2);
                return;
            }
            if (this.Platos.nomplato.Trim() == "")
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Rellene todos los campos correctamente.", 2);
                return;
            }
            System.Windows.Application.Current.Properties["NomCarta"] = this.Platos.nomplato;
            var AbrirPLatoDelivery = new DialogCartaDelivery
            {

            };
            var x3 = await DialogHost.Show(AbrirPLatoDelivery, "RootDialog");
        }
        private void Buscar()
        {
            ObservableCollection<Platos> ls = new ObservableCollection<Platos>();
            ls = DataPlatos = negPlatos.GetPlatos();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataPlatos = negPlatos.GetPlatos();
            }
            else
            {
                List<Platos> lista = ls
                    .Where(w =>
                    w.nomplato.ToUpper().Contains(TextoBuscar.ToUpper())).ToList();
                ObservableCollection<Platos> o = new ObservableCollection<Platos>(lista);
                DataPlatos = o;
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
            saveFileDialog1.FileName = "Platos " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("CATEGORIA");
            dt.Columns.Add("GRUPO");
            dt.Columns.Add("PLATO");
            dt.Columns.Add("PRECIO");
            dt.Columns.Add("NIVEL");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataPlatos)
            {
                dt.Rows.Add(new object[] { p.idplato, p.nomcat, p.nomgrup, p.nomplato, p.precplato, p.nivelplato, (p.estadoplato == 1) ? "Activo" : "Inactivo" });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE PLATOS");
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
            saveFileDialog1.FileName = "Platos " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("CATEGORIA");
            dt.Columns.Add("GRUPO");
            dt.Columns.Add("PLATO");
            dt.Columns.Add("PRECIO");
            dt.Columns.Add("NIVEL");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataPlatos)
            {
                dt.Rows.Add(new object[] { p.idplato, p.nomcat, p.nomgrup, p.nomplato, p.precplato, p.nivelplato, (p.estadoplato == 1) ? "Activo" : "Inactivo" });
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
        //void Impresoras()
        //{
        //    dt_impresoras.Columns.Add("id_impresora");
        //}
        #region Metodos de Command
        private void traercheck(object idchek)
        {
            if (this.Impresoras.Where(w => w.idimp == (int)idchek).FirstOrDefault() == null)
            {
                this.Impresoras.Add(new Impresora() { idimp = (int)idchek });
            }
            else
            {
                this.Impresoras.Remove(this.Impresoras.Where(w => w.idimp == (int)idchek).FirstOrDefault());
            }
        }

        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.Platos = this.DataPlatos.Where(w => w.idplato == (int)id).FirstOrDefault();
            System.Windows.Application.Current.Properties["CodCarta"] = this.Platos.idplato;
            System.Windows.Application.Current.Properties["NomCarta"] = this.Platos.nomplato;
            this.ComboNiveles = negCombo.GetComboNiveles();
            TextoNivelSeleccionados = "";
            ForegroundComboNiveles = "Black";
            string[] idnivel;
            char[] spearatorScat = { ',' };
            idnivel = this.Platos.id_niveles.Split(spearatorScat).ToArray();
            idnivel = idnivel.Distinct().ToArray();
            string h = "";
            for (int i = 0; i <= idnivel.Count() - 1; i++)
            {
                if (idnivel[i].ToString() != "0")
                {
                    this.ComboNiveles.Where(w => w.id == idnivel[i].ToString()).FirstOrDefault().ischeck = true;
                    string[] texto = this.ComboNiveles.Where(w => w.id == idnivel[i].ToString()).Select(s => s.nombre).ToArray();
                    for (int j = 0; j <= texto.Count() - 1; j++)
                    {
                        if (h == "") { h = texto[j].ToString(); }
                        else { h = h + ", " + texto[j].ToString(); }
                    }
                }
            }

            ForegroundComboNiveles = "Black";
            TextoNivelSeleccionados = h;


            this.idUnidadMedida = this.Platos.idUnidadMedida;
            string _mensaje = "";
            this.deliveryCheck = this.DataPlatos.Where(w => w.idplato == (int)id).FirstOrDefault().estadoD;
            if (this.deliveryCheck == false)
            {
                this.isDelivery = false;
            }
            else
            {
                this.isDelivery = true;
            }
            this.Impresoras = negPlatos.getImpresoraPlato(id.ToString(), ref _mensaje);
            var children = directoryStructure.GetLogicalDrives();
            foreach (var item in children)
            {
                item.valor1 = false;
            }
            this.checkItems = new ObservableCollection<PlatosItemViewModel>(
                         children.Select(drive => new PlatosItemViewModel(drive.idchek, drive.nomchek, (bool)drive.valor1)));
            bool vacio = true;
            foreach (var imp in this.Impresoras)
            {
                vacio = false;
                Boolean yapaso = false;
                foreach (var item in children)
                {
                    if (imp.idimp == item.idchek && yapaso == false)
                    {
                        item.valor1 = true;
                        yapaso = true;
                    }
                    else
                    {
                        //if ((Boolean)item.valor1 == true)
                        //{
                        //    if (yapaso == true)
                        //    {
                        //        item.valor1 = false;
                        //    }
                        //}
                    }
                }
            }

            this.checkItems = new ObservableCollection<PlatosItemViewModel>(
                     children.Select(drive => new PlatosItemViewModel(drive.idchek, drive.nomchek, (bool)drive.valor1)));





        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            this.ComboNiveles = negCombo.GetComboNiveles();
            TextoNivelSeleccionados = "";
            ForegroundComboNiveles = "Black";
            this.Operacion = "Nuevo";
            this.Platos = new Platos();
            this.Impresoras = new List<Impresora>();
            //Platos.idproditem = this.ComboClasProdItem.FirstOrDefault().id;
            this.ComboClasProdItem = negCombo.GetComboClasProdItem();
            System.Windows.Application.Current.Properties["CodCarta"] = null;
            deliveryCheck = false;
            RCCheck = false;
            var children = directoryStructure.GetLogicalDrives();
            this.checkItems = new ObservableCollection<PlatosItemViewModel>(
                children.Select(drive => new PlatosItemViewModel(drive.idchek, drive.nomchek, false)));
        }
        public ObservableCollection<Empresa> DataEmpresa { get; set; }
        Neg_Empresa negEmp = new Neg_Empresa();
        public ObservableCollection<CartaDeliveryWebService> DataCartaDelivery { get; set; }
        private CartaDeliveryWebService cartadelivery;
        public CartaDeliveryWebService CartaDelivery
        {
            get => cartadelivery;
            set
            {
                cartadelivery = value;
            }
        }

        public string Logo { get; private set; }
        private async void Cloud()
        {
            try
            {
                ObservableCollection<Platos> p = DataPlatos = negPlatos.GetPlatosCloud();
                if (DataPlatos.Count != 0)
                {
                    ObservableCollection<items> items = new ObservableCollection<items>();
                    DataEmpresa = negEmp.GetEmpresa();
                    string codigo_cliente2 = DataEmpresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo;
                    foreach (var item in this.DataPlatos)
                    {
                        items new_item = new items();
                        new_item.id_business = codigo_cliente2;
                        new_item.IDCARTA = item.idplato.ToString();
                        new_item.IDCAT = item.idcat;
                        new_item.CAR_NOM = item.nomplato;
                        new_item.CAR_PRECIO = item.precplato.ToString();
                        if (item.estadoplato == 1)
                        {
                            new_item.CAR_EST = "1";
                        }
                        else
                        {
                            new_item.CAR_EST = "0";
                        }
                        new_item.CAR_DESCRIP = item.descrip_plato;
                        new_item.CAR_PRECIO_DEL = item.precplato_delivery.ToString();
                        if (item.estadoplato_delivery == 1)
                        {
                            new_item.CAR_ESTADO_DEL = "1";
                        }
                        else
                        {
                            new_item.CAR_ESTADO_DEL = "0";
                        }
                        new_item.CAR_IMG = System.Convert.ToBase64String(item.imgplato);
                        items.Add(new_item);
                    }
                    string json_items = JsonConvert.SerializeObject(items);
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        values["items"] = json_items;

                        var response = client.UploadValues(negParametros.GuardarPlato(), values);
                        var responseString = Encoding.Default.GetString(response);
                        var WebService = new RespuestaWebService();

                        WebService = JsonConvert.DeserializeObject<RespuestaWebService>(responseString);
                        if (WebService == null)
                        {
                            var view = new MessageDialog
                            {
                                Mensaje = { Text = "Error al subir los platos." }
                            };
                            await DialogHost.Show(view, "RootDialog");
                        }
                        else
                        {
                            if (WebService.mensaje == "ok3")
                            {
                                int update = negPlatos.sp_cloud_update();
                                if (update == 1)
                                {
                                    var view = new MessageDialog
                                    {
                                        Mensaje = { Text = "Data subida correctamente." }
                                    };
                                    await DialogHost.Show(view, "RootDialog");
                                }
                                else
                                {
                                    var view = new MessageDialog
                                    {
                                        Mensaje = { Text = "Error al subir los platos." }
                                    };
                                    await DialogHost.Show(view, "RootDialog");
                                }
                            }
                            else
                            {
                                var view = new MessageDialog
                                {
                                    Mensaje = { Text = "Error al subir los platos." }
                                };
                                await DialogHost.Show(view, "RootDialog");
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
        }
        public int[] idNiveles = null;
        private async void Guardar()
        {
            try
            {
                if (Platos.idscat != null && Platos.idcat != null && Platos.idgrup != null && !string.IsNullOrWhiteSpace(platos.nomplato))
                {
                    idNiveles = ComboNiveles.Where(w => w.ischeck == true).Select(s => Convert.ToInt32(s.id)).ToArray();

                    if (idNiveles.Count() == 0)
                    {
                        ForegroundComboNiveles = "Red";
                        TextoNivelSeleccionados = "Seleccione un Nivel";
                        return;
                    }

                    Platos platos = this.Platos;
                    //this.platos.idimpre = Idcheck;
                    var _id = platos.idplato;

                    //string[] _impresoras = this.Impresoras.Select(s => s.idimp.ToString()).ToArray();
                    platos.impresoras = (this.Impresoras == null ? "" : String.Join(",", this.Impresoras.Select(s => s.idimp).ToArray()));
                    platos.id_niveles = String.Join(",", ComboNiveles.Where(w => w.ischeck == true).Select(s => s.id).ToArray());
                    platos.idUnidadMedida = idUnidadMedida;
                    platos.estadoD = this.deliveryCheck;
                    string _mensaje = "";
                    int result = negPlatos.SetPlatos((_id == 0 ? 1 : 2), platos, ref _mensaje);
                    if (result == 0)
                    {
                        var view = new MessageDialog
                        {
                            Mensaje = { Text = _mensaje }
                        };
                        await DialogHost.Show(view, "RootDialog");
                    }
                    if (result != 0)
                    {
                        this.Operacion = "Lista";
                        //using (var client = new WebClient())
                        //{
                        //    DataEmpresa = negEmp.GetEmpresa();
                        //    string codigo_cliente = DataEmpresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo;
                        //    var values = new NameValueCollection();
                        //    values["name"] = platos.nomplato;
                        //    values["id_business"] = codigo_cliente;
                        //    //values["token"] = "app963";
                        //    values["discount"] = "0";
                        //    values["price"] = (platos.precplato_delivery == null) ? "0": platos.precplato_delivery.ToString();
                        //    values["price_local"] = platos.precplato.ToString();
                        //    values["imagen"] = (platos.imgplato == null) ? "" : platos.imgplato.ToString();
                        //    values["id_carta"] = result.ToString();
                        //    if (platos.estadoplato_delivery == 1)
                        //    {
                        //        values["estado"] = "1";
                        //    }
                        //    else
                        //    {
                        //        values["estado"] = "0";
                        //    }
                        //    if (platos.estadoplato == 1)
                        //    {
                        //        values["estado_local"] = "1";
                        //    }
                        //    else
                        //    {
                        //        values["estado_local"] = "0";
                        //    }

                        //    var response = client.UploadValues(negParametros.GuardarPlato(), values);
                        //    var responseString = Encoding.Default.GetString(response);
                        //    var WebService = new RespuestaWebService();

                        //    WebService = JsonConvert.DeserializeObject<RespuestaWebService>(responseString);
                           
                        //    DialogHost.CloseDialogCommand.Execute(null, null);
                        //}
                    }
                    if (IdGrupo != "0")
                    {
                        ObservableCollection<Platos> p = DataPlatos = negPlatos.GetPlatos();

                        List<Platos> plato = new List<Platos>();
                        plato = p.Where(w => w.idgrup == IdGrupo).ToList();
                        ObservableCollection<Platos> pp = new ObservableCollection<Platos>(plato);
                        DataPlatos = pp;
                    }
                    else
                    {
                        var children = directoryStructure.GetLogicalDrives();
                        this.checkItems = new ObservableCollection<PlatosItemViewModel>(
                            children.Select(drive => new PlatosItemViewModel(drive.idchek, drive.nomchek, false)));
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
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
            Platos platos = new Platos();
            platos.idplato = (int)id;
            int result = negPlatos.SetPlatos(3, platos, ref _mensaje);
            if (result == 0)
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = _mensaje }
                };
                await DialogHost.Show(view, "RootDialog");
            }
            if (result != 0)
            {
                GetLista();
            }
        }

        private void GetLista()
        {
            if (IdGrupo != "0")
            {
                ObservableCollection<Platos> p = DataPlatos = negPlatos.GetPlatos();
                List<Platos> plato = new List<Platos>();
                plato = p.Where(w => w.idgrup == IdGrupo).ToList();
                ObservableCollection<Platos> pp = new ObservableCollection<Platos>(plato);
                DataPlatos = pp;
            }
            else
            {
                DataPlatos = negPlatos.GetPlatos();
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

                    byte[] imagen = null;
                    imagen = File.ReadAllBytes(RutaArchivo); //Convierto la imagen a byte[] para guardarlo en la base de datos.
                    decimal tamanio = imagen.Length;
                    if (tamanio <= maxTamanoImgKb)
                    {
                        this.Logo = RutaArchivo;
                        this.Platos.imgplato = imagen;
                    }
                    else
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "El peso de la imagen excede el peso maximo permitido", 2);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Error", "No se puede carga la imagen : " + ex.Message, 3);
                }
            }
        }
        #endregion
    }
}

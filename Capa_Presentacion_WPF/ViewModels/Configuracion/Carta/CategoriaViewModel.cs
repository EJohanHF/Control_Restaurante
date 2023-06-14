using Capa_Entidades.Models.Carta;
using Capa_Negocio.Carta;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows;
using Capa_Negocio;
using Capa_Entidades;
using Capa_Negocio.Funciones_Globales;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Capa_Negocio.Funciones_Globales;
using ExportToExcel;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Carta.ItemsViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Specialized;
using Capa_Entidades.Models.Web_Service;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Parametros;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Carta
{
    class CategoriaViewModel :IGeneric
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Categoria_carta negCate = new Neg_Categoria_carta();
        private Categoria categoria;
        private string operacion;
        public ObservableCollection<Empresa> empresa { get; set; }
        Neg_Empresa negEmp = new Neg_Empresa();
        Neg_Parametros negParametros = new Neg_Parametros();
        public ObservableCollection<Categoria> DataCategoria { get; set; }
        public ObservableCollection<Capa_Entidades.Models.Configuracion.Empresa> DataEmpresa { get; private set; }
        public List<SCategoria> ComboSuperCategoria { get; set; }
        public Categoria Categoria
        {
            get => categoria;
            set
            {
                categoria = value;
            }
        }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand btnImagenCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public ICommand PasarPlatosxCategoriaImpresoraCommand { get; set; }
        public ICommand AumentarPrecioCommand { get; set; }
        public ICommand DisminuirPrecioCommand { get; set; }
        public RelayCommand CloudCommand { get; }
        public object Logo { get; set; }
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
        public decimal masprecio { get; set; } = 0;
        public decimal menosprecio { get; set; } = 0;
        PlatosStructure directoryStructure = new PlatosStructure();
        public ObservableCollection<PlatosItemViewModel> checkItems { get; set; }
        public ICommand TraercheckImpreCommand { get; set; }
        public List<Impresora> Impresoras { get; set; }
        public CategoriaViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.btnImagenCommand = new RelayCommand(new Action(CargarIMG));
            this.TraercheckImpreCommand = new ParamCommand(new Action<object>(traercheck));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.PasarPlatosxCategoriaImpresoraCommand = new RelayCommand(new Action(PasarPlatosxCategoriaImpresora));
            this.AumentarPrecioCommand = new RelayCommand(new Action(AumentarPrecio));
            this.DisminuirPrecioCommand = new RelayCommand(new Action(DisminuirPrecio));
            this.CloudCommand = new RelayCommand(new Action(Cloud));
            this.ComboSuperCategoria = negCombo.GetComboSuperCategoria();
            this.categoria = new Categoria();

            var children = directoryStructure.GetLogicalDrives();
            this.checkItems = new ObservableCollection<PlatosItemViewModel>(
                children.Select(drive => new PlatosItemViewModel(drive.idchek, drive.nomchek, false)));
            this.Impresoras = new List<Impresora>();
        }
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

        private void Buscar()
        {
            ObservableCollection<Categoria> ls = new ObservableCollection<Categoria>();
            ls = DataCategoria = negCate.GetCategoria();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataCategoria = negCate.GetCategoria();
            }
            else
            {
                List<Categoria> lista = ls
                    .Where(w =>
                    w.nomcat.ToUpper().Contains(TextoBuscar.ToUpper())).ToList();
                ObservableCollection<Categoria> o = new ObservableCollection<Categoria>(lista);
                DataCategoria = o;
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
            saveFileDialog1.FileName = "Categorias " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE SUPER CATEGORIA");
            dt.Columns.Add("CATEGORIA");
            dt.Columns.Add("DESCUENTO");
            foreach (var p in DataCategoria)
            {
                dt.Rows.Add(new object[] { p.idcat, p.nomscat, p.nomcat, p.desccat});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE EMPLEADOS");
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
            saveFileDialog1.FileName = "Categorias " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE SUPER CATEGORIA");
            dt.Columns.Add("CATEGORIA");
            dt.Columns.Add("DESCUENTO");
            foreach (var p in DataCategoria)
            {
                dt.Rows.Add(new object[] { p.idcat, p.nomscat, p.nomcat, p.desccat });
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
        private async void PasarPlatosxCategoriaImpresora() {
            var view = new SiNoMessageDialog
            {
                Mensaje = { Text = "¿Deseas pasar los platos de la categoria a esta(s) impresora(s)?" }
            };
            var x = await DialogHost.Show(view, "RootDialog");
            if ((bool)x)
            {
                Categoria.impresoras = (this.Impresoras == null ? "" : String.Join(",", this.Impresoras.Select(s => s.idimp).ToArray()));
                bool result = negCate.setImpresoraCartaxCat(Categoria);
                Impresoras = new List<Impresora>();
            }
            else {
                return;
            }
        }
        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.Categoria = this.DataCategoria.Where(w => w.idcat == (int)id).FirstOrDefault();
            this.Logo = this.Categoria.imagencat;
        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            //DialogHost.OpenDialogCommand.Execute(DateTime.Now,null) ;
            this.Operacion = "Nuevo";
            this.Categoria = new Categoria();
            this.Logo = new object();
        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(Categoria.nomcat) && Categoria.idscat!=null && Categoria.imagencat != null && Categoria.desccat != null )
            {
                if (Categoria.desccat.ToString().Trim() == "")
                {
                    Categoria.desccat = 0;
                }
                Categoria categoria = this.Categoria;
                var _id = categoria.idcat;
                string _mensaje = "";
                bool result = negCate.SetsCategoria((_id == 0 ? 1 : 2), categoria, ref _mensaje);
                if (!result)
                {
                    var view = new MessageDialog
                    {
                        Mensaje = { Text = _mensaje }
                    };
                    await DialogHost.Show(view, "RootDialog");
                }
                
                if (result)
                {
                    this.Operacion = "Lista";
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese todos los campos", 2);
                return;
            }
            if (Categoria.imagencat == null) 
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = "Debe elegir una imagen para la categoria nueva" }
                };
                await DialogHost.Show(view, "RootDialog");
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
            Categoria categoria = new Categoria();
            categoria.idcat = (int)id;
            bool result = negCate.SetsCategoria(3, categoria, ref _mensaje);
            if (!result)
            {
                var view = new MessageDialog
                {
                    Mensaje = { Text = _mensaje }
                };
                await DialogHost.Show(view, "RootDialog");
            }
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
                    
                    byte[] imagen = null;
                    imagen = File.ReadAllBytes(RutaArchivo); //Convierto la imagen a byte[] para guardarlo en la base de datos.
                    decimal tamanio = imagen.Length;
                    if (tamanio <= maxTamanoImgKb)
                    {
                        this.Logo = RutaArchivo;
                        this.Categoria.imagencat = imagen;
                    }
                    else
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "El peso de la imagen excede el peso maximo permitido", 2);
                        return;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void GetLista()
        {
            DataCategoria = negCate.GetCategoria();
            Impresoras = new List<Impresora>();
        }
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
        private async void AumentarPrecio() {
            var view = new SiNoMessageDialog
            {
                Mensaje = { Text = "¿Deseas aumentar el precio de los platos de esta categoria?" }
            };
            var x = await DialogHost.Show(view, "RootDialog");
            if ((bool)x)
            {
                bool result = negCate.AumentarPrecioxCategoria(Categoria,masprecio);
            }
            else
            {
                return;
            }
        }
        private async void DisminuirPrecio() {
            var view = new SiNoMessageDialog
            {
                Mensaje = { Text = "¿Deseas disminuir el precio de los platos de esta categoria?" }
            };
            var x = await DialogHost.Show(view, "RootDialog");
            if ((bool)x)
            {
                bool result = negCate.DisminuirPrecioxCategoria(Categoria,menosprecio);
            }
            else
            {
                return;
            }
        }
        private async void Cloud()
        {
            try
            {
                ObservableCollection<Categoria> p = DataCategoria = negCate.GetCatCloud();
                if (DataCategoria.Count != 0)
                {
                    ObservableCollection<items_cat> items_cate = new ObservableCollection<items_cat>();
                    DataEmpresa = negEmp.GetEmpresa();
                    string codigo_cliente2 = DataEmpresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo;
                    foreach (var item in this.DataCategoria)
                    {
                        items_cat new_item = new items_cat();
                        new_item.id_business = codigo_cliente2;
                        new_item.id_cat = item.idcat.ToString();
                        new_item.nom_cat = item.nomcat.ToString();
                        new_item.img_cat = System.Convert.ToBase64String(item.imagencat);

                        items_cate.Add(new_item);
                    }
                    string json_items = JsonConvert.SerializeObject(items_cate);
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        values["items"] = json_items;

                        var response = client.UploadValues(negParametros.GuardarCat(), values);
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
                                int update = negCate.sp_cloud_update();
                                if (update == 1)
                                {
                                    var view = new MessageDialog
                                    {
                                        Mensaje = { Text = "¡Categoria(s) Sincronizadas Correctamente!" }
                                    };
                                    await DialogHost.Show(view, "RootDialog");

                                }
                                else
                                {
                                    var view = new MessageDialog
                                    {
                                        Mensaje = { Text = "Error al subir la(s) Categoria(s)" }
                                    };
                                    await DialogHost.Show(view, "RootDialog");
                                }
                            }
                            else
                            {
                                var view = new MessageDialog
                                {
                                    Mensaje = { Text = "Error al subir la(s) Categoria(s)." }
                                };
                                await DialogHost.Show(view, "RootDialog");
                            }
                            DataCategoria = negCate.GetCategoria();
                        }

                    }
                }

                DataCategoria = negCate.GetCategoria();
            }
            catch (Exception ex)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
            }
        }
    }
}

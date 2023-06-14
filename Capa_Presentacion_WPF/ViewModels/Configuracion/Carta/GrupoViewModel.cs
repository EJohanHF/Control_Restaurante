using Capa_Entidades;
using Capa_Entidades.Models.Carta;
using Capa_Negocio;
using Capa_Negocio.Carta;
using Capa_Negocio.Funciones_Globales;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Capa_Negocio.Funciones_Globales;
using ExportToExcel;
using Capa_Negocio.Parametros;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Carta
{
    public class GrupoViewModel : IGeneric
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Grupo negGrupo = new Neg_Grupo();
        Neg_Parametros negParametros = new Neg_Parametros();
        private Grupo _grupo;
        private string operacion;
        private SCategoria _SCatSelected;
        public ObservableCollection<Grupo> DataGrupo { get; set; }
        //public List<Ent_Combo_S_Cat_Carta> ComboSCat { get; set; }
        public List<Categoria> ComboCat { get; set; }
        public List<SCategoria> CombosCat { get; set; }
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
        public Grupo Grupo
        {
            get => _grupo;
            set
            {
                _grupo = value;
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
        public GrupoViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.btnImagenCommand = new RelayCommand(new Action(CargarIMG));

            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));

            this.CombosCat = negCombo.GetComboSuperCategoria();
            //this.CombosCat = new List<Ent_Combo>();

            //this.ComboCat = new List<Ent_Combo>(); 
        }


        private void Buscar()
        {
            ObservableCollection<Grupo> ls = new ObservableCollection<Grupo>();
            ls = DataGrupo = negGrupo.GetGupo();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataGrupo = negGrupo.GetGupo();
            }
            else
            {
                List<Grupo> lista = ls
                    .Where(w =>
                    w.nomgrup.ToUpper().Contains(TextoBuscar.ToUpper())).ToList();
                ObservableCollection<Grupo> o = new ObservableCollection<Grupo>(lista);
                DataGrupo = o;
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
            saveFileDialog1.FileName = "Grupos " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("SUPER CATEGORIA");
            dt.Columns.Add("CATEGORIA");
            dt.Columns.Add("GRUPO");
            dt.Columns.Add("DESCUENTO");
            foreach (var p in DataGrupo)
            {
                dt.Rows.Add(new object[] { p.idgrup, p.nomscat, p.nomcat, p.nomgrup, p.descgrup});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE GRUPOS");
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
            saveFileDialog1.FileName = "Grupos " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("SUPER CATEGORIA");
            dt.Columns.Add("CATEGORIA");
            dt.Columns.Add("GRUPO");
            dt.Columns.Add("DESCUENTO");
            foreach (var p in DataGrupo)
            {
                dt.Rows.Add(new object[] { p.idgrup, p.nomscat, p.nomcat, p.nomgrup, p.descgrup });
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
            this.Grupo = this.DataGrupo.Where(w => w.idgrup == (int)id).FirstOrDefault();
            this.Logo = this.Grupo.imagengrup;
        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            //DialogHost.OpenDialogCommand.Execute(DateTime.Now,null) ;
            this.Operacion = "Nuevo";
            this.Grupo = new Grupo();
            this.Logo = new object();
        }
        private async void Guardar()
        {
            if (Grupo.idscat!=null && Grupo.idcat !=null  && !string.IsNullOrWhiteSpace(Grupo.nomgrup) && Grupo.imagengrup != null && Grupo.descgrup != null)
            {
                if (Grupo.descgrup.ToString().Trim() == "")
                {
                    Grupo.descgrup = 0;
                }
                Grupo grupo = this.Grupo;
                var _id = grupo.idgrup;
                string _mensaje = "";
                bool result = negGrupo.SetGrupo((_id == 0 ? 1 : 2), grupo, ref _mensaje);
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
            Grupo grupo = new Grupo();
            grupo.idgrup = (int)id;
            bool result = negGrupo.SetGrupo(3, grupo, ref _mensaje);
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
                        this.Grupo.imagengrup = imagen;
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
            DataGrupo = negGrupo.GetGupo();
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

    }

}

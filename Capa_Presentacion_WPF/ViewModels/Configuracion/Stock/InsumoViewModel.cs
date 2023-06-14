using Capa_Entidades;
using Capa_Entidades.Models.Stock;
using Capa_Negocio;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Stock;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Factura_Compra;
using Capa_Presentacion_WPF.ViewModels.Configuracion.Stock.Insumo_;
using Capa_Presentacion_WPF.Views.Dialogs;
using ExportToExcel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Stock
{
    public class InsumoViewModel : IGeneric
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_UnidaMedida negUM = new Neg_UnidaMedida();
        Neg_Insumos negInsumo = new Neg_Insumos();
        Neg_Combo negCombo = new Neg_Combo();
        InsumosStructure directoryStructure = new InsumosStructure();
        private Insumos insumos;
        private string operacion;

        public ObservableCollection<Insumos> DataInsumos { get; set; }
        public ObservableCollection<InsumoItemsViewModel> checkItems { get; set; }
        public Insumos Insumos
        {
            get => insumos;
            set
            {
                insumos = value;
            }
        }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand TraercheckImpreCommand { get; set; }

        //public ICommand SunatCommand { get; set; }

        public List<Ent_Combo> ComboCategoria { get; set; }
        public List<Ent_Combo> ComboAlmacen { get; set; }
        public List<Ent_Combo> ComboUnidadMedida { get; set; }
        public List<Ent_Combo> ComboTipo_Insumo { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        public List<Almacen> Almacen { get; set; }

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
        public InsumoViewModel()
        {
            this.Operacion = "Lista";
            string OperacionInicial;

            if (System.Windows.Application.Current.Properties["OperacionxFactCompra"] != null) // Estas acciones son para cuando se abra el usercontrol desde otro user control segun la operacion que desea ver
            {
                OperacionInicial = System.Windows.Application.Current.Properties["OperacionxFactCompra"].ToString();
                if (OperacionInicial == "Nuevo")
                {
                    this.Operacion = OperacionInicial;
                    Nuevo();
                }
                else if (OperacionInicial == "Lista")
                {
                    this.Operacion = OperacionInicial;
                }
            }
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.TraercheckImpreCommand = new ParamCommand(new Action<object>(traercheck));

            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));

            //this.SunatCommand = new ParamCommand(new Action<object>(ConsultaSunat));
            this.ComboCategoria = negCombo.GetCate();
            this.ComboAlmacen = negCombo.GetCombo_Almacen();
            this.ComboUnidadMedida = negCombo.GetCombo_UM();
            this.ComboTipo_Insumo = negCombo.GetComboTipo_Insumo();
            this.Almacen = new List<Almacen>();
            #region structureviewmodel

            var children = directoryStructure.GetLogicalDrives();
            this.checkItems = new ObservableCollection<InsumoItemsViewModel>(
                children.Select(drive => new InsumoItemsViewModel(drive.idchek, drive.nomchek, false)));

            #endregion
        }

        private void Buscar()
        {
            DataInsumos = negInsumo.GetInsumo();

            ObservableCollection<Insumos> ls = new ObservableCollection<Insumos>();
            ls = DataInsumos;
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataInsumos = negInsumo.GetInsumo();
            }
            else
            {
                List<Insumos> lista = ls
                    .Where(w =>
                    w.nomins.ToUpper().Contains(TextoBuscar.ToUpper())).ToList();
                ObservableCollection<Insumos> o = new ObservableCollection<Insumos>(lista);
                DataInsumos = o;
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
            saveFileDialog1.FileName = "Insumos " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE INSUMO");
            dt.Columns.Add("UNI. MEDIDA");
            dt.Columns.Add("CANTIDAD MINIMA");
            dt.Columns.Add("PRECIO");
            dt.Columns.Add("TIPO");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataInsumos)
            {
                dt.Rows.Add(new object[] { p.idins, p.nomins, p.nomaum,p.cantminins,p.precio,p.tipoins, (p.estadoins == 1) ? "Activo" : "Inactivo" });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE INSUMOS");
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
            saveFileDialog1.FileName = "Insumos " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE INSUMO");
            dt.Columns.Add("UNI. MEDIDA");
            dt.Columns.Add("CANTIDAD MINIMA");
            dt.Columns.Add("PRECIO");
            dt.Columns.Add("TIPO");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataInsumos)
            {
                dt.Rows.Add(new object[] { p.idins, p.nomins, p.nomaum, p.cantminins, p.precio, p.tipoins, (p.estadoins == 1) ? "Activo" : "Inactivo" });
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
        private void traercheck(object idchek)
        {
            if (this.Almacen.Where(w => w.idalm == (int)idchek).FirstOrDefault() == null)
            {
                this.Almacen.Add(new Almacen() { idalm = (int)idchek });
            }
            else
            {
                this.Almacen.Remove(this.Almacen.Where(w => w.idalm == (int)idchek).FirstOrDefault());
            }
        }
        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.Insumos = this.DataInsumos.Where(w => w.idins == (int)id).FirstOrDefault();
            string _mensaje = "";

            this.Almacen = negInsumo.getInsumoAlmacen(id.ToString(), ref _mensaje);
            var children = directoryStructure.GetLogicalDrives();
            foreach (var item in children)
            {
                item.valor1 = false;
            }
            this.checkItems = new ObservableCollection<InsumoItemsViewModel>(
                         children.Select(drive => new InsumoItemsViewModel(drive.idchek, drive.nomchek, (bool)drive.valor1)));
            bool vacio = true;
            foreach (var alm in this.Almacen)
            {
                vacio = false;
                Boolean yapaso = false;
                foreach (var item in children)
                {
                    if (alm.idalm == item.idchek && yapaso == false)
                    {
                        item.valor1 = true;
                        yapaso = true;
                    }
                    else
                    {

                    }
                }
            }

            this.checkItems = new ObservableCollection<InsumoItemsViewModel>(
                     children.Select(drive => new InsumoItemsViewModel(drive.idchek, drive.nomchek, (bool)drive.valor1)));
        }
        private void Nuevo()
        {
            this.Operacion = "Nuevo";
            this.Insumos = new Insumos();
            var children = directoryStructure.GetLogicalDrives();
            this.checkItems = new ObservableCollection<InsumoItemsViewModel>(
                children.Select(drive => new InsumoItemsViewModel(drive.idchek, drive.nomchek, false)));
        }
        private void Cancelar()
        {
            this.Operacion = "Lista";
            string OperacionInicial;
            if (System.Windows.Application.Current.Properties["OperacionxFactCompra"] != null) // Estas acciones son para cuando se abra el usercontrol desde otro user control segun la operacion que desea ver
            {
                OperacionInicial = System.Windows.Application.Current.Properties["OperacionxFactCompra"].ToString();
                if (OperacionInicial == "Nuevo")
                { DialogHost.CloseDialogCommand.Execute(null, null); }
            }
        }

        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(Insumos.nomins) &&!string.IsNullOrWhiteSpace(Insumos.idaum) && !string.IsNullOrWhiteSpace(Insumos.cantminins.ToString()) && !string.IsNullOrWhiteSpace(Insumos.precio.ToString()) && !string.IsNullOrWhiteSpace(Insumos.tipoins))
            {
                Insumos insumos = this.Insumos;
                var _id = insumos.idins;
                insumos.almacen = (this.Almacen == null ? "" : String.Join(",", this.Almacen.Select(s => s.idalm).ToArray()));
                string _mensaje = "";
                bool result = negInsumo.SetInsumo((_id == 0 ? 1 : 2), insumos, ref _mensaje);
                var children = directoryStructure.GetLogicalDrives();
                this.checkItems = new ObservableCollection<InsumoItemsViewModel>(
                    children.Select(drive => new InsumoItemsViewModel(drive.idchek, drive.nomchek, false)));
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
                var view = new MessageDialog
                {
                    Mensaje = { Text = "Debe ingresar todos los valores" }
                };
                await DialogHost.Show(view, "RootDialog");
            }
            string OperacionInicial;
            if (System.Windows.Application.Current.Properties["OperacionxFactCompra"] != null) // Estas acciones son para cuando se abra el usercontrol desde otro user control segun la operacion que desea ver
            {
                OperacionInicial = System.Windows.Application.Current.Properties["OperacionxFactCompra"].ToString();
                if (OperacionInicial == "Nuevo")
                { DialogHost.CloseDialogCommand.Execute(null, null); }
            }
                    
        }
        private async void Eliminar(object id)
        {
            try
            {
                var SiNo = new SiNoMessageDialog
                {
                    Mensaje = { Text = "Desea eliminar el registro ?" }
                };
                var x = await DialogHost.Show(SiNo, "RootDialog");
                if (!(bool)x)
                    return;
                string _mensaje = "";
                Insumos insumos = new Insumos();
                insumos.idins = (int)id;
                bool result = negInsumo.SetInsumo(3, insumos, ref _mensaje);
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
            catch (Exception)
            {
                var SiNo = new MessageDialog
                {
                    Mensaje = { Text = "El Campo esta siendo usado " }
                };

            }

        }
        private void GetLista()
        {
            DataInsumos = negInsumo.GetInsumo();
        }


    }
}

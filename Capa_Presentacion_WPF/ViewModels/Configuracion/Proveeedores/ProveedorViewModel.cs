using Capa_Entidades;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Funciones_Globales;
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

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Proveeedores
{
    public class ProveedorViewModel : IGeneric
    {
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Proveedor negProv = new Neg_Proveedor();

        Funcion_Global negFuncionGlobal = new Funcion_Global();
        private Proveedor proveedor;
        private string operacion;

        public ObservableCollection<Proveedor> DataProveedor { get; set; }
        public List<Ent_Combo> ComboTipoDoc { get; set; }
        public Proveedor Proveedor
        {
            get => proveedor;
            set
            {
                proveedor = value;
            }
        }
        public string Consulta { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand CargarSunatCommand { get; set; }
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

        #region BotonServicioSunat
        private string nombre;
        private string rubro;
        private string direccion;
        public string Nombre
        {
            get => nombre;
            set
            {
                if (value != null)
                {
                    this.proveedor.prov_nom = value;
                }
                nombre = value;
            }
        }
        public string Rubro
        {
            get => rubro;
            set
            {
                if (value != null)
                {
                    this.proveedor.prov_rubro = value;
                }
                rubro = value;
            }
        }
        //public string Nombre
        //{
        //    get
        //    {
        //        return this.proveedor == null ? "" : this.proveedor.prov_nom;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            this.proveedor.prov_nom = value;
        //        }
        //        nombre = value;
        //    }
        //}
        //public string Direccion
        //{
        //    get
        //    {
        //        return this.proveedor == null ? "" : this.proveedor.prov_direc;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            this.proveedor.prov_direc = value;
        //        }
        //        direccion = value;
        //    }
        //}
        public string Direccion
        {
            get => direccion;
            set
            {
                if (value != null)
                {
                    this.proveedor.prov_direc = value;
                }
                direccion = value;
            }
        }
        #endregion
        public ProveedorViewModel()
        {
            this.Operacion = "Lista";
            string OperacionInicial;
            
            if (System.Windows.Application.Current.Properties["OperacionxFactCompra"] != null)
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
            this.CargarSunatCommand = new ParamCommand(new Action<object>(ServicioSunat));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ComboTipoDoc = negCombo.GetComboTipoDI();
            Proveedor prov = new Proveedor();
            
        }

        private void Buscar()
        {
            DataProveedor = negProv.GetProv();

            ObservableCollection<Proveedor> ls = new ObservableCollection<Proveedor>();
            ls = DataProveedor;
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataProveedor = negProv.GetProv();
            }
            else
            {
                List<Proveedor> lista = ls
                    .Where(w =>
                    w.prov_nom.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.prov_nrdoc.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.prov_direc.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.prov_telfijo.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.prov_telmovil.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.prov_correo.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.prov_rubro.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.nomdoc.ToUpper().Contains(TextoBuscar.ToUpper()) 
                    ).ToList();
                ObservableCollection<Proveedor> o = new ObservableCollection<Proveedor>(lista);
                DataProveedor = o;
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
            saveFileDialog1.FileName = "Proveedores " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("TIPO DOCUMENTO");
            dt.Columns.Add("NRO DOCUMENTO");
            dt.Columns.Add("NOMBRE PROVEEDOR");
            dt.Columns.Add("RUBRO");
            dt.Columns.Add("DIRECCION");
            dt.Columns.Add("TELEFONO FIJO");
            dt.Columns.Add("TELEFONO MOVIL");
            dt.Columns.Add("CORREO");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataProveedor)
            {
                dt.Rows.Add(new object[] { p.idp,p.nomdoc,p.prov_nrdoc, p.prov_nom,p.prov_rubro,p.prov_direc,p.prov_telfijo,p.prov_telmovil,p.prov_correo, (p.prov_activo== 1) ? "Activo" : "Inactivo" });
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE PROVEEDORES");
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
            saveFileDialog1.FileName = "Proveedores " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("TIPO DOCUMENTO");
            dt.Columns.Add("NRO DOCUMENTO");
            dt.Columns.Add("NOMBRE PROVEEDOR");
            dt.Columns.Add("RUBRO");
            dt.Columns.Add("DIRECCION");
            dt.Columns.Add("TELEFONO FIJO");
            dt.Columns.Add("TELEFONO MOVIL");
            dt.Columns.Add("CORREO");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataProveedor)
            {
                dt.Rows.Add(new object[] { p.idp, p.nomdoc, p.prov_nrdoc, p.prov_nom, p.prov_rubro, p.prov_direc, p.prov_telfijo, p.prov_telmovil, p.prov_correo, (p.prov_activo == 1) ? "Activo" : "Inactivo" });
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
            this.Proveedor = this.DataProveedor.Where(w => w.idp == (int)id).FirstOrDefault();
            Proveedor proveedor = this.Proveedor;
            this.Direccion = proveedor.prov_direc;
            this.Nombre = proveedor.prov_nom;
            this.Rubro = proveedor.prov_rubro;
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
        private void Nuevo()
        {
            this.Operacion = "Nuevo";
            this.Proveedor = new Proveedor();
        }
        private void Guardar()
        {
            Proveedor proveedor = this.Proveedor;
            var _id = proveedor.idp;
            string _mensaje = "";
            proveedor.prov_nom= Nombre;
            proveedor.prov_direc = Direccion;
            proveedor.prov_rubro = Rubro;
            bool result = negProv.SetProveedor((_id == 0 ? 1 : 2), proveedor, ref _mensaje);
            

            this.Operacion = "Lista";
            string OperacionInicial;
            if (System.Windows.Application.Current.Properties["OperacionxFactCompra"] != null) // Estas acciones son para cuando se abra el usercontrol desde otro user control segun la operacion que desea ver
            {
                OperacionInicial = System.Windows.Application.Current.Properties["OperacionxFactCompra"].ToString();
                if (OperacionInicial == "Nuevo")
                {
                    this.Operacion = "Lista";
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
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
            Proveedor proveedor = new Proveedor();
            proveedor.idp = (int)id;
            bool result = negProv.SetProveedor(3, proveedor, ref _mensaje);
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
            DataProveedor = negProv.GetProv();
        }
        private async void ServicioSunat(object numdoc)
        {
            //string nombrecompleto;
            Consulta = "false";
            ServiceReference1.sosfoodSoapClient vf = new ServiceReference1.sosfoodSoapClient();

            try
            {
                if (!string.IsNullOrWhiteSpace(numdoc.ToString()))
                {
                    if (proveedor.iddoc == "1")
                    {
                        this.Nombre = vf.ConsultaDNI(numdoc.ToString());
                        Consulta = "true";
                    }
                    else if (proveedor.iddoc == "2")
                    {
                        ServiceReference1.ClsClienteConsultaEN cn;
                        cn = vf.ConsultaRuc("0a682fbe-009d-4758-aad1-2ff1092ab7c2-838d6cd5-620a-4add-9632-a3b37c5ae216", numdoc.ToString());
                        this.Nombre = cn.nombre_o_razon_social;
                        this.Direccion = cn.direccion_completa;
                        Consulta = "true";
                    }
                    
                }
                else {
                    Consulta = "true";
                    return;
                }
                
                
            }
            catch (Exception ex)
            {
                Consulta = "true";
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

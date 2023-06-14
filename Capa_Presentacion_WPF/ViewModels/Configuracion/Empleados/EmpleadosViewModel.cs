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
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Data;
using ExportToExcel;
using Capa_Negocio.Funciones_Globales;

namespace Capa_Presentacion_WPF
{
    public class EmpleadosViewModel : IGeneric
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Descripcion, int Reserved);

        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Empleado negEmp = new Neg_Empleado();
        private Empleado empleado;
        private string operacion;
        //private Ent_Combo _TipoDocSelected;

        public ObservableCollection<Empleado> DataEmpleados { get; set; }
        public List<Ent_Combo> ComboTipoDI { get; set; }
        public List<Ent_Combo> ComboRolCargo { get; set; }

        #region BotonServicioSunat
        private string nombre;
        private string apellido;
        public string Nombre
        {
            get
            {
                return this.empleado == null ? "" : this.empleado.nombres;
            }
            set
            {
                if (value != null)
                {
                    this.empleado.nombres = value;
                }
                nombre = value;
            }
        }
        public string Apellido
        {
            get
            {
                return this.empleado == null ? "" : this.empleado.apellidos;
            }
            set
            {
                if (value != null)
                {
                    this.empleado.apellidos = value;
                }
                apellido = value;
            }
        }
        #endregion

        public Empleado Empleado
        {
            get => empleado;
            set
            {
                empleado = value;
                CheckedGenero();
            }
        }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand SunatCommand { get; set; }
        public ICommand CargarSunatCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
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
        public bool F { get; set; }
        public bool M { get; set; }
        public string TextoBuscar { get; set; }
        public EmpleadosViewModel()
        {

            this.Operacion = "Lista";
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.CargarSunatCommand = new ParamCommand(new Action<object>(ServicioSunat));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ComboTipoDI = negCombo.GetComboTipoDI().ToList();
            this.ComboRolCargo = negCombo.GetRolCargo();
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
            saveFileDialog1.FileName = "Empleados " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("DOCUMENTO");
            dt.Columns.Add("NOMBRES");
            dt.Columns.Add("APELLIDOS");
            dt.Columns.Add("CARGO");
            dt.Columns.Add("GENERO");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("FECHA NAC");
            foreach (var p in DataEmpleados)
            {
                dt.Rows.Add(new object[] { p.id, p.nroDocumento, p.nombres, p.apellidos, p.cargo, p.genero, p.estado, p.fecNacimiento});
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
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            saveFileDialog1.FileName = "Empleados " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("DOCUMENTO");
            dt.Columns.Add("NOMBRES");
            dt.Columns.Add("APELLIDOS");
            dt.Columns.Add("CARGO");
            dt.Columns.Add("GENERO");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("FECHA NAC");
            foreach (var p in DataEmpleados)
            {
                dt.Rows.Add(new object[] { p.id, p.nroDocumento, p.nombres, p.apellidos, p.cargo, p.genero, (p.estado == 1)?"Activo":"Inactivo", p.fecNacimiento });
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
        
        private void Buscar()
        {
            ObservableCollection<Empleado> ls = new ObservableCollection<Empleado>();
            ls = DataEmpleados = negEmp.GetEmpleados();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataEmpleados = negEmp.GetEmpleados();
            }
            else
            {
                List<Empleado> lista = ls
                    .Where(w => 
                    w.nombres.ToUpper().Contains(TextoBuscar.ToUpper()) || 
                    w.apellidos.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    (w.nombres + " " + w.apellidos).ToUpper().Contains(TextoBuscar.ToUpper())
                    ).ToList();
                ObservableCollection<Empleado> o = new ObservableCollection<Empleado>(lista);
                DataEmpleados = o;
            }
        }
        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.Empleado = this.DataEmpleados.Where(w => w.id == (int)id).FirstOrDefault();
            this.Apellido = Empleado.apellidos;
            this.Nombre = Empleado.nombres;
        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            this.Operacion = "Nuevo";
            this.Empleado = new Empleado();
        }
        private void CheckedGenero()
        {
            if (this.Empleado != null)
            {
                this.F = this.Empleado.genero == "F";
                this.M = this.Empleado.genero == "M";
            }
        }
        private async void Guardar()
        {
            if (Empleado.idcargo!=null && Empleado.idTipoDI != null && !string.IsNullOrWhiteSpace(Empleado.nombres))
            {
                Empleado empleado = this.Empleado;
                empleado.genero = this.F ? "F" : "M";
                var _id = empleado.id;
                string _mensaje = "";
                bool result = negEmp.SetEmpleados((_id == 0 ? 1 : 2), empleado, ref _mensaje);
                var view = new MessageDialog
                {
                    Mensaje = { Text = _mensaje }
                };
                await DialogHost.Show(view, "RootDialog");
                if (result)
                {
                    this.Operacion = "Lista";
                }
            }return;
            
        }
        private async void Eliminar(object id)
        {
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "Desea eliminar el registro " + id + "?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
                return;
            string _mensaje = "";
            Empleado empleado = new Empleado();
            empleado.id = (int)id;
            bool result = negEmp.SetEmpleados(3, empleado, ref _mensaje);
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
            DataEmpleados = negEmp.GetEmpleados();
        }

        private async void ServicioSunat(object numdoc)
        {
            //string nombrecompleto;

            bool internet = negFuncionGlobal.ValidarInternet();
            if (internet == false)
            {
                string Estado = "Estimado Cliente:\n" +
                                "actualmente usted no cuenta con conexión a internet por favor registre manualmente los datos de su cliente.";
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", Estado, 2);
                return;
            }
            ServiceReference1.sosfoodSoapClient vf = new ServiceReference1.sosfoodSoapClient();
            string nombrecompleto;
            try
            {
                if (!string.IsNullOrWhiteSpace(numdoc.ToString()))
                {
                    if (empleado.idTipoDI == "1")
                    {
                        nombrecompleto = vf.ConsultaDNI(numdoc.ToString());

                        string[] partes = nombrecompleto.Split(' ');
                        string result = partes[0] + ' ' + partes[1];
                        string result1 = partes[2] + ' ' + partes[3];
                        this.Apellido = result1;
                        this.Nombre = result;
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

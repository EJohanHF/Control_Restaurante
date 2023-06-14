using Capa_Negocio.Configuracion;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio;
using Capa_Negocio.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using Capa_Entidades.Models;
using Capa_Negocio.Principal;
using System.Windows;
using Capa_Entidades;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Capa_Negocio.Funciones_Globales;
using ExportToExcel;
using Menu = Capa_Entidades.Models.Configuracion.Menu;
using MenuItem = Capa_Entidades.Models.MenuItem;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Role
{
    public class RolesViewModel : IGeneric
    {
        static List<Ent_Combo> checkItem = new List<Ent_Combo>();
        MenuStructureTotal directoryStructure = new MenuStructureTotal();
        Neg_Roles negRoles = new Neg_Roles();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        private Roles roles;
        private string operacion;
        public ObservableCollection<Roles> DataRoles { get; set; }
        static List<MenuItem> menuItems = new List<Capa_Entidades.Models.MenuItem>();
        public ObservableCollection<MenuItemViewModelTotal> Items { get; set; }
        public List<Menu> Menu { get; set; }
        public int id_rol;
        public Roles Roles
        {
            get => roles;
            set
            {
                roles = value;
            }
        }
        public string TextoBuscar { get; set; }
        private string prueba { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand TraercheckCommand { get; set; }

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
        public bool _idMenu;
        public bool IdMenu
        {
            get { return _idMenu; }
            set
            {
                if (value == true)

                    _idMenu = value;
                return;
            }
        }
        public RolesViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.TraercheckCommand = new ParamCommand(new Action<object>(traercheck));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            var children = directoryStructure.GetLogicalDrives();
            this.Items = new ObservableCollection<MenuItemViewModelTotal>(
               children.Select(drive => new MenuItemViewModelTotal(drive.idPadre, drive.id, (bool)drive.value, 0)));
            this.Menu = new List<Menu>();
        }

        private void Buscar()
        {
            ObservableCollection<Roles> ls = new ObservableCollection<Roles>();
            ls = DataRoles = negRoles.GetRoles();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataRoles = negRoles.GetRoles();
            }
            else
            {
                List<Roles> lista = ls
                    .Where(w =>
                    w.nomrol.ToUpper().Contains(TextoBuscar.ToUpper())).ToList();
                ObservableCollection<Roles> o = new ObservableCollection<Roles>(lista);
                DataRoles = o;
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
            saveFileDialog1.FileName = "Roles " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE ROL");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataRoles)
            {
                dt.Rows.Add(new object[] { p.idrol, p.nomrol, (p.estadorol == 1) ? "Activo" : "Inactivo"});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE ROLES");
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
            saveFileDialog1.FileName = "Roles " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE ROL");
            dt.Columns.Add("ESTADO");
            foreach (var p in DataRoles)
            {
                dt.Rows.Add(new object[] { p.idrol, p.nomrol, (p.estadorol == 1) ? "Activo" : "Inactivo" });
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
        private void GetLista()
        {
            DataRoles = negRoles.GetRoles();
        }
        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.Roles = this.DataRoles.Where(w => w.idrol == (int)id).FirstOrDefault();

            var children = directoryStructure.GetLogicalDrives();

            foreach (var menu in children)
            {
                menu.value = false;
            }

            this.Items = new ObservableCollection<MenuItemViewModelTotal>(
               children.Select(drive => new MenuItemViewModelTotal(drive.idPadre, drive.id, (bool)drive.value, (int)id)));
            string _mensaje = "";
            this.Menu = negRoles.getMenuRol(id.ToString(), ref _mensaje);
            foreach (var menu in children)
            {
                Boolean yapaso = false;
                foreach (var menuBD in this.Menu)
                {
                    if (menu.id == menuBD.idmenu && yapaso == false)
                    {
                        menu.value = true;
                        yapaso = true;
                    }
                }
            }
            this.Items = new ObservableCollection<MenuItemViewModelTotal>(
               children.Select(drive => new MenuItemViewModelTotal(drive.idPadre, drive.id, (bool)drive.value, (int)id)));
            //foreach (var padre in children)
            //{
            //    var idPadre = padre.idPadre;
            //    var contador = Items.Where(i => i.idPadre == idPadre).Count();
            //    var contadorTodo = Items.Where(i => i.idPadre == idPadre && i.value == true).Count();
            //    if (contador == contadorTodo)
            //    {
            //        padre.value = true;
            //    }
            //}
            //this.Items = new ObservableCollection<MenuItemViewModelTotal>(
            //   children.Select(drive => new MenuItemViewModelTotal(drive.idPadre, drive.id, (bool)drive.value, (int)id)));
        }
        private void Nuevo()
        {
            this.Operacion = "Nuevo";
            this.Roles = new Roles();
            var children = directoryStructure.GetLogicalDrives();
            this.Items = new ObservableCollection<MenuItemViewModelTotal>(
               children.Select(drive => new MenuItemViewModelTotal(drive.idPadre, drive.id, (bool)drive.value, 0)));
        }
        private void traercheck(object id)
        {
            if (this.Menu.Where(w => w.idmenu == (int)id).FirstOrDefault() == null)
            {
                this.Menu.Add(new Menu() { idmenu = (int)id });
            }
            else
            {
                this.Menu.Remove(this.Menu.Where(w => w.idmenu == (int)id).FirstOrDefault());
            }
        }
        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(Roles.nomrol))
            {
                Roles roles = this.Roles;

                var _id = this.roles.idrol;
                roles.menu = (this.Menu == null ? "" : String.Join(",", this.Menu.Select(s => s.idmenu).ToArray()));
                string _mensaje = "";
                bool result = negRoles.SetRoles((_id == 0 ? 1 : 2), roles, ref _mensaje);
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
            return;
        }
        private async void Eliminar(object id)
        {
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "Desea Desactivar el rol ?" + Environment.NewLine + "Al hacer esta accion desactivara todos los usuarios que dependen de este rol." }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
                return;
            string _mensaje = "";
            Roles usuario = new Roles();
            usuario.idrol = (int)id;
            bool result = negRoles.SetRoles(3, roles, ref _mensaje);
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
    }
}
using Capa_Entidades;
using Capa_Entidades.Acceso;
using Capa_Entidades.Models.Usuario;
using Capa_Negocio;
using Capa_Negocio.Acceso;
using Capa_Negocio.User;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.User
{
    public class UsuarioViewModel : IGeneric
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Usuario negUser = new Neg_Usuario();
        Neg_Combo negCombo = new Neg_Combo();
        private Usuario usuario;
        private string operacion;
        private string usuarioname;
        private string mensajeusu;
        public ObservableCollection<Usuario> DataUsuario { get; set; }
        public List<Ent_Combo> ComboEmpleado { get; set; }
        public List<Ent_Combo> ComboRoles { get; set; }
        public Usuario Usuario
        {
            get => usuario;
            set
            {
                usuario = value;
            }
        }
        public string UsuarioName
        {
            get => usuarioname;
            set
            {

                usuarioname = value;
            }
        }

        public ICommand EditPassCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand NuevoCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand CerrarDialog { get; set; }
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
        public string Mensajeuser
        {
            get => mensajeusu;
            set
            {
                mensajeusu = value;
            }
        }
        public string TextoBuscar { get; set; }
        public UsuarioViewModel()
        {
            this.Operacion = "Lista";
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.NuevoCommand = new RelayCommand(new Action(Nuevo));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.EditPassCommand = new RelayCommand(new Action(EditarPass));

            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.ComboEmpleado = negCombo.GetEmpleado();
            this.ComboRoles = negCombo.GetRoles();
            this.Usuario = new Usuario();
            this.UsuarioName = Convert.ToString(System.Windows.Application.Current.Properties["NomUsuario"]);
            this.CerrarDialog = new RelayCommand(new Action(CerrarDialogo));
        }


        private void Buscar()
        {
            ObservableCollection<Usuario> ls = new ObservableCollection<Usuario>();
            ls = DataUsuario = negUser.GetUsuario();
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DataUsuario = negUser.GetUsuario();
            }
            else
            {
                List<Usuario> lista = ls
                    .Where(w =>
                    w.nomusu.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.nomemple.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    (w.nomusu + " " + w.apeusu).ToUpper().Contains(TextoBuscar.ToUpper())
                    ).ToList();
                ObservableCollection<Usuario> o = new ObservableCollection<Usuario>(lista);
                DataUsuario = o;
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
            saveFileDialog1.FileName = "Usuarios " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE USUARIO");
            dt.Columns.Add("APELLIDO USUARIO");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("NOMBRE EMPLEADO");
            foreach (var p in DataUsuario)
            {
                dt.Rows.Add(new object[] { p.idusu, p.nomusu, p.apeusu, (p.estadousu == 1) ? "Activo" : "Inactivo", p.nomemple});
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
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE USUARIOS");
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
            saveFileDialog1.FileName = "Usuarios " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE USUARIO");
            dt.Columns.Add("APELLIDO USUARIO");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("NOMBRE EMPLEADO");
            foreach (var p in DataUsuario)
            {
                dt.Rows.Add(new object[] { p.idusu, p.nomusu, p.apeusu, (p.estadousu == 1) ? "Activo" : "Inactivo", p.nomemple });
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
            this.Usuario = this.DataUsuario.Where(w => w.idusu == (int)id).FirstOrDefault();

        }
        private void Nuevo()
        {
            this.Operacion = "Nuevo";
            this.Usuario = new Usuario();
        }
        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void CerrarDialogo()
        {
            System.Windows.Application.Current.Properties["CambioContraseña"] = false;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private async void Guardar()
        {
            if (Usuario.idemple != null && !string.IsNullOrWhiteSpace(Usuario.nomusu) && !string.IsNullOrWhiteSpace(Usuario.claveusu_cambio) && Usuario.idrol != null)
            {
                Usuario usuario = this.Usuario;
                usuario.claveusu_cambio = BCrypt.Net.BCrypt.HashPassword(usuario.claveusu_cambio, BCrypt.Net.BCrypt.GenerateSalt());
                var _id = this.usuario.idusu;
                string _mensaje = "";
                bool result = negUser.SetUsuario((_id == 0 ? 1 : 2), usuario, ref _mensaje);
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
                Mensaje = { Text = "Desea eliminar el registro ?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
                return;
            string _mensaje = "";
            Usuario usuario = new Usuario();
            usuario.idusu = (int)id;
            bool result = negUser.SetUsuario(3, usuario, ref _mensaje);
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
            DataUsuario = negUser.GetUsuario();
        }
        public bool result;
        public bool result2;
        private void EditarPass()
        {
            if (!string.IsNullOrWhiteSpace(Usuario.claveusu))
            {
                Neg_Login neg_log = new Neg_Login();
                int id = 0;
                id = Convert.ToInt32(System.Windows.Application.Current.Properties["IdUsuario"].ToString());
                string mensaje = "";

                Ent_Usuario ent_usu = neg_log.login(id, Usuario.claveusu, ref mensaje);
                Boolean comparar = new Boolean();
                comparar = BCrypt.Net.BCrypt.Verify(Usuario.claveusu, ent_usu.usu_pass);

                if (comparar == true)
                {
                    if (!string.IsNullOrWhiteSpace(Usuario.claveusu_cambio))
                    {
                        string _mensaje = "";
                        this.Usuario.idusu = Convert.ToInt32(id);
                        this.Usuario.claveusu_cambio = BCrypt.Net.BCrypt.HashPassword(Usuario.claveusu_cambio, BCrypt.Net.BCrypt.GenerateSalt());
                        result = negUser.SetUsuarioUpdEstado(2, this.Usuario, ref _mensaje);
                        //ToastNotificationWindow toast = new ToastNotificationWindow();
                        //toast.Show();
                        //result = false;     
                    }
                    else this.Mensajeuser = "Ingrese nueva contraseña";
                }
                else this.Mensajeuser = "Contraseña inválido";

                if (result == true)
                {
                    System.Windows.Application.Current.Properties["CambioContraseña"] = true;
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
            else this.Mensajeuser = "Insgrese Contraseñas ";
        }
    }
}

using Capa_Entidades;
using Capa_Entidades.Acceso;
using Capa_Entidades.Models.Usuario;
using Capa_Negocio;
using Capa_Negocio.Acceso;
using Capa_Negocio.Funciones_Globales;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogClaveEmpleadoViewModel
    {
        private Usuario usuario;
        public Usuario Usuario
        {
            get => usuario;
            set
            {
                usuario = value;
            }
        }
        Neg_Combo negCombo = new Neg_Combo();
        public List<Ent_Combo> ComboEmpleado { get; set; }
        public string idempl { get; set; }
        public ICommand Pedir { get; set; }
        public ICommand CerrarDialog { get; set; }
        public DialogClaveEmpleadoViewModel()
        {
            this.ComboEmpleado = negCombo.GetEmpleado();
            this.Pedir = new ParamCommand(new Action<object>(PedirMozo));
            this.Usuario = new Usuario();
            this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
            this.idempl = this.ComboEmpleado.ToList().FirstOrDefault().id.ToString();
        }
        private void CloseDialog()
        {
            Application.Current.Properties["IdEmpleado"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public async void PedirMozo(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            Usuario.claveusu = password;
            if (!string.IsNullOrWhiteSpace(Usuario.claveusu))
            {
                Neg_Login neg_log = new Neg_Login();
                var _id = Convert.ToInt32(idempl);
                //id = Convert.ToInt32(System.Windows.Application.Current.Properties["IdUsuario"].ToString());
                string mensaje = "";
                Ent_Usuario ent_usu = new Ent_Usuario();
                ent_usu = neg_log.VerificaEmpleado(Usuario.claveusu, ref mensaje);
                if (ent_usu == null)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Contraseña incorrecta", 2);
                    Application.Current.Properties["IdEmpleado"] = null;
                    return;
                }
                Boolean comparar = new Boolean();
                comparar = false;
                if (Usuario.claveusu == ent_usu.usu_pass)
                {
                    comparar = true;
                }
                else
                {
                    comparar = false;
                }
                //comparar = BCrypt.Net.BCrypt.Verify(Usuario.claveusu, ent_usu.usu_pass);
                if (comparar == true)
                {
                    Application.Current.Properties["IdEmpleado"] = ent_usu.id;
                    int id_empleado_anterior_pedido = Convert.ToInt32(Application.Current.Properties["id_empleado_anterior"]);

                    if (Application.Current.Properties["id_empleado_anterior"] != null) {
                        if (id_empleado_anterior_pedido != Convert.ToInt32(Application.Current.Properties["IdEmpleado"]))
                        {
                            negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Esta mesa esta siendo atendida por " + Application.Current.Properties["nombre_empleado_anterior"].ToString(), 2);
                            Application.Current.Properties["IdEmpleado"] = null;
                            Application.Current.Properties["id_empleado_anterior"] = null;
                        }
                    }
                    

                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Contraseña incorrecta", 2);
                    Application.Current.Properties["IdEmpleado"] = null;
                    Application.Current.Properties["id_empleado_anterior"] = null;
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese contraseña", 2);
                Application.Current.Properties["IdEmpleado"] = null;
                Application.Current.Properties["id_empleado_anterior"] = null;
            }
        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
    }
}

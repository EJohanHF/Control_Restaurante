using Capa_Entidades;
using Capa_Entidades.Acceso;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Usuario;
using Capa_Negocio;
using Capa_Negocio.Acceso;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Pedido;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Negocio.User;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogCambiarPrecioViewModel : IGeneric
    {
        public ICommand GuardarDescuentoPedido { get; set; }
        public ICommand RbtTagCommand { get; set; }
        public List<Ent_Combo> ComboUsuario { get; set; }
        Neg_Pedido negPedido = new Neg_Pedido();
        Neg_Usuario negUser = new Neg_Usuario();
        Neg_Pagar negPagar = new Neg_Pagar();
        private Usuario usuario;
        private Descuento descuento;
        private string rutaImagen { get; set; }
        public string rutaImagenBotones { get; set; }
        Funcion_Global global = new Funcion_Global();
        public decimal SubTotal { get; set; }
        public decimal MontoActual { get; set; }
        public decimal NuevoMonto { get; set; }
        public List<Descuento> ComboTipDesc { get; set; }
        public string chkMonto { get; set; }
        public string chkPorcentaje { get; set; }
        public string IdCheck { get; set; }
        public Descuento _IdDescuento { get; set; }
        public Descuento IdDescuento
        {
            get => _IdDescuento;
            set
            {
                if (IdCheck == "2")
                {
                    var _id_tip_desc = value.id;
                    var porcentaje = this.ComboTipDesc.Where(d => d.id == Convert.ToInt32(_id_tip_desc)).FirstOrDefault().TD_PORCENTAJE;
                    decimal MontPorcen = this.MontoActual * (Convert.ToDecimal(porcentaje) / 100);
                    this.NuevoMonto = Math.Round(this.MontoActual - MontPorcen, 2);
                }
                _IdDescuento = value;
            }
        }
        public Usuario Usuario
        {
            get => usuario;
            set
            {
                usuario = value;
            }
        }
        public Descuento Descuento
        {
            get => descuento;
            set
            {
                descuento = value;
            }
        }
        Neg_Combo negCombo = new Neg_Combo();
        private string operacion_detPago;
        public string Operacion_detPago
        {
            get => operacion_detPago;
            set
            {
                operacion_detPago = value;
            }
        }

        public DialogCambiarPrecioViewModel()
        {
            this.ComboUsuario = negCombo.GetUsuarios();
            valorbt();
            IdCheck = "0";
            if (Application.Current.Properties["OperacionDetPago"] != null)
            {
                this.Operacion_detPago = Application.Current.Properties["OperacionDetPago"].ToString();
            }
            else
            {
                this.Operacion_detPago = "CAMBIAR PRECIO TOTAL";
            }
            if (this.Operacion_detPago == "CAMBIAR PRECIO TOTAL")
            {
                if (Application.Current.Properties["TotalPedido"] != null)
                {
                    this.MontoActual = Convert.ToDecimal(Application.Current.Properties["TotalPedido"]);
                }
                else
                {
                    this.MontoActual = 0;
                }
            }
            else
            {
                if (Application.Current.Properties["PrePlato"] != null)
                {
                    this.MontoActual = Convert.ToDecimal(Application.Current.Properties["PrePlato"]);
                }
                else
                {
                    this.MontoActual = 0;
                }
            }

            if (Application.Current.Properties["SubTotalPedido"] != null)
            {
                this.SubTotal = Convert.ToDecimal(Application.Current.Properties["SubTotalPedido"]);
            }
            else
            {
                this.SubTotal = 0;
            }
            //this.GuardarDescuentoPedido = new RelayCommand(new Action(GuardarDescPedido));
            this.GuardarDescuentoPedido = new ParamCommand(new Action<object>(GuardarDescPedido));
            this.NuevoMonto = 0;
            this.Usuario = new Usuario();
            this.ComboTipDesc = negCombo.GetTipoDescuento();
            this.Descuento = new Descuento();
            RbtTagCommand = new ParamCommand(new Action<object>(IdRadiobuton));
        }
        private void IdRadiobuton(object id)
        {
            if (id.ToString() == "1")
            {
                this.NuevoMonto = 0;
                IdCheck = id.ToString();
            }
            else if(id.ToString() == "2")
            {
                var _id_tip_desc = Descuento.id;
                var porcentaje = this.ComboTipDesc.Where(d => d.id == Convert.ToInt32(_id_tip_desc)).FirstOrDefault().TD_PORCENTAJE;
                decimal MontPorcen = this.MontoActual * (Convert.ToDecimal(porcentaje) / 100);
                this.NuevoMonto = Math.Round(this.MontoActual - MontPorcen,2);
                IdCheck = id.ToString();
            }
        }
        void valorbt()
        {
            chkMonto = "1";
            chkPorcentaje = "2";
        }
        private void GuardarDescPedido(object parameter)
        {
            if (this.NuevoMonto < 0) {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "El monto no puede ser menor a 0", 2);
                return;
            }
            if (this.Operacion_detPago == "CAMBIAR PRECIO TOTAL")
            {
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;
                Usuario.claveusu = password;
                if (!string.IsNullOrWhiteSpace(Usuario.claveusu))
                {
                    Neg_Login neg_log = new Neg_Login();
                    var _id = Usuario.idusu;
                    //id = Convert.ToInt32(System.Windows.Application.Current.Properties["IdUsuario"].ToString());
                    string mensaje = "";
                    var _id_tip_desc = Descuento.id;
                    Ent_Usuario ent_usu = neg_log.login(_id, Usuario.claveusu, ref mensaje);
                    Boolean comparar = new Boolean();
                    comparar = BCrypt.Net.BCrypt.Verify(Usuario.claveusu, ent_usu.usu_pass);

                    if (comparar == true)
                    {
                        if (this.MontoActual >= this.NuevoMonto)
                        {
                            int idPedido = 0;
                            string _mensaje = "";
                            idPedido = Convert.ToInt32(Application.Current.Properties["Id_Pedido"]);
                            bool result = false;
                            //bool result2 = negPagar.QuitarDescuento(idPedido);
                            result = negPedido.DescuentoPedido(idPedido, this.NuevoMonto, _id_tip_desc, ref _mensaje);
                            if (result == true)
                            {
                                DialogHost.CloseDialogCommand.Execute(null, null);
                                Application.Current.Properties["DescuentoCorrecto"] = "1";
                            }
                            else
                            {
                                Application.Current.Properties["DescuentoCorrecto"] = null;
                            }
                        }
                        else
                        {
                            negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "El monto no puede ser mayor al subtotal", 2);
                        }
                    }
                    else
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Contraseña incorrecta", 2);
                        Application.Current.Properties["DescuentoCorrecto"] = null;
                    }
                }
            }
            else
            {
                int idDetPed = Convert.ToInt32(Application.Current.Properties["IdDetPedido"]);
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;
                Usuario.claveusu = password;
                if (!string.IsNullOrWhiteSpace(Usuario.claveusu))
                {
                    Neg_Login neg_log = new Neg_Login();
                    var _id = Usuario.idusu;
                    //id = Convert.ToInt32(System.Windows.Application.Current.Properties["IdUsuario"].ToString());
                    string mensaje = "";
                    var _id_tip_desc = Descuento.id;
                    Ent_Usuario ent_usu = neg_log.login(_id, Usuario.claveusu, ref mensaje);
                    Boolean comparar = new Boolean();
                    comparar = BCrypt.Net.BCrypt.Verify(Usuario.claveusu, ent_usu.usu_pass);

                    if (comparar == true)
                    {
                        decimal importe = Convert.ToDecimal(Application.Current.Properties["PrePlato"]);
                        if (importe >= this.NuevoMonto)
                        {
                            string _mensaje = "";
                            bool result = false;
                            result = negPedido.DescuentoPlato(idDetPed, this.NuevoMonto, _id_tip_desc, ref _mensaje);
                            if (result == true)
                            {
                                DialogHost.CloseDialogCommand.Execute(null, null);
                                Application.Current.Properties["DescuentoCorrecto"] = "1";
                            }
                            else
                            {
                                Application.Current.Properties["DescuentoCorrecto"] = null;
                            }
                        }
                        else
                        {
                            //negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "El monto no puede ser mayor al importe del plato", 2);
                            string _mensaje = "";
                            bool result = false;
                            result = negPedido.DescuentoPlato(idDetPed, this.NuevoMonto, _id_tip_desc, ref _mensaje);
                            if (result == true)
                            {
                                DialogHost.CloseDialogCommand.Execute(null, null);
                                Application.Current.Properties["DescuentoCorrecto"] = "1";
                            }
                            else
                            {
                                Application.Current.Properties["DescuentoCorrecto"] = null;
                            }
                        }
                    }
                    else
                    {
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Contraseña incorrecta", 2);
                        Application.Current.Properties["DescuentoCorrecto"] = null;
                    }
                }
            }

        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
    }
}

using Capa_Entidades;
using Capa_Entidades.Acceso;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Entidades.Models.Usuario;
using Capa_Negocio;
using Capa_Negocio.Acceso;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Pedido;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Presentacion_WPF.Views.Dialogs;
using Capa_Presentacion_WPF.Views.Dialogs.DialogVentaporDia;
using Capa_Presentacion_WPF.Views.Dialogs.Pedidos;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using tickets;
using static Capa_Entidades.Models.Reportes.DetalleporDia.Detalle_Pedido;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.ReporteporDias
{
    class VentasDiaDetPedViewModel : IGeneric
    {
        public Detalle_Pedido SelectedDataFile { get; set; }
        public ObservableCollection<Detalle_Pedido> DataDetprod { get; set; }
        Neg_Pedido negPedido = new Neg_Pedido();
        Neg_Combo negCombo = new Neg_Combo();
        Funcion_Global globales = new Funcion_Global();
        Neg_Detalle_pedido negDetVent = new Neg_Detalle_pedido();
        public List<Ent_Combo> ComboUsuario { get; set; }
        //public List<Detalle_Pedido> CantEliminar { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand ObtenerNombreCommand { get; set; }
        public ICommand EliminaPlatoPedidoCommand { get; set; }
        public List<llenarCantidad> ListaEliminar { get; set; }
        private Usuario usuario;
        private cantidad cantidades;
        private Detalle_Pedido detallepedido;
        public int IDCantidad { get; set; }
        public string ObtenerComentario { get; set; }
        private string operacion;
        private cantidad cbocantidad { get; set; }
        public cantidad CboCantidad
        {
            get { return cbocantidad; }
            set
            {
                cantidad v = this.cantidad;
                if (value != null)
                {
                    this.IDCantidad = (((cantidad)value).id);
                    if (ListaEliminar.Count>=1 && ListaEliminar.Where(w=> w.idDetPed== Convert.ToInt32(IdDetPEd)).Count()>=1)
                    {
                        var itemToRemove = ListaEliminar.SingleOrDefault(w => w.idDetPed == Convert.ToInt32(IdDetPEd));
                        if (itemToRemove != null)
                            ListaEliminar.Remove(itemToRemove);
                        ListaEliminar.Add(new llenarCantidad
                        {
                            idDetPed = Convert.ToInt32(IdDetPEd),
                            Elim = IDCantidad
                        });

                    }
                    else
                    {
                        ListaEliminar.Add(new llenarCantidad
                        {
                            idDetPed = Convert.ToInt32(IdDetPEd),
                            Elim = IDCantidad
                        });
                    }
                }
                cbocantidad = value;
            }
        }
        public object IDPlato { get; set; }
        public object IDDEPROD { get; set; }
        private int cantidadElimin;
        public object IdDetPEd { get; set; }
     
        public int CantidadElimin
        {
            get => cantidadElimin;
            set
            {
                cantidadElimin = value;
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

        public cantidad cantidad
        {
            get => cantidades;
            set
            {
                cantidades = value;
            }   
        }
        
        public Detalle_Pedido Detalle_Pedido
        {
            get => detallepedido;
            set
            {
                detallepedido = value;
            }
        }
        private ICommand m_RowClickCommand;
        public ICommand RowClickCommand
        {
            get
            {
                return m_RowClickCommand;
            }
        }
        public class DelegateCommand : ICommand
        {
            private Action m_Action;
            public DelegateCommand(Action action)
            {
                this.m_Action = action;
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                m_Action();
            }
        }
        public VentasDiaDetPedViewModel()
        {
            this.ListaEliminar = new List<llenarCantidad>();
            this.cantidad = new cantidad();
            this.llenarCantidads = new llenarCantidad();
            this.Operacion = "Lista";
            DataDetprod = (ObservableCollection<Detalle_Pedido>)Application.Current.Properties["detPEdido"];
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.EliminarCommand = new ParamCommand(new Action<object>(Eliminar));
            this.EliminaPlatoPedidoCommand = new ParamCommand(new Action<object>(EliminarPlatos));
            this.ObtenerNombreCommand = new ParamCommand(new Action<object>(comentario));
            this.Usuario = new Usuario();
            this.ComboUsuario = negCombo.GetUsuarios();
            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = this.SelectedDataFile;
                if (set != null)
                {
                    this.IdDetPEd = set.DProd_id_Det_ped; 
                }

            });

        }
        private void comentario(object com)
        {
            ObtenerComentario = com.ToString();
        }
        public string Operacion
        {
            get => operacion;
            set
            {
                if (value== "Anular Pedido")
                {
                    textRB();
                    
                }
                operacion = value;
            }
        }
        private void Eliminar(object id)
        {
            if (this.DataDetprod.Count == 1 && this.DataDetprod.Where(w => w.DProd_id_Det_ped == id).FirstOrDefault().DProd_cant == 1)
            {
                globales.Mensaje("SOS-FOOD - Informacion", " No es posible eliminar un plato, ELIMINE EL PEDIDO !!!", 2);
            }
            else
            {
                if (this.DataDetprod.Count > 1 && this.DataDetprod.Where(w => w.DProd_id_Det_ped == id).FirstOrDefault().DProd_cant == 1)
                {
                    //int cantanulado = this.DataDetprod.Where(w => w.DProd_id_Det_ped == id).FirstOrDefault().cant.Select(s => s.ids).FirstOrDefault();
                    Application.Current.Properties["IdDetPedido"] = id;
                    this.Operacion = "Anular Pedido";
                }
                else
                {
                    if (this.DataDetprod.Count == 1 && this.DataDetprod.Where(w => w.DProd_id_Det_ped == id).FirstOrDefault().DProd_cant == this.ListaEliminar.Where(w => w.idDetPed == Convert.ToInt32(IdDetPEd)).FirstOrDefault().Elim)
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", " No es posible eliminar, seleccione una cantidad menor al total !!!", 2);
                    }
                    else if (this.ListaEliminar.Where(w => w.idDetPed == Convert.ToInt32(IdDetPEd)).Count() != 0)
                    {
                        Application.Current.Properties["IdDetPedido"] = id;
                        this.Operacion = "Anular Pedido";
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", " Seleccione una cantidad a eliminar !!!", 2);
                    }
                } 
            }

        }
        private void EliminarPlatos(object parameter)
        {
            if (parameter.ToString() != null)
            {
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password;
                Usuario.claveusu = password;
                if (!string.IsNullOrWhiteSpace(Usuario.claveusu) && Usuario.idusu >= 0)
                {
                    Neg_Login neg_log = new Neg_Login();
                    var _id = Usuario.idusu;
                    string mensaje = "";

                    Ent_Usuario ent_usu = neg_log.login(_id, Usuario.claveusu, ref mensaje);
                    Boolean comparar = new Boolean();
                    comparar = BCrypt.Net.BCrypt.Verify(Usuario.claveusu, ent_usu.usu_pass);

                    if (comparar == true)
                    {
                        this.IDPlato = Convert.ToInt32(Application.Current.Properties["IdDetPedido"]);
                        if (this.DataDetprod.Where(w => Convert.ToInt32(w.DProd_id_Det_ped) == Convert.ToInt32(IDPlato)).FirstOrDefault() != null)
                        {
                            
                            int idPed = this.DataDetprod.Where(w => Convert.ToInt32(w.DProd_id_Det_ped) == Convert.ToInt32(IDPlato)).FirstOrDefault().DProd_id_ped;

                            bool result = false;
                            string _mensaje = "";
                            if (ObtenerComentario == null)
                            {
                                ObtenerComentario = "Error Digitacion";
                            }
                            int cantanulado = this.ListaEliminar.Where(w => w.idDetPed == Convert.ToInt32(IdDetPEd)).Select(s => s.Elim).FirstOrDefault();
                            Usuario usu = this.Usuario;
                            var idusuarioanulacion = usu.idusu;
                            if (this.DataDetprod.Where(w => Convert.ToInt32(w.DProd_id_Det_ped) == Convert.ToInt32(IDPlato)).FirstOrDefault().DProd_cant ==1)
                            {
                                result = negPedido.EliminarPlatoPedido(Convert.ToInt32(IDPlato), idPed, ObtenerComentario, ref _mensaje, idusuarioanulacion);
                            }
                            else
                            {
                                result = negPedido.EliminarPlatoPedidoCantidad(Convert.ToInt32(IDPlato), idPed, ObtenerComentario, Convert.ToDecimal(cantanulado), ref _mensaje, idusuarioanulacion);
                            }
                            

                            if (result == true)
                            {

                                ImprimirComandaPlatoAnulada(Convert.ToInt32(IDPlato));
                                ImpComandaPlatoAnuladoCaja(Convert.ToInt32(IDPlato));
                                this.DataDetprod = negDetVent.GetDetProducto(Convert.ToString(idPed));
                                this.Operacion = "Lista";
                            }
                        }
                        Application.Current.Properties["AnularPedido"] = null;
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "contrasenia incorrecta", 2);
                    }
                }
            }
        }
        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void ImpComandaPlatoAnuladoCaja(int id_det_ped)
        {

            if (id_det_ped == 0)
            {
                /*aqui va el snackbar*/
                return;
            }
            else
            {
                DataTable dt = new DataTable();
                dt = negDetVent.GetComandaPlatoAnuladaPedido(id_det_ped);
                Ticket ticket = new Ticket();

                ticket.AddHeaderLine_2("PEDIDO ANULADO N°: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                ticket.AddHeaderLine("");

                ticket.AddSubHeaderLine("Atendido Por: " + dt.Rows[0]["EMPL_NOM"].ToString());
                ticket.AddSubHeaderLine("Mesa: " + dt.Rows[0]["M_NOM"].ToString());
                ticket.AddSubHeaderLine("");

                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    ticket.AddItemComanda(dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString(), dt.Rows[j]["DP_DESCRIP"].ToString());
                }

                ticket.AddFooterLine("");

                ticket.AddFooterLine("");

                if (ticket.PrinterExists(globales.ImpCaja()) != true)
                {
                    globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + globales.ImpCaja() + " no está disponible.", 2);
                }
                else
                {
                    ticket.PrintTicket(globales.ImpCaja());
                }
            }


        }
        private void ImprimirComandaPlatoAnulada(int cod_det_ped)
        {
            DataTable impresora = new DataTable();
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            impresora = negPedido.GetImpresoraxIdDetPedido(cod_det_ped, Convert.ToInt32(Modulo));
            for (int i = 0; i < impresora.Rows.Count; i++)
            {
                DataTable pedido = new DataTable();
                pedido = negPedido.GET_DETALLE_X_PLATO_ANULADA(cod_det_ped, Convert.ToInt32(impresora.Rows[i]["IDIMP"].ToString()));
                string cliente = "";
                string descripcion = "";
                string cod_diario = "";
                string nombre_mesa = "";
                string nombre_empleado = "";
                Ticket ticket = new Ticket();
                for (int j = 0; j < pedido.Rows.Count; j++)
                {
                    cliente = pedido.Rows[0]["C_NOM"].ToString().ToUpper();
                    descripcion = pedido.Rows[j]["DP_DESCRIP"].ToString().ToUpper();
                    cod_diario = pedido.Rows[j]["ped_num_diario"].ToString().ToUpper();
                    nombre_mesa = pedido.Rows[j]["M_NOM"].ToString().ToUpper();
                    nombre_empleado = pedido.Rows[j]["EMPL_NOM"].ToString().ToUpper();
                    ticket.AddItemComanda(pedido.Rows[j]["DP_CANT"].ToString().ToUpper(), descripcion);
                }
                ticket.AddHeaderLine_2("PEDIDO ANULADO Nº:" + cod_diario.ToUpper());
                ticket.AddHeaderLine("");
                ticket.AddSubHeaderLine("Atendido Por: " + nombre_empleado.ToUpper());
                ticket.AddSubHeaderLine("Mesa: " + nombre_mesa.ToUpper());
                ticket.AddSubHeaderLine("");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                if (ticket.PrinterExists(impresora.Rows[i]["NOMIMP"].ToString()))
                {
                    ticket.PrintTicket(impresora.Rows[i]["NOMIMP"].ToString());
                }
                else
                    globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + impresora.Rows[i]["NOMIMP"].ToString() + " no está disponible.", 2);
            }
        }
        //private void AnularPedido(object parameter)
        //{
        //    var passwordBox = parameter as PasswordBox;
        //    var password = passwordBox.Password;
        //    Usuario.claveusu = password;
        //    if (!string.IsNullOrWhiteSpace(Usuario.claveusu) && Usuario.idusu >= 0)
        //    {
        //        Neg_Login neg_log = new Neg_Login();
        //        var _id = Usuario.idusu;
        //        string mensaje = "";

        //        Ent_Usuario ent_usu = neg_log.login(_id, Usuario.claveusu, ref mensaje);
        //        Boolean comparar = new Boolean();
        //        comparar = BCrypt.Net.BCrypt.Verify(Usuario.claveusu, ent_usu.usu_pass);

        //        if (comparar == true)
        //        {
        //            MessageBox.Show("todo correcto");
        //        }
        //        else
        //        {
        //            globales.Mensaje("SOS-FOOD - Informacion", "contrasenia incorrecta", 2);


        //        }
        //    }

        //}
      
        #region Text RadioButtons
        private string comentariotxt;

        public string Rbtext1 { get; set; }
        public string Rbtext2 { get; set; }
        public string Rbtext3 { get; set; }
        public string Rbtext4 { get; set; }
        public string Rbtext5 { get; set; }
        public void textRB()
        {
            this.Rbtext1 = "Error Digitacion";
            this.Rbtext2 = "Mala Elaboracion";
            this.Rbtext3 = "Tardó el plato";
            this.Rbtext4 = "Cambio de producto";
            this.Rbtext5 = "Otros";

        }
        public string ComentarioTXT
        {
            get => comentariotxt;
            set
            {
                if (value !=null)
                {
                    if (ObtenerComentario == "Otros")
                    {
                        ObtenerComentario = value;
                    }
                }
                comentariotxt = value;

            }
        }


        #endregion

        private llenarCantidad llenarcantidad;
        public llenarCantidad llenarCantidads
        {
            get => llenarcantidad;
            set
            {
                llenarcantidad = value;
            }
        }
        public class llenarCantidad
        {
            public int idDetPed { get; set; }
            public int Elim { get; set; }
        }
    }
}

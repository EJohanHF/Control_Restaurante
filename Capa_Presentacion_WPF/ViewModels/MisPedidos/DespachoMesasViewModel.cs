using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Despacho;
using Capa_Entidades.Models.Reservas;
using Capa_Negocio.Despacho;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Negocio.Reservas;
using Capa_Presentacion_WPF.ViewModels.Ambientes;
using Capa_Presentacion_WPF.ViewModels.Reservas;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.MisPedidos
{
    public class DespachoMesasViewModel : IGeneric
    {
        private bool isOpenDialogSubMesa;
        private string mesaPadre;
        private string operacion;
        private string nombreAmbiente;
        public string NumerosPedidos { get; set; }
        public int[] AIdPedidos { get; set; }
        public List<int> lista_pedidos_deleccionados { get; set; }
        Neg_Reservas neg_reserv = new Neg_Reservas();
        Funcion_Global globales = new Funcion_Global();
        Neg_Despacho negDespacho = new Neg_Despacho();
        Neg_Detalle_pedido negDetVent = new Neg_Detalle_pedido();
        Neg_Parametros negParametros = new Neg_Parametros();
        ReservasStructure rs = new ReservasStructure();
        AmbientesStructure directoryStructure = new AmbientesStructure();
        public ObservableCollection<MesasItemViewModel> DataMesas { get; set; }
        public ObservableCollection<MesasItemViewModel> DataSubMesas { get; set; }
        public ObservableCollection<AmbientesItemViewModel> DataAmbientes { get; set; }
        public ObservableCollection<Ent_Despacho> DataPedidosDeliveryBackUp { get; set; }
        public ObservableCollection<Ent_Despacho> DataPedidosDelivery { get; set; }
        public List<Ent_Reserva> _reserva { get; private set; }
        public List<Ent_Reserva> reserva { get; private set; }
        public bool IsOpenDialogSubMesa { get => isOpenDialogSubMesa; set { isOpenDialogSubMesa = value; } }
        public string MesaPadre { get => mesaPadre; set { mesaPadre = value; } }
        public string NombreAmbiente { get => nombreAmbiente; set { nombreAmbiente = value; } }
        public int nroColumnas { get; set; }
        private Mesa mesa;
        public ICommand CerrarDialog { get; set; }
        public ICommand RegistrarPedidoCommand { get; set; }
        public ICommand TraerMesasCommand { get; set; }
        public ICommand ProcesarPedidosCommand { get; set; }
        public Mesa Mesa
        {
            get => mesa;
            set
            {
                mesa = value;
            }
        }
        public string Operacion
        {
            get => operacion;
            set
            {
                try
                {
                    if (value == "Lista")
                    {
                        GetAmbientes();
                        int idAmbiente = 1;
                        if (Application.Current.Properties["IdAmbiente"] == null)
                        {
                            idAmbiente = DataAmbientes.FirstOrDefault().ID;
                            Application.Current.Properties["IdAmbiente"] = idAmbiente.ToString();
                        }
                        else
                        {
                            idAmbiente = Convert.ToInt32(Application.Current.Properties["IdAmbiente"]);
                        }
                        GetMesas(idAmbiente);
                    }
                    operacion = value;
                }
                catch (Exception)
                {
                    //globales.Mensaje("SOS-FOOD - Error: ", "No hay data de ambientes ni mesas", 3);
                }

            }
        }
        public DespachoMesasViewModel()
        {
            this.Operacion = "Lista";
            DataPedidosDelivery = new ObservableCollection<Ent_Despacho>();
            lista_pedidos_deleccionados = new List<int>();
            DataPedidosDelivery = negDespacho.GetPedidosPendientesDelivery();
            this.TraerMesasCommand = new ParamCommand(new Action<object>(TraerMesas));
            this.RegistrarPedidoCommand = new ParamCommand(new Action<object>(RegistrarPedido));
            this.ProcesarPedidosCommand = new RelayCommand(new Action(ProcesarPedidos));
            //Estado Mesa
            Timer timer = new Timer(5000);
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_elapsed);
            timer.Start();
            //FIN
        }
        private async void ProcesarPedidos()
        {
            try
            {
                if (NumerosPedidos == null || NumerosPedidos.Trim() == "")
                {
                    return;
                }
                Application.Current.Properties["CerrarDialogMotorizado"] = "SI";
                Application.Current.Properties["NumeroDiarioPedidos"] = NumerosPedidos;
                var dialog = new DialogCodigoMotorizado() { };
                var x = await DialogHost.Show(dialog, "RootDialog");
                if (Application.Current.Properties["CerrarDialogMotorizado"].ToString() == "NO")
                {
                    DataPedidosDelivery = negDespacho.GetPedidosPendientesDelivery();
                    DataPedidosDeliveryBackUp = DataPedidosDelivery;
                    lista_pedidos_deleccionados = new List<int>();
                    //imprimirCuentas();
                    NumerosPedidos = "";
                    Application.Current.Properties["NumeroDiarioPedidos"] = null;

                    //MESAS
                    directoryStructure = new AmbientesStructure();
                    if (Application.Current.Properties["IdAmbiente"] == null)
                    {
                        int idAmbiente = DataAmbientes.FirstOrDefault().ID;
                        Application.Current.Properties["IdAmbiente"] = idAmbiente.ToString();
                    }
                    GetMesas(Application.Current.Properties["IdAmbiente"].ToString());
                    //FIN
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
        private void imprimirCuentas()
        {
            DataTable pedidos = new DataTable();
            pedidos = negDespacho.GetPedidosPendientesxnumdiario(NumerosPedidos);
            string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();


            foreach (DataRow dr in pedidos.Rows)
            {
                DataTable dt = new DataTable();
                dt = negDetVent.GetCuentaPedido(Convert.ToInt32(dr["ID"]));
                Ticket ticket = new Ticket();
                System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["EMPR_LOGO"]));
                ticket.MargenLogo = negParametros.margenLogoTicket();
                ticket.MaxCharDescrip = negParametros.margenMaxDescrip();
                ticket.HeaderImage = x;

                ticket.AddSubHeaderLineEnorme("         " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                ticket.AddSubHeaderLine("Mesa: " + dt.Rows[0]["M_NOM"].ToString());
                ticket.AddSubHeaderLine("Atendido Por: " + dt.Rows[0]["EMPL_NOM"].ToString());
                ticket.AddSubHeaderLine("" + "");
                ticket.AddSubHeaderLine("" + "");
                ticket.AddSubHeaderLine("Paga con: " + dt.Rows[0]["DENO_PAGO"].ToString() + " - S/" + dt.Rows[0]["PC_MONTO"].ToString());
                ticket.AddSubHeaderLine("Vuelto: " + dt.Rows[0]["PC_VUELTO"].ToString());
                ticket.AddSubHeaderLine("");
                ticket.AddSubHeaderLine("");

                ticket.AddSubHeaderLine("Cliente: " + dt.Rows[0]["C_NOM"].ToString());
                ticket.AddSubHeaderLine("" + "");
                ticket.AddSubHeaderLine("Nro Documento:" + dt.Rows[0]["C_NRO_DOC"].ToString());
                ticket.AddSubHeaderLine("" + "");
                if (dt.Rows[0]["C_DISTR"].ToString() == "" && dt.Rows[0]["C_CALLE"].ToString() == "")
                {
                    ticket.AddSubHeaderLine("Direccion: " + dt.Rows[0]["C_DIREC"].ToString());
                }
                else
                {
                    ticket.AddSubHeaderLine("Distrito: " + dt.Rows[0]["C_DISTR"].ToString());
                    ticket.AddSubHeaderLine("" + "");
                    ticket.AddSubHeaderLine("Calle: " + dt.Rows[0]["C_CALLE"].ToString());
                    ticket.AddSubHeaderLine("" + "");
                    ticket.AddSubHeaderLine("Referencia: " + dt.Rows[0]["C_REF"].ToString());
                    ticket.AddSubHeaderLine("" + "");
                }
                ticket.AddSubHeaderLineTelefono("" + "");
                ticket.AddSubHeaderLineTelefono("Telefono: " + dt.Rows[0]["C_TEL"].ToString());

                ticket.AddSubHeaderLine("" + "");
                ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["DP_COMENTARIO"].ToString().Trim() == "")
                    {
                        ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                    }
                    else
                    {
                        ticket.AddItem("[" + dt.Rows[j]["CANTIDAD_DETALLEPEDIDO"].ToString() + "]" + dt.Rows[j]["DP_DESCRIP"].ToString() + "c/ " + dt.Rows[j]["DP_COMENTARIO"].ToString(), dt.Rows[j]["DP_PRE_UNI"].ToString(), dt.Rows[j]["IMPORTE_DETALLEPEDIDO"].ToString());
                    }

                    if (j == 35 || j == 70 || j == 105 || j == 140 || j == 175 || j == 205)
                    {
                        if (ticket.PrinterExists(ImpCaja))
                        {
                            ticket.PrintTicket(ImpCaja);
                            ticket = new Ticket();
                        }
                        else
                        {
                            globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                        }
                    }
                }
                ticket.AddTotal("SUB TOTAL", dt.Rows[0]["PED_SUBTOTAL"].ToString());
                ticket.AddTotal("DESC.", dt.Rows[0]["PED_DESCUENTO"].ToString());
                ticket.AddTotal("", "---------");
                ticket.AddTotal("TOTAL", dt.Rows[0]["PED_TOTAL"].ToString());
                ticket.AddFooterLine("");
                ticket.AddTotal("", "---------"); ticket.AddFooterLine("");
                ticket.AddFooterLine("");
                ticket.AddFooterLine("Este documento no es comprobante de pago.");

                if (ticket.PrinterExists(ImpCaja))
                {
                    ticket.PrintTicket(ImpCaja);
                }
                else
                {
                    globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                }
            }
        }
        private async void TraerMesas(object id)
        {
            this.Operacion = "Mesas";
            Application.Current.Properties["IdAmbiente"] = id.ToString();
            GetMesas(id);

            this.Mesa = new Mesa();
        }

        private void timer_elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            directoryStructure = new AmbientesStructure();
            if (Application.Current.Properties["IdAmbiente"] == null)
            {
                int idAmbiente = DataAmbientes.FirstOrDefault().ID;
                Application.Current.Properties["IdAmbiente"] = idAmbiente.ToString();
            }
            GetMesas(Application.Current.Properties["IdAmbiente"].ToString());
            DataPedidosDelivery = negDespacho.GetPedidosPendientesDelivery();
            //ConsultarDeliveryWebService();
        }
        private async void RegistrarPedido(object id)
        {
            try
            {
                NumerosPedidos = "";
                int num_diario = Convert.ToInt32(DataPedidosDelivery.Where(w => w.PED_ID_MESA == int.Parse(id.ToString())).FirstOrDefault().PED_NUM_DIARIO);
                if (lista_pedidos_deleccionados.Count() != 0)
                {
                    int i = 0;
                    foreach (var h in lista_pedidos_deleccionados)
                    {
                        if (h == num_diario)
                        {
                            DataPedidosDelivery.Where(w => w.PED_NUM_DIARIO == num_diario).FirstOrDefault().ColorSeleccionado = "White";
                            lista_pedidos_deleccionados.RemoveAt(i);
                            foreach (var y in lista_pedidos_deleccionados)
                            {
                                if (NumerosPedidos.Trim() == "")
                                {
                                    NumerosPedidos = y.ToString();
                                }
                                else
                                {
                                    NumerosPedidos = NumerosPedidos + "," + y.ToString();
                                }
                            }
                            DataPedidosDelivery = DataPedidosDelivery;
                            return;
                        }
                        i = i + 1;
                    }
                    DataPedidosDelivery.Where(w => w.PED_NUM_DIARIO == num_diario).FirstOrDefault().ColorSeleccionado = "SkyBlue";
                    lista_pedidos_deleccionados.Add(DataPedidosDelivery.Where(w => w.PED_NUM_DIARIO == num_diario).FirstOrDefault().PED_NUM_DIARIO);
                }
                else
                {
                    DataPedidosDelivery.Where(w => w.PED_NUM_DIARIO == num_diario).FirstOrDefault().ColorSeleccionado = "SkyBlue";
                    lista_pedidos_deleccionados.Add(DataPedidosDelivery.Where(w => w.PED_NUM_DIARIO == num_diario).FirstOrDefault().PED_NUM_DIARIO);
                }

                foreach (var h in lista_pedidos_deleccionados)
                {
                    if (NumerosPedidos.Trim() == "")
                    {
                        NumerosPedidos = h.ToString();
                    }
                    else
                    {
                        NumerosPedidos = NumerosPedidos + "," + h.ToString();
                    }
                }
                DataPedidosDelivery = DataPedidosDelivery;
            }
            catch (Exception ex)
            {

            }
            //globales.Mensaje("SOS-FOOD - Error", id.ToString(), 1);
        }
        private void GetMesas(object id)
        {
            try
            {
                var mesas = directoryStructure.GetLogicalDrivesMesasDelivery(id);
                this.DataMesas = new ObservableCollection<MesasItemViewModel>(
                    mesas.Select(c => new MesasItemViewModel(c.ID, c.M_NOM, c.M_EST, c.M_ID_AMB, c.M_X, c.M_ATENDIDA, c.M_WIDTH,
                    c.M_HEIGHT, c.M_TEXTO, c.M_TIPO, c.M_ACT, c.M_F_CREATE, c.color, c.M_ID_PADRE, c.M_F_MODIFICACION, c.NOMBRE_PADRE, c.PED_ID_CLIENTE, c.C_NOMINA, c.nomllevar, c.EMPL_NOM, c.mesa)));

                _reserva = rs.GetReservas(DateTime.Now, DateTime.Now);
                int contar_reserva = _reserva.Where(w => w.R_ID_ESTADO == 1 && (w.R_F_RESERVA_DESDE - DateTime.Now).TotalHours <= 2).Count();
                if (contar_reserva > 0)
                {
                    reserva = _reserva.Where(w => w.R_ID_ESTADO == 1 && (w.R_F_RESERVA_DESDE - DateTime.Now).TotalHours <= 2).ToList();
                    //this.DataMesas.Where(w => w.ID == reserva.FirstOrDefault().R_ID_MESA).FirstOrDefault().color = "#EEB810";

                    foreach (var h in reserva)
                    {
                        if (this.DataMesas.Where(w => w.ID == h.R_ID_MESA).Count() > 0)
                        {
                            if (this.DataMesas.Where(w => w.ID == h.R_ID_MESA && w.M_EST == 0).Count() > 0)
                            {
                                this.DataMesas.Where(w => w.ID == h.R_ID_MESA && w.M_EST == 0).FirstOrDefault().color = "#EEB810";
                            }
                            this.DataMesas.Where(w => w.ID == h.R_ID_MESA).FirstOrDefault().isreservada = 1;
                        }
                    }
                }

                neg_reserv.SetReservasEstados();

                //Obtener Nombre del Ambiente 
                var nom_amb = directoryStructure.GetLogicalDrivesNomAmb(Convert.ToInt32(id));
                this.nroColumnas = mesas.Select(s => s.NroColumnas).FirstOrDefault();
                this.NombreAmbiente = nom_amb.ToString();
                Application.Current.Properties["IdAmbiente"] = id.ToString();
            }
            catch (Exception ex)
            {
                //globales.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
        }
        private void GetAmbientes()
        {
            var w = System.Windows.SystemParameters.PrimaryScreenWidth;
            var h = System.Windows.SystemParameters.PrimaryScreenHeight;

            var children = directoryStructure.GetLogicalDrivesDelivery();
            this.DataAmbientes = new ObservableCollection<AmbientesItemViewModel>(
                children.Select(d => new AmbientesItemViewModel(d.ID, d.A_NOM, d.A_X, d.A_Y, d.A_WIDTH, d.A_HEIGHT, d.A_TEXTO, d.A_ACT, d.A_F_CREATE)));

        }
    }
}

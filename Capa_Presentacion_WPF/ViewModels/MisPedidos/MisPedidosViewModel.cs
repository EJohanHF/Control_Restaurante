using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Pedido;
using Capa_Entidades.Models.Sostic;
using Capa_Negocio.Parametros;
using Capa_Negocio.Pedido;
using Capa_Negocio.Sostic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using WMPLib;
namespace Capa_Presentacion_WPF.ViewModels.MisPedidos
{
    public class MisPedidosViewModel : IGeneric
    {
        #region Negocio
        Neg_SosTic sostic = new Neg_SosTic();
        Neg_Pedido neg_pedidos = new Neg_Pedido();
        Neg_Parametros_Pedidos neg_param = new Neg_Parametros_Pedidos();
        #endregion
        #region Lists
        static List<Sos_Tic> sostic2 = new List<Sos_Tic>();
        ObservableCollection<Pedidos> DataPedidosOLd = new ObservableCollection<Pedidos>();
        public ObservableCollection<Pedidos> DataPedidos { get; set; }
        public ObservableCollection<Impresora> DataPantallas { get; set; }
        #endregion
        #region Objects
        public object UserControl { get; set; }
        public string NombreImpresora { get; set; }
        public int TipoCarga { get; set; }
        decimal limiteMinutos = 0;
        string background1 = "";
        string background2 = "";
        string background3Anulado = "";
        string foreground1Anulado = "";
        decimal limiteMinutosPlatoAnulado = 0;
        bool conSonidoTarde = false;
        bool conSonidoLlegada = false;
        public decimal minutos { get; set; }
        public string color { get; set; }
        //public string ColorPedido { get; set; }
        #endregion
        #region Commands
        public ICommand EntregarPedidoCommand { get; set; }
        #endregion
        private Impresora _PantallaSelected;
        public Impresora PantallaSelected {
            get => _PantallaSelected;
            set {
                NombreImpresora = ((Impresora)value).nomimp;
                DataPedidos = neg_pedidos.GET_PEDIDOS_PENDIENTES(NombreImpresora);
                _PantallaSelected = value;
            }
        }
        public MisPedidosViewModel()
        {
            try
            {
                NombreImpresora = ConfigurationManager.AppSettings["NombreImpresoraPedido"];
                TipoCarga = Convert.ToInt32(ConfigurationManager.AppSettings["TipoCargaPedidos"]);
                this.EntregarPedidoCommand = new ParamCommand(new Action<object>(EntregarPedido));
                getData2();
                DataPantallas = neg_pedidos.getPantallas();
                Timer timer1 = new Timer(8000);
                timer1.AutoReset = true;
                timer1.Elapsed += new ElapsedEventHandler(getData);
                timer1.Start();

                Timer timer2 = new Timer(5000);
                timer2.AutoReset = true;
                timer2.Elapsed += new ElapsedEventHandler(getColoresTarde1);
                timer2.Start();

                
            }
            catch (Exception ex){}
        }
        private void EntregarPedido(object id_detalle_pedido)
        {
            try
            {
                neg_pedidos.SET_ENTREGAR_PEDIDO(Convert.ToInt32(id_detalle_pedido));
                getData2();
            }
            catch (Exception ex)
            {
            }
        }
        private void getData(object sender, ElapsedEventArgs e)
        {
            try
            {
                GetParametros();
                DataPedidosOLd = DataPedidos;
                
                DataPedidos = neg_pedidos.GET_PEDIDOS_PENDIENTES(NombreImpresora);
                int c = (DataPedidosOLd == null) ? 0 : DataPedidosOLd.Count();
                if (DataPedidos.Count() > c)
                {
                    GetSonidoLlegada();
                }
                foreach (var item in DataPedidos.Where(w => w.DP_EST == "0").ToList())
                {
                    item.ColorPedido = background3Anulado;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void getColoresTarde1(object sender, ElapsedEventArgs e)
        {
            try
            {
                foreach (var item in DataPedidos.Where(w => w.DP_EST == "1").ToList())
                {
                    minutos = (DateTime.Now - item.DP_FEC_REG).Minutes;
                    if (minutos > Convert.ToInt32(limiteMinutos))
                    {
                        //item.ColorPedido = background1;
                        if (item.ColorPedido == background2)
                        {
                            item.ColorPedido = background1;

                        }
                        else if (item.ColorPedido == background1)
                        {
                            item.ColorPedido = background2;

                        }
                        color = item.ColorPedido;
                        if (item.ColorPedido == "")
                        {
                            item.ColorPedido = background1;

                        }
                    }
                }

                if (DataPedidos.Where(w => w.ColorPedido == background1 || w.ColorPedido == background2).Count() > 0)
                {
                    GetSonidoTarde();
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void getData2()
        {
            try
            {
                GetParametros();
                DataPedidosOLd = DataPedidos;
                DataPedidos = neg_pedidos.GET_PEDIDOS_PENDIENTES(NombreImpresora);
                int c = (DataPedidosOLd == null) ? 0 : DataPedidosOLd.Count();
                if (DataPedidos.Count() > c)
                {
                    GetSonidoLlegada();
                }
                foreach (var item in DataPedidos.Where(w => w.DP_EST == "1").ToList())
                {
                    decimal minutos = (DateTime.Now - item.DP_FEC_REG).Minutes;
                    if (minutos >= Convert.ToInt32(limiteMinutos))
                    {
                        item.ColorPedido = background1;
                        if (item.ColorPedido == background2)
                        {
                            item.ColorPedido = background1;
                        }
                        else if (item.ColorPedido == background1)
                        {
                            item.ColorPedido = background2;
                        }
                        else
                        {
                            item.ColorPedido = background1;
                        }
                    }
                }
                foreach (var item in DataPedidos.Where(w => w.DP_EST == "0").ToList())
                {
                    item.ColorPedido = background3Anulado;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void GetParametros() {
            try
            {
                background1 = neg_param.Background1();
                background2 = neg_param.Background2();
                background3Anulado = neg_param.Background3Anulado();
                foreground1Anulado = neg_param.Forecolor1Anulado();
                limiteMinutos = Convert.ToDecimal(neg_param.LimiteMinutos());
                limiteMinutosPlatoAnulado = Convert.ToDecimal(neg_param.LimiteMinutosPlatoAnulado());
                conSonidoTarde = Convert.ToBoolean(neg_param.conSonidoTarde());
                conSonidoLlegada = Convert.ToBoolean(neg_param.conSonidoLlegada());
            }
            catch (Exception ex)
            {
            }
        }
        private void GetSonidoLlegada() {
            try
            {
                if (conSonidoLlegada) {
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "campana.mp3";
                    WindowsMediaPlayer wplayer = new WindowsMediaPlayer();
                    wplayer.URL = ruta;
                    wplayer.controls.play();
                }
            }
            catch (Exception ex){}
        }
        private void GetSonidoTarde() {
            try
            {
                if (conSonidoTarde) {
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "ring.mp3";
                    WindowsMediaPlayer wplayer = new WindowsMediaPlayer();
                    wplayer.URL = ruta;
                    wplayer.controls.play();
                }
            }
            catch (Exception ex){}
        }
    }
}

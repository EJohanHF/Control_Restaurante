using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Reservas;
using Capa_Negocio.Reservas;
using Capa_Presentacion_WPF.Views.Dialogs;
using Capa_Presentacion_WPF.Views.Dialogs.Pedidos;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Reservas
{
    public class RptReservasViewModel : IGeneric
    {
        ReservasStructure rs = new ReservasStructure();
        #region Negocio
        Neg_Reservas neg_re = new Neg_Reservas();
        #endregion
        #region Entidad
        private Cliente _Cliente;
        public Cliente Cliente
        {
            get => _Cliente;
            set
            {
                _Cliente = value;
            }
        }
        private MesasItem _MesasItem;
        public MesasItem MesasItem { get => _MesasItem; set { _MesasItem = value; } }

        private AmbientesItem _AmbientesItem;
        public AmbientesItem AmbientesItem { get => _AmbientesItem; set { _AmbientesItem = value; } }

        private Ent_Reserva _Ent_Reserva;
        public Ent_Reserva Ent_Reserva { get => _Ent_Reserva; set { _Ent_Reserva = value; } }
        #endregion
        #region Objetos
        public string NomCliente { get; set; }
        public string IdCliente { get; set; }
        public DateTime FechaDesde { get; set; } = DateTime.Now;
        public DateTime FechaHasta { get; set; } = DateTime.Now;
        public DateTime HoraStart { get; set; }
        public DateTime HoraFinish { get; set; }
        public int EstadoSeleccionado { get; set; } = 1;
        #endregion
        #region Listas
        public List<Ent_Reserva> DataReservas { get; set; }
        public List<Ent_Reserva> DataEstadosReserva { get; set; }
        #endregion
        #region SeleccionCombos

        private Ent_Reserva _TipoReservaSelected;
        public Ent_Reserva TipoReservaSelected
        {
            get => _TipoReservaSelected;
            set
            {
                if (value != null)
                {
                    Ent_Reserva.TR_ID = ((Ent_Reserva)value).TR_ID;
                }
                _TipoReservaSelected = value;
            }
        }
        #endregion
        #region Commands
        public ICommand SeleccionEstadoReservaCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        #endregion

        public RptReservasViewModel()
        {
            this.DataEstadosReserva = rs.GetEstadosReserva();
            this.SeleccionEstadoReservaCommand = new ParamCommand(new Action<object>(SeleccionEstadoReserva));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            GetLista();
        }
        private void Buscar()
        {
            DataReservas = rs.GetReservasXEstado(EstadoSeleccionado, FechaDesde, FechaHasta);
        }
        private void SeleccionEstadoReserva(object _id)
        {
            int id = Convert.ToInt32(_id);
            EstadoSeleccionado = id;
            DataReservas = rs.GetReservasXEstado(EstadoSeleccionado, FechaDesde, FechaHasta);
            DataEstadosReserva.Where(w => w.ER_ID == id).First().check = true;
            DataEstadosReserva.Where(w => w.ER_ID != id).First().check = false;
        }
        private void GetLista()
        {
            DataReservas = rs.GetReservasXEstado(EstadoSeleccionado, FechaDesde, FechaHasta);
        }
    }
}

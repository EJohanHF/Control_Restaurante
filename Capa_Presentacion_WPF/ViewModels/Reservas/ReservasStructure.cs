using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Reservas;
using Capa_Negocio.Ambientes;
using Capa_Negocio.Reservas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Reservas
{
    public class ReservasStructure
    {
        #region Negocio
        Neg_Ambientes neg = new Neg_Ambientes();
        Neg_Mesa negM = new Neg_Mesa();
        Neg_Reservas negr = new Neg_Reservas();
        #endregion

        #region Listas
        static List<AmbientesItem> ambientesItem = new List<AmbientesItem>();
        static List<MesasItem> mesasItem = new List<MesasItem>();
        static ObservableCollection<Platos> platos = new ObservableCollection<Platos>();
        static ObservableCollection<Ent_Reserva> reservas = new ObservableCollection<Ent_Reserva>();
        static ObservableCollection<Ent_Reserva> treservas = new ObservableCollection<Ent_Reserva>();
        static ObservableCollection<Ent_Reserva> ereservas = new ObservableCollection<Ent_Reserva>();
        #endregion

        public ReservasStructure()
        {
            ambientesItem = neg.GetAmbientes();
            mesasItem = negM.GetMesas();
            reservas = negr.GetReserva(DateTime.Now, DateTime.Now);
            treservas = negr.GetTipoReserva();
            ereservas = negr.GetEstadosReserva();
        }
        public List<AmbientesItem> GetAmbientes()
        {
            return ambientesItem.Where(w => w.A_ACT == true).ToList();
        }
        public List<MesasItem> GetMesas()
        {
            return mesasItem.Where(w => w.M_ACT == true && w.M_ID_PADRE == 0).ToList();
        }
        public List<MesasItem> GetMesas(object id)
        {
            int _id = Convert.ToInt32(id);
            return mesasItem.Where(w => w.M_ID_AMB == _id && w.M_ACT == true && w.M_ID_PADRE == 0).ToList();
        }
        public List<Ent_Reserva> GetReservas(DateTime inicio,DateTime final)
        {
            reservas = negr.GetReserva(inicio,final);
            return reservas.ToList();
        }
        public List<Platos> GetDetalleReserva(int id_reserva)
        {
            platos = negr.GetDetalleReserva(id_reserva);
            return platos.ToList();
        }
        public List<Ent_Reserva> GetTipoReservas()
        {
            return treservas.Where(w=> w.TR_ACT == true).ToList();
        }
        public List<Ent_Reserva> GetReservasXEstado(int id_estado,DateTime f1,DateTime f2)
        {
            var lista = negr.GetReservaxEstado(id_estado, f1, f2);
            return lista;
        }
        public List<Ent_Reserva> GetEstadosReserva()
        {
            return ereservas.Where(w => w.ER_ACT == true).ToList();
        }
    }
}

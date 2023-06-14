using Capa_Datos.Reservas;
using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Reservas;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Reservas
{
    public class Neg_Reservas
    {
        Dat_Reservas dat = new Dat_Reservas();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Ent_Reserva> GetReserva(DateTime inicio,DateTime final)
        {
            ObservableCollection<Ent_Reserva> reserva = new ObservableCollection<Ent_Reserva>();
            try
            {
                DataTable dt = dat.GetReserva(inicio,final);
                foreach (DataRow row in dt.Rows)
                {
                    Ent_Reserva _r = new Ent_Reserva();
                    _r.ID = Convert.ToInt32(row["ID"]);
                    _r.R_ID_CLIENTE = Convert.ToInt32(row["R_ID_CLIENTE"]);
                    _r.C_NOMINA = row["C_NOMINA"].ToString();
                    _r.C_NRO_DOC = row["C_NRO_DOC"].ToString();
                    _r.C_TEL = row["C_TEL"].ToString();
                    _r.C_COR = row["C_COR"].ToString();
                    _r.R_ID_USUARIO = Convert.ToInt32(row["R_ID_USUARIO"]); 
                    _r.USU_NOM = row["USU_NOM"].ToString();
                    _r.R_ID_MESA = Convert.ToInt32(row["R_ID_MESA"]);
                    _r.R_ID_AMBIENTE = Convert.ToInt32(row["M_ID_AMB"]);
                    _r.M_NOM = row["M_NOM"].ToString();
                    _r.R_F_RESERVA_DESDE = Convert.ToDateTime(row["R_F_RESERVA_DESDE"]);
                    _r.R_F_RESERVA_HASTA = Convert.ToDateTime(row["R_F_RESERVA_HASTA"]);
                    _r.R_ID_ESTADO = Convert.ToInt32(row["R_ID_ESTADO"]);
                    _r.ER_DESCR = row["ER_DESCR"].ToString();
                    _r.R_OBS = row["R_OBS"].ToString();
                    _r.R_ID_TIPO_RESERVA = Convert.ToInt32(row["R_ID_TIPO_RESERVA"]);
                    _r.TR_ID = Convert.ToInt32(row["R_ID_TIPO_RESERVA"]);
                    _r.R_DESCR = row["R_DESCR"].ToString();
                    _r.R_F_CREATE = Convert.ToDateTime(row["R_F_CREATE"]);
                    _r.R_AMORTIZADO = Convert.ToDecimal(row["R_AMORTIZADO"]);
                    reserva.Add(_r);
                }
            }
            catch (Exception ex)
            {
                reserva = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return reserva;
        }
        public ObservableCollection<Platos> GetDetalleReserva(int id_reserva)
        {
            ObservableCollection<Platos> platos = new ObservableCollection<Platos>();
            try
            {
                DataTable dt = dat.GetDetalleReserva(id_reserva);
                int i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    Platos _p = new Platos();
                    _p.orden = i;
                    _p.idplato = Convert.ToInt32(row["DR_ID_CARTA"]);
                    _p.nomplato = row["CAR_NOM"].ToString();
                    _p.comentario = "";
                    _p.cantidad = row["DR_CANT"].ToString();
                    _p.precplato = row["CAR_PRECIO"].ToString();
                    _p.importe = row["IMPORTE"].ToString();
                    platos.Add(_p);
                    i = i + 1;
                }
            }
            catch (Exception)
            {
                platos = null;
            }
            return platos;
        }
        public List<Ent_Reserva> GetReservaxEstado(int id_estado,DateTime inicio,DateTime final)
        {
            List<Ent_Reserva> reserva = new List<Ent_Reserva>();
            try
            {
                DataTable dt = dat.GetReservaxEstado(id_estado,inicio,final);
                int i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    Ent_Reserva _r = new Ent_Reserva();
                    _r.ID = Convert.ToInt32(row["ID"]);
                    _r.R_ID_CLIENTE = Convert.ToInt32(row["R_ID_CLIENTE"]);
                    _r.C_NOMINA = row["C_NOMINA"].ToString();
                    _r.C_NRO_DOC = row["C_NRO_DOC"].ToString();
                    _r.C_TEL = row["C_TEL"].ToString();
                    _r.C_COR = row["C_COR"].ToString();
                    _r.R_ID_USUARIO = Convert.ToInt32(row["R_ID_USUARIO"]);
                    _r.USU_NOM = row["USU_NOM"].ToString();
                    _r.R_ID_MESA = Convert.ToInt32(row["R_ID_MESA"]);
                    _r.R_ID_AMBIENTE = Convert.ToInt32(row["M_ID_AMB"]);
                    _r.M_NOM = row["M_NOM"].ToString();
                    _r.R_F_RESERVA_DESDE = Convert.ToDateTime(row["R_F_RESERVA_DESDE"]);
                    _r.R_F_RESERVA_HASTA = Convert.ToDateTime(row["R_F_RESERVA_HASTA"]);
                    _r.R_ID_ESTADO = Convert.ToInt32(row["R_ID_ESTADO"]);
                    _r.ER_DESCR = row["ER_DESCR"].ToString();
                    _r.R_OBS = row["R_OBS"].ToString();
                    _r.R_ID_TIPO_RESERVA = Convert.ToInt32(row["R_ID_TIPO_RESERVA"]);
                    _r.TR_ID = Convert.ToInt32(row["R_ID_TIPO_RESERVA"]);
                    _r.R_DESCR = row["R_DESCR"].ToString();
                    _r.R_F_CREATE = Convert.ToDateTime(row["R_F_CREATE"]);
                    _r.R_AMORTIZADO = Convert.ToDecimal(row["R_AMORTIZADO"]);
                    reserva.Add(_r);
                }
            }
            catch (Exception)
            {
                reserva = null;
            }
            return reserva;
        }
        public ObservableCollection<Ent_Reserva> GetTipoReserva()
        {
            ObservableCollection<Ent_Reserva> treserva = new ObservableCollection<Ent_Reserva>();
            try
            {
                DataTable dt = dat.GetTipoReserva();
                foreach (DataRow row in dt.Rows)
                {
                    Ent_Reserva _r = new Ent_Reserva();
                    _r.TR_ID = Convert.ToInt32(row["ID"]);
                    _r.TR_DESCR = row["TR_DESCR"].ToString();
                    _r.TR_ACT= Convert.ToBoolean(row["TR_ACT"]);
                    _r.TR_F_CREATE= Convert.ToDateTime(row["TR_F_CREATE"]);
                    _r.TR_F_MODIFICACION= Convert.ToDateTime(row["TR_F_MODIFICACION"]);
                    treserva.Add(_r);
                }
            }
            catch (Exception ex)
            {
                treserva = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return treserva;
        }
        
        public ObservableCollection<Ent_Reserva> GetEstadosReserva()
        {
            ObservableCollection<Ent_Reserva> ereserva = new ObservableCollection<Ent_Reserva>();
            try
            {
                DataTable dt = dat.GetEstadosReserva();
                foreach (DataRow row in dt.Rows)
                {
                    Ent_Reserva _r = new Ent_Reserva();
                    _r.ER_ID = Convert.ToInt32(row["ID"]);
                    _r.ER_DESCRIPCION = row["ER_DESCR"].ToString();
                    _r.ER_ACT= Convert.ToBoolean(row["ER_ACT"]);
                    _r.ER_F_CREATE= Convert.ToDateTime(row["ER_F_CREATE"]);
                    _r.ER_F_MODIFICACION= Convert.ToDateTime(row["ER_F_MODIFICACION"]);
                    ereserva.Add(_r);
                }
            }
            catch (Exception ex)
            {
                ereserva = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return ereserva;
        }


        public bool SetReserva(int operacion,Cliente Cliente, Ent_Reserva Ent_Reserva, MesasItem MesasItem,DateTime FechaHasta, DateTime FechaDesde,int idUsuario,DataTable dt)
        {
            bool result = false;
            result = dat.SetReserva(operacion, Cliente, Ent_Reserva, MesasItem, FechaHasta, FechaDesde, idUsuario,dt);
            return result;
        }
        public bool SetReservasEstados()
        {
            bool result = false;
            result = dat.SetReservasEstados();
            return result;
        }
    }
}

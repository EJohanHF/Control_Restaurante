using Capa_Entidades;
using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Reservas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Reservas
{
    public class Dat_Reservas
    {
        public DataTable GetTipoReserva()
        {
            string query = "[USP_GET_TIPO_RESERVA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception)
            {
                dt = null;
               //throw;
            }
            return dt;
        }
        public DataTable GetReserva(DateTime inicio,DateTime final)
        {
            string query = "[USP_GET_RESERVA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Inicio", inicio);
                    cmd.Parameters.AddWithValue("@Final", final);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception)
            {
                dt = null;
               //throw;
            }
            return dt;
        }
        
        public DataTable GetDetalleReserva(int id_reserva)
        {
            string query = "[USP_GET_DETALLE_RESERVA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_RESERVA", id_reserva);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception)
            {
                dt = null;
               //throw;
            }
            return dt;
        }
        
        public DataTable GetReservaxEstado(int id_estado,DateTime inicio, DateTime final)
        {
            string query = "[USP_GET_RESERVA_X_ESTADO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_ESTADO", id_estado);
                    cmd.Parameters.AddWithValue("@INICIO", inicio);
                    cmd.Parameters.AddWithValue("@FINAL", final);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                dt = null;
               //throw;
            }
            return dt;
        }
        
        public DataTable GetEstadosReserva()
        {
            string query = "[USP_GET_ESTADOS_RESERVA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception)
            {
                dt = null;
               //throw;
            }
            return dt;
        }

        public bool SetReserva(int operacion, Cliente Cliente, Ent_Reserva Ent_Reserva, MesasItem MesasItem, DateTime FechaDesde,DateTime FechaHasta,int idUsuario,DataTable dt)
        {
            string query = "[USP_SET_RESERVA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID", Ent_Reserva.ID);
                    cmd.Parameters.AddWithValue("@R_ID_CLIENTE", Cliente.idcli);
                    cmd.Parameters.AddWithValue("@R_ID_USUARIO", idUsuario);
                    cmd.Parameters.AddWithValue("@R_ID_MESA", MesasItem.ID);
                    cmd.Parameters.AddWithValue("@R_F_RESERVA_DESDE", FechaDesde);
                    cmd.Parameters.AddWithValue("@R_F_RESERVA_HASTA", FechaHasta);
                    cmd.Parameters.AddWithValue("@R_OBS", Ent_Reserva.R_OBS);
                    cmd.Parameters.AddWithValue("@R_ID_TIPO_RESERVA", Ent_Reserva.TR_ID);
                    cmd.Parameters.AddWithValue("@R_AMORTIZADO", Ent_Reserva.R_AMORTIZADO);
                    cmd.Parameters.AddWithValue("@dt", dt);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
               //throw;
                result = false;
            }
            return result;
        }
        public bool SetReservasEstados()
        {
            string query = "[USP_SET_ESTADO_RESERVA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}

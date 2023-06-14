using Capa_Entidades;
using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Configuracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Ambientes
{
    public class Dat_Mesa
    {
        public DataTable GetMesa()
        {
            string query = "USP_GET_MESAS";
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
        public DataTable GetMesasActiva()
        {
            string query = "USP_GET_MESAS_ACTIVAS";
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
        public DataTable GetMesasActivaDelivery()
        {
            string query = "USP_GET_MESAS_ACTIVAS_DELIVERY";
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
        public bool SetMesas(int operacion, MesasItem mes, ref string _mensaje)
        {
            string query = "[USP_SET_MESAS]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@id", mes.ID);
                    cmd.Parameters.AddWithValue("@nombre", mes.M_NOM);
                    cmd.Parameters.AddWithValue("@Idambiente", mes.M_ID_AMB);
                    cmd.Parameters.AddWithValue("@estado", mes.M_ACT);
                    cmd.Parameters.AddWithValue("@tipoMesa", mes.ID_TM);
                    cmd.Parameters.AddWithValue("@fecRegistro", mes.M_F_CREATE);
                    cmd.Parameters.AddWithValue("@idpadre", mes.M_ID_PADRE);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                result = false;
            }
            return result;
        }
        public DataTable GettipoMesas()
        {
            string query = "USP_GET_TIPO_MESAS";
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
        public bool SetTamañoAmbiente(MesasItem mes, ref string _mensaje)
        {
            string query = "[USP_SET_TAMANIO_AMBIENTES]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idambiente", mes.M_ID_AMB);
                    cmd.Parameters.AddWithValue("@width", mes.M_WIDTH);
                    cmd.Parameters.AddWithValue("@height", mes.M_HEIGHT);
                    cmd.Parameters.AddWithValue("@left", mes.A_X);
                    cmd.Parameters.AddWithValue("@right", mes.A_Y);
                    cmd.Parameters.AddWithValue("@top", mes.A_TOP);
                    cmd.Parameters.AddWithValue("@bottom", mes.A_BOTTOM);
                    cmd.Parameters.AddWithValue("@nroColumns", mes.NroColumnas);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                result = false;
            }
            return result;
        }
        public DataTable GetMesaDisponible(string nombre_mesa)
        {
            string query = "sp_validar_mesa";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_mesa", nombre_mesa);
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
    }
}

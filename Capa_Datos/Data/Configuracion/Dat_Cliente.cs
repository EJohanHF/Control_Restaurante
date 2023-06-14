using Capa_Entidades;
using Capa_Entidades.Models.Configuracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Configuracion
{
  public  class Dat_Cliente
    {
        public DataTable GetCliente()
        {
            string query = "[USP_GET_CLIENTE]";
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

        
        public bool SetCliente(int operacion, Cliente cliente, ref string _mensaje)
        {
            string query = "[USP_SET_CLIENTE]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idcli", cliente.idcli);
                    cmd.Parameters.AddWithValue("@c_nominacion",cliente.denominacion);
                    //cmd.Parameters.AddWithValue("@c_ape", cliente.apecli);
                    cmd.Parameters.AddWithValue("@c_num_doc ", cliente.ndoc);
                    cmd.Parameters.AddWithValue("@id_tipo_doc", cliente.idtd);
                    //cmd.Parameters.AddWithValue("@c_direc", cliente.dircli);
                    cmd.Parameters.AddWithValue("@c_distrito ", cliente.distritocli);
                    cmd.Parameters.AddWithValue("@c_calle ", cliente.callecli);
                    cmd.Parameters.AddWithValue("@c_referencia ", cliente.referenciacli);
                    cmd.Parameters.AddWithValue("@c_tel", cliente.telcli);
                    cmd.Parameters.AddWithValue("@c_cor ", cliente.corcli);
                    cmd.Parameters.AddWithValue("@c_act", cliente.estadocli);
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
        public int SetCliente2(int operacion, Cliente cliente, ref string _mensaje)
        {
            string query = "USP_SET_CLIENTE2";
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idcli", cliente.idcli);
                    cmd.Parameters.AddWithValue("@c_nominacion",cliente.denominacion);
                    //cmd.Parameters.AddWithValue("@c_ape", cliente.apecli);
                    cmd.Parameters.AddWithValue("@c_num_doc ", cliente.ndoc);
                    cmd.Parameters.AddWithValue("@id_tipo_doc", cliente.idtd);
                    //cmd.Parameters.AddWithValue("@c_direc", cliente.dircli);
                    cmd.Parameters.AddWithValue("@c_distrito ", cliente.distritocli);
                    cmd.Parameters.AddWithValue("@c_calle ", cliente.callecli);
                    cmd.Parameters.AddWithValue("@c_referencia ", cliente.referenciacli);
                    cmd.Parameters.AddWithValue("@c_tel", cliente.telcli);
                    cmd.Parameters.AddWithValue("@c_cor ", cliente.corcli);
                    cmd.Parameters.AddWithValue("@c_act", cliente.estadocli);
                    cmd.Parameters.AddWithValue("@mensaje", 0).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(cmd.Parameters["@mensaje"].Value);
                    //result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                result = 0;
            }
            return result;
        }
        public DataTable GetClientexNombreTelefono(string buscador)
        {
            string query = "[USP_GET_CLIENTE_X_NOMBRE_TELEFONO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@criterio", buscador);
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
        public DataTable GetClientexTelefono(string buscador)
        {
            string query = "[USP_GET_CLIENTE_X_TELEFONO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@criterio", buscador);
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
        public DataTable GetClientexId(int idcli)
        {
            string query = "USP_GET_CLIENTE_X_ID";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcli", idcli);
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
    }
}

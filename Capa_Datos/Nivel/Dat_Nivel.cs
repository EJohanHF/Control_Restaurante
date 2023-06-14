using Capa_Entidades;
using Capa_Entidades.Models.Nivel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Nivel
{
    public class Dat_Nivel
    {
        public DataTable GetNivel()
        {
            string query = "[USP_GET_NIVEL]";
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
        public DataTable GetSubNivel()
        {
            string query = "[USP_GET_SUBNIVEL]";
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
        public DataTable GetSeleccionNivel()
        {
            string query = "[USP_GET_SELECCION_NIVEL]";
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
            }catch (Exception)
            {
                dt = null;
               //throw;
            }return dt;

        }
        public bool SetNivel(int operacion, Ent_Nivel nivel,DataTable dt, ref string _mensaje)
        {
            string query = "[USP_SET_NIVEL]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID", nivel.ID);
                    cmd.Parameters.AddWithValue("@N_NOM", nivel.N_NOM);
                    cmd.Parameters.AddWithValue("@N_ACT", nivel.N_ACT);
                    cmd.Parameters.AddWithValue("@N_TIPO_SELEC", nivel.N_TIPO_SELEC);
                    cmd.Parameters.AddWithValue("@TABLE", dt);
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
    }
}

using Capa_Entidades;
using Capa_Entidades.Models.Carta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Carta
{
   public class Dat_Grupo
    {
        public DataTable GetGrupo()
        {
            string query = "[USP_GET_GRUPO]";
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
        public DataTable GetGupoxCategoriaTodos(int idcat)
        {
            string query = "USP_GET_GRUPO_TODOS";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcat", idcat);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        public DataTable getGruposCombo()
        {
            string query = "USP_GET_GRUPO_COMBO";
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
            }
            return dt;
        }
        public bool SetGrupo(int operacion, Grupo grupo, ref string _mensaje)
        {
            string query = "[USP_SET_GRUPO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idgrup", grupo.idgrup);
                    cmd.Parameters.AddWithValue("@idcat", grupo.idcat);
                    cmd.Parameters.AddWithValue("@grup_nom", grupo.nomgrup);
                    cmd.Parameters.AddWithValue("@grup_image", grupo.imagengrup);
                    cmd.Parameters.AddWithValue("@grup_desc", grupo.descgrup);
                    cmd.Parameters.AddWithValue("@idscat", grupo.idscat);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                _mensaje = "" + grupo.idgrup + " está en uso, desvincule para eliminar";
                result = false;
            }
            return result;
        }
        public DataTable GetGrupoxCategoria(int id)
        {
            string query = "[USP_GET_GRUPO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_CAT", id);
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

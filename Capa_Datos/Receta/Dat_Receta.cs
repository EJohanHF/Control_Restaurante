using Capa_Entidades;
using Capa_Entidades.Models.Receta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Receta
{
    public class Dat_Receta
    {
        public DataTable GetReceta()
        {
            string query = "[USP_GET_RECETA]";
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
        public DataTable GetSubReceta()
        {
            string query = "[SP_GET_SUB_RECETA]";
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

        public DataTable GetDetalleSubRecetaInsumo()
        {
            string query = "[USP_GET_DETALLE_SUB_RECETA_INSUMO]";
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
        public bool SetReceta(int operacion, Recetas receta, DataTable dtt, ref string _mensaje)
        {
            string query = "[USP_SET_RECETA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@id", receta.ID);
                    cmd.Parameters.AddWithValue("@RE_ID_CARTA", receta.RE_ID_CARTA);
                    cmd.Parameters.AddWithValue("@RE_ID_INS", receta.RE_ID_INS);
                    cmd.Parameters.AddWithValue("@RE_ACT", receta.RE_ACT);
                    cmd.Parameters.AddWithValue("@RE_CANT_INS", receta.RE_CANT_INS);
                    cmd.Parameters.AddWithValue("@RE_ID_SUB_RECETA", receta.SR_ID);
                    cmd.Parameters.AddWithValue("@SUB_RECETA", receta.RE_SUB_RECETA);
                    cmd.Parameters.AddWithValue("@RE_INS_ACT", receta.RE_INS_ACT);
                    cmd.Parameters.AddWithValue("@TABLE", dtt);
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
        public bool SetSubReceta(int operacion, Recetas receta, DataTable dtt, ref string _mensaje)
        {
            string query = "[USP_SET_SUB_RECETA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID", receta.SR_ID);
                    cmd.Parameters.AddWithValue("@SR_DESCR", receta.SR_DESCR);
                    cmd.Parameters.AddWithValue("@SR_ACT", receta.SR_ACT);
                    cmd.Parameters.AddWithValue("@SR_COSTO", receta.SR_COSTO);
                    cmd.Parameters.AddWithValue("@TABLE", dtt);
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

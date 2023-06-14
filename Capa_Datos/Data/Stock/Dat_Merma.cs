using Capa_Entidades;
using Capa_Entidades.Models.Stock;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Data.Stock
{
    public class Dat_Merma
    {
        public DataTable GetMermas(DateTime? desde, DateTime? hasta, int id_insumo)
        {
            string query = "USP_GET_MERMAS";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", hasta);
                    cmd.Parameters.AddWithValue("@id_insumo", id_insumo);
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
        public DataTable GetComboInsumos()
        {
            string query = "USP_GET_COMBO_INSUMOS";
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
        public DataTable GetCantidadxInsumo(int idins, int id_alma)
        {
            string query = "USP_GET_CANTIDAD_X_INSUMO";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idins", idins);
                    cmd.Parameters.AddWithValue("@idalma", id_alma);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }
        public bool setMerma(int op, Ent_Merma Merma)
        {
            string query = "USP_SET_MERMA";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@op", op);
                    cmd.Parameters.AddWithValue("@id_ins", Merma.MI_ID_INS);
                    cmd.Parameters.AddWithValue("@cant", Merma.MI_CANT);
                    cmd.Parameters.AddWithValue("@id_usu", Merma.MI_ID_USU);
                    cmd.Parameters.AddWithValue("@id_alm", Merma.MI_ID_ALM);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = true;
                    }
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

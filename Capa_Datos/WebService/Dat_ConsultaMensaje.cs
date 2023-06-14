using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.WebService
{
    public class Dat_ConsultaMensaje
    {
        public bool sp_set_mensaje(string id_unique_messages, string title_messages, string text_messages, string date_messages, string cod_business, string executed, string received, string type_messages, Byte[] image_business)
        {
            string query = "sp_set_mensaje";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_unique_messages", id_unique_messages);
                    cmd.Parameters.AddWithValue("@title_messages", title_messages);
                    cmd.Parameters.AddWithValue("@text_messages", text_messages);
                    cmd.Parameters.AddWithValue("@date_messages", date_messages);
                    cmd.Parameters.AddWithValue("@cod_business", cod_business);
                    cmd.Parameters.AddWithValue("@executed", executed);
                    cmd.Parameters.AddWithValue("@received", received);
                    cmd.Parameters.AddWithValue("@type_messages", type_messages);
                    cmd.Parameters.AddWithValue("@image_business", image_business);
                    cmd.Parameters.AddWithValue("@text_messages_abrev", text_messages);
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
        public bool sp_set_mensaje_leido(int id_mensaje)
        {
            string query = "sp_mensaje_leido";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_unique_messages", id_mensaje);
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
        public DataTable GetMensaje()
        {
            string query = "[sg_get_mensaje]";
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
    }
}

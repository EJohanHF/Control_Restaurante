using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Sostic
{
    public class Dat_Sostic
    {
        public DataTable GetEmpresa()
        {
            string query = "USP_GET_SOSTIC";
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
        public bool SetSostic(string telefono1,string telefono2, string correo,Byte[] logo_sostic,Byte[] logo_sosfood)
        {
            string query = "SF_SET_SOSTIC";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TELEFONO1", telefono1);
                    cmd.Parameters.AddWithValue("@TELEFONO2", telefono2);
                    cmd.Parameters.AddWithValue("@CORREO1", correo);
                    cmd.Parameters.AddWithValue("@LOGO_SOSTIC", logo_sostic);
                    cmd.Parameters.AddWithValue("@LOGO_SOSFOOD", logo_sosfood);
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

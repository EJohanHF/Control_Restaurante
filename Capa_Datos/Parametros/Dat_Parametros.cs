using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Parametros
{
    public class Dat_Parametros
    {
        public DataTable SelectParametros()
        {
            string query = "[SP_SELECT_PARAMETROS]";
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
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }
        public DataTable SelectParametros2()
        {
            string query = "[SP_SELECT_PARAMETROS2]";
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
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }
        public DataTable GetParametrosImpuestos()
        {
            string query = "[USP_GET_IMPUESTOS]";
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
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }
        public DataTable SelectParametrosSUNAT()
        {
            string query = "[USP_GET_PARAMETROS_SUNAT]";
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
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable SelectParametrosWebService()
        {
            string query = "[sp_select_parametros_web_service]";
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
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }

        public bool SetParametros(Capa_Entidades.Models.Parametros.Parametros parametros)
        {
            string query = "[USP_SET_PARAMETROS]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valor_par", parametros.VALOR_PAR);
                    cmd.Parameters.AddWithValue("@id_par", parametros.ID_PAR);
                    cmd.Parameters.AddWithValue("@tipo_parametro", parametros.tipoParametro);
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

        public bool Set_Parametros_Impuestos(Capa_Entidades.Models.Parametros.Parametros parametros)
        {
            string query = "USP_SET_IMPUESTOS";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valor_par", parametros.VALOR_PAR);
                    cmd.Parameters.AddWithValue("@id_par", parametros.ID_PAR);
                    cmd.Parameters.AddWithValue("@tipo_parametro", parametros.tipoParametro);
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
        public DataTable SelectTipoMoneda()
        {
            string query = "[sp_get_cambio]";
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
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }
    }
}

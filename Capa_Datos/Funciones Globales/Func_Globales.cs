using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Funciones_Globales
{
    public class Func_Globales
    {
        public DataTable ImpCaja()
        {
            string query = "[USP_IMPCAJA]";
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
        public DataTable EstMesaWeb()
        {
            string query = "[sp_select_mesaweb]";
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
        public DataTable DesbloquearMesas()
        {
            string query = "[sp_desbloquear_mesas]";
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
        public DataTable BloquearMesaWeb(string mesa)
        {
            string query = "[sp_bloquear_mesa_web]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mesa", mesa);
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
        public DataTable DesbloquearMesaWeb(string mesa)
        {
            string query = "[sp_desbloquear_mesa_web]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mesa", mesa);
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
    }
}

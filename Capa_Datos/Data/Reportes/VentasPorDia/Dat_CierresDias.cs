using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Data.Reportes.VentasPorDia
{
    public class Dat_CierresDias
    {
        MEnsaje mensaje = new MEnsaje();
        public DataTable GET_CIERRES()
        {
            string query = "GET_CIERRES";
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
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;

        }
        public DataTable GET_CIERRES_X_DIA(string dia)
        {
            string query = "sp_get_cierre_dia";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fecha", dia);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;

        }
        public DataTable GET_CIERRES_ENTRE_DIA(string fecha,string fecha2)
        {
            string query = "sp_get_cierre_entre_dia";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@fecha2", fecha2);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;

        }
    }
}

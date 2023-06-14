using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Data.Reportes.Pedidos
{
    public class Dat_DpAdelantoxEmpleado
    {
        public DataTable GetAdelantoxEmpleado(string desde, string hasta, string mes, string dia)
        {
            string query = "[sp_get_adelanto_x_empleado]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FECHA_DESDE", desde);
                    cmd.Parameters.AddWithValue("@FECHA_HASTA", hasta);
                    cmd.Parameters.AddWithValue("@MES", mes);
                    cmd.Parameters.AddWithValue("@DIA", dia);
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
        public DataTable GetTotalAdelantoxEmpleado(string desde, string hasta, string dia)
        {
            string query = "[sp_get_adelanto_x_empleado_group]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FECHA_DESDE", desde);
                    cmd.Parameters.AddWithValue("@FECHA_HASTA", hasta);
                    cmd.Parameters.AddWithValue("@DIA", dia);
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

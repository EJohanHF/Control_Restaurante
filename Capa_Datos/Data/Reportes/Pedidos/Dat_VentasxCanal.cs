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
    public class Dat_VentasxCanal
    {
        public DataTable getVentasCanal(DateTime desde, DateTime hasta, int id_canal, int id_tipo_pago)
        {
            string query = "USP_GET_VENTAS_X_CANAL_X_PAGO";
            DataTable dt = new DataTable();
            try
            {
                string ini = desde.ToString("yyyy-MM-dd");
                DateTime inicio = Convert.ToDateTime(ini);

                string fin = hasta.ToString("yyyy-MM-dd");
                DateTime final = Convert.ToDateTime(fin);
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_CANAL", id_canal);
                    cmd.Parameters.AddWithValue("@ID_TIPO_PAGO", id_tipo_pago);
                    cmd.Parameters.AddWithValue("@DESDE", ini);
                    cmd.Parameters.AddWithValue("@HASTA", fin);
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
        public DataTable getPlatosVendidos(DateTime desde, DateTime hasta, int id_canal)
        {
            string query = "USP_GET_PLATOS_VENDIDOS_X_CANALVENTA";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_CANAL", id_canal);
                    cmd.Parameters.AddWithValue("@DESDE", desde);
                    cmd.Parameters.AddWithValue("@HASTA", hasta);
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
        public DataTable getComboCanal()
        {
            string query = "USP_GET_CANAL_VENTA";
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
        public DataTable getComboTipoPago()
        {
            string query = "USP_GET_TIPO_PAGO_COMBO";
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
    }
}

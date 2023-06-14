using Capa_Entidades;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Data.Reportes.VentasPorDia
{
 public class Dat_Detalle_Pedido
    {
        public DataTable GetDetallePedido(string NombreEquipo)
        {
            string query = "[USP_GET_DETALLE_VENTA_DIARIO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NOMEQUIPO", NombreEquipo);
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

        public DataTable GetCuentaxPedido(int id_pedido)
        {
            string query = "[SP_REPORT_CUENTA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COD_PEDIDO", id_pedido);
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
        public DataTable GetCuentaxPedidototal(int id_pedido)
        {
            string query = "[SP_REPORT_CUENTA_TOTAL]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COD_PEDIDO", id_pedido);
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
        public DataTable GetCargarPLatosSelect()
        {
            string query = "[sp_get_select_platos]";
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
        public DataTable GetComandaAnuladaxPedido(int id_pedido)
        {
            string query = "[SP_REPORT_COMANDA_CAJA_ANULADA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COD_PEDIDO", id_pedido);
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
        public DataTable GetComandaPlatoAnuladaxPedido(int id_det_pedido)
        {
            string query = "[SP_REPORT_COMANDA_PLATO_CAJA_ANULADA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COD_DET_PED", id_det_pedido);
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
        public int SetCierreCaja(int operacion, VentasDia ventadia, ref string _mensaje, string NombreEquipo)
        {
            string query = "[USP_SET_CERRAR_CAJA]";
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idusu", ventadia.CJ_id_usu);
                    cmd.Parameters.AddWithValue("@mensaje", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@NOMEQUIPO", NombreEquipo);
                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(cmd.Parameters["@mensaje"].Value);
                    if (cn.State == ConnectionState.Open) cn.Close();

                }
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                result = 0;
            }
            return result;

        }

        #region Dashboard torta
        public DataTable GetPlatoxArea(string NombreEquipo)
        {
            string query = "[USP_GET_DASHBOARD_PLATOSxAREA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NOMEQUIPO", NombreEquipo);
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
        public DataTable GetPlatoxArea_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            string query = "[USP_GET_DASHBOARD_PLATOSxAREA_HISTORIAL]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", @hasta);
                    cmd.Parameters.AddWithValue("@dia", dia);
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

        public DataTable GetVentavsCosto(string NombreEquipo)
        {
            string query = "[USP_GET_DASHBOARD_VENTAvsCOSTO]";
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
        public DataTable GetCanaldeVenta(string NombreEquipo)
        {
            string query = "[USP_GET_DASHBOARD_CANALDEVENTA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NOMEQUIPO", NombreEquipo);
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
        public DataTable GetCanaldeVenta_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            string query = "USP_GET_DASHBOARD_CANALDEVENTA_HISTORIAL";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", @hasta);
                    cmd.Parameters.AddWithValue("@dia", dia);
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
        #endregion
        #region DetallePedido
        public DataTable GetDetProducto(string idemp)
        {
            string query = "USP_GET_RPT_VDC_DET_PROD";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDEMP", idemp);
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
        #endregion
        public DataTable GetPlatosVendidosxImpresora(string desde, string hasta, string dia, int id_impre)
        {
            string query = "[SP_REPORT_PLATO_VENDIDO_X_IMPRESORA]";
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
                    cmd.Parameters.AddWithValue("@id_impre", id_impre);
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
        public DataTable GetCanaldeVentaConsolidado(string NombreEquipo)
        {
            string query = "[USP_GET_CONSOLIDADO_VENTASXCANAL]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NOMEQUIPO", NombreEquipo);
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
        public DataTable GetCanaldeVentaConsolidado_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            string query = "USP_GET_CONSOLIDADO_VENTASXCANAL_HISTORIAL";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", @hasta);
                    cmd.Parameters.AddWithValue("@dia", dia);
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
        public DataTable GetDetallePedido_Historial(DateTime? desde, DateTime? hasta, DateTime? dia)
        {
            string query = "USP_GET_DETALLE_VENTA_DIARIO_HISTORIAL";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", @hasta);
                    cmd.Parameters.AddWithValue("@dia", dia);
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

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
    public class Dat_DpVentaMozo
    {
        public DataTable GetDpVentasMozo(string desde, string hasta,string dia, string idemp /*, string mes, string dia*/)
        {
            string query = "[USP_GET_RPT_VENTAS_MOZO]";
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
                    cmd.Parameters.AddWithValue("@ID", idemp);
                    //cmd.Parameters.AddWithValue("@MES", mes);
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

        public DataTable GetRankingMozo(string desde, string hasta , string dia /*, string mes, string dia*/)
        {
            string query = "[USP_GET_RPT_RANKING_MOZO]";
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
        #region TIPOPAGO
        //public DataTable GetTipoPago(string desde, string hasta)
        //{
        //    string query = "[USP_GET_RPT_TIPO_PAGO]";
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
        //        {
        //            SqlCommand cmd = new SqlCommand(query, cn);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@FECHA_DESDE", desde);
        //            cmd.Parameters.AddWithValue("@FECHA_HASTA", hasta);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(dt);

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        dt = null;
        //       //throw;
        //    }
        //    return dt;

        //}
        #endregion
        #region Detalle pedido
        public DataTable GetDetPedido(string desde, string hasta, string dia, object idped)
        {
            string query = "[USP_GET_RPT_DET_PED_MOZO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FECHA_DESDE", desde);
                    cmd.Parameters.AddWithValue("@FECHA_HASTA", hasta);
                    cmd.Parameters.AddWithValue("@DIA", dia);
                    cmd.Parameters.AddWithValue("@IDPED", idped);
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

        #region Detalle producto
        public DataTable GetDetProducto(string desde, string hasta, string dia, string idemp)
        {
            string query = "[USP_GET_RPT_DET_PROD_MOZO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FECHA_DESDE", desde);
                    cmd.Parameters.AddWithValue("@FECHA_HASTA", hasta);
                    cmd.Parameters.AddWithValue("@DIA", dia);
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
        #region TIPOPAGO
        public DataTable GetTipoPago(string desde, string hasta, string dia, string idemp)
        {
            string query = "[USP_GET_RPT_DET_PAGO_MOZO]";
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
    }
}

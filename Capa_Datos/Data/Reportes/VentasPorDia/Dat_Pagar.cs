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
    public class Dat_Pagar
    {
        public DataTable GetPagoDet(int id_ped)
        {
            string query = "[USP_GET_PAGO_DET_PAGO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_ped);
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

        public bool SetPagoDet(int operacion, Pagar pagar, ref string _mensaje,string nomequipo, DataTable dt_pagos)
        {
            DataTable table;
            string query = "[USP_SET_PAGO_DET_PAGO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID", pagar.id);
                    cmd.Parameters.AddWithValue("@TOTAL ", pagar.totals);
                    cmd.Parameters.AddWithValue("@ABONADO ", pagar.amortizars);
                    cmd.Parameters.AddWithValue("@SALDO ", pagar.saldos);

                    cmd.Parameters.AddWithValue("@ID_PED", pagar.idpedido);
                    cmd.Parameters.AddWithValue("@ID_USU ", pagar.idusuario);
                    cmd.Parameters.AddWithValue("@MONTO ", pagar.monto);
                    cmd.Parameters.AddWithValue("@ID_FPAGO ", pagar.idtpago);
                    cmd.Parameters.AddWithValue("@ID_TMONEDA ", pagar.idtmoneda);
                    cmd.Parameters.AddWithValue("@AMORTIZAR ", pagar.amortizars);
                    cmd.Parameters.AddWithValue("@NOMEQUIPO ", nomequipo);
                    cmd.Parameters.AddWithValue("@IDFORMAPAGO ", pagar.idtpagoPropina);
                    cmd.Parameters.AddWithValue("@MONTOPROPINA ", pagar.monto_propina);
                    cmd.Parameters.AddWithValue("@TB_DET_PAGO", dt_pagos);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                result = false;
            }
            return result;
        }
        public bool SetPagoDetOnline(int operacion, Pagar pagar, ref string _mensaje, DataTable dt_pagos)
        {
            DataTable table;
            string query = "[USP_SET_PAGO_DET_PAGO_ONLINE]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID", pagar.id);
                    cmd.Parameters.AddWithValue("@TOTAL ", pagar.totals);
                    cmd.Parameters.AddWithValue("@ABONADO ", pagar.amortizars);
                    cmd.Parameters.AddWithValue("@SALDO ", pagar.saldos);

                    cmd.Parameters.AddWithValue("@ID_PED", pagar.idpedido);
                    cmd.Parameters.AddWithValue("@ID_USU ", pagar.idusuario);
                    cmd.Parameters.AddWithValue("@MONTO ", pagar.monto);
                    cmd.Parameters.AddWithValue("@ID_FPAGO ", pagar.idtpago);
                    cmd.Parameters.AddWithValue("@ID_TMONEDA ", pagar.idtmoneda);
                    cmd.Parameters.AddWithValue("@AMORTIZAR ", pagar.amortizars);
                    cmd.Parameters.AddWithValue("@TB_DET_PAGO", dt_pagos);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                result = false;
            }
            return result;
        }
        public bool SetAnularPlato(int operacion, Pagar pagar, ref string _mensaje, DataTable dt_id)
        {

            string query = "[USP_SET_PAGO_DET_PAGO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@TB_DET_PAGO", dt_id);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                result = false;
            }
            return result;
        }

        public bool SetAnularPedido(int operacion, Pagar pagar, ref string _mensaje,int idusuarioanulacion)
        {

            string query = "[USP_SET_ANULAR_PEDIDO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID_PED", pagar.idpedido);
                    cmd.Parameters.AddWithValue("@COMENTARIO", (pagar.comentario == null)?"": pagar.comentario);
                    cmd.Parameters.AddWithValue("@ID_USU", idusuarioanulacion);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                result = false;
            }
            return result;
        }
        public bool QuitarDesc(int id_pedido)
        {

            string query = "[Quitar_Desc]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_PED", id_pedido);
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
    }
}
using Capa_Entidades;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Cuentas
{
    public class Dat_Cuentas
    {
        public DataTable sp_get_cuentasx_id_ped(int id_pedido)
        {
            string query = "sp_get_cuentasx_id_ped";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_ped", id_pedido);
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
        public int SetCuenta(DataTable dt, string id_ped, string nom_cuenta, ref string _mensaje)
        {
            string query = "[USP_GENERARCUENTA]";
            int result = 0;
            try
            {

                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TB_CUENTADETALLE", dt);
                    cmd.Parameters.AddWithValue("@idpedido", id_ped);
                    cmd.Parameters.AddWithValue("@nomcuenta", nom_cuenta);
                    cmd.Parameters.AddWithValue("@mensaje", 0).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(cmd.Parameters["@mensaje"].Value);
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                //_mensaje = "Error al generar el pedido, intente nuevamente";
                result = 0;
               

            }
            return result;
        }
        public bool SetPagoDet(int operacion, Pagar pagar, ref string _mensaje, string nomequipo, DataTable dt_pagos, string id_cuenta)
        {
            DataTable table;
            string query = "[USP_SET_PAGO_DET_PAGO_CUENTA]";
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
                    cmd.Parameters.AddWithValue("@id_cuenta ", id_cuenta);
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
        public DataTable GetCuentaxIdCuenta(int id_cuenta)
        {
            string query = "[SP_GET_CUENTA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COD_CUENTA", id_cuenta);
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
        public DataTable sp_verifica_pago(int id_ped)
        {
            string query = "[sp_verifica_pago]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_ped", id_ped);
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
        public bool sp_anular_cuenta(string IdCuenta, ref string _mensaje)
        {
            string query = "[sp_anular_cuenta]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cuenta", IdCuenta);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = true;
                    }

                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                _mensaje = "Error al generar el pedido, intente nuevamente";
                result = false;
            }
            return result;
        }
        public DataTable GetPagoDet(int id_ped,int id_cuent)
        {
            string query = "[USP_GET_PAGO_DET_PAGO_CUENTA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_ped);
                    cmd.Parameters.AddWithValue("@id_cuenta", id_cuent);
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

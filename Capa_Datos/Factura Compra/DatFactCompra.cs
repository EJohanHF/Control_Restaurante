using Capa_Entidades;
using Capa_Entidades.Models.Factura_Compra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Factura_Compra
{
    public class DatFactCompra
    {
        public DataTable GetFacCompra()
        {
            string query = "[USP_GET_DOC_COMPRA]";
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
        public DataTable GetFacCompraDet()
        {
            string query = "[USP_GET_DOC_COMPRA_DET]";
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
        public DataTable GetFacCompraDetTotal(int tipo, DateTime desde, DateTime hasta)
        {
            string query = "[USP_GET_DOC_COMPRA_DET_PRODUCTOS]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TIPO", tipo);
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

        public DataTable GetCondicionPago()
        {
            string query = "[USP_GET_CONDICION_PAGO_FACTURA_COMPRA]";
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


        public DataTable GetFacCompraTipoDoc()
        {
            string query = "SP_GET_DOC_ELECTRONICO_COMPRA";
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
        public bool SetFactCompra(int operacion, FactCompra factCom, DataTable dt, ref string _mensaje)
        {
            string query = "[USP_SET_FACTURA_COMPRA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID", factCom.ID);
                    cmd.Parameters.AddWithValue("@FC_TIP_DOC", factCom.FC_TIP_DOC);
                    cmd.Parameters.AddWithValue("@FC_SER_NRO", factCom.FC_SER_NRO);
                    cmd.Parameters.AddWithValue("@FC_F_EMISION", factCom.FC_F_EMISION);
                    cmd.Parameters.AddWithValue("@FC_TIP_MONEDA", "");
                    cmd.Parameters.AddWithValue("@FC_TOTAL_OP_GRABADAS", factCom.FC_TOTAL_OP_GRABADAS);
                    cmd.Parameters.AddWithValue("@FC_SUMA_IGV", factCom.FC_SUMA_IGV);
                    cmd.Parameters.AddWithValue("@FC_IMPORTE_TOTAL_COMPRA", factCom.FC_IMPORTE_TOTAL_COMPRA);
                    cmd.Parameters.AddWithValue("@FC_TOTAL_DESC", factCom.FC_TOTAL_DESC);
                    cmd.Parameters.AddWithValue("@FC_DESCUENTOS_GLOBALES", factCom.FC_DESCUENTOS_GLOBALES);
                    cmd.Parameters.AddWithValue("@FC_MONTO_LETRA", "");
                    cmd.Parameters.AddWithValue("@FC_ID_PROVEEDOR", factCom.FC_ID_PROVEEDOR);
                    cmd.Parameters.AddWithValue("@FC_ESTADO_DOC", factCom.FC_ESTADO_DOC);
                    cmd.Parameters.AddWithValue("@FC_VENCIMIENTO", factCom.FC_VENCIMIENTO);
                    cmd.Parameters.AddWithValue("@FC_TOTAL_PAGADO", factCom.FC_TOTAL_PAGADO);
                    cmd.Parameters.AddWithValue("@FC_CONDICION_PAGO", factCom.FC_CONDICION_PAGO);
                    cmd.Parameters.AddWithValue("@TABLE", dt);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
            }
            return result;
        }
        public bool SetSetProductos(int operacion, int id,int id_usuario ,ref string _mensaje)
        {
            string query = "[USP_SET_INSUMOS_X_FACTCOMPRA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@ID_USU", id_usuario);
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
        public DataTable GetFacCompraEstado()
        {
            string query = "[USP_GET_DOC_COMPRA_ESTADO]";
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

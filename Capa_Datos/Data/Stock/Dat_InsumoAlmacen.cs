using Capa_Entidades;
using Capa_Entidades.Models.Stock;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Data.Stock
{
 public class Dat_InsumoAlmacen
    {
        public bool SetInsumo_Almancen(int operacion, InsumoAlmacen InsumoAlmacen, ref string _mensaje)
        {
            string query = "[USP_SET_LLENAR_ALMACEN]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idalm", InsumoAlmacen.id_almacen);
                    cmd.Parameters.AddWithValue("@idins", InsumoAlmacen.id_insumo);
                    cmd.Parameters.AddWithValue("@cant", InsumoAlmacen.cantidad);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                _mensaje = "Acción no realizada";
                result = false;
            }
            return result;
        }
        public bool SetMovimiento_Insumo_Almancen(int operacion, InsumoAlmacen InsumoAlmacen, ref string _mensaje)
        {
            string query = "[USP_SET_MOVIMIENTO_ENTRE_ALMACEN]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idalm", InsumoAlmacen.id_almacen);
                    cmd.Parameters.AddWithValue("@idalmentrada", InsumoAlmacen.id_almacensal);
                    cmd.Parameters.AddWithValue("@idins", InsumoAlmacen.id_insumo);
                    cmd.Parameters.AddWithValue("@cant", InsumoAlmacen.cantidad);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception)
            {
                _mensaje = "Acción no realizada";
                result = false;
            }
            return result;
        }
        public DataTable GetTipoMovimientoInsumo()
        {
            string query = "[USP_GET_TIPO_MOV_INS]";
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
        public DataTable GetMovimientoInsumoAlmacen()
        {
            string query = "[USP_GET_MOVIMIENTO_INSUMO]";
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
        public DataTable GetCierresInsumos()
        {
            string query = "[USP_GET_CIERRE_INSUMO]";
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
    }
}

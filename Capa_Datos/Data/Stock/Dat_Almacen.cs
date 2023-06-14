using Capa_Entidades;
using Capa_Entidades.Models;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Stock;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Stock
{
  public  class Dat_Almacen
    {
        public DataTable GetAlmacen()
        {
            string query = "[USP_GET_ALMACEN]";
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
        public DataTable GetConsumoInterno()
        {
            string query = "[USP_GET_CONSUMO_INTERNO]";
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
        public DataTable GetConsumoInternoFecha(DateTime? desde, DateTime? hasta)
        {
            string query = "[USP_GET_CONSUMO_INTERNO_FECHA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", hasta);
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
        public DataTable GetTipoConsumo()
        {
            string query = "[USP_GET_TIPO_CONSUMO]";
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
        public bool SetAlmancen(int operacion, Almacen almacen, ref string _mensaje)
        {
            string query = "USP_SET_ALMACEN";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idalm", almacen.idalm);
                    cmd.Parameters.AddWithValue("@ALM_NOM", almacen.nomalm);
                    cmd.Parameters.AddWithValue("@ALM_ACT ", almacen.estadoalm);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception )
            {
                _mensaje = ""+almacen.nomalm+" está en uso, desvincule para eliminar";
                result = false;
            }
            return result;
        }
        public bool SetConsumoInterno(int operacion, ConsumoInterno ci, Empleado emp, TipoConsumo tc, Platos platos, Insumos ins, decimal cant_plato, decimal cant_ins, int idalm, string obs)
        {
            string query = "[USP_SET_CONSUMO_INTERNO]";
            bool result = false;
            try
            {
                int idusu = Convert.ToInt32(System.Windows.Application.Current.Properties["IdUsuario"]);
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID", ci.ID);
                    cmd.Parameters.AddWithValue("@CI_ID_EMPLEADO", emp.id);
                    cmd.Parameters.AddWithValue("@CI_ID_TIPO_CONSUMO ", tc.ID);
                    cmd.Parameters.AddWithValue("@CI_CANT_PLATO ", cant_plato);
                    cmd.Parameters.AddWithValue("@CI_CANT_INS ", cant_ins);
                    cmd.Parameters.AddWithValue("@CI_ID_ALM ", idalm);
                    cmd.Parameters.AddWithValue("@CI_ID_CARTA ", platos.idplato);
                    cmd.Parameters.AddWithValue("@CI_ID_INSUMO ", ins.idins);
                    cmd.Parameters.AddWithValue("@CI_OBS ", obs);
                    cmd.Parameters.AddWithValue("@ID_USU ", idusu);
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

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
  public  class Dat_BoletaFactura
    {
        public DataTable GetBoletaFactura(int id_pedido)
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
        //public DataTable GetBoletaFactura2()
        //{
        //    string query = "[SP_REPORT_CUENTA]";
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
        //        {
        //            SqlCommand cmd = new SqlCommand(query, cn);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@COD_PEDIDO", id_pedido);
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
    }
}

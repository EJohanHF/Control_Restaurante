using Capa_Entidades;
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
  public  class Dat_UnidadMedida
    {
        public DataTable GetUnidadMedida()
        {
            string query = "[USP_GET_UNIDAD_MEDIDA]";
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
        public bool SetUnidadMedida(int operacion, UnidadMedida unidadmedida, ref string _mensaje)
        {
            string query = "[USP_SET_UNIDAD_MEDIDA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idumed", unidadmedida.idum);
                    cmd.Parameters.AddWithValue("@TM_DESC", unidadmedida.descum);
                    cmd.Parameters.AddWithValue("@TM_DENOM", unidadmedida.denom);
                    cmd.Parameters.AddWithValue("@TM_ESTADO", unidadmedida.estadoum);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception)
            {
                _mensaje = "" + unidadmedida.descum + " está en uso, desvincule para eliminar";
                result = false;
            }
            return result;
        }
    }
}

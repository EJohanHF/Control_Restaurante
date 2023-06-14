using Capa_Entidades;
using Capa_Entidades.Models.Configuracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Configuracion
{
    public class Dat_Descuento
    {
      
        public DataTable GetDescuento()
        {
            string query = "[USP_GET_DESCUENTO]";
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
        public bool SetDescuento(int operacion, Descuento Descuentosep, ref string _mensaje)
        {
            string query = "[USP_SET_TIPO_DESCUENTO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@id", Descuentosep.id);
                    cmd.Parameters.AddWithValue("@TD_DESCR", Descuentosep.TD_DESCR);
                    cmd.Parameters.AddWithValue("@TD_ACT", Descuentosep.TD_ACT);
                    cmd.Parameters.AddWithValue("@TD_PORCENTAJE", Descuentosep.TD_PORCENTAJE);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception)
            {
                _mensaje = "¡" + Descuentosep.id + " está en uso, desvincule para eliminar!";
                result = false;
            }
            return result;
        }
    }
}

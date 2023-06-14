using Capa_Entidades;
using Capa_Entidades.Models.Carta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Carta
{
    public class Dat_DetDescripciones
    {
        public DataTable GetDetDescripciones()
        {
            string query = "[USP_GET_SF_DET_DESCRIPTION]";
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
        public bool SetDetDescripciones(int operacion, DetDescripciones detdescripciones, ref string _mensaje)
        {
            string query = "[USP_SET_DET_DESCRIPTION]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@id", detdescripciones.ID);
                    cmd.Parameters.AddWithValue("@DET_DESCRIPTION", detdescripciones.DET_DESCRIPTION);
                    cmd.Parameters.AddWithValue("@DP_ID_DESC", detdescripciones.DP_ID_DESC);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception)
            {
                _mensaje = "" + detdescripciones.ID + " está en uso, desvincule para eliminar";
                result = false;
            }
            return result;
        }
    }
}

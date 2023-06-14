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
    public class Dat_Descripciones
    {
        public DataTable GetDescripciones()
        {
            string query = "[USP_GET_SF_TITLE_DESCRIPTION]";
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
        public bool SetDescripciones(int operacion, Descripciones descripciones, ref string _mensaje)
        {
            string query = "[USP_SET_TITLE_DESCRIPTION]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@id", descripciones.ID);
                    cmd.Parameters.AddWithValue("@TITLE_DESCRIPTION", descripciones.TITLE_DESCRIPTION);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception)
            {
                _mensaje = "" + descripciones.TITLE_DESCRIPTION + " está en uso, desvincule para eliminar";
                result = false;
            }
            return result;
        }
    }
}

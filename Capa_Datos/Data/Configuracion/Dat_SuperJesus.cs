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
    public class Dat_SuperJesus
    {
        public DataTable GetSuperJesus()
        {
            string query = "[USP_GET_TIPO_PAGO]";
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
        public bool SetSuperJesus(int operacion, SJesus superjesus, ref string _mensaje)
        {
            string query = "[USP_SET_SUP_TIPO_PAGO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@id", superjesus.id);
                    cmd.Parameters.AddWithValue("@deno_pago", superjesus.deno_pago);
                    cmd.Parameters.AddWithValue("@tp_act", superjesus.tp_act);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception)
            {
                _mensaje = "" + superjesus.id + " está en uso, desvincule para eliminar";
                result = false;
            }
            return result;
        }
    }
}

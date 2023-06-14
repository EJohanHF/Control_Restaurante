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
    public class Dat_TipoCaja
    {

        public DataTable GetTipoCaja()
        {
            string query = "[USP_GET_MOV_CAJA_CHICA]";
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
        public bool SetTipoCaja(int operacion, TipoCaja Tipocajasep, ref string _mensaje)
        {
            string query = "[USP_SET_TIPO_CAJACHICA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@id", Tipocajasep.id);
                    cmd.Parameters.AddWithValue("@MC_DESCR", Tipocajasep.MC_DESCR);
                    cmd.Parameters.AddWithValue("@MC_ACT", Tipocajasep.MC_ACT);
                    cmd.Parameters.AddWithValue("@MC_ID_TIPO", Tipocajasep.MC_ID_TIPO);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception)
            {
                _mensaje = "está en uso, desvincule para eliminar!";
                result = false;
            }
            return result;
        }
    }
}

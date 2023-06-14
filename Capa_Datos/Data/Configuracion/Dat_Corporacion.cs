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
   public class Dat_Corporacion
    {
        public DataTable GetCorporacion()
        {
            string query = "[USP_GET_CORPORACION]";
            DataTable dt = new DataTable()        ;
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
        public bool SetCorporacion(int operacion, Corporacion corporacion, ref string _mensaje)
        {
            string query = "[USP_SET_CORPORACION]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query,cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idcorp", corporacion.id);
                    cmd.Parameters.AddWithValue("@nomcorpo", corporacion.corp_nom);
                    //cmd.Parameters.AddWithValue("@fcreate", corporacion.corp_f_create);
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
    }
}

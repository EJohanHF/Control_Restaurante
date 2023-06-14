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
    public class Dat_Proveedor
    {
        public DataTable GetProveedor()
        {
            string query = "[USP_GET_PROVEEDOR]";
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
        public bool SetProveedor(int operacion, Proveedor proveedor, ref string _mensaje)
        {
            string query = "[USP_SET_PROVEEDOR]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idp", proveedor.idp);
                    cmd.Parameters.AddWithValue("@P_ID_TIPO_DOC", proveedor.iddoc);
                    cmd.Parameters.AddWithValue("@P_NRO_DOC", proveedor.prov_nrdoc);
                    cmd.Parameters.AddWithValue("@P_NOM", proveedor.prov_nom);
                    cmd.Parameters.AddWithValue("@P_DIREC", proveedor.prov_direc);
                    cmd.Parameters.AddWithValue("@P_DIST", proveedor.prov_dist);
                    cmd.Parameters.AddWithValue("@P_TEL_FIJO", proveedor.prov_telfijo);
                    cmd.Parameters.AddWithValue("@P_TEL_MOV", proveedor.prov_telmovil);
                    cmd.Parameters.AddWithValue("@P_COR", proveedor.prov_correo);
                    cmd.Parameters.AddWithValue("@P_ACT", proveedor.prov_activo);
                    cmd.Parameters.AddWithValue("@P_RUBRO", proveedor.prov_rubro);
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

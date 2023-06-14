
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Capa_Entidades;
using Capa_Entidades.Models;


namespace Capa_Datos.Configuracion
{
    public class Dat_Empleados
    {
        public DataTable GetEmpleados()
        {
            string query = "[USP_GET_EMPLEADOS]";
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
        public bool SetEmpleados(int operacion ,Empleado empleado , ref string _mensaje )
        {
            string query = "[USP_SET_EMPLEADOS]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@id", empleado.id);
                    cmd.Parameters.AddWithValue("@EMPL_NOM", empleado.nombres);
                    cmd.Parameters.AddWithValue("@EMPL_APE", empleado.apellidos);
                    cmd.Parameters.AddWithValue("@EMPL_NRO_DOC", empleado.nroDocumento);
                    cmd.Parameters.AddWithValue("@EMPL_EST", empleado.estado);
                    cmd.Parameters.AddWithValue("@EMPL_ID_CARGO", empleado.idcargo);
                    cmd.Parameters.AddWithValue("@EMPL_GEN", empleado.genero);
                    cmd.Parameters.AddWithValue("@TIPODI", empleado.idTipoDI);
                    cmd.Parameters.AddWithValue("@EMPL_F_NAC", empleado.fecNacimiento);
                    cmd.Parameters.AddWithValue("@clave", empleado.clave);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                _mensaje = "No se pudo completar el registro, verifique que la clave de mozo no exista o complete todos los campos";
                result = false;                
            }
            return result;
        }
    }
    
}

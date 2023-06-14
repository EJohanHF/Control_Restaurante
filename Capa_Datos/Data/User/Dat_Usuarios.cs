using Capa_Entidades;
using Capa_Entidades.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.User
{
    public class Dat_Usuarios
    {
        public DataTable GetUsuario()
        {
            string query = "[USP_GET_USUARIO]";
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
        public DataTable getUsuariosActivos()
        {
            string query = "[USP_GET_USUARIO_ACTIVOS]";
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
        public DataTable GetUsuarioActivo()
        {
            string query = "[sp_validar_sesion_usu]";
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
        public bool SetUsuario(int operacion, Usuario usuario, ref string _mensaje)
        {
            string query = "[USP_SET_USUARIO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idusu", usuario.idusu);
                    cmd.Parameters.AddWithValue("@usu_nom", usuario.nomusu);
                    cmd.Parameters.AddWithValue("@usu_ape", usuario.apeusu);
                    cmd.Parameters.AddWithValue("@usu_est ", usuario.estadousu);
                    cmd.Parameters.AddWithValue("@usu_clave", usuario.claveusu_cambio);
                    cmd.Parameters.AddWithValue("@idemp ", usuario.idemple);
                    cmd.Parameters.AddWithValue("@idrol ", usuario.idrol);
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
        public bool SetUsuarioUpdateEstado(int operacion, Usuario usuario, ref string _mensaje)
        {
            string query = "[USP_SET_USU_ESTADO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idusu", usuario.idusu);
                    cmd.Parameters.AddWithValue("@newpass", (usuario.claveusu_cambio) == "" ? null : usuario.claveusu_cambio);
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

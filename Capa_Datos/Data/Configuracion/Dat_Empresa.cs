using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Capa_Entidades;
using Capa_Entidades.Models;
using Capa_Entidades.Models.Configuracion;

namespace Capa_Datos.Configuracion
{
    public class Dat_Empresa
    {
        public DataTable GetEmpresa()
        {
            string query = "[USP_GET_EMPRESA]";
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
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;

        }
        public DataTable GetEmpresaCorreo(int idemp)
        {
            string query = "[USP_GET_EMPRESA_CORREOS]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDCOR", idemp);
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
        public bool SetEmpresa(int operacion, Empresa empresa, ref string _mensaje)
        {
            string query = "[USP_SET_EMPRESA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idEmp", empresa.idEmpr);
                    cmd.Parameters.AddWithValue("@empr_ruc", empresa.empr_ruc);
                    cmd.Parameters.AddWithValue("@empr_nom", empresa.empr_nom);
                    cmd.Parameters.AddWithValue("@empr_nom_com", empresa.empr_nom_com);
                    cmd.Parameters.AddWithValue("@empr_tel", empresa.empr_tel);
                    cmd.Parameters.AddWithValue("@empr_cor", empresa.empr_cor);
                    cmd.Parameters.AddWithValue("@idpais", empresa.idpais);
                    cmd.Parameters.AddWithValue("@empr_ubig", empresa.empr_ubig);
                    cmd.Parameters.AddWithValue("@iddepa", empresa.iddepa);
                    cmd.Parameters.AddWithValue("@idprov", empresa.idprov);
                    cmd.Parameters.AddWithValue("@iddist", empresa.iddist);
                    cmd.Parameters.AddWithValue("@empr_urb", empresa.empr_urb);
                    cmd.Parameters.AddWithValue("@empr_logo", empresa.empr_logo);
                    cmd.Parameters.AddWithValue("@idcorp", empresa.idcorp);
                    cmd.Parameters.AddWithValue("@empr_firma", empresa.empr_firma);
                    cmd.Parameters.AddWithValue("@empr_firma_alias", empresa.empr_firma_alias);
                    cmd.Parameters.AddWithValue("@empr_firma_clave_alias", empresa.empr_firma_clave_alias);
                    cmd.Parameters.AddWithValue("@empr_firma_usr_sol", empresa.empr_firma_clave_sol);
                    cmd.Parameters.AddWithValue("@empr_firma_clave_sol", empresa.empr_firma_clave_sol);
                    cmd.Parameters.AddWithValue("@empr_direc", empresa.empr_direc);
                    cmd.Parameters.AddWithValue("@empr_ruta_fact", empresa.empr_ruta_facelect);
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
        public bool ActualizarLicencia (string codigo)
        {
            string query = "usp_updatelicencia";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@value", codigo);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();

                }
            }
            catch (Exception ex)
            {
               
                result = false;
            }
            return result;
        }
        public bool SetEmpresaCorreo(int operacion, Empresa empresa, ref string _mensaje, DataTable dtCorreo)
        {
            string query = "[USP_SET_EMPRESA_CORREOS]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idEmp", empresa.idEmpr);
                    cmd.Parameters.AddWithValue("@TB_EMPRESA_CORREOS", dtCorreo);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Capa_Entidades.Acceso;
using System.Data.SqlClient;
using System.Data;
using Capa_Entidades;

namespace Capa_Datos.Acceso
{
    public class Dat_Usuario
    {
        public string conexion_sosfood = ConfigurationManager.ConnectionStrings["conexion_sosfood"].ConnectionString;

        public Ent_Usuario login(int id)
        {
            DataTable dt_usuario = new DataTable();

            Ent_Usuario ent_usu = null;

            string query = "USP_LOGIN_UPDATE";

            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    //cmd.Parameters.AddWithValue("@PASS", pass);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt_usuario);

                    if (dt_usuario.Rows.Count > 0)
                    {
                        ent_usu = new Ent_Usuario();
                        ent_usu.id = Convert.ToInt32(dt_usuario.Rows[0]["ID"].ToString());
                        ent_usu.usu_nom = dt_usuario.Rows[0]["USU_NOM"].ToString();
                        ent_usu.usu_pass = dt_usuario.Rows[0]["USU_CLAVE"].ToString();
                        ent_usu.estadopriv = Convert.ToByte(dt_usuario.Rows[0]["PRIVILEGIO_ROL"]);
                    }
                    else
                    {
                        ent_usu = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ent_usu = null;
            }
            return ent_usu;
        }

        public Ent_Usuario VerificaEmpleado(string password)
        {
            DataTable dt_usuario = new DataTable();

            Ent_Usuario ent_usu = null;

            string query = "USP_VERIFICA_EMPLEADO";

            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PASSWORD", password);
                    //cmd.Parameters.AddWithValue("@PASS", pass);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt_usuario);

                    if (dt_usuario.Rows.Count > 0)
                    {
                        ent_usu = new Ent_Usuario();
                        ent_usu.id = Convert.ToInt32(dt_usuario.Rows[0]["ID"].ToString());
                        ent_usu.id_empl = Convert.ToInt32(dt_usuario.Rows[0]["USU_ID_EMPL"].ToString());
                        ent_usu.usu_nom = dt_usuario.Rows[0]["EMPL_NOM"].ToString();
                        ent_usu.usu_pass = dt_usuario.Rows[0]["CLAVE"].ToString();
                    }
                    //else
                    //{
                    //    ent_usu = null;
                    //}
                }
            }
            catch (Exception)
            {
                ent_usu = null;
            }
            return ent_usu;
        }
        //public Ent_Usuario login(string usuario, string pass)
        //{
        //    DataTable dt_usuario = new DataTable();

        //    Ent_Usuario ent_usu = null;

        //    string query = "USP_LOGIN";

        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
        //        {
        //            SqlCommand cmd = new SqlCommand(query, cn);

        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@USUARIO", usuario);
        //            cmd.Parameters.AddWithValue("@PASS", pass);

        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(dt_usuario);

        //            if (dt_usuario.Rows.Count > 0)
        //            {
        //                ent_usu = new Ent_Usuario();
        //                ent_usu.usu_nom = dt_usuario.Rows[0]["USU_NOM"].ToString();
        //                ent_usu.usu_pass = dt_usuario.Rows[0]["USU_CLAVE"].ToString();
        //                //ent_usu.usu_ape = dt_usuario.Rows[0]["USU_APE"].ToString();
        //            }
        //            else
        //            {
        //                ent_usu = null;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ent_usu = null;
        //    }
        //    return ent_usu;
        //}

        public Ent_Usuario login2(string idusuario, string nomequipo, string ip)
        {
            DataTable dt_usuario = new DataTable();

            Ent_Usuario ent_usu = null;

            string query = "USP_LOGIN";

            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_USUARIO", idusuario);
                    cmd.Parameters.AddWithValue("@NOMEQUIPO", nomequipo);
                    cmd.Parameters.AddWithValue("@IP", ip);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt_usuario);

                    if (dt_usuario.Rows.Count > 0)
                    {
                        ent_usu = new Ent_Usuario();
                        ent_usu.id = Convert.ToInt32(dt_usuario.Rows[0]["ID"].ToString());
                        ent_usu.usu_nom = dt_usuario.Rows[0]["USU_NOM"].ToString();
                        ent_usu.usu_pass = dt_usuario.Rows[0]["USU_CLAVE"].ToString();
                        ent_usu.idrol = Convert.ToInt32(dt_usuario.Rows[0]["ID_ROL"].ToString());
                        ent_usu.id_empl = Convert.ToInt32(dt_usuario.Rows[0]["USU_ID_EMPL"].ToString());
                        //ent_usu.usu_ape = dt_usuario.Rows[0]["USU_APE"].ToString();
                    }
                    else
                    {
                        ent_usu = null;
                    }
                }
            }
            catch (Exception)
            {
                ent_usu = null;
            }
            return ent_usu;
        }
        public bool ActualizarEstadoLogueado(string id)
        {
            string query = "[sp_actualizar_est_log]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = true;
                    }

                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Capa_Entidades.Acceso;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos.Acceso
{

    public class MantenimientoCorporacion
    {
        public string conexion_sosfood = ConfigurationManager.ConnectionStrings["conexion_sosfood"].ConnectionString;
        Ent_Corporacion ent_cor = null;

        public Ent_Corporacion D_Registrar(string nombre)
        {
        try{

            using (SqlConnection cn = new SqlConnection(conexion_sosfood))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("RegistrarCorporacion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
        }
        catch (Exception){}
            return ent_cor;
        }

        public Ent_Corporacion D_Actualizar(string nombre,int id)
        {
            try
            {

                using (SqlConnection cn = new SqlConnection(conexion_sosfood))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("ActualizarCorporacion", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (Exception) { }
            return ent_cor;
        }

        public Ent_Corporacion D_Eliminar(int id)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion_sosfood))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("EliminarCorporacion", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id",id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (Exception) { }
            return ent_cor;
        }



        /*public  void  InsertarCorp(string nombre)
        {
            try { 
                using (SqlConnection cn = new SqlConnection(conexion_sosfood)) {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("RegistrarCorporacion", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cn.Close();
                }
            }
                catch (Exception) {}
        }*/

        public DataTable listar()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion_sosfood))
                {
                    SqlCommand cmd = new SqlCommand("ListarCorporacion", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    return dt;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

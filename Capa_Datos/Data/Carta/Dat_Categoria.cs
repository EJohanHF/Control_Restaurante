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
   public class Dat_Categoria
    {
        public DataTable GetCategoria()
        {
            string query = "[USP_GET_CATEGORIA_CARTA]";
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
        public DataTable GetCategoriaTodos()
        {
            string query = "USP_GET_CATEGORIA_CARTA_TODO";
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
        public DataTable GetCategoriaxSuperCategoria(int idsuper)
        {
            string query = "[USP_GET_CATEGORIA_CARTA_SUPER]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_SUPERCATEGORIA", idsuper);//@idcat
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
        public bool SetCategoria(int operacion, Categoria categoria, ref string _mensaje)
        {
            string query = "[USP_SET_CATEGORIA_CARTA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);//@idcat
                    cmd.Parameters.AddWithValue("@idcat", categoria.idcat);
                    cmd.Parameters.AddWithValue("@idscat", categoria.idscat);
                    cmd.Parameters.AddWithValue("@cat_nom", categoria.nomcat);
                    cmd.Parameters.AddWithValue("@cat_desc", (categoria.desccat == null)?0: categoria.desccat);
                    cmd.Parameters.AddWithValue("@cat_image", categoria.imagencat);
                    cmd.Parameters.AddWithValue("@cloud", categoria.cloud);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                _mensaje = "" + categoria.idcat + " está en uso, desvincule para eliminar";
                result = false;
            }
            return result;
        }
        public bool setImpresoraCartaxCat(Categoria categoria)
        {
            string query = "[USP_SET_IMPRESORA_X_CATEGORIA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@impresora", categoria.impresoras);
                    cmd.Parameters.AddWithValue("@id_cat", categoria.idcat);
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
        public bool AumentarPrecioxCategoria(Categoria categoria,decimal cantidad)
        {
            string query = "[USP_SET_CAMBIAR_PRECIOXCATEGORIA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@op", 1);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@id_cat", categoria.idcat);
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
        public bool DisminuirPrecioxCategoria(Categoria categoria, decimal cantidad)
        {
            string query = "[USP_SET_CAMBIAR_PRECIOXCATEGORIA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@op", 2);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@id_cat", categoria.idcat);
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
        public DataTable GetCatCloud()
        {
            string query = "[USP_GET_CATEGORIA_CLOUD]";
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
        public int sp_cloud_update()
        {
            string query = "[sp_cloud_update_categoria]";
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = 1;
                    }
                    if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
    }
}

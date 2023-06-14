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
    public class Dat_Plato
    {
        public DataTable GetPlatos()
        {
            string query = "[USP_GET_CARTA]";
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
        public DataTable getPlatosCombo(int idgrup)
        {
            string query = "USP_GET_PLATOS_COMBO";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idgrup", idgrup);
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
        public DataTable GetPlatosCloud()
        {
            string query = "[USP_GET_CARTA_CLOUD]";
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
        public DataTable GetPlatosActivo()
        {
            string query = "USP_GET_CARTA_ACTIVO";
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
        public DataTable GetPlatosxGrupo(int idgrupo)
        {
            string query = "USP_GET_CARTA_GRUPO";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COD_GRUPOCARTA", idgrupo);
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
        public int SetPlato(int operacion, Platos platos, ref string _mensaje)
        {
            string query = "[USP_SET_CARTA]";
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@idcart", platos.idplato);
                    cmd.Parameters.AddWithValue("@idcat", platos.idcat);
                    cmd.Parameters.AddWithValue("@idgrup", platos.idgrup);
                    cmd.Parameters.AddWithValue("@nom_cart", platos.nomplato);
                    cmd.Parameters.AddWithValue("@prec_cart", platos.precplato);
                    cmd.Parameters.AddWithValue("@nivel_cart", platos.id_niveles);
                    cmd.Parameters.AddWithValue("@est_cart", platos.estadoplato);
                    cmd.Parameters.AddWithValue("@porcion_cart", platos.porcionplato);
                    cmd.Parameters.AddWithValue("@idproditem", (platos.idproditem == "" ? null : platos.idproditem));
                    cmd.Parameters.AddWithValue("@idscat", platos.idscat);
                    cmd.Parameters.AddWithValue("@impresoras", platos.impresoras);
                    cmd.Parameters.AddWithValue("@rc", platos.RC);
                    cmd.Parameters.AddWithValue("@exonerada", platos.exonerada);
                    cmd.Parameters.AddWithValue("@mensaje", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@CAR_PRECIO_DEL", platos.precplato_delivery);
                    cmd.Parameters.AddWithValue("@CAR_ESTADO_DEL", platos.estadoplato_delivery);
                    cmd.Parameters.AddWithValue("@CAR_IMG", platos.imgplato);
                    cmd.Parameters.AddWithValue("@CLOUD", 1);
                    cmd.Parameters.AddWithValue("@CAR_COD_BAR", platos.cbarplato);
                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(cmd.Parameters["@mensaje"].Value);
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                string mensaje = "";
                if (operacion == 1)
                {
                    mensaje = "No se pudo insertar el plato, asegurese de llenar los datos correctos";
                }
                else if (operacion == 2)
                {
                    mensaje = "No se pudo actualizar el plato, asegurese de llenar los datos correctos";
                }
                else if (operacion == 3) {
                    mensaje = "No se pudo eliminar el plato, asegurese de que el plato no tenga asignada una venta anterior";
                }
                _mensaje = mensaje;
                result = 0;
            }
            return result;
        }

        public DataTable getImpresoraPlato(string id)
        {
            string query = "[USP_GET_IMPRESORA_PLATO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cod_prod", id);
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
        public DataTable getDescPlatoGrupo(int id)
        {
            string query = "[sp_desc_grupo]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcarta", id);
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
        public DataTable GetPedidoPendienteImprimir()
        {
            string query = "[GetPedidoPendienteImprimir]";
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
        public DataTable GetCuentaPendienteImprimir()
        {
            string query = "[sp_get_cuenta_pedido]";
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
        public DataTable Get_ImpresoraxDocumento(int ID_DOCUMENTO)
        {
            string query = "[USP_GET_IMPRESORAxDOCUMENTO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_DOC", ID_DOCUMENTO);
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
        public DataTable Get_Documentos()
        {
            string query = "[USP_GET_DOCUMENTOS]";
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

        public int SetDocumentos(int operacion, Documentos documentos)
        {
            string query = "[USP_SET_DOCUMENTOS]";
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID_DOC", documentos.ID);
                    cmd.Parameters.AddWithValue("@impresoras", documentos.impresoras);
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
        public int sp_cloud_update()
        {
            string query = "[sp_cloud_update]";
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

        public DataTable GetPlatosMasVendidos()
        {
            string query = "[USP_GET_CARTA_MAS_VENDIDOS]";
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
    }
}

using Capa_Entidades;
using Capa_Entidades.Models.Delivery_Web_Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Delivery_Web_Service
{
    public class Dat_DeliveryWebService
    {
        MEnsaje mensaje = new MEnsaje();
        public DataTable sp_get_pedido_delivery()
        {
            string query = "[sp_get_pedido_delivery]";
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
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;

        }
        public DataTable sp_get_pedido_recojo()
        {
            string query = "[sp_get_pedido_recojo]";
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
               // mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;

        }
        public DataTable sp_get_pedido_mesa()
        {
            string query = "[sp_get_pedido_mesa]";
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
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;

        }
        public DataTable sp_get_pedido_mesa_total()
        {
            string query = "[sp_get_pedido_mesa_total]";
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
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;

        }

        public DataTable sp_get_pedido_recojo_x_fecha(DateTime f1, DateTime f2)
        {
            string query = "[sp_get_pedido_recojo_x_fecha]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@f1", f1);
                    cmd.Parameters.AddWithValue("@f2", f2);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;

        }
        public DataTable sp_get_pedido_mesa_x_fecha(DateTime f1, DateTime f2)
        {
            string query = "[sp_get_pedido_mesa_x_fecha]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@f1", f1);
                    cmd.Parameters.AddWithValue("@f2", f2);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;

        }
        public DataTable sp_get_pedido_delivery_x_fecha(DateTime f1,DateTime f2)
        {
            string query = "[sp_get_pedido_delivery_x_fecha]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@f1", f1);
                    cmd.Parameters.AddWithValue("@f2", f2);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                }
            }
            catch (Exception ex)
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;

        }
        public bool sp_insert_pedido(DeliveryWebService datadelivery)
        {
            string query = "[sp_insert_pedido]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", datadelivery.cod_orden);
                    cmd.Parameters.AddWithValue("@nombre", datadelivery.fname);
                    cmd.Parameters.AddWithValue("@telefono", datadelivery.mobile);
                    cmd.Parameters.AddWithValue("@direccion", datadelivery.address);
                    cmd.Parameters.AddWithValue("@estado", datadelivery.status);
                    cmd.Parameters.AddWithValue("@fecha_pedido", datadelivery.date);
                    cmd.Parameters.AddWithValue("@total", datadelivery.total);
                    cmd.Parameters.AddWithValue("@idpedido", datadelivery.idpedido);
                    cmd.Parameters.AddWithValue("@idempresa", datadelivery.id_business);
                    cmd.Parameters.AddWithValue("@latitud", datadelivery.latitud);
                    cmd.Parameters.AddWithValue("@longitud", datadelivery.longitud);
                    cmd.Parameters.AddWithValue("@tipo_doc_electronico", datadelivery.tipo_doc_electronico);
                    cmd.Parameters.AddWithValue("@tipo_doc_identidad", datadelivery.tipo_doc_identidad);
                    cmd.Parameters.AddWithValue("@nro_identidad_cliente", datadelivery.nro_identidad_cliente);
                    cmd.Parameters.AddWithValue("@deno_cliente", datadelivery.deno_cliente);
                    cmd.Parameters.AddWithValue("@tipo_entrega", datadelivery.deno_entrega);
                    cmd.Parameters.AddWithValue("@nombre_mesa", datadelivery.nombre_mesa);
                    cmd.Parameters.AddWithValue("@token", datadelivery.token);
                    cmd.Parameters.AddWithValue("@tipo_sosfood", datadelivery.tipo_sosfood);
                    cmd.Parameters.AddWithValue("@metodo_pago", datadelivery.metodo_pago);
                    //cmd.Parameters.AddWithValue("@paga_con", datadelivery.paga_con);
                    //cmd.Parameters.AddWithValue("@vuelto", datadelivery.vuelto);
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
        public bool sp_aceptar_pedido(int id)
        {
            string query = "[sp_aceptar_pedido]";
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
        public bool sp_rechazar_pedido(int id)
        {
            string query = "[sp_rechazar_pedido]";
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
        public bool sp_delivery_pedido(int id, int idpedido)
        {
            string query = "[sp_delivery_pedido]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@idpedido", idpedido);
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
        public bool sp_cambiar_estado_pedido_delivery(int id, string estado)
        {
            string query = "[sp_cambiar_estado_pedido_delivery]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@estado", estado);
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
        public bool sp_cambiar_mesa_pedido_delivery(int id, string cod_mesa)
        {
            string query = "[sp_cambiar_mesa_pedido_delivery]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@cod_mesa", cod_mesa);
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

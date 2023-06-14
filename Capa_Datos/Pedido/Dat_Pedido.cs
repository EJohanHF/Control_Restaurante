using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Capa_Datos.Pedido
{
    public class Dat_Pedido
    {
        public void ActualizarMesaAtendida(int id_mesa)
        {
            string query = "[sp_bloquear_mesas]";

            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_mesa", id_mesa);
                    cmd.ExecuteNonQuery();
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                //mensaje.Mensaje(" ", ex.Message, 3);
            }

        }
        public void ActualizarMesaLibre(int id_mesa)
        {
            string query = "[sp_liberar_mesas]";
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_mesa", id_mesa);
                    cmd.ExecuteNonQuery();
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
        }
        public DataTable GET_PEDIDO_X_ID(int id_pedido)
        {
            string query = "[SP_SELECT_PEDIDO_X_ID]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
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
        public DataTable GET_PEDIDO_X_ID_TOTAL(int id_pedido)
        {
            string query = "[SP_SELECT_PEDIDO_X_ID_TOTAL]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
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
        public DataTable SP_SELECT_PEDIDO_X_ID_CUENTA(int id_pedido)
        {
            string query = "[SP_SELECT_PEDIDO_X_ID_CUENTA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
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
        public DataTable SP_SELECT_DET_X_ID_CUENTA2(int id_pedido,int id_cuenta)
        {
            string query = "[SP_SELECT_PEDIDO_X_ID_CUENTA2]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
                    cmd.Parameters.AddWithValue("@id_cuenta", id_cuenta);
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
        public DataTable getInformeVentas(int caja, string comprobante, int estado, int canal_venta, string serie, string numero, DateTime desde, DateTime hasta, DateTime HoraInicio, DateTime HoraFin)
        {
            string query = "GET_DATA_INFORME";
            DataTable dt = new DataTable();
            try
            {
                string ini = desde.ToString("yyyy-MM-dd") + " " + HoraInicio.ToString("HH:mm:ss");
                DateTime inicio = Convert.ToDateTime(ini);

                string fin = hasta.ToString("yyyy-MM-dd") + " " + HoraFin.ToString("HH:mm:ss");
                DateTime final = Convert.ToDateTime(fin);

                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@caja", caja);
                    cmd.Parameters.AddWithValue("@comprobante", comprobante);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@canal_venta", canal_venta);
                    cmd.Parameters.AddWithValue("@serie", serie);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.Parameters.AddWithValue("@desde", ini);
                    cmd.Parameters.AddWithValue("@hasta", fin);
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
        public DataTable getVentaProductos(int caja, string comprobante, int canal_venta, int idplato, DateTime desde, DateTime hasta, DateTime HoraInicio, DateTime HoraFin)
        {
            string query = "USP_GET_VENTA_X_PRODUCTOS";
            DataTable dt = new DataTable();
            try
            {
                //string ini = desde.ToShortDateString() + " " + HoraInicio.ToShortTimeString();
                //DateTime inicio = Convert.ToDateTime(ini);

                //string fin = hasta.ToShortDateString() + " " + HoraFin.ToShortTimeString();
                //DateTime final = Convert.ToDateTime(ini);

                string ini = desde.ToString("yyyy-MM-dd") + " " + HoraInicio.ToString("HH:mm:ss");
                DateTime inicio = Convert.ToDateTime(ini);

                string fin = hasta.ToString("yyyy-MM-dd") + " " + HoraFin.ToString("HH:mm:ss");
                DateTime final = Convert.ToDateTime(fin);

                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@caja", caja);
                    cmd.Parameters.AddWithValue("@comprobante", comprobante);
                    cmd.Parameters.AddWithValue("@canal_venta", canal_venta);
                    cmd.Parameters.AddWithValue("@idplato", idplato);
                    cmd.Parameters.AddWithValue("@desde", ini);
                    cmd.Parameters.AddWithValue("@hasta", fin);
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
        public DataTable getVentaProductosConsolidado(int caja, string comprobante, int canal_venta, int idplato, DateTime desde, DateTime hasta, DateTime HoraInicio, DateTime HoraFin)
        {
            string query = "USP_GET_VENTA_X_PRODUCTOS_CONSOLIDADO";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@caja", caja);
                    cmd.Parameters.AddWithValue("@comprobante", comprobante);
                    cmd.Parameters.AddWithValue("@canal_venta", canal_venta);
                    cmd.Parameters.AddWithValue("@idplato", idplato);
                    cmd.Parameters.AddWithValue("@desde", desde.ToString("yyyy-MM-dd") + " " + HoraInicio.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@hasta", hasta.ToString("yyyy-MM-dd") + " " + HoraFin.ToString("HH:mm:ss"));
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
        public int SetPedido(int id_mesa, int id_usuario, int cod_empleado, int cod_cliente, DataTable dt,string opcion,string nro_personas,int isreserva, string nomcliente, string telfcliente, string nomequipo, string motorizado, ref string _mensaje)
        {
            string query = "[USP_GENERARPEDIDO]";
            int result = 0;
            try
            {
                
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ped_id_mesa", id_mesa);
                    cmd.Parameters.AddWithValue("@cod_usuario", id_usuario);
                    cmd.Parameters.AddWithValue("@cod_empleado", cod_empleado);
                    if(cod_cliente == 0)
                    {
                        cmd.Parameters.AddWithValue("@cod_cliente", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@cod_cliente", cod_cliente);
                    }
                    cmd.Parameters.AddWithValue("@TB_PEDIDODETALLE", dt);
                    cmd.Parameters.AddWithValue("@opcion", opcion);
                    cmd.Parameters.AddWithValue("@nro_personas", nro_personas);
                    cmd.Parameters.AddWithValue("@isreserva", isreserva);
                    cmd.Parameters.AddWithValue("@nomcliente", nomcliente);
                    cmd.Parameters.AddWithValue("@telcliente", telfcliente);
                    cmd.Parameters.AddWithValue("@nomequipo", nomequipo);
                    cmd.Parameters.AddWithValue("@motorizado", motorizado);
                    cmd.Parameters.AddWithValue("@mensaje", 0).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(cmd.Parameters["@mensaje"].Value);
                    if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch (Exception ex)
            {
                //_mensaje = "Error al generar el pedido, intente nuevamente";
                result = 0;
                if (ex.Message.Contains("SF_USUARIO"))
                {
                    //mensaje.Mensaje("SOS-FOOD - Error","No hay ningun usuario logueado en caja", 3);
                }
                else
                {
                    //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                }
                
            }
            return result;
        }
        public int SetPedidoSinInsumo(int id_mesa, int id_usuario, int cod_empleado, int cod_cliente, DataTable dt, string opcion, string nro_personas, int isreserva, ref string _mensaje,int iddet, string nomcliente, string telfcliente, string nomequipo, string motorizado)
        {
            string query = "[USP_GENERARPEDIDO_SININSUMO]";
            int result = 0;
            try
            {

                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ped_id_mesa", id_mesa);
                    cmd.Parameters.AddWithValue("@cod_usuario", id_usuario);
                    cmd.Parameters.AddWithValue("@cod_empleado", cod_empleado);
                    if (cod_cliente == 0)
                    {
                        cmd.Parameters.AddWithValue("@cod_cliente", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@cod_cliente", cod_cliente);
                    }
                    cmd.Parameters.AddWithValue("@TB_PEDIDODETALLE", dt);
                    cmd.Parameters.AddWithValue("@opcion", opcion);
                    cmd.Parameters.AddWithValue("@nro_personas", nro_personas);
                    cmd.Parameters.AddWithValue("@isreserva", isreserva);
                    cmd.Parameters.AddWithValue("@nomcliente", nomcliente);
                    cmd.Parameters.AddWithValue("@telcliente", telfcliente);
                    cmd.Parameters.AddWithValue("@nomequipo", nomequipo);
                    cmd.Parameters.AddWithValue("@motorizado", motorizado);
                    cmd.Parameters.AddWithValue("@iddet", iddet);
                    cmd.Parameters.AddWithValue("@mensaje", 0).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(cmd.Parameters["@mensaje"].Value);
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                //_mensaje = "Error al generar el pedido, intente nuevamente";
                result = 0;
                if (ex.Message.Contains("SF_USUARIO"))
                {
                    //mensaje.Mensaje("SOS-FOOD - Error", "No hay ningun usuario logueado en caja", 3);
                }
                else
                {
                    //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                }

            }
            return result;
        }
        public DataTable GetDeliveryxPedidoApp(int id_pedido)
        {
            string query = "[get_pedido_delivery]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
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
        public DataTable GetImpresoraxIdPedido(int id_pedido,int tipo)
        {
            string query = "[GET_IMPRESORA_X_PEDIDO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
                    cmd.Parameters.AddWithValue("@tipImp", tipo);
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
        public DataTable GetReImpresoraxIdPedido(int id_det_pedido, int tipo)
        {
            string query = "[GET_REIMP_IMPRESORA_X_PEDIDO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_det_pedido", id_det_pedido);
                    cmd.Parameters.AddWithValue("@tipImp", tipo);
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
        public DataTable GetImpresoraxIdPedidoAnulado(int id_pedido, int tipo)
        {
            string query = "[GET_IMPRESORA_X_PEDIDO_ANULADO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
                    //cmd.Parameters.AddWithValue("@tipImp", tipo);
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
        public DataTable GET_DETALLE_X_PEDIDO(int id_pedido,int id_impresora)
        {
            string query = "[GET_DETALLE_X_PEDIDO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
                    cmd.Parameters.AddWithValue("@ID_IMP", id_impresora);
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
        public DataTable GET_REIMP_DETALLE_X_PEDIDO(int id_det_pedido, int id_impresora)
        {
            string query = "[GET_REIMP_DETALLE_X_PEDIDO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_DET_PEDIDO", id_det_pedido);
                    cmd.Parameters.AddWithValue("@ID_IMP", id_impresora);
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
        public DataTable GetImpresoraxPlato(int cod_carta)
        {
            string query = "[GET_IMPRESORA_X_CARTA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_carta", cod_carta);
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
        public DataTable DescontarStockSubNivel(int cod_carta)
        {
            string query = "[USP_DESCONTAR_STOCK_NIVEL]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_carta", cod_carta);
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
        public DataTable GET_DETALLE_X_PEDIDO_ANULADA(int id_pedido, int id_impresora)
        {
            string query = "[GET_DETALLE_X_PEDIDO_ANULADO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
                    cmd.Parameters.AddWithValue("@ID_IMP", id_impresora);
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
        public DataTable GET_MESA_OCUPADA(int cod_mesa)
        {
            string query = "[SP_SELECT_MESA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cod_mesa", cod_mesa);
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
        public DataTable GET_PEDIDO_X_MESA(int cod_mesa)
        {
            string query = "[SP_SELECT_PEDIDO_X_MESA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cod_mesa", cod_mesa);
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
        public DataTable GET_INFO_PEDIDO_DELIVERY(int id_pedido)
        {
            string query = "USP_GET_INFO_PEDIDO_DELIVERY";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
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
        public DataTable getPedidoxCliente(int idcli, int idpedido)
        {
            string query = "USP_GET_PEDIDO_X_CLIENTE";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcli", idcli);
                    cmd.Parameters.AddWithValue("@idpedido", idpedido);
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
        public DataTable get_platos_vendidos_todos(int op,DateTime dia, DateTime desde, DateTime hasta)
        {
            string query = "[USP_GET_PLATOS_VENDIDOS_TOTAL]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@op", op);
                    cmd.Parameters.AddWithValue("@dia", dia.Date);
                    cmd.Parameters.AddWithValue("@desde", desde.Date);
                    cmd.Parameters.AddWithValue("@hasta", hasta.Date);
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
        public DataTable get_platos_vendidos(int Idradiobuton, int idgrup,int idcat,DateTime dia, DateTime desde, DateTime hasta)
        {
            string query = "[USP_GET_PLATOS_VENDIDOS]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (Idradiobuton == 1) { }
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idgrup", idgrup);
                    cmd.Parameters.AddWithValue("@idcat", idcat);
                    cmd.Parameters.AddWithValue("@dia", (Idradiobuton == 1) ? dia.Date.ToString("yyyy-MM-dd"): null);
                    cmd.Parameters.AddWithValue("@desde", (Idradiobuton == 2) ? desde.Date.ToString("yyyy-MM-dd") : null);
                    cmd.Parameters.AddWithValue("@hasta", (Idradiobuton == 2) ? hasta.Date.ToString("yyyy-MM-dd") : null);
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

        public DataTable getplatoscriollosmarinos(int op, DateTime dia, DateTime desde, DateTime hasta)
        {
            string query = "[USP_PLATO_CRIOLLO_MARINO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@op", op);
                    cmd.Parameters.AddWithValue("@dia", dia.Date);
                    cmd.Parameters.AddWithValue("@desde", desde.Date);
                    cmd.Parameters.AddWithValue("@hasta", hasta.Date);
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
        MEnsaje mensaje = new MEnsaje();
        public void ActualizarCuentaPedidoImp(int id_pedido)
        {
            string query = "[sp_set_cuenta_pedido]";

            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
                    cmd.ExecuteNonQuery();
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

        }
        public void ActualizarDetPedidoImp(int id_pedido)
        {
            string query = "[SP_ActualizarDetPedidoImp]";
           
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_PEDIDO", id_pedido);
                    cmd.ExecuteNonQuery();
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
         
        }
        public void ActualizarDetPedidoReImp(int id_det_pedido)
        {
            string query = "[SP_ActualizarDetPedidoReImp]";

            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_DET_PEDIDO", id_det_pedido);
                    cmd.ExecuteNonQuery();
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

        }
        public void ActualizarDetPedidoImpAnul(int id_det_pedido)
        {
            string query = "[sp_actualizar_anulado]";

            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_det_ped", id_det_pedido);
                    cmd.ExecuteNonQuery();
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
        }
        public bool EliminarPlatoPedido(int IdDetPed, int IdPed, string comentario,ref string _mensaje,int idusuarioanulacion)
        {
            string query = "[USP_ELIMINARPLATOXPEDIDO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_DET_PED", IdDetPed);
                    cmd.Parameters.AddWithValue("@ID_PED", IdPed);
                    cmd.Parameters.AddWithValue("@COMENTARIO", comentario);
                    cmd.Parameters.AddWithValue("@ID_USU", idusuarioanulacion);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = true;
                    }
                    
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                _mensaje = "Error al generar el pedido, intente nuevamente";
                result = false;
            }
            return result;
        }
        public bool EliminarPlatoPedidoxCantidad(int IdDetPed, int IdPed, string comentario,decimal cantidad ,ref string _mensaje,int idusuarioanulacion)
        {
            string query = "[USP_ELIMINARPLATOXPEDIDOXCANTIDAD]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_DET_PED", IdDetPed);
                    cmd.Parameters.AddWithValue("@ID_PED", IdPed);
                    cmd.Parameters.AddWithValue("@COMENTARIO", comentario);
                    cmd.Parameters.AddWithValue("@CANT_ANULADO", cantidad);
                    cmd.Parameters.AddWithValue("@ID_USU", idusuarioanulacion);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = true;
                    }

                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception ex)
            {
                _mensaje = "Error al generar el pedido, intente nuevamente";
                result = false;
            }
            return result;
        }
        public bool QuitarPlatoMesa(int IdPed, Decimal CantAnu, int IdUsu, int IdMesa)
        {
            string query = "[SP_QUITAR_PLATO_MESA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@iddet", IdPed);
                    cmd.Parameters.AddWithValue("@cant_anu", CantAnu);
                    cmd.Parameters.AddWithValue("@id_usu", IdUsu);
                    cmd.Parameters.AddWithValue("@id_mesa", IdMesa);
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
        public bool CambiarPlatoMesa(int IdPed,Decimal CantAnu,int IdUsu,int IdMesa)
        {
            string query = "[SP_CAMBIAR_PLATO_MESA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@iddet", IdPed);
                    cmd.Parameters.AddWithValue("@cant_anu", CantAnu);
                    cmd.Parameters.AddWithValue("@id_usu", IdUsu);
                    cmd.Parameters.AddWithValue("@id_mesa", IdMesa);
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
        public bool DescuentoPedido(int IdPed, decimal nuevo_monto,int id_tip_desc, ref string _mensaje)
        {
            string query = "[usp_descuento_x_pedido]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_ped", IdPed);
                    cmd.Parameters.AddWithValue("@nuevo_monto", nuevo_monto);
                    cmd.Parameters.AddWithValue("@id_tip_desc", id_tip_desc);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = true;
                    }

                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                _mensaje = "Error al generar el pedido, intente nuevamente";
                result = false;
            }
            return result;
        }
        public bool DescuentoPlato(int IdDetPed, decimal nuevo_monto, int id_tip_desc, ref string _mensaje)
        {
            string query = "[usp_descuento_x_plato]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_det_ped", IdDetPed);
                    cmd.Parameters.AddWithValue("@nuevo_monto", nuevo_monto);
                    cmd.Parameters.AddWithValue("@id_tip_desc", id_tip_desc);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = true;
                    }

                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                _mensaje = "Error al generar el pedido, intente nuevamente";
                result = false;
            }
            return result;
        }

        //
        public DataTable GET_PEDIDO()
        {
            string query = "[SP_PEDIDO]";
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
        public DataTable GET_DETALLEPEDIDO()
        {
            string query = "[SP_GET_DETALLEPEDIDO]";
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
        public DataTable GET_DETALLEPEDIDO2()
        {
            string query = "[SP_GET_DETALLEPEDIDO2]";
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
        public DataTable GetRecurrenciaClientes()
        {
            string query = "[USP_GET_RECURRENCIA_CLIENTES]";
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
              //  mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;

        }
        public DataTable GET_DETALLE_X_PLATO_ANULADA(int id_det_pedido, int id_impresora)
        {
            string query = "[GET_DETALLE_X_PLATO_ANULADO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_DET_PED", id_det_pedido);
                    cmd.Parameters.AddWithValue("@ID_IMP", id_impresora);
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

        public DataTable GetImpresoraxIdDetPedido(int id_det_pedido,int tipo)
        {
            string query = "[GET_IMPRESORA_X_PLATO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_det_ped", id_det_pedido);
                    cmd.Parameters.AddWithValue("@tipImp", tipo);
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

        public DataTable GET_MESA_EXISTE(string nom_mesa)
        {
            string query = "[SP_SELECT_MESA_EXISTE]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nom_mesa", nom_mesa);
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

        public bool CambioMesa(int op, int CodMesaAntes, int CodMesaNuevo, int idPedido, ref string _mensaje)
        {
            string query = "[sp_cambio_mesa]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@op", op);
                    cmd.Parameters.AddWithValue("@cod_mesa_antes", CodMesaAntes);
                    cmd.Parameters.AddWithValue("@cod_mesa_nuevo", CodMesaNuevo);
                    cmd.Parameters.AddWithValue("@cod_pedido", idPedido);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = true;
                    }

                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                _mensaje = "Error al cambiar de mesa, intente nuevamente";
                result = false;
            }
            return result;
        }


        public bool SetPagoCliente(int operacion, int id_pedido, int tipo_pago, decimal monto, decimal vuelto)
        {
            string query = "[USP_SET_PAGO_CLIENTE]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID_PEDIDO", id_pedido);
                    cmd.Parameters.AddWithValue("@ID_TIPO_PAGO", tipo_pago);
                    cmd.Parameters.AddWithValue("@MONTO", monto);
                    cmd.Parameters.AddWithValue("@VUELTO", vuelto);
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
               // mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return result;
        }


        public DataTable getBoletaxPedido(int id_pedido)
        {
            string query = "[SP_GET_BOLETA_X_PEDIDO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
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
        public DataTable sp_verificar_plato_cuenta(int id_det_pedido)
        {
            string query = "[sp_verificar_plato_cuenta]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_det_cuen", id_det_pedido);
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
        public DataTable sp_verificar_pedido_cuenta(int id_pedido)
        {
            string query = "[sp_verificar_pedido_cuenta]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_ped_cuen", id_pedido);
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
        public DataTable get_pedidos_pendientes(string NombreImpresora)
        {
            string query = "[USP_GET_PEDIDOS_PENDIENTES]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreImpresora", NombreImpresora);
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
        public DataTable getPedidosEntregados(string NombreImpresora)
        {
            string query = "[USP_GET_PEDIDOS_ENTREGADOS]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreImpresora", NombreImpresora);
                    //cmd.Parameters.AddWithValue("@op", op);
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
        public DataTable getPantallas()
        {
            string query = "USP_GET_NOMBRE_PANTALLAS";
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

        public bool SET_ENTREGAR_PEDIDO(int id_detalle_pedido)
        {
            string query = "[USP_SET_EST_ENTREGADO_PEDIDO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_detalle_pedido", id_detalle_pedido);
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
        public bool setNomCliente(int id_pedido, string NombreCliente)
        {
            string query = "[USP_SET_NOMBRE_CLIENTE]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
                    cmd.Parameters.AddWithValue("@nombre_cliente", NombreCliente);
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
        public bool QuitarDescuentoPlato(int id_det_pedido)
        {
            string query = "[USP_QUITAR_DESCUENTO_PLATO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_det_pedido", id_det_pedido);
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
        public bool ResetearPlatoPedido(int id_det_pedido)
        {
            string query = "[USP_RESETEAR_PLATO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_det_pedido", id_det_pedido);
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
        public DataTable getEmpleadoPedido(int id_mesa)
        {
            string query = "[USP_GET_EMPLEADO_PEDIDO]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_mesa", id_mesa);
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

        public DataTable GetPrioridades()
        {
            string query = "[USP_GET_PRIORIDADES]";
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

        public bool SET_PRIORIDAD(string prioridad)
        {
            string query = "[USP_SET_PRIORIDAD]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@prioridad", prioridad);
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
        public bool EliminarPrioridad(int id_prioridad)
        {
            string query = "[USP_ELIMINAR_PRIORIDAD]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_prioridad", id_prioridad);
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
        public bool setPedidoPendiente(int id)
        {
            string query = "USP_SET_PEDIDO_PENDIENTE";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
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

        public bool setMotorizado(int id_pedido, string Motorizado)
        {
            string query = "[USP_SET_MOTORIZADO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
                    cmd.Parameters.AddWithValue("@motorizado", Motorizado);
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
    }
}

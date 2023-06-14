using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Data.Reportes.VentasPorDia
{
public class Dat_CierreParcial
    {
        public DataTable GetCabeceraBoleta()
        {
            string query = "[sp_cabecera_boleta]";
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
        public bool sp_actualizar_faltante(string serieNumero)
        {
            string query = "[sp_update_doc_faltante]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@serieNumero", serieNumero);
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
        public bool sp_actualizar_envio(string id)
        {
            string query = "[sp_update_doc]";
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
        public DataTable GetDetalleJson(string iddoc)
        {
            string query = "[sp_generar_json_detalle]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@iddoc", iddoc);
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
        public DataTable GetAplatosXComentario(string NombreEquipo)
        {
            string query = "[USP_GET_RESUMEN_ANULADOS_PLATO_DIA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NOMEQUIPO", NombreEquipo);
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
        public DataTable GetAplatosXComentario2(int idCierre)
        {
            string query = "[USP_GET_RESUMEN_ANULADOS_PLATO_DIA2]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cierre", idCierre);
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
        public DataTable GetCierreJson()
        {
            string query = "[sp_generar_json_ventas]";
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
        public DataTable GetreporteVentasDia(string NombreEquipo)
        {
            string query = "[USP_GET_RESUMEN_VENTAS_DIA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NOMEQUIPO", NombreEquipo);
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
        
        public DataTable GetreporteVentasDia2(int idCierre)
        {
            string query = "[USP_GET_RESUMEN_VENTAS_DIA2]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cierre", idCierre);
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

        #region doc electronico 
        public DataTable GetreporteDocumentosElect(string NombreEquipo)
        {
            string query = "[USP_GET_RESUMEN_DOCUMENTOS_ELECTRONICOS]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NOMEQUIPO", NombreEquipo);
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
        public DataTable GetreporteDocumentosElect2(int idCierre)
        {
            string query = "[USP_GET_RESUMEN_DOCUMENTOS_ELECTRONICOS2]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cierre", idCierre);
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
        public DataTable GetTipoPago(string NombreEquipo)
        {
            string query = "[USP_GET_TIPO_PAGO_TICKET]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NOMEQUIPO", NombreEquipo);
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
        public DataTable GetTipoPago2(int idCierre)
        {
            string query = "[USP_GET_TIPO_PAGO_TICKET2]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cierre", idCierre);
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
        #endregion
        public DataTable GetVentaDia()
        {
            string query = "[SP_VENTAS_DEL_DIA]";
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
        public DataTable GetVentaDiaCierre(string idcierre)
        {
            string query = "[SP_VENTAS_DEL_DIA_CIERRE]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cierre", idcierre);
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
        public DataTable GettotalPedidosDia( string idscat, string NombreEquipo)
        {
            string query = "[USP_GET_RESUMEN_PLATOS_VENDIDOS_DIA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SUEPERCAR", idscat);
                    cmd.Parameters.AddWithValue("@NOMEQUIPO", NombreEquipo);
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
        public DataTable GettotalPedidosDia2(string idscat, int idCierre)
        {
            string query = "[USP_GET_RESUMEN_PLATOS_VENDIDOS_DIA2]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SUEPERCAR", idscat);
                    cmd.Parameters.AddWithValue("@id_cierre", idCierre);
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
        public DataTable GetDetallePagos()
        {
            string query = "[sp_detalle_pagos]";
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
        public DataTable GetDetallePagosCierre(string idcierre)
        {
            string query = "[sp_detalle_pagos_cierre]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cierre", idcierre);
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
        public DataTable GetReporteCajaDia()
        {
            string query = "[USP_GET_RESUMEN_REPORTE_CAJA]";
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
        public DataTable GetReporteCajaDia2(int idCierre)
        {
            string query = "[USP_GET_RESUMEN_REPORTE_CAJA2]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cierre", idCierre);
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

        public DataTable GetReporteFecha(string desde, string hasta)
        {
            string query = "[SP_GET_RESUMEN_EXPORTAR_REPORTE_FECHA]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FECHA_DESDE", desde);
                    cmd.Parameters.AddWithValue("@FECHA_HASTA", hasta);
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

        public DataTable GetTopPlatosCierre(string idcierre)
        {
            string query = "USP_GET_PLATOS_VENDIDOS_TOP";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_CIERRE", idcierre);
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
        public DataTable GetTopPlatos()
        {
            string query = "[USP_GET_PLATOS_VENDIDOS_TOP_DIA]";
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
        public DataTable GetMesa()
        {
            string query = "[USP_GET_MESAS_TOTALES]";
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
        public DataTable GetStock()
        {
            string query = "[sp_stock_actual]";
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
        public DataTable GetCajaChicaCierre(string idcierre,string NomEquipo)
        {
            string query = "[USP_GET_CAJA_CHICA_DIA_CIERRE]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_CIERRE", idcierre);
                    cmd.Parameters.AddWithValue("@NOMEQUIPO", NomEquipo);
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
        public DataTable GetPlatosWebService()
        {
            string query = "[USP_GET_PLATOS_WEB_SERVICE]";
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
        public bool sp_actualizar_plato_web_service(string id_carta, string nom_plato, string precio_plato)
        {
            string query = "[sp_actualizar_plato_web_service]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_carta", id_carta);
                    cmd.Parameters.AddWithValue("@nom_carta", nom_plato);
                    cmd.Parameters.AddWithValue("@precio_carta", precio_plato);
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

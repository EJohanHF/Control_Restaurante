using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Facturacion_Electronica
{
    public class Dat_Doc_Electronico
    {
        public DataTable GetDocElectronico(int idDocElectr)
        {
            string query = "SP_GET_DOC_ELECTRONICO";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idDocElectr", idDocElectr);
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
        public DataTable GetMontoLetras(decimal numero)
        {
            string query = "sp_numeros_a_letras";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Numero", numero);
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
        public DataTable sp_generar_json_fe(int idDocElec)
        {
            string query = "sp_generar_json_fe";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idDocElec", idDocElec);
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

        public int SetDocElectronico(decimal sumatoriaImpuestoBolsas, int idCliente, string direccionReceptor, decimal sumatoriaOtrosCargos, decimal porcentajeOtrosCargos, string telefono, string codigoTipoOperacion,string idpedido, string codigoPaisReceptor,
            decimal importeTotal, string correoReceptor, string numeroDocIdentidadEmisor, string tipoDocIdentidadEmisor, string tipoDocumento, string serieNumero, DateTime fechaEmision, string tipoMoneda, string numeroDocIdentidadReceptor,
            string razonSocialReceptor, string tipoDocIdentidadReceptor, decimal totalOPGravadas, decimal totalOPExoneradas, decimal totalOPNoGravadas, decimal totalOPGratuitas, decimal sumatoriaIGV, decimal sumatoriaISC, decimal importeTotalVenta,
            decimal totalDescuentos, decimal descuentosGlobales, string montoEnLetras,string nomequipo, DataTable dt)
        {
            string query = "[USP_GENERARDOC_ELECTRONICO]";
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {

                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sumatoriaImpuestoBolsas", sumatoriaImpuestoBolsas);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.Parameters.AddWithValue("@direccionReceptor", direccionReceptor);
                    cmd.Parameters.AddWithValue("@sumatoriaOtrosCargos", sumatoriaOtrosCargos);
                    cmd.Parameters.AddWithValue("@porcentajeOtrosCargos", porcentajeOtrosCargos);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@codigoTipoOperacion", codigoTipoOperacion);
                    cmd.Parameters.AddWithValue("@idpedido", idpedido);
                    cmd.Parameters.AddWithValue("@codigoPaisReceptor", codigoPaisReceptor);
                    cmd.Parameters.AddWithValue("@importeTotal", importeTotal);
                    cmd.Parameters.AddWithValue("@correoReceptor", correoReceptor);
                    cmd.Parameters.AddWithValue("@numeroDocIdentidadEmisor", numeroDocIdentidadEmisor);
                    cmd.Parameters.AddWithValue("@tipoDocIdentidadEmisor", tipoDocIdentidadEmisor);
                    cmd.Parameters.AddWithValue("@tipoDocumento", tipoDocumento);
                    cmd.Parameters.AddWithValue("@serieNumero", serieNumero);
                    cmd.Parameters.AddWithValue("@fechaEmision", fechaEmision.ToString("yyyyMMdd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@tipoMoneda", tipoMoneda);
                    cmd.Parameters.AddWithValue("@numeroDocIdentidadReceptor", numeroDocIdentidadReceptor);
                    cmd.Parameters.AddWithValue("@razonSocialReceptor", razonSocialReceptor);
                    cmd.Parameters.AddWithValue("@tipoDocIdentidadReceptor", tipoDocIdentidadReceptor);
                    cmd.Parameters.AddWithValue("@totalOPGravadas", totalOPGravadas);
                    cmd.Parameters.AddWithValue("@totalOPExoneradas", totalOPExoneradas);
                    cmd.Parameters.AddWithValue("@totalOPNoGravadas", totalOPNoGravadas);
                    cmd.Parameters.AddWithValue("@totalOPGratuitas", totalOPGratuitas);
                    cmd.Parameters.AddWithValue("@sumatoriaIGV", sumatoriaIGV);
                    cmd.Parameters.AddWithValue("@sumatoriaISC", sumatoriaISC);
                    cmd.Parameters.AddWithValue("@importeTotalVenta", importeTotalVenta);
                    cmd.Parameters.AddWithValue("@totalDescuentos", totalDescuentos);
                    cmd.Parameters.AddWithValue("@descuentosGlobales", descuentosGlobales);
                    cmd.Parameters.AddWithValue("@montoEnLetras", montoEnLetras);
                    cmd.Parameters.AddWithValue("@nomequipo", nomequipo);
                    cmd.Parameters.AddWithValue("@TB_SF_DOC_ELECTRONICO_DET", dt);
                    cmd.Parameters.AddWithValue("@mensaje", 0).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(cmd.Parameters["@mensaje"].Value);
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
        public bool EliminarDocElectronico(int idDocElectr)
        {
            string query = "[sp_borrar_doc_electronico]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idDocElectr", idDocElectr);
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
       
            public bool VerificaCliente(string nrodoc, string nombre, int tipo_doc, string direccion, string telefono, string correo)
        {
            string query = "[sp_cliente_boleta]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nro_doc", nrodoc);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@id_tipo_doc", tipo_doc);
                    cmd.Parameters.AddWithValue("@direccion", direccion);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@correo", correo);
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
        public bool ActualizarCorrelaticoDocElectronico(string serie)
        {
            string query = "[aumentar_correlativo]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@serie", serie);
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
        public DataTable getCabecera(int idDocElec)
        {
            string query = "USP_GET_DATA_CABECERA";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_doc_electronico", idDocElec);
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
        public DataTable getDetalle(int idDocElec)
        {
            string query = "USP_GET_DATA_DETALLE";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_doc_electronico", idDocElec);
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
        public DataTable getTributo(int idDocElec)
        {
            string query = "USP_GET_DATA_TRIBUTO";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_doc_electronico", idDocElec);
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
        public DataTable getResumen(string fecha)
        {
            string query = "sp_generar_resumen";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fecha", fecha);
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

        public DataTable getResumenTrd(string fecha)
        {
            string query = "sp_generar_resumen_trd";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fecha", fecha);
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

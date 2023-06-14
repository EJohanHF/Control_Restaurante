using Capa_Entidades;
using Capa_Entidades.Models.Centro_Costos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Centro_Costos
{
    public class DatCentroCostos
    {
        public DataTable GetCentroCostos()
        {
            string query = "[USP_GET_CENTRO_COSTOS]";
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
        public DataTable GetTiposCostos()
        {
            string query = "[USP_GET_TIPOS_COSTOS]";
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
        public DataTable GetDetPrecioProd(int id_carta, int op, CentroCostos cc)
        {
            string query = "[USP_GET_DET_PRECIO_PROD]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OP", op);
                    cmd.Parameters.AddWithValue("@ID_CARTA", id_carta);
                    if (op == 1)
                    {
                        cmd.Parameters.AddWithValue("@DIA", cc.Dia);
                        cmd.Parameters.AddWithValue("@MES", null);
                        cmd.Parameters.AddWithValue("@ANO", null);
                        cmd.Parameters.AddWithValue("@DESDE", null);
                        cmd.Parameters.AddWithValue("@HASTA", null);
                    }
                    else if (op == 2)
                    {
                        cmd.Parameters.AddWithValue("@DIA", null);
                        cmd.Parameters.AddWithValue("@MES", cc.Mes);
                        cmd.Parameters.AddWithValue("@ANO", cc.Año);
                        cmd.Parameters.AddWithValue("@DESDE", null);
                        cmd.Parameters.AddWithValue("@HASTA", null);
                    }
                    else if (op == 3)
                    {
                        cmd.Parameters.AddWithValue("@DIA", null);
                        cmd.Parameters.AddWithValue("@MES", null);
                        cmd.Parameters.AddWithValue("@ANO", null);
                        cmd.Parameters.AddWithValue("@DESDE", cc.Desde);
                        cmd.Parameters.AddWithValue("@HASTA", cc.Hasta);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                }
            }
            catch (Exception ex )
            {
                dt = null;
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;

        }
        MEnsaje mensaje = new MEnsaje();
        public bool SetCentroCosto(int operacion, CentroCostos ccostos, ref string _mensaje)
        {
            string query = "[USP_SET_CENTRO_COSTOS]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID", ccostos.ID);
                    cmd.Parameters.AddWithValue("@CC_FEC", ccostos.CC_FEC);
                    cmd.Parameters.AddWithValue("@CC_MES", ccostos.CC_MES);
                    cmd.Parameters.AddWithValue("@CC_TIPO", ccostos.CC_TIPO);
                    cmd.Parameters.AddWithValue("@CC_MONTO", ccostos.CC_MONTO);
                    cmd.Parameters.AddWithValue("@CC_AÑO", ccostos.CC_AÑO);
                    cmd.Parameters.AddWithValue("@CC_OBS", ccostos.CC_OBS);
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
        public bool setTiposCostos(int operacion, TiposCostos tcostos, ref string _mensaje)
        {
            string query = "[USP_SET_TIPOS_COSTOS]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@ID", tcostos.ID);
                    cmd.Parameters.AddWithValue("@TP_CODIGO", tcostos.TP_CODIGO);
                    cmd.Parameters.AddWithValue("@TP_DENOMINACION", tcostos.TP_DENOMINACION);
                    cmd.Parameters.AddWithValue("@TP_TIPO", tcostos.TP_TIPO);
                    cmd.Parameters.AddWithValue("@TP_CLASE", tcostos.TP_CLASE);
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

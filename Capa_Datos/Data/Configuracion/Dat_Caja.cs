using Capa_Entidades;
using Capa_Entidades.Models.Configuracion;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos.Configuracion
{
    public class dat_Caja
    {
        public DataTable GetResumenCaja()
        {
            string query = "[USP_GET_MONTOS_CAJA_CHICA]";
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

        public DataTable GetCaja(string NombreEquipo)
        {
            string query = "[USP_GET_CAJA_CHICA_DIA]";
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
        public bool SetCaja(int operacion, Caja cajasep,string NombreEquipo, ref string _mensaje)
        {
            string query = "[USP_SET_CAJA_CHICA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                    cmd.Parameters.AddWithValue("@id", cajasep.id);
                    cmd.Parameters.AddWithValue("@CC_ID_MOV", cajasep.CC_ID_MOV);
                    cmd.Parameters.AddWithValue("@CC_ID_TIPO", cajasep.CC_ID_TIPO);
                    cmd.Parameters.AddWithValue("@CC_MONTO", cajasep.CC_MONTO);
                    cmd.Parameters.AddWithValue("@CC_ID_EMPL", cajasep.CC_ID_EMPL);
                    cmd.Parameters.AddWithValue("@CC_DESCR", cajasep.CC_DESCR);
                    cmd.Parameters.AddWithValue("@NOM_EQUIPO", NombreEquipo);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch (Exception ex)
            {
                _mensaje = "¡ERROR OK!";
                result = false;
            }
            return result;
        }

        public bool SetCaja1(int operacion, Caja cajasep, ref string mensaje)
        {
            throw new NotImplementedException();
        }
    }
}

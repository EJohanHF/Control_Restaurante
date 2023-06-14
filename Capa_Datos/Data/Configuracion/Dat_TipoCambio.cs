using Capa_Entidades;
using Capa_Entidades.Models.Configuracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Data.Configuracion
{
    public class Dat_TipoCambio
    {
        public DataTable GetTiposCambio()
        {
            string query = "[USP_COMBO_TIPO_MONEDA]";
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

        public bool setTipoCambio(TipoCambio tc)
        {
            string query = "[USP_SET_TIPO_CAMBIO]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", tc.ID);
                    cmd.Parameters.AddWithValue("@TC_CAMBIO", tc.TC_CAMBIO);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        result = true;
                    }
                    if (cn.State == ConnectionState.Open) cn.Close();

                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;

        }
    }
}

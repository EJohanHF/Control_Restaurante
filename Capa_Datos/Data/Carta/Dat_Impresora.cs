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
   public class Dat_Impresora
    {
        public DataTable GetImpresora()
        {
            string query = "[USP_GET_IMPRESORA]";
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
        public bool SetImpresora(int operacion, Impresora impresora, ref string _mensaje)
        {
            string query = "[USP_SET_IMPRESORA]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@operacion", operacion);//@idcat
                    cmd.Parameters.AddWithValue("@idimp", impresora.idimp);
                    cmd.Parameters.AddWithValue("@ipimp", impresora.ipimp);
                    cmd.Parameters.AddWithValue("@nomimp", impresora.nomimp);
                    cmd.Parameters.AddWithValue("@estadoimp", impresora.estadoimp);
                    cmd.Parameters.AddWithValue("@equipoimp", impresora.nomequipoimp);
                    cmd.Parameters.AddWithValue("@nomimppedido", impresora.nomimpppedido);
                    var f = cmd.ExecuteNonQuery();
                    result = f > 0;
                    if (cn.State == ConnectionState.Open) cn.Close();

                }

            }
            catch (Exception)
            {
                _mensaje = "" + impresora.idimp + " está en uso, desvincule para eliminar";
                result = false;
            }
            return result;
        }
    }
}

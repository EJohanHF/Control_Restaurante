using Capa_Entidades;
using Capa_Entidades.Models.Web_Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.WebService
{
    public class Dat_CartaDeliveryWebService
    {
        public DataTable GetCartaDeliveryWebService(int idcarta)
        {
            string query = "[sp_get_carta_delivery]";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcarta", idcarta);//@idcat
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
        public bool SetCartaDeliveryWebService(CartaDeliveryWebService cartadelivery, int idcarta,bool estado)
        {
            string query = "[sf_add_carta_delivery]";
            bool result = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Variables.sql_conexion))
                {
                    if (cn.State == 0) cn.Open();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcarta", idcarta);//@idcat
                    cmd.Parameters.AddWithValue("@precio", cartadelivery.price);
                    cmd.Parameters.AddWithValue("@descuento", cartadelivery.discount);
                    cmd.Parameters.AddWithValue("@imagen", cartadelivery.imagen);
                    if (cartadelivery.estado_del == false)
                    {
                        cmd.Parameters.AddWithValue("@estado", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@estado", 1);
                    }
                    cmd.Parameters.AddWithValue("@nom_car", cartadelivery.Name);//@idcat
                    cmd.Parameters.AddWithValue("@precio_sal", cartadelivery.price_salon);
                    cmd.Parameters.AddWithValue("@descuento_sal", cartadelivery.discount_salon);
                    if (cartadelivery.estado_sal == false)
                    {
                        cmd.Parameters.AddWithValue("@estado_sal", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@estado_sal", 1);
                    }
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

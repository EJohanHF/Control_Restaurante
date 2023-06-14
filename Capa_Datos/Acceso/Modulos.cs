using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Capa_Entidades.Acceso;
using System.Data.SqlClient;
using System.Data;

namespace Capa_Datos.Acceso
{
    public class Modulos
    {
        public string conexion_sosfood = ConfigurationManager.ConnectionStrings["conexion_sosfood"].ConnectionString;

        public DataTable listarMenu()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion_sosfood))
                {
                    SqlCommand cmd = new SqlCommand("ListarMenus", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    return dt;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable listarSubMenu(int idmenu)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion_sosfood))
                {
                    SqlCommand cmd = new SqlCommand("ListarSubMenu", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idMenu", idmenu);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    return dt;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}

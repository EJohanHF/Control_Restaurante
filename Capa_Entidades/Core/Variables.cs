using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class Variables
    {
        public static string sql_conexion { get; set; } = ConfigurationManager.ConnectionStrings["conexion_sosfood"].ConnectionString;
    }
}

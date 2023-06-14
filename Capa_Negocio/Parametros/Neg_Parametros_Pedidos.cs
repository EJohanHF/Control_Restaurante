using Capa_Datos.Parametros;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Parametros
{
    public class Neg_Parametros_Pedidos
    {
        Dat_Parametros_Pedidos datParametros = new Dat_Parametros_Pedidos();
        public string LimiteMinutos()
        {
            string rutafe = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rutafe = dt.Rows[0][2].ToString();
            return rutafe;
        }
        public string Background1()
        {
            string rutafe = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rutafe = dt.Rows[1][2].ToString();
            return rutafe;
        }
        public string Background2()
        {
            string rutafe = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rutafe = dt.Rows[2][2].ToString();
            return rutafe;
        }
        public string Background3Anulado()
        {
            string rutafe = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rutafe = dt.Rows[3][2].ToString();
            return rutafe;
        }
        public string Forecolor1Anulado()
        {
            string rutafe = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rutafe = dt.Rows[4][2].ToString();
            return rutafe;
        }
        public string LimiteMinutosPlatoAnulado()
        {
            string rutafe = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rutafe = dt.Rows[5][2].ToString();
            return rutafe;
        }
        public string conSonidoTarde()
        {
            string rutafe = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rutafe = dt.Rows[6][2].ToString();
            return rutafe;
        }
        public string conSonidoLlegada()
        {
            string rutafe = "";
            DataTable dt = new DataTable();
            dt = datParametros.SelectParametros();
            rutafe = dt.Rows[7][2].ToString();
            return rutafe;
        }
    }
}

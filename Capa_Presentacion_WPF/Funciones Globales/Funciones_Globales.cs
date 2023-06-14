using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.Funciones_Globales
{
    public class Funciones_Globales
    {
        Funcion_Global global = new Funcion_Global();
        public string ImpresoraCaja()
        {
            string impresora = global.ImpCaja();
            return impresora;
        }
      
    }
   
}

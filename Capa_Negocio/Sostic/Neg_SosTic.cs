using Capa_Datos.Sostic;
using Capa_Entidades.Models;
using Capa_Entidades.Models.Sostic;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Sostic
{
    public class Neg_SosTic
    {
        Dat_Sostic datSostic = new Dat_Sostic();
        Funcion_Global globales = new Funcion_Global();
        public List<Sos_Tic> GetSostic()
        {

            List<Sos_Tic> sostic = new List<Sos_Tic>();
            try
            {
                DataTable dt = datSostic.GetEmpresa();
                foreach (DataRow row in dt.Rows)
                {
                    Sos_Tic _sos = new Sos_Tic();
                    _sos.ID = Convert.ToInt32(row["ID"]);
                    _sos.RAZON_SOCIAL = row["RAZON_SOCIAL"].ToString();
                    _sos.RUC = row["RUC"].ToString();
                    _sos.TELEFONO1 = row["TELEFONO1"].ToString();
                    _sos.TELEFONO2 = row["TELEFONO2"].ToString();
                    _sos.CORREO1 = row["CORREO1"].ToString();
                    _sos.CORREO2 = row["CORREO2"].ToString();
                    _sos.LOGO_SOSTIC = (byte[])row["LOGO_SOSTIC"];
                    _sos.LOGO_SOSFOOD = (byte[])row["LOGO_SOSFOOD"];
                    sostic.Add(_sos);
                }
            }
            catch(Exception ex)
            {
                sostic = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return sostic;
        }
        public bool SetSostic(ConsultaWebService webservice)
        {
            bool result = false;
            result = datSostic.SetSostic(webservice.fijo, webservice.movil, webservice.correo, webservice.imagen_empresa, webservice.imagen_producto);
            if (result != false)
            {
               
            }
            return result;
        }
    }
}

using Capa_Datos.Ambientes;
using Capa_Entidades.Models.Ambientes;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Ambientes
{
    public class Neg_Ambientes
    {
        Funcion_Global globales = new Funcion_Global();
        Dat_Ambiente datAmbientes = new Dat_Ambiente();
        public List<AmbientesItem> GetAmbientes()
        {
            List<AmbientesItem> ambientes = new List<AmbientesItem>();
            try
            {
                DataTable dt = new DataTable();
                dt = datAmbientes.GetMenu();
                foreach (DataRow row in dt.Rows)
                {
                    AmbientesItem _amb = new AmbientesItem();
                    _amb.ID = Convert.ToInt32(row["ID"]);
                    _amb.A_NOM = row["A_NOM"].ToString();
                    _amb.A_HEIGHT = Convert.ToInt32(row["A_HEIGHT"]);
                    _amb.A_WIDTH = Convert.ToInt32(row["A_WIDTH"]);
                    _amb.A_ACT = Convert.ToBoolean(row["A_ACT"].ToString());
                    _amb.A_TEXTO = row["A_TEXTO"].ToString();
                    _amb.A_F_CREATE = Convert.ToDateTime(row["A_F_CREATE"].ToString());
                    _amb.nrocolumnas = Convert.ToInt32(row["A_NRO_COLUMNA"].ToString());
                    _amb.A_X = Convert.ToInt32(row["A_X"].ToString());
                    _amb.A_Y = Convert.ToInt32(row["A_Y"].ToString());
                    _amb.A_TOP = Convert.ToInt32(row["A_TOP"].ToString());
                    _amb.A_BOTTOM = Convert.ToInt32(row["A_BOTTOM"].ToString());
                   
                    ambientes.Add(_amb);
                }
            }
            catch (Exception ex)
            {
                ambientes = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return ambientes;
        }
        public List<AmbientesItem> GetAmbientesActivo(string caja)
        {
            List<AmbientesItem> ambientes = new List<AmbientesItem>();
            try
            {
                DataTable dt = new DataTable();
                dt = datAmbientes.GetMenuActivo(caja);
                foreach (DataRow row in dt.Rows)
                {
                    AmbientesItem _amb = new AmbientesItem();
                    _amb.ID = Convert.ToInt32(row["ID"]);
                    _amb.A_NOM = row["A_NOM"].ToString();
                    _amb.A_HEIGHT = Convert.ToInt32(row["A_HEIGHT"]);
                    _amb.A_WIDTH = Convert.ToInt32(row["A_WIDTH"]);
                    _amb.A_ACT = Convert.ToBoolean(row["A_ACT"].ToString());
                    _amb.A_TEXTO = row["A_TEXTO"].ToString();
                    _amb.A_F_CREATE = Convert.ToDateTime(row["A_F_CREATE"].ToString());
                    _amb.nrocolumnas = Convert.ToInt32(row["A_NRO_COLUMNA"].ToString());
                    _amb.A_X = Convert.ToInt32(row["A_X"].ToString());
                    _amb.A_Y = Convert.ToInt32(row["A_Y"].ToString());
                    _amb.A_TOP = Convert.ToInt32(row["A_TOP"].ToString());
                    _amb.A_BOTTOM = Convert.ToInt32(row["A_BOTTOM"].ToString());

                    ambientes.Add(_amb);
                }
            }
            catch (Exception ex)
            {
                ambientes = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return ambientes;
        }
        public List<AmbientesItem> GetAmbientesActivoDelivery(string caja)
        {
            List<AmbientesItem> ambientes = new List<AmbientesItem>();
            try
            {
                DataTable dt = new DataTable();
                dt = datAmbientes.GetMenuActivoDelivery(caja);
                foreach (DataRow row in dt.Rows)
                {
                    AmbientesItem _amb = new AmbientesItem();
                    _amb.ID = Convert.ToInt32(row["ID"]);
                    _amb.A_NOM = row["A_NOM"].ToString();
                    _amb.A_HEIGHT = Convert.ToInt32(row["A_HEIGHT"]);
                    _amb.A_WIDTH = Convert.ToInt32(row["A_WIDTH"]);
                    _amb.A_ACT = Convert.ToBoolean(row["A_ACT"].ToString());
                    _amb.A_TEXTO = row["A_TEXTO"].ToString();
                    _amb.A_F_CREATE = Convert.ToDateTime(row["A_F_CREATE"].ToString());
                    _amb.nrocolumnas = Convert.ToInt32(row["A_NRO_COLUMNA"].ToString());
                    _amb.A_X = Convert.ToInt32(row["A_X"].ToString());
                    _amb.A_Y = Convert.ToInt32(row["A_Y"].ToString());
                    _amb.A_TOP = Convert.ToInt32(row["A_TOP"].ToString());
                    _amb.A_BOTTOM = Convert.ToInt32(row["A_BOTTOM"].ToString());

                    ambientes.Add(_amb);
                }
            }
            catch (Exception ex)
            {
                ambientes = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return ambientes;
        }
        public bool SetAmbiente(int operacion, AmbientesItem amb, ref string _mensaje)
        {
            bool result = false;
            result = datAmbientes.SetAmbiente(operacion, amb, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public bool SetTamañoAmbiente(AmbientesItem amb, ref string _mensaje)
        {
            bool result = false;
            result = datAmbientes.SetTamañoAmbiente(amb, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
    }
}

using Capa_Datos.Carta;
using Capa_Entidades.Models.Carta;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Carta
{
    public class Neg_DetDescripciones
    {
        Dat_DetDescripciones dataDetDesc = new Dat_DetDescripciones();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<DetDescripciones> GetDetDes()
        {
            ObservableCollection<DetDescripciones> detdescripciones = new ObservableCollection<DetDescripciones>();
            try
            {
                DataTable dt = dataDetDesc.GetDetDescripciones();
                foreach (DataRow row in dt.Rows)
                {
                    DetDescripciones _scat = new DetDescripciones();
                    _scat.ID = Convert.ToInt32(row["id"]);
                    _scat.TITLE_DESCRIPTION = row["TITLE_DESCRIPTION"].ToString();
                    _scat.DET_DESCRIPTION = row["DET_DESCRIPTION"].ToString();
                    _scat.DP_ID_DESC = Convert.ToInt32(row["DP_ID_DESC"]);
                    detdescripciones.Add(_scat);
                }
            }
            catch (Exception ex)
            {
                detdescripciones = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return detdescripciones;
        }
        public bool SetsDetDescripciones(int operacion, DetDescripciones detdescripciones, ref string _mensaje)
        {
            bool result = false;
            result = dataDetDesc.SetDetDescripciones(operacion, detdescripciones, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }

        public bool SetsDetDescripcione(int v, DetDescripciones detdescripciones, ref string mensaje)
        {
            throw new NotImplementedException();
        }
    }
}

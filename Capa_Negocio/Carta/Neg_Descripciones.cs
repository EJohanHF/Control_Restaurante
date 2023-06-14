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
    public class Neg_Descripciones
    {
        Dat_Descripciones dataDesc = new Dat_Descripciones();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Descripciones> GetDes()
        {
            ObservableCollection<Descripciones> descripciones = new ObservableCollection<Descripciones>();
            try
            {
                DataTable dt = dataDesc.GetDescripciones();
                foreach (DataRow row in dt.Rows)
                {
                    Descripciones _scat = new Descripciones();
                    _scat.ID = Convert.ToInt32(row["id"]);
                    _scat.TITLE_DESCRIPTION = row["TITLE_DESCRIPTION"].ToString();
                    descripciones.Add(_scat);
                }
            }
            catch (Exception ex)
            {
                descripciones = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return descripciones;
        }
        public bool SetsDescripciones(int operacion, Descripciones descripciones, ref string _mensaje)
        {
            bool result = false;
            result = dataDesc.SetDescripciones(operacion, descripciones, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }

        public bool SetsDescripcione(int v, Descripciones descripciones, ref string mensaje)
        {
            throw new NotImplementedException();
        }
    }
}

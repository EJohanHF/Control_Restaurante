using Capa_Datos.Nivel;
using Capa_Entidades.Models.Nivel;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Nivel
{
    public class Neg_Nivel
    {
        Dat_Nivel datNiv = new Dat_Nivel();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Ent_Nivel> GetNivel()
        {
            ObservableCollection<Ent_Nivel> nivel = new ObservableCollection<Ent_Nivel>();
            try
            {

                DataTable dt = datNiv.GetNivel();
                foreach (DataRow row in dt.Rows)
                {
                    Ent_Nivel _niv = new Ent_Nivel();
                    _niv.ID = Convert.ToInt32(row["ID"]);
                    _niv.N_NOM = row["N_NOM"].ToString();
                    _niv.N_ACT = Convert.ToBoolean(row["N_ACT"]);
                    _niv.N_F_CREATE = Convert.ToDateTime(row["N_F_CREATE"]);
                    _niv.N_F_MODIFICACION = Convert.ToDateTime(row["N_F_MODIFICACION"]);
                    _niv.N_TIPO_SELEC = Convert.ToInt32(row["N_TIPO_SELEC"]);
                    nivel.Add(_niv);
                }
            }
            catch (Exception ex)
            {
                nivel = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return nivel;
        }
        public ObservableCollection<Ent_Nivel> GetSubNivel()
        {
            ObservableCollection<Ent_Nivel> nivel = new ObservableCollection<Ent_Nivel>();
            try
            {

                DataTable dt = datNiv.GetSubNivel();
                foreach (DataRow row in dt.Rows)
                {
                    Ent_Nivel _niv = new Ent_Nivel();
                    _niv.ID_SN = Convert.ToInt32(row["ID"]);
                    _niv.SN_ID_NIVEL = Convert.ToInt32(row["SN_ID_NIVEL"]);
                    _niv.SN_NOM = row["SN_NOM"].ToString();
                    _niv.SN_ACT = Convert.ToBoolean(row["SN_ACT"]);
                    _niv.SN_IMAGEN = (byte[])row["SN_IMAGEN"];
                    _niv.SN_F_CREATE = Convert.ToDateTime(row["SN_F_CREATE"]);
                    _niv.SN_F_MODIFICACION = Convert.ToDateTime(row["SN_F_MODIFICACION"]);
                    _niv.SN_ID_CARTA = Convert.ToInt32(row["SN_ID_CARTA"]);
                    _niv.SN_ID_GRUPO = Convert.ToInt32(row["SN_ID_GRUP"]);
                    _niv.CAR_NOM = row["CAR_NOM"].ToString();
                    _niv.GR_NOM = row["GR_NOM"].ToString();
                    
                    nivel.Add(_niv);
                }
            }
            catch (Exception ex)
            {                 
                nivel = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return nivel;
        }
        public ObservableCollection<Ent_Nivel> GetSeleccionNivel()
        {
            ObservableCollection<Ent_Nivel> nivel = new ObservableCollection<Ent_Nivel>();
            try
            {

                DataTable dt = datNiv.GetSeleccionNivel();
                foreach (DataRow row in dt.Rows)
                {
                    Ent_Nivel _niv = new Ent_Nivel();
                    _niv.ID_SS = Convert.ToInt32(row["ID"]);
                    _niv.SS_DESCR =row["SS_DESCR"].ToString();
                    _niv.SS_ACT = Convert.ToBoolean(row["SS_ACT"]);
                    _niv.SS_F_CREATE = Convert.ToDateTime(row["SS_F_CREATE"]);
                    _niv.SS_F_MODIFICACION = Convert.ToDateTime(row["SS_F_MODIFICACION"]);
                    nivel.Add(_niv);
                }
            }
            catch (Exception ex)
            {
                nivel = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return nivel;
        }
        public bool SetFacturaCompra(int operacion, Ent_Nivel nivel,DataTable dt, ref string _mensaje)
        {
            bool result = false;
            result = datNiv.SetNivel(operacion, nivel,dt, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
    }
}

using Capa_Datos.Stock;
using Capa_Entidades.Models;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Stock;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Stock
{
  public  class Neg_Almacen
    {
        Dat_Almacen datAlmacen = new Dat_Almacen();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Almacen> GetAlmacen()
        {
            ObservableCollection<Almacen> almacen = new ObservableCollection<Almacen>();
            try
            {
                DataTable dt = datAlmacen.GetAlmacen();
                foreach (DataRow row in dt.Rows)
                {
                    Almacen _almacen = new Almacen();
                    _almacen.idalm = Convert.ToInt32(row["ID"]);
                    _almacen.nomalm = row["ALM_NOM"].ToString();
                    _almacen.estadoalm = Convert.ToByte(row["ALM_ACT"]);
                    _almacen.iddefault = Convert.ToByte(row["ALM_DEFAULT"]);
                    almacen.Add(_almacen);
                }
            }
            catch (Exception ex)
            {
                almacen = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return almacen;
        }
        public ObservableCollection<ConsumoInterno> GetConsumoInterno()
        {
            ObservableCollection<ConsumoInterno> almacen = new ObservableCollection<ConsumoInterno>();
            try
            {
                DataTable dt = datAlmacen.GetConsumoInterno();
                foreach (DataRow row in dt.Rows)
                {
                    ConsumoInterno ci = new ConsumoInterno();
                    ci.ID = Convert.ToInt32(row["ID"]);
                    ci.CI_ID_EMPLEADO = Convert.ToInt32(row["CI_ID_EMPLEADO"]);
                    ci.EMPL_NOM = row["EMPL_NOM"].ToString();
                    ci.CI_ID_TIPO_CONSUMO = Convert.ToInt32(row["CI_ID_TIPO_CONSUMO"]);
                    ci.TC_DESCR = row["TC_DESCR"].ToString();
                    ci.CI_CANT = Math.Round(Convert.ToDecimal(row["CI_CANT"]),3);
                    ci.CI_ID_CARTA = Convert.ToInt32(row["CI_ID_CARTA"]);
                    ci.CAR_NOM = row["CAR_NOM"].ToString();
                    ci.CI_ID_INSUMO = Convert.ToInt32(row["CI_ID_INSUMO"]);
                    ci.INS_NOM = row["INS_NOM"].ToString();
                    ci.CI_F_CREATE = Convert.ToDateTime(row["CI_F_CREATE"]);
                    ci.CI_F_MODIFICACION = Convert.ToDateTime(row["CI_F_MODIFICACION"]);
                    ci.CI_OBS = row["CI_OBS"].ToString();
                    almacen.Add(ci);
                }

            }
            catch (Exception ex)
            {
                almacen = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return almacen;
        }
        public ObservableCollection<ConsumoInterno> GetConsumoInternoFecha(DateTime? desde, DateTime? hasta)
        {
            ObservableCollection<ConsumoInterno> almacen = new ObservableCollection<ConsumoInterno>();
            try
            {
                DataTable dt = datAlmacen.GetConsumoInternoFecha(desde, hasta);
                foreach (DataRow row in dt.Rows)
                {
                    ConsumoInterno ci = new ConsumoInterno();
                    ci.ID = Convert.ToInt32(row["ID"]);
                    ci.CI_ID_EMPLEADO = Convert.ToInt32(row["CI_ID_EMPLEADO"]);
                    ci.EMPL_NOM = row["EMPL_NOM"].ToString();
                    ci.CI_ID_TIPO_CONSUMO = Convert.ToInt32(row["CI_ID_TIPO_CONSUMO"]);
                    ci.TC_DESCR = row["TC_DESCR"].ToString();
                    ci.CI_CANT = Math.Round(Convert.ToDecimal(row["CI_CANT"]), 3);
                    ci.CI_ID_CARTA = Convert.ToInt32(row["CI_ID_CARTA"]);
                    ci.CAR_NOM = row["CAR_NOM"].ToString();
                    ci.CI_ID_INSUMO = Convert.ToInt32(row["CI_ID_INSUMO"]);
                    ci.INS_NOM = row["INS_NOM"].ToString();
                    ci.CI_F_CREATE = Convert.ToDateTime(row["CI_F_CREATE"]);
                    ci.CI_F_MODIFICACION = Convert.ToDateTime(row["CI_F_MODIFICACION"]);
                    ci.CI_OBS = row["CI_OBS"].ToString();
                    almacen.Add(ci);
                }

            }
            catch (Exception ex)
            {
                almacen = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return almacen;
        }
        public ObservableCollection<TipoConsumo> GetTipoConsumo()
        {
            ObservableCollection<TipoConsumo> almacen = new ObservableCollection<TipoConsumo>();
            try
            {
                DataTable dt = datAlmacen.GetTipoConsumo();
                foreach (DataRow row in dt.Rows)
                {
                    TipoConsumo tc = new TipoConsumo();
                    tc.ID = Convert.ToInt32(row["ID"]);
                    tc.TC_DESCR = row["TC_DESCR"].ToString();
                    tc.TC_F_CREATE = Convert.ToDateTime(row["TC_F_CREATE"]);
                    tc.TC_F_MODIFICACION = Convert.ToDateTime(row["TC_F_MODIFICACION"]);
                    almacen.Add(tc);
                }

            }
            catch (Exception ex)
            {
                almacen = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return almacen;
        }
        
        public bool SetAlmacen(int operacion, Almacen almacen, ref string _mensaje)
        {
            bool result = false;
            result = datAlmacen.SetAlmancen(operacion, almacen, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public bool SetConsumoInterno(int operacion,ConsumoInterno ci, Empleado emp,TipoConsumo tc,Platos platos,Insumos ins,decimal cant_plato,decimal cant_ins,int idalm, string obs)
        {
            bool result = false;
            result = datAlmacen.SetConsumoInterno(operacion,ci, emp,tc,platos,ins,cant_plato,cant_ins, idalm, obs);
            return result;
        }
    }
}

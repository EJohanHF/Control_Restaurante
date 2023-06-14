using Capa_Datos.Stock;
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
     public class Neg_Insumos
     {
        Funcion_Global globales = new Funcion_Global();
        Dat_Insumos datInsumo = new Dat_Insumos();
        public ObservableCollection<Insumos> GetInsumo()
        {
            ObservableCollection<Insumos> insumo = new ObservableCollection<Insumos>();
            try
            {
                DataTable dt = datInsumo.GetInsumo();
                foreach (DataRow row in dt.Rows)
                {
                    Insumos _insumo = new Insumos();
                    _insumo.idins = Convert.ToInt32(row["ID"]);
                    _insumo.nomins = row["INS_NOM"].ToString();

                    //_insumo.idcat = row["IDCAT"].ToString();
                    //_insumo.nomcat = row["CAT_NOM"].ToString();

                    _insumo.idaum = row["IDUM"].ToString();
                    _insumo.nomaum = row["TM_DESC"].ToString();

                    _insumo.cantminins = Convert.ToDecimal(row["INS_CANT_MIN"]);
                    _insumo.precio = Convert.ToDecimal(row["INS_PRECIO"]);
                    //_insumo.condins = row["INS_CONDICION"].ToString();
                    _insumo.estadoins = Convert.ToByte(row["INS_ESTADO"]);

                    _insumo.tipoins = row["INS_TIPO"].ToString();
                    _insumo.nomtipo = row["NOM_TIPO"].ToString();
                    insumo.Add(_insumo);

                }

            }
            catch (Exception ex)
            {
                insumo = null;
                //globales.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return insumo;
        }
        public ObservableCollection<Insumos> GetInsumoySubReceta()
        {
            ObservableCollection<Insumos> insumo = new ObservableCollection<Insumos>();
            try
            {
                DataTable dt = datInsumo.GetInsumoySubReceta();
                foreach (DataRow row in dt.Rows)
                {
                    Insumos _insumo = new Insumos();
                    _insumo.idins = Convert.ToInt32(row["ID"]);
                    _insumo.nomins = row["INS_NOM"].ToString();
                    _insumo.idaum = row["IDUM"].ToString();
                    _insumo.nomaum = row["TM_DESC"].ToString();
                    _insumo.cantminins = Convert.ToDecimal(row["INS_CANT_MIN"]);
                    _insumo.estadoins = Convert.ToByte(row["INS_ESTADO"]);
                    _insumo.tipoins = row["INS_TIPO"].ToString();
                    _insumo.SUBRECETA= Convert.ToBoolean(row["SUBRECETA"]);

                    if(_insumo.SUBRECETA == true) 
                    {
                        _insumo.sren = "Visible";
                    }
                    else 
                    {
                        _insumo.sren = "Hidden";
                    }
                    insumo.Add(_insumo);

                }

            }
            catch (Exception ex)
            {
                insumo = null;
                //globales.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return insumo;
        }
        #region InsumoAlmacen
        public List<Almacen> getInsumoAlmacen(string id, ref string _mensaje)
        {
            List<Almacen> insumoalmacen = null;
            try
            {
                insumoalmacen = new List<Almacen>();
                DataTable dt = datInsumo.getInsumoAlmacen(id);
                foreach (DataRow item in dt.Rows)
                {
                    Almacen _insAlm = new Almacen();
                    _insAlm.idalm = Convert.ToInt32(item["ID"]);
                    _insAlm.nomalm = item["ALM_NOM"].ToString();
                    _insAlm.estadoalm = Convert.ToByte(item["ALM_ACT"]);
                    insumoalmacen.Add(_insAlm);
                }
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                insumoalmacen = null;
                //globales.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return insumoalmacen;
        }
        #endregion
        public bool SetInsumo(int operacion, Insumos insumos, ref string _mensaje)
        {
            bool result = false;
            result = datInsumo.SetInsumo(operacion, insumos, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }

        public ObservableCollection<Insumos> GetAlmacenInsumo()
        {
            ObservableCollection<Insumos> insumo = new ObservableCollection<Insumos>();
            try
            {
                DataTable dt = datInsumo.getAlmacenInsumo();
                foreach (DataRow row in dt.Rows)
                {
                    Insumos _insumo = new Insumos();
                    _insumo.ID = Convert.ToInt32(row["ID"]);
                    _insumo.idins = Convert.ToInt32(row["ID_INS"]);
                    _insumo.nomins = row["INS_NOM"].ToString();
                    _insumo.ID_ALMA = Convert.ToInt32(row["ID_ALMA"]);
                    _insumo.almacen = row["ALM_NOM"].ToString();
                    _insumo.INS_CANTIDAD = Convert.ToInt32(row["CANT"]);
                    _insumo.F_CREATE = Convert.ToDateTime(row["F_CREATE"]);
                    _insumo.F_MODIFICACION = Convert.ToDateTime(row["F_MODIFICACION"]);

                    insumo.Add(_insumo);
                }
            }
            catch (Exception ex)
            {
                insumo = null;
                //globales.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return insumo;
        }
        public string ALERTA_INSUMO(int id_carta)
        {
            string mensaje = "";
            try
            {
                DataTable dt = new DataTable();
                dt = datInsumo.getAlertaInsumo(id_carta);
                if (dt.Rows.Count > 0)
                {
                    mensaje = dt.Rows[0][0].ToString();
                }
                else
                {
                    mensaje = "";
                }
            }
            catch (Exception ex)
            {
                mensaje = "";
                ////globales.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return mensaje;
        }
        public ObservableCollection<Insumos> GetInsumoTotal()
        {
            ObservableCollection<Insumos> insumo = new ObservableCollection<Insumos>();
            try
            {
                DataTable dt = datInsumo.GetInsumoTotal();
                foreach (DataRow row in dt.Rows)
                {
                    Insumos _insumo = new Insumos();
                    _insumo.ID = Convert.ToInt32(row["ID"]);
                    _insumo.idins = Convert.ToInt32(row["ID_INS"]);
                    _insumo.nomins = row["INS_NOM"].ToString();
                    _insumo.cantminins = Convert.ToDecimal(row["INS_CANT_MIN"]);
                    _insumo.precio = Convert.ToDecimal(row["INS_PRECIO"]);
                    _insumo.estadoins = Convert.ToByte(row["INS_ESTADO"]);
                    _insumo.ID_ALMA = Convert.ToInt32(row["ID_ALMA"]);
                    insumo.Add(_insumo);

                }

            }
            catch (Exception ex)
            {
                insumo = null;
                //globales.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return insumo;
        }
    }
}

using Capa_Datos.Receta;
using Capa_Datos.Stock;
using Capa_Entidades.Models.Receta;
using Capa_Entidades.Models.Stock;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace Capa_Negocio.Receta
{
    public class Neg_Receta
    {
        Dat_Receta datReceta = new Dat_Receta();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Recetas> GetReceta()
        {
            ObservableCollection<Recetas> receta = new ObservableCollection<Recetas>();
            try
            {
                DataTable dt = datReceta.GetReceta();
                foreach (DataRow row in dt.Rows)
                {
                    Recetas _receta = new Recetas();
                    _receta.ID = Convert.ToInt32(row["ID"]);
                    _receta.RE_ID_CARTA = Convert.ToInt32(row["RE_ID_CARTA"]);
                    _receta.INS_NOM = row["INS_NOM"].ToString();
                    _receta.RE_ID_INS = Convert.ToInt32(row["RE_ID_INS"]);
                    _receta.RE_ACT = Convert.ToBoolean(row["RE_ACT"]);
                    _receta.RE_F_CREATE = Convert.ToDateTime(row["RE_F_CREATE"]);
                    _receta.RE_CANT_INS = Convert.ToDecimal(row["RE_CANT_INS"]);
                    _receta.TM_DESC = row["TM_DESC"].ToString();
                    _receta.RE_CANT_MED_INS = row["RE_CANT_MED_INS"].ToString();
                    _receta.RE_COSTO_RECETA = Convert.ToDecimal(row["RE_COSTO_RECETA"]);
                    _receta.SR_ID = Convert.ToInt32(row["RE_ID_SUB_RECETA"]);
                    _receta.RE_INS_ACT = Convert.ToBoolean(row["RE_INS_ACT"]);
                    if (_receta.SR_ID > 0)
                    {
                        _receta.VisibilityEdit = "Hidden";
                    }
                    else 
                    {
                        _receta.VisibilityEdit = "Visible";
                    }
                    receta.Add(_receta);
                }
            }
            catch (Exception ex)
            {
                receta = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return receta;
        }
        public ObservableCollection<Recetas> GetSubReceta()
        {
            ObservableCollection<Recetas> receta = new ObservableCollection<Recetas>();
            try
            {
                DataTable dt = datReceta.GetSubReceta();
                foreach (DataRow row in dt.Rows)
                {
                    Recetas _receta = new Recetas();
                    _receta.SR_ID = Convert.ToInt32(row["ID"]);
                    _receta.SR_DESCR = row["SR_DESCR"].ToString();
                    _receta.SR_ACT = Convert.ToBoolean(row["SR_ACT"]);
                    _receta.SR_COSTO = Convert.ToDecimal(row["SR_COSTO"]);
                    _receta.SR_F_CREATE = Convert.ToDateTime(row["SR_F_CREATE"]);
                    _receta.SR_F_MODIFICACION = Convert.ToDateTime(row["SR_F_MODIFICACION"]);
                    if (_receta.SR_ACT == true)
                    {
                        _receta.TextoActDes = "Desactivar";
                        _receta.iconoActDes = "Close";
                    }
                    else
                    {
                        _receta.TextoActDes = "Activar";
                        _receta.iconoActDes = "CheckBold";
                    }
                    receta.Add(_receta);
                }
            }
            catch (Exception ex)
            {
                receta = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return receta;
        }

        public ObservableCollection<Detalle_SubReceta_Insumo> GetDetalleSubRecetaInsumo()
        {
            ObservableCollection<Detalle_SubReceta_Insumo> DetSubRecetaInsumo = new ObservableCollection<Detalle_SubReceta_Insumo>();
            try
            {
                DataTable dt = datReceta.GetDetalleSubRecetaInsumo();
                foreach (DataRow row in dt.Rows)
                {
                    Detalle_SubReceta_Insumo _detsrins = new Detalle_SubReceta_Insumo();
                    _detsrins.ID = Convert.ToInt32(row["ID"]);
                    _detsrins.DSI_ID_INS = Convert.ToInt32(row["DSI_ID_INS"]);
                    _detsrins.INS_NOM = Convert.ToString(row["INS_NOM"]);
                    _detsrins.DSI_ID_SUB_RECETA = Convert.ToInt32(row["DSI_ID_SUB_RECETA"]);
                    _detsrins.DSI_ACT = Convert.ToBoolean(row["DSI_ACT"]);
                    _detsrins.DSI_COSTO_INS = Convert.ToDecimal(row["DSI_COSTO_INS"]);
                    _detsrins.DSI_F_CREATE = Convert.ToDateTime(row["DSI_F_CREATE"]);
                    _detsrins.DSI_F_MODIFICACION = Convert.ToDateTime(row["DSI_F_MODIFICACION"]);
                    _detsrins.DSI_CANT_INS = Convert.ToDecimal(row["DSI_CANT_INS"]);
                    _detsrins.SR_DESCR = row["SR_DESCR"].ToString();

                    DetSubRecetaInsumo.Add(_detsrins);
                }
            }
            catch (Exception ex)
            {
                DetSubRecetaInsumo = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return DetSubRecetaInsumo;
        }

        public bool SetReceta(int operacion, Recetas receta, DataTable dtt, ref string _mensaje)
        {
            bool result = false;
            result = datReceta.SetReceta(operacion, receta, dtt, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public bool SetSubReceta(int operacion, Recetas receta, DataTable dtt, ref string _mensaje)
        {
            bool result = false;
            result = datReceta.SetSubReceta(operacion, receta, dtt, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }

    }
}

using Capa_Datos.Centro_Costos;
using Capa_Entidades.Models.Centro_Costos;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Centro_Costos
{
    public class Neg_CentroCostos
    {
        DatCentroCostos datCentroCostos = new DatCentroCostos();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public ObservableCollection<CentroCostos> GetCentroCostos()
        {
            ObservableCollection<CentroCostos> cfijos = new ObservableCollection<CentroCostos>();
            try
            {
                DataTable dt = datCentroCostos.GetCentroCostos();
                foreach (DataRow row in dt.Rows)
                {
                    CentroCostos _td = new CentroCostos();
                    _td.ID = Convert.ToInt32(row["ID"]);
                    _td.CC_MES = row["CC_MES"].ToString();
                    _td.CC_TIPO = Convert.ToInt32(row["CC_TIPO"]);
                    _td.TP_DENOMINACION = row["TP_DENOMINACION"].ToString();
                    _td.CATEGORIA = Convert.ToInt32(row["CATEGORIA"]);
                    _td.CAT_DESCR = row["CAT_DESCR"].ToString();
                    _td.CC_MONTO = Convert.ToDecimal(row["CC_MONTO"]);
                    _td.CC_F_REG = Convert.ToDateTime(row["CC_F_REG"]);
                    _td.CC_F_MODIFICACION = Convert.ToDateTime(row["CC_F_MODIFICACION"]);
                    _td.CC_AÑO = row["CC_AÑO"].ToString();
                    _td.CC_OBS = row["CC_OBS"].ToString();
                    cfijos.Add(_td);
                }
            }
            catch (Exception ex)
            {
                cfijos = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3) ;
            }
            return cfijos;
        }
        public ObservableCollection<TiposCostos> GetTiposCostos()
        {
            ObservableCollection<TiposCostos> tcostos = new ObservableCollection<TiposCostos>();
            try
            {
                DataTable dt = datCentroCostos.GetTiposCostos();
                foreach (DataRow row in dt.Rows)
                {
                    TiposCostos _td = new TiposCostos();
                    _td.ID = Convert.ToInt32(row["ID"]);
                    _td.TP_CODIGO = row["TP_CODIGO"].ToString();
                    _td.TP_DENOMINACION = row["TP_DENOMINACION"].ToString();
                    _td.TP_TIPO = Convert.ToInt32(row["TP_TIPO"]);
                    _td.CAT_DESCR = row["CAT_DESCR"].ToString();
                    _td.TP_DENOMINACION = row["TP_DENOMINACION"].ToString();
                    _td.TP_F_CREATE = Convert.ToDateTime(row["TP_F_CREATE"]);
                    _td.TP_F_MODIFICACION = Convert.ToDateTime(row["TP_F_MODIFICACION"]);
                    _td.TP_CLASE = Convert.ToBoolean(row["TP_CLASE"]);
                    _td.TP_CLASE_NOM = row["TP_CLASE_NOM"].ToString();
                    _td.TP_ACT = Convert.ToBoolean(row["TP_ACT"]);
                    if (_td.TP_ACT == true)
                    {
                        _td.icon = "WindowClose";
                        _td.tooltipboton = "Desactivar";
                    }
                    else
                    {
                        _td.icon = "CheckBold";
                        _td.tooltipboton = "Activar";
                    }
                    tcostos.Add(_td);
                }
            }
            catch (Exception ex)
            {
                tcostos = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return tcostos;
        }
        
        public ObservableCollection<CentroCostos> GetDetPrecioProd(int id,int op, CentroCostos cc)
        {
            ObservableCollection<CentroCostos> tcostos = new ObservableCollection<CentroCostos>();
            try
            {
                DataTable dt = datCentroCostos.GetDetPrecioProd(id,op,cc);
                foreach (DataRow row in dt.Rows)
                {
                    CentroCostos _td = new CentroCostos();
                    _td.RE_CANT_INS = Convert.ToDecimal(row["RE_CANT_INS"]);
                    _td.TM_DESC = row["TM_DESC"].ToString();
                    _td.INS_NOM = row["INS_NOM"].ToString();
                    _td.RE_PRE_UNIT = Convert.ToDecimal(row["RE_PRE_UNIT"]);
                    _td.RE_COSTO_RECETA = Convert.ToDecimal(row["RE_COSTO_RECETA"]);
                    _td.COSTO_FIJO = Convert.ToDecimal(row["COSTO_FIJO"]);
                    _td.COSTO_VARIABLE = Convert.ToDecimal(row["COSTO_VARIABLE"]);
                    tcostos.Add(_td);
                }
            }
            catch (Exception)
            {
                tcostos = null;
            }
            return tcostos;
        }

        public bool SetCentroCostos(int operacion, CentroCostos cCostos, ref string _mensaje)
        {
            bool result = false;
            result = datCentroCostos.SetCentroCosto(operacion, cCostos, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public bool setTiposCostos(int operacion, TiposCostos tcostos, ref string _mensaje)
        {
            bool result = false;
            result = datCentroCostos.setTiposCostos(operacion, tcostos, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }

    }
}

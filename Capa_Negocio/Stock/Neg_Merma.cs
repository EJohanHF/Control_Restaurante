using Capa_Datos.Data.Stock;
using Capa_Entidades.Models.Stock;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Stock
{
    public class Neg_Merma
    {
        Dat_Merma datMermas = new Dat_Merma();
        Funcion_Global globales = new Funcion_Global();
        public List<Ent_Merma> GetMermas(DateTime? desde, DateTime? hasta, int id_insumo)
        {
            List<Ent_Merma> merma = new List<Ent_Merma>();
            try
            {
                DataTable dt = datMermas.GetMermas(desde, hasta, id_insumo);
                foreach (DataRow row in dt.Rows)
                {
                    Ent_Merma _merma = new Ent_Merma();
                    _merma.ID = Convert.ToInt32(row["ID"]);
                    //_merma.MI_ID_INS = Convert.ToInt32(row["MI_ID_INS"]);
                    _merma.MI_ID_INS = row["MI_ID_INS"].ToString();
                    _merma.MI_CANT = Convert.ToDecimal(row["MI_CANT"]);
                    _merma.INS_NOM = row["INS_NOM"].ToString();
                    _merma.MI_ID_USU = Convert.ToInt32(row["MI_ID_USU"]);
                    _merma.USU_NOM = row["USU_NOM"].ToString();
                    _merma.MI_DESCR = row["MI_DESCR"].ToString();
                    _merma.MI_F_CREATE = Convert.ToDateTime(row["MI_F_CREATE"]);
                    _merma.MI_ID_ALM = Convert.ToInt32(row["MI_ID_ALM"]);
                    _merma.ALM_NOM = row["ALM_NOM"].ToString();
                    merma.Add(_merma);
                }
            }
            catch (Exception ex)
            {
                merma = null;
            }
            return merma;
        }
        public List<Insumos> GetComboInsumos()
        {
            List<Insumos> ins = new List<Insumos>();
            try
            {
                DataTable dt = datMermas.GetComboInsumos();
                foreach (DataRow row in dt.Rows)
                {
                    Insumos _ins = new Insumos();
                    _ins.ID = Convert.ToInt32(row["ID"]);
                    _ins.nomins = row["INS_NOM"].ToString();
                    ins.Add(_ins);
                }
            }
            catch (Exception ex)
            {
                ins = null;
            }
            return ins;
        }
        public DataTable GetCantidadxInsumo(int idins, int idalma)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datMermas.GetCantidadxInsumo(idins, idalma);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }
        public bool setMerma(int op, Ent_Merma Merma)
        {
            bool result = false;
            result = datMermas.setMerma(op, Merma);
            return result;
        }
    }
}

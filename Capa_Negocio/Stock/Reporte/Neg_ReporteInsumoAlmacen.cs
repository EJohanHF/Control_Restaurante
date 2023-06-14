using Capa_Datos.Data.Stock;
using Capa_Datos.Data.Stock.Reporte;
using Capa_Entidades.Models.Stock;
using Capa_Entidades.Models.Stock.Reporte;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Stock.Reporte
{
    public class Neg_ReporteInsumoAlmacen
    {
        Dat_Report_InsumoAlmacen datAlmacenIns = new Dat_Report_InsumoAlmacen();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<ReporteInsumoAlmacen> GetInsumoAlmacen(string idalm)
        {
            ObservableCollection<ReporteInsumoAlmacen> reportInAl = new ObservableCollection<ReporteInsumoAlmacen>();
            try
            {
                DataTable dt = datAlmacenIns.GetInsumoAlmacen(idalm);
                foreach (DataRow row in dt.Rows)
                {
                    ReporteInsumoAlmacen _report = new ReporteInsumoAlmacen();
                    _report.id = Convert.ToInt32(row["ID"]);
                    _report.id_insum = Convert.ToInt32(row["ID_INS"]);
                    _report.nomal = row["ALM_NOM"].ToString();
                    _report.nomins = row["INS_NOM"].ToString();
                    _report.umed = row["TM_DESC"].ToString();
                    _report.cant = Convert.ToDecimal(row["CANT"]);
                    reportInAl.Add(_report);
                }
            }
            catch (Exception ex)
            {
                reportInAl = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return reportInAl;
        }
        public ObservableCollection<ReporteInsumoAlmacen> GetInsumoAlmacenxNombre(string idalm, string nombre)
        {
            ObservableCollection<ReporteInsumoAlmacen> reportInAl = new ObservableCollection<ReporteInsumoAlmacen>();
            try
            {
                DataTable dt = datAlmacenIns.GetInsumoAlmacenxNombre(idalm, nombre);
                foreach (DataRow row in dt.Rows)
                {
                    ReporteInsumoAlmacen _report = new ReporteInsumoAlmacen();
                    _report.id = Convert.ToInt32(row["ID"]);
                    _report.id_insum = Convert.ToInt32(row["ID_INS"]);
                    _report.nomal = row["ALM_NOM"].ToString();
                    _report.nomins = row["INS_NOM"].ToString();
                    _report.umed = row["TM_DESC"].ToString();
                    _report.cant = Convert.ToDecimal(row["CANT"]);
                    reportInAl.Add(_report);
                }
            }
            catch (Exception ex)
            {
                reportInAl = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return reportInAl;
        }
        public ObservableCollection<ReporteInsumoAlmacen> GetListaInsumoAlmacen()
        {
            ObservableCollection<ReporteInsumoAlmacen> reportInAl = new ObservableCollection<ReporteInsumoAlmacen>();
            try
            {
                DataTable dt = datAlmacenIns.GetListaInsumoAlmacen();
                foreach (DataRow row in dt.Rows)
                {
                    ReporteInsumoAlmacen _report = new ReporteInsumoAlmacen();
                    _report.id = Convert.ToInt32(row["ID"]);
                    _report.id_insum = Convert.ToInt32(row["ID_INS"]);
                    _report.id_alm = Convert.ToInt32(row["ID_ALM"]);
                    _report.nomal = row["ALM_NOM"].ToString();
                    _report.nomins = row["INS_NOM"].ToString();
                    //_report.cant = Convert.ToDecimal(row["CANT"]);
                    _report.cant = Convert.ToDecimal(row["CANT"]);
                    reportInAl.Add(_report);
                }
            }
            catch (Exception ex)
            {
                reportInAl = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return reportInAl;
        }
        public List<ReporteInsumoAlmacen> Get_Lista_Combo_Almacen()
        {
            List<ReporteInsumoAlmacen> menu = new List<ReporteInsumoAlmacen>();
            try
            {
                DataTable dt = new DataTable();
                dt = datAlmacenIns.GetListaInsumoAlmacen();
                menu = (from DataRow dr in dt.Rows
                        select new ReporteInsumoAlmacen()
                        {
                            id = Convert.ToInt32(dr["ID"]),
                            id_insum = Convert.ToInt32(dr["ID_INS"]),
                            id_alm = Convert.ToInt32(dr["ID_ALM"]),
                            nomal = dr["ALM_NOM"].ToString(),
                            nomins = dr["INS_NOM"].ToString(),
                            //_report.cant = Convert.ToDecimal(row["CANT"]);
                            cant = Convert.ToDecimal(dr["CANT"])
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #region Dashboar AlamcenCantidad Dashboard
        public List<ReporteInsumoAlmacen> GetAlmacenCant()
        {
            List<ReporteInsumoAlmacen> menu = new List<ReporteInsumoAlmacen>();
            try
            {
                DataTable dt = new DataTable();
                dt = datAlmacenIns.GetAlmacenCant();
                menu = (from DataRow dr in dt.Rows
                        select new ReporteInsumoAlmacen()
                        {
                            AC_NomAlm = dr["ALM_NOM"].ToString(),
                            AC_Cant_Prod = Convert.ToInt32(dr["ID_INS"])
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion

        #region Dashboar Alamcen Detalle insumos
        public List<ReporteInsumoAlmacen> GetAlmacenDetInusmo(string idins)
        {
            List<ReporteInsumoAlmacen> menu = new List<ReporteInsumoAlmacen>();
            try
            {
                DataTable dt = new DataTable();
                dt = datAlmacenIns.GetAlmacenDetInusmo(idins);
                menu = (from DataRow dr in dt.Rows
                        select new ReporteInsumoAlmacen()
                        {
                            AI_NomAlmacen = dr["ALM_NOM"].ToString(),
                            AI_NomInsumo = dr["INS_NOM"].ToString(),
                            AI_Cant_Prod = Convert.ToDecimal(dr["CANT"]),
                            AI_UM_Deno = dr["TM_DESC"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
    }
}

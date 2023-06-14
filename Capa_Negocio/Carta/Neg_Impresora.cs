using Capa_Datos.Carta;
using Capa_Entidades;
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
  public class Neg_Impresora
    {
        Dat_Impresora dataImp = new Dat_Impresora();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Impresora> GetImpre()
        {
            ObservableCollection<Impresora> impresora = new ObservableCollection<Impresora>();
            try
            {
                DataTable dt = dataImp.GetImpresora();
                foreach (DataRow row in dt.Rows)
                {
                    Impresora _impre = new Impresora();
                    _impre.idimp = Convert.ToInt32(row["IDIMP"]);
                    _impre.ipimp =row["IPIMP"].ToString();
                    _impre.nomimp = row["NOMIMP"].ToString();
                    _impre.estadoimp = row["ESTADOIMP"].ToString();
                    _impre.nomequipoimp= row["NOMEQUIPOIMP"].ToString();
                    _impre.nomimpppedido = row["NOMIMPPPEDIDO"].ToString();
                    impresora.Add(_impre);
                }
            }
            catch (Exception ex)
            {
                impresora = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return impresora;
        }
        public bool SetImpresora(int operacion, Impresora impresora, ref string _mensaje)
        {
            bool result = false;
            result = dataImp.SetImpresora(operacion, impresora, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public List<Ent_Combo> GetListaImpresoras()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IMPRESORA");
                foreach (string prtn in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    DataRow dr = dt.NewRow();
                    //var row = dt.NewRow();
                    dr["IMPRESORA"] = prtn;
                    dt.Rows.Add(dr);
                }
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            nombre = dr["IMPRESORA"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
            //public DataTable GetListaImpresoras()
            //{
            //    DataTable dt = new DataTable();
            //    DataColumn column = new DataColumn();
            //    column.ColumnName = "IMPRESORA";
            //    try
            //    {
            //        foreach (string prtn in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            //        {

            //            dt.Rows.Add(prtn);
            //        }
            //        //dt = datDetPed.GetCanaldeVenta();
            //    }
            //    catch (Exception)
            //    {
            //        dt = null;
            //    }
            //    return dt;
            //}
            /**/
        }
}

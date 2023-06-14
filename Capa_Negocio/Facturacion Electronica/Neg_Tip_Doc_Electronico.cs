using Capa_Datos.Facturacion_Electronica;
using Capa_Entidades.Models.Facturacion_Electronica;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Facturacion_Electronica
{
    public class Neg_Tip_Doc_Electronico
    {
        Dat_Tipo_Doc_Electronico datTipDocElect = new Dat_Tipo_Doc_Electronico();
        Funcion_Global globales = new Funcion_Global();
        public List<Tipo_Doc_Electronico> GetTipDocElectronico()
        {
            List<Tipo_Doc_Electronico> TipDocElectronico = new List<Tipo_Doc_Electronico>();
            try
            {
                DataTable dt = new DataTable();
                dt = datTipDocElect.GetTipDocElectronico();
                TipDocElectronico = (from DataRow dr in dt.Rows
                         select new Tipo_Doc_Electronico()
                         {
                             ID = Convert.ToInt32(dr["ID"]),
                             NOM_TIPO_DOC = Convert.ToString(dr["NOM_TIPO_DOC"]),
                             VALOR_TIPO_DOC = Convert.ToString(dr["VALOR_TIPO_DOC"]),
                             EST_TIP_DOC = Convert.ToString(dr["EST_TIP_DOC"])
                         }).ToList();
            }
            catch (Exception ex)
            {
                TipDocElectronico = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return TipDocElectronico;
        }
        public List<Serie_Numero_Documento> GetSerieNumeroDocumento()
        {
            List<Serie_Numero_Documento> SerieNumeroDocumento = new List<Serie_Numero_Documento>();
            try
            {
                DataTable dt = new DataTable();
                dt = datTipDocElect.GetSerieNumeroDocumento();
                SerieNumeroDocumento = (from DataRow dr in dt.Rows
                                     select new Serie_Numero_Documento()
                                     {
                                         ID = Convert.ToInt32(dr["ID"]),
                                         SERIE = Convert.ToString(dr["SERIE"]),
                                         VALOR = Convert.ToString(dr["VALOR"]),
                                         EST = Convert.ToString(dr["EST"]),
                                         FEC_CREAC = Convert.ToDateTime(dr["FEC_CREAC"]),
                                         FEC_UPDA = Convert.ToDateTime(dr["FEC_UPDA"])
                                     }).ToList();
            }
            catch (Exception ex)
            {
                SerieNumeroDocumento = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return SerieNumeroDocumento;
        }

        public string GetSerieNumero(string serie)
        {
            DataTable dt = new DataTable();
            string serienumero = "";
            try
            {
                dt = datTipDocElect.GetSerieNumero(serie);
                serienumero = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                serienumero = "";
                dt = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return serienumero;
        }
    }
}

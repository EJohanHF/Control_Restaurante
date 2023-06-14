using Capa_Datos.Configuracion;
using Capa_Datos.Data.Configuracion;
using Capa_Entidades.Models.Factura_Compra;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Configuracion
{
    public class Neg_Tipodoc
    {
        Dat_TipoDoc datTipoDoc = new Dat_TipoDoc();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<TipoDoc> GetTipoDoc()
        {
            ObservableCollection<TipoDoc> tipDoc = new ObservableCollection<TipoDoc>();
            try
            {
                DataTable dt = datTipoDoc.GetTipoDoc();
                foreach (DataRow row in dt.Rows)
                {
                    TipoDoc _td = new TipoDoc();
                    _td.ID = Convert.ToInt32(row["ID"]);
                    _td.DC_DESCR = row["DC_DESCR"].ToString();
                    _td.DC_ACT = Convert.ToBoolean(row["DC_ACT"]);
                    _td.DC_ID_SERIE = Convert.ToInt32(row["DC_ID_SERIE"]);
                    _td.DC_F_CREATE = Convert.ToDateTime(row["DC_F_CREATE"]);
                    _td.SERIE = row["SERIE"].ToString();
                    tipDoc.Add(_td);
                }

            }
            catch (Exception ex)
            {
                tipDoc = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return tipDoc;
        }
    }
}

using Capa_Datos.Data.Configuracion;
using Capa_Entidades.Models.Configuracion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Configuracion
{
    public class NegTipoCambio
    {
        Dat_TipoCambio dat_tipocambio = new Dat_TipoCambio();
        public ObservableCollection<TipoCambio> GetTiposCambio()
        {
            ObservableCollection<TipoCambio> tc = new ObservableCollection<TipoCambio>();
            try
            {
                DataTable dt = dat_tipocambio.GetTiposCambio();
                foreach (DataRow row in dt.Rows)
                {
                    TipoCambio _tc = new TipoCambio();
                    _tc.ID = Convert.ToInt32(row["ID"]);
                    _tc.TM_DESCR = row["TM_DESCR"].ToString();
                    _tc.VALOR = row["VALOR"].ToString();
                    _tc.TC_CAMBIO = Convert.ToDecimal(row["TC_CAMBIO"]);
                    tc.Add(_tc);
                }
            }
            catch (Exception ex)
            {
                tc = null;
            }
            return tc;
        }

        public bool setTipoCambio(TipoCambio tc)
        {
            bool result = false;
            result = dat_tipocambio.setTipoCambio(tc);
            return result;
        }
    }
}

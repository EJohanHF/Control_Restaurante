using Capa_Datos.Pedido;
using Capa_Entidades.Models.Pedido;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Pedido
{
    public class Neg_Estado_Financiero
    {

        Dat_Estado_Financiero datEstadoFinanciero = new Dat_Estado_Financiero();
        public ObservableCollection<Estado_Financiero> getEstadoFinanciero()
        {
            ObservableCollection<Estado_Financiero> est_f = new ObservableCollection<Estado_Financiero>();
            try
            {
                DataTable dt = datEstadoFinanciero.getEstadoFinanciero();
                foreach (DataRow row in dt.Rows)
                {
                    Estado_Financiero _est_f = new Estado_Financiero();
                    _est_f.ID = Convert.ToInt32(row["ID"]);
                    _est_f.DESC_EST = row["DESC_EST"].ToString();
                    est_f.Add(_est_f);
                }
            }
            catch (Exception ex)
            {
                est_f = null;
            }
            return est_f;
        }
    }
}

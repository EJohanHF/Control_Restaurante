using Capa_Datos.Data.Reportes;
using Capa_Entidades.Models.Reportes;
using Capa_Entidades.Models.Reportes.Pedido;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Reportes
{
    public class Neg_DpAnulados
    {
        Dat_DpAnulados dataAnulados = new Dat_DpAnulados();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<DpAnulados> GetPdAnulados(string desde, string hasta, string mes, string dia)
        {
            ObservableCollection<DpAnulados> Anulados = new ObservableCollection<DpAnulados>();
            try
            {
                DataTable dt = dataAnulados.GetDpAnulados(desde, hasta, mes, dia);
                foreach (DataRow row in dt.Rows)
                {
                    DpAnulados _anulad = new DpAnulados();
                    _anulad.IDPED = Convert.ToInt32(row["IDPED"]);
                    _anulad.PED_NUM_DIARIO = row["PED_NUM_DIARIO"].ToString();
                    _anulad.M_NOM = row["M_NOM"].ToString();
                    _anulad.PED_TOTAL = row["PED_TOTAL"].ToString();
                    _anulad.USU_NOM = row["USU_NOM"].ToString();
                    _anulad.EMPL_NOM = row["EMPL_NOM"].ToString();
                    _anulad.nomllevar = row["nomllevar"].ToString();
                    _anulad.NOM_CARGO = row["NOM_CARGO"].ToString();
                    _anulad.PED_FECH_PED = Convert.ToDateTime(row["PED_FECH_PED"]).ToString("dd/MM/yyyy HH:mm:ss");
                    Anulados.Add(_anulad);
                }
            }
            catch (Exception ex)
            {
                Anulados = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return Anulados;
        }
        public ObservableCollection<DpVentas> GetDetPedidoAnulado(string idped)
        {
            ObservableCollection<DpVentas> detPed = new ObservableCollection<DpVentas>();
            try
            {
                DataTable dt = dataAnulados.GetDetPedidoAnulado(idped);
                foreach (DataRow row in dt.Rows)
                {
                    DpVentas _detPed = new DpVentas();
                    _detPed.cant_ped = Convert.ToInt32(row["DP_CANT"]);
                    _detPed.DEt_ped = row["DP_DESCRIP"].ToString();
                    _detPed.Import_ped = Convert.ToDecimal(row["DP_IMPORT"]);
                    detPed.Add(_detPed);
                }
            }
            catch (Exception ex)
            {
                detPed = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return detPed;
        }
    }
}

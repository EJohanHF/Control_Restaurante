using Capa_Datos.Configuracion;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Configuracion;
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
    public class Neg_SuperJesus
    {
        Dat_SuperJesus dataSCat = new Dat_SuperJesus();
        Funcion_Global globales = new Funcion_Global();

        public ObservableCollection<SJesus> GetSCat()
        {
            ObservableCollection<SJesus> superjesus = new ObservableCollection<SJesus>();
            try
            {
                DataTable dt = dataSCat.GetSuperJesus();
                foreach (DataRow row in dt.Rows)
                {
                    SJesus _scat = new SJesus();
                    _scat.id = Convert.ToInt32(row["id"]);
                    _scat.deno_pago = row["deno_pago"].ToString();
                    _scat.tp_act = Convert.ToByte(row["tp_act"]);
                    superjesus.Add(_scat);
                }
            }
            catch (Exception ex)
            {
                superjesus = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return superjesus;
        }
        public bool SetsJesus(int operacion, SJesus superjesus, ref string _mensaje)
        {
            bool result = false;
            result = dataSCat.SetSuperJesus(operacion, superjesus, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
    }
}

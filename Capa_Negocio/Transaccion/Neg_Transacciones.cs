using Capa_Datos.Principal;
using Capa_Entidades.Models.Transacciones;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Transaccion
{
  public class Neg_Transacciones
    {
        Dat_Transacciones datTran = new Dat_Transacciones();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Transacciones> GetTransacciones()
        {
            ObservableCollection<Transacciones> transacciones = new ObservableCollection<Transacciones>();
            try
            {
                DataTable dt = datTran.GetTransacciones();
                foreach (DataRow row in dt.Rows)
                {
                    Transacciones _tran = new Transacciones();
                    _tran.ID = Convert.ToInt32(row["ID"]);

                    _tran.TIPO_OPERACION = row["TIPO_OPERACION"].ToString();
                    _tran.NOM_TABLA = row["NOM_TABLA"].ToString();

                    _tran.ID_CAMPO = row["ID_CAMPO"].ToString();

                    _tran.NOM_CAMPO = row["NOM_CAMPO"].ToString();
                    _tran.VALOR_ORIGINAL = row["VALOR_ORIGINAL"].ToString();
                    _tran.VALOR_NUEVO = row["VALOR_NUEVO"].ToString();
                    _tran.FECH_HORA_TRAN = Convert  .ToDateTime(row["FECH_HORA_TRAN"]);

                    _tran.IDUSU = row["IDUSU"].ToString();
                    _tran.USU_NOM = row["USU_NOM"].ToString();

                    _tran.IDROL = row["IDROL"].ToString();
                    _tran.NOM_ROL = row["NOM_ROL"].ToString();

                    _tran.NOM_EQUIPO = row["NOM_EQUIPO"].ToString();
                    _tran.ACCION = row["ACCION"].ToString();
                    _tran.IP_LOCAL = row["IP_LOCAL"].ToString();
                    transacciones.Add(_tran);
                }
            }
            catch (Exception ex)
            {
                transacciones = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return transacciones;
        }
    }
}

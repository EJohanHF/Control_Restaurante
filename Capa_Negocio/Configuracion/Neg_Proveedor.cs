using Capa_Datos.Configuracion;
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
 public   class Neg_Proveedor
    {
        Dat_Proveedor datProv = new Dat_Proveedor();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Proveedor> GetProv()
        {
            ObservableCollection<Proveedor> proveedor = new ObservableCollection<Proveedor>();
            try
            {

                DataTable dt = datProv.GetProveedor();
                foreach (DataRow row in dt.Rows)
                {
                    Proveedor _prov = new Proveedor();
                    _prov.idp = Convert.ToInt32(row["ID"]);

                    _prov.iddoc = row["id_tipo_doc"].ToString();
                    _prov.nomdoc = row["documento"].ToString();

                    _prov.prov_nrdoc = row["P_NRO_DOC"].ToString();
                    _prov.prov_nom = row["P_NOM"].ToString();
                    _prov.prov_direc = row["P_DIREC"].ToString();
                    _prov.prov_dist = row["P_DIST"].ToString();
                    _prov.prov_telfijo = row["P_TEL_FIJO"].ToString();
                    _prov.prov_telmovil  = row["P_TEL_MOV"].ToString();
                    _prov.prov_correo = row["P_COR"].ToString();
                    _prov.prov_activo =Convert.ToByte(row["P_ACT"]);
                    _prov.prov_rubro = row["P_RUBRO"].ToString();
                    proveedor.Add(_prov);
                }
            }
            catch (Exception ex)
            {
                proveedor = null;
                globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return proveedor;
        }
        public bool SetProveedor(int operacion, Proveedor proveedor, ref string _mensaje)
        {
            /*aqui se hacen las validaciones de los campos antes de enviarlos al metodo */
            bool result = false;
            result = datProv.SetProveedor(operacion, proveedor, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito.";
            }
            return result;
        }
    }
}

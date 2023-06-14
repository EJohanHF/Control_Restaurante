using Capa_Datos.WebService;
using Capa_Entidades.Models.Web_Service;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.WebService
{
    public class Neg_CartaDeliveryWebService
    {
        Funcion_Global globales = new Funcion_Global();
        Dat_CartaDeliveryWebService dataCartDWeb = new Dat_CartaDeliveryWebService();
        public ObservableCollection<CartaDeliveryWebService> GetCartaDeliveryWebService(int idcarta)
        {
            ObservableCollection<CartaDeliveryWebService> cartadelivery = new ObservableCollection<CartaDeliveryWebService>();
            try
            {
                DataTable dt = dataCartDWeb.GetCartaDeliveryWebService(idcarta);
                foreach (DataRow row in dt.Rows)
                {
                    CartaDeliveryWebService _cartadelivery = new CartaDeliveryWebService();
                    _cartadelivery.Name = row["nom_car"].ToString();
                    _cartadelivery.price = Convert.ToDecimal(row["precio"]);
                    _cartadelivery.discount = Convert.ToDecimal(row["descuento"]);
                    _cartadelivery.estado_del = Convert.ToBoolean(row["estado"]);
                    _cartadelivery.imagen = (byte[])(row["imagen"]);
                    _cartadelivery.price_salon = Convert.ToDecimal(row["precio_salon"]);
                    _cartadelivery.discount_salon = Convert.ToDecimal(row["descuento_salon"]);
                    _cartadelivery.estado_sal = Convert.ToBoolean(row["est_salon"]);
                    cartadelivery.Add(_cartadelivery);
                }
            }
            catch (Exception ex)
            {
                cartadelivery = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return cartadelivery;
        }
        public bool setCartaDeliveryWebService(CartaDeliveryWebService cartadelivery, int idcarta, bool estado)
        {
            bool result;
            result = dataCartDWeb.SetCartaDeliveryWebService(cartadelivery, idcarta, estado);
            return result;
        }
    }

}

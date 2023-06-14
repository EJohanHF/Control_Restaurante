using Capa_Datos.Carta;
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
   public class Neg_SuperCategoria
    {
        Dat_SuperCategoria dataSCat = new Dat_SuperCategoria();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<SCategoria> GetSCat()
        {
            ObservableCollection<SCategoria> supercategoria = new ObservableCollection<SCategoria>();
            try
            {
                DataTable dt = dataSCat.GetSuperCategoria();
                foreach (DataRow row in dt.Rows)
                {
                    SCategoria _scat = new SCategoria();
                    _scat.idscat = Convert.ToInt32(row["idscat"]);
                    _scat.nomscat= row["scat_nom"].ToString();
                    supercategoria.Add(_scat);
                }
            }
            catch (Exception ex)
            {
                supercategoria = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return supercategoria;
        }
        public bool SetsCategoria(int operacion, SCategoria supercategoria, ref string _mensaje)
        {
            bool result = false;
            result = dataSCat.SetSuperCategoria(operacion, supercategoria, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
    }
}

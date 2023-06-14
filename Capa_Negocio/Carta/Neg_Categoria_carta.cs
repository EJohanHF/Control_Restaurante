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
   public class Neg_Categoria_carta
    {
        Dat_Categoria dataCat = new Dat_Categoria();
        Funcion_Global globales = new Funcion_Global();

        public ObservableCollection<Categoria> GetCategoria()
        {
            ObservableCollection<Categoria> categoria = new ObservableCollection<Categoria>();
            try
            {
                DataTable dt = dataCat.GetCategoria();
                int cont = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Categoria _cat = new Categoria();
                    _cat.idcat = Convert.ToInt32(row["idcat"]);

                    _cat.idscat = Convert.ToInt32(row["idscat"]);
                    _cat.nomscat = row["scat_nom"].ToString();

                    _cat.nomcat = row["cat_nom"].ToString();
                    _cat.desccat = Convert.ToDecimal(row["cat_desc"]);
                    _cat.imagencat = (byte[])(row["cat_img"]);
                    if(cont % 2 == 0)
                    {
                        _cat.columna =0;
                    }
                    else
                    {
                        _cat.columna = 1;
                    }
                    categoria.Add(_cat);
                    cont += 1;
                }
            }
            catch (Exception ex)
            {
                categoria = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return categoria;
        }
        public ObservableCollection<Categoria> GetCategoriaTodos()
        {
            ObservableCollection<Categoria> categoria = new ObservableCollection<Categoria>();
            try
            {
                DataTable dt = dataCat.GetCategoriaTodos();
                int cont = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Categoria _cat = new Categoria();
                    _cat.idcat = Convert.ToInt32(row["IDCAT"]);
                    _cat.idscat = Convert.ToInt32(row["IDSCAT"]);
                    _cat.nomcat = row["CAT_NOM"].ToString();
                    categoria.Add(_cat);
                    cont += 1;
                }
            }
            catch (Exception ex)
            {
                categoria = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return categoria;
        }
        public bool SetsCategoria(int operacion, Categoria categoria, ref string _mensaje)
        {
            bool result = false;
            result = dataCat.SetCategoria(operacion, categoria, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public bool setImpresoraCartaxCat(Categoria categoria)
        {
            bool result = false;
            result = dataCat.setImpresoraCartaxCat(categoria);
            if (result)
            {
                result = true;
            }
            return result;
        }
        public bool AumentarPrecioxCategoria(Categoria categoria, decimal cantidad)
        {
            bool result = false;
            result = dataCat.AumentarPrecioxCategoria(categoria, cantidad);
            if (result)
            {
                result = true;
            }
            return result;
        }
        public bool DisminuirPrecioxCategoria(Categoria categoria, decimal cantidad)
        {
            bool result = false;
            result = dataCat.DisminuirPrecioxCategoria(categoria,cantidad);
            if (result)
            {
                result = true;
            }
            return result;
        }
        public ObservableCollection<Categoria> GetCategoriaPorSuperCategoria(int idsuper)
        {
            ObservableCollection<Categoria> categoria = new ObservableCollection<Categoria>();
            try
            {
                DataTable dt = dataCat.GetCategoriaxSuperCategoria(idsuper);
                foreach (DataRow row in dt.Rows)
                {
                    Categoria _cat = new Categoria();
                    _cat.idcat = Convert.ToInt32(row["idcat"]);

                    _cat.idscat = Convert.ToInt32(row["idscat"]);
                    _cat.nomscat = row["scat_nom"].ToString();

                    _cat.nomcat = row["cat_nom"].ToString();
                    _cat.desccat = Convert.ToDecimal(row["cat_desc"]);
                    _cat.imagencat = (byte[])(row["cat_img"]);
                    categoria.Add(_cat);
                }
            }
            catch (Exception ex)
            {
                categoria = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return categoria;
        }
        public ObservableCollection<Categoria> GetCatCloud()
        {
            ObservableCollection<Categoria> categoria = new ObservableCollection<Categoria>();
            try
            {
                DataTable dt = dataCat.GetCatCloud();
                foreach (DataRow row in dt.Rows)
                {
                    Categoria _cat = new Categoria();
                    _cat.idcat = Convert.ToInt32(row["idcat"]);

                    _cat.idscat = Convert.ToInt32(row["idscat"]);
                    _cat.nomscat = row["scat_nom"].ToString();

                    _cat.nomcat = row["cat_nom"].ToString();
                    _cat.desccat = Convert.ToDecimal(row["cat_desc"]);
                    _cat.imagencat = (byte[])(row["cat_img"]);
                    categoria.Add(_cat);
                }
            }
            catch (Exception ex)
            {
                categoria = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return categoria;
        }
        public int sp_cloud_update()
        {
            int result = 0;
            result = dataCat.sp_cloud_update();
            return result;


        }
    }
}

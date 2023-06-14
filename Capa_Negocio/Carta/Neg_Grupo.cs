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
   public class Neg_Grupo
    {
        Dat_Grupo dataGrupo = new Dat_Grupo();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Grupo> GetGupo()
        {
            ObservableCollection<Grupo> grupo = new ObservableCollection<Grupo>();
            try
            {
                DataTable dt = dataGrupo.GetGrupo();
                foreach (DataRow row in dt.Rows)
                {
                    Grupo _grup = new Grupo();
                    _grup.idgrup = Convert.ToInt32(row["idgrup"]);
                    _grup.idcat = row["idcat"].ToString();
                    _grup.idscat = row["idscat"].ToString();
                    _grup.nomscat = row["scat_nom"].ToString();
                    _grup.nomcat = row["cat_nom"].ToString();
                    _grup.nomgrup = row["gr_nom"].ToString();
                    _grup.imagengrup = (byte[])row["gr_img"];
                    _grup.descgrup = Convert.ToDecimal(row["gr_desc"]);
                    grupo.Add(_grup);
                }
            }
            catch (Exception ex)
            {
                grupo = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return grupo;
        }
        public ObservableCollection<Grupo> GetGupoxCategoriaTodos(int idcat)
        {
            ObservableCollection<Grupo> grupo = new ObservableCollection<Grupo>();
            try
            {
                DataTable dt = dataGrupo.GetGupoxCategoriaTodos(idcat);
                foreach (DataRow row in dt.Rows)
                {
                    Grupo _grup = new Grupo();
                    _grup.idgrup = Convert.ToInt32(row["IDGRUP"]);
                    _grup.idcat = row["IDCAT"].ToString();
                    _grup.nomgrup = row["GR_NOM"].ToString();
                    grupo.Add(_grup);
                }
            }
            catch (Exception ex)
            {
                grupo = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return grupo;
        }
        public ObservableCollection<Grupo> getGruposCombo()
        {
            ObservableCollection<Grupo> grupo = new ObservableCollection<Grupo>();
            try
            {
                DataTable dt = dataGrupo.getGruposCombo();
                foreach (DataRow row in dt.Rows)
                {
                    Grupo _grup = new Grupo();
                    _grup.idgrup = Convert.ToInt32(row["IDGRUP"]);
                    _grup.idcat = row["IDCAT"].ToString();
                    _grup.nomgrup = row["GR_NOM"].ToString();
                    grupo.Add(_grup);
                }
            }
            catch (Exception ex)
            {
                grupo = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return grupo;
        }
        public bool SetGrupo(int operacion, Grupo grupo, ref string _mensaje)
        {
            bool result = false;
            result = dataGrupo.SetGrupo(operacion, grupo, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public ObservableCollection<Grupo> GetGupoxCategoria(int id)
        {
            ObservableCollection<Grupo> grupo = new ObservableCollection<Grupo>();
            try
            {
                DataTable dt = dataGrupo.GetGrupoxCategoria(id);
                foreach (DataRow row in dt.Rows)
                {
                    Grupo _grup = new Grupo();
                    _grup.idgrup = Convert.ToInt32(row["idgrup"]);
                    _grup.idcat = row["idcat"].ToString();
                    _grup.idscat = row["idscat"].ToString();
                    _grup.nomscat = row["scat_nom"].ToString();
                    _grup.nomcat = row["cat_nom"].ToString();
                    _grup.nomgrup = row["gr_nom"].ToString();
                    _grup.imagengrup = (byte[])row["gr_img"];
                    if(row["gr_desc"] == null || row["gr_desc"].ToString() == "")
                    {
                        _grup.descgrup = 0;
                    }
                    else
                    {
                        _grup.descgrup = Convert.ToDecimal(row["gr_desc"]);
                    }
                    
                    grupo.Add(_grup);
                }
            }
            catch (Exception ex)
            {
                grupo = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return grupo;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_Datos;
using System.Data;
using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Funciones_Globales;

namespace Capa_Negocio
{
    public class Neg_Combo
    {
        Funcion_Global globales = new Funcion_Global();
        Dat_Util datUtil = new Dat_Util();
        #region GetComboTipoCaja
        public List<Ent_Combo> GetComboTipoCaja()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetTipoCaja();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["id"]),
                            nombre = Convert.ToString(dr["TM_DESCR"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboDescripciones
        public List<Ent_Combo> GetComboTitle()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetTitle();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["id"]),
                            nombre = Convert.ToString(dr["TITLE_DESCRIPTION"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboTipoCaja
        public List<Ent_Combo> GetCaja_Seleccion_Tipo(string id)
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetCajaSeleccion(id);
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["id"]),
                            nombre = Convert.ToString(dr["MC_DESCR"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetTipoDocumento
        public List<Ent_Combo> GetComboTipoDI()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetTipoDI();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id   = Convert.ToString(dr["id_tipo_doc"]),
                            nombre = Convert.ToString(dr["documento"]),
                            valor1 = dr["valor"]
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }

        
        #endregion
        #region GetComboCategoriaxSupercat
        public List<Categoria> GetCategoriaxSC(int scat)
        {
            List<Categoria> menu = new List<Categoria>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetCategoriaxSC(scat);
                menu = (from DataRow dr in dt.Rows
                        select new Categoria()
                        {
                            idcat = Convert.ToInt32(dr["idcat"]),
                            nomcat = Convert.ToString(dr["cat_nom"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
       
        #endregion
        #region GetComboCategoria
        public List<Ent_Combo> GetCate()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetCate();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["idcat"]),
                            nombre = Convert.ToString(dr["cat_nom"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboSuperCAtegoria
        #region GetComboSuperCAtegoria
        public List<SCategoria> GetComboSuperCategoria()
        {
            List<SCategoria> menu = new List<SCategoria>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetSuperCategoria();
                menu = (from DataRow dr in dt.Rows
                        select new SCategoria()
                        {
                            idscat = Convert.ToInt32(dr["IDSCAT"]),
                            nomscat = Convert.ToString(dr["SCAT_NOM"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #endregion
        #region GetInsumo
        //public List<Ent_Combo> GetInsumo()
        //{
        //    List<Ent_Combo> menu = new List<Ent_Combo>;
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt = datUtil.GetInsumo();
        //        menu = (from DataRow dr in dt.Rows
        //                select new Ent_Combo()
        //                {
        //                    id = Convert.ToString(dr["idscat"]),
        //                    nombre = Convert.ToString(dr["scat_nom"]),
        //                }).ToList();
        //    } catch (Exception)
        //    {
        //        menu = null;
        //        //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
        //    }
        //    return menu;
        //}
        #endregion
        #region GetComboClasificaciónProductoItem
        public List<Ent_Combo> GetComboClasProdItem()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetClasProdItem();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["idClasificacionProductoItem"]),
                            nombre = Convert.ToString(dr["producto"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboGrupo
        public List<Grupo> GetComboGrupo()
        {
            List<Grupo> menu = new List<Grupo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetGrupo();
                menu = (from DataRow dr in dt.Rows
                        select new Grupo()
                        {
                            idgrup = Convert.ToInt32(dr["IDGRUP"]),
                            idscat = Convert.ToString(dr["IDSCAT"]),
                            nomscat = Convert.ToString(dr["SCAT_NOM"]),
                            idcat = Convert.ToString(dr["IDCAT"]),
                            nomcat = Convert.ToString(dr["CAT_NOM"]),
                            nomgrup = Convert.ToString(dr["GR_NOM"]),
                            imagengrup = (Byte[])(dr["GR_IMG"]),
                            descgrup = Convert.ToDecimal(dr["GR_DESC"])

                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboCorporacion
        public List<Ent_Combo> GetCombo_Corp()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetNomCorp();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["id"]),
                            nombre = Convert.ToString(dr["corp_nom"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboPais
        public List<Ent_Combo> GetNomPais()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetNomPais();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["idpais"]),
                            nombre = Convert.ToString(dr["nompais"]),

                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboDepartamento
        public List<Ent_Combo> GetComboDepa(string pais)
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetNomDepa(pais);
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["iddepa"]),
                            nombre = Convert.ToString(dr["nomdepa"]),
                            //idPais = Convert.ToString(dr["id"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboProvincia
        public List<Ent_Combo> getNomProv(string dpto)
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetNomProv(dpto);
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["idprov"]),
                            nombre = Convert.ToString(dr["nomprov"]),

                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboDistrito
        public List<Ent_Combo> GetNomDist(string prov)
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetNomDist(prov);
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["iddist"]),
                            nombre = Convert.ToString(dr["nomdist"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }

            return menu;
        }
        #endregion
        #region GetComboRolCargo
        public List<Ent_Combo> GetRolCargo()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetNomRolCargo();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["id"]),
                            nombre = Convert.ToString(dr["nom_cargo"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;

        }

        #endregion
        #region GetComboEmpleado
        public List<Ent_Combo> GetEmpleado()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetNomEmpleado();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["id"]),
                            nombre = Convert.ToString(dr["EMPL_NOM"]).ToUpper(),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;

        }
        #endregion
        #region ComboUsuario
        public List<Ent_Combo> GetUsuarios()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetNomUsu();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["id"]),
                            nombre = Convert.ToString(dr["usu_nom"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboAlamacen
        public List<Ent_Combo> GetCombo_Almacen()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetNomAlmacen();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["ID"]),
                            nombre = Convert.ToString(dr["ALM_NOM"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboUM
        public List<Ent_Combo> GetCombo_UM()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetNomUM();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["ID"]),
                            nombre = Convert.ToString(dr["TM_DESC"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboIdimpresoraCarta
        public List<Ent_Combo> GetComboImpre_Carta(string idcart)
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetIdimpre(idcart );
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["ID"]),
                            nombre = Convert.ToString(dr["ID_IMPRE"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboRoles
        public List<Ent_Combo> GetRoles()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetRoles();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["ID"]),
                            nombre = Convert.ToString(dr["NOM_ROL"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;

        }
        #endregion
        #region GetComboPlato
        public List<Platos> GetComboPlato(int grup)
        {
            List<Platos> menu = new List<Platos>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.Combo_Plato(grup);
                menu = (from DataRow dr in dt.Rows
                        select new Platos()
                        {
                            idplato = Convert.ToInt32(dr["IDCARTA"]),
                            nomplato = Convert.ToString(dr["CAR_NOM"]),
                            precplato = Convert.ToString(dr["CAR_PRECIO"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboTipo_insumo

        public List<Ent_Combo> GetComboTipo_Insumo()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetTipo_Insumo();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["ID"]),
                            nombre = Convert.ToString(dr["NOM_TIPO"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;

        }
        #endregion
        #region GetComboinsumo

        public List<Ent_Combo> GetComboInsumo()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetInsumo();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["ID"]),
                            nombre = Convert.ToString(dr["INS_NOM"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;

        }
        #endregion
        #region GetComboTipoMoneda

        public List<Ent_Combo> GetComboTipoMoneda()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetTipoMoneda();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["ID"]),
                            nombre = Convert.ToString(dr["TM_DESCR"]),
                            valor1 = dr["VALOR"],
                            valor2 = dr["TC_CAMBIO"]
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;

        }
        #endregion
        #region GetComboFormaPago

        public List<Ent_Combo> GetComboFormaPago(int TIP_MON)
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetFormaPago(TIP_MON);
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["ID"]),
                            nombre = Convert.ToString(dr["DENO_PAGO"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;

        }
        #endregion
        #region GetComboFormaPago2

        public List<Ent_Combo> GetComboFormaPago2()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetFormaPago2();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["ID"]),
                            nombre = Convert.ToString(dr["DENO_PAGO"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;

        }
        #endregion
        #region GetComboCategoria
        public List<Categoria> GetCategoria()
        {
            List<Categoria> menu = new List<Categoria>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetCategoria();
                menu = (from DataRow dr in dt.Rows
                        select new Categoria()
                        {
                            idcat = Convert.ToInt32(dr["IDCAT"]),
                            idscat = Convert.ToInt32(dr["IDSCAT"]),
                            nomscat = Convert.ToString(dr["CAT_NOM"]),
                            desccat = Convert.ToDecimal(dr["CAT_DESC"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetPlato
        public List<Platos> GetPlato()
        {
            List<Platos> menu = new List<Platos>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.Get_Plato();
                menu = (from DataRow dr in dt.Rows
                        select new Platos()
                        {
                            idplato = Convert.ToInt32(dr["IDCARTA"]),
                            idcat = Convert.ToString(dr["IDCAT"]),
                            idgrup = Convert.ToString(dr["IDGRUP"]),
                            nomcat = Convert.ToString(dr["CAR_NOM"]),
                            precplato = dr["CAR_PRECIO"].ToString(),
                            nivelplato = Convert.ToString(dr["CAR_NIVEL"]),
                            estadoplato = Convert.ToByte(dr["CAR_EST"]),
                            porcionplato = Convert.ToString(dr["CAR_PORCION"]),
                            idproditem = Convert.ToString(dr["idClasificacionProductoItem"]),
                            idscat = Convert.ToString(dr["IDSCAT"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboGrupo
        public List<Grupo> GetComboGrupoxC(int scat)
        {
            List<Grupo> menu = new List<Grupo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetGrupoxC(scat);
                menu = (from DataRow dr in dt.Rows
                        select new Grupo()
                        {
                            idgrup = Convert.ToInt32(dr["IDGRUP"]),
                            nomgrup = Convert.ToString(dr["GR_NOM"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetTipoDescuento
        public List<Descuento> GetTipoDescuento()
        {
            List<Descuento> descuento = new List<Descuento>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetTipoDescuento();
                descuento = (from DataRow dr in dt.Rows
                        select new Descuento()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            TD_DESCR = Convert.ToString(dr["TD_DESCR"]),
                            TD_ACT = Convert.ToByte(dr["TD_ACT"]),
                            TD_PORCENTAJE = Convert.ToDecimal(dr["TD_PORCENTAJE"])
                        }).ToList();
            }
            catch (Exception ex)
            {
                descuento = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return descuento;
        }
        #endregion
        #region GetComboInsumoPorAlmacen
        public List<Ent_Combo> GetComboAlmacenInsumo(string idalma)
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetAlmacenInsumo(idalma);
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = (dr["ID_INS"]).ToString(),
                            nombre =(dr["INS_NOM"]).ToString(),
                            valor1 =Convert.ToDecimal((dr["CANT"])),
                            //valor3 =Convert.ToDecimal(dr["INS_NOM"])
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }

        #endregion
        #region GetComboAlamacenSalida
        public List<Ent_Combo> GetComboAlmacenSalida(string idalma, string idins)
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetAlmacenSalida(idalma, idins);
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = (dr["ID_ALMA"]).ToString(),
                            nombre =(dr["ALM_NOM"]).ToString(),
                            //valor3 =Convert.ToDecimal(dr["INS_NOM"])
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }

        #endregion 
        #region GetComboNiveles
        public List<Ent_Combo> GetComboNiveles()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetNiveles();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = (dr["ID"]).ToString(),
                            nombre =(dr["N_NOM"]).ToString(),
                            ischeck = false,
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }

        #endregion 
        #region GetComboImpresoras
        public List<Ent_Combo> GetComboImpresoras()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetImpresoras();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = (dr["IDIMP"]).ToString(),
                            nombre =(dr["NOMIMP"]).ToString(),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }

        #endregion
        #region GetComboMesaOcupadas
        public List<Ent_Combo> GetComboMesaOcupadas(int idmesa)
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetMesasOcupadas(idmesa);
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = (dr["ID"]).ToString(),
                            nombre = (dr["M_NOM"]).ToString(),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }

        #endregion
        #region GetComboMesaDisponibles
        public List<Ent_Combo> GetComboMesaDisponibles(int idmesa)
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetMesasDisponibles(idmesa);
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = (dr["ID"]).ToString(),
                            nombre = (dr["M_NOM"]).ToString(),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }

        #endregion
        #region GetComboMesaDelivery
        public List<Ent_Combo> GetComboMesaDelivery()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetMesasDelivery();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = (dr["ID"]).ToString(),
                            nombre = (dr["M_NOM"]).ToString(),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }

        #endregion
        #region GetComboRecojoDelivery
        public List<Ent_Combo> GetComboRecojoDelivery()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetRecojoDelivery();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = (dr["ID"]).ToString(),
                            nombre = (dr["M_NOM"]).ToString(),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }

        #endregion
        #region ComboUsuarioLogin
        public List<Ent_Combo> GetUsuariosLogin()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetNomUsuLogin();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["id"]),
                            nombre = Convert.ToString(dr["usu_nom"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboImpresoras
        public List<Ent_Combo> GetComboImpresorasReporte()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetImpresorasReporte();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = (dr["IDIMP"]).ToString(),
                            nombre = (dr["NOMIMP"]).ToString(),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region getCajas
        public List<Ent_Combo> getCajas()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.getCajas();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["ID"]),
                            nombre = dr["NOMBRE"].ToString(),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
            }
            return menu;
        }
        #endregion
        #region ComboMotorizado
        public List<Ent_Combo> GetComboMotorizado()
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetMotorizado();
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = Convert.ToString(dr["ID"]),
                            nombre = Convert.ToString(dr["EMPL_NOM"]),
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }
        #endregion
        #region GetComboInsumoPorId
        public List<Ent_Combo> GetInsumoxId(string idins)
        {
            List<Ent_Combo> menu = new List<Ent_Combo>();
            try
            {
                DataTable dt = new DataTable();
                dt = datUtil.GetInsumoxId(idins);
                menu = (from DataRow dr in dt.Rows
                        select new Ent_Combo()
                        {
                            id = (dr["ID_INS"]).ToString(),
                            nombre = (dr["INS_NOM"]).ToString(),
                            valor1 = Convert.ToDecimal((dr["CANT"])),
                            //valor3 =Convert.ToDecimal(dr["INS_NOM"])
                        }).ToList();
            }
            catch (Exception ex)
            {
                menu = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return menu;
        }

        #endregion
    }
}

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
    public class Neg_Platos
    {
        Dat_Plato dataPlat = new Dat_Plato();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public ObservableCollection<Platos> GetPlatos()
        {
            ObservableCollection<Platos> platos = new ObservableCollection<Platos>();
            try
            {
                DataTable dt = dataPlat.GetPlatos();
                foreach (DataRow row in dt.Rows)
                {
                    Platos _plat = new Platos();
                    _plat.idplato = Convert.ToInt32(row["IDCARTA"]);
                    _plat.idcat = row["idcat"].ToString();
                    _plat.nomcat = row["cat_nom"].ToString();
                    _plat.idgrup = (row["idgrup"]).ToString();
                    _plat.nomgrup = row["gr_nom"].ToString();
                    _plat.idscat = row["idscat"].ToString();
                    _plat.nomscat = row["scat_nom"].ToString();
                    _plat.nomplato = row["CAR_NOM"].ToString();
                    _plat.precplato = row["CAR_PRECIO"].ToString();
                    _plat.id_niveles = row["CAR_NIVEL"].ToString();
                    _plat.nivelplato = (row["NOM_NIVEL"]).ToString();
                    _plat.estadoplato = Convert.ToByte(row["CAR_EST"]);
                    _plat.porcionplato = row["CAR_PORCION"].ToString();
                    _plat.idproditem = (row["idClasificacionProductoItem"]).ToString();
                    _plat.nomproditem = row["producto"].ToString();
                    _plat.idscat = row["idscat"].ToString();
                    _plat.nomscat = row["scat_nom"].ToString();
                    _plat.comentario = "";
                    _plat.estadoD = Convert.ToBoolean(row["estadoD"]);
                    _plat.RC = Convert.ToByte(row["RC"]);
                    _plat.exonerada = Convert.ToByte(row["EXONERADA"]);
                    _plat.precplato_delivery = row["CAR_PRECIO_DEL"].ToString();
                    _plat.estadoplato_delivery = Convert.ToByte(row["CAR_ESTADO_DEL"]);
                    _plat.imgplato = (byte[])row["CAR_IMG"];
                    _plat.descrip_plato = row["CAR_DESCRIP"].ToString();
                    _plat.cbarplato = row["CAR_COD_BAR"].ToString();
                    platos.Add(_plat);
                }
            }
            catch (Exception ex)
            {
                platos = null;
            }
            return platos;
        }
        public ObservableCollection<Platos> getPlatosCombo(int idgrup)
        {
            ObservableCollection<Platos> platos = new ObservableCollection<Platos>();
            try
            {
                DataTable dt = dataPlat.getPlatosCombo(idgrup);
                foreach (DataRow row in dt.Rows)
                {
                    Platos _plat = new Platos();
                    _plat.idplato = Convert.ToInt32(row["IDCARTA"]);
                    _plat.idcat = row["IDCAT"].ToString();
                    _plat.idgrup = row["IDGRUP"].ToString();
                    _plat.nomplato = row["CAR_NOM"].ToString();
                    platos.Add(_plat);
                }
            }
            catch (Exception ex)
            {
                platos = null;
            }
            return platos;
        }
        public ObservableCollection<Platos> GetPlatosCloud()
        {
            ObservableCollection<Platos> platos = new ObservableCollection<Platos>();
            try
            {
                DataTable dt = dataPlat.GetPlatosCloud();
                foreach (DataRow row in dt.Rows)
                {
                    Platos _plat = new Platos();
                    _plat.idplato = Convert.ToInt32(row["IDCARTA"]);
                    _plat.idcat = row["idcat"].ToString();
                    _plat.nomcat = row["cat_nom"].ToString();
                    _plat.idgrup = (row["idgrup"]).ToString();
                    _plat.nomgrup = row["gr_nom"].ToString();
                    _plat.idscat = row["idscat"].ToString();
                    _plat.nomscat = row["scat_nom"].ToString();
                    //_plat.idimpre = Convert.ToByte(row["ID_IMPRE"]);
                    //_plat.nombimpre = row["nomimpre"].ToString();
                    _plat.nomplato = row["CAR_NOM"].ToString();
                    _plat.precplato = row["CAR_PRECIO"].ToString();
                    _plat.id_niveles = row["CAR_NIVEL"].ToString();
                    _plat.nivelplato = (row["CAR_NIVEL"]).ToString();
                    _plat.estadoplato = Convert.ToByte(row["CAR_EST"]);
                    _plat.porcionplato = row["CAR_PORCION"].ToString();
                    _plat.idproditem = (row["idClasificacionProductoItem"]).ToString();
                    _plat.nomproditem = row["producto"].ToString();
                    _plat.idscat = row["idscat"].ToString();
                    _plat.nomscat = row["scat_nom"].ToString();
                    _plat.comentario = "";
                    _plat.estadoD = Convert.ToBoolean(row["estadoD"]);
                    _plat.RC = Convert.ToByte(row["RC"]);
                    _plat.exonerada = Convert.ToByte(row["EXONERADA"]);
                    _plat.precplato_delivery = row["CAR_PRECIO_DEL"].ToString();
                    _plat.estadoplato_delivery = Convert.ToByte(row["CAR_ESTADO_DEL"]);
                    _plat.imgplato = (byte[])row["CAR_IMG"];
                    _plat.descrip_plato = row["CAR_DESCRIP"].ToString();

                    platos.Add(_plat);
                }
            }
            catch (Exception ex)
            {
                platos = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return platos;
        }
        public ObservableCollection<Platos> GetPlatosActivos()
        {
            ObservableCollection<Platos> platos = new ObservableCollection<Platos>();
            try
            {
                DataTable dt = dataPlat.GetPlatosActivo();
                foreach (DataRow row in dt.Rows)
                {
                    Platos _plat = new Platos();
                    _plat.idplato = Convert.ToInt32(row["IDCARTA"]);
                    _plat.idcat = row["idcat"].ToString();
                    _plat.nomcat = row["cat_nom"].ToString();
                    _plat.idgrup = (row["idgrup"]).ToString();
                    _plat.nomgrup = row["gr_nom"].ToString();
                    _plat.idscat = row["idscat"].ToString();
                    _plat.nomscat = row["scat_nom"].ToString();
                    _plat.nomplato = row["CAR_NOM"].ToString();
                    _plat.precplato = row["CAR_PRECIO"].ToString();
                    _plat.nivelplato = (row["CAR_NIVEL"]).ToString();
                    _plat.estadoplato = Convert.ToByte(row["CAR_EST"]);
                    _plat.porcionplato = row["CAR_PORCION"].ToString();
                    _plat.idproditem = (row["idClasificacionProductoItem"]).ToString();
                    _plat.nomproditem = row["producto"].ToString();
                    _plat.idscat = row["idscat"].ToString();
                    _plat.nomscat = row["scat_nom"].ToString();
                    _plat.cbarplato = row["CAR_COD_BAR"].ToString();
                    _plat.comentario = "";
                    platos.Add(_plat);
                }
            }
            catch (Exception ex)
            {
                platos = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return platos;
        }
        public ObservableCollection<Platos> GetPlatosxGrupo(int idgrupo)
        {
            ObservableCollection<Platos> platos = new ObservableCollection<Platos>();
            try
            {
                DataTable dt = dataPlat.GetPlatosxGrupo(idgrupo);
                foreach (DataRow row in dt.Rows)
                {
                    Platos _plat = new Platos();
                    _plat.idplato = Convert.ToInt32(row["IDCARTA"]);
                    _plat.idcat = row["idcat"].ToString();
                    _plat.nomcat = row["cat_nom"].ToString();
                    _plat.idgrup = (row["idgrup"]).ToString();
                    _plat.nomgrup = row["gr_nom"].ToString();
                    _plat.idscat = row["idscat"].ToString();
                    _plat.nomscat = row["scat_nom"].ToString();
                    //_plat.idimpre = Convert.ToByte(row["ID_IMPRE"]);
                    //_plat.nombimpre = row["nomimpre"].ToString();
                    _plat.nomplato = row["CAR_NOM"].ToString();
                    _plat.precplato = row["CAR_PRECIO"].ToString();
                    //_plat.id_niveles = row["ID_NIVEL"].ToString();
                    _plat.nivelplato = (row["CAR_NIVEL"]).ToString();
                    _plat.estadoplato = Convert.ToByte(row["CAR_EST"]);
                    _plat.porcionplato = row["CAR_PORCION"].ToString();
                    _plat.idproditem = (row["idClasificacionProductoItem"]).ToString();
                    _plat.nomproditem = row["producto"].ToString();
                    _plat.idscat = row["idscat"].ToString();
                    _plat.nomscat = row["scat_nom"].ToString();
                    _plat.cbarplato = row["CAR_COD_BAR"].ToString();
                    _plat.comentario = "";
                    platos.Add(_plat);
                }
            }
            catch (Exception ex)
            {
                platos = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return platos;
        }
        public int SetPlatos(int operacion, Platos platos, ref string _mensaje)
        {
            int result = 0;
            result = dataPlat.SetPlato(operacion, platos, ref _mensaje);
            if (result != 0)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public decimal GetDescPlatoGrupo(int idcarta)
        {
            decimal desc = 0;
            DataTable dt = dataPlat.getDescPlatoGrupo(idcarta);
            desc = Convert.ToDecimal(dt.Rows[0][0]);
            return desc;
        }

        public List<Impresora> getImpresoraPlato(string id, ref string _mensaje)
        {
            List<Impresora> impresoras = null;
            try
            {
                impresoras = new List<Impresora>();
                DataTable dt = dataPlat.getImpresoraPlato(id);
                foreach (DataRow item in dt.Rows)
                {
                    Impresora _imp = new Impresora();
                    _imp.idimp = Convert.ToInt32(item["IDIMP"]);
                    //_imp.ipimp = item["IPIMP"].ToString();
                    _imp.nomimp = item["NOMIMP"].ToString();
                    _imp.estadoimp = item["ESTADOIMP"].ToString();
                    _imp.nomequipoimp = item["NOMEQUIPOIMP"].ToString();
                    impresoras.Add(_imp);
                }
            }
            catch (Exception ex)
            {
                _mensaje = ex.Message;
                impresoras = null;
            }
            return impresoras;
        }
        public ObservableCollection<Documentos> GetImpresoraxDocumento_2(int ID_DOCUMENTO)
        {
            ObservableCollection<Documentos> impresoras = new ObservableCollection<Documentos>();
            try
            {
                DataTable dt = dataPlat.Get_ImpresoraxDocumento(ID_DOCUMENTO);
                foreach (DataRow row in dt.Rows)
                {
                    Documentos _imp = new Documentos();
                    _imp.ID = Convert.ToInt32(row["ID"]);
                    _imp.DI_ID_DOCUMENTO = Convert.ToInt32(row["DI_ID_DOCUMENTO"]);
                    _imp.DOC_NOM = row["DOC_NOM"].ToString();
                    _imp.DI_ID_IMP = Convert.ToInt32(row["DI_ID_IMP"]);
                    _imp.NOMIMP = row["NOMIMP"].ToString();
                    //_imp.DI_ACT = Convert.ToBoolean(row["DI_ACT"]);
                    impresoras.Add(_imp);
                }
            }
            catch (Exception ex)
            {
                impresoras = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return impresoras;
        }
        public DataTable GetImpresoraxDocumento(int ID_DOCUMENTO)
        {
            DataTable impresoras;
            try
            {
                DataTable dt = dataPlat.Get_ImpresoraxDocumento(ID_DOCUMENTO);
                impresoras = dt;
            }
            catch (Exception ex)
            {
                impresoras = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return impresoras;
        }
        public DataTable GetPedidoPendienteImprimir()
        {
            DataTable impresoras;
            try
            {
                DataTable dt = dataPlat.GetPedidoPendienteImprimir();
                impresoras = dt;
            }
            catch (Exception ex)
            {
                impresoras = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return impresoras;
        }
        public DataTable GetCuentaPendienteImprimir()
        {
            DataTable impresoras;
            try
            {
                DataTable dt = dataPlat.GetCuentaPendienteImprimir();
                impresoras = dt;
            }
            catch (Exception ex)
            {
                impresoras = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return impresoras;
        }
        public ObservableCollection<Documentos> GetDocumentos()
        {
            ObservableCollection<Documentos> documentos = new ObservableCollection<Documentos>();
            try
            {
                DataTable dt = dataPlat.Get_Documentos();
                foreach (DataRow row in dt.Rows)
                {
                    Documentos _imp = new Documentos();
                    _imp.ID = Convert.ToInt32(row["ID"]);
                    _imp.DOC_NOM = row["DOC_NOM"].ToString();
                    _imp.DOC_ACT = Convert.ToBoolean(row["DOC_ACT"]);
                    documentos.Add(_imp);
                }
            }
            catch (Exception ex)
            {
                documentos = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return documentos;
        }
        public int SetDocumentos(int operacion, Documentos documentos)
        {
            int result = 0;
            result = dataPlat.SetDocumentos(operacion, documentos);
            return result;
        }
        public int sp_cloud_update()
        {
            int result = 0;
            result = dataPlat.sp_cloud_update();
            return result;
        }

        public ObservableCollection<Platos> GetPlatosMasVendidos()
        {
            ObservableCollection<Platos> platos = new ObservableCollection<Platos>();
            try
            {
                DataTable dt = dataPlat.GetPlatosMasVendidos();
                foreach (DataRow row in dt.Rows)
                {
                    Platos _plat = new Platos();
                    _plat.idplato = Convert.ToInt32(row["IDCARTA"]);
                    _plat.idcat = row["idcat"].ToString();
                    _plat.nomcat = row["cat_nom"].ToString();
                    _plat.idgrup = (row["idgrup"]).ToString();
                    _plat.nomgrup = row["gr_nom"].ToString();
                    _plat.idscat = row["idscat"].ToString();
                    _plat.nomscat = row["scat_nom"].ToString();
                    //_plat.idimpre = Convert.ToByte(row["ID_IMPRE"]);
                    //_plat.nombimpre = row["nomimpre"].ToString();
                    _plat.nomplato = row["CAR_NOM"].ToString();
                    _plat.precplato = row["CAR_PRECIO"].ToString();
                    //_plat.id_niveles = row["ID_NIVEL"].ToString();
                    _plat.nivelplato = (row["CAR_NIVEL"]).ToString();
                    _plat.estadoplato = Convert.ToByte(row["CAR_EST"]);
                    _plat.porcionplato = row["CAR_PORCION"].ToString();
                    _plat.idproditem = (row["idClasificacionProductoItem"]).ToString();
                    _plat.nomproditem = row["producto"].ToString();
                    _plat.idscat = row["idscat"].ToString();
                    _plat.nomscat = row["scat_nom"].ToString();
                    _plat.comentario = "";
                    platos.Add(_plat);
                }
            }
            catch (Exception ex)
            {
                platos = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }

            return platos;
        }
    }
}

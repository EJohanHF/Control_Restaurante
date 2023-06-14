using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Centro_Costos;
using Capa_Entidades.Models.Factura_Compra;
using Capa_Entidades.Models.Pedido;
using Capa_Entidades.Models.Receta;
using Capa_Negocio;
using Capa_Negocio.Carta;
using Capa_Negocio.Centro_Costos;
using Capa_Negocio.Factura_Compra;
using Capa_Negocio.Pedido;
using Capa_Negocio.Receta;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Centro_Costos
{
    public class CentroCostosStructure
    {
        #region Negocio
        Neg_CentroCostos negCostos = new Neg_CentroCostos();
        Neg_FactCompra negFactCompra = new Neg_FactCompra();
        Neg_Pedido negPedido = new Neg_Pedido();
        Neg_Receta negReceta = new Neg_Receta();
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Platos negPlatos = new Neg_Platos();

        #endregion

        #region Entidad
        static ObservableCollection<CentroCostos> ccostos = new ObservableCollection<CentroCostos>();
        static ObservableCollection<TiposCostos> tcostos = new ObservableCollection<TiposCostos>();
        static ObservableCollection<FactCompra> fcompra = new ObservableCollection<FactCompra>();
        static ObservableCollection<Pedidos> npedido = new ObservableCollection<Pedidos>();
        static ObservableCollection<Pedidos> ndetallepedido = new ObservableCollection<Pedidos>();
        static ObservableCollection<Recetas> nreceta = new ObservableCollection<Recetas>();

        static ObservableCollection<Tiempo> año = new ObservableCollection<Tiempo>();
       

        static List<SCategoria> scat = new List<SCategoria>();
        static List<Categoria> cat = new List<Categoria>();
        static List<Grupo> gru = new List<Grupo>();
        static List<Platos> pla = new List<Platos>();
        #endregion

        public CentroCostosStructure() 
        {
            try
            {
                ccostos = negCostos.GetCentroCostos();
                tcostos = negCostos.GetTiposCostos();
                fcompra = negFactCompra.GetFacturaCompra();
                npedido = negPedido.GET_PEDIDO();
                ndetallepedido = negPedido.GET_DETALLEPEDIDO();
                nreceta = negReceta.GetReceta();

                scat = negCombo.GetComboSuperCategoria();
                cat = negCombo.GetCategoria();
                gru = negCombo.GetComboGrupo();
                pla = negCombo.GetPlato();
            }
            catch (Exception)
            {

            }            
        }

        public List<CentroCostos> GetDataCostosFijos(int op) 
        {
            if (op == 1) { ccostos = negCostos.GetCentroCostos(); }
            var data = ccostos.Where(w => w.CATEGORIA == 1).ToList();
            return data;
        }
        public List<CentroCostos> GetDataCostosVariables(int op) 
        {
            if (op == 1) { ccostos = negCostos.GetCentroCostos(); }
            var data = ccostos.Where(w => w.CATEGORIA == 2).ToList();
            return data;
        }
        public List<CentroCostos> GetDataCostosFijosxFecha(string Mes,string Año)
        {
            var data = new List<CentroCostos>();
            if (Año == "0")
            {
                 data = ccostos.Where(w => w.CATEGORIA == 1 && w.CC_MES == Mes).ToList();
            }
            else 
            {
                 data = ccostos.Where(w => w.CATEGORIA == 1 && w.CC_MES == Mes && w.CC_AÑO == Año).ToList();
            }
            
            return data;
        }
        public List<CentroCostos> getDataMeses(string año)
        {
            return ccostos.GroupBy(c => c.CC_MES)
                   .Select(grp => grp.First()).Where(w => w.CATEGORIA == 1 && w.CC_AÑO == año)
                   .ToList();
        }
        public List<Pedidos> getDataMesesxPedidos(int año)
        {
            return npedido.GroupBy(c => c.DP_FEC_REG.Month)
                   .Select(grp => grp.First()).Where(w => w.DP_EST == "1" && w.DP_FEC_REG.Year == año)
                   .ToList();
        }
        public List<Pedidos> getDataAñosxPedidos()
        {
            return npedido.GroupBy(c => c.PED_FECH_PED)
                   .Select(grp => grp.First()).Where(w => w.PED_ID_ESTADO == 1)
                   .ToList();
            
            //foreach (var y in h) 
            //{
            //    Tiempo t = new Tiempo();
            //    t.DESCRPCION = y.DP_FEC_REG.Year.ToString();
            //    año.Add(t);
            //}
            //return año.ToList();
        }
        public List<TiposCostos> getDataTiposCostosFijos()
        {
            return tcostos.Where(w => w.TP_TIPO == 1).ToList();
        }
        public List<TiposCostos> getDataTiposCostosVariables()
        {
            return tcostos.Where(w => w.TP_TIPO == 2).ToList();
        }
        public List<CentroCostos> getDataAños()
        {
            return ccostos.GroupBy(c => c.CC_AÑO)
                   .Select(grp => grp.First()).Where(w => w.CATEGORIA == 1)
                   .ToList();
        }
        public List<CentroCostos> GetDataReporteTiposCostos()
        {
            List<CentroCostos> result = ccostos
                    .GroupBy(l => l.CC_TIPO)
                    .Select(cl => new CentroCostos
                    {
                        CC_TIPO = cl.First().CC_TIPO,
                        TP_DENOMINACION = cl.First().TP_DENOMINACION,
                        Cantidad = cl.Count().ToString(),
                        CC_MONTO = cl.Sum(c => c.CC_MONTO),
                    }).Where(w => w.CC_TIPO == 1 || w.CC_TIPO == 2).ToList();

            return result;

        }


        public decimal GetMP(int num_mes, int año) //MATERIA PRIMA
        {
            var _m = fcompra.Where(w => w.FC_ESTADO_CARGA == true && w.FC_F_EMISION.Month == num_mes && w.FC_F_EMISION.Year == año).Sum(s => s.FC_IMPORTE_TOTAL_COMPRA);
            return _m;
        }
        public decimal GetMO(string nom_mes, int año) //MANO DE OBRA
        {
            var _m = ccostos.Where(w => w.CC_TIPO == 2 && w.CC_MES == nom_mes.ToUpper() && w.CC_AÑO == año.ToString()).Sum(s => s.CC_MONTO);
            return _m;
        }
        public decimal GetGIF(string nom_mes, int año) //MANO DE OBRA
        {
            var _m = ccostos.Where(w => w.CC_TIPO == 14 && w.CC_MES == nom_mes.ToUpper() && w.CC_AÑO == año.ToString()).Sum(s => s.CC_MONTO);
            return _m;
        }
       
        public decimal GetMPI(string nom_mes, int año) //MATERIA PRIMA INDIRECTA
        {
            var _m = ccostos.Where(w => w.CC_TIPO == 22 && w.CC_MES == nom_mes.ToUpper() && w.CC_AÑO == año.ToString()).Sum(s => s.CC_MONTO);
            return _m;
        }
        public decimal GetMOI(string nom_mes, int año) 
        {
            var _m = ccostos.Where(w => w.CC_TIPO == 23 && w.CC_MES == nom_mes.ToUpper() && w.CC_AÑO == año.ToString()).Sum(s => s.CC_MONTO);
            return _m;
        }
        
        public decimal GetSEGURO(string nom_mes, int año) 
        {
            var _m = ccostos.Where(w => w.CC_TIPO == 24 && w.CC_MES == nom_mes.ToUpper() && w.CC_AÑO == año.ToString()).Sum(s => s.CC_MONTO);
            return _m;
        }
        public decimal GetENERGIA(string nom_mes, int año) 
        {
            var _m = ccostos.Where(w => w.CC_TIPO == 15 && w.CC_MES == nom_mes.ToUpper() && w.CC_AÑO == año.ToString()).Sum(s => s.CC_MONTO);
            return _m;
        }
        public decimal GetDEPÑREE(string nom_mes, int año) 
        {
            var _m = ccostos.Where(w => w.CC_TIPO == 25 && w.CC_MES == nom_mes.ToUpper() && w.CC_AÑO == año.ToString()).Sum(s => s.CC_MONTO);
            return _m;
        }
        public decimal GetDEPREL(string nom_mes, int año) 
        {
            var _m = ccostos.Where(w => w.CC_TIPO == 26 && w.CC_MES == nom_mes.ToUpper() && w.CC_AÑO == año.ToString()).Sum(s => s.CC_MONTO);
            return _m;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        public decimal GetSueldoAdm(string nom_mes, int año)
        {
            var _m = ccostos.Where(w => w.CC_TIPO == 27 && w.CC_MES == nom_mes.ToUpper() && w.CC_AÑO == año.ToString()).Sum(s => s.CC_MONTO);
            return _m;
        }
        public decimal GetSueldoComi(string nom_mes, int año)
        {
            var _m = ccostos.Where(w => w.CC_TIPO == 31 && w.CC_MES == nom_mes.ToUpper() && w.CC_AÑO == año.ToString()).Sum(s => s.CC_MONTO);
            return _m;
        }
        public decimal GetPUBVen(string nom_mes, int año)
        {
            var _m = ccostos.Where(w => w.CC_TIPO == 32 && w.CC_MES == nom_mes.ToUpper() && w.CC_AÑO == año.ToString()).Sum(s => s.CC_MONTO);
            return _m;
        }

        public decimal GetCantidad(int num_mes, int año)
        {
            var _m = ndetallepedido.Where(w => w.DP_EST == "1" && w.DP_FEC_REG.Month == num_mes && w.DP_FEC_REG.Year == año).Sum(s => s.DP_CANT);
            return _m;
        }

        //RPT
        public List<Categoria> GetCategoria(int idscat)
        {
            return cat.Where(w => w.idscat == idscat).ToList();
        }
        public List<SCategoria> GetSuperCategoria()
        {
            return scat.ToList();
        }
        public decimal GetCantidadUniProd(int id,int op,CentroCostos cc)
        {
            decimal cant = 0;
            if (op == 1) 
            {
                cant = ndetallepedido.Where(w => w.DP_ID_CARTA == id && w.DP_EST == "1" && w.DP_FEC_REG.Date == cc.Dia.Date).Sum(s => s.DP_CANT);
            }
            else if (op == 2)
            {
                cant = ndetallepedido.Where(w => w.DP_ID_CARTA == id && w.DP_EST == "1" && w.DP_FEC_REG.Month == cc.Mes && w.DP_FEC_REG.Year == cc.Año).Sum(s => s.DP_CANT);
            }
            else if (op == 3)
            {
                cant = ndetallepedido.Where(w => w.DP_ID_CARTA == id && w.DP_EST == "1" && w.DP_FEC_REG >= cc.Desde && w.DP_FEC_REG <= cc.Hasta).Sum(s => s.DP_CANT);
            }
            return cant;
        }

        public List<TiposCostos> GetDataTiposCostos(int op)
        {
            if (op == 1) { tcostos = negCostos.GetTiposCostos(); }
            var h = tcostos.ToList();
            return h;
        }
        public List<TiposCostos> GetCategoriaTiposCostos()
        {
            return tcostos.GroupBy(c => c.CAT_DESCR)
                   .Select(grp => grp.First()).ToList();
        }
        public List<TiposCostos> GetDataClaseTipoCosto()
        {
            return tcostos.GroupBy(c => c.TP_CLASE_NOM)
                   .Select(grp => grp.First()).ToList();
        }

        public decimal GetCostosFijosxMes(int op, CentroCostos cc)
        {
            return ccostos.Where(w => w.CATEGORIA == 1 && w.CC_F_REG.Month == cc.Mes && w.CC_F_REG.Year == cc.Año && (w.CC_TIPO == 2 || w.CC_TIPO == 15)).Sum(s => s.CC_MONTO);
        }
        public List<CentroCostos> GetCostosFijosxMesLista(int op, CentroCostos cc)
        {
            return ccostos.Where(w => w.CATEGORIA == 1 && w.CC_F_REG.Month == cc.Mes && w.CC_F_REG.Year == cc.Año && (w.CC_TIPO == 2 || w.CC_TIPO == 15)).ToList();
        }
        public decimal GetCostoReceta(int idcarta)
        {
            return nreceta.Where(w => w.RE_ID_CARTA == idcarta).Sum(s => s.RE_COSTO_RECETA);
        }
        public List<Recetas> GetRecetaxPlato(int idcarta)
        {
            return nreceta.Where(w => w.RE_ID_CARTA == idcarta).ToList();
        }
    }
}

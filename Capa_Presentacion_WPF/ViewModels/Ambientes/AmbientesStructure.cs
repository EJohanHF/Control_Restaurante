using Capa_Datos;
using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Parametros;
using Capa_Entidades.Models.Pedido;
using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Entidades.Models.Sostic;
using Capa_Negocio.Ambientes;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Parametros;
using Capa_Negocio.Pedido;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Negocio.Sostic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion_WPF.ViewModels.Ambientes
{
    public class AmbientesStructure
    {
        Neg_Ambientes neg = new Neg_Ambientes();
        Neg_Mesa negM = new Neg_Mesa();
        Neg_Empresa negEmpr = new Neg_Empresa();
        Neg_SosTic negSosTic = new Neg_SosTic();
        Neg_CierresDias neg_cierres = new Neg_CierresDias();
        Neg_Parametros neg_par = new Neg_Parametros();
        Neg_Pedido neg_pedido = new Neg_Pedido();
        static List<AmbientesItem> ambientesItem = new List<AmbientesItem>();
        static List<MesasItem> mesasItem = new List<MesasItem>();
        static List<MesasItem> mesasItemDelivery = new List<MesasItem>();
        static ObservableCollection<Empresa> empresa = new ObservableCollection<Empresa>();
        static List<Sos_Tic> sostic = new List<Sos_Tic>();
        static ObservableCollection<Ent_CierresDias> cierres = new ObservableCollection<Ent_CierresDias>();
        static ObservableCollection<Parametros> parametros = new ObservableCollection<Parametros>();
        static ObservableCollection<Pedidos> pedidos = new ObservableCollection<Pedidos>();
        public AmbientesStructure()
        {
            try
            {
                string Modulo = ConfigurationManager.AppSettings["caja2"].ToString();
                int caja = 1;
                if (Modulo == "SI") { caja = 2; } else if (Modulo == "NO") { caja = 1; }
                ambientesItem = neg.GetAmbientesActivo(caja.ToString());
                mesasItem = negM.GetMesasActiva();
                mesasItemDelivery = negM.GetMesasActivaDelivery();
                empresa = negEmpr.GetEmpresa();
                sostic = negSosTic.GetSostic();
                cierres = neg_cierres.GET_CIERRES();
                parametros = neg_par.GetParametros();
                pedidos = neg_pedido.GET_PEDIDO();
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje(" ", ex.Message, 3);
            }
        }
        MEnsaje mensaje = new MEnsaje();
        public List<MesasItem> GetMesas()
        {
            try
            {
                mesasItem = negM.GetMesasActiva();
                return mesasItem.ToList();
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("", ex.Message, 3);
                return null;
            }
        }
        public List<AmbientesItem> GetLogicalDrives()
        {
            try
            {
                string Modulo = ConfigurationManager.AppSettings["caja2"].ToString();
                int caja = 1;
                if (Modulo == "SI") { caja = 2; } else if (Modulo == "NO") { caja = 1; }
                ambientesItem = neg.GetAmbientesActivo(caja.ToString());
                return ambientesItem.Where(w => w.A_ACT == true).ToList();
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public List<AmbientesItem> GetLogicalDrivesDelivery()
        {
            try
            {
                string Modulo = ConfigurationManager.AppSettings["caja2"].ToString();
                int caja = 1;
                if (Modulo == "SI") { caja = 2; } else if (Modulo == "NO") { caja = 1; }
                ambientesItem = neg.GetAmbientesActivoDelivery(caja.ToString());
                return ambientesItem.Where(w => w.A_ACT == true).ToList();
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public List<AmbientesItem> GetAmbientes()
        {
            try
            {
                ambientesItem = neg.GetAmbientes();
                return ambientesItem.ToList();
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        
        public List<MesasItem> GetLogicalDrivesMesas(object id)
        {
            try
            {
                int _id = Convert.ToInt32(id);
                return mesasItem.Where(w => w.M_ID_AMB == _id && w.M_ACT == true && w.M_ID_PADRE == 0).ToList();
            }
            catch (Exception ex)
            {
             
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public List<MesasItem> GetLogicalDrivesMesasDelivery(object id)
        {
            try
            {
                int _id = Convert.ToInt32(id);
                return mesasItemDelivery.Where(w => w.M_ID_AMB == _id && w.M_ACT == true && w.M_ID_PADRE == 0).ToList();
            }
            catch (Exception ex)
            {

                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public List<MesasItem> ObtenerSubMesa(object id)
        {
                        try
                        {
                            int id_padre = Convert.ToInt32(id);
            return mesasItem.Where(w => w.M_ID_PADRE == id_padre && w.M_TIPO == 2 && w.M_ACT == true).ToList();
            }
            catch (Exception ex)
            {
               
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public int GetPedidoIsNull()
        {
            try
            {
                int i = 0;
                //int dia_limite = Convert.ToInt32(parametros.Where(w => w.NOM_PAR == "DiaLimite_CierreCaja").FirstOrDefault().VALOR_PAR);
                //var hora_limite = parametros.Where(w => w.NOM_PAR == "HoraLimite_CierreCaja").FirstOrDefault().VALOR_PAR;
                int c = pedidos.Where(w => w.PED_ID_CIERRE == 0).Count();//contar si hay pedidos sin codigo de cierre
                if (c > 0)
                {
                    DateTime fecha = pedidos.Where(w=> w.PED_ID_CIERRE == 0).OrderBy(o=> o.ID).LastOrDefault().PED_FECH_PED;
                    DateTime fecha_actual = DateTime.Now;

                    i = Convert.ToInt32((DateTime.Now - fecha).TotalHours);
                }
            
                return i;
            }
            catch (Exception ex)
            {
                
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return 0;
            }
        }
        public String NombreMesaPadre(object id)
        {
                                try
                                {
                                    int id_padre = Convert.ToInt32(id);
            var nom = mesasItem.Where(w => w.M_ID_PADRE == id_padre && w.M_TIPO == 2).First();
            String nombrePadre = nom.NOMBRE_PADRE;
            return nombrePadre;
            }
            catch (Exception ex)
            {
                
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public String NombreMesa(object id)
        {
                                    try
                                    {
                                        int id_mesa = Convert.ToInt32(id);
            var nom = mesasItem.Where(w => w.ID == id_mesa && w.M_TIPO == 1).First(); //
            String nombreMesa = nom.M_NOM;
            return nombreMesa;
            }
            catch (Exception ex)
            {
               
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public String NombreSubMesa(object id)
        {
                                        try
                                        {
                                            int id_mesa = Convert.ToInt32(id);
            var nom = mesasItem.Where(w => w.ID == id_mesa && w.M_TIPO == 2).First();
            String nombreSubMesa = nom.M_NOM;
            return nombreSubMesa;
            }
            catch (Exception ex)
            {
               
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }

        public String GetLogicalDrivesNomAmb(object id)
        {
                                            try
                                            {
                                                int _id = Convert.ToInt32(id);
            var ambi = ambientesItem.Where(w => w.ID == _id).First();
            String _nom = ambi.A_NOM;
            return _nom;
            }
            catch (Exception ex)
            {

                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }

        public Byte[] ObtenerLogoEmpresa(object idEmpresa)
        {
            try
            {
                int _idEmpresa = Convert.ToInt32(idEmpresa);
                var logo_ = empresa.Where(w => w.EMPR_DEFAULT == "1").First();
                Byte[] logo = logo_.empr_logo;
                return logo;
            }
            catch (Exception ex)
            {

//                mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public String ObtenerNombreEmpresa(object idEmpresa)
        {
                                                    try
                                                    {
                                                        int _idEmpresa = Convert.ToInt32(idEmpresa);
            var nomEmpresa_ = empresa.Where(w => w.EMPR_DEFAULT == "1").First();
            String nomEmpresa = nomEmpresa_.empr_nom_com;
            return nomEmpresa;
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public String ObtenerToken()
        {
                                                        try
                                                        {
                                                            var TokenEmpresa_ = empresa.Where(w => w.EMPR_DEFAULT == "1").First();
            String TokenEmpresa = TokenEmpresa_.token;
            return TokenEmpresa;
            }
            catch (Exception ex)
            {
               // mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public String ObtenerCodigo()
        {
                                                            try
                                                            {
                                                                var CodigoEmpresa_ = empresa.Where(w => w.EMPR_DEFAULT == "1").First();
            String CodigoEmpresa = CodigoEmpresa_.codigo;
            return CodigoEmpresa;
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public Byte[] ObtenerLogoSosFood()
        {
            try
            {
                var logo_ = sostic.First();
                Byte[] logo = logo_.LOGO_SOSFOOD;
                return logo;
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public Byte[] ObtenerLogoSosTic()
        {
            try
            {
                var logo_ = sostic.First();
                Byte[] logo = logo_.LOGO_SOSTIC;
                return logo;
            }
            catch (Exception ex)
            {
                // mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public string ObtenerTelefono1()
        {
            try
            {
                var telefono1_ = sostic.First();
                string telefono1 = telefono1_.TELEFONO1;
                return telefono1;
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public string ObtenerTelefono2()
        {
            try
            {
                var telefono2_ = sostic.First();
                string telefono2 = telefono2_.TELEFONO2;
                return telefono2;
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public string ObtenerCorreo()
        {
            try
            {
                var correo1_ = sostic.First();
                string correo1 = correo1_.CORREO1;
                return correo1;
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return null;
            }
        }
        public static List<AmbientesItem> GetDirectoryContents(int id)
        {
            var items = new List<AmbientesItem>();
            try
            {
                items = ambientesItem.ToList();
            }
            catch { }
            return items;
        }
        IEnumerable<string> strings = new List<string>();
        public int MesaAtendida(int id)
        {

            try
            {
                var mesa_ = mesasItem.Where(w => w.M_ATENDIDA == 1 && w.ID == id).First();
                int mesa = mesa_.ID;
                return mesa;
            }
            catch (Exception ex)
            {
                //mensaje.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                return 0;
            }
        }
    }
}

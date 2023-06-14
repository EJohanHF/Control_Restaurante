using Capa_Datos.Ambientes;
using Capa_Datos.Configuracion;
using Capa_Entidades.Models.Ambientes;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Ambientes
{
    public class Neg_Mesa
    {
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Dat_Mesa datMesas = new Dat_Mesa();
        public List<MesasItem> GetMesas()
        {
            List<MesasItem> mesas = new List<MesasItem>();
            try
            {
                DataTable dt = new DataTable();
                dt = datMesas.GetMesa();
                mesas = (from DataRow dr in dt.Rows
                         select new MesasItem()
                         {
                             ID = Convert.ToInt32(dr["ID"]),
                             M_NOM = Convert.ToString(dr["M_NOM"]),
                             M_EST = Convert.ToInt32(dr["M_EST"]),
                             M_ID_AMB = Convert.ToInt32(dr["M_ID_AMB"]),
                             A_NOM = dr["A_NOM"].ToString(),
                             M_X = Convert.ToString(dr["M_X"]),
                             M_ATENDIDA = Convert.ToInt32(dr["M_ATENDIDA"]),
                             M_WIDTH = Convert.ToInt32(dr["M_WIDTH"]),
                             M_HEIGHT = Convert.ToInt32(dr["M_HEIGHT"]),
                             M_TEXTO = Convert.ToString(dr["M_TEXTO"]),
                             M_TIPO = Convert.ToInt32(dr["M_TIPO"]),
                             M_ACT = Convert.ToBoolean(dr["M_ACT"]),
                             M_F_CREATE = Convert.ToDateTime(dr["M_F_CREATE"]),
                             M_F_MODIFICACION = Convert.ToDateTime(dr["M_F_MODIFICACION"]),
                             M_ID_PADRE = Convert.ToInt32(dr["ID_PADRE"]),
                             NOMBRE_PADRE = Convert.ToString(dr["NOMBRE_PADRE"]),
                             M_NOMBRE_TIPO = dr["M_NOMBRE_TIPO"].ToString(),
                             ESTADO_MESA = dr["ESTADO_MESA"].ToString(),
                             NroColumnas = Convert.ToInt32(dr["A_NRO_COLUMNA"]),
                             PED_ID_CLIENTE = Convert.ToInt32(dr["PED_ID_CLIENTE"]),
                             C_NOMINA = dr["C_NOMINA"].ToString(),
                             nomllevar = dr["nomllevar"].ToString(),
                             EMPL_NOM = dr["EMPL_NOM"].ToString(),
                             mesa = Convert.ToInt32(dr["mesa"])
                         }).ToList();
            }
            catch (Exception ex)
            {
                mesas = null;
               // negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return mesas;
        }
        public DataTable GetMesaDisponible(string nombre_mesa)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datMesas.GetMesaDisponible(nombre_mesa);
            }
            catch (Exception ex)
            {
                dt = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;
        }
        public DataTable GetMesa()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = datMesas.GetMesa();
            }
            catch (Exception ex)
            {
                dt = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return dt;
        }
        public List<MesasItem> GetMesasActiva()
        {
            List<MesasItem> mesas = new List<MesasItem>();
            try
            {
                DataTable dt = new DataTable();
                dt = datMesas.GetMesasActiva();
                mesas = (from DataRow dr in dt.Rows
                         select new MesasItem()
                         {
                             ID = Convert.ToInt32(dr["ID"]),
                             M_NOM = Convert.ToString(dr["M_NOM"]),
                             M_EST = Convert.ToInt32(dr["M_EST"]),
                             M_ID_AMB = Convert.ToInt32(dr["M_ID_AMB"]),
                             A_NOM = dr["A_NOM"].ToString(),
                             M_X = Convert.ToString(dr["M_X"]),
                             M_ATENDIDA = Convert.ToInt32(dr["M_ATENDIDA"]),
                             M_WIDTH = Convert.ToInt32(dr["M_WIDTH"]),
                             M_HEIGHT = Convert.ToInt32(dr["M_HEIGHT"]),
                             M_TEXTO = Convert.ToString(dr["M_TEXTO"]),
                             M_TIPO = Convert.ToInt32(dr["M_TIPO"]),
                             M_ACT = Convert.ToBoolean(dr["M_ACT"]),
                             M_F_CREATE = Convert.ToDateTime(dr["M_F_CREATE"]),
                             M_F_MODIFICACION = Convert.ToDateTime(dr["M_F_MODIFICACION"]),
                             M_ID_PADRE = Convert.ToInt32(dr["ID_PADRE"]),
                             NOMBRE_PADRE = Convert.ToString(dr["NOMBRE_PADRE"]),
                             M_NOMBRE_TIPO = dr["M_NOMBRE_TIPO"].ToString(),
                             ESTADO_MESA = dr["ESTADO_MESA"].ToString(),
                             NroColumnas = Convert.ToInt32(dr["A_NRO_COLUMNA"]),
                             PED_ID_CLIENTE = Convert.ToInt32(dr["PED_ID_CLIENTE"]),
                             C_NOMINA = dr["C_NOMINA"].ToString(),
                             nomllevar = dr["nomllevar"].ToString(),
                             EMPL_NOM = dr["EMPL_NOM"].ToString(),
                             mesa = Convert.ToInt32(dr["mesa"])
                         }).ToList();
            }
            catch (Exception ex)
            {
                mesas = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return mesas;
        }
        public List<MesasItem> GetMesasActivaDelivery()
        {
            List<MesasItem> mesas = new List<MesasItem>();
            try
            {
                DataTable dt = new DataTable();
                dt = datMesas.GetMesasActivaDelivery();
                mesas = (from DataRow dr in dt.Rows
                         select new MesasItem()
                         {
                             ID = Convert.ToInt32(dr["ID"]),
                             M_NOM = Convert.ToString(dr["M_NOM"]),
                             M_EST = Convert.ToInt32(dr["M_EST"]),
                             M_ID_AMB = Convert.ToInt32(dr["M_ID_AMB"]),
                             A_NOM = dr["A_NOM"].ToString(),
                             M_X = Convert.ToString(dr["M_X"]),
                             M_ATENDIDA = Convert.ToInt32(dr["M_ATENDIDA"]),
                             M_WIDTH = Convert.ToInt32(dr["M_WIDTH"]),
                             M_HEIGHT = Convert.ToInt32(dr["M_HEIGHT"]),
                             M_TEXTO = Convert.ToString(dr["M_TEXTO"]),
                             M_TIPO = Convert.ToInt32(dr["M_TIPO"]),
                             M_ACT = Convert.ToBoolean(dr["M_ACT"]),
                             M_F_CREATE = Convert.ToDateTime(dr["M_F_CREATE"]),
                             M_F_MODIFICACION = Convert.ToDateTime(dr["M_F_MODIFICACION"]),
                             M_ID_PADRE = Convert.ToInt32(dr["ID_PADRE"]),
                             NOMBRE_PADRE = Convert.ToString(dr["NOMBRE_PADRE"]),
                             M_NOMBRE_TIPO = dr["M_NOMBRE_TIPO"].ToString(),
                             ESTADO_MESA = dr["ESTADO_MESA"].ToString(),
                             NroColumnas = Convert.ToInt32(dr["A_NRO_COLUMNA"]),
                             PED_ID_CLIENTE = Convert.ToInt32(dr["PED_ID_CLIENTE"]),
                             C_NOMINA = dr["C_NOMINA"].ToString(),
                             nomllevar = dr["nomllevar"].ToString(),
                             EMPL_NOM = dr["EMPL_NOM"].ToString(),
                             mesa = Convert.ToInt32(dr["mesa"])
                         }).ToList();
            }
            catch (Exception ex)
            {
                mesas = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return mesas;
        }
        public bool SetMesas(int operacion, MesasItem mes, ref string _mensaje)
        {
            bool result = false;
            result = datMesas.SetMesas(operacion, mes, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public List<MesasItem> GetTipoMesas()
        {
            List<MesasItem> mesas = new List<MesasItem>();
            try
            {
                DataTable dt = new DataTable();
                dt = datMesas.GettipoMesas();
                mesas = (from DataRow dr in dt.Rows
                         select new MesasItem()
                         {
                             ID_TM = Convert.ToInt32(dr["ID"]),
                             TM_DESCR = Convert.ToString(dr["TM_DESCR"]),
                             TM_ACT = Convert.ToBoolean(dr["TM_ACT"]),
                             TM_F_CREATE = Convert.ToDateTime(dr["TM_F_CREATE"]),
                             TM_F_MODIFICACION = Convert.ToDateTime(dr["TM_F_MODIFICACION"])
                         }).ToList();
            }
            catch (Exception ex)
            {
                mesas = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return mesas;
        }

        public bool SetTamañoAmbiente(MesasItem mes, ref string _mensaje)
        {
            bool result = false;
            result = datMesas.SetTamañoAmbiente(mes, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
    }
}

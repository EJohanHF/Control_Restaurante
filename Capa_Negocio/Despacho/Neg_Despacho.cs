using Capa_Datos.Despacho;
using Capa_Entidades.Models.Despacho;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Capa_Negocio.Despacho
{
    public class Neg_Despacho
    {
        Dat_Despacho dat_despacho = new Dat_Despacho();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public ObservableCollection<Ent_Despacho> GetPedidosPendientesDelivery()
        {
            ObservableCollection<Ent_Despacho> entDespacho = new ObservableCollection<Ent_Despacho>();
            try
            {
                DataTable dt = dat_despacho.GetPedidosPendientesDelivery();
                foreach (DataRow row in dt.Rows)
                {
                    Ent_Despacho _d = new Ent_Despacho();
                    _d.ID = Convert.ToInt32(row["ID"]);
                    _d.PED_ID_MESA = Convert.ToInt32(row["PED_ID_MESA"]);
                    _d.M_NOM = row["M_NOM"].ToString();
                    _d.PED_ID_EMPL = Convert.ToInt32(row["PED_ID_EMPL"]);
                    _d.PED_FECH_PED = Convert.ToDateTime(row["PED_FECH_PED"]);
                    _d.PED_ID_ESTADO = Convert.ToInt32(row["PED_ID_ESTADO"]);
                    _d.PED_IMPORTE = Convert.ToInt32(row["PED_IMPORTE"]);
                    _d.PED_DESCUENTO = Convert.ToDecimal(row["PED_DESCUENTO"]);
                    _d.PED_ID_TIP_DESC = Convert.ToInt32(row["PED_ID_TIP_DESC"]);
                    _d.PED_TOTAL = Convert.ToDecimal(row["PED_TOTAL"]);
                    _d.PED_ID_CLIENTE = Convert.ToInt32(row["PED_ID_CLIENTE"]);
                    _d.PED_ID_CIERRE = Convert.ToInt32(row["PED_ID_CIERRE"]);
                    _d.PED_ID_USU = Convert.ToInt32(row["PED_ID_USU"]);
                    _d.PED_ID_TURNO = Convert.ToInt32(row["PED_ID_TURNO"]);
                    _d.PED_FECH_MODIFI = Convert.ToDateTime(row["PED_FECH_MODIFI"]);
                    _d.PED_ID_CAMBIO_MONE = Convert.ToInt32(row["PED_ID_CAMBIO_MONE"]);
                    _d.PED_NUM_DIARIO = Convert.ToInt32(row["PED_NUM_DIARIO"]);
                    _d.PED_SUBTOTAL = Convert.ToDecimal(row["PED_SUBTOTAL"]);
                    _d.PED_NRO_PERSONAS = Convert.ToInt32(row["PED_NRO_PERSONAS"]);
                    _d.nomllevar = row["nomllevar"].ToString();
                    _d.telefcli = row["telefcli"].ToString();
                    _d.C_NOMINA = row["C_NOMINA"].ToString();
                    _d.C_TEL = row["C_TEL"].ToString();
                    _d.C_DIREC = row["C_DIREC"].ToString();
                    entDespacho.Add(_d);
                }
            }
            catch (Exception ex)
            {
                entDespacho = null;
            }
            return entDespacho;
        }
        public DataTable getEmpleadoExiste(string codigoEmpleado)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dat_despacho.getEmpleadoExiste(codigoEmpleado);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }
        public DataTable GetPedidosPendientesxnumdiario(string numdiaro)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dat_despacho.GetPedidosPendientesxnumdiario(numdiaro);
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }
        public bool setDespacho(string num_pedidos, int id_empleado)
        {
            bool result = false;
            result = dat_despacho.setDespacho(num_pedidos, id_empleado);
            return result;
        }
    }
}

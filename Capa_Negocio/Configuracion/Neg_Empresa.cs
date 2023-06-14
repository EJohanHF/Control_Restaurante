using Capa_Datos.Configuracion;
using Capa_Entidades.Models;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Configuracion
{
    public class Neg_Empresa
    {
        Dat_Empresa datEmpre = new Dat_Empresa();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Empresa> GetEmpresa()
        {
            ObservableCollection<Empresa> empresa = new ObservableCollection<Empresa>();
            try
            {
                
                DataTable dt = datEmpre.GetEmpresa();
                foreach (DataRow row in dt.Rows)
                {
                    Empresa _emp = new Empresa();
                    _emp.idEmpr = Convert.ToInt32(row["id"]);
                    _emp.empr_ruc = row["empr_ruc"].ToString();
                    _emp.empr_nom = row["empr_nom"].ToString();
                    _emp.empr_nom_com = row["empr_nom_com"].ToString(); //xd
                    _emp.empr_tel = row["empr_tel"].ToString();
                    _emp.empr_cor = row["empr_cor"].ToString();

                    _emp.idcorp = row["idcorp"].ToString();
                    _emp.empr_corp = row["corp_nom"].ToString();

                    _emp.idpais = row["idpais"].ToString();
                    _emp.empr_pais = row["nompais"].ToString();

                    _emp.iddepa = row["iddepa"].ToString();
                    _emp.empr_depa = row["nomdepa"].ToString();

                    _emp.idprov = row["idprov"].ToString();
                    _emp.empr_prov = row["nomprov"].ToString();

                    _emp.iddist = row["iddist"].ToString();
                    _emp.empr_dist = row["nomdist"].ToString();

                    _emp.empr_ubig = row["empr_ubig"].ToString(); //xd
                    _emp.empr_urb = row["empr_urb"].ToString();
                    _emp.empr_logo = (byte[])row["empr_logo"];
                   // aqui se convierte en ToByte;
                    _emp.empr_firma = row["empr_firma"].ToString();
                    _emp.empr_firma_alias = row["empr_firma_alias"].ToString(); //xd
                    _emp.empr_firma_clave_alias = row["empr_firma_clave_alias"].ToString();
                    _emp.empr_firma_usr_sol = row["empr_firma_usr_sol"].ToString();
                    _emp.empr_firma_clave_sol = row["empr_firma_clave_sol"].ToString();
                    _emp.empr_direc = row["empr_direc"].ToString();
                    _emp.EMPR_DEFAULT = row["EMPR_DEFAULT"].ToString();
                    _emp.codigo = row["codigo"].ToString();
                    _emp.token = row["token"].ToString();
                    _emp.empr_ruta_facelect = row["EMPR_RUTA_FAC_ELECTRONICA"].ToString();
                    _emp.EMPR_FV_LINCENCIA = row["EMPR_FV_LINCENCIA"].ToString();
                    empresa.Add(_emp);
                }
            }
            catch(Exception ex)
            {
                empresa = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return empresa;
        }
        public ObservableCollection<Empresa> GetEmpresaCorreo(int idcor)
        {
            ObservableCollection<Empresa> empresa = new ObservableCollection<Empresa>();
            try
            {

                DataTable dt = datEmpre.GetEmpresaCorreo(idcor);
                foreach (DataRow row in dt.Rows)
                {
                    Empresa _emp = new Empresa();
                    _emp.idcorreo = Convert.ToInt32(row["ID"]);
                    _emp.correo = row["EMCO_NOMBRE"].ToString();
                    empresa.Add(_emp);
                }
            }
            catch (Exception ex)
            {
                empresa = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return empresa;
        }
        public bool ActualizarLicencia(string codigo)
        {
            bool result = false;
            result = datEmpre.ActualizarLicencia(codigo);
            if (result)
            {
               
            }
            return result;
        }
        public bool SetEmpresa(int operacion, Empresa empresa, ref string _mensaje)
        {
            /*aqui se hacen las validaciones de los campos antes de enviarlos al metodo */
            bool result = false;
            result = datEmpre.SetEmpresa(operacion, empresa, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito.";
            }
            return result;
        }
        public bool SetEmpresaCorrreo(int operacion, Empresa empresa, ref string _mensaje, DataTable dt)
        {
            /*aqui se hacen las validaciones de los campos antes de enviarlos al metodo */
            bool result = false;
            result = datEmpre.SetEmpresaCorreo(operacion, empresa, ref _mensaje, dt);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito.";
            }
            return result;
        }
    }
}

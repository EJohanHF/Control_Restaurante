using Capa_Datos.Configuracion;
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
   public class Neg_Cliente
    {
        Dat_Cliente datCli = new Dat_Cliente();
        Funcion_Global globales = new Funcion_Global();
        public ObservableCollection<Cliente> GetCliente()
        {
            ObservableCollection<Cliente> cliente = new ObservableCollection<Cliente>();
            try
            {
                DataTable dt = datCli.GetCliente();
                foreach (DataRow row in dt.Rows) { 
                    Cliente _cli = new Cliente();
                    _cli.idcli = Convert.ToInt32(row["IDCLI"]);
                    _cli.denominacion = row["C_NOMINA"].ToString();
                    //_cli.apecli = row["C_APE"].ToString();
                    _cli.ndoc = row["C_NRO_DOC"].ToString();
                    _cli.idtd = Convert.ToInt32(row["id_tipo_doc"]); 
                    //_cli.tipodoc = row["DOCUMENTO"].ToString();
                    _cli.dircli = row["C_DIREC"].ToString();
                    _cli.distritocli = row["C_DISTR"].ToString();
                    _cli.callecli = row["C_CALLE"].ToString();
                    _cli.referenciacli= row["C_REF"].ToString();
                    _cli.telcli = row["C_TEL"].ToString();
                    _cli.corcli = row["C_COR"].ToString();
                    _cli.estadocli = Convert.ToByte(row["C_ACT"]);
                    cliente.Add(_cli);
                }

            }
            catch (Exception ex)
            {
                cliente = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return cliente;
        }
        public ObservableCollection<Cliente> GetCliente2()
        {
            ObservableCollection<Cliente> cliente = new ObservableCollection<Cliente>();
            try
            {
                DataTable dt = datCli.GetCliente();
                foreach (DataRow row in dt.Rows)
                {
                    Cliente _cli = new Cliente();
                    _cli.idcli = Convert.ToInt32(row["IDCLI"]);
                    _cli.denominacion2 = row["C_NRO_DOC"].ToString() + "-" + row["C_NOMINA"].ToString();
                    _cli.denominacion = row["C_NOMINA"].ToString();
                    //_cli.apecli = row["C_APE"].ToString();
                    _cli.ndoc = row["C_NRO_DOC"].ToString();
                    _cli.idtd = Convert.ToInt32(row["id_tipo_doc"]);
                    //_cli.tipodoc = row["DOCUMENTO"].ToString();
                    _cli.dircli = row["C_DIREC"].ToString();
                    _cli.telcli = row["C_TEL"].ToString();
                    _cli.corcli = row["C_COR"].ToString();
                    _cli.estadocli = Convert.ToByte(row["C_ACT"]);
                    cliente.Add(_cli);
                }

            }
            catch (Exception ex)
            {
                cliente = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return cliente;
        }
        public bool SetCliente(int operacion, Cliente cliente, ref string _mensaje)
        {
            bool result = false;
            result = datCli.SetCliente(operacion, cliente, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public int SetCliente2(int operacion, Cliente cliente, ref string _mensaje)
        {
            int result = 0;
            result = datCli.SetCliente2(operacion, cliente, ref _mensaje);
            if (result > 0)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public ObservableCollection<Cliente> GetClientexNombreTelefono(string buscador)
        {
            ObservableCollection<Cliente> cliente = new ObservableCollection<Cliente>();
            try
            {
                DataTable dt = datCli.GetClientexNombreTelefono(buscador);
                int contador = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Cliente _cli = new Cliente();
                    _cli.contador = contador + 1;
                    _cli.idcli = Convert.ToInt32(row["IDCLI"]);
                    _cli.denominacion2 = row["C_NRO_DOC"].ToString() + "-" + row["C_NOMINA"].ToString();
                    _cli.denominacion = row["C_NOMINA"].ToString();
                    _cli.ndoc = row["C_NRO_DOC"].ToString();
                    _cli.idtd = Convert.ToInt32(row["id_tipo_doc"]);
                    _cli.dircli = row["C_DIREC"].ToString();
                    _cli.telcli = row["C_TEL"].ToString();
                    _cli.corcli = row["C_COR"].ToString();
                    _cli.distritocli = row["C_DISTR"].ToString();
                    _cli.callecli = row["C_CALLE"].ToString();
                    _cli.referenciacli = row["C_REF"].ToString();
                    _cli.estadocli = Convert.ToByte(row["C_ACT"]);
                    cliente.Add(_cli);
                    contador = contador + 1;
                }
            }
            catch (Exception ex)
            {
                cliente = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return cliente;
        }
        public ObservableCollection<Cliente> GetClientexTelefono(string buscador)
        {
            ObservableCollection<Cliente> cliente = new ObservableCollection<Cliente>();
            try
            {
                DataTable dt = datCli.GetClientexTelefono(buscador);
                foreach (DataRow row in dt.Rows)
                {
                    Cliente _cli = new Cliente();
                    _cli.idcli = Convert.ToInt32(row["IDCLI"]);
                    _cli.denominacion2 = row["C_NRO_DOC"].ToString() + "-" + row["C_NOMINA"].ToString();
                    _cli.denominacion = row["C_NOMINA"].ToString();
                    //_cli.apecli = row["C_APE"].ToString();
                    _cli.ndoc = row["C_NRO_DOC"].ToString();
                    _cli.idtd = Convert.ToInt32(row["id_tipo_doc"]);
                    //_cli.tipodoc = row["DOCUMENTO"].ToString();
                    _cli.dircli = row["C_DIREC"].ToString();
                    _cli.telcli = row["C_TEL"].ToString();
                    _cli.corcli = row["C_COR"].ToString();
                    _cli.estadocli = Convert.ToByte(row["C_ACT"]);
                    cliente.Add(_cli);
                }
            }
            catch (Exception ex)
            {
                cliente = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return cliente;
        }
        public ObservableCollection<Cliente> GetClientexId(int idcli)
        {
            ObservableCollection<Cliente> cliente = new ObservableCollection<Cliente>();
            try
            {
                DataTable dt = datCli.GetClientexId(idcli);
                foreach (DataRow row in dt.Rows)
                {
                    Cliente _cli = new Cliente();
                    _cli.idcli = Convert.ToInt32(row["IDCLI"]);
                    _cli.denominacion2 = row["C_NRO_DOC"].ToString() + "-" + row["C_NOMINA"].ToString();
                    _cli.denominacion = row["C_NOMINA"].ToString();
                    //_cli.apecli = row["C_APE"].ToString();
                    _cli.ndoc = row["C_NRO_DOC"].ToString();
                    _cli.idtd = Convert.ToInt32(row["id_tipo_doc"]);
                    //_cli.tipodoc = row["DOCUMENTO"].ToString();
                    _cli.dircli = row["C_DIREC"].ToString();
                    _cli.telcli = row["C_TEL"].ToString();
                    _cli.corcli = row["C_COR"].ToString();
                    _cli.estadocli = Convert.ToByte(row["C_ACT"]);
                    cliente.Add(_cli);
                }

            }
            catch (Exception ex)
            {
                cliente = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return cliente;
        }
    }
}
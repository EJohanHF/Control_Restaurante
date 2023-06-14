using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos.Acceso;
using Capa_Entidades.Acceso;
using System.Data;

namespace Capa_Negocio.Acceso
{
    public class Neg_Corporacion
    {

        MantenimientoCorporacion dat_corp = new MantenimientoCorporacion();

        public DataTable N_listado()
        {
            return dat_corp.listar();
        }

        public Ent_Corporacion Registrar(string nombre, ref string _msj)
        {
            Ent_Corporacion _corp = null;
            String mensaje = "";

            try
            {
                if (nombre == "")
                {
                    mensaje += "Ingrese un nombre";
                }
                else
                {
                    _corp = dat_corp.D_Registrar(nombre);
                }
            }
            catch (Exception) { }
            _msj = mensaje;
            return _corp;

        }

        public Ent_Corporacion Actualizar(string nombre,int id,ref string _msj)
        {
            Ent_Corporacion _corp = null;
            String mensaje = "";

            try
            {
                if (nombre == "")
                {
                    mensaje += "Ingrese un nombre";
                }
                else
                {
                    _corp = dat_corp.D_Actualizar(nombre,id);
                }
            }
            catch (Exception) { }
            _msj = mensaje;
            return _corp;

        }

        public Ent_Corporacion Eliminar(int id,ref string _msj)
        {
            Ent_Corporacion _corp = null;
            String mensaje = "";

            try
            {
                
                    _corp = dat_corp.D_Eliminar(id);
            }
            catch (Exception) { }
            _msj = mensaje;
            return _corp;
        }

        /*public void Registrarxd(string nombre)
{
    dat_corp.InsertarCorp(nombre);
}*/


    }
}

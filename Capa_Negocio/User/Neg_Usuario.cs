using Capa_Datos.Acceso;
using Capa_Datos.User;
using Capa_Entidades.Models.Usuario;
using Capa_Negocio.Funciones_Globales;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.User
{
   public class Neg_Usuario
    {
        Dat_Usuarios datUsu = new Dat_Usuarios();
        Funcion_Global globales = new Funcion_Global();

        public ObservableCollection<Usuario> GetUsuario()
        {
            ObservableCollection<Usuario> usuario = new ObservableCollection<Usuario>();
            try
            {
                DataTable dt = datUsu.GetUsuario();
                foreach (DataRow row in dt.Rows)
                {
                    Usuario _usu = new Usuario();
                    _usu.idusu   = Convert.ToInt32(row["ID"]);

                    _usu.idemple = row["idemp"].ToString();
                    _usu.nomemple = row["EMPL_NOM"].ToString();

                    _usu.nomusu = row["USU_NOM"].ToString();
                    _usu.idrol = row["id_rol"].ToString();
                    _usu.apeusu = row["USU_AP"].ToString();
                    _usu.estadousu = Convert.ToByte(row["USU_EST"]);
                    _usu.claveusu = row["USU_CLAVE"].ToString();
                    usuario.Add(_usu);
                }

            }
            catch (Exception ex)
            {
                usuario = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return usuario;
        }
        public ObservableCollection<Usuario> getUsuariosActivos()
        {
            ObservableCollection<Usuario> usuario = new ObservableCollection<Usuario>();
            try
            {
                DataTable dt = datUsu.getUsuariosActivos();
                foreach (DataRow row in dt.Rows)
                {
                    Usuario _usu = new Usuario();
                    _usu.idusu   = Convert.ToInt32(row["ID"]);
                    _usu.idemple = row["idemp"].ToString();
                    _usu.nomemple = row["EMPL_NOM"].ToString();
                    _usu.nomusu = row["USU_NOM"].ToString();
                    _usu.idrol = row["id_rol"].ToString();
                    _usu.apeusu = row["USU_AP"].ToString();
                    _usu.estadousu = Convert.ToByte(row["USU_EST"]);
                    _usu.claveusu = row["USU_CLAVE"].ToString();
                    usuario.Add(_usu);
                }
            }
            catch (Exception ex)
            {
                usuario = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return usuario;
        }
        public DataTable GetUsuarioActivo()
        {
            DataTable usuario;
            try
            {
                DataTable dt = datUsu.GetUsuarioActivo();
                usuario = dt;
            }
            catch (Exception ex)
            {
                usuario = null;
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            return usuario;
        }
        public bool SetUsuario(int operacion, Usuario usuario, ref string _mensaje)
        {
            bool result = false;
            result = datUsu.SetUsuario(operacion, usuario, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }
            return result;
        }
        public bool SetUsuarioUpdEstado(int operacion, Usuario usuario, ref string _mensaje)
        {
            bool result = false;
            result = datUsu.SetUsuarioUpdateEstado(operacion, usuario, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito !";
            }

            return result;
        }
    }
}

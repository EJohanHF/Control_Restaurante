using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos.Acceso;
using Capa_Entidades.Acceso;

namespace Capa_Negocio.Acceso
{
    public class Neg_Login
    {
        Dat_Usuario dat_usuario = new Dat_Usuario();

        public Ent_Usuario login(int id, string contraseña, ref string _mensaje)
        {
            Ent_Usuario _usu = null;
            string mensaje = "";
            try
            {
                if (contraseña == "")
                {
                    mensaje += "Contraseña inválido." + Environment.NewLine;
                }
                if (mensaje != "")
                {
                    _usu = null;
                }
                else
                {
                    _usu = dat_usuario.login(id);
                }
            }
            catch (Exception e)
            {
                _usu = null;
            }
            _mensaje = mensaje;
            return _usu;
        }
        public Ent_Usuario VerificaEmpleado(string contraseña, ref string _mensaje)
        {
            Ent_Usuario _usu = null;
            string mensaje = "";
            try
            {
                if (contraseña == "")
                {
                    mensaje += "Contraseña inválido." + Environment.NewLine;
                }
                if (mensaje != "")
                {
                    _usu = null;
                }
                else
                {
                    _usu = dat_usuario.VerificaEmpleado(contraseña);
                }
            }
            catch (Exception e)
            {
                _usu = null;
            }
            _mensaje = mensaje;
            return _usu;
        }
        public Ent_Usuario login2(string usuario, string contraseña, string nomequipo, string ip, ref string _mensaje)
        {
            Ent_Usuario _usu = null;
            string mensaje = "";
            try
            {
                /*validacion de los campos*/
                if (usuario == "")
                {
                    mensaje += "Usuario no puede tener menos de 8 caracteres." + Environment.NewLine;
                }
                if (contraseña == "")
                {
                    mensaje += "Contraseña inválido." + Environment.NewLine;
                }
                if (mensaje != "")
                {
                    _usu = null;
                }
                else
                {
                    _usu = dat_usuario.login2(usuario, nomequipo, ip);

                }
            }
            catch (Exception e)
            {
                _usu = null;
            }
            _mensaje = mensaje;
            return _usu;
        }
        public bool ActualizarEstadoLogueado(string id)
        {
            bool resul = false;
            try
            {
                resul = dat_usuario.ActualizarEstadoLogueado(id);
            }
            catch (Exception)
            {
                resul = false;
            }
            return resul;
            
        }
    }
}

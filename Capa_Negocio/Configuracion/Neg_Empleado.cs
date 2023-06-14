using Capa_Datos.Configuracion;
using Capa_Entidades.Models;
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
    public class Neg_Empleado
    {
        Dat_Empleados datEmp = new Dat_Empleados();
        Funcion_Global globales = new Funcion_Global();

        public ObservableCollection<Empleado> GetEmpleados()
        {
            ObservableCollection<Empleado> empleados = new ObservableCollection<Empleado>();
            try
            {
                DataTable dt = datEmp.GetEmpleados();
                foreach (DataRow row in dt.Rows)
                {
                    Empleado _emp = new Empleado();
                    _emp.id = Convert.ToInt32(row["id"]);

                    _emp.idTipoDI = row["id_tipo_doc"].ToString();
                    _emp.tipoDocumento = row["DOCUMENTO"].ToString(); //xd
                    
                    _emp.idcargo = row["IDCARGO"].ToString();
                    _emp.cargo = row["nom_cargo"].ToString(); //xd

                    _emp.nroDocumento = row["EMPL_NRO_DOC"].ToString();
                    _emp.nombres = row["EMPL_NOM"].ToString();
                    _emp.apellidos = row["EMPL_APE"].ToString();
                    _emp.genero = row["EMPL_GEN"].ToString();
                    _emp.estado = Convert.ToByte(row["EMPL_EST"]);
                    _emp.fecNacimiento = Convert.ToDateTime(row["EMPL_F_NAC"].ToString());
                    _emp.clave = row["clave"].ToString();
                    empleados.Add(_emp);

                }
            }
            catch(Exception ex)
            {
                empleados = null;
                //globales.Mensaje("SOS-FOOD Error", ex.Message, 3);
            }
            return empleados;           
        }
        public bool SetEmpleados(int operacion, Empleado empleado, ref string _mensaje)
        {
            /*aqui se hacen las validaciones de los campos antes de enviarlos al metodo */
            bool result = false;
            result = datEmp.SetEmpleados(operacion, empleado, ref _mensaje);
            if (result)
            {
                _mensaje = "Operacion realizada con éxito.";
            }
            return result;
        }
        
    }
}

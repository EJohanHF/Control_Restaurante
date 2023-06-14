using Capa_Negocio.Despacho;
using Capa_Negocio.Funciones_Globales;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
namespace Capa_Presentacion_WPF.ViewModels.DeliveryWebService
{
    public class DialogCodigoMotorizadoViewModel : IGeneric
    {
        #region Objetos
        public string Pedidos { get; set; }
        public string CodigoEmpleado { get; set; }
        #endregion
        #region Negocio
        Neg_Despacho negDespacho = new Neg_Despacho();
        Funcion_Global globales = new Funcion_Global();
        #endregion
        #region Commands
        public ICommand CerrarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        #endregion
        public DialogCodigoMotorizadoViewModel()
        {
            this.CerrarCommand = new RelayCommand(new Action(Cerrar));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            Pedidos = Application.Current.Properties["NumeroDiarioPedidos"].ToString();
        }
        private void Cerrar()
        {
            Application.Current.Properties["CerrarDialogMotorizado"] = "SI";
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void Guardar()
        {
            try
            {
                if (CodigoEmpleado.Trim() != "")
                {
                    DataTable empleado = negDespacho.getEmpleadoExiste(CodigoEmpleado);
                    if (empleado != null)
                    {
                        if (empleado.Rows.Count > 0)
                        {
                            bool res = negDespacho.setDespacho(Pedidos, Convert.ToInt32(empleado.Rows[0]["ID"]));
                            if (res == true)
                            {
                                Application.Current.Properties["CerrarDialogMotorizado"] = "NO";
                                Application.Current.Properties["NumeroDiarioPedidos"] = null;
                                DialogHost.CloseDialogCommand.Execute(null, null);
                            }
                        }
                        else
                        {
                            globales.Mensaje("SOS-FOOD - Informacion", "El codigo de empleado ingresado no existe.", 2);
                        }
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "El codigo de empleado ingresado no existe.", 2);
                    }
                }
            }
            catch (Exception ex)
            {
                globales.Mensaje("SOS-FOOD - Informacion", "El codigo de empleado ingresado no existe.", 2);
            }
        }
    }
}

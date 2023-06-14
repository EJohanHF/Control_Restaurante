using Capa_Negocio.Funciones_Globales;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogCambiarCantidadViewModel
    {
        public bool isactive { get; set; }
        private string mensajesnak;
        public static int contador = 0;
        #region SnackBar
        public bool IsActive
        {
            get => isactive;
            set
            {
                //this.usua = Pagar.idusuario;

                if (value == true)
                {
                    var ofSatckbar = (TimeSpan.FromMilliseconds(9000));
                    if (ofSatckbar == (TimeSpan.FromMilliseconds(9000)))
                    {
                        isactive = false;
                    }
                }
                isactive = value;
            }
        }
        public string mensajeSnack
        {
            get => mensajesnak;
            set
            {
                mensajesnak = value;
            }
        }

        public void ocultarSnackBar(TimeSpan tiempo)
        {
            IsActive = false;
        }
        #endregion
        Funcion_Global globales = new Funcion_Global();
        public decimal cantidad { get; set; }
        public string cantidadnueva { get; set; }
        public ICommand CerrarDialog { get; set; }
        public ICommand Guardar { get; set; }
        public DialogCambiarCantidadViewModel()
        {
            try
            {
                this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
                //this.Guardar = new RelayCommand(new Action(GuardarCantidad));
                this.Guardar = new ParamCommand(new Action<object>(GuardarCantidad));
                if (Application.Current.Properties["CantidadActual"] != null)
                {
                    this.cantidad = Convert.ToDecimal(Application.Current.Properties["CantidadActual"]);
                }
                else
                {
                    this.cantidad = 0;
                }
            }
            catch (Exception ex)
            {
                //globales.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            
            
        }
        private void CloseDialog()
        {
            Application.Current.Properties["NuevaCantidad"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void GuardarCantidad(object parameter)
        {
            try
            {
                var textBox = parameter as TextBox;
                var texto = textBox.Text;
                cantidadnueva = texto;

                if (cantidadnueva != null && cantidadnueva.ToString().Trim() != "")
                {
                    if (Convert.ToDecimal(cantidadnueva) != 0)
                    {
                        Application.Current.Properties["NuevaCantidad"] = cantidadnueva;
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                    else
                    {
                        globales.Mensaje("SOS-FOOD - Informacion", "Ingrese cantidad valida", 3);
                    }
                }
                else
                {
                    globales.Mensaje("SOS-FOOD - Informacion", "Ingrese cantidad valida", 3);
                }

            }
            catch (Exception ex)
            {
                //globales.Mensaje("SOS-FOOD - Error", ex.Message, 3);
            }
            
        }
        public void MensajeSnack(string mensaje)
       {
            contador = 0;
            IsActive = true;
            mensajeSnack = mensaje;

            Timer timer = new Timer(5000);
            timer.AutoReset = true;

            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_elapsed);

            timer.Start();
        }
        private void timer_elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (contador == 1)
            {
                IsActive = false;
            }
            if (contador == 0)
            {
                contador += 1;
            }
        }
    }
}

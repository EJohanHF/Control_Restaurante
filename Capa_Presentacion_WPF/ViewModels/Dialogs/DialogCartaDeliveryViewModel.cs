using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Delivery_Web_Service;
using Capa_Entidades.Models.Web_Service;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Negocio.WebService;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs
{
    public class DialogCartaDeliveryViewModel : IGeneric
    {
        Neg_Parametros negParametros = new Neg_Parametros();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        public ICommand btnImagenCommand { get; set; }
        public ICommand CerrarDialog { get; set; }
        public ICommand Guardar { get; set; }
        public object Logo { get; set; }
        public byte[] Imagen { get; set; }
        public decimal PrecioPlato { get; set; }
        public decimal DescPlato { get; set; }
        public int IdCarta { get; set; }
        public ObservableCollection<CartaDeliveryWebService> DataCartaDelivery { get; set; }
        Neg_CartaDeliveryWebService negCartaDelivery = new Neg_CartaDeliveryWebService();
        private CartaDeliveryWebService cartadelivery;
        public CartaDeliveryWebService CartaDelivery
        {
            get => cartadelivery;
            set
            {
                cartadelivery = value;
            }
        }
        public DialogCartaDeliveryViewModel()
        {
            this.btnImagenCommand = new RelayCommand(new Action(CargarIMG));
            this.CerrarDialog = new RelayCommand(new Action(CloseDialog));
            this.Guardar = new RelayCommand(new Action(GuardarPlatoDelivery));
            this.Logo = new object();
            if(Application.Current.Properties["CodCarta"] != null)
            {
                this.IdCarta = Convert.ToInt32(Application.Current.Properties["CodCarta"]);
                this.DataCartaDelivery = negCartaDelivery.GetCartaDeliveryWebService(IdCarta);
                    this.CartaDelivery = this.DataCartaDelivery.FirstOrDefault();
                if (this.CartaDelivery == null)
                {
                    this.CartaDelivery = new CartaDeliveryWebService();
                    this.CartaDelivery.Name = Application.Current.Properties["NomCarta"].ToString();
                }
                else
                {
                    this.Logo = this.CartaDelivery.imagen;
                }
               
            }
            else
            {
                this.IdCarta = 0;
                this.CartaDelivery = new CartaDeliveryWebService();
                this.CartaDelivery.Name = Application.Current.Properties["NomCarta"].ToString();
            }
        }
        public ObservableCollection<Empresa> DataEmpresa { get; set; }
       
        Neg_Empresa negEmp = new Neg_Empresa();
        public async void GuardarPlatoDelivery()
        {
            
            CartaDeliveryWebService cartadelivery = this.CartaDelivery;
            //if (cartadelivery.imagen == null)
            //{
            //    negFuncionGlobal.Mensaje("SOS-FOOD - Error", "Debe elegir una imagen para la categoria nueva", 2);
            //    return;
            //}
            
            if (this.IdCarta == 0)
            {
                this.cartadelivery.id_carta = this.IdCarta.ToString();
                Application.Current.Properties["CartaDelivery"] = cartadelivery;
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                DataEmpresa = negEmp.GetEmpresa();
                string codigo_cliente = DataEmpresa.Where(e => e.EMPR_DEFAULT == "1").FirstOrDefault().codigo;
                this.cartadelivery.id_carta = this.IdCarta.ToString();
                bool result = negCartaDelivery.setCartaDeliveryWebService(cartadelivery, this.IdCarta, true);
                try
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        values["name"] = this.cartadelivery.Name;
                        values["id_business"] = codigo_cliente;
                        //values["token"] = "app963";
                        values["discount"] = this.cartadelivery.discount.ToString();
                        values["price"] = this.cartadelivery.price.ToString();
                        values["price_local"] = this.cartadelivery.price_salon.ToString();
                        values["imagen"] = (this.cartadelivery.imagen == null) ? "" : this.cartadelivery.imagen.ToString();
                        values["id_carta"] = this.cartadelivery.id_carta.ToString();
                        if (this.cartadelivery.estado_del == true)
                        {
                            values["estado"] = "1";
                        }
                        else
                        {
                            values["estado"] = "0";
                        }
                        if (this.cartadelivery.estado_sal == true)
                        {
                            values["estado_local"] = "1";
                        }
                        else
                        {
                            values["estado_local"] = "0";
                        }

                        var response = client.UploadValues(negParametros.GuardarPlato(), values);
                        var responseString = Encoding.Default.GetString(response);
                        //var WebService = new RespuestaWebService();

                        //WebService = JsonConvert.DeserializeObject<RespuestaWebService>(responseString);





                    }


                   
                   
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                catch (Exception ex)
                {
                    //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message.ToString(), 3);
                }
            }
        }
        private void CloseDialog()
        {
            Application.Current.Properties["CartaDelivery"] = null;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        private void CargarIMG()
        {
            Microsoft.Win32.OpenFileDialog getimg = new Microsoft.Win32.OpenFileDialog();
            getimg.InitialDirectory = "C:\\";
            getimg.Filter = "Archivos de imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG (*.png)|*.png";

            if (getimg.ShowDialog() == true)

            {
                try
                {
                    //string NombreArchivo;

                    string RutaArchivo;
                    RutaArchivo = getimg.FileName.ToString();
                    this.Logo = RutaArchivo;
                    /*
                     Hasta aqui muestra la imagen del directorio.
                     */


                    byte[] imagen = null;
                    imagen = File.ReadAllBytes(RutaArchivo); //Convierto la imagen a byte[] para guardarlo en la base de datos.
                    this.CartaDelivery.imagen = imagen;
                    //Empresa empresa = this.Empresa;
                    // var _id = empresa.empr_logo();
                    //this.Logo2 = LoadImage(imagen);// si la imagen viene de la base de datos en byte[] lo puedes levantar con ese metodo

                }
                catch (Exception ex)
                {
                    //negFuncionGlobal.Mensaje("SOS-FOOD - Error", "No se puede carga la imagen : " + ex.Message, 3);
                }
            }
        }
    }
}

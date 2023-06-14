using Capa_Entidades;
using Capa_Entidades.Models.Configuracion;
using Capa_Entidades.Models.Facturacion_Electronica;
using Capa_Negocio;
using Capa_Negocio.Carta;
using Capa_Negocio.Configuracion;
using Capa_Negocio.Facturacion_Electronica;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

using System.Windows.Input;
using ThoughtWorks.QRCode.Codec;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.Dialogs.Facturacion_Electronica
{
    public class DialogFacturacionElectronicaViewModel : INotifyPropertyChanged
    {
        #region INotify
        //Para el INotifyPropertyChanged sea Inicializado.
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        //
        //public string RucCliente { get; set; }
        private string _ValidarNumero="";
        public event PropertyChangedEventHandler PropertyChanged_ValidarNumero;
        public string RucCliente
        {
            get { return _ValidarNumero; }
            set
            {
                if ((value.Length <= 11 && tipDoc.ToString() == "6") || (value.Length <= 8 && tipDoc.ToString() == "1"))
                {
                    _ValidarNumero = value;
                    OnPropertyChanged_ValidarNumero("RucCliente");
                }
                else if (tipDoc.ToString() == "4" || tipDoc.ToString() == "7")
                {
                    _ValidarNumero = value;
                    OnPropertyChanged_ValidarNumero("RucCliente");
                }
            }
        }

        public void OnPropertyChanged_ValidarNumero(string txtRucCliente)
        {
            PropertyChangedEventHandler handler = PropertyChanged_ValidarNumero;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(txtRucCliente));

            if(this.RucCliente != "")
            {
                this.RucCliente = isCaracterValido(this.RucCliente);
            }
        }
        public void BuscCliente(string criterio)
        {
            PropertyChangedEventHandler handler = PropertyChanged_ValidarNumero;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(criterio));

            if (this.RucCliente != "")
            {
               
                this.DataClienteBusqueda = new ObservableCollection<Cliente>(this.DataCliente.Where(c => c.ndoc.Contains(this.RucCliente.ToString())));
            }

            
        }

        #endregion
        #region negocio
        Neg_Cliente negCli = new Neg_Cliente();
        Neg_Doc_Electronico negDocElectronico = new Neg_Doc_Electronico();
        Neg_Data_Documento_Electronico negDataDocumentoElectronico = new Neg_Data_Documento_Electronico();
        Neg_Empresa negEmpr = new Neg_Empresa();
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Parametros negParametros = new Neg_Parametros();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        #endregion
        #region Lista
        public ObservableCollection<Data_Documento_Electronico> DataDocumentoElectronico { get; set; }
        public ObservableCollection<Data_Documento_Electronico> DataDocumentoElectronico2 { get; set; }
        public List<Tipo_Doc_Electronico> TipDocElectr = new List<Tipo_Doc_Electronico>();
        public List<Serie_Numero_Documento> SerieNumeroDocumento = new List<Serie_Numero_Documento>();
        public List<Ent_Combo> ComboTipoMoneda { get; set; }
        public Neg_Tip_Doc_Electronico negTipDocElec = new Neg_Tip_Doc_Electronico();
        public List<Ent_Combo> ComboTipoDoc { get; set; }
        public ObservableCollection<Cliente> DataCliente { get; set; }
        public ObservableCollection<Cliente> DataClienteBusqueda { get; set; }
        static ObservableCollection<Empresa> empresa = new ObservableCollection<Empresa>();
        #endregion
        #region Command
        public ICommand CancelarCommand { get; set; }
        public ICommand BuscarCliente { get; set; }
        public ICommand GenerarFE { get; set; }
        public ICommand EliminarPlato { get; set; }
        public ICommand GenerarFECop { get; set; }
        public ICommand MostrarTodo { get; set; }
        #endregion
        #region Objetos
        public bool copia { get; set; }
        public string NomTipDoc { get; set; }
        public string BotonImprimir { get; set; }
        public string tipDoc { get; set; } = "";
        public string idtmoneda { get; set; }
        public int id { get; set; }
        public string NomEmpr { get; set; }
        public string RucEmpr { get; set; }
        public string DirEmpr { get; set; }
        public string DistEmpr { get; set; }
        public string ImagenBoton { get; set; }
        public DateTime Fecha { get; set; }
        public int idPed { get; set; }
        public decimal TotalDoc { get; set; }
        public decimal DescDoc { get; set; }
        public decimal IgvDoc { get; set; }
        public decimal DescSinIgvDoc { get; set; }
        public decimal OpGravadas { get; set; }
        public decimal OpExoneradas { get; set; }
        public string serie { get; set; }
        public string RazonCliente { get; set; }
        public string CorreoCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string IdCLiente { get; set; }
        public string Max { get; set; }
        public bool Busqueda { get; set; } = true;
        #endregion
        #region Entidad
        public Cliente _SelectCliente { get; set; }
        public Cliente ClienteSelected
        {
            get => _SelectCliente;
            set
            {
                if (value != null)
                {
                    Busqueda = false;

                    this.IdCLiente = value.idcli.ToString();
                    this.RucCliente = value.ndoc;
                    this.RazonCliente = value.denominacion;
                    this.CorreoCliente = value.corcli;
                    this.TelefonoCliente = value.telcli;
                    this.DireccionCliente = value.dircli;
                    Busqueda = true;
                }
                _SelectCliente = value;
            }
        }
        #endregion

        public DialogFacturacionElectronicaViewModel()
        {
            decimal rc = negParametros.RC();
            decimal igv = negParametros.IGV();
            decimal d = rc + igv + 1;
            DataClienteBusqueda = new ObservableCollection<Cliente>();
            DataCliente = negCli.GetCliente2();
            factura_detalle();
            this.RucCliente = "";
            this.RazonCliente = "";
            this.CorreoCliente = "";
            this.TelefonoCliente = "";
            this.DireccionCliente = "";
            this.GenerarFE = new RelayCommand(new Action(GeneraFE));
            this.GenerarFECop = new RelayCommand(new Action(GeneraFECopia));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
            this.BuscarCliente = new RelayCommand(new Action(BuscaCliente));
            this.TipDocElectr = negTipDocElec.GetTipDocElectronico();
            this.SerieNumeroDocumento = negTipDocElec.GetSerieNumeroDocumento();
            if (Application.Current.Properties["TipDocElectronico"] == null)
            {
                id = 1;
            }
            else
            {
                id = (int)Application.Current.Properties["TipDocElectronico"];
            }
            if (Application.Current.Properties["ClienteLlevar"] != null)
            {
                this.RazonCliente = Application.Current.Properties["ClienteLlevar"].ToString();
            }
            this.NomTipDoc = this.TipDocElectr.Where(t => t.ID == id).ToList().FirstOrDefault().NOM_TIPO_DOC;
            string caja2 = ConfigurationManager.AppSettings["caja2"].ToString();
            if (caja2 == "SI")
            {
                this.serie = this.SerieNumeroDocumento.Where(t => t.VALOR == (string)this.TipDocElectr.Where(j => j.ID == id).ToList().LastOrDefault().VALOR_TIPO_DOC).ToList().LastOrDefault().SERIE;
                this.serie = negTipDocElec.GetSerieNumero(this.serie);
            }
            else
            {
                this.serie = this.SerieNumeroDocumento.Where(t => t.VALOR == (string)this.TipDocElectr.Where(j => j.ID == id).ToList().FirstOrDefault().VALOR_TIPO_DOC).ToList().FirstOrDefault().SERIE;
                this.serie = negTipDocElec.GetSerieNumero(this.serie);
            }

            empresa = negEmpr.GetEmpresa();
            this.NomEmpr = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().empr_nom;
            this.RucEmpr = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().empr_ruc;
            this.DirEmpr = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().empr_direc;
            this.DistEmpr = empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().empr_depa + "-" + empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().empr_prov + "-" + empresa.Where(w => w.EMPR_DEFAULT == "1").ToList().FirstOrDefault().empr_dist;
            this.Fecha = DateTime.Now;
            this.ComboTipoDoc = negCombo.GetComboTipoDI();
            if (id == 1)
            {
                this.tipDoc = "1";
                this.Max = "100";
                this.ImagenBoton = "/Resources/Images/botones/reniec.png";
            } else if (id == 2) {
                this.tipDoc = "6";
                this.Max = "11";
                this.ImagenBoton = "/Resources/Images/botones/sunat.png";
            }
            else
            {
                this.tipDoc = "0";
                this.ImagenBoton = "/Resources/Images/botones/reniec.png";
            }
            this.ComboTipoMoneda = negCombo.GetComboTipoMoneda();
            if (Application.Current.Properties["IdPedidoDoc"] != null)
            {
                idPed = (int)Application.Current.Properties["IdPedidoDoc"];
            }
            else
            {
                idPed = 0;
            }

            this.DataDocumentoElectronico = negDataDocumentoElectronico.GetDataDocumentoElectronico(idPed);
            this.DataDocumentoElectronico2 = negDataDocumentoElectronico.GetDataDocumentoElectronico(idPed);
            foreach (var item in this.DataDocumentoElectronico2)
            {
                if(item.CANT_DOC > 0)
                {
                    for(int j = 1;j <= item.CANT_DOC; j++)
                    {
                        EliminarPlatos(item.ID);
                    }
                    
                }
            }
            if (this.DataDocumentoElectronico.Count == 0)
            {
                this.TotalDoc = 0;
                this.OpGravadas = 0;
                this.DescDoc = 0;
                this.DescSinIgvDoc = 0;
                this.IgvDoc = 0;

            }
            else
            {
                this.DescDoc = this.DataDocumentoElectronico.ToList().FirstOrDefault().DESCUENTO_TOTAL;
                this.TotalDoc = this.DataDocumentoElectronico.Sum(w => (decimal)w.IMPORTE) + this.DataDocumentoElectronico.Sum(w => (decimal)w.DESCUENTO);
                this.TotalDoc = this.TotalDoc - DescDoc;
                this.OpExoneradas = this.DataDocumentoElectronico.Where(w => w.EXONERADA == 1).Sum(s => s.IMPORTE);
                this.OpGravadas = Math.Round((Convert.ToDecimal(((double)TotalDoc - (double)OpExoneradas) / (double)d)), 2, MidpointRounding.ToEven);
                this.DescSinIgvDoc = Math.Round((Convert.ToDecimal((double)DescDoc / 1.18)), 2, MidpointRounding.ToEven);
                this.IgvDoc = Math.Round((Convert.ToDecimal(this.TotalDoc - this.OpGravadas - this.OpExoneradas)), 2, MidpointRounding.ToEven);
            }
           
            this.EliminarPlato = new ParamCommand(new Action<object>(EliminarPlatos));
            this.MostrarTodo = new RelayCommand(new Action(MostrarTodoPedido));

            if (Application.Current.Properties["IdClienteDel"] != null )
            {
                if(Convert.ToInt32(Application.Current.Properties["IdClienteDel"])!= 0)
                {
                    if (DataCliente != null)
                    {
                        if (DataCliente.Where(c => c.idcli == Convert.ToInt32(Application.Current.Properties["IdClienteDel"])).Count() != 0)
                        {
                            this.IdCLiente = DataCliente.Where(c => c.idcli == Convert.ToInt32(Application.Current.Properties["IdClienteDel"])).FirstOrDefault().idcli.ToString();
                            this.RucCliente = DataCliente.Where(c => c.idcli == Convert.ToInt32(Application.Current.Properties["IdClienteDel"])).FirstOrDefault().ndoc;
                            this.RazonCliente = DataCliente.Where(c => c.idcli == Convert.ToInt32(Application.Current.Properties["IdClienteDel"])).FirstOrDefault().denominacion;
                            this.CorreoCliente = DataCliente.Where(c => c.idcli == Convert.ToInt32(Application.Current.Properties["IdClienteDel"])).FirstOrDefault().corcli;
                            this.TelefonoCliente = DataCliente.Where(c => c.idcli == Convert.ToInt32(Application.Current.Properties["IdClienteDel"])).FirstOrDefault().telcli;
                            this.DireccionCliente = DataCliente.Where(c => c.idcli == Convert.ToInt32(Application.Current.Properties["IdClienteDel"])).FirstOrDefault().dircli;
                            this.tipDoc = DataCliente.Where(c => c.idcli == Convert.ToInt32(Application.Current.Properties["IdClienteDel"])).FirstOrDefault().idtd.ToString();
                            return;
                        }
                    }
                }
            }

        }
        public void MostrarTodoPedido()
        {
            decimal rc = negParametros.RC();
            decimal igv = negParametros.IGV();
            decimal d = rc + igv + 1;
            this.DataDocumentoElectronico = negDataDocumentoElectronico.GetDataDocumentoElectronicoTotal(idPed);
            if (this.DataDocumentoElectronico.Count == 0)
            {
                this.TotalDoc = 0;
                this.OpGravadas = 0;
                this.DescDoc = 0;
                this.DescSinIgvDoc = 0;
                this.IgvDoc = 0;

            }
            else
            {
                //this.DescDoc = this.DataDocumentoElectronico.ToList().FirstOrDefault().DESCUENTO_TOTAL;
                //this.TotalDoc = this.DataDocumentoElectronico.Sum(w => (decimal)w.IMPORTE) + this.DataDocumentoElectronico.Sum(w => (decimal)w.DESCUENTO);
                //this.TotalDoc = this.TotalDoc - DescDoc;
                //this.OpGravadas = Math.Round((Convert.ToDecimal((double)TotalDoc / 1.18)), 2, MidpointRounding.ToEven);
                //this.DescSinIgvDoc = Math.Round((Convert.ToDecimal((double)DescDoc / 1.18)), 2, MidpointRounding.ToEven);
                //this.IgvDoc = Math.Round((Convert.ToDecimal(this.TotalDoc - this.OpGravadas)), 2, MidpointRounding.ToEven);

                this.DescDoc = this.DataDocumentoElectronico.ToList().FirstOrDefault().DESCUENTO_TOTAL;
                this.TotalDoc = this.DataDocumentoElectronico.Sum(w => (decimal)w.IMPORTE) + this.DataDocumentoElectronico.Sum(w => (decimal)w.DESCUENTO);
                this.TotalDoc = this.TotalDoc - DescDoc;
                this.OpExoneradas = this.DataDocumentoElectronico.Where(w => w.EXONERADA == 1).Sum(s => s.IMPORTE);
                this.OpGravadas = Math.Round((Convert.ToDecimal(((double)TotalDoc - (double)OpExoneradas) / (double)d)), 2, MidpointRounding.ToEven);
                this.DescSinIgvDoc = Math.Round((Convert.ToDecimal((double)DescDoc / 1.18)), 2, MidpointRounding.ToEven);
                this.IgvDoc = Math.Round((Convert.ToDecimal(this.TotalDoc - this.OpGravadas - this.OpExoneradas)), 2, MidpointRounding.ToEven);
            }
        }

        public string isCaracterValido(string txt)
        {
            string ultimo = txt.Substring(txt.Length-1,1);
            Char c = Convert.ToChar(ultimo);
            if ((c >= '0' && c <= '9'))
            {
                return txt;
            }
            else
            {
                return txt.Replace(ultimo,"");
            }
            
        }
        private void EliminarPlatos(object id)
        {
            if (DataDocumentoElectronico.Count() > 1)
            {
                decimal cant = this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().CANTIDAD;
                if (cant > 1)
                {
                    Data_Documento_Electronico Data = new Data_Documento_Electronico();
                    this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().CANTIDAD = this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().CANTIDAD - 1;
                    this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().IMPORTE = Math.Round((Convert.ToDecimal(this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().PRECIO_UNI * this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().CANTIDAD)), 2, MidpointRounding.ToEven);
                    this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().IMPORTE = Math.Round((Convert.ToDecimal(this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().IMPORTE - this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().DESCUENTO)), 2, MidpointRounding.ToEven);
                    this.TotalDoc = this.DataDocumentoElectronico.Sum(w => w.IMPORTE);
                    //this.OpGravadas = Math.Round((Convert.ToDecimal((double)TotalDoc / 1.18)), 2, MidpointRounding.ToEven);
                    this.OpExoneradas = this.DataDocumentoElectronico.Where(w => w.EXONERADA == 1).Sum(s => s.IMPORTE);
                    this.OpGravadas = Math.Round((Convert.ToDecimal(((double)TotalDoc - (double)OpExoneradas) / 1.18)), 2, MidpointRounding.ToEven);
                    this.DescDoc = this.DataDocumentoElectronico.Sum(w => w.DESCUENTO);
                    this.DescSinIgvDoc = Math.Round((Convert.ToDecimal((double)DescDoc / 1.18)), 2, MidpointRounding.ToEven);
                    this.IgvDoc = Math.Round((Convert.ToDecimal(this.TotalDoc - this.OpGravadas - this.OpExoneradas)), 2, MidpointRounding.ToEven);
                    Data = this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault();
                    this.DataDocumentoElectronico.Remove(this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault());
                    this.DataDocumentoElectronico.Add(Data);
                    this.DataDocumentoElectronico = new ObservableCollection<Data_Documento_Electronico>(DataDocumentoElectronico.OrderBy(i => i.orden));
                }
                else
                {
                    this.DataDocumentoElectronico.Remove(this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault());
                    this.TotalDoc = this.DataDocumentoElectronico.Sum(w => w.IMPORTE);
                    //this.OpGravadas = Math.Round((Convert.ToDecimal((double)TotalDoc / 1.18)), 2, MidpointRounding.ToEven);
                    this.OpExoneradas = this.DataDocumentoElectronico.Where(w => w.EXONERADA == 1).Sum(s => s.IMPORTE);
                    this.OpGravadas = Math.Round((Convert.ToDecimal(((double)TotalDoc - (double)OpExoneradas) / 1.18)), 2, MidpointRounding.ToEven);
                    this.DescDoc = this.DataDocumentoElectronico.Sum(w => w.DESCUENTO);
                    this.DescSinIgvDoc = Math.Round((Convert.ToDecimal((double)DescDoc / 1.18)), 2, MidpointRounding.ToEven);
                    this.IgvDoc = Math.Round((Convert.ToDecimal(this.TotalDoc - this.OpGravadas - this.OpExoneradas)), 2, MidpointRounding.ToEven);
                    this.DataDocumentoElectronico = new ObservableCollection<Data_Documento_Electronico>(DataDocumentoElectronico.OrderBy(i => i.orden));
                }
            }
            else
            {
                decimal cant = this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().CANTIDAD;
                if (cant > 1)
                {
                    Data_Documento_Electronico Data = new Data_Documento_Electronico();
                    this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().CANTIDAD = this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().CANTIDAD - 1;
                    this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().IMPORTE = Math.Round((Convert.ToDecimal(this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().PRECIO_UNI * this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().CANTIDAD)), 2, MidpointRounding.ToEven);
                    this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().IMPORTE = Math.Round((Convert.ToDecimal(this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().IMPORTE - this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault().DESCUENTO)), 2, MidpointRounding.ToEven);
                    this.TotalDoc = this.DataDocumentoElectronico.Sum(w => w.IMPORTE);
                    //this.OpGravadas = Math.Round((Convert.ToDecimal((double)TotalDoc / 1.18)), 2, MidpointRounding.ToEven);
                    this.OpExoneradas = this.DataDocumentoElectronico.Where(w => w.EXONERADA == 1).Sum(s => s.IMPORTE);
                    this.OpGravadas = Math.Round((Convert.ToDecimal(((double)TotalDoc - (double)OpExoneradas) / 1.18)), 2, MidpointRounding.ToEven);
                    this.DescDoc = this.DataDocumentoElectronico.Sum(w => w.DESCUENTO);
                    this.DescSinIgvDoc = Math.Round((Convert.ToDecimal((double)DescDoc / 1.18)), 2, MidpointRounding.ToEven);
                    this.IgvDoc = Math.Round((Convert.ToDecimal(this.TotalDoc - this.OpGravadas - this.OpExoneradas)), 2, MidpointRounding.ToEven);
                    Data = this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault();
                    this.DataDocumentoElectronico.Remove(this.DataDocumentoElectronico.Where(w => w.ID == (int)id).FirstOrDefault());
                    this.DataDocumentoElectronico.Add(Data);
                    this.DataDocumentoElectronico = new ObservableCollection<Data_Documento_Electronico>(DataDocumentoElectronico.OrderBy(i => i.orden));
                }
                else
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe haber al menos un item en categoria", 2);
                    return;
                }
                
            }
            
        }
        public void Cancelar()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Descripcion, int Reserved);
        public string Estado { get; set; }
        
        public async void BuscaCliente()
        {   
            string nombrecompleto;
            bool internet = true; //negFuncionGlobal.ValidarInternet();
            ServiceReference1.sosfoodSoapClient vf = new ServiceReference1.sosfoodSoapClient();
            var cadena = this.RucCliente;
            if (cadena.Trim() != "")
            {
                if (DataCliente != null)
                {
                    if (DataCliente.Where(c => c.ndoc == cadena).Count() != 0)
                    {
                        this.IdCLiente = DataCliente.Where(c => c.ndoc == cadena).FirstOrDefault().idcli.ToString();
                        this.RucCliente = DataCliente.Where(c => c.ndoc == cadena).FirstOrDefault().ndoc;
                        this.RazonCliente = DataCliente.Where(c => c.ndoc == cadena).FirstOrDefault().denominacion;
                        this.CorreoCliente = DataCliente.Where(c => c.ndoc == cadena).FirstOrDefault().corcli;
                        this.TelefonoCliente = DataCliente.Where(c => c.ndoc == cadena).FirstOrDefault().telcli;
                        this.DireccionCliente = DataCliente.Where(c => c.ndoc == cadena).FirstOrDefault().dircli;
                        return;
                    }
                }
            }
            else
            {
                return;
            }
            int Desc;

            //string verificar = InternetGetConnectedState(out Desc, 0).ToString();
            if (this.tipDoc == null)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Seleccione un Tipo de Documento", 2);
                return;
            }
            internet = true;
            if (internet)
            {
                if (this.tipDoc.ToString() == "1")
                {
                    if (cadena != "" && cadena.Length == 8)
                    {
                        try
                        {
                            nombrecompleto = vf.ConsultaDNI(this.RucCliente);

                            listo = true;
                            string[] partes = nombrecompleto.Split(' ');
                            string result = partes[0] + ' ' + partes[1];
                            string result1 = partes[2] + ' ' + partes[3];
                            this.RazonCliente = result1 + " " + result;
                            this.CorreoCliente = "";
                            this.TelefonoCliente = "";
                            this.DireccionCliente = "";
                        }
                        catch (Exception ex)
                        {
                            //negFuncionGlobal.Mensaje("SOS-FOOD - Error", "El nro documento no se encontro. Vuelva a intentarlo o ingrese manulamente.", 3);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else if (this.tipDoc.ToString() == "6")
                {
                    if (cadena != "" && cadena.Length == 11)
                    {
                        try
                        {
                            string valor = "";
                            int _existe = 0;


                            ServiceReference1.ClsClienteConsultaEN cn;
                            cn = vf.ConsultaRuc("0a682fbe-009d-4758-aad1-2ff1092ab7c2-838d6cd5-620a-4add-9632-a3b37c5ae216", this.RucCliente);
                            this.RazonCliente = cn.nombre_o_razon_social;
                            this.DireccionCliente = cn.direccion_completa;
                            this.CorreoCliente = "";
                            this.TelefonoCliente = "";

                            valor = cn.estado_del_contribuyente;

                        }
                        catch (Exception ex)
                        {
                            //negFuncionGlobal.Mensaje("SOS-FOOD - Error", "El nro documento no se encontro. Vuelva a intentarlo o ingrese manulamente.", 3);
                            return;
                        }

                    }
                    else
                    {
                        return;
                    }


                }
            }
            else
            {
                string Estado = "Estimado Cliente:\n" +
                    "actualmente usted no cuenta con conexión a internet por favor registre manualmente los datos de su cliente.";
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", Estado, 2);
            }
        }
        public int cc { get; set; } = 0;
        public bool listo { get; set; } = false;
        private void contar(object sender, System.Timers.ElapsedEventArgs e) {
            cc = cc + 1;
            if (cc == 1 || listo == true) {
                //Matar el proceso de consulta DNI
                foreach (Process proceso in Process.GetProcesses()) {
                    if(proceso.ProcessName == "taskhostw")
                    {
                        proceso.Kill();
                        negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Tiempo de espera agotado", 2);
                        cc = 0;
                        //timer2.Stop();
                    }
                }
            }
        }
        DataTable detalle_ticket = new DataTable();
        public void factura_detalle()
        {
            detalle_ticket.Columns.Add("id_det_pedido");
            detalle_ticket.Columns.Add("ordenItem");
            detalle_ticket.Columns.Add("codigoProductoItem");
            detalle_ticket.Columns.Add("descripcionItem");
            detalle_ticket.Columns.Add("unidadMedidaItem");
            detalle_ticket.Columns.Add("cantidadItem");
            detalle_ticket.Columns.Add("valorUnitarioSinIgv");
            detalle_ticket.Columns.Add("precioUnitarioConIgv");
            detalle_ticket.Columns.Add("codTipoPrecioVtaUnitarioItem");
            detalle_ticket.Columns.Add("importeIGVItem");
            detalle_ticket.Columns.Add("codigoAfectacionIGVItem");
            detalle_ticket.Columns.Add("descuentoItem");
            detalle_ticket.Columns.Add("valorVentaItem");
            detalle_ticket.Columns.Add("_descuentoItem");
            detalle_ticket.Columns.Add("_precioUnitario");
            detalle_ticket.Columns.Add("_valorVentaSinIGV");
            detalle_ticket.Columns.Add("montoReferenciaItem");
            detalle_ticket.Columns.Add("montoReferencialUnitarioItem");
            detalle_ticket.Columns.Add("clasificacionProductoItem");
            detalle_ticket.Columns.Add("montoTotalItem");
            detalle_ticket.Columns.Add("cantidadBolsasItem");
            detalle_ticket.Columns.Add("montoUnitarioBolsasItem");
            detalle_ticket.Columns.Add("importeBolsasItem");
            detalle_ticket.Columns.Add("opGratuitas");
            detalle_ticket.Columns.Add("opExoneradas");
            detalle_ticket.Columns.Add("ISC");
        }
        public ClsRespuestaTicketElectronico _fileReader(string ruta)
        {
            string fileReader ="";
            fileReader = File.ReadAllText(ruta);
            ClsRespuestaTicketElectronico respuesta_fe = new ClsRespuestaTicketElectronico();
            respuesta_fe = JsonConvert.DeserializeObject<ClsRespuestaTicketElectronico>(fileReader);
            return respuesta_fe;
        }
        
        public async void GeneraFE()
        {
            double dias = (DateTime.Now - this.Fecha).TotalDays;
            if (dias > 7) {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "No es posible emitir un comprobante con mas de 7 dias de antiguedad", 2);
                return;
            }
            if (dias < 0) {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "No es posible emitir un comprobante con fecha mayor al de hoy", 2);
                return;
            }
            BotonImprimir = "False";
            if (DataDocumentoElectronico == null)
            {
                BotonImprimir = "True";
                return;
            }
            if (DataDocumentoElectronico.Count == 0)
            {
                BotonImprimir = "True";
                return;
            }
            int idDocElectronico = 0;
           
            copia = false;
            decimal rc = negParametros.RC();
            decimal igv = negParametros.IGV();
            decimal bp = negParametros.BP();
            string tipOperacion = negParametros.tipOperacion(); //S
            string codLocalEmisor = negParametros.codLocalEmisor(); //S
            string ublVersionId = negParametros.ublVersionId(); //S
            string customizationId = negParametros.customizationId(); //S
            string rutaFacturacion = negParametros.rutaFacturacion(); //S
            double d = (double)(igv + rc) + 1;
            decimal total = 0;


            sfsCabecera cabecera = new sfsCabecera();
            List<sfsDetalle> detalles = new List<sfsDetalle>();
            String linea_detalle = "";
            String h, m, s;
            h = DateTime.Now.Hour.ToString("D2");
            m = DateTime.Now.Minute.ToString("D2");
            s = DateTime.Now.Second.ToString("D2");

            sfsDetalle detalle = new sfsDetalle();
            sfsTributos tributo = new sfsTributos();
            sfsLeyendas leyendas = new sfsLeyendas();
            StringBuilder texto_cabecera = new StringBuilder();
            StringBuilder texto_detalle = new StringBuilder();
            StringBuilder texto_tri = new StringBuilder();

            List<String> aDetalle = new List<string>();
            if (tipDoc == null)
            {
                BotonImprimir = "True";
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe seleccionar un tipo de documento", 2);
                return;
            }
            if (tipDoc.ToString().Trim() == "")
            {
                BotonImprimir = "True";
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe seleccionar un tipo de documento", 2);
                return;
            }
            if (id == 2)
            {
                if (tipDoc.ToString().Trim() != "6")
                {
                    BotonImprimir = "True";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe seleccionar un tipo de documento valido.", 2);
                    return;
                }
                if (this.RucCliente.Trim() == "")
                {
                    BotonImprimir = "True";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion","Ingrese numero documento",2);
                    return;
                }
                if (this.RucCliente.Trim().Length != 11)
                {
                    BotonImprimir = "True";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese un numero de documento valido", 2);
                    return;
                }
                int result;
                if (this.RazonCliente.Trim() == "")
                {
                    BotonImprimir = "True";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese razon social", 2);
                    return;
                }
            }
            bool retorno = false;
            string numero = "";
            bool bolsa = true;
            int ordenItem = 0;

            decimal cabecera_valorventabruto = 0;
            decimal cabecera_descuento = 0;
            decimal cabecera_valorventatotalgravadas = 0;
            decimal cabecera_valorventatotalexoneradas = 0;
            decimal cabecera_valorventatotal = 0;
            decimal cabecera_igv = 0;
            decimal cabecera_rc = 0;
            decimal VALOR_VENTA_TOTAL = 0;

            
            foreach (var item in this.DataDocumentoElectronico)
            {
                decimal PRECIO_UNITARIO = 0;
                decimal VALOR_UNITARIO = 0;
                decimal VALOR_VENTA_BRUTO = 0;
                decimal DESCUENTO_ITEM = 0;
                decimal DESCUENTO_ITEM_SINIMPUESTO = 0;
                decimal IGV_ITEM = 0;
                decimal RC_ITEM = 0;
                decimal opGratuitas = 0;
                decimal opExoneradas = 0;
                decimal ISC = 0;

                ordenItem += 1;
                total += item.IMPORTE;
                byte BP = item.RC;
                byte EX = item.EXONERADA;

                if (EX == 0 && BP == 0)
                {
                    PRECIO_UNITARIO = item.PRECIO_UNI;
                    VALOR_UNITARIO = (decimal)((double)item.PRECIO_UNI / (double)d);
                    VALOR_VENTA_BRUTO = (decimal)((double)item.PRECIO_UNI / (double)d) * item.CANTIDAD;
                    cabecera_valorventabruto += VALOR_VENTA_BRUTO;
                    DESCUENTO_ITEM = (decimal)((double)item.DESCUENTO); 
                    cabecera_descuento += DESCUENTO_ITEM;
                    DESCUENTO_ITEM_SINIMPUESTO = (decimal)((double)DESCUENTO_ITEM / (double)d);// descuento / 1.31 || descuento / 1.18

                    VALOR_VENTA_TOTAL = VALOR_VENTA_BRUTO - DESCUENTO_ITEM_SINIMPUESTO;
                    cabecera_valorventatotalgravadas += (BP == 0 && EX == 0) ? VALOR_VENTA_TOTAL : 0;
                    cabecera_valorventatotal += VALOR_VENTA_TOTAL;

                    IGV_ITEM = VALOR_VENTA_TOTAL * igv;
                    cabecera_igv += IGV_ITEM;

                    RC_ITEM = VALOR_VENTA_TOTAL * rc;
                    ISC = Math.Round(RC_ITEM, 2, MidpointRounding.ToEven); 
                    cabecera_rc += Math.Round(RC_ITEM, 2, MidpointRounding.ToEven);
                }
                else if (EX == 1 && BP == 0)
                {
                    opExoneradas = item.PRECIO_UNI * item.CANTIDAD;
                    cabecera_valorventatotalexoneradas += item.PRECIO_UNI * item.CANTIDAD;
                    cabecera_valorventatotal += cabecera_valorventatotalexoneradas;
                }
                else if (BP == 1 && EX == 0) {
                    //VALOR_VENTA_TOTAL
                }

                decimal precioUnitarioConIgv = item.PRECIO_UNI;
                //decimal descuentoItem = Math.Round((decimal)((double)item.DESCUENTO / d), 2, MidpointRounding.ToEven); // descuento / 1.31 || descuento / 1.18
                

                string codigoProductoItem = item.DP_ID_CARTA;
                string descripcionItem = item.DESCRIPCION;
                string unidadMedidaItem = item.TM_DESC;
                decimal cantidadItem = item.CANTIDAD;
                decimal valorUnitarioSinIgv = ((decimal)((double)item.PRECIO_UNI / d)); // http://blog.pucp.edu.pe/blog/contribuyente/2018/07/30/recargo-al-consumo/
                string codTipoPrecioVtaUnitarioItem = "01";
                decimal rc_item = 0;
                if (rc > 0)
                {
                    rc_item = Math.Round((decimal)((double)item.PRECIO_UNI / (double)rc), 2);
                }
                else {
                    rc_item = 0;
                }
                

                decimal valorVentaItem = (decimal)((double)item.IMPORTE / d);
                decimal importeIGVItem = valorVentaItem * igv;
                string codigoAfectacionIGVItem = "";
                if(item.IMPORTE <= 0)
                {
                    codigoAfectacionIGVItem = "15";
                }
                else
                {
                    codigoAfectacionIGVItem = "10";
                }
                decimal descuentoItem = (decimal)((double)item.DESCUENTO / d);
                decimal _descuentoItem = item.DESCUENTO;
                decimal _precioUnitario = item.PRECIO_UNI;
                decimal _valorVentaSinIGV = item.IMPORTE;
                decimal montoReferenciaItem = 0;
                decimal montoReferencialUnitarioItem = 0;
                string clasificacionProductoItem = item.CLASIFICACION_PRODUCTO_ITEM;
                decimal montoTotalItem = item.IMPORTE;
                decimal cantidadBolsasItem = 0;
                decimal montoUnitarioBolsasItem = 0;
                decimal importeBolsasItem = 0;
                if (_valorVentaSinIGV == 0)
                {
                    montoReferenciaItem = item.IMPORTE;
                    montoReferencialUnitarioItem = item.PRECIO_UNI;
                }
                if (BP == 1)
                {
                    cantidadBolsasItem = item.CANTIDAD;
                    montoUnitarioBolsasItem = bp;
                    importeBolsasItem = item.CANTIDAD * montoUnitarioBolsasItem;
                    _precioUnitario = item.PRECIO_UNI - bp;
                    _valorVentaSinIGV = item.IMPORTE - importeBolsasItem;
                    VALOR_VENTA_TOTAL = item.IMPORTE - importeBolsasItem;
                    if (_valorVentaSinIGV == 0)
                    {
                        montoReferenciaItem = importeBolsasItem;
                        opGratuitas = montoReferenciaItem;
                        montoReferencialUnitarioItem = bp;
                    }
                }
                _valorVentaSinIGV = Math.Round((decimal)((double)_valorVentaSinIGV / d), 2, MidpointRounding.ToEven);
                //detalle_ticket.Rows.Add(item.ID,ordenItem, codigoProductoItem, descripcionItem, unidadMedidaItem, cantidadItem, valorUnitarioSinIgv, 
                //    precioUnitarioConIgv, codTipoPrecioVtaUnitarioItem, importeIGVItem, codigoAfectacionIGVItem, descuentoItem,
                //   valorVentaItem, _descuentoItem, _precioUnitario, _valorVentaSinIGV, montoReferenciaItem, montoReferencialUnitarioItem, 
                //   clasificacionProductoItem, montoTotalItem, cantidadBolsasItem, montoUnitarioBolsasItem, importeBolsasItem);

                detalle_ticket.Rows.Add(item.ID, ordenItem, codigoProductoItem, descripcionItem, unidadMedidaItem, cantidadItem, VALOR_UNITARIO,
                    PRECIO_UNITARIO, codTipoPrecioVtaUnitarioItem, IGV_ITEM, codigoAfectacionIGVItem, DESCUENTO_ITEM_SINIMPUESTO,
                   valorVentaItem, DESCUENTO_ITEM, PRECIO_UNITARIO, _valorVentaSinIGV, montoReferenciaItem, montoReferencialUnitarioItem,
                   clasificacionProductoItem, montoTotalItem, cantidadBolsasItem, montoUnitarioBolsasItem, importeBolsasItem, opGratuitas, opExoneradas, ISC);

                //detalle
                if (detalle.mtoValorVentaItem > 0) {
                    aDetalle.Clear();
                    aDetalle.Add(detalle.codUnidadMedida);
                    aDetalle.Add(detalle.ctdUnidadItem.ToString());
                    aDetalle.Add(detalle.codProducto);
                    aDetalle.Add(detalle.codProductoSUNAT);
                    aDetalle.Add(detalle.desItem);
                    aDetalle.Add(detalle.mtoValorUnitario.ToString("N2"));
                    aDetalle.Add(detalle.sumTotTributosItem.ToString("N2"));
                    aDetalle.Add(detalle.codTriIGV);
                    aDetalle.Add(detalle.mtoIgvItem.ToString("N2"));
                    aDetalle.Add(detalle.mtoBaseIgvItem.ToString("N2"));
                    aDetalle.Add(detalle.nomTributoIgvItem);
                    aDetalle.Add(detalle.codTipTributoIgvItem);
                    aDetalle.Add(detalle.tipAfeIGV);
                    aDetalle.Add(detalle.porIgvItem.ToString("N2"));
                    aDetalle.Add(detalle.codTriISC);
                    aDetalle.Add(detalle.mtoIscItem.ToString("N2"));
                    aDetalle.Add(detalle.mtoBaseIscItem);
                    aDetalle.Add(detalle.nomTributoIscItem);
                    aDetalle.Add(detalle.codTipTributoIscItem);
                    aDetalle.Add(detalle.tipSisISC);
                    aDetalle.Add(detalle.porIscItem.ToString("N2"));
                    aDetalle.Add(detalle.codTriOtroItem);
                    aDetalle.Add(detalle.mtoTriOtroItem.ToString("N2"));
                    aDetalle.Add(detalle.mtoBaseTriOtroItem.ToString("N2"));
                    aDetalle.Add(detalle.nomTributoIOtroItem);
                    aDetalle.Add(detalle.codTipTributoIOtroItem);
                    aDetalle.Add(detalle.porTriOtroItem);
                    aDetalle.Add(detalle.codTriIcbper);
                    aDetalle.Add(detalle.mtoTriIcbperItem.ToString());
                    aDetalle.Add(detalle.ctdBolsasTriIcbperItem.ToString());
                    aDetalle.Add(detalle.nomTributoIcbperItem);
                    aDetalle.Add(detalle.codTipTributoIcbperItem);
                    aDetalle.Add(detalle.mtoTriIcbperUnidad.ToString());
                    aDetalle.Add(detalle.mtoPrecioVentaUnitario.ToString("N2"));
                    aDetalle.Add(detalle.mtoValorVentaItem.ToString("N2"));
                    aDetalle.Add(detalle.mtoValorReferencialUnitario.ToString("N2") + "\n");
                    linea_detalle = linea_detalle + String.Join("|", aDetalle.ToArray());
                }
            }

            tributo.ideTributo = "1000";
            tributo.nomTributo = "IGV";
            tributo.codTipTributo = "VAT";
            tributo.mtoBaseImponible = TotalDoc - IgvDoc;
            tributo.mtoTributo = IgvDoc;

            List<String> aTributo = new List<string>();
            aTributo.Add(tributo.ideTributo);
            aTributo.Add(tributo.nomTributo);
            aTributo.Add(tributo.codTipTributo);
            aTributo.Add(tributo.mtoBaseImponible.ToString());
            aTributo.Add(tributo.mtoTributo.ToString());

            String linea_tributo = String.Join("|", aTributo.ToArray());

            decimal totalValorVentaItem = 0;
            decimal totalImporteIGVItem = 0;
            decimal totalDescuentoItem = 0;
            decimal totalValorVentaConIGV = 0;
            decimal sumatoriaImpuestoBolsas = 0;
            decimal totalOPGratuitas = 0;
            decimal descuentos_x_item = 0;
            for(int i = 0;i <= detalle_ticket.Rows.Count -1 ; i++)
            {
                totalValorVentaItem += Convert.ToDecimal(detalle_ticket.Rows[i]["valorVentaItem"]);
                totalImporteIGVItem += Convert.ToDecimal(detalle_ticket.Rows[i]["importeIGVItem"]);
                totalDescuentoItem += Convert.ToDecimal(detalle_ticket.Rows[i]["descuentoItem"]);
                totalValorVentaConIGV += Convert.ToDecimal(detalle_ticket.Rows[i]["valorVentaItem"]);
                sumatoriaImpuestoBolsas += Convert.ToDecimal(detalle_ticket.Rows[i]["importeBolsasItem"]);
                totalOPGratuitas += Convert.ToDecimal(detalle_ticket.Rows[i]["montoReferenciaItem"]);
                descuentos_x_item += Convert.ToDecimal(detalle_ticket.Rows[i]["_descuentoItem"]);
            }
            decimal descuentoGlobal = 0;
            this.DescDoc = DescDoc - descuentos_x_item;

            if ((double)this.DescDoc == 0)
            {
                //descuentoGlobal = Convert.ToDecimal(this.DescDoc) - sumatoriaImpuestoBolsas;
                descuentoGlobal = 0;
            }
            else
            {
                descuentoGlobal = Math.Round((decimal)((double)this.DescDoc ), 2, MidpointRounding.ToEven);
                //descuentoGlobal = descuentoGlobal - descuentos_x_item;
            }

            //decimal totalOPGravadas = Math.Round((decimal)(((double)this.TotalDoc - (double)sumatoriaImpuestoBolsas - (double)OpExoneradas) / d), 2, MidpointRounding.ToEven);
            //decimal totalOPGravadas = Math.Round((decimal)(((double)total - (double)sumatoriaImpuestoBolsas - (double)OpExoneradas) / 1.18), 2, MidpointRounding.ToEven); //22/10/2020
            decimal total_sinbpexo = total - sumatoriaImpuestoBolsas - OpExoneradas - descuentoGlobal; //28102020
            //decimal total_sinbpexo = total - sumatoriaImpuestoBolsas - OpExoneradas;
            decimal totalOPGravadas = (decimal)((double)total_sinbpexo / d); //22/10/2020
            decimal recargoconsumo = totalOPGravadas * rc;
            decimal sumatoriaIGV = total_sinbpexo - totalOPGravadas - recargoconsumo;
            decimal icbper = sumatoriaImpuestoBolsas;
            decimal totalOpExoneradas = OpExoneradas;

            double recrgcon = 0.00;

            string MONTO_LETRAS = negDocElectronico.MONTO_LETRAS(this.TotalDoc);
            if (this.RucCliente.Trim() == "")
            {
                this.RucCliente = "0";
                this.tipDoc = "0";
            }
            if(this.RazonCliente.Trim() == "")
            {
                this.RazonCliente = "PUBLICO GENERAL";
            }
            if (tipDoc == null)
            {
                this.tipDoc = "0";
            }
            try
            {
                idDocElectronico = negDocElectronico.SetDocElectronico(sumatoriaImpuestoBolsas, 0, this.DireccionCliente, recargoconsumo, rc * 100, this.TelefonoCliente, "0101", 
                    idPed.ToString(), "PE", this.TotalDoc, this.CorreoCliente, this.RucEmpr, "6",this.TipDocElectr.Where(t => t.ID == id).ToList().FirstOrDefault().VALOR_TIPO_DOC, 
                    this.serie, this.Fecha, this.idtmoneda, this.RucCliente, this.RazonCliente,this.tipDoc, totalOPGravadas,OpExoneradas, 0, totalOPGratuitas, sumatoriaIGV, recargoconsumo,
                    this.TotalDoc, descuentos_x_item, descuentoGlobal, MONTO_LETRAS, ConfigurationManager.AppSettings["NombreEquipo"].ToString(), detalle_ticket);
                if (idDocElectronico != 0)
                {
                    cabecera.tipOperacion = tipOperacion;
                    cabecera.fecEmision = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    cabecera.horEmision = h + ":" + m + ":" + s;
                    cabecera.fecVencimiento = "-";
                    cabecera.codLocalEmisor = codLocalEmisor;
                    cabecera.tipDocUsuario = tipDoc;
                    cabecera.numDocUsuario = (RucCliente == "") ? "00000000" : RucCliente;
                    cabecera.rznSocialUsuario = (RazonCliente == "") ? "SIN DNI" : RazonCliente;
                    cabecera.tipMoneda = idtmoneda;
                    cabecera.sumTotTributos = Math.Round(totalOPGravadas, 2, MidpointRounding.ToEven);
                    cabecera.sumTotValVenta = Math.Round(sumatoriaIGV, 2, MidpointRounding.ToEven);
                    cabecera.sumPrecioVenta = TotalDoc;
                    cabecera.sumDescTotal = DescDoc;
                    cabecera.sumOtrosCargos = Math.Round(recargoconsumo, 2, MidpointRounding.ToEven);

                    if (cabecera.sumTotTributos + cabecera.sumTotValVenta + cabecera.sumOtrosCargos == total) {

                    } else if (cabecera.sumTotTributos + cabecera.sumTotValVenta + cabecera.sumOtrosCargos > total) {
                        cabecera.sumOtrosCargos = Convert.ToDecimal(Convert.ToDouble(cabecera.sumOtrosCargos) - 0.01);
                    }
                    else {
                        cabecera.sumOtrosCargos = Convert.ToDecimal(Convert.ToDouble(cabecera.sumOtrosCargos) + 0.01);
                    }

                    List<String> aCabecera = new List<string>();
                    aCabecera.Add(cabecera.tipOperacion);
                    aCabecera.Add(cabecera.fecEmision);
                    aCabecera.Add(cabecera.horEmision);
                    aCabecera.Add(cabecera.fecVencimiento);
                    aCabecera.Add(cabecera.codLocalEmisor);
                    aCabecera.Add(cabecera.tipDocUsuario);
                    aCabecera.Add(cabecera.numDocUsuario);
                    aCabecera.Add(cabecera.rznSocialUsuario);
                    aCabecera.Add(cabecera.tipMoneda);
                    aCabecera.Add(cabecera.sumTotTributos.ToString());
                    aCabecera.Add(cabecera.sumTotValVenta.ToString());
                    aCabecera.Add(cabecera.sumPrecioVenta.ToString());
                    aCabecera.Add(cabecera.sumDescTotal.ToString());
                    aCabecera.Add(cabecera.sumOtrosCargos.ToString());
                    aCabecera.Add(cabecera.sumTotalAnticipos.ToString());
                    aCabecera.Add(cabecera.sumImpVenta.ToString());
                    aCabecera.Add(cabecera.ublVersionId);
                    aCabecera.Add(cabecera.customizationId);

                    //String linea_cabecera = negDocElectronico.getCabecera(idDocElectronico);
                    //linea_detalle = negDocElectronico.getDetalle(idDocElectronico);
                    //linea_tributo = negDocElectronico.getTributo(idDocElectronico);

                    //string str_nombre_fichero = this.RucEmpr + "-" + this.TipDocElectr.Where(j => j.ID == id).ToList().FirstOrDefault().VALOR_TIPO_DOC + "-" + this.serie;

                    //string fichero2 = rutaFacturacion + str_nombre_fichero + ".cab";
                    //string ficheroDetalle = rutaFacturacion + str_nombre_fichero + ".det";
                    //string ficherotri = rutaFacturacion + str_nombre_fichero + ".tri";

                    //string fichero = negParametros.RutaFePrueba() + str_nombre_fichero + ".INPUT.txt";
                    //System.IO.StreamWriter a = new System.IO.StreamWriter(fichero2);
                    //a.WriteLine(linea_cabecera);
                    //a.Close();

                    //System.IO.StreamWriter b = new System.IO.StreamWriter(ficheroDetalle);
                    //b.WriteLine(linea_detalle);
                    //b.Close();

                    //System.IO.StreamWriter c = new System.IO.StreamWriter(ficherotri);
                    //c.WriteLine(linea_tributo);
                    //c.Close();

                    bool cliente = negDocElectronico.VerificaCliente(this.RucCliente, this.RazonCliente, Convert.ToInt32(this.tipDoc), this.DireccionCliente, this.TelefonoCliente, this.CorreoCliente);
                    bool result1 = false;
                    string caja2 = ConfigurationManager.AppSettings["caja2"].ToString();
                    if (caja2 == "SI")
                    {
                        result1 = negDocElectronico.ActualizarCorrelaticoDocElectronico(this.SerieNumeroDocumento.Where(t => t.VALOR == (string)this.TipDocElectr.Where(j => j.ID == id).ToList().LastOrDefault().VALOR_TIPO_DOC).ToList().LastOrDefault().SERIE);
                    }
                    else
                    {
                        result1 = negDocElectronico.ActualizarCorrelaticoDocElectronico(this.SerieNumeroDocumento.Where(t => t.VALOR == (string)this.TipDocElectr.Where(j => j.ID == id).ToList().FirstOrDefault().VALOR_TIPO_DOC).ToList().FirstOrDefault().SERIE);
                    }
                    if (!result1)
                    {
                        BotonImprimir = "True";
                        bool result = negDocElectronico.EliminarDocElectronico(idDocElectronico);
                    }
                    else
                    {
                        imprimir_CPE(idDocElectronico);
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                }
                else
                {
                    BotonImprimir = "True";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Error", "Error al insertar el documento a la base de datos. Intentelo nuevamente.", 3);
                    bool result = negDocElectronico.EliminarDocElectronico(idDocElectronico);
                    detalle_ticket = new DataTable();
                    factura_detalle();
                }
            }
            catch (Exception ex)
            {
                BotonImprimir = "True";
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                negFuncionGlobal.Mensaje("SOS-FOOD - Error", "Error al insertar el documento a la base de datos. Intentelo nuevamente.", 3);
                bool result = negDocElectronico.EliminarDocElectronico(idDocElectronico);
                detalle_ticket = new DataTable();
                factura_detalle();
            }
        }
        int qrBackColor = Color.FromArgb(255, 255, 255, 255).ToArgb();
        int qrForeColor = Color.FromArgb(255, 0, 0, 0).ToArgb();

        public async void GeneraFECopia()
        {
            double dias = (DateTime.Now - this.Fecha).TotalDays;
            if (dias > 7)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "No es posible emitir un comprobante con mas de 7 dias de antiguedad", 2);
                return;
            }
            if (dias < 0)
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "No es posible emitir un comprobante con fecha mayor al de hoy", 2);
                return;
            }
            BotonImprimir = "False";
            if (DataDocumentoElectronico == null)
            {
                BotonImprimir = "True";
                return;
            }
            if (DataDocumentoElectronico.Count == 0)
            {
                BotonImprimir = "True";
                return;
            }
            int idDocElectronico = 0;

            copia = true;
            decimal rc = negParametros.RC();
            decimal igv = negParametros.IGV();
            decimal bp = negParametros.BP();
            string tipOperacion = negParametros.tipOperacion(); //S
            string codLocalEmisor = negParametros.codLocalEmisor(); //S
            string ublVersionId = negParametros.ublVersionId(); //S
            string customizationId = negParametros.customizationId(); //S
            string rutaFacturacion = negParametros.rutaFacturacion(); //S
            double d = (double)(igv + rc) + 1;
            decimal total = 0;


            sfsCabecera cabecera = new sfsCabecera();
            List<sfsDetalle> detalles = new List<sfsDetalle>();
            String linea_detalle = "";
            String h, m, s;
            h = DateTime.Now.Hour.ToString("D2");
            m = DateTime.Now.Minute.ToString("D2");
            s = DateTime.Now.Second.ToString("D2");

            sfsDetalle detalle = new sfsDetalle();
            sfsTributos tributo = new sfsTributos();
            sfsLeyendas leyendas = new sfsLeyendas();
            StringBuilder texto_cabecera = new StringBuilder();
            StringBuilder texto_detalle = new StringBuilder();
            StringBuilder texto_tri = new StringBuilder();

            List<String> aDetalle = new List<string>();
            if (tipDoc == null)
            {
                BotonImprimir = "True";
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe seleccionar un tipo de documento", 2);
                return;
            }
            if (tipDoc.ToString().Trim() == "")
            {
                BotonImprimir = "True";
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe seleccionar un tipo de documento", 2);
                return;
            }
            if (id == 2)
            {
                if (tipDoc.ToString().Trim() != "6")
                {
                    BotonImprimir = "True";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Debe seleccionar un tipo de documento valido.", 2);
                    return;
                }
                if (this.RucCliente.Trim() == "")
                {
                    BotonImprimir = "True";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese numero documento", 2);
                    return;
                }
                if (this.RucCliente.Trim().Length != 11)
                {
                    BotonImprimir = "True";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese un numero de documento valido", 2);
                    return;
                }
                int result;
                if (this.RazonCliente.Trim() == "")
                {
                    BotonImprimir = "True";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Ingrese razon social", 2);
                    return;
                }
            }
            bool retorno = false;
            string numero = "";
            bool bolsa = true;
            int ordenItem = 0;

            decimal cabecera_valorventabruto = 0;
            decimal cabecera_descuento = 0;
            decimal cabecera_valorventatotalgravadas = 0;
            decimal cabecera_valorventatotalexoneradas = 0;
            decimal cabecera_valorventatotal = 0;
            decimal cabecera_igv = 0;
            decimal cabecera_rc = 0;
            decimal VALOR_VENTA_TOTAL = 0;


            foreach (var item in this.DataDocumentoElectronico)
            {
                decimal PRECIO_UNITARIO = 0;
                decimal VALOR_UNITARIO = 0;
                decimal VALOR_VENTA_BRUTO = 0;
                decimal DESCUENTO_ITEM = 0;
                decimal DESCUENTO_ITEM_SINIMPUESTO = 0;
                decimal IGV_ITEM = 0;
                decimal RC_ITEM = 0;
                decimal opGratuitas = 0;
                decimal opExoneradas = 0;
                decimal ISC = 0;

                ordenItem += 1;
                total += item.IMPORTE;
                byte BP = item.RC;
                byte EX = item.EXONERADA;

                if (EX == 0 && BP == 0)
                {
                    PRECIO_UNITARIO = item.PRECIO_UNI;
                    VALOR_UNITARIO = (decimal)((double)item.PRECIO_UNI / (double)d);
                    VALOR_VENTA_BRUTO = (decimal)((double)item.PRECIO_UNI / (double)d) * item.CANTIDAD;
                    cabecera_valorventabruto += VALOR_VENTA_BRUTO;
                    DESCUENTO_ITEM = (decimal)((double)item.DESCUENTO);
                    cabecera_descuento += DESCUENTO_ITEM;
                    DESCUENTO_ITEM_SINIMPUESTO = (decimal)((double)DESCUENTO_ITEM / (double)d);// descuento / 1.31 || descuento / 1.18

                    VALOR_VENTA_TOTAL = VALOR_VENTA_BRUTO - DESCUENTO_ITEM_SINIMPUESTO;
                    cabecera_valorventatotalgravadas += (BP == 0 && EX == 0) ? VALOR_VENTA_TOTAL : 0;
                    cabecera_valorventatotal += VALOR_VENTA_TOTAL;

                    IGV_ITEM = VALOR_VENTA_TOTAL * igv;
                    cabecera_igv += IGV_ITEM;

                    RC_ITEM = VALOR_VENTA_TOTAL * rc;
                    ISC = Math.Round(RC_ITEM, 2, MidpointRounding.ToEven);
                    cabecera_rc += Math.Round(RC_ITEM, 2, MidpointRounding.ToEven);
                }
                else if (EX == 1 && BP == 0)
                {
                    opExoneradas = item.PRECIO_UNI * item.CANTIDAD;
                    cabecera_valorventatotalexoneradas += item.PRECIO_UNI * item.CANTIDAD;
                    cabecera_valorventatotal += cabecera_valorventatotalexoneradas;
                }
                else if (BP == 1 && EX == 0)
                {
                    //VALOR_VENTA_TOTAL
                }

                decimal precioUnitarioConIgv = item.PRECIO_UNI;
                //decimal descuentoItem = Math.Round((decimal)((double)item.DESCUENTO / d), 2, MidpointRounding.ToEven); // descuento / 1.31 || descuento / 1.18


                string codigoProductoItem = item.DP_ID_CARTA;
                string descripcionItem = item.DESCRIPCION;
                string unidadMedidaItem = item.TM_DESC;
                decimal cantidadItem = item.CANTIDAD;
                decimal valorUnitarioSinIgv = ((decimal)((double)item.PRECIO_UNI / d)); // http://blog.pucp.edu.pe/blog/contribuyente/2018/07/30/recargo-al-consumo/
                string codTipoPrecioVtaUnitarioItem = "01";
                decimal rc_item = 0;
                if (rc > 0)
                {
                    rc_item = Math.Round((decimal)((double)item.PRECIO_UNI / (double)rc), 2);
                }
                else
                {
                    rc_item = 0;
                }


                decimal valorVentaItem = (decimal)((double)item.IMPORTE / d);
                decimal importeIGVItem = valorVentaItem * igv;
                string codigoAfectacionIGVItem = "";
                if (item.IMPORTE <= 0)
                {
                    codigoAfectacionIGVItem = "15";
                }
                else
                {
                    codigoAfectacionIGVItem = "10";
                }
                decimal descuentoItem = (decimal)((double)item.DESCUENTO / d);
                decimal _descuentoItem = item.DESCUENTO;
                decimal _precioUnitario = item.PRECIO_UNI;
                decimal _valorVentaSinIGV = item.IMPORTE;
                decimal montoReferenciaItem = 0;
                decimal montoReferencialUnitarioItem = 0;
                string clasificacionProductoItem = item.CLASIFICACION_PRODUCTO_ITEM;
                decimal montoTotalItem = item.IMPORTE;
                decimal cantidadBolsasItem = 0;
                decimal montoUnitarioBolsasItem = 0;
                decimal importeBolsasItem = 0;
                if (_valorVentaSinIGV == 0)
                {
                    montoReferenciaItem = item.IMPORTE;
                    montoReferencialUnitarioItem = item.PRECIO_UNI;
                }
                if (BP == 1)
                {
                    cantidadBolsasItem = item.CANTIDAD;
                    montoUnitarioBolsasItem = bp;
                    importeBolsasItem = item.CANTIDAD * montoUnitarioBolsasItem;
                    _precioUnitario = item.PRECIO_UNI - bp;
                    _valorVentaSinIGV = item.IMPORTE - importeBolsasItem;
                    VALOR_VENTA_TOTAL = item.IMPORTE - importeBolsasItem;
                    if (_valorVentaSinIGV == 0)
                    {
                        montoReferenciaItem = importeBolsasItem;
                        opGratuitas = montoReferenciaItem;
                        montoReferencialUnitarioItem = bp;
                    }
                }
                _valorVentaSinIGV = Math.Round((decimal)((double)_valorVentaSinIGV / d), 2, MidpointRounding.ToEven);
                //detalle_ticket.Rows.Add(item.ID,ordenItem, codigoProductoItem, descripcionItem, unidadMedidaItem, cantidadItem, valorUnitarioSinIgv, 
                //    precioUnitarioConIgv, codTipoPrecioVtaUnitarioItem, importeIGVItem, codigoAfectacionIGVItem, descuentoItem,
                //   valorVentaItem, _descuentoItem, _precioUnitario, _valorVentaSinIGV, montoReferenciaItem, montoReferencialUnitarioItem, 
                //   clasificacionProductoItem, montoTotalItem, cantidadBolsasItem, montoUnitarioBolsasItem, importeBolsasItem);

                detalle_ticket.Rows.Add(item.ID, ordenItem, codigoProductoItem, descripcionItem, unidadMedidaItem, cantidadItem, VALOR_UNITARIO,
                    PRECIO_UNITARIO, codTipoPrecioVtaUnitarioItem, IGV_ITEM, codigoAfectacionIGVItem, DESCUENTO_ITEM_SINIMPUESTO,
                   valorVentaItem, DESCUENTO_ITEM, PRECIO_UNITARIO, _valorVentaSinIGV, montoReferenciaItem, montoReferencialUnitarioItem,
                   clasificacionProductoItem, montoTotalItem, cantidadBolsasItem, montoUnitarioBolsasItem, importeBolsasItem, opGratuitas, opExoneradas, ISC);

                //detalle
                if (detalle.mtoValorVentaItem > 0)
                {
                    aDetalle.Clear();
                    aDetalle.Add(detalle.codUnidadMedida);
                    aDetalle.Add(detalle.ctdUnidadItem.ToString());
                    aDetalle.Add(detalle.codProducto);
                    aDetalle.Add(detalle.codProductoSUNAT);
                    aDetalle.Add(detalle.desItem);
                    aDetalle.Add(detalle.mtoValorUnitario.ToString("N2"));
                    aDetalle.Add(detalle.sumTotTributosItem.ToString("N2"));
                    aDetalle.Add(detalle.codTriIGV);
                    aDetalle.Add(detalle.mtoIgvItem.ToString("N2"));
                    aDetalle.Add(detalle.mtoBaseIgvItem.ToString("N2"));
                    aDetalle.Add(detalle.nomTributoIgvItem);
                    aDetalle.Add(detalle.codTipTributoIgvItem);
                    aDetalle.Add(detalle.tipAfeIGV);
                    aDetalle.Add(detalle.porIgvItem.ToString("N2"));
                    aDetalle.Add(detalle.codTriISC);
                    aDetalle.Add(detalle.mtoIscItem.ToString("N2"));
                    aDetalle.Add(detalle.mtoBaseIscItem);
                    aDetalle.Add(detalle.nomTributoIscItem);
                    aDetalle.Add(detalle.codTipTributoIscItem);
                    aDetalle.Add(detalle.tipSisISC);
                    aDetalle.Add(detalle.porIscItem.ToString("N2"));
                    aDetalle.Add(detalle.codTriOtroItem);
                    aDetalle.Add(detalle.mtoTriOtroItem.ToString("N2"));
                    aDetalle.Add(detalle.mtoBaseTriOtroItem.ToString("N2"));
                    aDetalle.Add(detalle.nomTributoIOtroItem);
                    aDetalle.Add(detalle.codTipTributoIOtroItem);
                    aDetalle.Add(detalle.porTriOtroItem);
                    aDetalle.Add(detalle.codTriIcbper);
                    aDetalle.Add(detalle.mtoTriIcbperItem.ToString());
                    aDetalle.Add(detalle.ctdBolsasTriIcbperItem.ToString());
                    aDetalle.Add(detalle.nomTributoIcbperItem);
                    aDetalle.Add(detalle.codTipTributoIcbperItem);
                    aDetalle.Add(detalle.mtoTriIcbperUnidad.ToString());
                    aDetalle.Add(detalle.mtoPrecioVentaUnitario.ToString("N2"));
                    aDetalle.Add(detalle.mtoValorVentaItem.ToString("N2"));
                    aDetalle.Add(detalle.mtoValorReferencialUnitario.ToString("N2") + "\n");
                    linea_detalle = linea_detalle + String.Join("|", aDetalle.ToArray());
                }
            }

            tributo.ideTributo = "1000";
            tributo.nomTributo = "IGV";
            tributo.codTipTributo = "VAT";
            tributo.mtoBaseImponible = TotalDoc - IgvDoc;
            tributo.mtoTributo = IgvDoc;

            List<String> aTributo = new List<string>();
            aTributo.Add(tributo.ideTributo);
            aTributo.Add(tributo.nomTributo);
            aTributo.Add(tributo.codTipTributo);
            aTributo.Add(tributo.mtoBaseImponible.ToString());
            aTributo.Add(tributo.mtoTributo.ToString());

            String linea_tributo = String.Join("|", aTributo.ToArray());

            decimal totalValorVentaItem = 0;
            decimal totalImporteIGVItem = 0;
            decimal totalDescuentoItem = 0;
            decimal totalValorVentaConIGV = 0;
            decimal sumatoriaImpuestoBolsas = 0;
            decimal totalOPGratuitas = 0;
            decimal descuentos_x_item = 0;
            for (int i = 0; i <= detalle_ticket.Rows.Count - 1; i++)
            {
                totalValorVentaItem += Convert.ToDecimal(detalle_ticket.Rows[i]["valorVentaItem"]);
                totalImporteIGVItem += Convert.ToDecimal(detalle_ticket.Rows[i]["importeIGVItem"]);
                totalDescuentoItem += Convert.ToDecimal(detalle_ticket.Rows[i]["descuentoItem"]);
                totalValorVentaConIGV += Convert.ToDecimal(detalle_ticket.Rows[i]["valorVentaItem"]);
                sumatoriaImpuestoBolsas += Convert.ToDecimal(detalle_ticket.Rows[i]["importeBolsasItem"]);
                totalOPGratuitas += Convert.ToDecimal(detalle_ticket.Rows[i]["montoReferenciaItem"]);
                descuentos_x_item += Convert.ToDecimal(detalle_ticket.Rows[i]["_descuentoItem"]);
            }
            decimal descuentoGlobal = 0;
            this.DescDoc = DescDoc - descuentos_x_item;

            if ((double)this.DescDoc == 0)
            {
                //descuentoGlobal = Convert.ToDecimal(this.DescDoc) - sumatoriaImpuestoBolsas;
                descuentoGlobal = 0;
            }
            else
            {
                descuentoGlobal = Math.Round((decimal)((double)this.DescDoc), 2, MidpointRounding.ToEven);
                //descuentoGlobal = descuentoGlobal - descuentos_x_item;
            }

            //decimal totalOPGravadas = Math.Round((decimal)(((double)this.TotalDoc - (double)sumatoriaImpuestoBolsas - (double)OpExoneradas) / d), 2, MidpointRounding.ToEven);
            //decimal totalOPGravadas = Math.Round((decimal)(((double)total - (double)sumatoriaImpuestoBolsas - (double)OpExoneradas) / 1.18), 2, MidpointRounding.ToEven); //22/10/2020
            decimal total_sinbpexo = total - sumatoriaImpuestoBolsas - OpExoneradas - descuentoGlobal;
            decimal totalOPGravadas = (decimal)((double)total_sinbpexo / d); //22/10/2020
            decimal recargoconsumo = totalOPGravadas * rc;
            decimal sumatoriaIGV = total_sinbpexo - totalOPGravadas - recargoconsumo;
            decimal icbper = sumatoriaImpuestoBolsas;
            decimal totalOpExoneradas = OpExoneradas;

            double recrgcon = 0.00;

            string MONTO_LETRAS = negDocElectronico.MONTO_LETRAS(this.TotalDoc);
            if (this.RucCliente.Trim() == "")
            {
                this.RucCliente = "0";
                this.tipDoc = "0";
            }
            if (this.RazonCliente.Trim() == "")
            {
                this.RazonCliente = "PUBLICO GENERAL";
            }
            if (tipDoc == null)
            {
                this.tipDoc = "0";
            }
            try
            {
                idDocElectronico = negDocElectronico.SetDocElectronico(sumatoriaImpuestoBolsas, 0, this.DireccionCliente, recargoconsumo, rc * 100, this.TelefonoCliente, "0101",
                    idPed.ToString(), "PE", this.TotalDoc, this.CorreoCliente, this.RucEmpr, "6", this.TipDocElectr.Where(t => t.ID == id).ToList().FirstOrDefault().VALOR_TIPO_DOC,
                    this.serie, this.Fecha, this.idtmoneda, this.RucCliente, this.RazonCliente, this.tipDoc, totalOPGravadas, OpExoneradas, 0, totalOPGratuitas, sumatoriaIGV, recargoconsumo,
                    this.TotalDoc, descuentos_x_item, descuentoGlobal, MONTO_LETRAS, ConfigurationManager.AppSettings["NombreEquipo"].ToString(), detalle_ticket);
                if (idDocElectronico != 0)
                {
                    cabecera.tipOperacion = tipOperacion;
                    cabecera.fecEmision = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    cabecera.horEmision = h + ":" + m + ":" + s;
                    cabecera.fecVencimiento = "-";
                    cabecera.codLocalEmisor = codLocalEmisor;
                    cabecera.tipDocUsuario = tipDoc;
                    cabecera.numDocUsuario = (RucCliente == "") ? "00000000" : RucCliente;
                    cabecera.rznSocialUsuario = (RazonCliente == "") ? "SIN DNI" : RazonCliente;
                    cabecera.tipMoneda = idtmoneda;
                    cabecera.sumTotTributos = Math.Round(totalOPGravadas, 2, MidpointRounding.ToEven);
                    cabecera.sumTotValVenta = Math.Round(sumatoriaIGV, 2, MidpointRounding.ToEven);
                    cabecera.sumPrecioVenta = TotalDoc;
                    cabecera.sumDescTotal = DescDoc;
                    cabecera.sumOtrosCargos = Math.Round(recargoconsumo, 2, MidpointRounding.ToEven);

                    if (cabecera.sumTotTributos + cabecera.sumTotValVenta + cabecera.sumOtrosCargos == total)
                    {

                    }
                    else if (cabecera.sumTotTributos + cabecera.sumTotValVenta + cabecera.sumOtrosCargos > total)
                    {
                        cabecera.sumOtrosCargos = Convert.ToDecimal(Convert.ToDouble(cabecera.sumOtrosCargos) - 0.01);
                    }
                    else
                    {
                        cabecera.sumOtrosCargos = Convert.ToDecimal(Convert.ToDouble(cabecera.sumOtrosCargos) + 0.01);
                    }

                    List<String> aCabecera = new List<string>();
                    aCabecera.Add(cabecera.tipOperacion);
                    aCabecera.Add(cabecera.fecEmision);
                    aCabecera.Add(cabecera.horEmision);
                    aCabecera.Add(cabecera.fecVencimiento);
                    aCabecera.Add(cabecera.codLocalEmisor);
                    aCabecera.Add(cabecera.tipDocUsuario);
                    aCabecera.Add(cabecera.numDocUsuario);
                    aCabecera.Add(cabecera.rznSocialUsuario);
                    aCabecera.Add(cabecera.tipMoneda);
                    aCabecera.Add(cabecera.sumTotTributos.ToString());
                    aCabecera.Add(cabecera.sumTotValVenta.ToString());
                    aCabecera.Add(cabecera.sumPrecioVenta.ToString());
                    aCabecera.Add(cabecera.sumDescTotal.ToString());
                    aCabecera.Add(cabecera.sumOtrosCargos.ToString());
                    aCabecera.Add(cabecera.sumTotalAnticipos.ToString());
                    aCabecera.Add(cabecera.sumImpVenta.ToString());
                    aCabecera.Add(cabecera.ublVersionId);
                    aCabecera.Add(cabecera.customizationId);

                    //String linea_cabecera = negDocElectronico.getCabecera(idDocElectronico);
                    //linea_detalle = negDocElectronico.getDetalle(idDocElectronico);
                    //linea_tributo = negDocElectronico.getTributo(idDocElectronico);

                    //string str_nombre_fichero = this.RucEmpr + "-" + this.TipDocElectr.Where(j => j.ID == id).ToList().FirstOrDefault().VALOR_TIPO_DOC + "-" + this.serie;

                    //string fichero2 = rutaFacturacion + str_nombre_fichero + ".cab";
                    //string ficheroDetalle = rutaFacturacion + str_nombre_fichero + ".det";
                    //string ficherotri = rutaFacturacion + str_nombre_fichero + ".tri";

                    ////string fichero = negParametros.RutaFePrueba() + str_nombre_fichero + ".INPUT.txt";
                    //System.IO.StreamWriter a = new System.IO.StreamWriter(fichero2);
                    //a.WriteLine(linea_cabecera);
                    //a.Close();

                    //System.IO.StreamWriter b = new System.IO.StreamWriter(ficheroDetalle);
                    //b.WriteLine(linea_detalle);
                    //b.Close();

                    //System.IO.StreamWriter c = new System.IO.StreamWriter(ficherotri);
                    //c.WriteLine(linea_tributo);
                    //c.Close();

                    bool cliente = negDocElectronico.VerificaCliente(this.RucCliente, this.RazonCliente, Convert.ToInt32(this.tipDoc), this.DireccionCliente, this.TelefonoCliente, this.CorreoCliente);
                    bool result1 = false;
                    string caja2 = ConfigurationManager.AppSettings["caja2"].ToString();
                    if (caja2 == "SI")
                    {
                        result1 = negDocElectronico.ActualizarCorrelaticoDocElectronico(this.SerieNumeroDocumento.Where(t => t.VALOR == (string)this.TipDocElectr.Where(j => j.ID == id).ToList().LastOrDefault().VALOR_TIPO_DOC).ToList().LastOrDefault().SERIE);
                    }
                    else
                    {
                        result1 = negDocElectronico.ActualizarCorrelaticoDocElectronico(this.SerieNumeroDocumento.Where(t => t.VALOR == (string)this.TipDocElectr.Where(j => j.ID == id).ToList().FirstOrDefault().VALOR_TIPO_DOC).ToList().FirstOrDefault().SERIE);
                    }
                    if (!result1)
                    {
                        BotonImprimir = "True";
                        bool result = negDocElectronico.EliminarDocElectronico(idDocElectronico);
                    }
                    else
                    {
                        imprimir_CPE(idDocElectronico);
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                }
                else
                {
                    BotonImprimir = "True";
                    negFuncionGlobal.Mensaje("SOS-FOOD - Error", "Error al insertar el documento a la base de datos. Intentelo nuevamente.", 3);
                    bool result = negDocElectronico.EliminarDocElectronico(idDocElectronico);
                    detalle_ticket = new DataTable();
                    factura_detalle();
                }
            }
            catch (Exception ex)
            {
                BotonImprimir = "True";
                //negFuncionGlobal.Mensaje("SOS-FOOD - Error", ex.Message, 3);
                negFuncionGlobal.Mensaje("SOS-FOOD - Error", "Error al insertar el documento a la base de datos. Intentelo nuevamente.", 3);
                bool result = negDocElectronico.EliminarDocElectronico(idDocElectronico);
                detalle_ticket = new DataTable();
                factura_detalle();
            }
        }
        Neg_Platos negPlatos = new Neg_Platos();
        public void imprimir_CPE(int id_CPE)
        {
            if (negFuncionGlobal.ImpCaja().Trim() == "")
            {
                return;
            }

            DataTable impresora = new DataTable();
            string Modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            impresora = negPlatos.GetImpresoraxDocumento(2); //Comprobante
            for (int imp = 0; imp < impresora.Rows.Count; imp++)
            {
                ObservableCollection<Empresa> dtEmpresa = new ObservableCollection<Empresa>();
                dtEmpresa = negEmpr.GetEmpresa();
                DataTable dt = new DataTable();
                string URL = negParametros.URLEasyfact();
                dt = negDocElectronico.GetDocElectronico(id_CPE);
                Ticket ticket = new Ticket();
                ticket.MaxChar = Convert.ToInt32(negParametros.NroColumnasTicket());
                ticket.MaxCharDescrip = negParametros.margenMaxDescrip();
                if (Convert.ToBoolean(negParametros.logoFacturacion()) == true) {
                    System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(dtEmpresa.FirstOrDefault().empr_logo));
                    ticket.MargenLogo = negParametros.margenLogoTicket();
                    ticket.HeaderImage = x;
                }
                ticket.AddHeaderLine(this.RucEmpr.ToUpper());
                ticket.AddHeaderLine(this.NomEmpr.ToUpper());
                ticket.AddHeaderLine(this.DirEmpr.ToUpper());
                ticket.AddHeaderLine(this.DistEmpr.ToUpper());
                // ticket.AddHeaderLine("JR. CAPAC YUPANQUI NRO. 2730 INT. 101 (A 1 CDRA DE WONG 2 DE MAYO S.I.)")
                // ticket.AddHeaderLine("LIMA - LIMA - LINCE")

                ticket.AddTitleLine("");
                ticket.AddTitleLine(this.NomTipDoc.ToUpper());
                ticket.AddTitleLine(dt.Rows[0]["serieNumero"].ToString());
                ticket.AddTitleLine("");
                DateTime fecha = Convert.ToDateTime(dt.Rows[0]["fechaEmision"].ToString());
                string fecha2 = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                ticket.AddSubHeaderLine("FECHA: " + fecha2);
                ticket.AddSubHeaderLine("NRO. ORDEN: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                ticket.AddSubHeaderLine("MESA: " + dt.Rows[0]["M_NOM"].ToString());
                ticket.AddSubHeaderLine("MOZO: " + dt.Rows[0]["EMPL_NOM"].ToString().ToUpper());
                ticket.AddSubHeaderLineBold("Cliente: " + dt.Rows[0]["razonSocialReceptor"].ToString());
                ticket.AddSubHeaderLineBold("Nro. Doc : " + dt.Rows[0]["numeroDocIdentidadReceptor"].ToString());
                ticket.AddSubHeaderLineBold("Dirección : " + dt.Rows[0]["direccionReceptor"].ToString());
                ticket.AddSubHeaderLineBold("");

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    ticket.AddItem("[" + dt.Rows[i]["cantidadItem"].ToString() + "]" + dt.Rows[i]["descripcionItem"].ToString(), dt.Rows[i]["_precioUnitario"].ToString(), dt.Rows[i]["montoTotalItem"].ToString());
                }

                decimal total = (decimal)((decimal)dt.Rows[0]["importeTotalVenta"] + (decimal)dt.Rows[0]["totalDescuentos"]);

                decimal rc = negParametros.RC();
                decimal igv = negParametros.IGV();

                string IGV = (igv * 100).ToString();
                string RC = (rc * 100).ToString();


                ticket.AddTotal("OP. GRAVADAS", "S/    " + dt.Rows[0]["totalOPGravadas"].ToString());
                ticket.AddTotal("OP. EXONERADAS", "S/    " + dt.Rows[0]["totalOPExoneradas"].ToString());
                ticket.AddTotal("IGV (" + IGV + "%)", "S/    " + dt.Rows[0]["sumatoriaIGV"].ToString());
                ticket.AddTotal("RC (" + RC + "%)", "S/    " + dt.Rows[0]["sumatoriaOtrosCargos"].ToString());
                ticket.AddTotal("ICBPER", "S/    " + dt.Rows[0]["sumatoriaImpuestoBolsas"].ToString());
                ticket.AddTotal("", "-----------");
                //ticket.AddTotal("SUB TOTAL", "S/    " + total);
                //ticket.AddTotal("TOTAL DESCUENTOS", "S/    " + (Convert.ToDecimal(dt.Rows[0]["totalDescuentos"]) + Convert.ToDecimal(dt.Rows[0]["descuentosGlobales"])).ToString());
                ticket.AddTotal("TOTAL DESCUENTOS", "S/    " + dt.Rows[0]["descuentosGlobales"].ToString());
                ticket.AddTotal("TOTAL A COBRAR", "S/    " + dt.Rows[0]["importeTotalVenta"].ToString());
                ticket.AddTotal("", "");

                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

                qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
                // qrCodeEncoder.QRCodeEncodeMode = Codec.QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC
                qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M;
                qrCodeEncoder.QRCodeScale = Convert.ToInt32(3);
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor);
                qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor);

                string textoQR = "";
                textoQR += this.RucEmpr.ToUpper() + "|";
                textoQR += this.TipDocElectr.Where(t => t.ID == id).ToList().FirstOrDefault().VALOR_TIPO_DOC + "|";
                textoQR += dt.Rows[0]["serieNumero"].ToString().Substring(0, 4) + "|";
                textoQR += dt.Rows[0]["serieNumero"].ToString().Substring(5) + "|";
                textoQR += dt.Rows[0]["sumatoriaIGV"].ToString() + "|";
                textoQR += dt.Rows[0]["importeTotalVenta"].ToString() + "|";
                textoQR += dt.Rows[0]["fechaEmision"].ToString() + "|";
                // textoQR &= dt(0)("razonSocialReceptor").ToString & "|"
                //textoQR += dt(0)("hash").ToString;

                Bitmap bitmap = qrCodeEncoder.Encode(textoQR, System.Text.Encoding.UTF8);
                ticket.QrImage = bitmap;

                ticket.AddFooterLine("MONTO EN LETRAS: " + dt.Rows[0]["montoEnLetras"].ToString());
                //ticket.AddFooterLine("    HASH: " + dt(0)("hash").ToString);
                ticket.AddFooterLine("Representación impresa de comprobante de pago electrónico.");
                ticket.AddFooterLine("Puede descargar este documento en el enlace");
                ticket.AddFooterLine(URL);
                ticket.AddFooterLine("");
                ticket.AddFooterLine(negFuncionGlobal.PublicidadFacturacion());
                //ticket.AddFooterLine("        Desarrollado por SOS-TIC");
                //ticket.AddFooterLine("            www.sos-tic.com");

                string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
                if (ticket.PrinterExists(ImpCaja))
                {
                    ticket.PrintTicket(ImpCaja);
                }
                else
                {
                    //globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                }
                if (copia == true)
                {
                    Ticket ticket2 = new Ticket();
                    ticket2.MaxChar = Convert.ToInt32(negParametros.NroColumnasTicket());
                    ticket2.MaxCharDescription = Convert.ToInt32(negParametros.MaxCharDescriptionTicket());
                    ticket2.MaxCharDescrip = negParametros.margenMaxDescrip();
                    ticket2.MargenLogo = Convert.ToInt32(negParametros.margenLogoTicket());
                    ticket2.MargenQR = Convert.ToInt32(negParametros.margenQRTicket());
                    if (File.Exists("logo.png"))
                        ticket2.HeaderImage = Image.FromFile("logo.png");

                    ticket2.AddHeaderLine(this.RucEmpr.ToUpper());
                    ticket2.AddHeaderLine(this.NomEmpr.ToUpper());
                    ticket2.AddHeaderLine(this.DirEmpr.ToUpper());
                    ticket2.AddHeaderLine(this.DistEmpr.ToUpper());
                    // ticket.AddHeaderLine("JR. CAPAC YUPANQUI NRO. 2730 INT. 101 (A 1 CDRA DE WONG 2 DE MAYO S.I.)")
                    // ticket.AddHeaderLine("LIMA - LIMA - LINCE")

                    ticket2.AddTitleLine("");
                    ticket2.AddTitleLine(this.NomTipDoc.ToUpper());
                    ticket2.AddTitleLine(dt.Rows[0]["serieNumero"].ToString());
                    ticket2.AddTitleLine("");
                    DateTime fecha3 = Convert.ToDateTime(dt.Rows[0]["fechaEmision"].ToString());
                    string fecha4 = fecha3.ToString("dd/MM/yyyy HH:mm:ss");
                    ticket2.AddSubHeaderLine("FECHA: " + fecha4);
                    ticket2.AddSubHeaderLine("NRO. ORDEN: " + dt.Rows[0]["PED_NUM_DIARIO"].ToString());
                    ticket2.AddSubHeaderLine("MESA: " + dt.Rows[0]["M_NOM"].ToString() + "/MOZO: " + dt.Rows[0]["EMPL_NOM"].ToString().ToUpper());
                    ticket2.AddSubHeaderLineBold("Cliente: " + dt.Rows[0]["razonSocialReceptor"].ToString());
                    ticket2.AddSubHeaderLineBold("Nro. Doc : " + dt.Rows[0]["numeroDocIdentidadReceptor"].ToString());
                    ticket2.AddSubHeaderLineBold("Dirección : " + dt.Rows[0]["direccionReceptor"].ToString());
                    ticket2.AddSubHeaderLineBold("");

                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        ticket2.AddItem("[" + dt.Rows[i]["cantidadItem"].ToString() + "]" + dt.Rows[i]["descripcionItem"].ToString(), dt.Rows[i]["_precioUnitario"].ToString(), dt.Rows[i]["montoTotalItem"].ToString());
                    }

                    decimal total2 = (decimal)((decimal)dt.Rows[0]["importeTotalVenta"] + (decimal)dt.Rows[0]["totalDescuentos"]);

                    decimal rc2 = negParametros.RC();
                    decimal igv2 = negParametros.IGV();

                    string IGV2 = (igv2 * 100).ToString();
                    string RC2 = (rc2 * 100).ToString();


                    ticket2.AddTotal("OP. GRAVADAS", "S/    " + dt.Rows[0]["totalOPGravadas"].ToString());
                    ticket2.AddTotal("OP. EXONERADAS", "S/    " + dt.Rows[0]["totalOPExoneradas"].ToString());
                    ticket2.AddTotal("IGV (" + IGV2 + "%)", "S/    " + dt.Rows[0]["sumatoriaIGV"].ToString());
                    ticket2.AddTotal("RC (" + RC2 + "%)", "S/    " + dt.Rows[0]["sumatoriaOtrosCargos"].ToString());
                    ticket2.AddTotal("ICBPER", "S/    " + dt.Rows[0]["sumatoriaImpuestoBolsas"].ToString());
                    ticket2.AddTotal("", "-----------");
                    //ticket2.AddTotal("SUB TOTAL", "S/    " + total2);
                    ticket2.AddTotal("TOTAL DESCUENTOS", "S/    " + dt.Rows[0]["descuentosGlobales"].ToString());
                    ticket2.AddTotal("TOTAL A COBRAR", "S/    " + dt.Rows[0]["importeTotalVenta"].ToString());
                    ticket2.AddTotal("", "");

                    QRCodeEncoder qrCodeEncoder2 = new QRCodeEncoder();

                    qrCodeEncoder2.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
                    // qrCodeEncoder.QRCodeEncodeMode = Codec.QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC
                    qrCodeEncoder2.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M;
                    qrCodeEncoder2.QRCodeScale = Convert.ToInt32(3);
                    qrCodeEncoder2.QRCodeVersion = 0;
                    qrCodeEncoder2.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor);
                    qrCodeEncoder2.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor);

                    string textoQR2 = "";
                    textoQR2 += this.RucEmpr.ToUpper() + "|";
                    textoQR2 += this.TipDocElectr.Where(t => t.ID == id).ToList().FirstOrDefault().VALOR_TIPO_DOC + "|";
                    textoQR2 += dt.Rows[0]["serieNumero"].ToString().Substring(0, 4) + "|";
                    textoQR2 += dt.Rows[0]["serieNumero"].ToString().Substring(5) + "|";
                    textoQR2 += dt.Rows[0]["sumatoriaIGV"].ToString() + "|";
                    textoQR2 += dt.Rows[0]["importeTotalVenta"].ToString() + "|";
                    textoQR2 += dt.Rows[0]["fechaEmision"].ToString() + "|";
                    // textoQR &= dt(0)("razonSocialReceptor").ToString & "|"
                    //textoQR += dt(0)("hash").ToString;

                    Bitmap bitmap2 = qrCodeEncoder.Encode(textoQR, System.Text.Encoding.UTF8);
                    ticket2.QrImage = bitmap2;

                    ticket.AddFooterLine("MONTO EN LETRAS: " + dt.Rows[0]["montoEnLetras"].ToString());
                    ticket.AddFooterLine("Representación impresa de comprobante de pago electrónico.");
                    ticket.AddFooterLine("Puede descargar este documento en el enlace");
                    ticket.AddFooterLine(URL);
                    ticket.AddFooterLine("");
                    ticket.AddFooterLine(negFuncionGlobal.PublicidadFacturacion());

                    //ticket2.AddFooterLine("MONTO EN LETRAS: " + dt.Rows[0]["montoEnLetras"].ToString());
                    ////ticket.AddFooterLine("    HASH: " + dt(0)("hash").ToString);
                    //ticket2.AddFooterLine("Representación impresa de comprobante de pago electrónico.");
                    ////ticket2.AddFooterLine("Puede descargar este documento en el enlace");
                    ////ticket2.AddFooterLine(URL);
                    //ticket2.AddFooterLine("");
                    //ticket2.AddFooterLine("Desarrollado por https://sos-tic.com/");
                    if (ticket2.PrinterExists(ImpCaja))
                    {
                        ticket2.PrintTicket(ImpCaja);
                    }
                    else
                    {
                         //globales.Mensaje("SOS-FOOD - Informacion", "Impresora: " + ImpCaja + " no está disponible.", 2);
                    }
                }
            }
        }

    }
}

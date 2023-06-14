using Capa_Entidades.Models.Reportes.DetalleporDia;
using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Reportes.VentasporDia;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tickets;

namespace Capa_Presentacion_WPF.ViewModels.Reporte.ReporteporDias
{
    class CierrePacialViewModel:IGeneric
    {
        Neg_CierreParcial negCierre = new Neg_CierreParcial();
        public ICommand CierreParcialCommand { get; set; }
        private ICommand m_RowClickCommand;
        private string _EstadoPago;
        private string pedido;
        public int id_pedido { get; set; }
        public Detalle_Pedido SelectedDataFile { get; set; }
        //List<Pagar> list = new List<Pagar>();

         public ICommand RowClickCommand
        {
            get
            {
                return m_RowClickCommand;
            }
        }
        public class DelegateCommand : ICommand
        {
            private Action m_Action;
            public DelegateCommand(Action action)
            {
                this.m_Action = action;
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                m_Action();
            }
        }
        public string Pedido
        {
            get => pedido;
            set
            {
                pedido = value;
            }
        }
        public string EstadoPago
        {
            get => _EstadoPago;
            set
            {
                _EstadoPago = value;
            }
        }
        public CierrePacialViewModel()
        {
            this.CierreParcialCommand = new RelayCommand(new Action(CierreParcial));

            #region commandClickCaptureDatagrid
            m_RowClickCommand = new DelegateCommand(() =>
            {
                var set = this.SelectedDataFile;
                if (set != null)
                {
                    if (Application.Current.Properties["id_pedido"] == null)
                    {

                    }
                    this.EstadoPago = set.nom_estado_f.ToString();
                    this.id_pedido = set.id_ped;


                    Application.Current.Properties["id_pedido"] = set.id_ped;
                    Application.Current.Properties["Totals"] = set.total_ped;
                    Application.Current.Properties["Usuario"] = set.id_usu;
                    Application.Current.Properties["Pedido"] = set.id_ped;
                    Application.Current.Properties["EstadoPago"] = set.nom_estado_f;

                    //this.DataBolFac = negBolFac.GetBoletaFactura(this.id_pedido);

                    //ObservableCollection<BoletaFactura> DataBolFactura = new ObservableCollection<BoletaFactura>(DataBolFac);
                }

            });
            if (Application.Current.Properties["id_pedido"] != null)
            {
                this.Pedido = Application.Current.Properties["id_pedido"].ToString();
            }
            if (Application.Current.Properties["EstadoF"] != null)
            {
                this.EstadoPago = Application.Current.Properties["EstadoF"].ToString();
            }
            #endregion
        }


        private async void CierreParcial()
        {
            if (this.id_pedido == 0)
            {
                var SiNo = new SiNoMessageDialog
                {
                    Mensaje = { Text = "Debe seleccionar un pedido" }
                };
                var x = await DialogHost.Show(SiNo, "RootDialog");
                if (!(bool)x)
                    return;
            }
            else
            {
                //DataTable dt = new DataTable();
                //dt = negCierre.GetAplatosXComentario();
                //Ticket ticket = new Ticket();
                //ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                //ticket.AddHeaderLine_2("PRODUCTOS ANULADOS");
                //ticket.AddHeaderLine("");
                //ticket.AddSubHeaderLine(dt.Rows[0]["COMENTARIO"].ToString());
                //ticket.AddSubHeaderLine("");
                
                //for (int j = 0; j < dt.Rows.Count; j++)
                //{
                //    ticket.AddItemSinRecorte(dt.Rows[j]["CANTIDAD"].ToString(), dt.Rows[j]["DESCRIPCION"].ToString(), dt.Rows[j]["IMPORTE"].ToString());
                //}
                //ticket.AddFooterLine("");
                //if (ticket.PrinterExists("BIXOLON SRP-F312") != true)
                //{
                //    MessageBox.Show("Impresora: BIXOLON SRP-F312 no está disponible.");
                //}
                //else
                //{
                //    ticket.PrintTicket("BIXOLON SRP-F312");
                //}

                DataTable dt = new DataTable();
                dt = negCierre.GetreporteVetasDia(ConfigurationManager.AppSettings["NombreEquipo"].ToString());
                Ticket ticket = new Ticket();
                
                ticket.AddHeaderLine_2("REPORTE DE VENTA DEL DIA");
                ticket.AddSubHeaderLine("FECHA INICIO: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                ticket.AddSubHeaderLine("FECHA INICIO: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                ticket.AddHeaderLine("");
                ticket.AddSubHeaderLine(dt.Rows[0]["COMENTARIO"].ToString());
                ticket.AddSubHeaderLine("");

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    ticket.AddItemSinRecorte(dt.Rows[j]["CANTIDAD"].ToString(), dt.Rows[j]["DESCRIPCION"].ToString(), dt.Rows[j]["IMPORTE"].ToString());
                }
                ticket.AddFooterLine("");
                if (ticket.PrinterExists(negFuncionGlobal.ImpCaja()) != true)
                {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "Impresora: " + negFuncionGlobal.ImpCaja() + " no está disponible.", 2);
                }
                else
                {
                    ticket.PrintTicket(negFuncionGlobal.ImpCaja());
                }
            }

        }
        Funcion_Global negFuncionGlobal = new Funcion_Global();
    }
}

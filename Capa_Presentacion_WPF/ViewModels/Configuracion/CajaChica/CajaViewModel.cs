using Capa_Entidades.Models.Carta;
using Capa_Entidades.Models.Configuracion;
using Capa_Negocio.Carta;
using Capa_Negocio.Configuracion;
using Capa_Presentacion_WPF.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Capa_Entidades;
using Capa_Negocio;
using Capa_Negocio.Funciones_Globales;
using System.IO;
using System.Windows.Forms;
using System.Data;
using ExportToExcel;
using System.Configuration;
using tickets;
using Capa_Negocio.Parametros;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion
{
    public class CajaViewModel : IGeneric
    {
        #region Negocio
        Neg_Combo negCombo = new Neg_Combo();
        Neg_Caja negsCaja = new Neg_Caja();
        Funcion_Global negFuncionGlobal = new Funcion_Global();
        Neg_Parametros negParametros = new Neg_Parametros();
        #endregion
        #region Entidad
        private Caja Cajasep;
        private Ent_Combo _SCatSelected;
        public Caja Caja
        {
            get => Cajasep;
            set
            {
                Cajasep = value;
            }
        }

        public Ent_Combo SCatSelected
        {
            get => _SCatSelected;
            set
            {
                if (value != null)
                {
                    this.ComboCat = negCombo.GetCaja_Seleccion_Tipo(((Ent_Combo)value).id);
                }
                _SCatSelected = value;
            }
        }
        #endregion
        #region Objetos
        public string Operacion
        {
            get => operacion;
            set
            {
                if (value == "Lista")
                    GetLista();
                operacion = value;

            }
        }
        public string TextoBuscar { get; set; }
        public string NombreEquipo { get; set; }
        private string operacion;
        private decimal _monto;
        private decimal _monto2;
        private decimal _monto3;
        public bool imprimirCajaChica { get; set; } = false;
        #endregion
        #region Listas
        public List<Caja> datatotal { get; set; }
        public List<Ent_Combo> ComboCat { get; set; }
        public ObservableCollection<Caja> DatasCaja { get; set; }
        public List<Ent_Combo> ComboTipoCaja { get; set; }
        public List<Ent_Combo> ComboEmpleado { get; set; }
        #endregion
        #region Commands
        public ICommand EditarCommandCaja { get; set; }
        public ICommand EliminarCommandCaja { get; set; }
        public ICommand CancelarCommandCaja { get; set; }
        public ICommand NuevoCommandCaja { get; set; }
        public ICommand GuardarCommandCaja { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand ExportaPDFCommand { get; set; }
        public ICommand ExportaExcelCommand { get; set; }
        #endregion
        public CajaViewModel()
        {
            this.NombreEquipo = ConfigurationManager.AppSettings["NombreEquipo"].ToString();
            this.Operacion = "Lista";
            this.EditarCommandCaja = new ParamCommand(new Action<object>(Editar));
            this.CancelarCommandCaja = new RelayCommand(new Action(Cancelar));
            this.NuevoCommandCaja = new RelayCommand(new Action(Nuevo));
            this.GuardarCommandCaja = new RelayCommand(new Action(Guardar));
            this.EliminarCommandCaja = new ParamCommand(new Action<object>(Eliminar));
            this.BuscarCommand = new RelayCommand(new Action(Buscar));
            this.ExportaPDFCommand = new RelayCommand(new Action(ExportarPDF));
            this.ExportaExcelCommand = new RelayCommand(new Action(ExportarExcel));
            this.Cajasep = new Caja();
            this.ComboTipoCaja = negCombo.GetComboTipoCaja();
          //  this.datatotal = negsCaja.GetResumenCaja();
            this.ComboEmpleado = negCombo.GetEmpleado();
            this.ComboCat = new List<Ent_Combo>();
            Data();
            imprimirCajaChica = negParametros.ImprimirCajaChica();
        }
        private void Buscar()
        {
            ObservableCollection<Caja> ls = new ObservableCollection<Caja>();
            ls = DatasCaja = negsCaja.GetSCaja(NombreEquipo);
            if (this.TextoBuscar == null || this.TextoBuscar == "")
            {
                DatasCaja = negsCaja.GetSCaja(NombreEquipo);
            }
            else
            {
                List<Caja> lista = ls
                    .Where(w =>
                    w.TM_DESCR.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.MC_DESCR.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.NOMBRE_EMPLEADO.ToUpper().Contains(TextoBuscar.ToUpper()) ||
                    w.CC_DESCR.ToUpper().Contains(TextoBuscar.ToUpper())
                    ).ToList();
                ObservableCollection<Caja> o = new ObservableCollection<Caja>(lista);
                DatasCaja = o;
            }
        }
        private void ExportarPDF()
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a PDF";
            saveFileDialog1.FileName = "Movimientos de Caja Chica " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("DENOMINACION MOVIMIENTO");
            dt.Columns.Add("TIPO MOVIMIENTO");
            dt.Columns.Add("DESCRIPCION");
            dt.Columns.Add("REPSONSABLE");
            dt.Columns.Add("MONTO");
            dt.Columns.Add("FECHA Y HORA");
            foreach (var p in DatasCaja)
            {
                dt.Rows.Add(new object[] { p.id, p.TM_DESCR, p.MC_DESCR, p.CC_DESCR, p.NOMBRE_EMPLEADO,p.CC_MONTO,p.CC_F_CREATE});
            }
            if (dt.Rows.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        string direccion = saveFileDialog1.InitialDirectory;
                        string nombrearchivo = saveFileDialog1.FileName;
                        string ubicacion = saveFileDialog1.FileName;
                        myStream.Close();
                        negFuncionGlobal.ExportDataTableToPdf(dt, ubicacion, "LISTADO DE MOVIMIENTOS DE CAJA CHICA");
                    }
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }

        private void ExportarExcel()
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = @"C:\Users\user\Documents";

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Exportar Archivo a Excel";
            saveFileDialog1.FileName = "Movimientos de Caja Chica " + DateTime.Now.ToString("dd-MM-yyyy hhmmss");

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("DENOMINACION MOVIMIENTO");
            dt.Columns.Add("TIPO MOVIMIENTO");
            dt.Columns.Add("DESCRIPCION");
            dt.Columns.Add("REPSONSABLE");
            dt.Columns.Add("MONTO");
            dt.Columns.Add("FECHA Y HORA");
            foreach (var p in DatasCaja)
            {
                dt.Rows.Add(new object[] { p.id, p.TM_DESCR, p.MC_DESCR, p.CC_DESCR, p.NOMBRE_EMPLEADO, p.CC_MONTO, p.CC_F_CREATE });
            }
            if (dt.Rows.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        string direccion = saveFileDialog1.InitialDirectory;
                        string nombrearchivo = saveFileDialog1.FileName;
                        string ubicacion = saveFileDialog1.FileName;
                        myStream.Close();
                        CreateExcelFile.CreateExcelDocument(dt, ubicacion);
                    }
                }
            }
            else
            {
                negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "no hay registros", 2);
            }
        }
        private void Editar(object id)
        {
            this.Operacion = "Editar";
            this.Caja = this.DatasCaja.Where(w => w.id == (int)id).FirstOrDefault();
        }

        private void Cancelar()
        {
            this.Operacion = "Lista";
        }
        private void Nuevo()
        {
            //DialogHost.OpenDialogCommand.Execute(DateTime.Now,null) ;
            this.Operacion = "Nuevo";
            this.Caja = new Caja();
           

        }
        private async void Guardar()
        {
            if (!string.IsNullOrWhiteSpace(Cajasep.CC_DESCR))
            {
                if (Cajasep.CC_MONTO == null || Convert.ToDecimal(Cajasep.CC_MONTO) <= 0) {
                    negFuncionGlobal.Mensaje("SOS-FOOD - Informacion", "ingresar monto del registro", 2);
                    return;
                }
                Caja caja = this.Cajasep;
                var _id = Caja.id;
                string _mensaje = "";
                bool result = negsCaja.SetCaja((_id == 0 ? 1 : 2), Cajasep, NombreEquipo, ref _mensaje);
                var view = new MessageDialog
                {
                    Mensaje = { Text = _mensaje }
                };
                await DialogHost.Show(view, "RootDialog");
                if (result)
                {
                    if (imprimirCajaChica == true) {
                        Ticket ticket = new Ticket();
                        ticket.AddSubHeaderLine("FECHA/HORA: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        ticket.AddHeaderLine_2(this.ComboTipoCaja.Where(C => C.id == caja.CC_ID_TIPO.ToString()).FirstOrDefault().nombre);
                        ticket.AddHeaderLine("");

                        ticket.AddItemSinRecorte(caja.CC_DESCR, "", caja.CC_MONTO.ToString());

                        ticket.AddFooterLine("");
                        string ImpCaja = ConfigurationManager.AppSettings["ImpCaja"].ToString();
                        if (ticket.PrinterExists(ImpCaja) != true)
                        {
                            MessageBox.Show("Impresora: " + ImpCaja + " no está disponible.");
                        }
                        else
                        {
                            ticket.PrintTicket(ImpCaja);
                        }
                    }
                    this.Operacion = "Lista";
                }
                this.INGRESOS = this.DatasCaja.Where(w => w.CC_ID_TIPO == 1).Sum(w => (decimal)w.CC_MONTO);
                this.EGRESOS = this.DatasCaja.Where(w => w.CC_ID_TIPO == 2).Sum(w => (decimal)w.CC_MONTO);
                this.total_saldo = INGRESOS - EGRESOS;
            }
             return;
        }
        public decimal INGRESOS
        {
            get => _monto;
            set
            {
                _monto = value;
            }
        }


        public decimal EGRESOS
        {
            get => _monto2;
            set
            {
                _monto2 = value;
            }
        }


        public decimal total_saldo
        {
            get => _monto3;
            set
            {
                _monto3 = value;
            }
        }

        private void Data()
        {
            this.INGRESOS = this.DatasCaja.Where(w => w.CC_ID_TIPO == 1).Sum(w => (decimal)w.CC_MONTO);
            this.EGRESOS = this.DatasCaja.Where(w => w.CC_ID_TIPO == 2).Sum(w => (decimal)w.CC_MONTO);
            this.total_saldo = INGRESOS - EGRESOS;
        }


        private async void Eliminar(object id)
        {
            var SiNo = new SiNoMessageDialog
            {
                Mensaje = { Text = "¿Desea eliminar el registro?" }
            };
            var x = await DialogHost.Show(SiNo, "RootDialog");
            if (!(bool)x)
                return;
            string _mensaje = "";

            Cajasep.id = (int)id;
            bool result = negsCaja.SetCaja(3, Cajasep, NombreEquipo, ref _mensaje);
            var view = new MessageDialog
            {
                Mensaje = { Text = _mensaje }
            };
            await DialogHost.Show(view, "RootDialog");
            if (result)
            {
                GetLista();
            }
            this.INGRESOS = this.DatasCaja.Where(w => w.CC_ID_TIPO == 1).Sum(w => (decimal)w.CC_MONTO);
            this.EGRESOS = this.DatasCaja.Where(w => w.CC_ID_TIPO == 2).Sum(w => (decimal)w.CC_MONTO);
            this.total_saldo = INGRESOS - EGRESOS;
        }
        private void GetLista()
        {
            DatasCaja = negsCaja.GetSCaja(NombreEquipo);
        }


    }
}

using Capa_Negocio.Funciones_Globales;
using Capa_Negocio.Parametros;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Capa_Presentacion_WPF.ViewModels.Configuracion.Transaccion
{
    public class RptImpuestosViewModel : INotifyPropertyChanged
    {
        //Para el INotifyPropertyChanged sea Inicializado.
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #region Negocio
        Neg_Parametros neg_Parametros = new Neg_Parametros();
        Funcion_Global globales = new Funcion_Global();
        #endregion
        #region Objetos
        private string _operacion;
        public string Operacion
        {
            get => _operacion;
            set
            {
                if (value == "Lista")
                {
                    getLista();
                }
                _operacion = value;
            }
        }
        public bool ischeckedValor { get; set; } = false;
        public bool V_0 { get; set; }
        public bool V_1 { get; set; }
        public bool V_2 { get; set; }
        public bool V_3 { get; set; }
        public bool V_4 { get; set; }

        private string _ValorTexto;
        public string ValorTexto
        {
            get { return _ValorTexto; }
            set
            {
                _ValorTexto = value;
                OnPropertyChanged_ValidarNumero("ValorTexto");
                //if ((value.Length <= 11 && tipDoc.ToString() == "6") || (value.Length <= 8 && tipDoc.ToString() == "1"))
                //{

                //}
                //else if (tipDoc.ToString() == "4" || tipDoc.ToString() == "7")
                //{
                //    _ValorTexto = value;
                //    OnPropertyChanged_ValidarNumero("RucCliente");
                //}
            }
        }
        public event PropertyChangedEventHandler PropertyChanged_ValidarNumero;
        public void OnPropertyChanged_ValidarNumero(string txtRucCliente)
        {
            PropertyChangedEventHandler handler = PropertyChanged_ValidarNumero;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(txtRucCliente));

            if (this.ValorTexto != "")
            {
                this.ValorTexto = isCaracterValido(this.ValorTexto);
            }
        }
        public string isCaracterValido(string txt)
        {
            string ultimo = txt.Substring(txt.Length - 1, 1);
            Char c = Convert.ToChar(ultimo);
            if ((c >= '0' && c <= '9') || c == '.')
            {
                return txt;
            }
            else
            {
                return txt.Replace(ultimo, "");
            }

        }

        #endregion
        #region Lista
        public ObservableCollection<Capa_Entidades.Models.Parametros.Parametros> DataParametros { get; set; }
        #endregion
        #region Commands
        public ICommand EditarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand IsCheckedValorCommand { get; set; }
        #endregion
        #region Entidad
        private Capa_Entidades.Models.Parametros.Parametros _parametros;
        public Capa_Entidades.Models.Parametros.Parametros Parametros
        {
            get => _parametros;
            set
            {
                _parametros = value;
            }
        }
        #endregion
        public RptImpuestosViewModel() {
            Operacion = "Lista";
            this.EditarCommand = new ParamCommand(new Action<object>(Editar));
            this.GuardarCommand = new RelayCommand(new Action(Guardar));
            this.CancelarCommand = new RelayCommand(new Action(Cancelar));
        }
        private void Editar(object id) {
            try
            {
                char[] spearator = { '-' };
                string[] id_parametro;
                int id_parametro_seleccionado = 0;
                int id_tipo_parametro = 0;
                id_parametro = id.ToString().Split(spearator).ToArray();
                id_parametro = id_parametro.ToArray();

                for (int p = 0; p < id_parametro.Distinct().Count(); p++)
                {
                    id_parametro_seleccionado = Convert.ToInt32(id_parametro[0]);
                    id_tipo_parametro = Convert.ToInt32(id_parametro[1]);
                }


                Operacion = "Editar";
                Parametros = DataParametros.Where(w => w.ID_PAR == id_parametro_seleccionado && w.tipoParametro == id_tipo_parametro).FirstOrDefault();
                ValorTexto = Parametros.VALOR_PAR;
                ischeckedValor = Convert.ToBoolean((Parametros.est_activo == "1") ? true : false);
            }
            catch (Exception)
            {
            }
        }
        private void Guardar() {
            if (Parametros.NOM_PAR == "Facturación Electrónica" || Parametros.NOM_PAR == "EnvioDoc")
            {
                Parametros.VALOR_PAR = ischeckedValor.ToString();
            }
            else if (Parametros.NOM_PAR == "Ticket Promedio") {
                if (ischeckedValor == true)
                {
                    Parametros.VALOR_PAR = "2";
                }
                else if (ischeckedValor == false)
                {
                    Parametros.VALOR_PAR = "1";
                }
            }
            else if (Parametros.NOM_PAR == "IGV" || Parametros.NOM_PAR == "RC" || Parametros.NOM_PAR == "BP" || Parametros.NOM_PAR == "BOLETA" || Parametros.NOM_PAR == "FACTURA")
            {
                Parametros.VALOR_PAR = ValorTexto;
            }

            bool x = neg_Parametros.Set_Parametros_Impuestos(Parametros);
            if (x == false)
            {
                globales.Mensaje("SOS-FOOD - Informacion ", "No se pudo actualizar el impuesto", 2);
                return;
            }
            else
            {
                Operacion = "Lista";
            }
        }
        private void Cancelar() {
            this.Operacion = "Lista";
        }
        private void getLista() {
            DataParametros = neg_Parametros.GetParametrosImpuestos();
        }
    }
}

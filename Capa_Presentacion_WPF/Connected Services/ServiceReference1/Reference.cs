﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Capa_Presentacion_WPF.ServiceReference1 {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://sos-tic.com/", ConfigurationName="ServiceReference1.sosfoodSoap")]
    public interface sosfoodSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sos-tic.com/EnviaCorreoCierre", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool EnviaCorreoCierre(System.Data.DataSet dataSet, string nombre_local);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sos-tic.com/EnviaCorreoCierre", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> EnviaCorreoCierreAsync(System.Data.DataSet dataSet, string nombre_local);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sos-tic.com/ConsultaDNI", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ConsultaDNI(string dni);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sos-tic.com/ConsultaDNI", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ConsultaDNIAsync(string dni);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sos-tic.com/ConsultaRuc", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Capa_Presentacion_WPF.ServiceReference1.ClsClienteConsultaEN ConsultaRuc(string token, string ruc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sos-tic.com/ConsultaRuc", ReplyAction="*")]
        System.Threading.Tasks.Task<Capa_Presentacion_WPF.ServiceReference1.ClsClienteConsultaEN> ConsultaRucAsync(string token, string ruc);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://sos-tic.com/")]
    public partial class ClsClienteConsultaEN : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string errField;
        
        private System.DateTime ultima_actualizacionField;
        
        private string direccion_completaField;
        
        private string ubigeoField;
        
        private string condicion_de_domicilioField;
        
        private string estado_del_contribuyenteField;
        
        private string nombre_o_razon_socialField;
        
        private bool successField;
        
        private string rucField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string err {
            get {
                return this.errField;
            }
            set {
                this.errField = value;
                this.RaisePropertyChanged("err");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public System.DateTime ultima_actualizacion {
            get {
                return this.ultima_actualizacionField;
            }
            set {
                this.ultima_actualizacionField = value;
                this.RaisePropertyChanged("ultima_actualizacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string direccion_completa {
            get {
                return this.direccion_completaField;
            }
            set {
                this.direccion_completaField = value;
                this.RaisePropertyChanged("direccion_completa");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string ubigeo {
            get {
                return this.ubigeoField;
            }
            set {
                this.ubigeoField = value;
                this.RaisePropertyChanged("ubigeo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string condicion_de_domicilio {
            get {
                return this.condicion_de_domicilioField;
            }
            set {
                this.condicion_de_domicilioField = value;
                this.RaisePropertyChanged("condicion_de_domicilio");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string estado_del_contribuyente {
            get {
                return this.estado_del_contribuyenteField;
            }
            set {
                this.estado_del_contribuyenteField = value;
                this.RaisePropertyChanged("estado_del_contribuyente");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string nombre_o_razon_social {
            get {
                return this.nombre_o_razon_socialField;
            }
            set {
                this.nombre_o_razon_socialField = value;
                this.RaisePropertyChanged("nombre_o_razon_social");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public bool success {
            get {
                return this.successField;
            }
            set {
                this.successField = value;
                this.RaisePropertyChanged("success");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string ruc {
            get {
                return this.rucField;
            }
            set {
                this.rucField = value;
                this.RaisePropertyChanged("ruc");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface sosfoodSoapChannel : Capa_Presentacion_WPF.ServiceReference1.sosfoodSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class sosfoodSoapClient : System.ServiceModel.ClientBase<Capa_Presentacion_WPF.ServiceReference1.sosfoodSoap>, Capa_Presentacion_WPF.ServiceReference1.sosfoodSoap {
        
        public sosfoodSoapClient() {
        }
        
        public sosfoodSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public sosfoodSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public sosfoodSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public sosfoodSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool EnviaCorreoCierre(System.Data.DataSet dataSet, string nombre_local) {
            return base.Channel.EnviaCorreoCierre(dataSet, nombre_local);
        }
        
        public System.Threading.Tasks.Task<bool> EnviaCorreoCierreAsync(System.Data.DataSet dataSet, string nombre_local) {
            return base.Channel.EnviaCorreoCierreAsync(dataSet, nombre_local);
        }
        
        public string ConsultaDNI(string dni) {
            return base.Channel.ConsultaDNI(dni);
        }
        
        public System.Threading.Tasks.Task<string> ConsultaDNIAsync(string dni) {
            return base.Channel.ConsultaDNIAsync(dni);
        }
        
        public Capa_Presentacion_WPF.ServiceReference1.ClsClienteConsultaEN ConsultaRuc(string token, string ruc) {
            return base.Channel.ConsultaRuc(token, ruc);
        }
        
        public System.Threading.Tasks.Task<Capa_Presentacion_WPF.ServiceReference1.ClsClienteConsultaEN> ConsultaRucAsync(string token, string ruc) {
            return base.Channel.ConsultaRucAsync(token, ruc);
        }
    }
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CustomerApp.WcfService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/Server")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ProductExtension", Namespace="http://schemas.datacontract.org/2004/07/Server.DTO")]
    [System.SerializableAttribute()]
    public partial class ProductExtension : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int QuantityFromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int QuantityToField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal UnitPriceFromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal UnitPriceToField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int QuantityFrom {
            get {
                return this.QuantityFromField;
            }
            set {
                if ((this.QuantityFromField.Equals(value) != true)) {
                    this.QuantityFromField = value;
                    this.RaisePropertyChanged("QuantityFrom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int QuantityTo {
            get {
                return this.QuantityToField;
            }
            set {
                if ((this.QuantityToField.Equals(value) != true)) {
                    this.QuantityToField = value;
                    this.RaisePropertyChanged("QuantityTo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal UnitPriceFrom {
            get {
                return this.UnitPriceFromField;
            }
            set {
                if ((this.UnitPriceFromField.Equals(value) != true)) {
                    this.UnitPriceFromField = value;
                    this.RaisePropertyChanged("UnitPriceFrom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal UnitPriceTo {
            get {
                return this.UnitPriceToField;
            }
            set {
                if ((this.UnitPriceToField.Equals(value) != true)) {
                    this.UnitPriceToField = value;
                    this.RaisePropertyChanged("UnitPriceTo");
                }
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfService.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetData", ReplyAction="http://tempuri.org/IService1/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        CustomerApp.WcfService.CompositeType GetDataUsingDataContract(CustomerApp.WcfService.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService1/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<CustomerApp.WcfService.CompositeType> GetDataUsingDataContractAsync(CustomerApp.WcfService.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ProductExtensions", ReplyAction="http://tempuri.org/IService1/ProductExtensionsResponse")]
        System.Collections.Generic.List<CustomerApp.WcfService.ProductExtension> ProductExtensions();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ProductExtensions", ReplyAction="http://tempuri.org/IService1/ProductExtensionsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<CustomerApp.WcfService.ProductExtension>> ProductExtensionsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetProducts", ReplyAction="http://tempuri.org/IService1/GetProductsResponse")]
        System.Collections.Generic.List<Model.Products> GetProducts(Model.Products product, CustomerApp.WcfService.ProductExtension productExtension);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetProducts", ReplyAction="http://tempuri.org/IService1/GetProductsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Model.Products>> GetProductsAsync(Model.Products product, CustomerApp.WcfService.ProductExtension productExtension);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetCustomerData", ReplyAction="http://tempuri.org/IService1/GetCustomerDataResponse")]
        System.Collections.Generic.List<Model.Customers> GetCustomerData(int customerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetCustomerData", ReplyAction="http://tempuri.org/IService1/GetCustomerDataResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Model.Customers>> GetCustomerDataAsync(int customerId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : CustomerApp.WcfService.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<CustomerApp.WcfService.IService1>, CustomerApp.WcfService.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public CustomerApp.WcfService.CompositeType GetDataUsingDataContract(CustomerApp.WcfService.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<CustomerApp.WcfService.CompositeType> GetDataUsingDataContractAsync(CustomerApp.WcfService.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public System.Collections.Generic.List<CustomerApp.WcfService.ProductExtension> ProductExtensions() {
            return base.Channel.ProductExtensions();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<CustomerApp.WcfService.ProductExtension>> ProductExtensionsAsync() {
            return base.Channel.ProductExtensionsAsync();
        }
        
        public System.Collections.Generic.List<Model.Products> GetProducts(Model.Products product, CustomerApp.WcfService.ProductExtension productExtension) {
            return base.Channel.GetProducts(product, productExtension);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Model.Products>> GetProductsAsync(Model.Products product, CustomerApp.WcfService.ProductExtension productExtension) {
            return base.Channel.GetProductsAsync(product, productExtension);
        }
        
        public System.Collections.Generic.List<Model.Customers> GetCustomerData(int customerId) {
            return base.Channel.GetCustomerData(customerId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Model.Customers>> GetCustomerDataAsync(int customerId) {
            return base.Channel.GetCustomerDataAsync(customerId);
        }
    }
}
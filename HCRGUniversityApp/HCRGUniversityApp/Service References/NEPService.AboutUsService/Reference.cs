﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HCRGUniversityApp.NEPService.AboutUsService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseColumn", Namespace="http://schemas.datacontract.org/2004/07/HCRGUniversityService.DTO.Base")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HCRGUniversityApp.NEPService.AboutUsService.AboutUs))]
    public partial class BaseColumn : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ClientIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int OrganizationIDField;
        
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
        public int ClientID {
            get {
                return this.ClientIDField;
            }
            set {
                if ((this.ClientIDField.Equals(value) != true)) {
                    this.ClientIDField = value;
                    this.RaisePropertyChanged("ClientID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int OrganizationID {
            get {
                return this.OrganizationIDField;
            }
            set {
                if ((this.OrganizationIDField.Equals(value) != true)) {
                    this.OrganizationIDField = value;
                    this.RaisePropertyChanged("OrganizationID");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="AboutUs", Namespace="http://schemas.datacontract.org/2004/07/HCRGUniversityService.DTO")]
    [System.SerializableAttribute()]
    public partial class AboutUs : HCRGUniversityApp.NEPService.AboutUsService.BaseColumn {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AboutUsIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AboutUsID {
            get {
                return this.AboutUsIDField;
            }
            set {
                if ((this.AboutUsIDField.Equals(value) != true)) {
                    this.AboutUsIDField = value;
                    this.RaisePropertyChanged("AboutUsID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PagedAboutUs", Namespace="http://schemas.datacontract.org/2004/07/HCRGUniversityService.DTO.Paged")]
    [System.SerializableAttribute()]
    public partial class PagedAboutUs : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HCRGUniversityApp.NEPService.AboutUsService.AboutUs[] AboutUsRecordsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TotalCountField;
        
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
        public HCRGUniversityApp.NEPService.AboutUsService.AboutUs[] AboutUsRecords {
            get {
                return this.AboutUsRecordsField;
            }
            set {
                if ((object.ReferenceEquals(this.AboutUsRecordsField, value) != true)) {
                    this.AboutUsRecordsField = value;
                    this.RaisePropertyChanged("AboutUsRecords");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TotalCount {
            get {
                return this.TotalCountField;
            }
            set {
                if ((this.TotalCountField.Equals(value) != true)) {
                    this.TotalCountField = value;
                    this.RaisePropertyChanged("TotalCount");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="NEPService.AboutUsService.IAboutUsService")]
    public interface IAboutUsService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAboutUsService/AddAboutUs", ReplyAction="http://tempuri.org/IAboutUsService/AddAboutUsResponse")]
        int AddAboutUs(HCRGUniversityApp.NEPService.AboutUsService.AboutUs aboutus);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAboutUsService/AddAboutUs", ReplyAction="http://tempuri.org/IAboutUsService/AddAboutUsResponse")]
        System.Threading.Tasks.Task<int> AddAboutUsAsync(HCRGUniversityApp.NEPService.AboutUsService.AboutUs aboutus);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAboutUsService/UpdateAboutUs", ReplyAction="http://tempuri.org/IAboutUsService/UpdateAboutUsResponse")]
        int UpdateAboutUs(HCRGUniversityApp.NEPService.AboutUsService.AboutUs aboutus);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAboutUsService/UpdateAboutUs", ReplyAction="http://tempuri.org/IAboutUsService/UpdateAboutUsResponse")]
        System.Threading.Tasks.Task<int> UpdateAboutUsAsync(HCRGUniversityApp.NEPService.AboutUsService.AboutUs aboutus);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAboutUsService/DeleteAboutUs", ReplyAction="http://tempuri.org/IAboutUsService/DeleteAboutUsResponse")]
        void DeleteAboutUs(int aboutUsID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAboutUsService/DeleteAboutUs", ReplyAction="http://tempuri.org/IAboutUsService/DeleteAboutUsResponse")]
        System.Threading.Tasks.Task DeleteAboutUsAsync(int aboutUsID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAboutUsService/getAllRecords", ReplyAction="http://tempuri.org/IAboutUsService/getAllRecordsResponse")]
        HCRGUniversityApp.NEPService.AboutUsService.AboutUs[] getAllRecords(int OrganizationID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAboutUsService/getAllRecords", ReplyAction="http://tempuri.org/IAboutUsService/getAllRecordsResponse")]
        System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.AboutUsService.AboutUs[]> getAllRecordsAsync(int OrganizationID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAboutUsService/GetAllPagedAboutus", ReplyAction="http://tempuri.org/IAboutUsService/GetAllPagedAboutusResponse")]
        HCRGUniversityApp.NEPService.AboutUsService.PagedAboutUs GetAllPagedAboutus(int skip, int take, int OrganizationID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAboutUsService/GetAllPagedAboutus", ReplyAction="http://tempuri.org/IAboutUsService/GetAllPagedAboutusResponse")]
        System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.AboutUsService.PagedAboutUs> GetAllPagedAboutusAsync(int skip, int take, int OrganizationID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAboutUsServiceChannel : HCRGUniversityApp.NEPService.AboutUsService.IAboutUsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AboutUsServiceClient : System.ServiceModel.ClientBase<HCRGUniversityApp.NEPService.AboutUsService.IAboutUsService>, HCRGUniversityApp.NEPService.AboutUsService.IAboutUsService {
        
        public AboutUsServiceClient() {
        }
        
        public AboutUsServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AboutUsServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AboutUsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AboutUsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int AddAboutUs(HCRGUniversityApp.NEPService.AboutUsService.AboutUs aboutus) {
            return base.Channel.AddAboutUs(aboutus);
        }
        
        public System.Threading.Tasks.Task<int> AddAboutUsAsync(HCRGUniversityApp.NEPService.AboutUsService.AboutUs aboutus) {
            return base.Channel.AddAboutUsAsync(aboutus);
        }
        
        public int UpdateAboutUs(HCRGUniversityApp.NEPService.AboutUsService.AboutUs aboutus) {
            return base.Channel.UpdateAboutUs(aboutus);
        }
        
        public System.Threading.Tasks.Task<int> UpdateAboutUsAsync(HCRGUniversityApp.NEPService.AboutUsService.AboutUs aboutus) {
            return base.Channel.UpdateAboutUsAsync(aboutus);
        }
        
        public void DeleteAboutUs(int aboutUsID) {
            base.Channel.DeleteAboutUs(aboutUsID);
        }
        
        public System.Threading.Tasks.Task DeleteAboutUsAsync(int aboutUsID) {
            return base.Channel.DeleteAboutUsAsync(aboutUsID);
        }
        
        public HCRGUniversityApp.NEPService.AboutUsService.AboutUs[] getAllRecords(int OrganizationID) {
            return base.Channel.getAllRecords(OrganizationID);
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.AboutUsService.AboutUs[]> getAllRecordsAsync(int OrganizationID) {
            return base.Channel.getAllRecordsAsync(OrganizationID);
        }
        
        public HCRGUniversityApp.NEPService.AboutUsService.PagedAboutUs GetAllPagedAboutus(int skip, int take, int OrganizationID) {
            return base.Channel.GetAllPagedAboutus(skip, take, OrganizationID);
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.AboutUsService.PagedAboutUs> GetAllPagedAboutusAsync(int skip, int take, int OrganizationID) {
            return base.Channel.GetAllPagedAboutusAsync(skip, take, OrganizationID);
        }
    }
}

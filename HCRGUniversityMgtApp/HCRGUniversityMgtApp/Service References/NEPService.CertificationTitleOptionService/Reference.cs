﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CertificationTitleOption", Namespace="http://schemas.datacontract.org/2004/07/HCRGUniversityService.DTO")]
    [System.SerializableAttribute()]
    public partial class CertificationTitleOption : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CertificationContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CertificationTitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CertificationTitleOptionIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CourseNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int EducationIdField;
        
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
        public string CertificationContent {
            get {
                return this.CertificationContentField;
            }
            set {
                if ((object.ReferenceEquals(this.CertificationContentField, value) != true)) {
                    this.CertificationContentField = value;
                    this.RaisePropertyChanged("CertificationContent");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CertificationTitle {
            get {
                return this.CertificationTitleField;
            }
            set {
                if ((object.ReferenceEquals(this.CertificationTitleField, value) != true)) {
                    this.CertificationTitleField = value;
                    this.RaisePropertyChanged("CertificationTitle");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CertificationTitleOptionID {
            get {
                return this.CertificationTitleOptionIDField;
            }
            set {
                if ((this.CertificationTitleOptionIDField.Equals(value) != true)) {
                    this.CertificationTitleOptionIDField = value;
                    this.RaisePropertyChanged("CertificationTitleOptionID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CourseName {
            get {
                return this.CourseNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CourseNameField, value) != true)) {
                    this.CourseNameField = value;
                    this.RaisePropertyChanged("CourseName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EducationId {
            get {
                return this.EducationIdField;
            }
            set {
                if ((this.EducationIdField.Equals(value) != true)) {
                    this.EducationIdField = value;
                    this.RaisePropertyChanged("EducationId");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="CourseNameDropDownList", Namespace="http://schemas.datacontract.org/2004/07/HCRGUniversityService.DTO")]
    [System.SerializableAttribute()]
    public partial class CourseNameDropDownList : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CourseNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int EducationIDField;
        
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
        public string CourseName {
            get {
                return this.CourseNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CourseNameField, value) != true)) {
                    this.CourseNameField = value;
                    this.RaisePropertyChanged("CourseName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EducationID {
            get {
                return this.EducationIDField;
            }
            set {
                if ((this.EducationIDField.Equals(value) != true)) {
                    this.EducationIDField = value;
                    this.RaisePropertyChanged("EducationID");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="PagedCertificationTitleOption", Namespace="http://schemas.datacontract.org/2004/07/HCRGUniversityService.DTO.Paged")]
    [System.SerializableAttribute()]
    public partial class PagedCertificationTitleOption : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption[] CertificationTitleOptionDetailsField;
        
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
        public HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption[] CertificationTitleOptionDetails {
            get {
                return this.CertificationTitleOptionDetailsField;
            }
            set {
                if ((object.ReferenceEquals(this.CertificationTitleOptionDetailsField, value) != true)) {
                    this.CertificationTitleOptionDetailsField = value;
                    this.RaisePropertyChanged("CertificationTitleOptionDetails");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="NEPService.CertificationTitleOptionService.ICertificationTitleOptionService")]
    public interface ICertificationTitleOptionService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/AddCertificationTitleOption", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/AddCertificationTitleOptionRe" +
            "sponse")]
        int AddCertificationTitleOption(HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption _certificationTitleOption);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/AddCertificationTitleOption", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/AddCertificationTitleOptionRe" +
            "sponse")]
        System.Threading.Tasks.Task<int> AddCertificationTitleOptionAsync(HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption _certificationTitleOption);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/UpdateCertificationTitleOptio" +
            "n", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/UpdateCertificationTitleOptio" +
            "nResponse")]
        int UpdateCertificationTitleOption(HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption _certificationTitleOption);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/UpdateCertificationTitleOptio" +
            "n", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/UpdateCertificationTitleOptio" +
            "nResponse")]
        System.Threading.Tasks.Task<int> UpdateCertificationTitleOptionAsync(HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption _certificationTitleOption);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/DeleteCertificationTitleOptio" +
            "n", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/DeleteCertificationTitleOptio" +
            "nResponse")]
        void DeleteCertificationTitleOption(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/DeleteCertificationTitleOptio" +
            "n", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/DeleteCertificationTitleOptio" +
            "nResponse")]
        System.Threading.Tasks.Task DeleteCertificationTitleOptionAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/GetAllCertificationTitleOptio" +
            "ns", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/GetAllCertificationTitleOptio" +
            "nsResponse")]
        HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption[] GetAllCertificationTitleOptions();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/GetAllCertificationTitleOptio" +
            "ns", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/GetAllCertificationTitleOptio" +
            "nsResponse")]
        System.Threading.Tasks.Task<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption[]> GetAllCertificationTitleOptionsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/GetCourseDropDownList", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/GetCourseDropDownListResponse" +
            "")]
        HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CourseNameDropDownList[] GetCourseDropDownList(int OrganizationID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/GetCourseDropDownList", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/GetCourseDropDownListResponse" +
            "")]
        System.Threading.Tasks.Task<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CourseNameDropDownList[]> GetCourseDropDownListAsync(int OrganizationID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/GetCertificationTitleOptionsB" +
            "yID", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/GetCertificationTitleOptionsB" +
            "yIDResponse")]
        HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption GetCertificationTitleOptionsByID(int CertificationTitleOptionID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/GetCertificationTitleOptionsB" +
            "yID", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/GetCertificationTitleOptionsB" +
            "yIDResponse")]
        System.Threading.Tasks.Task<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption> GetCertificationTitleOptionsByIDAsync(int CertificationTitleOptionID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/GetCertificationTitleOptionsB" +
            "yEducationID", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/GetCertificationTitleOptionsB" +
            "yEducationIDResponse")]
        HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption GetCertificationTitleOptionsByEducationID(int CertificationTitleOptionID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/GetCertificationTitleOptionsB" +
            "yEducationID", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/GetCertificationTitleOptionsB" +
            "yEducationIDResponse")]
        System.Threading.Tasks.Task<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption> GetCertificationTitleOptionsByEducationIDAsync(int CertificationTitleOptionID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/GetAllPagedCertificationTitle" +
            "Option", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/GetAllPagedCertificationTitle" +
            "OptionResponse")]
        HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.PagedCertificationTitleOption GetAllPagedCertificationTitleOption(int skip, int take);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICertificationTitleOptionService/GetAllPagedCertificationTitle" +
            "Option", ReplyAction="http://tempuri.org/ICertificationTitleOptionService/GetAllPagedCertificationTitle" +
            "OptionResponse")]
        System.Threading.Tasks.Task<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.PagedCertificationTitleOption> GetAllPagedCertificationTitleOptionAsync(int skip, int take);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICertificationTitleOptionServiceChannel : HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.ICertificationTitleOptionService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CertificationTitleOptionServiceClient : System.ServiceModel.ClientBase<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.ICertificationTitleOptionService>, HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.ICertificationTitleOptionService {
        
        public CertificationTitleOptionServiceClient() {
        }
        
        public CertificationTitleOptionServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CertificationTitleOptionServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CertificationTitleOptionServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CertificationTitleOptionServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int AddCertificationTitleOption(HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption _certificationTitleOption) {
            return base.Channel.AddCertificationTitleOption(_certificationTitleOption);
        }
        
        public System.Threading.Tasks.Task<int> AddCertificationTitleOptionAsync(HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption _certificationTitleOption) {
            return base.Channel.AddCertificationTitleOptionAsync(_certificationTitleOption);
        }
        
        public int UpdateCertificationTitleOption(HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption _certificationTitleOption) {
            return base.Channel.UpdateCertificationTitleOption(_certificationTitleOption);
        }
        
        public System.Threading.Tasks.Task<int> UpdateCertificationTitleOptionAsync(HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption _certificationTitleOption) {
            return base.Channel.UpdateCertificationTitleOptionAsync(_certificationTitleOption);
        }
        
        public void DeleteCertificationTitleOption(int id) {
            base.Channel.DeleteCertificationTitleOption(id);
        }
        
        public System.Threading.Tasks.Task DeleteCertificationTitleOptionAsync(int id) {
            return base.Channel.DeleteCertificationTitleOptionAsync(id);
        }
        
        public HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption[] GetAllCertificationTitleOptions() {
            return base.Channel.GetAllCertificationTitleOptions();
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption[]> GetAllCertificationTitleOptionsAsync() {
            return base.Channel.GetAllCertificationTitleOptionsAsync();
        }
        
        public HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CourseNameDropDownList[] GetCourseDropDownList(int OrganizationID) {
            return base.Channel.GetCourseDropDownList(OrganizationID);
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CourseNameDropDownList[]> GetCourseDropDownListAsync(int OrganizationID) {
            return base.Channel.GetCourseDropDownListAsync(OrganizationID);
        }
        
        public HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption GetCertificationTitleOptionsByID(int CertificationTitleOptionID) {
            return base.Channel.GetCertificationTitleOptionsByID(CertificationTitleOptionID);
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption> GetCertificationTitleOptionsByIDAsync(int CertificationTitleOptionID) {
            return base.Channel.GetCertificationTitleOptionsByIDAsync(CertificationTitleOptionID);
        }
        
        public HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption GetCertificationTitleOptionsByEducationID(int CertificationTitleOptionID) {
            return base.Channel.GetCertificationTitleOptionsByEducationID(CertificationTitleOptionID);
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption> GetCertificationTitleOptionsByEducationIDAsync(int CertificationTitleOptionID) {
            return base.Channel.GetCertificationTitleOptionsByEducationIDAsync(CertificationTitleOptionID);
        }
        
        public HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.PagedCertificationTitleOption GetAllPagedCertificationTitleOption(int skip, int take) {
            return base.Channel.GetAllPagedCertificationTitleOption(skip, take);
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.PagedCertificationTitleOption> GetAllPagedCertificationTitleOptionAsync(int skip, int take) {
            return base.Channel.GetAllPagedCertificationTitleOptionAsync(skip, take);
        }
    }
}

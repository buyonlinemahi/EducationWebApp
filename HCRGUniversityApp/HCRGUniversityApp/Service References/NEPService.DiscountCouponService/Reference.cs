﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HCRGUniversityApp.NEPService.DiscountCouponService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DiscountCoupon", Namespace="http://schemas.datacontract.org/2004/07/HCRGUniversityService.DTO")]
    [System.SerializableAttribute()]
    public partial class DiscountCoupon : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool CoupanValidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CouponCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal CouponDiscountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CouponEducationIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CouponExpiryDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CouponIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CouponIssueDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CouponProduactIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CouponTypeField;
        
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
        public bool CoupanValid {
            get {
                return this.CoupanValidField;
            }
            set {
                if ((this.CoupanValidField.Equals(value) != true)) {
                    this.CoupanValidField = value;
                    this.RaisePropertyChanged("CoupanValid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CouponCode {
            get {
                return this.CouponCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.CouponCodeField, value) != true)) {
                    this.CouponCodeField = value;
                    this.RaisePropertyChanged("CouponCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal CouponDiscount {
            get {
                return this.CouponDiscountField;
            }
            set {
                if ((this.CouponDiscountField.Equals(value) != true)) {
                    this.CouponDiscountField = value;
                    this.RaisePropertyChanged("CouponDiscount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CouponEducationID {
            get {
                return this.CouponEducationIDField;
            }
            set {
                if ((this.CouponEducationIDField.Equals(value) != true)) {
                    this.CouponEducationIDField = value;
                    this.RaisePropertyChanged("CouponEducationID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CouponExpiryDate {
            get {
                return this.CouponExpiryDateField;
            }
            set {
                if ((this.CouponExpiryDateField.Equals(value) != true)) {
                    this.CouponExpiryDateField = value;
                    this.RaisePropertyChanged("CouponExpiryDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CouponID {
            get {
                return this.CouponIDField;
            }
            set {
                if ((this.CouponIDField.Equals(value) != true)) {
                    this.CouponIDField = value;
                    this.RaisePropertyChanged("CouponID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CouponIssueDate {
            get {
                return this.CouponIssueDateField;
            }
            set {
                if ((this.CouponIssueDateField.Equals(value) != true)) {
                    this.CouponIssueDateField = value;
                    this.RaisePropertyChanged("CouponIssueDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CouponProduactID {
            get {
                return this.CouponProduactIDField;
            }
            set {
                if ((this.CouponProduactIDField.Equals(value) != true)) {
                    this.CouponProduactIDField = value;
                    this.RaisePropertyChanged("CouponProduactID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CouponType {
            get {
                return this.CouponTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.CouponTypeField, value) != true)) {
                    this.CouponTypeField = value;
                    this.RaisePropertyChanged("CouponType");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="DiscountCouponForCourses", Namespace="http://schemas.datacontract.org/2004/07/HCRGUniversityService.DTO")]
    [System.SerializableAttribute()]
    public partial class DiscountCouponForCourses : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool CoupanValidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CouponCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal CouponDiscountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CouponEducationIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CouponExpiryDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CouponIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CouponIssueDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CouponProduactIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CouponTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CourseNameField;
        
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
        public bool CoupanValid {
            get {
                return this.CoupanValidField;
            }
            set {
                if ((this.CoupanValidField.Equals(value) != true)) {
                    this.CoupanValidField = value;
                    this.RaisePropertyChanged("CoupanValid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CouponCode {
            get {
                return this.CouponCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.CouponCodeField, value) != true)) {
                    this.CouponCodeField = value;
                    this.RaisePropertyChanged("CouponCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal CouponDiscount {
            get {
                return this.CouponDiscountField;
            }
            set {
                if ((this.CouponDiscountField.Equals(value) != true)) {
                    this.CouponDiscountField = value;
                    this.RaisePropertyChanged("CouponDiscount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CouponEducationID {
            get {
                return this.CouponEducationIDField;
            }
            set {
                if ((this.CouponEducationIDField.Equals(value) != true)) {
                    this.CouponEducationIDField = value;
                    this.RaisePropertyChanged("CouponEducationID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CouponExpiryDate {
            get {
                return this.CouponExpiryDateField;
            }
            set {
                if ((this.CouponExpiryDateField.Equals(value) != true)) {
                    this.CouponExpiryDateField = value;
                    this.RaisePropertyChanged("CouponExpiryDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CouponID {
            get {
                return this.CouponIDField;
            }
            set {
                if ((this.CouponIDField.Equals(value) != true)) {
                    this.CouponIDField = value;
                    this.RaisePropertyChanged("CouponID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CouponIssueDate {
            get {
                return this.CouponIssueDateField;
            }
            set {
                if ((this.CouponIssueDateField.Equals(value) != true)) {
                    this.CouponIssueDateField = value;
                    this.RaisePropertyChanged("CouponIssueDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CouponProduactID {
            get {
                return this.CouponProduactIDField;
            }
            set {
                if ((this.CouponProduactIDField.Equals(value) != true)) {
                    this.CouponProduactIDField = value;
                    this.RaisePropertyChanged("CouponProduactID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CouponType {
            get {
                return this.CouponTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.CouponTypeField, value) != true)) {
                    this.CouponTypeField = value;
                    this.RaisePropertyChanged("CouponType");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="PagedDiscountCoupon", Namespace="http://schemas.datacontract.org/2004/07/HCRGUniversityService.DTO.Paged")]
    [System.SerializableAttribute()]
    public partial class PagedDiscountCoupon : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon[] DiscountCoupansField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCouponForCourses[] DiscountCoupansForCourseField;
        
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
        public HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon[] DiscountCoupans {
            get {
                return this.DiscountCoupansField;
            }
            set {
                if ((object.ReferenceEquals(this.DiscountCoupansField, value) != true)) {
                    this.DiscountCoupansField = value;
                    this.RaisePropertyChanged("DiscountCoupans");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCouponForCourses[] DiscountCoupansForCourse {
            get {
                return this.DiscountCoupansForCourseField;
            }
            set {
                if ((object.ReferenceEquals(this.DiscountCoupansForCourseField, value) != true)) {
                    this.DiscountCoupansForCourseField = value;
                    this.RaisePropertyChanged("DiscountCoupansForCourse");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="NEPService.DiscountCouponService.IDiscountCouponService")]
    public interface IDiscountCouponService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/AddDiscountCoupon", ReplyAction="http://tempuri.org/IDiscountCouponService/AddDiscountCouponResponse")]
        int AddDiscountCoupon(HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon discountCoupon);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/AddDiscountCoupon", ReplyAction="http://tempuri.org/IDiscountCouponService/AddDiscountCouponResponse")]
        System.Threading.Tasks.Task<int> AddDiscountCouponAsync(HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon discountCoupon);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/UpdateDiscountCoupon", ReplyAction="http://tempuri.org/IDiscountCouponService/UpdateDiscountCouponResponse")]
        int UpdateDiscountCoupon(HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon discountCoupon);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/UpdateDiscountCoupon", ReplyAction="http://tempuri.org/IDiscountCouponService/UpdateDiscountCouponResponse")]
        System.Threading.Tasks.Task<int> UpdateDiscountCouponAsync(HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon discountCoupon);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/DeleteDiscountCoupon", ReplyAction="http://tempuri.org/IDiscountCouponService/DeleteDiscountCouponResponse")]
        void DeleteDiscountCoupon(int CouponID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/DeleteDiscountCoupon", ReplyAction="http://tempuri.org/IDiscountCouponService/DeleteDiscountCouponResponse")]
        System.Threading.Tasks.Task DeleteDiscountCouponAsync(int CouponID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/GetDiscountCouponByID", ReplyAction="http://tempuri.org/IDiscountCouponService/GetDiscountCouponByIDResponse")]
        HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon GetDiscountCouponByID(int CouponID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/GetDiscountCouponByID", ReplyAction="http://tempuri.org/IDiscountCouponService/GetDiscountCouponByIDResponse")]
        System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon> GetDiscountCouponByIDAsync(int CouponID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/getAllDiscountCoupons", ReplyAction="http://tempuri.org/IDiscountCouponService/getAllDiscountCouponsResponse")]
        HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon[] getAllDiscountCoupons();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/getAllDiscountCoupons", ReplyAction="http://tempuri.org/IDiscountCouponService/getAllDiscountCouponsResponse")]
        System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon[]> getAllDiscountCouponsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/GetDiscountCouponByCouponCode", ReplyAction="http://tempuri.org/IDiscountCouponService/GetDiscountCouponByCouponCodeResponse")]
        HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon GetDiscountCouponByCouponCode(string CouponCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/GetDiscountCouponByCouponCode", ReplyAction="http://tempuri.org/IDiscountCouponService/GetDiscountCouponByCouponCodeResponse")]
        System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon> GetDiscountCouponByCouponCodeAsync(string CouponCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/UpdateDiscountCouponStatus", ReplyAction="http://tempuri.org/IDiscountCouponService/UpdateDiscountCouponStatusResponse")]
        int UpdateDiscountCouponStatus(string couponCode, bool value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/UpdateDiscountCouponStatus", ReplyAction="http://tempuri.org/IDiscountCouponService/UpdateDiscountCouponStatusResponse")]
        System.Threading.Tasks.Task<int> UpdateDiscountCouponStatusAsync(string couponCode, bool value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/GetDiscountCouponForCourses", ReplyAction="http://tempuri.org/IDiscountCouponService/GetDiscountCouponForCoursesResponse")]
        HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCouponForCourses[] GetDiscountCouponForCourses(int ClientID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/GetDiscountCouponForCourses", ReplyAction="http://tempuri.org/IDiscountCouponService/GetDiscountCouponForCoursesResponse")]
        System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCouponForCourses[]> GetDiscountCouponForCoursesAsync(int ClientID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/GetDiscountCouponForProducts", ReplyAction="http://tempuri.org/IDiscountCouponService/GetDiscountCouponForProductsResponse")]
        HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCouponForCourses[] GetDiscountCouponForProducts(int ClientID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/GetDiscountCouponForProducts", ReplyAction="http://tempuri.org/IDiscountCouponService/GetDiscountCouponForProductsResponse")]
        System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCouponForCourses[]> GetDiscountCouponForProductsAsync(int ClientID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/GetAllPagedDiscountCoupon", ReplyAction="http://tempuri.org/IDiscountCouponService/GetAllPagedDiscountCouponResponse")]
        HCRGUniversityApp.NEPService.DiscountCouponService.PagedDiscountCoupon GetAllPagedDiscountCoupon(int skip, int take);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/GetAllPagedDiscountCoupon", ReplyAction="http://tempuri.org/IDiscountCouponService/GetAllPagedDiscountCouponResponse")]
        System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.PagedDiscountCoupon> GetAllPagedDiscountCouponAsync(int skip, int take);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/GetAllPagedDiscountCouponForCourses", ReplyAction="http://tempuri.org/IDiscountCouponService/GetAllPagedDiscountCouponForCoursesResp" +
            "onse")]
        HCRGUniversityApp.NEPService.DiscountCouponService.PagedDiscountCoupon GetAllPagedDiscountCouponForCourses(int skip, int take, int ClientID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDiscountCouponService/GetAllPagedDiscountCouponForCourses", ReplyAction="http://tempuri.org/IDiscountCouponService/GetAllPagedDiscountCouponForCoursesResp" +
            "onse")]
        System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.PagedDiscountCoupon> GetAllPagedDiscountCouponForCoursesAsync(int skip, int take, int ClientID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDiscountCouponServiceChannel : HCRGUniversityApp.NEPService.DiscountCouponService.IDiscountCouponService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DiscountCouponServiceClient : System.ServiceModel.ClientBase<HCRGUniversityApp.NEPService.DiscountCouponService.IDiscountCouponService>, HCRGUniversityApp.NEPService.DiscountCouponService.IDiscountCouponService {
        
        public DiscountCouponServiceClient() {
        }
        
        public DiscountCouponServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DiscountCouponServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DiscountCouponServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DiscountCouponServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int AddDiscountCoupon(HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon discountCoupon) {
            return base.Channel.AddDiscountCoupon(discountCoupon);
        }
        
        public System.Threading.Tasks.Task<int> AddDiscountCouponAsync(HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon discountCoupon) {
            return base.Channel.AddDiscountCouponAsync(discountCoupon);
        }
        
        public int UpdateDiscountCoupon(HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon discountCoupon) {
            return base.Channel.UpdateDiscountCoupon(discountCoupon);
        }
        
        public System.Threading.Tasks.Task<int> UpdateDiscountCouponAsync(HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon discountCoupon) {
            return base.Channel.UpdateDiscountCouponAsync(discountCoupon);
        }
        
        public void DeleteDiscountCoupon(int CouponID) {
            base.Channel.DeleteDiscountCoupon(CouponID);
        }
        
        public System.Threading.Tasks.Task DeleteDiscountCouponAsync(int CouponID) {
            return base.Channel.DeleteDiscountCouponAsync(CouponID);
        }
        
        public HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon GetDiscountCouponByID(int CouponID) {
            return base.Channel.GetDiscountCouponByID(CouponID);
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon> GetDiscountCouponByIDAsync(int CouponID) {
            return base.Channel.GetDiscountCouponByIDAsync(CouponID);
        }
        
        public HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon[] getAllDiscountCoupons() {
            return base.Channel.getAllDiscountCoupons();
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon[]> getAllDiscountCouponsAsync() {
            return base.Channel.getAllDiscountCouponsAsync();
        }
        
        public HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon GetDiscountCouponByCouponCode(string CouponCode) {
            return base.Channel.GetDiscountCouponByCouponCode(CouponCode);
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon> GetDiscountCouponByCouponCodeAsync(string CouponCode) {
            return base.Channel.GetDiscountCouponByCouponCodeAsync(CouponCode);
        }
        
        public int UpdateDiscountCouponStatus(string couponCode, bool value) {
            return base.Channel.UpdateDiscountCouponStatus(couponCode, value);
        }
        
        public System.Threading.Tasks.Task<int> UpdateDiscountCouponStatusAsync(string couponCode, bool value) {
            return base.Channel.UpdateDiscountCouponStatusAsync(couponCode, value);
        }
        
        public HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCouponForCourses[] GetDiscountCouponForCourses(int ClientID) {
            return base.Channel.GetDiscountCouponForCourses(ClientID);
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCouponForCourses[]> GetDiscountCouponForCoursesAsync(int ClientID) {
            return base.Channel.GetDiscountCouponForCoursesAsync(ClientID);
        }
        
        public HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCouponForCourses[] GetDiscountCouponForProducts(int ClientID) {
            return base.Channel.GetDiscountCouponForProducts(ClientID);
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCouponForCourses[]> GetDiscountCouponForProductsAsync(int ClientID) {
            return base.Channel.GetDiscountCouponForProductsAsync(ClientID);
        }
        
        public HCRGUniversityApp.NEPService.DiscountCouponService.PagedDiscountCoupon GetAllPagedDiscountCoupon(int skip, int take) {
            return base.Channel.GetAllPagedDiscountCoupon(skip, take);
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.PagedDiscountCoupon> GetAllPagedDiscountCouponAsync(int skip, int take) {
            return base.Channel.GetAllPagedDiscountCouponAsync(skip, take);
        }
        
        public HCRGUniversityApp.NEPService.DiscountCouponService.PagedDiscountCoupon GetAllPagedDiscountCouponForCourses(int skip, int take, int ClientID) {
            return base.Channel.GetAllPagedDiscountCouponForCourses(skip, take, ClientID);
        }
        
        public System.Threading.Tasks.Task<HCRGUniversityApp.NEPService.DiscountCouponService.PagedDiscountCoupon> GetAllPagedDiscountCouponForCoursesAsync(int skip, int take, int ClientID) {
            return base.Channel.GetAllPagedDiscountCouponForCoursesAsync(skip, take, ClientID);
        }
    }
}

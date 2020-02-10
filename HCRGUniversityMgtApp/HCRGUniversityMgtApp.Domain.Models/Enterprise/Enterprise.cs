using System;

namespace HCRGUniversityMgtApp.Domain.Models.Enterprise
{
    public class Enterprise : Base.BaseColumn 
    {
        public int EnterpriseID { get; set; }
        public string EnterpriseClientName { get; set; }
        public string EnterpriseAddress { get; set; }
        public string EnterpriseCity { get; set; }
        public int EnterpriseStateID { get; set; }
        public string EnterpriseZip { get; set; }
        public string EnterprisePhone { get; set; }
        public string EnterpriseEmail { get; set; }
        public decimal? EnterpriseCourseStartPrice { get; set; }
        public decimal? EnterpriseCourseEndPrice { get; set; }
        public bool? EnterpriseProgram { get; set; }
        public DateTime? EnterpriseEndDate { get; set; }
        public int EnterpriseNumberUser { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}

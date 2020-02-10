



namespace HCRGUniversityMgtApp.Domain.Models.CollegeModel
{
    public class College : Base.BaseColumn
    {
        public int CollegeID { get; set; }
        public string CollegeName { get; set; }
        public bool IsActive { get; set; }
        public bool flag { get; set; }

        public string Abbreviation { get; set; }
        public string StartNumber { get; set; }
        public string OrganizationName { get; set; }
    }
 
}

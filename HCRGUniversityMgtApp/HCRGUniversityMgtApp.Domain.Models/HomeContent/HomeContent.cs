
namespace HCRGUniversityMgtApp.Domain.Models.HomeContentModel
{
    public  class HomeContent : Base.BaseColumn
    {
        public int HomeContentID { get; set; }
        public string HomePageContent { get; set; }
        public string OrganizationName { get; set; }
        
        public string DescriptionShort { get; set; }

        public bool flag { get; set; }
        public string msg { get; set; }
    }
     
    }



namespace HCRGUniversityMgtApp.Domain.Models.AboutUsModel
{
    public class AboutUs : Base.BaseColumn
    {

        public int AboutUsID { get; set; }        

        public string Description { get; set; }

        public string DescriptionShort { get; set; }

        public bool flag { get; set; }

        public string OrganizationName { get; set; }
    }
}

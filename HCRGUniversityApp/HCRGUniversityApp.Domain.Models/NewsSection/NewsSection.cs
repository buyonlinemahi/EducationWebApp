using System;
namespace HCRGUniversityApp.Domain.Models.NewsSectionModel
{
    public class NewsSection
    {
        public Int64 NewsSectionID { get; set; }
        public string NewsSectionTitle { get; set; }
        public string NewsSectionType { get; set; }
        public string EncryptedNewsSectionID { get; set; }
    }
}

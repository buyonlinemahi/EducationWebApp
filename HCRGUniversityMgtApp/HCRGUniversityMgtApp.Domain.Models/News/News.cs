using System;

namespace HCRGUniversityMgtApp.Domain.Models.NewsModel
{
    public class News : Base.BaseColumn
    {
        public int NewsID { get; set; }
        public int NewsSectionID { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
        public string NewsType { get; set; }
        public DateTime NewsDate { get; set; }
        public bool NewsEditorPick { get; set; }
        public bool flag { get; set; }
        public string Type { get; set; }
        public string NewsDescriptionShort { get; set; }
        public string NewsAuthor { get; set; }
        public string msg { get; set; }
        public string OrganizationName { get; set; }
    }
}

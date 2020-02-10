using System;

namespace HCRGUniversityMgtApp.Domain.Models.NewsModel
{
    public class NewsSearchCarousel
    {
        public int NewsID { get; set; }
        public int NewsSectionID { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
        public string NewsType { get; set; }
        public bool NewsEditorPick { get; set; }
        public DateTime NewsDate { get; set; }
        public string NewsAuthor { get; set; }
        public string NewsSectionTitle { get; set; }
    }
}

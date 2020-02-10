using System;

namespace HCRGUniversityApp.Domain.Models.NewsModel
{
    public class NewsFullDetail
    {
        public Int64 NewsID { get; set; }
        public string EncryptedNewsID { get; set; }
        public int NewsSectionID { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
        public string NewsType { get; set; }
        public bool NewsEditorPick { get; set; }
        public DateTime NewsDate { get; set; }
        public string NewsSectionTitle { get; set; }
        public int PhotoVideoId { get; set; }
        public string PhotoVideoTitle { get; set; }
        public string NewsAuthor { get; set; }

    }

}

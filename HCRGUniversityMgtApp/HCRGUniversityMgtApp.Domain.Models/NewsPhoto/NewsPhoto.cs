using System.Web;

namespace HCRGUniversityMgtApp.Domain.Models.NewsPhotoModel
{
  public  class NewsPhoto
    {
        public int NewsPhotoID { get; set; }
        public int NewsID { get; set; }
        public string NewsPhotos { get; set; }
        public HttpPostedFileBase NewsPhotoFile { get; set; }
    }
}

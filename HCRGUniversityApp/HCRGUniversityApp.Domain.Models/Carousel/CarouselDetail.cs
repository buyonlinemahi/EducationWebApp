
using System;
namespace HCRGUniversityApp.Domain.Models.Carousel
{
    public class CarouselDetail
    {
        public string CarouselTitle { get; set; }
        public string CarouselDescription { get; set; }
        public string CarouselBgColor { get; set; }
        public string NewsPhotoUrl { get; set; }        
        public string CarouselPhotoUrl { get; set; }
        public string NewsTitle { get; set; }
        public string NewsType { get; set; }
        public Int64 NewsID { get; set; }
        public string EncryptedNewsID { get; set; }
    }
}

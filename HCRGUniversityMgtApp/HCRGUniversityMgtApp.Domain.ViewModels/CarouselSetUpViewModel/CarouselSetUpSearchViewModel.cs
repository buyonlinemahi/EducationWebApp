using HCRGUniversityMgtApp.Domain.Models.CarouselSetUp;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.CarouselSetUpViewModel
{
    public class CarouselSetUpSearchViewModel
    {
       
        public IEnumerable<CarouselSetUp> carouselSetupResult { get; set; }
        public CarouselSetUp carouselSetup { get; set; }
        public bool IsHCRGAdmin { get; set; }

    }
}

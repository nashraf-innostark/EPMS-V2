using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class HomePageResponse
    {
        public IEnumerable<ImageSlider> ImageSlider { get; set; }
        public IEnumerable<Partner> Partners { get; set; }
    }
}

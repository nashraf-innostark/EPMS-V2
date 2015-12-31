using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers.Website.HomePage
{
    public static class WebsiteHomePageMapper
    {
        public static WebsiteHomePage CreateFromClientToServer(this WebsiteModels.WebsiteHomePage source)
        {
            return new WebsiteHomePage
            {
                Id = source.Id,
                WebsiteLogoPath = source.WebsiteLogoPath,
                ShowProductPrice = source.ShowProductPrice
            };
        }

        public static WebsiteModels.WebsiteHomePage CreateFromServerToClient(this WebsiteHomePage source)
        {
            return new WebsiteModels.WebsiteHomePage
            {
                Id = source.Id,
                WebsiteLogoPath = source.WebsiteLogoPath,
                ShowProductPrice = source.ShowProductPrice
            };
        }
    }
}

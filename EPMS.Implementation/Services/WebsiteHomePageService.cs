using System;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    class WebsiteHomePageService : IWebsiteHomePageService
    {
        #region Private

        private readonly IWebsiteDepartmentService websiteDepartmentService;
        private readonly IPartnerService partnerService;
        private readonly IWebsiteHomePageRepository repository;

        #endregion

        #region Constructor

        public WebsiteHomePageService(IWebsiteDepartmentService websiteDepartmentService, IPartnerService partnerService, IWebsiteHomePageRepository repository)
        {
            this.websiteDepartmentService = websiteDepartmentService;
            this.partnerService = partnerService;
            this.repository = repository;
        }

        #endregion

        #region Public

        public WebsiteHomePage GetHomePageLogo()
        {
            return repository.GetHomePageLogo();
        }

        public WebsiteHomeResponse WebsiteHomeResponse()
        {
            WebsiteHomeResponse response = new WebsiteHomeResponse
            {
                WebsiteDepartments = websiteDepartmentService.GetAll(),
                Partners = partnerService.GetAll(),
            };
            return response;
        }

        public bool SaveLogo(WebsiteHomePage homePage)
        {
            try
            {
                var dbLogo = repository.GetHomePageLogo();
                if (dbLogo != null)
                {
                    dbLogo.WebsiteLogoPath = homePage.WebsiteLogoPath;
                    repository.Update(dbLogo);
                    repository.SaveChanges();
                }
                else
                {
                    repository.Add(homePage);
                    repository.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddWebsiteLogo(WebsiteHomePage homePage)
        {
            try
            {
                repository.Add(homePage);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateWebsiteLogo(WebsiteHomePage homePage)
        {
            try
            {
                repository.Update(homePage);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

    }
}

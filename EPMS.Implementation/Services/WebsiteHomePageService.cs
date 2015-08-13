using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    class WebsiteHomePageService : IWebsiteHomePageService
    {
        #region Private

        private readonly IWebsiteDepartmentService _websiteDepartmentService;
        private readonly IPartnerService _partnerService;

        #endregion

        #region Constructor

        public WebsiteHomePageService(IWebsiteDepartmentService websiteDepartmentService, IPartnerService partnerService)
        {
            _websiteDepartmentService = websiteDepartmentService;
            _partnerService = partnerService;
        }

        #endregion

        #region Public

        public WebsiteHomeResponse websiteHomeResponse()
        {
            WebsiteHomeResponse response = new WebsiteHomeResponse();
            response.WebsiteDepartments = _websiteDepartmentService.GetAll();
            response.Partners = _partnerService.GetAll();
            return response;
        }

        #endregion

    }
}

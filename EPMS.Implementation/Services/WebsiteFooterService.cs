using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Models.MenuModels;

namespace EPMS.Implementation.Services
{
    public class WebsiteFooterService : IWebsiteFooterService
    {
        private readonly INewsAndArticleService newsAndArticleService;
        private readonly IContactUsService contactUsService;
        private readonly IWebsiteDepartmentService departmentService;

        public WebsiteFooterService(INewsAndArticleService newsAndArticleService, IContactUsService contactUsService, IWebsiteDepartmentService departmentService)
        {
            this.newsAndArticleService = newsAndArticleService;
            this.contactUsService = contactUsService;
            this.departmentService = departmentService;
        }


        public WebsiteFooterMenuModel LoadFooterMenu()
        {
            WebsiteFooterMenuModel footerMenuModel = new WebsiteFooterMenuModel
            {
                ContactUs = contactUsService.GetDetail(),
                Departments = departmentService.GetAll().ToList(),
                NewsAndArticles = newsAndArticleService.GetAll().Where(y=>!(y.Type)).OrderByDescending(x => x.RecCreatedDt).Take(2)
            };
            return footerMenuModel;
        }
    }
}

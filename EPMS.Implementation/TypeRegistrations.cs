using EPMS.Implementation.Identity;
using EPMS.Implementation.Services;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.IdentityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;

namespace EPMS.Implementation
{
    public static class TypeRegistrations
    {
        public static void RegisterType(IUnityContainer unityContainer)
        {
            UnityConfig.UnityContainer = unityContainer;
            Repository.TypeRegistrations.RegisterType(unityContainer);
            unityContainer.RegisterType<IMenuRightsService, MenuRightsService>();
            unityContainer.RegisterType<ILogger, LoggerService>();
            unityContainer.RegisterType<IMenuRightsService, MenuRightsService>();
            unityContainer.RegisterType<IEmployeeService, EmployeeService>();
            unityContainer.RegisterType<IJobTitleService, JobTitleService>();
            unityContainer.RegisterType<IDepartmentService, DepartmentService>();
            unityContainer.RegisterType<IEmployeeRequestService, EmployeeRequestService>();
            unityContainer.RegisterType<IAllowanceService, AllowanceService>();
            unityContainer.RegisterType<IAspNetUserService, AspNetUserService>();
            unityContainer.RegisterType<IJobOfferedService, JobOfferedService>();
            unityContainer.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            unityContainer.RegisterType<IJobApplicantService, JobApplicantService>();
            unityContainer.RegisterType<IComplaintService, ComplaintService>();
            unityContainer.RegisterType<ICustomerService, CustomerService>();
            unityContainer.RegisterType<IOrdersService, OrdersService>();
            unityContainer.RegisterType<IQuotationService, QuotationService>();
            unityContainer.RegisterType<IQuotationItemService, QuotationItemService>();
            unityContainer.RegisterType<ICompanyProfileService, CompanyProfileService>();
            unityContainer.RegisterType<IPayrollService, PayrollService>();
            unityContainer.RegisterType<ICompanyDocumentService, CompanyDocumentService>();
            unityContainer.RegisterType<ICompanyBankService, CompanyBankService>();
            unityContainer.RegisterType<ICompanySocialService, CompanySocialService>();
            unityContainer.RegisterType<IProjectService, ProjectService>();
            unityContainer.RegisterType<IProjectDocumentService, ProjectDocumentService>();
            unityContainer.RegisterType<IProjectTaskService, ProjectTaskService>();
            unityContainer.RegisterType<IMeetingService, MeetingService>();
            unityContainer.RegisterType<IMeetingAttendeeService, MeetingAttendeeService>();
            unityContainer.RegisterType<IMeetingDocumentService, MeetingDocumentService>();
            unityContainer.RegisterType<ITaskEmployeeService, TaskEmployeeService>();
            unityContainer.RegisterType<IUserPrefrencesService, UserPrefrencesService>();
            unityContainer.RegisterType<INotificationService, NotificationService>();
            unityContainer.RegisterType<IDashboardWidgetPreferencesService, DashboardWidgetPreferencesService>();
            unityContainer.RegisterType<IQuickLaunchItemService, QuickLaunchItemService>();
            unityContainer.RegisterType<ILicenseControlPanelService, LicenseControlPanelService>();
            unityContainer.RegisterType<IVendorService, VendorService>();
            unityContainer.RegisterType<IVendorItemsService, VendorItemsService>();
            unityContainer.RegisterType<IInventoryDepartmentService, InventoryDepartmentService>();
            unityContainer.RegisterType<IInventoryItemService, InventoryItemService>();
            unityContainer.RegisterType<IColorService, ColorService>();
            unityContainer.RegisterType<ISizeService, SizeService>();
            unityContainer.RegisterType<IStatusService, StatusService>();
            unityContainer.RegisterType<IManufacturerService, ManufacturerService>();
            unityContainer.RegisterType<IItemImageService, ItemImageService>();
            unityContainer.RegisterType<IWarehouseService, WarehouseService>();
            unityContainer.RegisterType<IItemVariationService, ItemVariationService>();
            unityContainer.RegisterType<IRFIService, RFIService>();
            unityContainer.RegisterType<IWarehouseDetailService, WarehouseDetailService>();
            unityContainer.RegisterType<IItemReleaseService, ItemReleaseService>();
            unityContainer.RegisterType<IRIFService, RIFService>();
            unityContainer.RegisterType<IDIFService, DIFService>();
            unityContainer.RegisterType<IItemManufacturerService, ItemManufacturerService>();
            unityContainer.RegisterType<ITIRService, TIRService>();
            unityContainer.RegisterType<ITIRItemService, TIRItemService>();
            unityContainer.RegisterType<IPurchaseOrderService, PurchaseOrderService>();
            unityContainer.RegisterType<IPoItemService, PoItemService>();
            unityContainer.RegisterType<IItemWarehouseService, ItemWarehouseService>();
            unityContainer.RegisterType<IPhysicalCountService, PhysicalCountService>();
            unityContainer.RegisterType<IImageSliderService, ImageSliderService>();
            unityContainer.RegisterType<INewsAndArticleService, NewsAndArticleService>();
            unityContainer.RegisterType<IPartnerService, PartnerService>();
            unityContainer.RegisterType<IWebsiteDepartmentService, WebsiteDepartmentService>();
            unityContainer.RegisterType<IProductSectionService, ProductSectionService>();
            unityContainer.RegisterType<IProductService, ProductService>();
            unityContainer.RegisterType<IProductImageService, ProductImageService>();
            unityContainer.RegisterType<IContactUsService, ContactUsService>();
            unityContainer.RegisterType<IWebsiteMenuService, WebsiteMenuService>();
            unityContainer.RegisterType<IWebsiteHomePageService, WebsiteHomePageService>();
            unityContainer.RegisterType<IAboutUsService, AboutUsService>();
            unityContainer.RegisterType<IWebsiteServicesService, WebsiteServicesService>();
            unityContainer.RegisterType<IWebsiteFooterService, WebsiteFooterService>();
            unityContainer.RegisterType<IShoppingCartService, ShoppingCartService>();
            unityContainer.RegisterType<IWebsiteServicesService, WebsiteServicesService>();
            unityContainer.RegisterType<IWebsiteCustomerService, WebsiteCustomerService>();
            unityContainer.RegisterType<IShoppingCartItemService, ShoppingCartItemService>();
            unityContainer.RegisterType<IWebsiteUserPreferenceService, WebsiteUserPreferenceService>();
        }
    }
}

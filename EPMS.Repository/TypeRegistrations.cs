using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Repository.BaseRepository;
using EPMS.Repository.Repositories;
using Microsoft.Practices.Unity;

namespace EPMS.Repository
{
    public static class TypeRegistrations
    {
        public static void RegisterType(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IMenuRightRepository, MenuRightRepository>();
            unityContainer.RegisterType<IMenuRepository, MenuRepository>();
            unityContainer.RegisterType<IEmployeeRepository, EmployeeRepository>();
            unityContainer.RegisterType<IJobTitleRepository, JobTitleRepository>();
            unityContainer.RegisterType<IDepartmentRepository, DepartmentRepository>();
            unityContainer.RegisterType<IEmployeeRequestRepository, EmployeeRequestRepository>();
            unityContainer.RegisterType<IAllowanceRepository, AllowanceRepository>();
            unityContainer.RegisterType<IAspNetUserRepository, AspNetUserRepository>();
            unityContainer.RegisterType<IEmployeeRequestDetailRepository, EmployeeRequestDetailRepository>();
            unityContainer.RegisterType<DbContext, BaseDbContext>(new PerRequestLifetimeManager());
            unityContainer.RegisterType<IJobOfferedRepository, JobOfferedRepository>();
            unityContainer.RegisterType<IJobApplicantRepository, JobApplicantRepository>();
            unityContainer.RegisterType<IEmployeeJobHistoryRepository, EmployeeJobHistoryRepository>();
            unityContainer.RegisterType<IJobTitleHistoryRepository, JobTitleHistoryRepository>();
            unityContainer.RegisterType<IComplaintRepository, ComplaintRepository>();
            unityContainer.RegisterType<ICustomerRepository, CustomerRepository>();
            unityContainer.RegisterType<IOrdersRepository, OrdersRepository>();
            unityContainer.RegisterType<IQuotationRepository, QuotationRepository>();
            unityContainer.RegisterType<IQuotationItemRepository, QuotationItemRepository>();
            unityContainer.RegisterType<ICompanyProfileRepository, CompanyProfileRepository>();
            unityContainer.RegisterType<ICompanyBankRepository, CompanyBankRepository>();
            unityContainer.RegisterType<ICompanyDocumentRepository, CompanyDocumentRepository>();
            unityContainer.RegisterType<ICompanySocialRepository, CompanySocialRepository>();
            unityContainer.RegisterType<IProjectRepository, ProjectRepository>();
            unityContainer.RegisterType<IProjectDocumentRepository, ProjectDocumentRepository>();
            unityContainer.RegisterType<IProjectTaskRepository, ProjectTaskRepository>();
            unityContainer.RegisterType<IMeetingRepository, MeetingRepository>();
            unityContainer.RegisterType<IMeetingAttendeeRepository, MeetingAttendeeRepository>();
            unityContainer.RegisterType<IMeetingDocumentRepository, MeetingDocumentRepository>();
            unityContainer.RegisterType<ITaskEmployeeRepository, TaskEmployeeRepository>();
            unityContainer.RegisterType<IUserPrefrencesRepository, UserPrefrencesRepository>();
            unityContainer.RegisterType<INotificationRepository, NotificationRepository>();
            unityContainer.RegisterType<IDashboardWidgetPreferencesRepository, DashboardWidgetPreferencesRepository>();
            unityContainer.RegisterType<IQuickLaunchItemRepository, QuickLaunchItemRepository>();
            unityContainer.RegisterType<ILicenseControlPanelRepository, LicenseControlPanelRepository>();
            unityContainer.RegisterType<INotificationRecipientRepository, NotificationRecipientRepository>();
            unityContainer.RegisterType<IVendorRepository, VendorRepository>();
            unityContainer.RegisterType<IVendorItemsRepository, VendorItemsRepository>();
            unityContainer.RegisterType<IApplicantQualificationRepository, ApplicantQualificationRepository>();
            unityContainer.RegisterType<IApplicantExperienceRepository, ApplicantExperienceRepository>();
            unityContainer.RegisterType<IInventoryDepartmentRepository, InventoryDepartmentRepository>();
            unityContainer.RegisterType<IInventoryItemRepository, InventoryItemRepository>();
            unityContainer.RegisterType<IColorRepository, ColorRepository>();
            unityContainer.RegisterType<ISizeRepository, SizeRepository>();
            unityContainer.RegisterType<IStatusRepository, StatusRepository>();
            unityContainer.RegisterType<IItemImageRepository, ItemImageRepository>();
            unityContainer.RegisterType<IManufacturerRepository, ManufacturerRepository>();
            unityContainer.RegisterType<IWarehouseRepository, WarehouseRepository>();
            unityContainer.RegisterType<IItemVariationRepository, ItemVariationRepository>();
            unityContainer.RegisterType<IRFIRepository, RFIRepository>();
            unityContainer.RegisterType<IRFIItemRepository, RFIItemRepository>();
            unityContainer.RegisterType<IWarehouseDetailRepository, WarehouseDetailRepository>();
            unityContainer.RegisterType<IRIFRepository, RIFRepository>();
            unityContainer.RegisterType<IRIFItemRepository, RIFItemRepository>();
            unityContainer.RegisterType<IItemReleaseRepository, ItemReleaseRepository>();
            unityContainer.RegisterType<IItemReleaseDetailRepository, ItemReleaseDetailRepository>();
            unityContainer.RegisterType<IDIFRepository, DIFRepository>();
            unityContainer.RegisterType<IItemManufacturerRepository, ItemManufacturerRepository>();
            unityContainer.RegisterType<IDIFItemRepository, DIFItemRepository>();
            unityContainer.RegisterType<ITIRRepository, TIRRepository>();
            unityContainer.RegisterType<ITIRItemRepository, TIRItemRepository>();
            unityContainer.RegisterType<IPurchaseOrderRepository, PurchaseOrderRepository>();
            unityContainer.RegisterType<IPoItemRepository, PoItemRepository>();
        }
    }
}

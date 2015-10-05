using System.Data.Entity;
using EPMS.Models.DomainModels;
using EPMS.Models.LoggerModels;
using EPMS.Models.MenuModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.BaseRepository
{
    /// <summary>
    /// Base Db Context. Implements Identity Db Context over Application User
    /// </summary>
    public sealed class BaseDbContext : DbContext
    {
        #region Private
        private IUnityContainer container;
        #endregion
        #region Protected
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //base.OnModelCreating(modelBuilder);
        //    //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    //modelBuilder.Entity<Product>().HasKey(p => p.Id);
        //    //modelBuilder.Entity<Product>().Property(c => c.Id)
        //    //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        //}
        #endregion
        #region Constructor
        public BaseDbContext()
        {
        }
        #endregion
        #region Public

        public BaseDbContext(string connectionString, IUnityContainer container)
            : base(connectionString)
        {
            this.container = container;
        }
        //#region Logger

        /// <summary>
        /// Logs
        /// </summary>
        public DbSet<Log> Logs { get; set; }
        /// <summary>
        /// Log Categories
        /// </summary>
        public DbSet<LogCategory> LogCategories { get; set; }
        /// <summary>
        /// Category Logs
        /// </summary>
        public DbSet<EPMS.Models.LoggerModels.CategoryLog> CategoryLogs { get; set; }
        #endregion
        #region Menu Rights and Security
        /// <summary>
        /// Menu Rights
        /// </summary>
        public DbSet<MenuRight> MenuRights { get; set; }
        /// <summary>
        /// Menu
        /// </summary>
        public DbSet<Menu> Menus { get; set; }
        #endregion
        /// <summary>
        /// Users
        /// </summary>
        public DbSet<AspNetUser> Users { get; set; }

        /// <summary>
        /// User Roles
        /// </summary>
        public DbSet<AspNetRole> UserRoles { get; set; }
        public DbSet<AspNetUserClaim> UserClaims { get; set; }
        public DbSet<AspNetUserLogin> UserLogins { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobTitle> JobTitleses { get; set; }
        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<EmployeeRequest> EmployeeRequests { get; set; }
        public DbSet<RequestDetail> RequestDetails { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<JobTitleHistory> JobTitleHistory { get; set; }
        public DbSet<EmployeeJobHistory> EmployeeJobHistory { get; set; }
        public DbSet<JobOffered> JobsOffered { get; set; }
        public DbSet<JobApplicant> JobApplicants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Complaint> Complaint { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectDocument> ProjectDocuments { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<QuotationItemDetail> QuotationItemDetails { get; set; }
        public DbSet<CompanyProfile> Profile { get; set; }
        public DbSet<CompanyBankDetail> Bank { get; set; }
        public DbSet<CompanySocialDetail> Social { get; set; }
        public DbSet<CompanyDocumentDetail> Document { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<TaskEmployee> TaskEmployees { get; set; }
        public DbSet<PreRequisitTask> PreRequisitTasks { get; set; }
        public DbSet<MeetingDocument> MeetingDocuments { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingAttendee> MeetingAttendees { get; set; }
        public DbSet<UserPrefrence> UserPrefrence { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<NotificationRecipient> NotificationRecipient { get; set; }
        public DbSet<DashboardWidgetPreference> Preferences { get; set; }
        public DbSet<QuickLaunchItem> QuickLaunchItems { get; set; }
        public DbSet<LicenseControlPanel> LicenseControlPanels { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorItem> VendorItems { get; set; }
        public DbSet<ApplicantQualification> ApplicantQualifications { get; set; }
        public DbSet<ApplicantExperience> ApplicantExperiences { get; set; }
        public DbSet<InventoryDepartment> InventoryDepartments { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<ItemManufacturer> ItemManufacturers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ItemVariation> ItemVariations { get; set; }
        public DbSet<RFI> RFI { get; set; }
        public DbSet<RFIItem> RFIItem { get; set; }
        public DbSet<WarehouseDetail> WarehouseDetails { get; set; }
        public DbSet<RIF> RIF { get; set; }
        public DbSet<RIFItem> RIFItem { get; set; }
        public DbSet<ItemRelease> ItemReleases { get; set; }
        public DbSet<ItemReleaseDetail> ItemReleaseDetails { get; set; }
        public DbSet<DIF> DIF { get; set; }
        public DbSet<DIFItem> DIFItem { get; set; }
        public DbSet<TIR> TIR { get; set; }
        public DbSet<TIRItem> TIRItem { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<DIFHistory> DifHistories { get; set; }
        public DbSet<DIFItemHistory> DifItemHistories { get; set; }
        public DbSet<ItemReleaseHistory> ReleaseHistories { get; set; }
        public DbSet<ItemReleaseDetailHistory> ReleaseDetailHistories { get; set; }
        public DbSet<RFIHistory> RfiHistories { get; set; }
        public DbSet<ItemBarcode> ItemBarcode { get; set; }
        public DbSet<ItemWarehouse> ItemWarehouses { get; set; }
        public DbSet<RIFHistory> RifHistories { get; set; }
        public DbSet<RIFItemHistory> RifItemHistories { get; set; }
        public DbSet<TIRHistory> TirHistories { get; set; }
        public DbSet<TIRItemHistory> TirItemHistories { get; set; }
        public DbSet<ItemReleaseQuantity> ItemReleaseQuantities { get; set; }
        public DbSet<PurchaseOrderHistory> PurchaseOrderHistories { get; set; }
        public DbSet<PhysicalCount> PhysicalCount { get; set; }
        public DbSet<PhysicalCountItem> PhysicalCountItems { get; set; }
        public DbSet<ImageSlider> ImageSliders { get; set; }
        public DbSet<NewsAndArticle> NewsAndArticles { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<WebsiteDepartment> WebsiteDepartments { get; set; }
        public DbSet<ProductSection> ProductSections { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<WebsiteService> WebsiteServices { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<WebsiteCustomer> WebsiteCustomers { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<WebsiteHomePage> WebsiteHomePages { get; set; }
        public DbSet<WebsiteUserPrefrence> WebsiteUserPrefrences { get; set; }
        public DbSet<RFQ> Rfqs { get; set; }
    }
}

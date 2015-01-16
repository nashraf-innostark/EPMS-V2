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

        public BaseDbContext(string connectionString,IUnityContainer container)
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
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<QuotationItemDetail> QuotationItemDetails { get; set; }
        public DbSet<CompanyProfile> Profile { get; set; }
        public DbSet<CompanyBankDetail> Bank { get; set; }
        public DbSet<CompanySocialDetail> Social { get; set; }
        public DbSet<CompanyDocumentDetail> Document { get; set; }
    }
}

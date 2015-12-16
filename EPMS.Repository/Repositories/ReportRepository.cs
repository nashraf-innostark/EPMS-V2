﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;
using EPMS.Models.ResponseModels.ReportsResponseModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        #region Constructor

        public ReportRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Report> DbSet
        {
            get { return db.Report; }
        }

        #endregion

        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements for Project
        /// </summary>
        private readonly Dictionary<ProjectReportByColumn, Func<Report, object>> projectReportClause =

            new Dictionary<ProjectReportByColumn, Func<Report, object>>
            {
                {ProjectReportByColumn.Serial, c => c.ReportId},
                {ProjectReportByColumn.ReportId, c => c.ReportId},
                {ProjectReportByColumn.ReportCreatedBy, c => c.AspNetUser.Employee.EmployeeFirstNameE},
                {ProjectReportByColumn.ReportType, c => c.Project.NameE},
                {ProjectReportByColumn.ReportDateRange, c => c.ReportFromDate},
                {ProjectReportByColumn.ReportCreatedDate, c => c.ReportCreatedDate}
            };

        //For Customer Services Reports
        private readonly Dictionary<CustomerReportByColumn, Func<Report, object>> customerReportClause =

            new Dictionary<CustomerReportByColumn, Func<Report, object>>
            {
                {CustomerReportByColumn.Serial, c => c.ReportId},
                {CustomerReportByColumn.ReportId, c => c.ReportId},
                {CustomerReportByColumn.ReportCreatedBy, c => c.AspNetUser.Employee.EmployeeFirstNameE},
                {CustomerReportByColumn.ReportType, c => c.Employee.EmployeeFirstNameE},
                {CustomerReportByColumn.ReportDateRange, c => c.ReportFromDate},
                {CustomerReportByColumn.ReportCreatedDate, c => c.ReportCreatedDate}
            };

        /// <summary>
        /// Order by Column Names Dictionary statements for Task
        /// </summary>
        private readonly Dictionary<TaskReportByColumn, Func<Report, object>> taskReportClause =

            new Dictionary<TaskReportByColumn, Func<Report, object>>
            {
                {TaskReportByColumn.Serial, c => c.ReportId},
                {TaskReportByColumn.ReportId, c => c.ReportId},
                {TaskReportByColumn.ReportCreatedBy, c => c.AspNetUser.Employee.EmployeeFirstNameE},
                {TaskReportByColumn.ReportType, c => c.Project.NameE + c.ProjectTask.TaskNameE},
                {TaskReportByColumn.ReportDateRange, c => c.ReportFromDate},
                {TaskReportByColumn.ReportCreatedDate, c => c.ReportCreatedDate}
            };

        private readonly Dictionary<ProjectReportByColumn, Func<Report, object>> warehouseReportClause =

            new Dictionary<ProjectReportByColumn, Func<Report, object>>
            {
                {ProjectReportByColumn.Serial, c => c.ReportId},
                {ProjectReportByColumn.ReportId, c => c.ReportId},
                {ProjectReportByColumn.ReportCreatedBy, c => c.AspNetUser.Employee.EmployeeFirstNameE},
                {ProjectReportByColumn.ReportType, c => c.Warehouse.WarehouseNumber},
                {ProjectReportByColumn.ReportDateRange, c => c.ReportFromDate},
                {ProjectReportByColumn.ReportCreatedDate, c => c.ReportCreatedDate}
            };

        private readonly Dictionary<ProjectReportByColumn, Func<Report, object>> vendorReportClause =

            new Dictionary<ProjectReportByColumn, Func<Report, object>>
            {
                {ProjectReportByColumn.Serial, c => c.ReportId},
                {ProjectReportByColumn.ReportId, c => c.ReportId},
                {ProjectReportByColumn.ReportCreatedBy, c => c.AspNetUser.Employee.EmployeeFirstNameE},
                {ProjectReportByColumn.ReportType, c => c.Warehouse.WarehouseNumber},
                {ProjectReportByColumn.ReportDateRange, c => c.ReportFromDate},
                {ProjectReportByColumn.ReportCreatedDate, c => c.ReportCreatedDate}
            };

        #endregion

        public ReportsListRequestResponse GetProjectsReports(ProjectReportSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayLength;
            long reportId = 0;
            if (!string.IsNullOrEmpty(searchRequest.SearchString))
                Int64.TryParse(searchRequest.SearchString, out reportId);
            int reportCategory = (int) ReportCategory.Project;
            int allReportCategory = (int) ReportCategory.AllProjects;
            Expression<Func<Report, bool>> query =
                s =>
                    (s.ReportCategoryId.Equals(reportCategory) || s.ReportCategoryId.Equals(allReportCategory)) &&
                    ((string.IsNullOrEmpty(searchRequest.SearchString))
                     ||
                     (s.ReportId.Equals(reportId)) ||
                     (s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString)) ||
                     (s.Project.NameE.Contains(searchRequest.SearchString)) ||
                     (s.Project.NameA.Contains(searchRequest.SearchString))
                        );

            IEnumerable<Report> queryData = searchRequest.sSortDir_0 == "asc"
                ? DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                    .Where(query)
                    .OrderBy(projectReportClause[searchRequest.RequestByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                    .Where(query)
                    .OrderByDescending(projectReportClause[searchRequest.RequestByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

            return new ReportsListRequestResponse
            {
                Reports = queryData.ToList(),
                FilteredCount = DbSet.Count(query),
                TotalCount =
                    DbSet.Count(
                        s => s.ReportCategoryId.Equals(reportCategory) || s.ReportCategoryId.Equals(allReportCategory))
            };
        }

        public TaskReportsListRequestResponse GetTasksReports(TaskReportSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayLength;
            long reportId = 0;
            if (!string.IsNullOrEmpty(searchRequest.SearchString))
                Int64.TryParse(searchRequest.SearchString, out reportId);
            int projectTaskReportCategory = (int) ReportCategory.ProjectTask;
            int projectAllTasksReportCategory = (int) ReportCategory.ProjectAllTasks;
            int allProjectsAllTasksReportCategory = (int) ReportCategory.AllProjectsAllTasks;

            Expression<Func<Report, bool>> query =
                s =>
                    (s.ReportCategoryId.Equals(projectTaskReportCategory) ||
                     s.ReportCategoryId.Equals(projectAllTasksReportCategory)
                     || s.ReportCategoryId.Equals(allProjectsAllTasksReportCategory)) &&
                    ((string.IsNullOrEmpty(searchRequest.SearchString)) ||
                     (s.ReportId.Equals(reportId)) ||
                     (s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString)) ||
                     (s.Project.NameE.Contains(searchRequest.SearchString)) ||
                     (s.Project.NameA.Contains(searchRequest.SearchString))
                        );

            IEnumerable<Report> queryData = searchRequest.sSortDir_0 == "asc"
                ? DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                    .Where(query)
                    .OrderBy(taskReportClause[searchRequest.RequestByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                    .Where(query)
                    .OrderByDescending(taskReportClause[searchRequest.RequestByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

            return new TaskReportsListRequestResponse
            {
                Tasks = queryData.ToList(),
                FilteredCount = DbSet.Count(query),
                TotalCount =
                    DbSet.Count(
                        s =>
                            s.ReportCategoryId.Equals(projectTaskReportCategory) ||
                            s.ReportCategoryId.Equals(projectAllTasksReportCategory) ||
                            s.ReportCategoryId.Equals(allProjectsAllTasksReportCategory))
            };
        }

        public ReportsListRequestResponse GetWarehousesReports(WarehouseReportSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayLength;
            long reportId = 0;
            if (!string.IsNullOrEmpty(searchRequest.SearchString))
                Int64.TryParse(searchRequest.SearchString, out reportId);
            int reportCategory = (int) ReportCategory.Warehouse;
            int allReportCategory = (int) ReportCategory.AllWarehouse;
            Expression<Func<Report, bool>> query =
                s => (s.ReportCategoryId.Equals(reportCategory) || s.ReportCategoryId.Equals(allReportCategory)) &&
                     ((string.IsNullOrEmpty(searchRequest.SearchString)) ||
                      (s.ReportId.Equals(reportId)) ||
                      (s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString)) ||
                      (s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString)) ||
                      (s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString)) ||
                      (s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString)) ||
                      (s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString)) ||
                      (s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString)) ||
                      (s.Warehouse.WarehouseNumber.Contains(searchRequest.SearchString)) ||
                      (s.Warehouse.WarehouseNumber.Contains(searchRequest.SearchString))
                         );

            IEnumerable<Report> queryData = searchRequest.sSortDir_0 == "asc"
                ? DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                    .Where(query)
                    .OrderBy(warehouseReportClause[searchRequest.RequestByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                    .Where(query)
                    .OrderByDescending(warehouseReportClause[searchRequest.RequestByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

            return new ReportsListRequestResponse
            {
                Reports = queryData.ToList(),
                FilteredCount = DbSet.Count(query),
                TotalCount =
                    DbSet.Count(
                        s => s.ReportCategoryId.Equals(reportCategory) || s.ReportCategoryId.Equals(allReportCategory))
            };
        }

        public ReportsListRequestResponse GetInventoryItemsReports(WarehouseReportSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayLength;
            long reportId = 0;
            if (!string.IsNullOrEmpty(searchRequest.SearchString))
                Int64.TryParse(searchRequest.SearchString, out reportId);
            int reportCategory = (int)ReportCategory.Item;
            int allReportCategory = (int)ReportCategory.AllItems;
            Expression<Func<Report, bool>> query =
                s => (s.ReportCategoryId.Equals(reportCategory) || s.ReportCategoryId.Equals(allReportCategory)) &&
                    ((string.IsNullOrEmpty(searchRequest.SearchString)) ||
                    (s.ReportId.Equals(reportId)) ||
                    (s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString)) ||
                    (s.ReportInventoryItems.Any(x => x.NameA.Contains(searchRequest.SearchString))) ||
                     (s.ReportInventoryItems.Any(x => x.NameE.Contains(searchRequest.SearchString)))
                    );

            IEnumerable<Report> queryData = searchRequest.sSortDir_0 == "asc" ?
                DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                .Where(query).OrderBy(warehouseReportClause[searchRequest.RequestByColumn]).Skip(fromRow).Take(toRow).ToList() :

                DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                .Where(query).OrderByDescending(warehouseReportClause[searchRequest.RequestByColumn]).Skip(fromRow).Take(toRow).ToList();

            return new ReportsListRequestResponse
            {
                Reports = queryData.ToList(),
                FilteredCount = DbSet.Count(query),
                TotalCount = DbSet.Count(s => s.ReportCategoryId.Equals(reportCategory) || s.ReportCategoryId.Equals(allReportCategory))
            };
        }

        public ReportsListRequestResponse GetRFQOrdersReports(WarehouseReportSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayLength;
            long reportId = 0;
            if (!string.IsNullOrEmpty(searchRequest.SearchString))
                Int64.TryParse(searchRequest.SearchString, out reportId);
            int reportCategory = (int)ReportCategory.CustomerQO;
            int allReportCategory = (int)ReportCategory.AllCustomersQO;
            Expression<Func<Report, bool>> query =
                s => (s.ReportCategoryId.Equals(reportCategory) || s.ReportCategoryId.Equals(allReportCategory)) &&
                    ((string.IsNullOrEmpty(searchRequest.SearchString)) ||
                    (s.ReportId.Equals(reportId)) ||
                    (s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString)) ||
                    (s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString)) ||
                    (s.ReportInventoryItems.Any(x => x.NameA.Contains(searchRequest.SearchString))) ||
                     (s.ReportInventoryItems.Any(x => x.NameE.Contains(searchRequest.SearchString)))
                    );

            IEnumerable<Report> queryData = searchRequest.sSortDir_0 == "asc" ?
                DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                .Where(query).OrderBy(warehouseReportClause[searchRequest.RequestByColumn]).Skip(fromRow).Take(toRow).ToList() :

                DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                .Where(query).OrderByDescending(warehouseReportClause[searchRequest.RequestByColumn]).Skip(fromRow).Take(toRow).ToList();

            return new ReportsListRequestResponse
            {
                Reports = queryData.ToList(),
                FilteredCount = DbSet.Count(query),
                TotalCount = DbSet.Count(s => s.ReportCategoryId.Equals(reportCategory) || s.ReportCategoryId.Equals(allReportCategory))
            };
        }

        public ReportsListRequestResponse GetVendorsReports(VendorReportSearchRequest searchRequest)
        {
            int fromRow = searchRequest.iDisplayStart;
            int toRow = searchRequest.iDisplayLength;
            long reportId = 0;
            if (!string.IsNullOrEmpty(searchRequest.SearchString))
                Int64.TryParse(searchRequest.SearchString, out reportId);
            int reportCategory = (int) ReportCategory.Vendor;
            int allReportCategory = (int) ReportCategory.AllVendors;
            Expression<Func<Report, bool>> query =
                s =>
                    (s.ReportCategoryId.Equals(reportCategory) || s.ReportCategoryId.Equals(allReportCategory)) &&
                    ((string.IsNullOrEmpty(searchRequest.SearchString))
                     ||
                     (s.ReportId.Equals(reportId)) ||
                     (s.AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequest.SearchString)) ||
                     (s.AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequest.SearchString))
                        );

            IEnumerable<Report> queryData = searchRequest.sSortDir_0 == "asc"
                ? DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                    .Where(query)
                    .OrderBy(vendorReportClause[searchRequest.RequestByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                    .Where(query)
                    .OrderByDescending(vendorReportClause[searchRequest.RequestByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

            return new ReportsListRequestResponse
            {
                Reports = queryData.ToList(),
                FilteredCount = DbSet.Count(query),
                TotalCount =
                    DbSet.Count(
                        s => s.ReportCategoryId.Equals(reportCategory) || s.ReportCategoryId.Equals(allReportCategory))
            };
        }

        public CustomerReportListResponse GetQuotationInvoiceReports(CustomerServiceReportsSearchRequest request)
        {
            int fromRow = request.iDisplayStart;
            int toRow = request.iDisplayLength;
            long reportId = 0;
            if (!string.IsNullOrEmpty(request.SearchString))
                Int64.TryParse(request.SearchString, out reportId);
            int reportCategory = (int)ReportCategory.QuotationInvoice;
            int allReportCategory = (int)ReportCategory.AllQuotationInvoice;

            Expression<Func<Report, bool>> query =
                s => (s.ReportCategoryId.Equals(reportCategory) || s.ReportCategoryId.Equals(allReportCategory)) &&
                     ((string.IsNullOrEmpty(request.SearchString))||
                      (s.ReportId.Equals(reportId)) ||
                      (s.Employee.EmployeeFirstNameE.Contains(request.SearchString)) ||
                      (s.Employee.EmployeeLastNameE.Contains(request.SearchString)) ||
                      (s.Employee.EmployeeMiddleNameE.Contains(request.SearchString)) ||
                      (s.Employee.EmployeeFirstNameA.Contains(request.SearchString)) ||
                      (s.Employee.EmployeeLastNameA.Contains(request.SearchString)) ||
                      (s.Employee.EmployeeMiddleNameA.Contains(request.SearchString)));

            IEnumerable<Report> queryData = request.sSortDir_0 == "asc"
                ? DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                    .Where(query)
                    .OrderBy(customerReportClause[request.ReportByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                    .Where(query)
                    .OrderByDescending(customerReportClause[request.ReportByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

            return new CustomerReportListResponse
            {
                Reports = queryData.ToList(),
                FilteredCount = DbSet.Count(query),
                TotalCount =
                    DbSet.Count(
                        s => s.ReportCategoryId.Equals(reportCategory) || s.ReportCategoryId.Equals(allReportCategory))
            };
        }

        public CustomerReportListResponse GetAllCustoemrReport(CustomerServiceReportsSearchRequest request)
        {
            int fromRow = request.iDisplayStart;
            int toRow = request.iDisplayLength;
            long reportId = 0;
            if (!string.IsNullOrEmpty(request.SearchString))
                Int64.TryParse(request.SearchString, out reportId);
            int reportCategory = (int)ReportCategory.AllCustomer;

            Expression<Func<Report, bool>> query =
                s => (s.ReportCategoryId.Equals(reportCategory)) &&
                     ((string.IsNullOrEmpty(request.SearchString)));

            IEnumerable<Report> queryData = request.sSortDir_0 == "asc"
                ? DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                    .Where(query)
                    .OrderBy(customerReportClause[request.ReportByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Include(x => x.AspNetUser.Employee).Include(x => x.Project)
                    .Where(query)
                    .OrderByDescending(customerReportClause[request.ReportByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

            return new CustomerReportListResponse
            {
                Reports = queryData.ToList(),
                FilteredCount = DbSet.Count(query),
                TotalCount =
                    DbSet.Count(
                        s => s.ReportCategoryId.Equals(reportCategory))
            };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class ReportsMappers
    {

        public static ReportProject MapProjectToReportProject(this Project source)
        {
            var project = new ReportProject
            {
                ProjectId = source.ProjectId,
                NameE = source.NameE,
                NameA = source.NameA,
                StartDate = Convert.ToDateTime(source.StartDate),
                EndDate = Convert.ToDateTime(source.EndDate),
                Price = Convert.ToInt64(source.Price),
                OtherCost = Convert.ToInt64(source.OtherCost + source.ProjectTasks.Sum(x => x.TotalCost)),
                Status = source.Status,
                NoOfTasks = source.ProjectTasks.Count,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                CustomerNameE = source.Customer != null ? source.Customer.CustomerNameE : string.Empty,
                CustomerNameA = source.Customer != null ? source.Customer.CustomerNameA : string.Empty,

                NotesA = source.NotesA,
                NotesE = source.NotesE,
                DescriptionA = source.DescriptionA,
                DescriptionE = source.DescriptionE,
                NotesForCustomerA = source.NotesForCustomerA,
                NotesForCustomerE = source.NotesForCustomerE,
                SerialNo = source.SerialNo,
                RecCreatedBy = source.RecCreatedBy,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                ReportProjectTasks = source.ProjectTasks.Where(x=> !x.IsDeleted && x.IsParent).Select(x=>x.MapParentProjectTaskToReportProjectTask()).ToList()
            };

            return project;
        }
        public static ReportProjectTask MapParentProjectTaskToReportProjectTask(this ProjectTask source)
        {
            ReportProjectTask projectTask = new ReportProjectTask
            {
                TaskId = source.TaskId,
                TaskNameE = source.TaskNameE,
                StartDate = Convert.ToDateTime(source.StartDate),
                EndDate = Convert.ToDateTime(source.EndDate),
                TotalCost = source.TotalCost,
                TotalWeight = source.TotalWeight,
                TaskProgress = source.TaskProgress,
                NotesA = source.NotesA,
                NotesE = source.NotesE,
                DeletedDate = source.DeletedDate,
                IsDeleted = source.IsDeleted,
                DescriptionA = source.DescriptionA,
                DescriptionE = source.DescriptionE,
                IsParent = source.IsParent,
                NoOfSubTasks = source.SubTasks.Count,
                ProjectId = source.ProjectId,
                TaskNameA = source.TaskNameA,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                ParentTask = source.ParentTask,
                //ReportProjectSubTasks = source.SubTasks.ToList().Select(x => x.MapProjectTaskToReportProjectTask()).ToList(),
                ReportTaskEmployees = source.TaskEmployees.Select(x=>x.MapTaskEmployeeToReportTaskEmployee()).ToList()
            };


            return projectTask;
        }
        public static ReportProjectTask MapProjectTaskToReportProjectTask(this ProjectTask source)
        {
            ReportProjectTask projectTask = new ReportProjectTask
            {
                TaskId = source.TaskId,
                TaskNameE = source.TaskNameE,
                StartDate = Convert.ToDateTime(source.StartDate),
                EndDate = Convert.ToDateTime(source.EndDate),
                TotalCost = source.TotalCost,
                TotalWeight = source.TotalWeight,
                TaskProgress = source.TaskProgress,
                NotesA = source.NotesA,
                NotesE = source.NotesE,
                DeletedDate = source.DeletedDate,
                IsDeleted = source.IsDeleted,
                DescriptionA = source.DescriptionA,
                DescriptionE = source.DescriptionE,
                IsParent = source.IsParent,
                NoOfSubTasks = source.SubTasks.Count,
                ProjectId = source.ProjectId,
                TaskNameA = source.TaskNameA,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                ParentTask = source.ParentTask,
                ReportProjectSubTasks = source.SubTasks.ToList().Select(x => x.MapProjectTaskToReportProjectTask()).ToList(),
                ReportTaskEmployees = source.TaskEmployees.Select(x=>x.MapTaskEmployeeToReportTaskEmployee()).ToList()
            };


            return projectTask;
        }
        public static ReportTaskEmployee MapTaskEmployeeToReportTaskEmployee(this TaskEmployee source)
        {
            ReportTaskEmployee projectTask = new ReportTaskEmployee
            {
                TaskId = source.TaskId,
                TaskEmployeeId = source.TaskEmployeeId,

                EmployeeFirstNameA = source.Employee.EmployeeFirstNameA,
                EmployeeFirstNameE = source.Employee.EmployeeFirstNameE,

                EmployeeMiddleNameA = source.Employee.EmployeeMiddleNameE,
                EmployeeMiddleNameE = source.Employee.EmployeeMiddleNameE,

                EmployeeLastNameA = source.Employee.EmployeeLastNameA,
                EmployeeLastNameE = source.Employee.EmployeeLastNameE,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };

            return projectTask;
        }

        public static ReportInventoryItem MapInventoryItemToReportInventoryItem(this InventoryItem source)
        {
            var reportInventoryItem = new ReportInventoryItem
            {
                InventoryItemId = source.ItemId,
                NameA = source.ItemNameAr,
                NameE = source.ItemNameEn,
               
            };
            var sum = source.ItemVariations.Where(x => x.PriceCalculation).Sum(y => y.UnitPrice / source.ItemVariations.Count(z => z.PriceCalculation));
            if (sum != null)
                reportInventoryItem.Price = Math.Round((double)sum, 2);


            //reportInventoryItem.Price = (double) source.ItemVariations.Sum(x => x.UnitPrice);


            reportInventoryItem.Cost =
                Math.Round(
                    (double)
                        (source.ItemVariations.Where(x => x.CostCalculation)
                            .Sum(y => y.UnitCost/source.ItemVariations.Count(z => z.CostCalculation))), 2);


            reportInventoryItem.SoldQty =
                (int) source.ItemVariations.Sum(x => Convert.ToInt64(x.ItemReleaseQuantities.Sum(y => y.Quantity)));

            reportInventoryItem.DefectiveQty = (int)source.ItemVariations.Sum(x => x.DIFItems.Sum(y => y.ItemQty));

            ItemManufacturer bestManufacturer = source.ItemVariations.SelectMany(x => x.ItemManufacturers).OrderBy(x => Convert.ToDouble(x.Price)).FirstOrDefault();
            if (bestManufacturer != null)
            {
                reportInventoryItem.BestPriceVendorNameA = bestManufacturer.Manufacturer.ManufacturerNameAr;
                reportInventoryItem.BestPriceVendorNameE = bestManufacturer.Manufacturer.ManufacturerNameEn;
            }

            var mostBoughtManufacturer = source.ItemVariations.SelectMany(x => x.ItemManufacturers).OrderByDescending(x => x.Quantity).FirstOrDefault();
            if (mostBoughtManufacturer != null)
            {
                reportInventoryItem.MostBoughtVendorNameA = mostBoughtManufacturer.Manufacturer.ManufacturerNameAr;
                reportInventoryItem.MostBoughtVendorNameE = mostBoughtManufacturer.Manufacturer.ManufacturerNameEn;
            }

            return reportInventoryItem;
        }

        public static ReportInventoryItem MapInventoryItemVariationToReportInventoryItem(this ItemVariation source)
        {
            var reportInventoryItem = new ReportInventoryItem
            {
                InventoryItemId = source.ItemVariationId,
                NameA = source.SKUCode,
                NameE = source.SKUCode,

            };
            
            reportInventoryItem.Price = source.UnitPrice != null ? Math.Round((double)source.UnitPrice, 2) : 0;

            reportInventoryItem.Cost =
                Math.Round(
                    (double)
                        (source.ItemManufacturers.Sum(x => Convert.ToDouble(x.Price)) / source.ItemManufacturers.Count()), 2);
            reportInventoryItem.Cost = reportInventoryItem.Cost > 0 ? reportInventoryItem.Cost : 0;

            reportInventoryItem.SoldQty =
                Convert.ToInt32(source.ItemReleaseQuantities.Sum(y => y.Quantity));

            reportInventoryItem.DefectiveQty = (int)source.DIFItems.Sum(x=>x.ItemQty);

            ItemManufacturer bestManufacturer = source.ItemManufacturers.OrderBy(x => Convert.ToDouble(x.Price)).FirstOrDefault();
            if (bestManufacturer != null)
            {
                reportInventoryItem.BestPriceVendorNameA = bestManufacturer.Manufacturer.ManufacturerNameAr;
                reportInventoryItem.BestPriceVendorNameE = bestManufacturer.Manufacturer.ManufacturerNameEn;
            }

            var mostBoughtManufacturer = source.ItemManufacturers.OrderByDescending(x => x.Quantity).FirstOrDefault();
            if (mostBoughtManufacturer != null)
            {
                reportInventoryItem.MostBoughtVendorNameA = mostBoughtManufacturer.Manufacturer.ManufacturerNameAr;
                reportInventoryItem.MostBoughtVendorNameE = mostBoughtManufacturer.Manufacturer.ManufacturerNameEn;
            }

            return reportInventoryItem;
        }
    }
}

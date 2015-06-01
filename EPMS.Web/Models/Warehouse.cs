using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class Warehouse
    {
        public long WarehouseId { get; set; }
        public string WarehouseNumber { get; set; }
        [Required(ErrorMessage = "Warehouse Manager is required")]
        public string ManagerName { get; set; }
        public long? WarehouseManager { get; set; }
        public string WarehouseLocation { get; set; }
        public bool IsFull { get; set; }
        public string WarehouseSize { get; set; }
        public long? ParentId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
        public string EmployeeNameEn { get; set; }
        public string EmployeeNameAr { get; set; }
        public long NoOfAisles { get; set; }
        public long NoOfSections { get; set; }
        public long NoOfShalves { get; set; }
        public long NoOfSectoinsInShalves { get; set; }
        public long NoOfSpaces { get; set; }

        public virtual IList<WarehouseDetail> WarehouseDetails { get; set; }
    }
}
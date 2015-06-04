using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class ItemReleaseDetail
    {
        public long IRFDetailId { get; set; }
        public long ItemReleaseId { get; set; }
        [Required(ErrorMessage = "SDF")]
        public string ItemDetails { get; set; }
        public long? ItemVariationId { get; set; }
        public string PlaceInDepartment { get; set; }
        public string PlaceInWarehouse { get; set; }
        public long ItemQty { get; set; }
        public bool IsItemDescription { get; set; }
        public bool IsItemSKU { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
    }
}
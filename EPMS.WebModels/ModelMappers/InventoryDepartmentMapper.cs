using System.Collections.Generic;
using System.Linq;
using EPMS.Models.RequestModels;
using EPMS.WebModels.WebsiteModels.Common;

namespace EPMS.WebModels.ModelMappers
{
    public static class InventoryDepartmentMapper
    {
        public static WebsiteModels.InventoryDepartment CreateFromServerToClient(this Models.DomainModels.InventoryDepartment source)
        {
            return new WebsiteModels.InventoryDepartment
            {
                DepartmentId = source.DepartmentId,
                DepartmentNameEn = source.DepartmentNameEn,
                DepartmentNameAr = source.DepartmentNameAr,
                ParentId = source.ParentId,
                DepartmentColor = source.DepartmentColor,
                DepartmentDesc = source.DepartmentDesc,
                ParentDepartmentEn = source.ParentDepartment != null ? source.ParentDepartment.DepartmentNameEn : "",
                ParentDepartmentAr = source.ParentDepartment != null ? source.ParentDepartment.DepartmentNameAr : "",
                NoOfSections = source.InventoryDepartments.Any() ? source.InventoryDepartments.Count : 0,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                ParentSection = source.ParentDepartment != null ? source.ParentDepartment.CreateFromServerToClientSections() : new WebsiteModels.InventorySections(),
            };
        }
        public static WebsiteModels.InventoryDepartment CreateForProductSectionFromServerToClient(this Models.DomainModels.InventoryDepartment source)
        {
            return new WebsiteModels.InventoryDepartment
            {
                DepartmentId = source.DepartmentId,
                DepartmentNameEn = source.DepartmentNameEn,
                DepartmentNameAr = source.DepartmentNameAr,
                ParentId = source.ParentId,
                DepartmentColor = source.DepartmentColor,
                DepartmentDesc = source.DepartmentDesc,
                ParentDepartmentEn = source.ParentDepartment != null ? source.ParentDepartment.DepartmentNameEn : "",
                ParentDepartmentAr = source.ParentDepartment != null ? source.ParentDepartment.DepartmentNameAr : "",
                NoOfSections = source.InventoryDepartments.Any() ? source.InventoryDepartments.Count : 0,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                ParentSection = source.ParentDepartment != null ? source.ParentDepartment.CreateFromServerToClientSections() : new WebsiteModels.InventorySections(),
                InventoryDepartments = source.InventoryDepartments.Select(x => x.CreateForProductSectionFromServerToClient()).ToList()
            };
        }

        public static WebsiteModels.InventoryDepartment CreateFromServerToClientLv(this Models.DomainModels.InventoryDepartment source)
        {
            var descp = source.DepartmentDesc;
            descp = descp.Replace("'", "\"");
            WebsiteModels.InventoryDepartment retVal = new WebsiteModels.InventoryDepartment();
            retVal.DepartmentId = source.DepartmentId;
            retVal.DepartmentNameEn = source.DepartmentNameEn;
            retVal.DepartmentNameAr = source.DepartmentNameAr;
            retVal.ParentId = source.ParentId;
            retVal.DepartmentColor = source.DepartmentColor;
            retVal.DepartmentDesc = descp;
            var parent = source.ParentDepartment;
            while (parent != null && parent.ParentDepartment != null)
            {
                parent = parent.ParentDepartment;
            }
            retVal.ParentDepartmentEn = parent != null ? parent.DepartmentNameEn : "None";
            retVal.ParentDepartmentAr = parent != null ? parent.DepartmentNameAr : "None";
            retVal.NoOfSections = source.InventoryDepartments.Any() ? source.InventoryDepartments.Count : 0;
            retVal.RecCreatedBy = source.RecCreatedBy;
            retVal.RecCreatedDt = source.RecCreatedDt;
            retVal.RecLastUpdatedBy = source.RecLastUpdatedBy;
            retVal.RecLastUpdatedDt = source.RecLastUpdatedDt;
            retVal.InventoryDepartments = source.InventoryDepartments.Any()
                ? source.InventoryDepartments.Select(x => x.CreateFromServerToClientLv()).ToList() : new List<WebsiteModels.InventoryDepartment>();
            retVal.ParentSection = source.ParentDepartment != null ? source.ParentDepartment.CreateFromServerToClientSections() : new WebsiteModels.InventorySections();
            return retVal;
        }

        public static WebsiteModels.InventorySections CreateFromServerToClientSections(this Models.DomainModels.InventoryDepartment source)
        {
            return new WebsiteModels.InventorySections
            {
                DepartmentId = source.DepartmentId,
                DepartmentNameEn = source.DepartmentNameEn,
                DepartmentNameAr = source.DepartmentNameAr,
                ParentId = source.ParentId,
                DepartmentColor = source.DepartmentColor,
                ParentSections = source.ParentDepartment != null ? source.ParentDepartment.CreateFromServerToClientSections() : new WebsiteModels.InventorySections()
            };
        }
        public static InventoryDepartmentRequest CreateFromClientToServer(this WebsiteModels.InventoryDepartment source)
        {
            var dept = new Models.DomainModels.InventoryDepartment();
            dept.DepartmentId = source.DepartmentId;
            dept.DepartmentNameEn = source.DepartmentNameEn;
            dept.DepartmentNameAr = source.DepartmentNameAr;
            dept.ParentId = source.ParentId;
            dept.DepartmentColor = source.DepartmentColor;
            dept.DepartmentDesc = source.DepartmentDesc;
            dept.RecCreatedBy = source.RecCreatedBy;
            dept.RecCreatedDt = source.RecCreatedDt;
            dept.RecLastUpdatedBy = source.RecLastUpdatedBy;
            dept.RecLastUpdatedDt = source.RecLastUpdatedDt;
            var request = new InventoryDepartmentRequest {InventoryDepartment = dept};
            return request;
        }
        public static Models.DomainModels.InventoryDepartment CreateFromClientToServerModel(this WebsiteModels.InventoryDepartment source)
        {
            var dept = new Models.DomainModels.InventoryDepartment();
            dept.DepartmentId = source.DepartmentId;
            dept.DepartmentNameEn = source.DepartmentNameEn;
            dept.DepartmentNameAr = source.DepartmentNameAr;
            dept.ParentId = source.ParentId;
            dept.DepartmentColor = source.DepartmentColor;
            dept.DepartmentDesc = source.DepartmentDesc;
            dept.RecCreatedBy = source.RecCreatedBy;
            dept.RecCreatedDt = source.RecCreatedDt;
            dept.RecLastUpdatedBy = source.RecLastUpdatedBy;
            dept.RecLastUpdatedDt = source.RecLastUpdatedDt;
            return dept;
        }

        public static JsTree CreateForJsTree(this Models.DomainModels.InventoryDepartment source)
        {
            return new JsTree
            {
                NodeId = source.DepartmentId,
                NodeTitleEn = source.DepartmentNameEn,
                NodeTitleAr = source.DepartmentNameAr,
                ParentId = source.ParentId ?? 0
            };
        }
        public static JsTreeJson CreateForJsTreeJsonEn(this Models.DomainModels.InventoryDepartment source)
        {
            return new JsTreeJson
            {
                id = source.DepartmentId + "_department",
                text = source.DepartmentNameEn,
                parent = source.ParentId != null ? source.ParentId + "_department" : "#"
            };
        }
        public static JsTreeJson CreateForJsTreeJsonAr(this Models.DomainModels.InventoryDepartment source)
        {
            return new JsTreeJson
            {
                id = source.DepartmentId + "_department",
                text = source.DepartmentNameAr,
                parent = source.ParentId != null ? source.ParentId + "_department" : "#"
            };
        }
    }
}
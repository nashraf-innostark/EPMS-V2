using System.Collections.Generic;
using System.Linq;
using EPMS.Models.RequestModels;
using EPMS.Web.Models.Common;
using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class InventoryDepartmentMapper
    {
        public static WebModels.InventoryDepartment CreateFromServerToClient(this DomainModels.InventoryDepartment source)
        {
            return new WebModels.InventoryDepartment
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
                ParentSection = source.ParentDepartment != null ? source.ParentDepartment.CreateFromServerToClientSections() : new Models.InventorySections(),
            };
        }

        public static WebModels.InventoryDepartment CreateFromServerToClientLv(this DomainModels.InventoryDepartment source)
        {
            var descp = source.DepartmentDesc;
            descp = descp.Replace("'", "\"");
            WebModels.InventoryDepartment retVal = new WebModels.InventoryDepartment();
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
                ? source.InventoryDepartments.Select(x => x.CreateFromServerToClientLv()).ToList() : new List<WebModels.InventoryDepartment>();
            retVal.ParentSection = source.ParentDepartment != null ? source.ParentDepartment.CreateFromServerToClientSections() : new Models.InventorySections();
            return retVal;
        }

        public static WebModels.InventorySections CreateFromServerToClientSections(this DomainModels.InventoryDepartment source)
        {
            return new WebModels.InventorySections
            {
                DepartmentId = source.DepartmentId,
                DepartmentNameEn = source.DepartmentNameEn,
                DepartmentNameAr = source.DepartmentNameAr,
                ParentId = source.ParentId,
                DepartmentColor = source.DepartmentColor,
                ParentSections = source.ParentDepartment != null ? source.ParentDepartment.CreateFromServerToClientSections() : new WebModels.InventorySections()
            };
        }
        public static InventoryDepartmentRequest CreateFromClientToServer(this WebModels.InventoryDepartment source)
        {
            var dept = new DomainModels.InventoryDepartment();
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
        public static DomainModels.InventoryDepartment CreateFromClientToServerModel(this WebModels.InventoryDepartment source)
        {
            var dept = new DomainModels.InventoryDepartment();
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

        public static JsTree CreateForJsTree(this DomainModels.InventoryDepartment source)
        {
            return new JsTree
            {
                NodeId = source.DepartmentId,
                NodeTitleEn = source.DepartmentNameEn,
                NodeTitleAr = source.DepartmentNameAr,
                ParentId = source.ParentId ?? 0
            };
        }
        public static JsTreeJson CreateForJsTreeJsonEn(this DomainModels.InventoryDepartment source)
        {
            return new JsTreeJson
            {
                id = source.DepartmentId + "_department",
                text = source.DepartmentNameEn,
                parent = source.ParentId != null ? source.ParentId + "_department" : "#"
            };
        }
        public static JsTreeJson CreateForJsTreeJsonAr(this DomainModels.InventoryDepartment source)
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
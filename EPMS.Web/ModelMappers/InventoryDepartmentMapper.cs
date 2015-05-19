using System.Web.Razor.Parser.SyntaxTree;
using EPMS.Models.RequestModels;
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
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
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
            var request = new InventoryDepartmentRequest();
            request.InventoryDepartment = dept;
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
    }
}
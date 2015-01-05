using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class AllownaceMapper
    {
        public static Allowance CreateFromClientToServer(this Models.Allowance source)
        {
            var caseType = new Allowance
            {
                Allowance1 = source.Allowance1,
                Allowance2 = source.Allowance2,
                Allowance3 = source.Allowance3,
                Allowance4 = source.Allowance4,
                Allowance5 = source.Allowance5,
                AllowanceDesc1 = source.AllowanceDesc1,
                AllowanceDesc2 = source.AllowanceDesc2,
                AllowanceDesc3 = source.AllowanceDesc3,
                AllowanceDesc4 = source.AllowanceDesc4,
                AllowanceDesc5 = source.AllowanceDesc5,
                EmployeeId = source.EmployeeId,
                AllowanceId = source.AllowanceId,
                AllowanceDate = source.AllowanceDate
            };
            return caseType;
        }
        public static Models.Allowance CreateFromServerToClient(this Allowance source)
        {
            return new Models.Allowance
            {
                Allowance1 = source.Allowance1 ?? 0,
                Allowance2 = source.Allowance2 ?? 0,
                Allowance3 = source.Allowance3 ?? 0,
                Allowance4 = source.Allowance4 ?? 0,
                Allowance5 = source.Allowance5 ?? 0,
                AllowanceDesc1 = source.AllowanceDesc1,
                AllowanceDesc2 = source.AllowanceDesc2,
                AllowanceDesc3 = source.AllowanceDesc3,
                AllowanceDesc4 = source.AllowanceDesc4,
                AllowanceDesc5 = source.AllowanceDesc5,
                EmployeeId = source.EmployeeId,
                AllowanceId = source.AllowanceId,
                AllowanceDate = source.AllowanceDate
            };
        }
    }
}
namespace EPMS.Web.Models
{
    public class JobTitleAndDepartment
    {
        public long JobId { get; set; }
        public string JobTitle{ get; set; }
        public long DeptId { get; set; }
        public string DeptNameE { get; set; }
        public string DeptNameA { get; set; }
        public double BasicSalary { get; set; }
    }
}
namespace EPMS.Models.ResponseModels
{
    public class ProjectTaskResponse
    {
        public long TaskId { get; set; }
        public string TaskNameE { get; set; }
        public string TaskNameA { get; set; }
        public string TaskNameEShort { get; set; }
        public string TaskNameAShort { get; set; }
        public decimal? TaskProgress { get; set; }
        public decimal? TaskProgressPercentage { get; set; }
    }
}

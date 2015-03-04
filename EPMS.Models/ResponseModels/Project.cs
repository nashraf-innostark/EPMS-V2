namespace EPMS.Models.ResponseModels
{
    public class Project
    {
        public long ProjectId { get; set; }
        public string NameE { get; set; }
        public string NameA { get; set; }
        public string NameEShort { get; set; }
        public string NameAShort { get; set; }
        public int ProgressTotal { get; set; }
    }
}

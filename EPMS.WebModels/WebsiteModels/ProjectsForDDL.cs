namespace EPMS.WebModels.WebsiteModels
{
    public class ProjectsForDDL
    {
        public long ProjectId { get; set; }
        public string NameE { get; set; }
        public string NameA { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int ProjectTasksSum { get; set; }
    }
}
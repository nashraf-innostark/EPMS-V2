namespace EPMS.Models.DomainModels
{
    public class ProjectDocument
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string FileName { get; set; }

        public virtual Project Project { get; set; }
    }
}

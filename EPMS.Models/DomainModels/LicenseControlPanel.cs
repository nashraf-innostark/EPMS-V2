namespace EPMS.Models.DomainModels
{
    public class LicenseControlPanel
    {
        public long LicenseControlPanelId { get; set; }
        public string CompanyName { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string LandLine { get; set; }
        public string Mobile { get; set; }
        public string CommercialRegister { get; set; }
        public string ProductNumber { get; set; }
        public int NoOfUsers { get; set; }
        public string LicenseNumber { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }
}

namespace EPMS.Web.Files
{
    public class PDFOtherFieldType : PDFFieldType
    {
        public override int Type
        {
            get { return -1; }
        }

        public override string ToString()
        {
            return "Other";
        }
    }
}
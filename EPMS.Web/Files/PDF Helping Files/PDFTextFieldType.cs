namespace EPMS.Web.Files
{
    public class PDFTextFieldType : PDFFieldType
    {
        public override int Type
        {
            get { return 4; }
        }

        public override string ToString()
        {
            return "TextField";
        }
    }
}
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using EPMS.WebModels.ViewModels.CKEditor;

namespace EPMS.Web.Areas.Common.Controllers
{
    public class FileUploadCkControllerController : Controller
    {
        //// GET: Common/FileUploadCkController
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public void FileUpload(HttpPostedFileWrapper upload)
        {
            if (upload != null)
            {
                string imageName = upload.FileName;
                string path = Path.Combine(Server.MapPath("~/Images/CKUploads"), imageName);
                upload.SaveAs(path);
            }
        }
        public ActionResult UploadPartial()
        {
            var appData = Server.MapPath("~/Images/CKUploads");
            var images = Directory.GetFiles(appData).Select(x => new ImagesViewModel
            {
                Url = Url.Content("/images/CKUploads/" + Path.GetFileName(x))
            });
            return View(images);
        }
    }
}
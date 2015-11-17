using System;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class ImageController : Controller
    {
        [HttpPost]
        public ActionResult UploadReportChartImage(string base64String, string imageName)
        {
            try
            {
                string fullOutputPath = Server.MapPath(ConfigurationManager.AppSettings["ReportImage"]);
                string pathWithImageName = fullOutputPath + imageName + ".png";
                //data:image/png;base64,i
                string img = base64String.Split(';')[1];
                byte[] bytes = Convert.FromBase64String(img.Split(',')[1]);
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                image.Save(pathWithImageName, System.Drawing.Imaging.ImageFormat.Png);
                return Json(new { response = "Successfully uploaded!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { response = "Failed to upload. Error: ", status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
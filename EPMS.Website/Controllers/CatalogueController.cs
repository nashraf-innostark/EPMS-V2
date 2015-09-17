using System.IO;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers.Website.Product;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Product;

namespace EPMS.Website.Controllers
{
    public class CatalogueController : BaseController
    {
        private readonly IProductService productService;

        public CatalogueController(IProductService productService)
        {
            this.productService = productService;
        }

        public ActionResult Index()
        {
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = productService.GetAll().Select(x => x.CreateFromServerToClient()).ToList()
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        public ActionResult SteveJobs()
        {
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = productService.GetAll().Select(x => x.CreateFromServerToClient()).ToList()
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        public ActionResult CatalogueHtml()
        {
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = productService.GetAll().Select(x => x.CreateFromServerToClient()).ToList()
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return PartialView(viewModel);
        }
        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.ToString();
            }
        }

        // Generate Pdf

        public ActionResult Catalogue()
        {
            ////Create a byte array that will eventually hold our final PDF
            //Byte[] bytes;

            ////Boilerplate iTextSharp setup here
            ////Create a stream that we can write to, in this case a MemoryStream
            //using (var ms = new MemoryStream())
            //{
            //    //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
            //    using (var doc = new Document())
            //    {
            //        //Create a writer that's bound to our PDF abstraction and our stream
            //        using (var writer = PdfWriter.GetInstance(doc, ms))
            //        {
            //            //Open the document for writing
            //            doc.Open();

            //            //Our sample HTML and CSS
            //            ProductListViewModel viewModel = new ProductListViewModel
            //            {
            //                Products = productService.GetAll().Select(x => x.CreateFromServerToClient()).ToList()
            //            };
            //            var example_html = RenderPartialViewToString(this, "CatalogueHtml", viewModel);
            //            var example_css = @".headline{font-size:200%}";

            //            /* ************************************************
            //             * Example #1                                     *
            //             *                                                *
            //             * Use the built-in HTMLWorker to parse the HTML. *
            //             * Only inline CSS is supported.                  *
            //             * ************************************************/

            //            //Create a new HTMLWorker bound to our document
            //            using (var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc))
            //            {

            //                //HTMLWorker doesn't read a string directly but instead needs a TextReader (which StringReader subclasses)
            //                using (var sr = new StringReader(example_html))
            //                {

            //                    //Parse the HTML
            //                    htmlWorker.Parse(sr);
            //                }
            //            }
            //            doc.Close();
            //        }
            //    }
            //    //After all of the PDF "stuff" above is done and closed but **before** we
            //    //close the MemoryStream, grab all of the active bytes from the stream
            //    bytes = ms.ToArray();
            //}
            ////Now we just need to do something with those bytes.
            ////Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
            ////You could also write the bytes to a database in a varbinary() column (but please don't) or you
            ////could pass them to another function for further PDF processing.
            //var testFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.pdf");
            //System.IO.File.WriteAllBytes(testFile, bytes);
            //return File(testFile, "application/pdf");
            return new Rotativa.ActionAsPdf("CatalogueHtml"){FileName = "EPMS Catalogue.pdf"};
        }
    }
}
using System.Web.Mvc;
using EPMS.Interfaces.IServices;

namespace EPMS.Website.Controllers
{
    public class ProductController : Controller
    {
        #region Private

        private readonly IProductService productService;

        #endregion

        #region Constructor
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        #endregion

        #region Public
        
        #region Index
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Details
        public ActionResult Details(long? id, string from)
        {

            return View();
        }

        #endregion

        #endregion
    }
}
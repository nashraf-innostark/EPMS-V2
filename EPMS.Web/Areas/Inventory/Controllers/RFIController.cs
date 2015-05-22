using System.Web.Mvc;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class RFIController : Controller
    {
        // GET: Inventory/RFI
        public ActionResult Index()
        {
            return View();
        }

        // GET: Inventory/RFI/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inventory/RFI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventory/RFI/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventory/RFI/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inventory/RFI/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventory/RFI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inventory/RFI/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

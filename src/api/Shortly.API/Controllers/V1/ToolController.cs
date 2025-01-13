using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shortly.API.Controllers.V1
{
    public class ToolController : Controller
    {
        // GET: ToolController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ToolController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ToolController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToolController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ToolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToolController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ToolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

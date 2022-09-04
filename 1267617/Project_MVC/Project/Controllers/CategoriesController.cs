using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        JewelleryDbContext db = new JewelleryDbContext();
        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return PartialView("_success");
            }
            return PartialView("_error");
        }
        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(Category c)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var cat = db.Categories.Find(id);
            db.Categories.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
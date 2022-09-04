using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.Models.ViewModels;
using PagedList;
using PagedList.Mvc;

namespace Project.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        JewelleryDbContext db = new JewelleryDbContext();
        // GET: Items
        public ActionResult Index(int page = 1, string sort = "", string search = "")
        {
           ViewBag.sortBy = sort == "name" ? "name_desc" : "name";
            ViewBag.currentSort = sort;
            var data = db.Items.Select(x => new ItemVM
            {
                Id = x.Id,
                ItemName = x.ItemName,
                CategoryId = x.CategoryId,
                Price = x.Price,
                StoreDate = x.StoreDate,
                PicturePath = x.PicturePath,
                Available = x.Available
            });
            switch (sort)
            {
                case "name":
                    data = data.OrderBy(x => x.ItemName);
                    break;
                case "name_desc":
                    data = data.OrderByDescending(x => x.ItemName);
                    break;
                default :
                    data = data.OrderBy(x => x.ItemName);
                    break;
            }
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.ItemName.ToLower().StartsWith(search.ToLower()));
            }
            return View(data.ToPagedList(page,5));
      
        }
        public ActionResult Create()
        {
            ViewBag.categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(ItemVM ivm)
        {
            if (ModelState.IsValid)
            {
                if (ivm.Picture != null)
                {
                    string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(ivm.Picture.FileName));
                    ivm.Picture.SaveAs(Server.MapPath(filePath));

                    Items items = new Items
                    {
                        ItemName = ivm.ItemName,
                        CategoryId = ivm.CategoryId,
                        Price = ivm.Price,
                        StoreDate = ivm.StoreDate,
                        PicturePath = filePath,
                        Available= ivm.Available
                    };
                    db.Items.Add(items);
                    db.SaveChanges();
                    return PartialView("_success");
                }
            }
            ViewBag.categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return PartialView("_error");
        }
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            ItemVM ivm = new ItemVM
            {
                Id = items.Id,
                ItemName = items.ItemName,
                CategoryId = items.CategoryId,
                Price = items.Price,
                StoreDate = items.StoreDate,
                PicturePath = items.PicturePath,
                Available = items.Available
            };
            ViewBag.categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(ivm);
        }
        [HttpPost]
        public ActionResult Edit(ItemVM ivm)
        {
            if (ModelState.IsValid)
            {
                string filePath = ivm.PicturePath;
                if (ivm.Picture != null)
                {
                    filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(ivm.Picture.FileName));
                    ivm.Picture.SaveAs(Server.MapPath(filePath));

                    Items items = new Items
                    {
                        Id = ivm.Id,
                        ItemName = ivm.ItemName,
                        CategoryId = ivm.CategoryId,
                        Price = ivm.Price,
                        StoreDate = ivm.StoreDate,
                        PicturePath = filePath,
                        Available = ivm.Available
                    };
                    db.Entry(items).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("_success");
                }
                else
                {
                    Items items = new Items
                    {
                        Id = ivm.Id,
                        ItemName = ivm.ItemName,
                        CategoryId = ivm.CategoryId,
                        Price = ivm.Price,
                        StoreDate = ivm.StoreDate,
                        PicturePath = filePath,
                        Available = ivm.Available
                    };
                    db.Entry(items).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("_success");
                }
            }
            ViewBag.categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return PartialView("_error");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            Items items = db.Items.Find(id);
            string file_name = items.PicturePath;
            string path = Server.MapPath(file_name);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            db.Items.Remove(items);
            db.SaveChanges();
            return PartialView("_success");
        }

    }
}
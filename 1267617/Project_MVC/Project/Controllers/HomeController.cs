using Microsoft.AspNet.Identity;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        JewelleryDbContext db = new JewelleryDbContext();
        // GET: Home
       
        public ActionResult Index()
        {
            return View();
        }
        //[Authorize]
        //public ActionResult Add(Items it)
        //{

        //    if (Session["cart"] == null)
        //    {
        //        List<Items> li = new List<Items>();

        //        li.Add(it);
        //        Session["cart"] = li;
        //        ViewBag.cart = li.Count();


        //        Session["count"] = 1;


        //    }
        //    else
        //    {
        //        List<Items> li = (List<Items>)Session["cart"];
        //        li.Add(it);
        //        Session["cart"] = li;
        //        ViewBag.cart = li.Count();
        //        Session["count"] = Convert.ToInt32(Session["count"]) + 1;

        //    }
        //    return RedirectToAction("Index", "Home");
        //}
        ////HTTP GET Home/CheckOut
        //public ActionResult Myorder()
        //{

        //    return View((List<Items>)Session["cart"]);

        //}
        //public ActionResult Remove(Items it)
        //{
        //    List<Items> li = (List<Items>)Session["cart"];
        //    li.RemoveAll(x =>x.Id == it.Id);
        //    Session["cart"] = li;
        //    Session["count"] = Convert.ToInt32(Session["count"]) - 1;
        //    return RedirectToAction("Myorder", "AddToCart");

        //}
    }
}
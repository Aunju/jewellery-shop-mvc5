using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.AspNet.Identity;
using Project.Models;

namespace Project.Controllers
{

    public class JewelleryViewController : Controller
    {

        JewelleryDbContext db = new JewelleryDbContext();
        // GET: JewelleryView
      
        public ActionResult Index()
        {
            return View(db.Items.ToList());
        }
        // GET: /Products/Details/5
        public ActionResult Details(int? id)
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

        public ActionResult BuyNow()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult BuyNow(Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Customer cus = new Customer
        //        {
        //            CustomerName = customer.CustomerName,
        //            Address = customer.Address
        //        };
        //        db.Customers.Add(customer);
        //        db.SaveChanges();
        //        return RedirectToAction(nameof(OrderReceived));
        //    }
        //    return View();
        //}
        public ActionResult CusIndex()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                Customer cus = new Customer
                {
                    CustomerName = customer.CustomerName,
                    Address = customer.Address
                };
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction(nameof(CusIndex));
            }
            return View();
        }
        public ActionResult OrderReceived()
        {
            return View();
        }
       

    }
}
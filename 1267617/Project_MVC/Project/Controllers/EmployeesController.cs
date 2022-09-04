using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.Models.ViewModels;

namespace Project.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        JewelleryDbContext db = new JewelleryDbContext();
        // GET: Employees
        public ActionResult Index()
        {
            var employeeInfo = (
                                from e in db.Employees
                                join ea in db.EmployeeAddresses on e.EmployeeId equals ea.EmployeeId
                                join ep in db.EmployeePhotos on e.EmployeeId equals ep.EmployeeId
                                select new EmployeeRetriveVM
                                {
                                    EmployeeId = e.EmployeeId,
                                    EmployeeName = e.EmployeeName,
                                    Address = ea.Address,
                                    PostCode = ea.PostCode,
                                    Image = ep.Image
                                }).ToList();

            return View(employeeInfo);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeVM employee)
        {
            string msg = "";
            using (var context = new JewelleryDbContext())
            {
                using (DbContextTransaction dbTran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            if (employee.Image != null)
                            {
                                string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(employee.Image.FileName));
                                employee.Image.SaveAs(Server.MapPath(filePath));

                                Employee e = new Employee { EmployeeName = employee.EmployeeName };
                                EmployeeAddress a = new EmployeeAddress { Address = employee.Address, PostCode = employee.PostCode, Employee = e };
                                EmployeePhoto ep = new EmployeePhoto { Employee = e, Image = filePath };

                                db.EmployeeAddresses.Add(a);
                                db.EmployeePhotos.Add(ep);
                                db.SaveChanges();
                                return RedirectToAction("Index");

                            }
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        dbTran.Rollback();
                        msg = ex.Message;
                        ViewBag.msg = msg;
                    }
                }
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var emp = db.Employees.FirstOrDefault(x => x.EmployeeId == id);
                var ea = db.EmployeeAddresses.FirstOrDefault(x => x.EmployeeId == id);
                var ep = db.EmployeePhotos.FirstOrDefault(x => x.EmployeeId == id);
                EmployeeRetriveVM vm = new EmployeeRetriveVM { EmployeeId = emp.EmployeeId, EmployeeName = emp.EmployeeName, Address = ea.Address, PostCode = ea.PostCode, Image = ep.Image };
                return View(vm);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Edit(EmployeeVM vm)
        {
            if (ModelState.IsValid)
            {

                if (vm.Image != null)
                {
                    string newFilePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(vm.Image.FileName));
                    vm.Image.SaveAs(Server.MapPath(newFilePath));

                    Employee employee = new Employee { EmployeeId = vm.EmployeeId, EmployeeName = vm.EmployeeName };
                    EmployeeAddress ea = new EmployeeAddress { EmployeeId = vm.EmployeeId, Address = vm.Address, PostCode = vm.PostCode };
                    EmployeePhoto ep = new EmployeePhoto { EmployeeId = vm.EmployeeId, Image = newFilePath };

                    db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(ea).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(ep).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Employee employee = new Employee { EmployeeId = vm.EmployeeId, EmployeeName = vm.EmployeeName };
                    EmployeeAddress ea = new EmployeeAddress { EmployeeId = vm.EmployeeId, Address = vm.Address, PostCode = vm.PostCode, Employee = employee };

                    db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(ea).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            return View();
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var emp = db.Employees.FirstOrDefault(x => x.EmployeeId == id);
                var ea = db.EmployeeAddresses.FirstOrDefault(x => x.EmployeeId == id);
                var ep = db.EmployeePhotos.FirstOrDefault(x => x.EmployeeId == id);

                db.EmployeePhotos.Remove(ep);
                db.EmployeeAddresses.Remove(ea);
                db.Employees.Remove(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
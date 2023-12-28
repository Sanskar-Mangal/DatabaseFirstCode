using DatabaseFirstCode.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatabaseFirstCode.Controllers
{
    public class HomeController : Controller
    {

        DatabaseFirstEntities db = new DatabaseFirstEntities();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.students.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(student s)  //student.cs is class name
        {
            if(ModelState.IsValid == true)  //agr error nahi hai toh true hoga
            {
                db.students.Add(s);   //students db name mai set hai
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["InsertMessage"] = "<script>alert('Inserted')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["InsertMessage"] = "<script>alert('Not Inserted')</script>";
                }
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            var row = db.students.Where(model => model.id == id).FirstOrDefault();  //id check or read first row
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(student s)  //student class ka s is object
        {
            if(ModelState.IsValid == true)
            {
                db.Entry(s).State = EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["UpdateMessage"] = "<script>alert('Updated')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["UpdateMessage"] = "<script>alert('Not Updated')</script>";
                }

            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var DeletedRow = db.students.Where(model => model.id == id).FirstOrDefault();  //id check or read first row
            return View(DeletedRow);
        }

        [HttpPost]
        public ActionResult Delete(student s)
        {
            db.Entry(s).State = EntityState.Deleted;
            int a = db.SaveChanges();
            if (a > 0)
            {
                TempData["DeleteMessage"] = "<script>alert('Deleted')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["DeleteMessage"] = "<script>alert('Not Deleted')</script>";
            }

            return View();
        }


    }
}
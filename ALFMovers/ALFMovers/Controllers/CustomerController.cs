using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ALFMovers.Models;

namespace ALFMovers.Controllers
{
    public class CustomerController : Controller
    {
        private ALFMoversEntities db = new ALFMoversEntities();

        // GET: Customer
        public ActionResult List()
        {

            return View(db.Customers.ToList());
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustID,CustName,CustContact,FromAddrss,ToAddrss,IssuedDate,SchedDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.IssuedDate = DateTime.Now;
                customer.CustID = 1;
                
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public object GetClient()
        {
            
            var custid = Convert.ToInt32(Session["customerID"]);
            var customers = db.Customers.Where(c => c.CustID == custid ).FirstOrDefault();
            
            return  Json(new
            {
                name = customers.CustName,
                faddress = customers.FromAddrss,
                taddress = customers.ToAddrss,
            },JsonRequestBehavior.AllowGet);
        }




    }
}

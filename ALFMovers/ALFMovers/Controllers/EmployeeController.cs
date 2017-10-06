using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ALFMovers.Models;
using System.Data.SqlClient;

namespace ALFMovers.Controllers
{
    public class EmployeeController : Controller
    {
        private ALFMoversEntities db = new ALFMoversEntities();

        // GET: Employee
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpID,EmpFName,EmpLName,EmpContact,Position,EmpJoined,EmpLicenset,Status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.EmpJoined = DateTime.Now;
                employee.EmpID = 1;
                employee.Status = "Available";
                
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult EditEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpID,EmpFName,EmpLName,EmpContact,EmpJoined,EmpLicense")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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

        public ActionResult Sched(int? id)
        {
            Session["customerID"] = id;
            Customer c = db.Customers.Find(id);
            var test = c.SchedDate.ToString();
            var customerList = db.Employees.SqlQuery("select * from Employee WHERE EmpID NOT IN (SELECT TransEmp.EmpID FROM TransEmp INNER JOIN Transactions ON TransEmp.TransID = Transactions.TransID where Transactions.SchedDate = '"+test+"')").ToList<Employee>();

            return View(customerList);
            
        }

        public JsonResult GetTransEmps()
        {
            var id = Session["TransactionId"];
            var empList = db.Employees.SqlQuery("select * from Employee WHERE EmpID  IN (SELECT TransEmp.EmpID FROM TransEmp INNER JOIN Transactions ON TransEmp.TransID = Transactions.TransID where Transactions.TransID = " + id + ")").ToList<Employee>();
            string all = "";
            foreach (var t in empList)
            {
                all += "<tr> <td>" + t.EmpFName.ToString() + " "+t.EmpLName.ToString()+ "</td><td>" + t.Position.ToString() + "</td><td>" + t.EmpContact.ToString() + "</td></ tr > ";
            }
            return Json(all, JsonRequestBehavior.AllowGet);

        }

        public String SchedSubmit(int id)
        {
            TransEmp E = new TransEmp();
            E.EmpID = id;
            E.TransID = Convert.ToInt32(Session["transid"]);
            db.TransEmps.Add(E);
            db.SaveChanges();
            return "Success"+id;
        }
    }
}

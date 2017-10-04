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
    public class TruckController : Controller
    {
        private ALFMoversEntities db = new ALFMoversEntities();

        // GET: Truck
        public ActionResult Index()
        {
            return View(db.Trucks.ToList());
        }

        // GET: Truck/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = db.Trucks.Find(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            return View(truck);
        }

        // GET: Truck/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Truck/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TruckPlateNo,TruckModel,Capacity,TruckAdded")] Truck truck)
        {
            if (ModelState.IsValid)
            {
                truck.TruckAdded = DateTime.Now;    
                
                db.Trucks.Add(truck);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(truck);
        }

        // GET: Truck/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = db.Trucks.Find(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            return View(truck);
        }

        // POST: Truck/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TruckPlateNo,TruckModel,Capacity,TruckAdded")] Truck truck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(truck).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(truck);
        }

        // GET: Truck/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Truck truck = db.Trucks.Find(id);
            if (truck == null)
            {
                return HttpNotFound();
            }
            return View(truck);
        }

        // POST: Truck/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Truck truck = db.Trucks.Find(id);
            db.Trucks.Remove(truck);
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
            var test = c.SchedDate;
            var truckList = db.Trucks.SqlQuery("select * from Truck WHERE EmpID NOT IN (SELECT TransTruck.TruckPlateNo FROM TransTruck INNER JOIN Transactions ON TransTruck.TransID = Transactions.TransID where Transactions.SchedDate = '" + test + "')").ToList<Truck>();

            return View(truckList);

        }

        public String SchedSubmit(string id)
        {
            TransTruck T = new TransTruck();
            T.TruckPlateNo = id.ToString();
            T.TransID = Convert.ToInt32(Session["transid"]);
            db.TransTrucks.Add(T);
            db.SaveChanges();
            return "Success" + id;
        }

        public JsonResult GetTrucks()
        {
            int id = Convert.ToInt32(Session["customerID"]);
            Customer c = db.Customers.Find(id);
            var test = c.SchedDate;
            var truckList = db.Trucks.SqlQuery("select * from Truck WHERE Truck.Status !=  'inactive' and TruckPlateNO NOT IN (SELECT TransTruck.TruckPlateNo FROM TransTruck INNER JOIN Transactions ON TransTruck.TransID = Transactions.TransID where Transactions.SchedDate = '" + test + "')").ToList<Truck>();
            string all = "";
            foreach (var t in truckList )
            {
                all += "<tr> <td>"+t.TruckPlateNo.ToString()+"</td><td>"+t.TruckModel.ToString()+"</td><td>"+t.Capacity.ToString()+ "</td><td><input type='checkbox' value='"+t.TruckPlateNo.ToString()+"' name='truck' /></td></ tr > ";
            }
            return Json(all,JsonRequestBehavior.AllowGet); 
        }

        public JsonResult GetTransTrucks()
        {
            int id = Convert.ToInt32(Session["TransactionId"]);
            var truckList = db.Trucks.SqlQuery("select * from Truck WHERE TruckPlateNO IN (SELECT TransTruck.TruckPlateNo FROM TransTruck INNER JOIN Transactions ON TransTruck.TransID = Transactions.TransID where Transactions.TransID = " + id + ")").ToList<Truck>();
            string all = "";
            foreach (var t in truckList)
            {
                all += "<tr> <td>" + t.TruckPlateNo.ToString() + "</td><td>" + t.TruckModel.ToString() + "</td><td>" + t.Capacity.ToString() + "</td></ tr > ";
            }
            return Json(all, JsonRequestBehavior.AllowGet);
        }

        public string ChangeStatus(string id)
        {
            Truck truck = db.Trucks.Find(id);
            if (truck.Status=="active")
            {
                truck.Status = "inactive";
                db.Entry(truck).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                truck.Status = "active";
                db.Entry(truck).State = EntityState.Modified;
                db.SaveChanges();
            }
            return "Status Changed to " + truck.Status;
        }
    }
}
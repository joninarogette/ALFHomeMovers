using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALFMovers.Controllers
{
    public class PublicController : Controller
    {
        // GET: Public
        public ActionResult Index()
        {
            return View();
        }

        protected override void HandleUnknownAction(string Dashboard)
        {
            try
            {
                this.View("Dashboard").ExecuteResult(this.ControllerContext);
            }
            catch(Exception ex)
            {
                Response.Redirect("Dashboard");
            }
        }
    }
}
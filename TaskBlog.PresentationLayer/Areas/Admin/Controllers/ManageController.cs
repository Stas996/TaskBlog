using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskBlog.PresentationLayer.Areas.Admin.Controllers
{
    [Authorize]
    [RouteArea("Admin")]
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }
    }
}
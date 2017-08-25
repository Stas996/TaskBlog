using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskBlog.BusinessLogicLayer.Services;

namespace TaskBlog.PresentationLayer.Areas.Admin.Controllers
{
    [Authorize]
    [RouteArea("Admin")]
    public class ManageController : Controller
    {
        ArticleService _articleService;
        TagService _tagService;

        public ManageController(ArticleService articleService, TagService tagService)
        {
            _articleService = articleService;
            _tagService = tagService;
        }

        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

    }
}
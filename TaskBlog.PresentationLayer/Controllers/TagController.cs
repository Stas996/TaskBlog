using System;
using System.Linq;
using System.Web.Mvc;
using TaskBlog.ViewModels;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.Services;

namespace TaskBlog.PresentationLayer.Controllers
{
    public class TagController : Controller
    {
        TagService _service;

        public TagController(IService<TagViewModel> service)
        {
            _service = service as TagService;
        }

        // GET: Article
        [Authorize]
        public ActionResult Index()
        {
            var viewModels = _service.GetAll().OrderBy(a => a.Name).ToList();
            return View(viewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagViewModel tag)
        {
            _service.Create(tag);
            _service.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMany(string tags)
        {
            var t = tags.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries);
            _service.CreateMany(t);
            _service.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            _service.Save();
            return RedirectToAction("Index");
        }
    }
}
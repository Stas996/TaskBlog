using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using TaskBlog.PresentationLayer.ViewModels;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.Services;
using TaskBlog.BusinessLogicLayer.DTOModels;
using AutoMapper;

namespace TaskBlog.PresentationLayer.Controllers
{
    public class TagController : Controller
    {
        TagService _service;
        IMapper _modelsMapper;

        public TagController(IService<TagDTO> service)
        {
            _service = service as TagService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TagDTO, TagViewModel>();
            });
            _modelsMapper = config.CreateMapper();
        }

        // GET: Article
        [Authorize]
        public ActionResult Index()
        {
            var dtoModels = _service.GetAll().OrderBy(a => a.Name).ToList();
            var viewModels = _modelsMapper.Map<List<TagDTO>, TagViewModel[]>(dtoModels);
            return View(viewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagDTO tag)
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
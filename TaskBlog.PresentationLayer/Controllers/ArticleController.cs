using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.Services;
using TaskBlog.BusinessLogicLayer.DTOModels;
using TaskBlog.PresentationLayer.ViewModels;
using AutoMapper;

namespace TaskBlog.PresentationLayer.Controllers
{
    public class ArticleController : Controller
    {
        ArticleService _articleService;
        TagService _tagService;

        IMapper _modelsMapper;

        public ArticleController(IService<ArticleDTO> articleService, IService<TagDTO> tagService)
        {
            _articleService = articleService as ArticleService;
            _tagService = tagService as TagService;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ArticleDTO, ArticleViewModel>();
                cfg.CreateMap<TagDTO, TagViewModel>();
                cfg.CreateMap<CommentDTO, CommentViewModel>();
                cfg.CreateMap<ArticleViewModel, ArticleDTO>();
            });

            _modelsMapper = config.CreateMapper();
        }

        // GET: Article
        public ActionResult Index()
        {
            var articles = _articleService.GetAll().OrderByDescending(a => a.DateTime).ToList();
            var viewArticles = _modelsMapper.Map<List<ArticleDTO>, List<ArticleViewModel>>(articles);
            ViewBag.Tags = _tagService.GetAll();
            return View(viewArticles);
        }

        public ActionResult Show(int id)
        {
            var article = _articleService.GetById(id);
            var viewArticle = _modelsMapper.Map<ArticleDTO, ArticleViewModel>(article);
            return View(viewArticle);
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Tags = _tagService.GetAll();
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleViewModel article)
        {
            var dtoModel = _modelsMapper.Map<ArticleViewModel, ArticleDTO>(article);
            _articleService.Create(dtoModel, article.TagsId);
            _articleService.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            _articleService.Delete(id);
            _articleService.Save();
            return RedirectToAction("Index");
        }

        public ActionResult GetByTag(int tagId)
        {
            var articles = _articleService.GetByTag(tagId).OrderByDescending(a => a.DateTime).ToList();
            var viewArticles = _modelsMapper.Map<List<ArticleDTO>, List<ArticleViewModel>>(articles);
            return View("Index", viewArticles);
        }
    }
}
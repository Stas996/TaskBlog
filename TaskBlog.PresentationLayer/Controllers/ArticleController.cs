using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.Services;
using TaskBlog.BusinessLogicLayer.DTOModels;
using TaskBlog.PresentationLayer.ViewModels;
using TaskBlog.PresentationLayer.Enums;
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
                cfg.CreateMap<UserProfileDTO, UserProfileViewModel>();
                cfg.CreateMap<ArticleDTO, ArticleViewModel>()
                    .ForMember(dest => dest.TagsId, 
                    opt => opt.MapFrom(src => src.Tags.Select(t => t.Id).ToArray()));
                cfg.CreateMap<TagDTO, TagViewModel>();
                cfg.CreateMap<CommentDTO, CommentViewModel>();
                cfg.CreateMap<ArticleViewModel, ArticleDTO>();
            });

            _modelsMapper = config.CreateMapper();
        }

        // GET: Article
        public ActionResult Index()
        {
            var articles = _articleService.GetConfirmed().OrderByDescending(a => a.DateTime).ToList();
            var viewArticles = _modelsMapper.Map<List<ArticleDTO>, List<ArticleViewModel>>(articles);
            return View(viewArticles);
        }

        public ActionResult FilterArticles(string userId = null, ArticleFilter filter = ArticleFilter.All)
        {
            var articles = (userId == null) ? _articleService.GetAll() : _articleService.GetByUserId(userId);

            switch(filter)
            {
                case ArticleFilter.Confirmed:
                    articles = articles.Where(a => a.IsConfirmed);
                    break;

                case ArticleFilter.NotConfirmed:
                    articles = articles.Where(a => !a.IsConfirmed);
                    break;
            }
        
            var viewArticles = _modelsMapper.Map<List<ArticleDTO>, List<ArticleViewModel>>(articles.ToList());
            return View("Index", viewArticles.ToList());
        }

        public ActionResult Show(int articleId)
        {
            var article = _articleService.GetById(articleId);
            var viewArticle = _modelsMapper.Map<ArticleDTO, ArticleViewModel>(article);
            return View(viewArticle);
        }

        public ActionResult Edit(int articleId)
        {
            var dtoModel = _articleService.GetById(articleId);
            ViewBag.Tags = _tagService.GetAll();
            var viewModel = _modelsMapper.Map<ArticleDTO, ArticleViewModel>(dtoModel);
            return View(viewModel);
        }

        public ActionResult Create()
        {
            ViewBag.Tags = _tagService.GetAll();
            return View();
        }

        public ActionResult Confirm(int articleId)
        {
            _articleService.Confirm(articleId);
            _articleService.Save();
            return Redirect(Request.UrlReferrer.ToString());
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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ArticleViewModel article)
        {
            var dtoModel = _modelsMapper.Map<ArticleViewModel, ArticleDTO>(article);
            _articleService.Update(dtoModel, article.TagsId);
            _articleService.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(int articleId)
        {
            _articleService.Delete(articleId);
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
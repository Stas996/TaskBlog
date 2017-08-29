using System.Linq;
using System.Web.Mvc;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.Services;
using TaskBlog.ViewModels;
using TaskBlog.PresentationLayer.Enums;

namespace TaskBlog.PresentationLayer.Controllers
{
    public class ArticleController : Controller
    {
        ArticleService _articleService;
        TagService _tagService;

        public ArticleController(IService<ArticleViewModel> articleService, IService<TagViewModel> tagService)
        {
            _articleService = articleService as ArticleService;
            _tagService = tagService as TagService;
        }

        // GET: Article
        public ActionResult Index()
        {
            var viewArticles = _articleService.GetConfirmed().OrderByDescending(a => a.DateTime).ToList();
            ViewBag.Title = "Популярные статьи";
            return View(viewArticles);
        }

        public ActionResult FilterArticles(string userId = null, ArticleFilter filter = ArticleFilter.All)
        {
            var viewArticles = (userId == null) ? _articleService.GetAll() : _articleService.GetByUserId(userId);
            ViewBag.Title = (userId == null) ? "Популярные статьи" : "Мои статьи";

            switch (filter)
            {
                case ArticleFilter.Confirmed:
                    viewArticles = viewArticles.Where(a => a.IsConfirmed);
                    break;

                case ArticleFilter.NotConfirmed:
                    viewArticles = viewArticles.Where(a => !a.IsConfirmed);
                    ViewBag.Title = "Неопубликованные статьи";
                    break;
            }
            
            return View("Index", viewArticles.ToList());
        }

        public ActionResult Show(int articleId)
        {
            var viewArticle = _articleService.GetById(articleId);
            return View(viewArticle);
        }

        public ActionResult Edit(int articleId)
        {
            ViewBag.Tags = _tagService.GetAll();
            var viewModel = _articleService.GetById(articleId);
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
            _articleService.Create(article, article.TagsId);
            _articleService.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ArticleViewModel article)
        {
            _articleService.Update(article, article.TagsId);
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
            var viewArticles = _articleService.GetByTag(tagId).OrderByDescending(a => a.DateTime).ToList();
            return View("Index", viewArticles);
        }
    }
}
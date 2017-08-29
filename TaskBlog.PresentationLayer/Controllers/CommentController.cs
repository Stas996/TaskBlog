using System.Linq;
using System.Web.Mvc;
using TaskBlog.ViewModels;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.Services;
using Microsoft.AspNet.Identity;

namespace TaskBlog.PresentationLayer.Controllers
{
    public class CommentController : Controller
    {
        CommentService _service;

        public CommentController(IService<CommentViewModel> service)
        {
            _service = service as CommentService;
        }

        // GET: Article
        public ActionResult GetByArticleId(int articleId)
        {
            var viewModels = _service.GetByArticleId(articleId).OrderByDescending(a => a.DateTime).ToList();
            return View(viewModels);
        }

        public PartialViewResult Create(int parentId)
        {
            return PartialView("_Form", 
                new CommentViewModel() { ParentPostId = parentId, UserId = User.Identity.GetUserId<string>() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentViewModel comment)
        {
            _service.Create(comment);
            _service.Save();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            _service.Save();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
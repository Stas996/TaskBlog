using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using TaskBlog.PresentationLayer.ViewModels;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.Services;
using TaskBlog.BusinessLogicLayer.DTOModels;
using Microsoft.AspNet.Identity;
using AutoMapper;

namespace TaskBlog.PresentationLayer.Controllers
{
    public class CommentController : Controller
    {
        CommentService _service;
        IMapper _modelsMapper;

        public CommentController(IService<CommentDTO> service)
        {
            _service = service as CommentService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserProfileDTO, UserProfileViewModel>();
                cfg.CreateMap<CommentDTO, CommentViewModel>();
            });

            _modelsMapper = config.CreateMapper();
        }

        // GET: Article
        public ActionResult GetByArticleId(int articleId)
        {
            var dtoModels = _service.GetByArticleId(articleId).OrderByDescending(a => a.DateTime).ToList();
            var viewModels = _modelsMapper.Map<List<CommentDTO>, List<CommentViewModel>>(dtoModels);
            return View(viewModels);
        }

        public PartialViewResult Create(int parentId)
        {
            return PartialView("_Form", 
                new CommentViewModel() { ParentPostId = parentId, UserId = User.Identity.GetUserId<string>() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentDTO comment)
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
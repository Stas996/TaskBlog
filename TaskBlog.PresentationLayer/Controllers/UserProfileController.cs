using System.Web.Mvc;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.ViewModels;
using AutoMapper;

namespace TaskBlog.PresentationLayer.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        IService<UserProfileViewModel> _service;
        IMapper _modelsMapper;

        public UserProfileController(IService<UserProfileViewModel> service)
        {
            _service = service;
        }

        // GET: UserProfile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(string userId)
        {
            var viewModel = _service.GetById(userId);
            return View(viewModel);
        }
    }
}
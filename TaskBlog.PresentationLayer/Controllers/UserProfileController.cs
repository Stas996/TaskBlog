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
    public class UserProfileController : Controller
    {
        IService<UserProfileDTO> _service;
        IMapper _modelsMapper;

        public UserProfileController(IService<UserProfileDTO> service)
        {
            _service = service;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserProfileDTO, UserProfileViewModel>();
            });
            _modelsMapper = config.CreateMapper();
        }

        // GET: UserProfile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(string userId)
        {
            var dtoModel = _service.GetById(userId);
            var viewModel = _modelsMapper.Map<UserProfileDTO, UserProfileViewModel>(dtoModel);
            return View(viewModel);
        }
    }
}
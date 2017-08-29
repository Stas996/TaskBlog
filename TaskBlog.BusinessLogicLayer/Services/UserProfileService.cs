using System.Linq;
using System.Collections.Generic;
using TaskBlog.DataLayer;
using TaskBlog.ViewModels;
using TaskBlog.BusinessLogicLayer.Interfaces;
using AutoMapper;
using System;

namespace TaskBlog.BusinessLogicLayer.Services
{
    public class UserProfileService : IService<UserProfileViewModel>
    {
        IRepository<UserProfile> _userRepository;
        IMapper _modelsMapper;

        public UserProfileService(IRepository<UserProfile> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Create(UserProfileViewModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserProfileViewModel> GetAll()
        {
            var models = _userRepository.GetAll().ToList();
            var viewModels = Mapper.Map<List<UserProfile>, List<UserProfileViewModel>>(models);
            return viewModels;
        }

        public UserProfileViewModel GetById(object id)
        {
            var model = _userRepository.GetById(id);
            var viewModel = Mapper.Map<UserProfile, UserProfileViewModel>(model);
            return viewModel;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(UserProfileViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}

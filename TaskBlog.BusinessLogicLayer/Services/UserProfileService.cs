using System.Linq;
using System.Collections.Generic;
using TaskBlog.DataLayer;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.DTOModels;
using AutoMapper;
using System;

namespace TaskBlog.BusinessLogicLayer.Services
{
    public class UserProfileService : IService<UserProfileDTO>
    {
        GenericUnitOfWork _db;
        IRepository<UserProfile> _userRepository;
        IMapper _modelsMapper;

        public UserProfileService(GenericUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
            _userRepository = _db.Repository<UserProfile>();

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<Post, ArticleDTO>()
                //    .ForMember(dest => dest.Tags, opt => opt.Ignore());
                //cfg.CreateMap<Post, CommentDTO>();
                //cfg.CreateMap<Tag, TagDTO>();
                cfg.CreateMap<UserProfile, UserProfileDTO>()
                  .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                  .ForMember(dest => dest.ArticlesCount, opt => opt.MapFrom(src => src.Posts.Where(p => p.ParentPostId == null).Count()));
                cfg.CreateMap<UserProfileDTO, UserProfile>();
            });

            _modelsMapper = config.CreateMapper();
        }

        public void Create(UserProfileDTO model)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserProfileDTO> GetAll()
        {
            var domModels = _userRepository.GetAll().ToList();
            var dtoModels = _modelsMapper.Map<List<UserProfile>, List<UserProfileDTO>>(domModels);
            return dtoModels;
        }

        public UserProfileDTO GetById(object id)
        {
            var domModel = _userRepository.GetById(id);
            var dtoModel = _modelsMapper.Map<UserProfile, UserProfileDTO>(domModel);
            return dtoModel;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(UserProfileDTO model)
        {
            throw new NotImplementedException();
        }
    }
}

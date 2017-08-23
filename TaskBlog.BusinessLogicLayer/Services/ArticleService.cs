using System;
using System.Linq;
using System.Collections.Generic;
using TaskBlog.DataLayer;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.DTOModels;
using AutoMapper;

namespace TaskBlog.BusinessLogicLayer.Services
{
    public class ArticleService : IService<ArticleDTO>
    {
        GenericUnitOfWork _db;
        IRepository<Post> _articleRepository;
        IRepository<Tag> _tagRepository;
        IMapper _modelsMapper;

        public ArticleService(GenericUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
            _articleRepository = _db.Repository<Post>();
            _tagRepository = _db.Repository<Tag>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, CommentDTO>();
                cfg.CreateMap<Tag, TagDTO>();
                cfg.CreateMap<Post, ArticleDTO>()
                    .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Posts));
                cfg.CreateMap<ArticleDTO, Post>();
            });

            _modelsMapper = config.CreateMapper();
        }

        public void Create(ArticleDTO dtoModel)
        {
            var domModel = _modelsMapper.Map<ArticleDTO, Post>(dtoModel);
            _articleRepository.Create(domModel);
        }

        public void Create(ArticleDTO dtoModel, int[] tagsId)
        {
            var domModel = _modelsMapper.Map<ArticleDTO, Post>(dtoModel);         
            foreach (var tagId in tagsId)
            {
                domModel.Tags.Add(_tagRepository.GetById(tagId));
            }
            _articleRepository.Create(domModel);
        }

        public void Delete(int id)
        {
            _articleRepository.Delete(id);
        }

        public IEnumerable<ArticleDTO> GetAll()
        {
            var domModels = _articleRepository
                .GetBy(p => p.ParentPostId == null)
                .ToList();
            
            var dtoModels = _modelsMapper.Map<List<Post>, List<ArticleDTO>>(domModels);
            return dtoModels;
        }

        public IEnumerable<ArticleDTO> GetByTag(int tagId)
        {
            var tag = _tagRepository.GetById(tagId);
            var dtoModels = _modelsMapper.Map<List<Post>, List<ArticleDTO>>(tag.Articles.ToList());
            return dtoModels;
        }

        public ArticleDTO GetById(int id)
        {
            var domModel = _articleRepository.GetById(id);
            var dtoModel = _modelsMapper.Map<Post, ArticleDTO>(domModel);
            return dtoModel;
        }

        public void Update(ArticleDTO dtoModel)
        {
            var domModel = _modelsMapper.Map<ArticleDTO, Post>(dtoModel);
            _articleRepository.Update(domModel);
        }

        public void Save()
        {
            _articleRepository.Save();
        }
    }
}
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
                cfg.CreateMap<UserProfile, UserProfileDTO>();
                cfg.CreateMap<UserProfileDTO, UserProfile>();
                cfg.CreateMap<Post, CommentDTO>()
                    .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Posts));
                //.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
                cfg.CreateMap<Tag, TagDTO>();
                cfg.CreateMap<TagDTO, Tag>();
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
            _articleRepository.Create(domModel);
            AddOrUpdateTags(domModel.Id, tagsId);
        }

        public void Delete(object id)
        {
            var domModel = _articleRepository.GetById(id);
            RecursionPostDelete(domModel);
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

        public IEnumerable<ArticleDTO> GetConfirmed()
        {
            var domModels = _articleRepository
                .GetBy(p => p.IsConfirmed)
                .ToList();

            var dtoModels = _modelsMapper.Map<List<Post>, List<ArticleDTO>>(domModels);
            return dtoModels;
        }

        public IEnumerable<ArticleDTO> GetByTag(int tagId)
        {
            var tag = _tagRepository.GetById(tagId);
            var dtoModels = _modelsMapper.Map<List<Post>, List<ArticleDTO>>(tag.Articles.Where(a => a.IsConfirmed).ToList());
            return dtoModels;
        }

        public IEnumerable<ArticleDTO> GetByUserId(string userId)
        {
            var domModels = _articleRepository.GetBy(a => a.ParentPostId == null && a.UserId == userId).ToList();
            var dtoModels = _modelsMapper.Map<List<Post>, List<ArticleDTO>>(domModels);
            return dtoModels;
        }

        public ArticleDTO GetById(object id)
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

        public void Confirm(int articleId)
        {
            var domModel = _articleRepository.GetById(articleId);
            domModel.IsConfirmed = true;
        }

        public void Update(ArticleDTO dtoModel, int[] tagsId)
        {
            var domModel = _modelsMapper.Map<ArticleDTO, Post>(dtoModel);
            _articleRepository.Update(domModel);
            AddOrUpdateTags(domModel.Id, tagsId);
        }

        public void AddOrUpdateTags(int articleId, int[] tagsId)
        {
            var domModel = _articleRepository.GetById(articleId);
            if (tagsId != null)
            {
                foreach (var tagId in tagsId)
                {
                    domModel.Tags.Add(_tagRepository.GetById(tagId));
                }
            }
        }

        public void Save()
        {
            _articleRepository.Save();
        }

        private void RecursionPostDelete(Post post)
        {
            foreach(var p in post.Posts.ToArray())
            {
                if (p.Posts.Count > 0)
                    RecursionPostDelete(p);
                else
                    _articleRepository.Delete(p.Id);
            }
        }
    }
}
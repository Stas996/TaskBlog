using System.Linq;
using System.Collections.Generic;
using TaskBlog.DataLayer;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.DTOModels;
using AutoMapper;

namespace TaskBlog.BusinessLogicLayer.Services
{
    public class CommentService : IService<CommentDTO>
    {
        GenericUnitOfWork _db;
        IRepository<Post> _postRepository;
        IMapper _modelsMapper;

        public CommentService(GenericUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
            _postRepository = _db.Repository<Post>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, CommentDTO>()
                    .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Posts));
                cfg.CreateMap<CommentDTO, Post>();
            });

            _modelsMapper = config.CreateMapper();
        }

        public void Create(CommentDTO dtoModel)
        {
            var domModel = _modelsMapper.Map<CommentDTO, Post>(dtoModel);
            _postRepository.Create(domModel);
        }

        public void Delete(object id)
        {
            _postRepository.Delete(id);
        }

        public IEnumerable<CommentDTO> GetAll()
        {
            var domModels = _postRepository
                .GetBy(p => p.ParentPostId != null)
                .ToList();

            var dtoModels = _modelsMapper.Map<List<Post>, List<CommentDTO>>(domModels);
            return dtoModels;
        }

        public IEnumerable<CommentDTO> GetByArticleId(int articleId)
        {
            var domModels = _postRepository
                .GetBy(p => p.ParentPost.ParentPostId == null && p.ParentPostId == articleId)
                .ToList();

            var dtoModels = _modelsMapper.Map<List<Post>, List<CommentDTO>>(domModels);
            return dtoModels;
        }

        public CommentDTO GetById(object id)
        {
            var domModel = _postRepository.GetById(id);
            var dtoModel = _modelsMapper.Map<Post, CommentDTO>(domModel);
            return dtoModel;
        }

        public void Update(CommentDTO dtoModel)
        {
            var domModel = _modelsMapper.Map<CommentDTO, Post>(dtoModel);
            _postRepository.Update(domModel);
        }

        public void Save()
        {
            _db.Save();
        }
    }
}
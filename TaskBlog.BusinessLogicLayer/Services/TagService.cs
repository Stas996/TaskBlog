using System.Linq;
using System.Collections.Generic;
using TaskBlog.DataLayer;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.DTOModels;
using AutoMapper;

namespace TaskBlog.BusinessLogicLayer.Services
{
    public class TagService : IService<TagDTO>
    {
        GenericUnitOfWork _db;
        IRepository<Tag> _tagRepository;
        IMapper _modelsMapper;

        public TagService(GenericUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
            _tagRepository = _db.Repository<Tag>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tag, TagDTO>();
                    //.ForMember(dest => dest.Articles, opt => opt.MapFrom(src => src.Articles));
                cfg.CreateMap<TagDTO, Tag>();
            });

            _modelsMapper = config.CreateMapper();
        }

        public void Create(TagDTO dtoModel)
        {
            var domModel = _modelsMapper.Map<TagDTO, Tag>(dtoModel);
            _tagRepository.Create(domModel);
        }

        public void CreateMany(string[] tags)
        {
            foreach (var t in tags.Select(tag => tag.Trim()))
            {
                if (!ContainsAny(t))
                    _tagRepository.Create(new Tag() { Name = t});
            }
        }

        public void Delete(object id)
        {
            _tagRepository.Delete(id);
        }

        public IEnumerable<TagDTO> GetAll()
        {
            var domModels = _tagRepository.GetAll().ToList();
            var dtoModels = _modelsMapper.Map<List<Tag>, List<TagDTO>>(domModels);
            return dtoModels;
        }

        public TagDTO GetById(object id)
        {
            var domModel = _tagRepository.GetById(id);
            var dtoModel = _modelsMapper.Map<Tag, TagDTO>(domModel);
            return dtoModel;
        }

        public void Update(TagDTO dtoModel)
        {
            var domModel = _modelsMapper.Map<TagDTO, Tag>(dtoModel);
            _tagRepository.Update(domModel);
        }

        bool ContainsAny(string tagName)
        {
            return _tagRepository.GetBy(t => t.Name == tagName).Count() > 0;
        }

        public void Save()
        {
            _db.Save();
        }
    }
}

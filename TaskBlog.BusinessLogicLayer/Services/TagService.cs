using System.Linq;
using System.Collections.Generic;
using TaskBlog.DataLayer;
using TaskBlog.ViewModels;
using TaskBlog.BusinessLogicLayer.Interfaces;
using AutoMapper;

namespace TaskBlog.BusinessLogicLayer.Services
{
    public class TagService : IService<TagViewModel>
    {
        IRepository<Tag> _tagRepository;

        public TagService(IRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public void Create(TagViewModel viewModel)
        {
            var model = Mapper.Map<TagViewModel, Tag>(viewModel);
            _tagRepository.Create(model);
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

        public IEnumerable<TagViewModel> GetAll()
        {
            var models = _tagRepository.GetAll().ToList();
            var viewModels = Mapper.Map<List<Tag>, List<TagViewModel>>(models);
            return viewModels;
        }

        public TagViewModel GetById(object id)
        {
            var model = _tagRepository.GetById(id);
            var viewModel = Mapper.Map<Tag, TagViewModel>(model);
            return viewModel;
        }

        public void Update(TagViewModel viewModel)
        {
            var model = Mapper.Map<TagViewModel, Tag>(viewModel);
            _tagRepository.Update(model);
        }

        bool ContainsAny(string tagName)
        {
            return _tagRepository.GetBy(t => t.Name == tagName).Count() > 0;
        }

        public void Save()
        {
            _tagRepository.Save();
        }
    }
}

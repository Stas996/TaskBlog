using System.Linq;
using System.Collections.Generic;
using TaskBlog.DataLayer;
using TaskBlog.ViewModels;
using TaskBlog.BusinessLogicLayer.Interfaces;
using AutoMapper;

namespace TaskBlog.BusinessLogicLayer.Services
{
    public class ArticleService : IService<ArticleViewModel>
    {
        IRepository<Post> _articleRepository;
        IRepository<Tag> _tagRepository;

        public ArticleService(IRepository<Post> articleRepository,
            IRepository<Tag> tagRepository)
        {
            _articleRepository = articleRepository;
            _tagRepository = tagRepository;
        }

        public void Create(ArticleViewModel viewModel)
        {
            var domModel = Mapper.Map<ArticleViewModel, Post>(viewModel);
            _articleRepository.Create(domModel);
        }

        public void Create(ArticleViewModel viewModel, int[] tagsId)
        {
            var domModel = Mapper.Map<ArticleViewModel, Post>(viewModel);
            _articleRepository.Create(domModel);
            AddOrUpdateTags(domModel.Id, tagsId);
        }

        public void Delete(object id)
        {
            var domModel = _articleRepository.GetById(id);
            RecursionPostDelete(domModel);
            _articleRepository.Delete(id);
        }

        public IEnumerable<ArticleViewModel> GetAll()
        {
            var models = _articleRepository
                .GetBy(p => p.ParentPostId == null)
                .ToList();
            var viewModels = Mapper.Map<List<Post>, List<ArticleViewModel>>(models);
            return viewModels;
        }

        public IEnumerable<ArticleViewModel> GetConfirmed()
        {
            var models = _articleRepository
                .GetBy(p => p.IsConfirmed)
                .ToList();

            var viewModels = Mapper.Map<List<Post>, List<ArticleViewModel>>(models);
            return viewModels;
        }

        public IEnumerable<ArticleViewModel> GetByTag(int tagId)
        {
            var tag = _tagRepository.GetById(tagId);
            var viewModels = Mapper
                .Map<List<Post>, List<ArticleViewModel>>(tag.Articles.Where(a => a.IsConfirmed).ToList());
            return viewModels;
        }

        public IEnumerable<ArticleViewModel> GetByUserId(string userId)
        {
            var models = _articleRepository.GetBy(a => a.ParentPostId == null && a.UserId == userId).ToList();
            var viewModels = Mapper.Map<List<Post>, List<ArticleViewModel>>(models);
            return viewModels;
        }

        public ArticleViewModel GetById(object id)
        {
            var model = _articleRepository.GetById(id);
            var viewModel = Mapper.Map<Post, ArticleViewModel>(model);
            return viewModel;
        }

        public void Update(ArticleViewModel viewModel)
        {
            var model = Mapper.Map<ArticleViewModel, Post>(viewModel);
            _articleRepository.Update(model);
        }

        public void Confirm(int articleId)
        {
            var domModel = _articleRepository.GetById(articleId);
            domModel.IsConfirmed = true;
        }

        public void Update(ArticleViewModel viewModel, int[] tagsId)
        {
            var model = Mapper.Map<ArticleViewModel, Post>(viewModel);
            _articleRepository.Update(model);
            AddOrUpdateTags(model.Id, tagsId);
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

        private void RecursionPostDelete(Post article)
        {
            foreach(var p in article.Posts.ToArray())
            {
                if (p.Posts.Count > 0)
                    RecursionPostDelete(p);
                else
                    _articleRepository.Delete(p.Id);
            }
        }

        public void Save()
        {
            _articleRepository.Save();
        }
    }
}
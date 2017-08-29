using System.Linq;
using System.Collections.Generic;
using TaskBlog.DataLayer;
using TaskBlog.ViewModels;
using TaskBlog.BusinessLogicLayer.Interfaces;
using AutoMapper;

namespace TaskBlog.BusinessLogicLayer.Services
{
    public class CommentService : IService<CommentViewModel>
    {
        IRepository<Post> _postRepository;
        IMapper _modelsMapper;

        public CommentService(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public void Create(CommentViewModel viewModel)
        {
            var model = Mapper.Map<CommentViewModel, Post>(viewModel);
            _postRepository.Create(model);
        }

        public void Delete(object id)
        {
            var domModel = _postRepository.GetById(id);
            RecursionPostDelete(domModel);
            _postRepository.Delete(id);
        }

        public IEnumerable<CommentViewModel> GetAll()
        {
            var models = _postRepository
                .GetBy(p => p.ParentPostId != null)
                .ToList();

            var viewModels = Mapper.Map<List<Post>, List<CommentViewModel>>(models);
            return viewModels;
        }

        public IEnumerable<CommentViewModel> GetByArticleId(int articleId)
        {
            var models = _postRepository
                .GetBy(p => p.ParentPost.ParentPostId == null && p.ParentPostId == articleId)
                .ToList();

            var viewModels = Mapper.Map<List<Post>, List<CommentViewModel>>(models);
            return viewModels;
        }

        public CommentViewModel GetById(object id)
        {
            var model = _postRepository.GetById(id);
            var viewModel = Mapper.Map<Post, CommentViewModel>(model);
            return viewModel;
        }

        public void Update(CommentViewModel viewModel)
        {
            var model = Mapper.Map<CommentViewModel, Post>(viewModel);
            _postRepository.Update(model);
        }

        public void Save()
        {
            _postRepository.Save();
        }

        private void RecursionPostDelete(Post post)
        {
            foreach (var p in post.Posts.ToArray())
            {
                if (p.Posts.Count > 0)
                    RecursionPostDelete(p);
                else
                    _postRepository.Delete(p.Id);
            }
        }
    }
}
using AutoMapper;
using TaskBlog.DataLayer;
using TaskBlog.ViewModels;

namespace TaskBlog.BusinessLogicLayer.Mapping
{
    public static class MappingConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Post, ArticleViewModel>()
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Posts));
                cfg.CreateMap<Post, CommentViewModel>()
                    .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Posts));
                cfg.CreateMap<UserProfile, UserProfileViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();
                cfg.CreateMap<ArticleViewModel, Post>();
                cfg.CreateMap<CommentViewModel, Post>();
                cfg.CreateMap<UserProfileViewModel, UserProfile>();
                cfg.CreateMap<TagViewModel, Tag>();
                cfg.CreateMap<RegisterViewModel, User>();
                cfg.CreateMap<LoginViewModel, User>();
            });
        }
    }
}
using System;

namespace TaskBlog.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ParentPostId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateTime { get; set; }

        public CommentViewModel[] Comments { get; set; }

        public UserProfileViewModel User { get; set; }
    }
}
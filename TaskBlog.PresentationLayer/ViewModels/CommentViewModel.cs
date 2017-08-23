using System;

namespace TaskBlog.PresentationLayer.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public int ParentPostId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateTime { get; set; }
    }
}
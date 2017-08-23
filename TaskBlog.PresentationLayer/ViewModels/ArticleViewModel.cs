using System;

namespace TaskBlog.PresentationLayer.ViewModels
{
    public class ArticleViewModel
    {
        public ArticleViewModel()
        {
            Tags = new TagViewModel[] { };
            Comments = new CommentViewModel[] { };
            DateTime = System.DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateTime { get; set; }

        public int[] TagsId { get; set; }

        public string ShortDescription
        {
            get
            {
                if (Description.Length > 200)
                    return Description.Substring(0, 200);
                return Description;
            }
        }

        public TagViewModel[] Tags { get; set; }

        public CommentViewModel[] Comments { get; set; }
    }
}
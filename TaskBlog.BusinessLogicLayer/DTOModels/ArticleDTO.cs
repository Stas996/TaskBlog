using System;
using System.Collections.Generic;

namespace TaskBlog.BusinessLogicLayer.DTOModels
{
    public class ArticleDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateTime { get; set; }

        public virtual ICollection<CommentDTO> Comments { get; set; }

        public virtual ICollection<TagDTO> Tags { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;

namespace TaskBlog.BusinessLogicLayer.DTOModels
{
    public class CommentDTO
    {
        public CommentDTO()
        {
            DateTime = System.DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public int? ParentPostId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateTime { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }

        public UserProfileDTO User { get; set; }

    }
}

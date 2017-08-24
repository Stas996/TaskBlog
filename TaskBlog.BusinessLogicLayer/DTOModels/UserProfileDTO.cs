using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBlog.BusinessLogicLayer.DTOModels
{
    public class UserProfileDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public ICollection<ArticleDTO> Articles { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }
    }
}

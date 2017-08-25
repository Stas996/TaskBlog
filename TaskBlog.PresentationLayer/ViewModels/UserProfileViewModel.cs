using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskBlog.PresentationLayer.ViewModels
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public string Country { get; set; }

        public string City { get; set; }

        public int ArticlesCount { get; set; }

        //public ArticleViewModel[] Articles { get; set; }

        //public CommentViewModel[] Comments { get; set; }
    }
}
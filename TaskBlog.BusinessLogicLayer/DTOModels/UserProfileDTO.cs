namespace TaskBlog.BusinessLogicLayer.DTOModels
{
    public class UserProfileDTO
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int ArticlesCount { get; set; }

        //public ICollection<ArticleDTO> Articles { get; set; }

        //public ICollection<CommentDTO> Comments { get; set; }
    }
}

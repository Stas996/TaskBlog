namespace TaskBlog.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    //Post can be article or comment
    [Table("Post")]
    public partial class Post
    {
        public int Id { get; set; }

        [ForeignKey("ParentPost")]
        public int? ParentPostId { get; set; }

        [ForeignKey("UserProfile")]
        public string UserProfileId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime? DateTime { get; set; }

        public virtual Post ParentPost { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}

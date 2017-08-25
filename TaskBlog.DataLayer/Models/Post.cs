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

        [ForeignKey("User")]
        public string UserId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime? DateTime { get; set; }

        public virtual Post ParentPost { get; set; }

        public virtual UserProfile User { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}

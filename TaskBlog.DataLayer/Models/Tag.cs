namespace TaskBlog.DataLayer
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tag")]
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        //[Index("TagName", IsUnique = true)]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Post> Articles { get; set; }
    }
}

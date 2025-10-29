using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myblogs.Models
{
    public class CommentsModel
    {
        [Key]
        public int CommentsId { get; set; }
        [ForeignKey("BlogId")]
        public int BlogId { get; set; }

        [DataType(DataType.MultilineText)]

        public string? Comment { get; set; }
        public string? UserId { get; set; }
        public string? NickName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace myblogs.Models.buttonTemplates
{
    public class BlogStatus
    {
        [Key]
        public int BlogStatusId { get; set; }
        public string? BlogStatusText { get; set; }
        public string? UserId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
    }
}

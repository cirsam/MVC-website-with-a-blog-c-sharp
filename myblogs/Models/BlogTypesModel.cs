using System.ComponentModel.DataAnnotations;

namespace myblogs.Models
{
    public class BlogTypesModel
    {
        [Key]
        public int BlogTypeId { get; set; }
        public string? UserId { get; set; }
        public string? BlogTypeName { get; set; }
        [DataType(DataType.MultilineText)]
        public string? BlogTypeDescription { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateCreated {get;set;}
    }
}

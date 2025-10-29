using System.ComponentModel.DataAnnotations;

namespace myblogs.Models.buttonTemplates
{
    public class BlogTypesSellectListItem
    {
        [Key]
        public int BlogTypesSellectListItemId { get; set; }
        public int BlogTypeId { get; set; }
        public string? BlogTypeName { get; set; }
    }
}

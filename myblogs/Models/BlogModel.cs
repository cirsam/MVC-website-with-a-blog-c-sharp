
using myblogs.Models.buttonTemplates;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace myblogs.Models
{
    public class BlogModel
    {


        [Key]
        public int BlogId { get; set; }

       
        public string? UserId { get; set; }
        public string? BlogTitle { get; set; }

        [ForeignKey("BlogTypeId")]
        public int SelectedblogTypeId { get; set; }
        [NotMapped]
        public IEnumerable<BlogTypesSellectListItem>? BlogTypesSellecListItemsModel { get; set; }
        [ForeignKey("BlogPublishStatusOptionsId")]
        public int SelectedBlogPublishStatusOptionsId { get; set; }
        [NotMapped]
        public IEnumerable<BlogStatus>? BloghStatus { get; set; }

        [DataType(DataType.MultilineText)]
        public string? BlogText { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Datecreated { get; set; }
    }
}

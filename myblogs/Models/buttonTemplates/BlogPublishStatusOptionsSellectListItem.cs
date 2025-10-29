using System.ComponentModel.DataAnnotations;

namespace myblogs.Models.buttonTemplates
{
	public class BlogPublishStatusOptionsSellectListItems
	{
		
		[Key]
		
		public int BlogPublishStatusOptionsSellectListItemId { get;set;}
		public string? BlogStatusText { get; set; }
		
	}		

	
}

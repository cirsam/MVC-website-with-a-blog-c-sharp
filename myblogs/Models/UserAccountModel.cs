using Microsoft.AspNetCore.Antiforgery;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

using System.ComponentModel.DataAnnotations;


namespace myblogs.Models
{
    public class UserAccountModel
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        public string? EmailAddress { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string? Password { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public bool AccountConfirmation { get; set; }
        public DateTime LastLogedIn { get; set; }

        public bool RememberMe { get; set; }


    }

}

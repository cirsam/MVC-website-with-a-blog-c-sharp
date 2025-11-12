using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myblogs.Models;
using myblogs.Models.buttonTemplates;

namespace myblogs.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        { }
        public DbSet<BlogModelClass> Blogs { get; set; }
        public DbSet<BlogTypesModel> BlogsTypes { get; set; }
        public DbSet<CommentsModel> Comments { get; set; }
        public DbSet<BlogStatus> BlogStatus { get; set; }



    }
}


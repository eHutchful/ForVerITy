using DivineApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DivineApp.Contexts
{
    public class MyContext:IdentityDbContext<CompanyUser>
    {
        public MyContext() : base("DefaultConnection") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
        public static MyContext Create()
        {
            return new MyContext();
        }

    }
}
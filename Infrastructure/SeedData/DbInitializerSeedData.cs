using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData
{
    public static class DbInitializerSeedData
    {
        public static void InitializeDatabase(BlogDbContext blogDbContext)
        {
            if (blogDbContext.Blogs.Any())
                return;

            var blogs = new Blog[]
            {
                new Blog
                {
                  Name = "Architecture",
                  Description = "Demo data"

                },
                new Blog
                {
                  Name = "Architecture 2",
                  Description = "Demo data"

                }
            };
            blogDbContext.Blogs.AddRangeAsync(blogs);
            blogDbContext.SaveChanges();

            if (!blogDbContext.Authors.Any())
            {
                var Authors = new Author[] { new Author { Name = "Sumair", Email = "sumairk801@gmail.com" } };
                blogDbContext.Authors.AddRangeAsync(Authors);
                blogDbContext.SaveChanges();
            }
               
        }
    }
}

using Microsoft.EntityFrameworkCore;
using BlogCrudApp.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Mvc;

namespace tests
{
    public class ContextGenerator
    {
        public static ApiDbContext Generator()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: "blog-crud-app")
                .Options;

            var context = new ApiDbContext(optionsBuilder);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }
}


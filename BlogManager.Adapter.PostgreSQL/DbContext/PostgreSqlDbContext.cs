using BlogManager.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Adapter.PostgreSQL.DbContext;

public class PostgreSqlDbContext : Microsoft.EntityFrameworkCore.DbContext, IPostgreSqlDbContext
{
        public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options)
        {
            // this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Blog>   Blogs   { get; set; }
        public DbSet<Author> Authors { get; set; }
}
using BlogManager.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Adapter.PostgreSQL.DbContext;

public interface IPostgreSqlDbContext
{
    DbSet<Blog>   Blogs   { get; set; }
    DbSet<Author> Authors { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
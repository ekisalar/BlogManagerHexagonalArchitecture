using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Core.Tests;

public static class DbContextFactory
{
    public static async Task<PostgreSqlDbContext> CreatePostgreSqlInMemoryDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostgreSqlDbContext>()
           .UseInMemoryDatabase("PostgreSqlInMemoryDatabase");

        var dbContext = new PostgreSqlDbContext(optionsBuilder.Options);
        await GenerateInitialDataAsync(dbContext);
        return dbContext;
    }

    private static async Task GenerateInitialDataAsync(PostgreSqlDbContext dbContext)
    {
        await dbContext.Authors.AddRangeAsync(await Author.CreateAsync("TestName 1", "TestSurname 1"),
                                              await Author.CreateAsync("TestName 2", "TestSurname 2"),
                                              await Author.CreateAsync("TestName 3", "TestSurname 3"),
                                              await Author.CreateAsync("TestName 4", "TestSurname 4"),
                                              await Author.CreateAsync("TestName 5", "TestSurname 5"));
        await dbContext.SaveChangesAsync();

        var authorIds = dbContext.Authors.Select(a => a.Id).ToArray();

        await dbContext.Blogs.AddRangeAsync(await Blog.CreateAsync(authorIds[0], "Test Title 1", "Test Description 1", "Test Content 1 "),
                                            await Blog.CreateAsync(authorIds[1], "Test Title 2", "Test Description 2", "Test Content 2"),
                                            await Blog.CreateAsync(authorIds[2], "Test Title 3", "Test Description 3", "Test Content 3"),
                                            await Blog.CreateAsync(authorIds[3], "Test Title 4", "Test Description 4", "Test Content 4"),
                                            await Blog.CreateAsync(authorIds[4], "Test Title 4", "Test Description 4", "Test Content 4"));
        await dbContext.SaveChangesAsync();
    }
}
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Handlers.QueryHandlers;
using BlogManager.Core.Queries;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Core.Tests.BlogTests;

public class BlogGetByIdTest
{
    private IPostgreSqlDbContext dbContext;

    [SetUp]
    public async Task Setup()
    {
        dbContext = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
    }

    [Test]
    public async Task BlogGetListTest_MustReturnCorrectBlog()
    {
        var handler           = new GetBlogByIdQueryHandler(new BlogRepository(dbContext));
        var blogFromDbContext = await dbContext.Blogs.FirstAsync();

        var result = await handler.Handle(new GetBlogByIdQuery() {Id = blogFromDbContext.Id}, new CancellationToken());
        result.Should().NotBeNull();
        result.Id.Should().Be(blogFromDbContext.Id);
    }
}
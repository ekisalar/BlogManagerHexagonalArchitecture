using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Handlers.QueryHandlers;
using BlogManager.Core.Queries;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Core.Tests.BlogTests;

public class BlogGetListTest
{
    private IPostgreSqlDbContext dbContext;

    [SetUp]
    public async Task Setup()
    {
        dbContext = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
    }

    [Test]
    public async Task BlogGetListTest_MustReturnCorrectListOfData_NotIncludeAuthorInfo()
    {
        var handler               = new GetBlogListQueryHandler(new BlogRepository(dbContext));
        var result                = await handler.Handle(new GetBlogListQuery(), new CancellationToken());
        var blogListFromDbContext = await dbContext.Blogs.ToListAsync();
        result.Should().NotBeNull();
        result.Blogs.Should().NotBeNull();
        result.Blogs.Any(b => b.Author != null).Should().BeFalse();
        result.Blogs.Count.Should().Be(blogListFromDbContext.Count);
        result.Blogs.Any(b => b.Title == "Test Title 1").Should().BeTrue();
        result.Blogs.Any(b => b.Description == "Test Description 4").Should().BeTrue();
    }

    [Test]
    public async Task BlogGetListTest_MustReturnCorrectListOfData_IncludeAuthorInfo()
    {
        var handler               = new GetBlogListQueryHandler(new BlogRepository(dbContext));
        var result                = await handler.Handle(new GetBlogListQuery() {IncludeAuthorInfo = true}, new CancellationToken());
        var blogListFromDbContext = await dbContext.Blogs.Include(b => b.Author).ToListAsync();
        result.Should().NotBeNull();
        result.Blogs.Should().NotBeNull();
        result.Blogs.All(b => b.Author != null).Should().BeTrue();
        result.Blogs.Count.Should().Be(blogListFromDbContext.Count);
        result.Blogs.Any(b => b.Title == "Test Title 1").Should().BeTrue();
        result.Blogs.Any(b => b.Description == "Test Description 4").Should().BeTrue();
    }
}
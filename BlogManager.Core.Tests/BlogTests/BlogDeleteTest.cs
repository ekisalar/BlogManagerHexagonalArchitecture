using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Commands.Blog;
using BlogManager.Core.Handlers.CommandHandlers.Blog;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Core.Tests.BlogTests;

public class BlogDeleteTest
{
    private IPostgreSqlDbContext dbContext;

    [SetUp]
    public async Task Setup()
    {
        dbContext = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
    }

    [Test]
    public async Task BlogDeleteTest_MustRemoveFromDb()
    {
        var blogDeleteHandler = new DeleteBlogCommandHandler(new BlogRepository(dbContext));
        var blogToDelete      = await dbContext.Blogs.FirstAsync();
        var deleteBlogCommand = new DeleteBlogCommand()
                                {
                                    Id = blogToDelete.Id,
                                };
        var result = await blogDeleteHandler.Handle(deleteBlogCommand, new CancellationToken());
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Id.Should().Be(deleteBlogCommand.Id);
        dbContext.Blogs.FirstOrDefault(b => b.Id == result.Id).Should().BeNull();
    }
}
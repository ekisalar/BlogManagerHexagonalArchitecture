using BlockManager.Tests.Shared;
using BlogManager.Adapter.Logger;
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Commands.Blog;
using BlogManager.Core.Handlers.CommandHandlers.Blog;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BlogManager.Core.Tests.BlogTests;

public class BlogUpdateTest
{
    private IBlogDbContext           dbContext;
    private Mock<IBlogManagerLogger> mockLogger;


    [SetUp]
    public async Task Setup()
    {
        dbContext  = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
        mockLogger = new Mock<IBlogManagerLogger>();

    }

    [Test]
    public async Task BlogUpdateTest_MustReturnCorrectIdAndTitle()
    {
        var blogUpdateHandler = new UpdateBlogCommandHandler(new BlogRepository(dbContext), mockLogger.Object);
        var blogToUpdate      = await dbContext.Blogs.FirstAsync();
        var updateBlogCommand = new UpdateBlogCommand()
                                {
                                    Id          = blogToUpdate.Id,
                                    AuthorId    = Guid.NewGuid(),
                                    Title       = "Test Title Updated",
                                    Description = "Test Description Updated",
                                    Content     = "Test Content Updated"
                                };
        var result = await blogUpdateHandler.Handle(updateBlogCommand, new CancellationToken());
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        var updatedBlogInDb = dbContext.Blogs.FirstOrDefault(b => b.Id == result.Id);
        updatedBlogInDb.Should().NotBeNull();
        updatedBlogInDb.AuthorId.Should().Be(updateBlogCommand.AuthorId);
        updatedBlogInDb.Title.Should().Be(updateBlogCommand.Title);
        updatedBlogInDb.Description.Should().Be(updateBlogCommand.Description);
        updatedBlogInDb.Content.Should().Be(updateBlogCommand.Content);
    }
}
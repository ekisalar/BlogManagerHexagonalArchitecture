using BlockManager.Tests.Shared;
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Commands.Blog;
using BlogManager.Core.Handlers.CommandHandlers.Blog;
using FluentAssertions;

namespace BlogManager.Core.Tests.BlogTests;

public class BlogCreateTest
{
    IBlogDbContext dbContext;

    [SetUp]
    public async Task Setup()
    {
        dbContext = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
    }

    [Test]
    public async Task BlogCreateTest_MustReturnCorrectAuthorId()
    {
        var blogCommandHandler = new CreateBlogCommandHandler(new BlogRepository(dbContext));
        var createBlogCommand  = new CreateBlogCommand(Guid.NewGuid(), "Test Title", "Test Description", "Test Content");
        var result             = await blogCommandHandler.Handle(createBlogCommand, new CancellationToken());
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        var createdBlogInDb = dbContext.Blogs.FirstOrDefault(b => b.Id == result.Id);
        createdBlogInDb.Should().NotBeNull();
        createdBlogInDb.AuthorId.Should().Be(createBlogCommand.AuthorId);
    }
}
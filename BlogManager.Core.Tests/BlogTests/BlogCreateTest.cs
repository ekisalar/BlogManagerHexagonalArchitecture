using BlockManager.Tests.Shared;
using BlogManager.Adapter.Logger;
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Commands.Blog;
using BlogManager.Core.Constants;
using BlogManager.Core.Handlers.CommandHandlers.Blog;
using FluentAssertions;
using Moq;

namespace BlogManager.Core.Tests.BlogTests;

public class BlogCreateTest
{
    IBlogDbContext                   dbContext;
    private Mock<IBlogManagerLogger> mockLogger;


    [SetUp]
    public async Task Setup()
    {
        dbContext  = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
        mockLogger = new Mock<IBlogManagerLogger>();
    }

    [Test]
    public async Task BlogCreateTest_MustReturnCorrectAuthorId()
    {
        var blogCommandHandler = new CreateBlogCommandHandler(new BlogRepository(dbContext), mockLogger.Object, new AuthorRepository(dbContext));
        var author             = dbContext.Authors.First();
        var createBlogCommand  = new CreateBlogCommand(author.Id, "Test Title", "Test Description", "Test Content");
        var result             = await blogCommandHandler.Handle(createBlogCommand, new CancellationToken());
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        var createdBlogInDb = dbContext.Blogs.FirstOrDefault(b => b.Id == result.Id);
        createdBlogInDb.Should().NotBeNull();
        createdBlogInDb.AuthorId.Should().Be(createBlogCommand.AuthorId);
    }

    [Test]
    public async Task BlogCreateTest_MustReturnExceptionAuthorIdNotFound()
    {
        var blogCommandHandler = new CreateBlogCommandHandler(new BlogRepository(dbContext), mockLogger.Object, new AuthorRepository(dbContext));
        var createBlogCommand  = new CreateBlogCommand(Guid.NewGuid(), "Test Title", "Test Description", "Test Content");

        Func<Task> act = async () => await blogCommandHandler.Handle(createBlogCommand, new CancellationToken());

        await act.Should().ThrowAsync<Exception>().WithMessage(ExceptionConstants.AuthorNotFound);
    }
}
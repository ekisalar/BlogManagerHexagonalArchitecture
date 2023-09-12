using BlockManager.Tests.Shared;
using BlogManager.Adapter.Logger;
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Commands.Author;
using BlogManager.Core.Handlers.CommandHandlers.Author;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BlogManager.Core.Tests.AuthorTests;

public class AuthorDeleteTest
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
    public async Task AuthorDeleteTest_MustRemoveFromDb()
    {
        var authorDeleteHandler = new DeleteAuthorCommandHandler(new AuthorRepository(dbContext), mockLogger.Object);
        var authorToDelete      = await dbContext.Authors.FirstAsync();
        var deleteAuthorCommand = new DeleteAuthorCommand()
                                  {
                                      Id = authorToDelete.Id,
                                  };
        var result = await authorDeleteHandler.Handle(deleteAuthorCommand, new CancellationToken());
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Id.Should().Be(deleteAuthorCommand.Id);
        dbContext.Authors.FirstOrDefault(b => b.Id == result.Id).Should().BeNull();
    }
}
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Commands.Author;
using BlogManager.Core.Handlers.CommandHandlers.Author;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Core.Tests.AuthorTests;

public class AuthorDeleteTest
{
    private IPostgreSqlDbContext dbContext;

    [SetUp]
    public async Task Setup()
    {
        dbContext = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
    }

    [Test]
    public async Task AuthorDeleteTest_MustRemoveFromDb()
    {
        var authorDeleteHandler = new DeleteAuthorCommandHandler(new AuthorRepository(dbContext));
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
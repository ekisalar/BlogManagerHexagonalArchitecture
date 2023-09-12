using BlockManager.Tests.Shared;
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Commands.Author;
using BlogManager.Core.Handlers.CommandHandlers.Author;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Core.Tests.AuthorTests;

public class AuthorUpdateTest
{
    private IBlogDbContext dbContext;

    [SetUp]
    public async Task Setup()
    {
        dbContext = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
    }

    [Test]
    public async Task AuthorUpdateTest_MustReturnCorrectIdAndTitle()
    {
        var authorUpdateHandler = new UpdateAuthorCommandHandler(new AuthorRepository(dbContext));
        var authorToUpdate      = await dbContext.Authors.FirstAsync();
        var updateAuthorCommand = new UpdateAuthorCommand()
                                  {
                                      Id      = authorToUpdate.Id,
                                      Name    = "TestName",
                                      Surname = "TestSurname"
                                  };
        var result = await authorUpdateHandler.Handle(updateAuthorCommand, new CancellationToken());
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        var updatedAuthorInDb = dbContext.Authors.FirstOrDefault(b => b.Id == result.Id);
        updatedAuthorInDb.Should().NotBeNull();
        updatedAuthorInDb.Id.Should().Be(updateAuthorCommand.Id);
        updatedAuthorInDb.Name.Should().Be(updateAuthorCommand.Name);
        updatedAuthorInDb.Surname.Should().Be(updateAuthorCommand.Surname);
    }
}
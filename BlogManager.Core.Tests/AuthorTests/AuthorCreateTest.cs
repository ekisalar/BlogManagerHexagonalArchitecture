using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Commands.Author;
using BlogManager.Core.Handlers.CommandHandlers.Author;
using FluentAssertions;

namespace BlogManager.Core.Tests.AuthorTests;

public class AuthorCreateTest
{
    IPostgreSqlDbContext dbContext;

    [SetUp]
    public async Task Setup()
    {
        dbContext = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
    }

    [Test]
    public async Task AuthorCreateTest_MustReturnCorrectNameAndSurname()
    {
        var authorCommandHandler = new CreateAuthorCommandHandler(new AuthorRepository(dbContext));
        var createAuthorCommand  = new CreateAuthorCommand("TestName", "TestSurname");

        var result = await authorCommandHandler.Handle(createAuthorCommand, new CancellationToken());
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        var createdAuthorInDb = dbContext.Authors.FirstOrDefault(b => b.Id == result.Id);
        createdAuthorInDb.Should().NotBeNull();
        createdAuthorInDb.Name.Should().Be(createAuthorCommand.Name);
        createdAuthorInDb.Surname.Should().Be(createAuthorCommand.Surname);
    }
}
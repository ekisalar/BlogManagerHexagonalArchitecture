using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Handlers.QueryHandlers;
using BlogManager.Core.Queries;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Core.Tests.AuthorTests;

public class AuthorGetByIdTest
{
    private IPostgreSqlDbContext dbContext;

    [SetUp]
    public async Task Setup()
    {
        dbContext = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
    }

    [Test]
    public async Task AuthorGetByIdTest_MustReturnCorrectAuthor()
    {
        var handler           = new GetAuthorByIdQueryHandler(new AuthorRepository(dbContext));
        var authorFromDbContext = await dbContext.Authors.FirstAsync();

        var result = await handler.Handle(new GetAuthorByIdQuery(authorFromDbContext.Id), CancellationToken.None);
        result.Should().NotBeNull();
        result.Id.Should().Be(authorFromDbContext.Id);
    }
    
}
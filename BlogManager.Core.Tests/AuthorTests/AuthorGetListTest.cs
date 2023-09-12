using BlockManager.Tests.Shared;
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Handlers.QueryHandlers;
using BlogManager.Core.Queries;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Core.Tests.AuthorTests;

public class AuthorGetListTest
{
    private IBlogDbContext dbContext;

    [SetUp]
    public async Task Setup()
    {
        dbContext = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
    }

    [Test]
    public async Task AuthorGetListTest_MustReturnCorrectListOfData()
    {
        var handler                 = new GetAuthorListQueryHandler(new AuthorRepository(dbContext));
        var result                  = await handler.Handle(new GetAuthorListQuery(), new CancellationToken());
        var authorListFromDbContext = await dbContext.Authors.ToListAsync();
        result.Should().NotBeNull();
        result.Should().NotBeNull();
        result.Count.Should().Be(authorListFromDbContext.Count);
    }
}
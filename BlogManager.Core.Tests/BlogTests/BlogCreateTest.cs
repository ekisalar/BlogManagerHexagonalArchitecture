using System.ComponentModel.Design;
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Commands.Blog;
using BlogManager.Core.Handlers.CommandHandlers.Blog;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManager.Core.Tests.BlogTests;

public class BlogCreateTest
{
    IPostgreSqlDbContext dbContext;

    [SetUp]
    public async Task Setup()
    {
        dbContext = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
    }

    // [Test]
    // public async Task BlogCreateTest_MustReturnCorrectAuthorId()
    // {
    //     var blogCommandHandler = new CreateBlogCommandHandler(new BlogRepository(dbContext), );
    //     var createBlogCommand = new CreateBlogCommand
    //                             {
    //                                 AuthorId    = Guid.NewGuid(),
    //                                 Title       = "Test Title",
    //                                 Description = "Test Description",
    //                                 Content     = "Test Content"
    //                             };
    //     var result = await blogCommandHandler.Handle(createBlogCommand, new CancellationToken());
    //     result.Should().NotBeNull();
    //     result.Id.Should().NotBeEmpty();
    //     var createdBlogInDb = dbContext.Blogs.FirstOrDefault(b => b.Id == result.Id);
    //     createdBlogInDb.Should().NotBeNull();
    //     createdBlogInDb.AuthorId.Should().Be(createBlogCommand.AuthorId);
    // }
}
using BlockManager.Tests.Shared;
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BlogManager.Adapter.PostgreSQL.Tests
{
    [TestFixture]
    public class BlogRepositoryTests
    {
        private IBlogDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = await DbContextFactory.CreatePostgreSqlInMemoryDbContext();
        }

        [Test]
        public async Task GetBlogByIdAsync_ReturnsBlogIfExists()
        {
            // Arrange
            var blogId            = dbContext.Blogs.First().Id;
            var includeAuthorInfo = true;
            var blogRepository    = new BlogRepository(dbContext);

            // Act
            var result = await blogRepository.GetBlogByIdAsync(blogId, includeAuthorInfo);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(blogId);
        }

        [Test]
        public async Task GetBlogByIdAsync_ReturnsNullForNonExistentBlog()
        {
            // Arrange
            var blogId            = Guid.NewGuid();
            var includeAuthorInfo = true;
            var blogRepository    = new BlogRepository(dbContext);

            // Act
            var result = await blogRepository.GetBlogByIdAsync(blogId, includeAuthorInfo);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task GetAllBlogsAsync_ReturnsListOfBlogs()
        {
            // Arrange
            var includeAuthorInfo = true;
            var blogRepository    = new BlogRepository(dbContext);

            // Act
            var result = await blogRepository.GetAllBlogsAsync(includeAuthorInfo);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Blog>>();
            result.All(r => r.Author != null).Should().BeTrue();
        }

        [Test]
        public async Task AddBlogAsync_AddsBlogToDatabase()
        {
            // Arrange
            var blog           = await Blog.CreateAsync(dbContext.Authors.First().Id, "TestTitle", "TestDescription", "TestContent");
            var blogRepository = new BlogRepository(dbContext);

            // Act
            var result = await blogRepository.AddBlogAsync(blog);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();

            // Check if the blog is actually added to the database
            var addedBlog = await dbContext.Blogs.FirstOrDefaultAsync(b => b.Id == result.Id);
            addedBlog.Should().NotBeNull();
            addedBlog.Title.Should().Be("TestTitle");
        }

        [Test]
        public async Task UpdateAsync_UpdatesBlogInDatabase()
        {
            // Arrange
            var blogRepository = new BlogRepository(dbContext);
            var blog           = await dbContext.Blogs.FirstAsync();

            // Modify the blog
            await Blog.UpdateAsync(blog, blog.AuthorId,  "UpdatedTitle", "UpdatedDescription", "UpdatedContent");

            // Act
            var updatedBlog = await blogRepository.UpdateAsync(blog);

            // Assert
            updatedBlog.Should().NotBeNull();
            updatedBlog.Title.Should().Be("UpdatedTitle");

            // Check if the blog is updated in the database
            var dbBlog = await dbContext.Blogs.FirstOrDefaultAsync(b => b.Id == blog.Id);
            dbBlog.Should().NotBeNull();
            dbBlog.Title.Should().Be("UpdatedTitle");
        }

        [Test]
        public async Task DeleteBlogAsync_DeletesBlogFromDatabase()
        {

            // Arrange
            var blogRepository = new BlogRepository(dbContext);
            var blog = await dbContext.Blogs.FirstAsync();

            // Act
            await blogRepository.DeleteBlogAsync(blog);

            // Assert
            // Check if the blog is deleted from the database
            var dbBlog = await dbContext.Blogs.FirstOrDefaultAsync(b => b.Id == blog.Id);
            dbBlog.Should().BeNull();
        }
    }
}
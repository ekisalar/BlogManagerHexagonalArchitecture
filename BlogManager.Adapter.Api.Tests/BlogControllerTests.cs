using BlogManager.Adapter.Api.Controllers;
using BlogManager.Core.Commands.Blog;
using BlogManager.Core.DTOs;
using BlogManager.Core.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlogManager.Adapter.Api.Tests
{
    public class BlogControllerTests
    {
        [Test]
        public async Task CreateBlog_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock      = new Mock<IMediator>();
            var createBlogCommand = new CreateBlogCommand(Guid.NewGuid(), "TestTitle", "Test Description", "TestContent");
            var expectedResult    = new CreateBlogResponseDto();

            mediatorMock.Setup(m => m.Send(createBlogCommand, CancellationToken.None))
                        .ReturnsAsync(expectedResult);

            var controller = new BlogController(mediatorMock.Object);

            // Act
            var actionResult = await controller.CreateBlog(createBlogCommand);

            // Assert
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            var blogDto  = okResult.Value.Should().BeOfType<CreateBlogResponseDto>().Subject;

            blogDto.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task CreateBlog_ReturnsBadRequest()
        {
            // Arrange
            var mediatorMock      = new Mock<IMediator>();
            var createBlogCommand = new CreateBlogCommand(Guid.NewGuid(), "TestTitle", "Test Description", "TestContent");

            mediatorMock.Setup(m => m.Send(createBlogCommand, CancellationToken.None))
                        .ReturnsAsync((CreateBlogResponseDto) null); // Simulate a failure

            var controller = new BlogController(mediatorMock.Object);

            // Act
            var actionResult = await controller.CreateBlog(createBlogCommand);

            // Assert
            actionResult.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task GetBlog_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock   = new Mock<IMediator>();
            var blogId         = Guid.NewGuid();
            var authorInfo     = true;
            var expectedResult = new BlogDto();

            mediatorMock.Setup(m => m.Send(It.IsAny<GetBlogByIdQuery>(), CancellationToken.None))
                        .ReturnsAsync(expectedResult);

            var controller = new BlogController(mediatorMock.Object);

            // Act
            var actionResult = await controller.GetBlog(blogId.ToString(), authorInfo);

            // Assert
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            var blogDto  = okResult.Value.Should().BeOfType<BlogDto>().Subject;

            blogDto.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetBlog_ReturnsBadRequest()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var blogId       = Guid.NewGuid();
            var authorInfo   = true;

            mediatorMock.Setup(m => m.Send(It.IsAny<GetBlogByIdQuery>(), CancellationToken.None))
                        .ReturnsAsync((BlogDto) null); // Simulate a failure

            var controller = new BlogController(mediatorMock.Object);

            // Act
            var actionResult = await controller.GetBlog(blogId.ToString(), authorInfo);

            // Assert
            actionResult.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task GetBlogList_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock   = new Mock<IMediator>();
            var authorInfo     = true;
            var expectedResult = new List<BlogDto>();

            mediatorMock.Setup(m => m.Send(It.IsAny<GetBlogListQuery>(), CancellationToken.None))
                        .ReturnsAsync(expectedResult);

            var controller = new BlogController(mediatorMock.Object);

            // Act
            var actionResult = await controller.GetBlogList(authorInfo);

            // Assert
            var okResult    = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            var blogListDto = okResult.Value.Should().BeOfType<List<BlogDto>>().Subject;

            blogListDto.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetBlogList_ReturnsBadRequest()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var authorInfo   = true;

            mediatorMock.Setup(m => m.Send(It.IsAny<GetBlogListQuery>(), CancellationToken.None))
                        .ReturnsAsync((List<BlogDto>?) null); // Simulate a failure

            var controller = new BlogController(mediatorMock.Object);

            // Act
            var actionResult = await controller.GetBlogList(authorInfo);

            // Assert
            actionResult.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
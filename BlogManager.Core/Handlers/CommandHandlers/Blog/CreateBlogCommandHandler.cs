using BlogManager.Core.Commands.Blog;
using BlogManager.Core.Constants;
using BlogManager.Core.DTOs;
using BlogManager.Core.Repositories;
using MediatR;

namespace BlogManager.Core.Handlers.CommandHandlers.Blog;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, CreateBlogResponseDto?>
{
    private readonly IBlogRepository    _blogRepository;
    private readonly IBlogManagerLogger _logger;

    public CreateBlogCommandHandler(IBlogRepository blogRepository, IBlogManagerLogger logger)
    {
        _blogRepository = blogRepository;
        _logger         = logger;
    }

    public async Task<CreateBlogResponseDto?> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var blogToCreate   = await Domain.Blog.CreateAsync(request.AuthorId, request.Title, request.Description, request.Content);
        var blogNewCreated = await _blogRepository.AddBlogAsync(blogToCreate);
        _logger.LogInformation($"Blog with ID {blogNewCreated.Id} created successfully");
        return new CreateBlogResponseDto() {Id = blogNewCreated.Id};
    }
}
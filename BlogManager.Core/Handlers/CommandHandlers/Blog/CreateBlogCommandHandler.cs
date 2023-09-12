using BlogManager.Core.Commands.Blog;
using BlogManager.Core.DTOs;
using BlogManager.Core.Repositories;
using MediatR;

namespace BlogManager.Core.Handlers.CommandHandlers.Blog;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, CreateBlogResponseDto?>
{
    private readonly IBlogRepository _blogRepository;
    // private readonly IMediator       _mediator;

    public CreateBlogCommandHandler(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<CreateBlogResponseDto?> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var blogToCreate     = await Domain.Blog.CreateAsync(request.AuthorId, request.Title, request.Description, request.Content);
        var blogNewCreated   = await _blogRepository.AddBlogAsync(blogToCreate);
        return new CreateBlogResponseDto() {Id = blogNewCreated.Id};    
    }
}
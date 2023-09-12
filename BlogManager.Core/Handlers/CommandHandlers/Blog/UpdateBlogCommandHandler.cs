using BlogManager.Core.Commands.Blog;
using BlogManager.Core.Constants;
using BlogManager.Core.DTOs;
using BlogManager.Core.Repositories;
using Mapster;
using MediatR;

namespace BlogManager.Core.Handlers.CommandHandlers.Blog;

public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, UpdateBlogResponseDto?>
{
    private readonly IBlogRepository    _blogRepository;
    private readonly IBlogManagerLogger _logger;

    public UpdateBlogCommandHandler(IBlogRepository blogRepository, IBlogManagerLogger logger)
    {
        _blogRepository = blogRepository;
        _logger         = logger;
    }


    public async Task<UpdateBlogResponseDto?> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var blogToUpdate = await _blogRepository.GetBlogByIdAsync(request.Id, false, false);
        if (blogToUpdate is null)
            throw new Exception(ExceptionConstants.BlogNotFound);
        await Domain.Blog.UpdateAsync(blogToUpdate, request.AuthorId, request.Title, request.Description, request.Content);
        var result = await _blogRepository.UpdateAsync(blogToUpdate);
        _logger.LogInformation($"Blog with ID {request.Id} updated successfully.");
        return result.Adapt<UpdateBlogResponseDto>();
    }
}
using BlogManager.Core.Commands.Blog;
using BlogManager.Core.Constants;
using BlogManager.Core.DTOs;
using BlogManager.Core.Repositories;
using MediatR;

namespace BlogManager.Core.Handlers.CommandHandlers.Blog;

public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, DeleteBlogResponseDto>
{
    private readonly IBlogRepository _blogRepository;

    public DeleteBlogCommandHandler(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }


    public async Task<DeleteBlogResponseDto> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        var blogToDelete = await _blogRepository.GetBlogByIdAsync(request.Id, false);
        if (blogToDelete is null)
            throw new Exception(ExceptionConstants.BlogNotFound);
        await Domain.Blog.DeleteAsync(blogToDelete);
        await _blogRepository.DeleteBlogAsync(blogToDelete);
        return new DeleteBlogResponseDto() {Id = blogToDelete.Id};
    }
}
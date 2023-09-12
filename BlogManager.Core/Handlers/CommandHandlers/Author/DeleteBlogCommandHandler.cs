using BlogManager.Core.Commands.Author;
using BlogManager.Core.Constants;
using BlogManager.Core.DTOs;
using BlogManager.Core.Repositories;
using MediatR;

namespace BlogManager.Core.Handlers.CommandHandlers.Author;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, DeleteAuthorResponseDto>
{
    private readonly IAuthorRepository  _authorRepository;
    private readonly IBlogManagerLogger _blogManagerLogger;

    public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IBlogManagerLogger blogManagerLogger)
    {
        _authorRepository  = authorRepository;
        _blogManagerLogger = blogManagerLogger;
    }


    public async Task<DeleteAuthorResponseDto> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorToDelete = await _authorRepository.GetAuthorByIdAsync(request.Id, false);
        if (authorToDelete is null)
            throw new Exception(ExceptionConstants.AuthorNotFound);
        await Domain.Author.DeleteAsync(authorToDelete);
        await _authorRepository.DeleteAuthorAsync(authorToDelete);
        _blogManagerLogger.LogInformation($"Author with ID {request.Id}  deleted successfully.");
        return new DeleteAuthorResponseDto() {Id = authorToDelete.Id};
    }
}
using BlogManager.Core.Commands.Author;
using BlogManager.Core.Constants;
using BlogManager.Core.DTOs;
using BlogManager.Core.Repositories;
using MediatR;

namespace BlogManager.Core.Handlers.CommandHandlers.Author;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, DeleteAuthorResponseDto>
{
    private readonly IAuthorRepository _authorRepository;

    public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }


    public async Task<DeleteAuthorResponseDto> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorToDelete = await _authorRepository.GetAuthorByIdAsync(request.Id, false);
        if (authorToDelete is null)
            throw new Exception(ExceptionConstants.AuthorNotFound);
        await Domain.Author.DeleteAsync(authorToDelete);
        await _authorRepository.DeleteAuthorAsync(authorToDelete);
        return new DeleteAuthorResponseDto() {Id = authorToDelete.Id};
    }
}
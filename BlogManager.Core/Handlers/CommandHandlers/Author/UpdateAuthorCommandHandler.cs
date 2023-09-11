using BlogManager.Core.Commands.Author;
using BlogManager.Core.Constants;
using BlogManager.Core.DTOs;
using BlogManager.Core.Repositories;
using Mapster;
using MediatR;

namespace BlogManager.Core.Handlers.CommandHandlers.Author;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, UpdateAuthorResponseDto?>
{
    private readonly IAuthorRepository _authorRepository;

    public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }


    public async Task<UpdateAuthorResponseDto?> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorToUpdate = await _authorRepository.GetAuthorByIdAsync(request.Id, false);
        if (authorToUpdate is null)
            throw new Exception(ExceptionConstants.AuthorNotFound);
        await Domain.Author.UpdateAsync(authorToUpdate, request.Name, request.Surname);
        var result = await _authorRepository.UpdateAsync(authorToUpdate);
        return result.Adapt<UpdateAuthorResponseDto>();
    }
}
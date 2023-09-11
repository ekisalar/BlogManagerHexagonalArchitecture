using BlogManager.Core.Commands.Author;
using BlogManager.Core.DTOs;
using BlogManager.Core.Repositories;
using MediatR;

namespace BlogManager.Core.Handlers.CommandHandlers.Author;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, CreateAuthorResponseDto?>
{
    private readonly IAuthorRepository _authorRepository;

    public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<CreateAuthorResponseDto?> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorToCreate = await Domain.Author.CreateAsync(request.Name, request.Surname);
        var addAuthorAsync = await _authorRepository.AddAuthorAsync(authorToCreate);
        return new CreateAuthorResponseDto() {Id = addAuthorAsync.Id};
    }
}
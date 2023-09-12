using BlogManager.Core.Commands.Author;
using BlogManager.Core.Constants;
using BlogManager.Core.DTOs;
using BlogManager.Core.Repositories;
using MediatR;

namespace BlogManager.Core.Handlers.CommandHandlers.Author;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, CreateAuthorResponseDto?>
{
    private readonly IAuthorRepository  _authorRepository;
    private readonly IBlogManagerLogger _logger;

    public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IBlogManagerLogger logger)
    {
        _authorRepository = authorRepository;
        _logger           = logger;
    }

    public async Task<CreateAuthorResponseDto?> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorToCreate = await Domain.Author.CreateAsync(request.Name, request.Surname);
        var addAuthorAsync = await _authorRepository.AddAuthorAsync(authorToCreate);
        _logger.LogInformation(LoggingConstants.AuthorCreatedSuccessfully);

        return new CreateAuthorResponseDto() {Id = addAuthorAsync.Id};
    }
}
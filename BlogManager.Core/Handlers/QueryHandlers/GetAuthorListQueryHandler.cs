using BlogManager.Core.Constants;
using BlogManager.Core.DTOs;
using BlogManager.Core.Queries;
using BlogManager.Core.Repositories;
using Mapster;
using MediatR;

namespace BlogManager.Core.Handlers.QueryHandlers;

public class GetAuthorListQueryHandler : IRequestHandler<GetAuthorListQuery, List<AuthorDto>?>
{
    private readonly IAuthorRepository  _authorRepository;
    private readonly IBlogManagerLogger _logger;

    public GetAuthorListQueryHandler(IAuthorRepository authorRepository, IBlogManagerLogger logger)
    {
        _authorRepository = authorRepository;
        _logger           = logger;
    }

    public async Task<List<AuthorDto>?> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAuthorsAsync();
        _logger.LogInformation(LoggingConstants.AuthorListGetSuccessfully);

        return authors?.Adapt<List<AuthorDto>>();
    }
}
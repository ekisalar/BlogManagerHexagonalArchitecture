using BlogManager.Core.DTOs;
using BlogManager.Core.Queries;
using BlogManager.Core.Repositories;
using Mapster;
using MediatR;

namespace BlogManager.Core.Handlers.QueryHandlers;

public class GetAuthorListQueryHandler : IRequestHandler<GetAuthorListQuery, List<AuthorDto>?>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorListQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<List<AuthorDto>?> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAuthorsAsync();
        return authors?.Adapt<List<AuthorDto>>();
    }
}
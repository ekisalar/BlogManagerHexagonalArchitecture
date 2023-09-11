using BlogManager.Core.DTOs;
using BlogManager.Core.Queries;
using BlogManager.Core.Repositories;
using Mapster;
using MediatR;

namespace BlogManager.Core.Handlers.QueryHandlers;

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto?>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }


    public async Task<AuthorDto?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetAuthorByIdAsync(request.Id);
        return author?.Adapt<AuthorDto>();
    }
}
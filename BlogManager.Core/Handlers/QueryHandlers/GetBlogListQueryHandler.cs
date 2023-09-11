using BlogManager.Core.DTOs;
using BlogManager.Core.Queries;
using BlogManager.Core.Repositories;
using Mapster;
using MediatR;

namespace BlogManager.Core.Handlers.QueryHandlers;

public class GetBlogListQueryHandler : IRequestHandler<GetBlogListQuery, List<BlogDto>?>
{
    private readonly IBlogRepository _blogRepository;

    public GetBlogListQueryHandler(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<List<BlogDto>?> Handle(GetBlogListQuery request, CancellationToken cancellationToken)
    {
        var blogs = await _blogRepository.GetAllBlogsAsync(request.IncludeAuthorInfo);
        return blogs?.Adapt<List<BlogDto>>();
    }
}
using BlogManager.Core.DTOs;
using BlogManager.Core.Queries;
using Mapster;
using MediatR;

namespace BlogManager.Core.Handlers.QueryHandlers;

public class GetBlogListQueryHandler : IRequestHandler<GetBlogListQuery, GetBlogListResponseDto>
{
    private readonly IBlogRepository _blogRepository;

    public GetBlogListQueryHandler(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<GetBlogListResponseDto> Handle(GetBlogListQuery request, CancellationToken cancellationToken)
    {
        var blogs = await _blogRepository.GetAllBlogsAsync(request.IncludeAuthorInfo);
        return new GetBlogListResponseDto() {Blogs = blogs?.Adapt<List<BlogDto>>()};
    }
}
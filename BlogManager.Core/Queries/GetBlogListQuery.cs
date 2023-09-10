using BlogManager.Core.DTOs;
using MediatR;

namespace BlogManager.Core.Queries;

public class GetBlogListQuery : IRequest<GetBlogListResponseDto>
{
    public bool IncludeAuthorInfo { get; set; } = false;
}
using BlogManager.Core.DTOs;
using MediatR;

namespace BlogManager.Core.Commands.Blog;

public class UpdateBlogCommand : IRequest<UpdateBlogResponseDto>
{
    public Guid   Id          { get; set; }
    public Guid   AuthorId    { get; set; }
    public string Title       { get; set; }
    public string Description { get; set; }
    public string Content     { get; set; }
}
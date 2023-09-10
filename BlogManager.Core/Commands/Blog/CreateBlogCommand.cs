using BlogManager.Core.DTOs;
using MediatR;

namespace BlogManager.Core.Commands.Blog;

public class CreateBlogCommand : IRequest<CreateBlogResponseDto?>
{
    public Guid   AuthorId    { get; set; }
    public string Title       { get; set; }
    public string Description { get; set; }
    public string Content     { get; set; }
}
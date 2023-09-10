using BlogManager.Core.DTOs;
using MediatR;

namespace BlogManager.Core.Commands.Blog;

public class DeleteBlogCommand : IRequest<DeleteBlogResponseDto>
{
   public Guid Id { get; set; }
}
using BlogManager.Core.DTOs;
using MediatR;

namespace BlogManager.Core.Queries;

public class GetBlogByIdQuery : IRequest<BlogDto>
{
    public Guid Id { get; set; }
}
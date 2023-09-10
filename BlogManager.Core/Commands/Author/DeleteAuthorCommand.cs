using BlogManager.Core.DTOs;
using MediatR;

namespace BlogManager.Core.Commands.Author;

public class DeleteAuthorCommand : IRequest<DeleteAuthorResponseDto>
{
   public Guid Id { get; set; }
}
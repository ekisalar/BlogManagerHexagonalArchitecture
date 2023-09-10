using BlogManager.Core.DTOs;
using MediatR;

namespace BlogManager.Core.Commands.Author;

public class CreateAuthorCommand : IRequest<CreateAuthorResponseDto?>
{
    public Guid   Id { get; set; }
    public string Name     { get; set; }
    public string Surname  { get; set; }
}
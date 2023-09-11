using MediatR;

namespace BlogManager.Core.Events;

public class BlogCreatedEvent:IRequest
{
    public Guid       Id          { get; set; }
    public Guid       AuthorId    { get; set; }
    public string     Title       { get; set; }
    public string     Description { get; set; }
    public string     Content     { get; set; }
}
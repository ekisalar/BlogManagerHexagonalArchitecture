namespace BlogManager.Core.Events;

public class BlogUpdatedEvent
{
    public Guid   Id          { get; set; }
    public Guid   AuthorId    { get; set; }
    public string Title       { get; set; }
    public string Description { get; set; }
    public string Content     { get; set; }
}
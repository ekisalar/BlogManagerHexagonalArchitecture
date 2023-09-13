using System.Xml.Serialization;
using BlogManager.Core.DTOs;
using MediatR;

namespace BlogManager.Core.Commands.Blog;

public class UpdateBlogCommand : IRequest<UpdateBlogResponseDto>
{

    public UpdateBlogCommand()
    {
        
    }
    
    [XmlElement("id")]
    public Guid   Id          { get; set; }
    
    [XmlElement("authorId")]
    public Guid   AuthorId    { get; set; }
    
    [XmlElement("title")]   
    public string Title       { get; set; }
    
    [XmlElement("description")]
    public string Description { get; set; }
    
    [XmlElement("content")]
    public string Content     { get; set; }
}
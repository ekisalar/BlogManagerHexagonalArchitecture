using System.Xml.Serialization;
using BlogManager.Core.DTOs;
using MediatR;

namespace BlogManager.Core.Commands.Author;

public class UpdateAuthorCommand : IRequest<UpdateAuthorResponseDto>
{
    //For Xml Serializer
    public UpdateAuthorCommand()
    {
    }
    
    [XmlElement("id")]
    public Guid   Id      { get; set; }
    
    [XmlElement("name")]
    public string Name    { get; set; }
    
    [XmlElement("surname")]
    public string Surname { get; set; }
}
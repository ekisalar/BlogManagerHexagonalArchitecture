using System.Xml.Serialization;
using BlogManager.Core.DTOs;
using MediatR;

namespace BlogManager.Core.Commands.Author;

public class DeleteAuthorCommand : IRequest<DeleteAuthorResponseDto>
{
   //For Xml Serializer
   public DeleteAuthorCommand()
   {
      
   }
   
   [XmlElement("id")]
   public Guid Id { get; set; }
}
using BlogManager.Core.Commands.Blog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogManager.Adapter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    private readonly IMediator _mediator;

    public BlogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "CreateBlog")]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogCommand createBlogCommand)
    {
       var result = await _mediator.Send(createBlogCommand);
        if (result != null)
            return Ok(result);

        return BadRequest("Failed To Create The Blog");
    }
}
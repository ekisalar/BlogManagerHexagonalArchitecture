namespace BlogManager.Core.DTOs;

public class GetBlogListResponseDto
{
    public IList<BlogDto>? Blogs { get; set; }
}
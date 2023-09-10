using FluentValidation;

namespace BlogManager.Core.Domain;

public class Blog
{
    public Guid   Id          { get; private set; }
    public Guid   AuthorId    { get; private set; }
    public string Title       { get; private set; }
    public string Description { get; private set; }
    public string Content     { get; private set; }
    public Author Author      { get; private set; }

    private Blog(Guid authorId, string title, string description, string content)
    {
        AuthorId    = authorId;
        Title       = title;
        Description = description;
        Content     = content;
    }

    public static async Task<Blog> CreateAsync(Guid authorId, string title, string description, string content)
    {
        var blogToCreate     = new Blog(authorId, title, description, content);
        var validator        = new CreateBlogValidator();
        var validationResult = await validator.ValidateAsync(blogToCreate);
        if (validationResult.IsValid)
            return blogToCreate;
        throw new Exception(validationResult.Errors.ToString());
    }

    public static async Task<Blog> UpdateAsync(Blog blogToUpdate, Guid authorId, string title, string description, string content)
    {
        blogToUpdate.AuthorId    = authorId;
        blogToUpdate.Title       = title;
        blogToUpdate.Description = description;
        blogToUpdate.Content     = content;
        var validator        = new UpdateBlogValidator();
        var validationResult = await validator.ValidateAsync(blogToUpdate);
        if (validationResult.IsValid)
            return blogToUpdate;
        throw new Exception(validationResult.Errors.ToString());
    }

    public static async Task<Blog> DeleteAsync(Blog blogToDelete)
    {
        var validator        = new DeleteBlogValidator();
        var validationResult = await validator.ValidateAsync(blogToDelete);
        if (validationResult.IsValid)
            return blogToDelete;
        throw new Exception(validationResult.Errors.ToString());
    }

    private class CreateBlogValidator : AbstractValidator<Blog>
    {
        public CreateBlogValidator()
        {
            RuleFor(blog => blog.AuthorId).NotEmpty();
            RuleFor(blog => blog.Title).NotEmpty().MaximumLength(150);
            RuleFor(blog => blog.Description).NotEmpty().MaximumLength(500);
            RuleFor(blog => blog.Content).NotEmpty().MaximumLength(1500);
        }
    }

    private class UpdateBlogValidator : AbstractValidator<Blog>
    {
        public UpdateBlogValidator()
        {
            RuleFor(blog => blog.Id).NotEmpty();
            RuleFor(blog => blog.AuthorId).NotEmpty();
            RuleFor(blog => blog.Title).NotEmpty().MaximumLength(150);
            RuleFor(blog => blog.Description).NotEmpty().MaximumLength(500);
            RuleFor(blog => blog.Content).NotEmpty().MaximumLength(1500);
        }
    }

    private class DeleteBlogValidator : AbstractValidator<Blog>
    {
        public DeleteBlogValidator()
        {
            RuleFor(blog => blog.Id).NotEmpty();
        }
    }
}
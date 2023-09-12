// See https://aka.ms/new-console-template for more information

using BlogManager.Adapter.Api;
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
                   .SetBasePath(AppContext.BaseDirectory) // Set the base path to your application's directory
                   .AddJsonFile("appsettings.json") // Load configuration from appsettings.json
                   .Build();

var api = new ApiAdapter(args, options =>
{
    options.AddScoped<IAuthorRepository, AuthorRepository>();
    options.AddScoped<IBlogRepository, BlogRepository>();
    options.AddDbContext<IBlogDbContext, BlogDbContext>(c => c.UseNpgsql(configuration.GetConnectionString("BlogDb")));
});

await api.StartAsync();
Console.WriteLine("Hello, World!");
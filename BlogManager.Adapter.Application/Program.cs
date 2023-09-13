
using BlogManager.Adapter.Api;
using BlogManager.Adapter.Logger;
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core;
using BlogManager.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
                   .SetBasePath(AppContext.BaseDirectory) 
                   .AddJsonFile("appsettings.json") 
                   .Build();

var api = new ApiAdapter(args, options =>
{
    options.AddScoped<IAuthorRepository, AuthorRepository>();
    options.AddScoped<IBlogRepository, BlogRepository>();
    options.AddDbContext<IBlogDbContext, BlogDbContext>(c => c.UseNpgsql(configuration.GetConnectionString("BlogDb")));
    options.AddSingleton<IBlogManagerLogger, SerilogAdapter>();
});

await api.StartAsync();

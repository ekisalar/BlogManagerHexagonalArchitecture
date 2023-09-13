using BlogManager.Adapter.Api;
using BlogManager.Adapter.Logger;
using BlogManager.Adapter.PostgreSQL.DbContext;
using BlogManager.Adapter.PostgreSQL.Repositories;
using BlogManager.Core;
using BlogManager.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

var configuration = new ConfigurationBuilder()
                   .SetBasePath(AppContext.BaseDirectory)
                   // .AddJsonFile("appsettings.json")
                   .AddJsonFile("appsettings.json",                    optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                   .Build();


var migrationServiceProvider = new ServiceCollection()
                              .AddDbContext<BlogDbContext>(options =>
                                                               options.UseNpgsql(
                                                                                 configuration.GetConnectionString("BlogDb"),
                                                                                 b => b.MigrationsAssembly(typeof(BlogDbContext).Assembly.FullName)
                                                                                )
                                                          )
                              .BuildServiceProvider();

using (var scope = migrationServiceProvider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
    dbContext.Database.Migrate();
}

var api = new ApiAdapter(args, options =>
{
    options.AddScoped<IAuthorRepository, AuthorRepository>();
    options.AddScoped<IBlogRepository, BlogRepository>();
    options.AddDbContext<IBlogDbContext, BlogDbContext>(c => c.UseNpgsql(configuration.GetConnectionString("BlogDb")));
    options.AddSingleton<IBlogManagerLogger, SerilogAdapter>();
});

await api.StartAsync();
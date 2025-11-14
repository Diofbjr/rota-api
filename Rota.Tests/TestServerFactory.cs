using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rota.Api;
using Rota.Api.Data;

namespace Rota.Tests;

public class TestServerFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test"); // <- Isso é crucial!

        builder.ConfigureServices(services =>
        {
            // Remover qualquer registro antigo (caso exista)
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<RotaDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<RotaDbContext>(options =>
            {
                options.UseInMemoryDatabase("RotaTestDb");
            });

            // Inicializar banco limpo
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<RotaDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        });
    }
}

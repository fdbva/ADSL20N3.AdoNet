using Data.Repositories;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Crosscutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IAutorRepository, AutorSqlRepository>();
            services.AddSingleton<ILivroRepository, LivroSqlRepository>();
            services.AddSingleton<IAutorService, AutorService>();
            services.AddSingleton<ILivroService, LivroService>();
        }
    }
}

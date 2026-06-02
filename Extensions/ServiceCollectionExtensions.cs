using HorsePedigree_2026.Data;
using HorsePedigree_2026.Repositories;
using HorsePedigree_2026.Services;
using HorsePedigree_2026.Services.Export;
using Microsoft.EntityFrameworkCore;

namespace HorsePedigree_2026.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("Supabase")
            ?? configuration.GetConnectionString("DefaultConnection")
            ?? Environment.GetEnvironmentVariable("SUPABASE_CONNECTION_STRING")
            ?? Environment.GetEnvironmentVariable("ConnectionStrings__Supabase");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Configure la cadena de conexión en ConnectionStrings:Supabase (appsettings.json), " +
                "user-secrets, variables de entorno SUPABASE_CONNECTION_STRING o ConnectionStrings__Supabase.");
        }

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }

    public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICampeonatoRepository, CampeonatoRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ICriaderoRepository, CriaderoRepository>();
        services.AddScoped<IEquinoRepository, EquinoRepository>();
        services.AddScoped<IEquinoCampeonatoRepository, EquinoCampeonatoRepository>();
        services.AddScoped<IEstadoRepository, EstadoRepository>();
        services.AddScoped<IPropietarioRepository, PropietarioRepository>();
        services.AddScoped<IRolRepository, RolRepository>();
        services.AddScoped<ITipoDePasoRepository, TipoDePasoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICampeonatoService, CampeonatoService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<ICriaderoService, CriaderoService>();
        services.AddScoped<IEquinoService, EquinoService>();
        services.AddScoped<IEquinoCampeonatoService, EquinoCampeonatoService>();
        services.AddScoped<IEstadoService, EstadoService>();
        services.AddScoped<IPropietarioService, PropietarioService>();
        services.AddScoped<IRolService, RolService>();
        services.AddScoped<ITipoDePasoService, TipoDePasoService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IReporteService, ReporteService>();
        services.AddSingleton<IEquinosPorEstadoPdfGenerator, EquinosPorEstadoPdfGenerator>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();
        return services;
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HorsePedigree_2026.Configuration;
using HorsePedigree_2026.Constants;
using HorsePedigree_2026.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HorsePedigree_2026.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
            ?? throw new InvalidOperationException(
                $"Configure la sección '{JwtSettings.SectionName}' en appsettings.json o user-secrets.");

        if (string.IsNullOrWhiteSpace(jwtSettings.Key) || jwtSettings.Key.Length < 32)
        {
            throw new InvalidOperationException(
                "Jwt:Key debe tener al menos 32 caracteres. Use user-secrets o variables de entorno en producción.");
        }

        services.AddMemoryCache();
        services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ClockSkew = TimeSpan.FromMinutes(1)
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var blacklist = context.HttpContext.RequestServices
                            .GetRequiredService<ITokenBlacklistService>();
                        var jti = context.Principal?.FindFirstValue(JwtRegisteredClaimNames.Jti);

                        if (!string.IsNullOrWhiteSpace(jti) && blacklist.IsRevoked(jti))
                        {
                            context.Fail("La sesión fue cerrada.");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPolicies.Administrador, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(AuthClaimTypes.RolId, RolIds.Administrador.ToString());
            });
        });

        return services;
    }
}
